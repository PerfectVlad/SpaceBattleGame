using System;
using SpaceBattle.IoC;
using SpaceBattle.Interfaces;

namespace SpaceBattle.Examples
{
    public class MacroCommandExample
    {
        public static void Run()
        {
            // Инициализируем все регистрации
            CommandRegistration.RegisterAll();
            QueueFactory.Register();

            // Создаем объект для операций
            var spaceship = new object();

            // Создаем макрокоманду Move
            Console.WriteLine("=== Задача 1: Макрокоманда ===");
            var moveMacro = IoCContainer.Resolve<ICommand>(
                "MacroCommand.Create", "Move", spaceship);
            
            // Выполняем макрокоманду
            moveMacro.Execute();

            // Создаем длительную операцию
            Console.WriteLine("\n=== Задача 2: Длительная операция ===");
            
            // Создаем длительную операцию
            var longOp = IoCContainer.Resolve<LongOperationInfo>(
                "LongOperation.Create", "Move", spaceship);

            // Запускаем операцию
            var startCmd = IoCContainer.Resolve<ICommand>("Move.Start", spaceship);
            startCmd.Execute();

            // Останавливаем операцию
            var stopCmd = IoCContainer.Resolve<ICommand>("Move.Stop", spaceship);
            stopCmd.Execute();
        }
    }
}