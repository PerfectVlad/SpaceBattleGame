using System;
using System.Collections.Generic;
using SpaceBattle.Interfaces;

namespace SpaceBattle.Commands
{
    public class MacroCommand : ICommand
    {
        private readonly IEnumerable<ICommand> commands;

        public MacroCommand(IEnumerable<ICommand> commands)
        {
            this.commands = commands ?? throw new ArgumentNullException(nameof(commands));
        }

        public void Execute()
        {
            foreach (var command in commands)
            {
                command.Execute();
            }
        }
    }
}