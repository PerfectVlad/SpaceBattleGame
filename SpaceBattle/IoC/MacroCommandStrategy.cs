using System;
using System.Collections.Generic;
using System.Linq;
using SpaceBattle.Interfaces;
using SpaceBattle.Commands;

namespace SpaceBattle.IoC
{

    /// Стратегия создания макрокоманды по имени составной операции

    public static class MacroCommandStrategy
    {
        public static void Register()
        {
            // Регистрируем стратегию создания макрокоманды
            IoCContainer.Register("MacroCommand.Create", args =>
            {
                string operationName = (string)args[0];
                object target = args[1];

                // Получаем информацию о макрокоманде
                var macroInfo = IoCContainer.Resolve<IMacroCommandInfo>(
                    $"{operationName}.Info", target);

                // Получаем имена зависимостей команд
                var dependencyNames = macroInfo.GetCommandDependencies();

                // Создаем команды через IoC
                var commands = new List<ICommand>();
                foreach (var depName in dependencyNames)
                {
                    var command = IoCContainer.Resolve<ICommand>(depName, target);
                    commands.Add(command);
                }

                // Создаем и возвращаем макрокоманду
                return new MacroCommand(commands);
            });

            // Регистрируем пример информации для операции "Move"
            IoCContainer.Register("Move.Info", args =>
            {
                object target = args[0];
                return new MoveMacroInfo(target);
            });
        }
    }

    /// Пример информации для макрокоманды Move

    public class MoveMacroInfo : IMacroCommandInfo
    {
        private readonly object target;

        public MoveMacroInfo(object target)
        {
            this.target = target;
        }

        public IEnumerable<string> GetCommandDependencies()
        {
            // Команды, которые выполняются при движении
            return new[]
            {
                "CheckFuel",      // Проверить топливо
                "Move",           // Движение
                "BurnFuel"        // Потратить топливо
            };
        }

        public object GetTarget() => target;
    }
}