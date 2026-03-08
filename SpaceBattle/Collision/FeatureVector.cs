using System;
using System.Collections.Generic;

namespace SpaceBattle.Collision
{
    // Вектор признаков для определения столкновения
    public class FeatureVector
    {
        public double[] Features { get; set; }      // Массив признаков
        public bool IsCollision { get; set; }        // Было ли столкновение (true/false)
        
        public FeatureVector(double[] features, bool isCollision)
        {
            Features = features;
            IsCollision = isCollision;
        }
    }
}