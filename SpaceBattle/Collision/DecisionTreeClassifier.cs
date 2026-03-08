using System;

namespace SpaceBattle.Collision
{
    public class DecisionTreeClassifier
    {
        private readonly DecisionTreeNode root;
        
        public DecisionTreeClassifier(DecisionTreeNode root)
        {
            this.root = root;
        }
        
        public bool Predict(double[] features)
        {
            return PredictRecursive(root, features);
        }
        
        private bool PredictRecursive(DecisionTreeNode node, double[] features)
        {
            if (node.IsLeaf == true)
            {
                return node.LeafValue.Value;
            }
            
            if (features[node.FeatureIndex] < node.Threshold)
            {
                return PredictRecursive(node.Left, features);
            }
            else
            {
                return PredictRecursive(node.Right, features);
            }
        }
    }
}