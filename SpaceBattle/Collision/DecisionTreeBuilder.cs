using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceBattle.Collision
{
    // дерево решений из векторов признаков
    public class DecisionTreeBuilder
    {
        private readonly int maxDepth;
        private readonly int minSamplesSplit;
        
        public DecisionTreeBuilder(int maxDepth = 5, int minSamplesSplit = 2)
        {
            this.maxDepth = maxDepth;
            this.minSamplesSplit = minSamplesSplit;
        }
        
        public DecisionTreeNode BuildTree(List<FeatureVector> data, int depth = 0)
        {
            // Если все примеры одного класса или достигли максимальной глубины
            if (depth >= maxDepth || data.Count < minSamplesSplit || AllSameClass(data))
            {
                return new DecisionTreeNode(MajorityClass(data));
            }
            
            // Находим лучшее разделение
            var bestSplit = FindBestSplit(data);
            
            if (bestSplit.Item1 == -1) // Не удалось найти хорошее разделение
            {
                return new DecisionTreeNode(MajorityClass(data));
            }
            
            var node = new DecisionTreeNode(bestSplit.Item1, bestSplit.Item2);
            
            // Разделяем данные
            var leftData = data.Where(v => v.Features[bestSplit.Item1] < bestSplit.Item2).ToList();
            var rightData = data.Where(v => v.Features[bestSplit.Item1] >= bestSplit.Item2).ToList();
            
            // Рекурсивно строим поддеревья
            node.Left = BuildTree(leftData, depth + 1);
            node.Right = BuildTree(rightData, depth + 1);
            
            return node;
        }
        
        private bool AllSameClass(List<FeatureVector> data)
        {
            return data.All(v => v.IsCollision == data[0].IsCollision);
        }
        
        private bool MajorityClass(List<FeatureVector> data)
        {
            int trueCount = data.Count(v => v.IsCollision);
            int falseCount = data.Count - trueCount;
            return trueCount >= falseCount;
        }
        
        private Tuple<int, double> FindBestSplit(List<FeatureVector> data)
        {
            int bestFeature = -1;
            double bestThreshold = 0;
            double bestGini = double.MaxValue;
            
            int featureCount = data[0].Features.Length;
            
            for (int feature = 0; feature < featureCount; feature++)
            {
                var values = data.Select(v => v.Features[feature]).Distinct().OrderBy(x => x).ToList();
                
                for (int i = 0; i < values.Count - 1; i++)
                {
                    double threshold = (values[i] + values[i + 1]) / 2;
                    
                    var leftData = data.Where(v => v.Features[feature] < threshold).ToList();
                    var rightData = data.Where(v => v.Features[feature] >= threshold).ToList();
                    
                    if (leftData.Count < minSamplesSplit || rightData.Count < minSamplesSplit)
                        continue;
                    
                    double gini = CalculateGini(leftData, rightData);
                    
                    if (gini < bestGini)
                    {
                        bestGini = gini;
                        bestFeature = feature;
                        bestThreshold = threshold;
                    }
                }
            }
            
            return Tuple.Create(bestFeature, bestThreshold);
        }
        
        private double CalculateGini(List<FeatureVector> left, List<FeatureVector> right)
        {
            int total = left.Count + right.Count;
            
            double leftGini = 1 - Math.Pow(left.Count(v => v.IsCollision) / (double)left.Count, 2) 
                                 - Math.Pow(left.Count(v => !v.IsCollision) / (double)left.Count, 2);
            
            double rightGini = 1 - Math.Pow(right.Count(v => v.IsCollision) / (double)right.Count, 2) 
                                  - Math.Pow(right.Count(v => !v.IsCollision) / (double)right.Count, 2);
            
            return (left.Count / (double)total) * leftGini + (right.Count / (double)total) * rightGini;
        }
    }
}