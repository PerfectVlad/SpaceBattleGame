using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization; 

namespace SpaceBattle.Collision
{
    public class DataFileParser
    {
        public List<FeatureVector> ParseFile(string filePath)
        {
            var result = new List<FeatureVector>();
            
            foreach (var line in File.ReadLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                    continue;
                    
                var parts = line.Split(',');
                
                var features = new double[parts.Length - 1];
                for (int i = 0; i < parts.Length - 1; i++)
                {
                    features[i] = double.Parse(parts[i].Trim(), CultureInfo.InvariantCulture);
                }
                
                bool isCollision = parts[parts.Length - 1].Trim() == "1";
                
                result.Add(new FeatureVector(features, isCollision));
            }
            
            return result;
        }
        
        public void GenerateSampleFile(string filePath)
        {
            var random = new Random();
            var lines = new List<string>();
            
            lines.Add("# distance, speed1, speed2, angle, collision");
            
            for (int i = 0; i < 100; i++)
            {
                double distance = random.NextDouble() * 100;
                double speed1 = random.NextDouble() * 50;
                double speed2 = random.NextDouble() * 50;
                double angle = random.NextDouble() * 360;
                
                bool collision = distance < 20;
                
                lines.Add(string.Format(CultureInfo.InvariantCulture,
                    "{0:F2},{1:F2},{2:F2},{3:F2},{4}",
                    distance, speed1, speed2, angle, collision ? 1 : 0));
            }
            
            File.WriteAllLines(filePath, lines);
        }
    }
}