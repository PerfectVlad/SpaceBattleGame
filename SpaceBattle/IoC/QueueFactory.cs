using System.Collections.Generic;
using SpaceBattle.Interfaces;
using SpaceBattle.Adapters;

namespace SpaceBattle.IoC
{
    public static class QueueFactory
    {
        public static void Register()
        {
            IoCContainer.Register("OperationQueue.Create", args =>
            {
                string operationName = (string)args[0];
                object target = args[1];

                // Создаем очередь для операций
                var queue = new QueueAdapter<ICommand>();

                // Сохраняем очередь для последующего доступа
                IoCContainer.Register($"{operationName}.Queue", _ => queue);

                return queue;
            });
        }
    }
}