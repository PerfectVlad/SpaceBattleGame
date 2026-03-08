using System;
using System.Collections.Generic;
using SpaceBattle.Interfaces;
using SpaceBattle.Commands;

namespace SpaceBattle.IoC
{
    public static class LongRunningOperationStrategy
    {
        public static void Register()
        {
            IoCContainer.Register("LongOperation.Create", args =>
            {
                string operationName = (string)args[0];
                object target = args[1];

                var macroCommand = IoCContainer.Resolve<ICommand>(
                    "MacroCommand.Create", operationName, target);

                var queue = IoCContainer.Resolve<IQueue<ICommand>>(
                    "OperationQueue.Create", operationName, target);

                var bridgeCommand = new BridgeCommand(macroCommand);

                var operationInfo = new LongOperationInfo
                {
                    Name = operationName,
                    Target = target,
                    Command = bridgeCommand,
                    Queue = queue
                };

                RegisterStartCommand(operationInfo);
                RegisterStopCommand(operationInfo);

                return operationInfo;
            });
        }

        private static void RegisterStartCommand(LongOperationInfo info)
        {
            IoCContainer.Register($"{info.Name}.Start", args =>
            {
                return new StartLongOperationCommand(info);
            });
        }

        private static void RegisterStopCommand(LongOperationInfo info)
        {
            IoCContainer.Register($"{info.Name}.Stop", args =>
            {
                return new StopLongOperationCommand(info);
            });
        }
    }

    public class LongOperationInfo
    {
        public string Name { get; set; } = string.Empty;  // Инициализируем пустой строкой
        public object Target { get; set; } = new object();  // Инициализируем новым объектом
        public BridgeCommand Command { get; set; } = null!;  
        public IQueue<ICommand> Queue { get; set; } = null!;  
    }

    public class StartLongOperationCommand : ICommand
    {
        private readonly LongOperationInfo info;

        public StartLongOperationCommand(LongOperationInfo info)
        {
            this.info = info;
        }

        public void Execute()
        {
            info.Queue.Enqueue(info.Command);
            Console.WriteLine($"Операция {info.Name} запущена");
        }
    }

    public class StopLongOperationCommand : ICommand
    {
        private readonly LongOperationInfo info;

        public StopLongOperationCommand(LongOperationInfo info)
        {
            this.info = info;
        }

        public void Execute()
        {
            info.Command.Inject(new EmptyCommand());  
            Console.WriteLine($"Операция {info.Name} остановлена");
        }
    }
}