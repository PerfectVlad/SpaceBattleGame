using SpaceBattle.Interfaces;
using SpaceBattle.IoC;
using SpaceBattle.Collision;

namespace SpaceBattle.Commands
{
    public class CheckCollisionCommand : ICommand
    {
        private readonly object object1;
        private readonly object object2;
        
        public CheckCollisionCommand(object obj1, object obj2)
        {
            object1 = obj1;
            object2 = obj2;
        }
        
        public void Execute()
        {
            // Получаем классификатор из IoC
            var classifier = IoCContainer.Resolve<DecisionTreeClassifier>("CollisionClassifier");
            
            // Извлекаем признаки из объектов
            var features = ExtractFeatures(object1, object2);
            
            // Проверяем столкновение
            bool isCollision = classifier.Predict(features);
            
            if (isCollision)
            {
                Console.WriteLine($"Объекты столкнулись!");
            }
            else
            {
                Console.WriteLine($"Столкновения нет");
            }
        }
        
        private double[] ExtractFeatures(object obj1, object obj2)
        {
            var random = new Random();
            return new double[]
            {
                random.NextDouble() * 100,  // расстояние
                random.NextDouble() * 50,   // скорость1
                random.NextDouble() * 50,   // скорость2
                random.NextDouble() * 360   // угол
            };
        }
    }
}