using System;
using System.Collections.Generic;
using SpaceBattle.Collision;

namespace SpaceBattle.IoC
{
    public static class CollisionIoCInitializer
    {
        public static void Initialize(string dataFilePath)
        {
            // Регистрируем парсер данных
            IoCContainer.Register("DataFileParser", args => new DataFileParser());
            
            // Регистрируем построитель дерева
            IoCContainer.Register("DecisionTreeBuilder", args => 
            {
                int maxDepth = args.Length > 0 ? (int)args[0] : 5;
                int minSamples = args.Length > 1 ? (int)args[1] : 2;
                return new DecisionTreeBuilder(maxDepth, minSamples);
            });
            
            // Загружаем данные и строим дерево
            var parser = IoCContainer.Resolve<DataFileParser>("DataFileParser");
            var data = parser.ParseFile(dataFilePath);
            
            var builder = IoCContainer.Resolve<DecisionTreeBuilder>("DecisionTreeBuilder");
            var tree = builder.BuildTree(data);
            
            var classifier = new DecisionTreeClassifier(tree);
            
            // Регистрируем классификатор в IoC
            IoCContainer.Register("CollisionClassifier", args => classifier);
        }
    }
}