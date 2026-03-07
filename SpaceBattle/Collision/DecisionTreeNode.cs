using System;
using System.Collections.Generic;

namespace SpaceBattle.Collision
{
    // Узел дерева решений
    public class DecisionTreeNode
    {
        public int FeatureIndex { get; set; }        // Индекс признака для проверки
        public double Threshold { get; set; }         // Пороговое значение
        public DecisionTreeNode? Left { get; set; }    // Левое поддерево (условие выполнено)
        public DecisionTreeNode? Right { get; set; }   // Правое поддерево (условие не выполнено)
        public bool? IsLeaf { get; set; }             // Является ли листом
        public bool? LeafValue { get; set; }          // Значение в листе (true - столкновение, false - нет)
        
        public DecisionTreeNode()
        {
        }
        
        public DecisionTreeNode(bool leafValue)
        {
            IsLeaf = true;
            LeafValue = leafValue;
        }
        
        public DecisionTreeNode(int featureIndex, double threshold)
        {
            FeatureIndex = featureIndex;
            Threshold = threshold;
            IsLeaf = false;
        }
    }
}