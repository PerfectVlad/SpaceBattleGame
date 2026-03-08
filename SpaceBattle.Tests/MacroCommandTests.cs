using Xunit;
using System.Collections.Generic;
using SpaceBattle.Commands;
using SpaceBattle.Interfaces;
using SpaceBattle.IoC;

namespace SpaceBattle.Tests
{
    public class TestMacroInfo : IMacroCommandInfo
    {
        private readonly object target;
        private readonly IEnumerable<string> dependencies;

        public TestMacroInfo(object target, IEnumerable<string> deps)
        {
            this.target = target;
            this.dependencies = deps;
        }

        public IEnumerable<string> GetCommandDependencies() => dependencies;
        public object GetTarget() => target;
    }

    public class MacroCommandTests
    {
        public MacroCommandTests()
        {
            // Регистрируем тестовые команды
            IoCContainer.Register("TestCommand1", args => new TestCommand());
            IoCContainer.Register("TestCommand2", args => new TestCommand());
            IoCContainer.Register("TestCommand3", args => new TestCommand());

            // Регистрируем тестовую информацию
            IoCContainer.Register("Test.Info", args =>
                new TestMacroInfo(args[0], new[] { "TestCommand1", "TestCommand2" }));
        }

        [Fact]
        public void MacroCommand_Execute_ExecutesAllCommands()
        {
            // Arrange
            var commands = new List<ICommand>
            {
                new TestCommand(),
                new TestCommand(),
                new TestCommand()
            };
            var macro = new MacroCommand(commands);

            // Act
            macro.Execute();

            // Assert
            foreach (var cmd in commands)
            {
                Assert.True(((TestCommand)cmd).Executed);
            }
        }

        [Fact]
        public void MacroCommandStrategy_Create_ReturnsMacroCommand()
        {
            // Arrange
            MacroCommandStrategy.Register();
            var target = new object();

            // Act
            var macro = IoCContainer.Resolve<ICommand>(
                "MacroCommand.Create", "Test", target);

            // Assert
            Assert.IsType<MacroCommand>(macro);
        }

        [Fact]
        public void LongOperationStrategy_Create_ReturnsOperationInfo()
        {
            // Arrange
            CommandRegistration.RegisterAll();
            QueueFactory.Register();
            var target = new object();

            // Act
            var opInfo = IoCContainer.Resolve<LongOperationInfo>(
                "LongOperation.Create", "Test", target);

            // Assert
            Assert.NotNull(opInfo);
            Assert.Equal("Test", opInfo.Name);
            Assert.Equal(target, opInfo.Target);
            Assert.NotNull(opInfo.Command);
            Assert.NotNull(opInfo.Queue);
        }

        [Fact]
        public void StartLongOperation_AddsCommandToQueue()
        {
            // Arrange
            CommandRegistration.RegisterAll();
            QueueFactory.Register();
            var target = new object();

            var opInfo = IoCContainer.Resolve<LongOperationInfo>(
                "LongOperation.Create", "Test", target);

            var startCmd = new StartLongOperationCommand(opInfo);

            // Act
            startCmd.Execute();

            // Assert
            Assert.Equal(1, opInfo.Queue.Count);
        }

        [Fact]
        public void StopLongOperation_ReplacesCommandWithEmpty()
        {
            // Arrange
            CommandRegistration.RegisterAll();
            QueueFactory.Register();
            var target = new object();

            var opInfo = IoCContainer.Resolve<LongOperationInfo>(
                "LongOperation.Create", "Test", target);

            var originalCommand = opInfo.Command;

            // Act
            var stopCmd = new StopLongOperationCommand(opInfo);
            stopCmd.Execute();

            // Assert
            // Не должно быть исключений при выполнении
            opInfo.Command.Execute();
        }
    }

    public class TestCommand : ICommand
    {
        public bool Executed { get; private set; }

        public void Execute()
        {
            Executed = true;
        }
    }
}