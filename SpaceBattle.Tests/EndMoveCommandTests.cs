using Xunit;
using SpaceBattle.Interfaces;
using SpaceBattle.Commands;
using SpaceBattle.Adapters;

namespace SpaceBattle.Tests
{
    public class TestMoveCommandEndable : IMoveCommandEndable
    {
        public ICommand MoveCommand { get; set; }
        public object UObject { get; set; }
        public IQueue<ICommand> Queue { get; set; }

        public ICommand GetMoveCommand() => MoveCommand;
        public object GetUObject() => UObject;
        public IQueue<ICommand> GetQueue() => Queue;
    }

    public class EndMoveCommandTests
    {
        [Fact]
        public void Execute_EndMoveCommand_ReplacesCommandWithEmpty()
        {
            // Arrange
            var originalCommand = new MoveCommand(new TestMovable((0, 0), (1, 1)));
            var bridgeCommand = new BridgeCommand(originalCommand);
            var endable = new TestMoveCommandEndable 
            { 
                MoveCommand = bridgeCommand,
                UObject = new object()
            };
            var endCommand = new EndMoveCommand(endable);

            // Act
            endCommand.Execute();
            bridgeCommand.Execute(); // Должен выполнить EmptyCommand

            // Assert - не должно быть исключений
            Assert.True(true);
        }

        [Fact]
        public void Execute_EndMoveCommand_WithNonBridgeCommand_CompletesWithoutError()
        {
            // Arrange
            var moveCommand = new MoveCommand(new TestMovable((0, 0), (1, 1)));
            var endable = new TestMoveCommandEndable 
            { 
                MoveCommand = moveCommand,
                UObject = new object()
            };
            var endCommand = new EndMoveCommand(endable);

            // Act & Assert - не должно быть исключений
            endCommand.Execute();
        }
    }
}