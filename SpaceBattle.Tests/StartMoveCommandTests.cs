using Xunit;
using SpaceBattle.Interfaces;
using SpaceBattle.Commands;
using SpaceBattle.Adapters;
using System.Collections.Generic;

namespace SpaceBattle.Tests
{
    public class TestMoveCommandStartable : IMoveCommandStartable
    {
        public object UObject { get; set; }
        public (int x, int y) Velocity { get; set; }
        public IQueue<ICommand> Queue { get; set; }

        public object GetUObject() => UObject;
        public (int x, int y) GetVelocity() => Velocity;
        public IQueue<ICommand> GetQueue() => Queue;
    }

    public class StartMoveCommandTests
    {
        [Fact]
        public void Execute_StartMoveCommand_AddsCommandToQueue()
        {
            // Arrange
            var uObject = new object();
            var queue = new QueueAdapter<ICommand>();
            var startable = new TestMoveCommandStartable 
            { 
                UObject = uObject, 
                Velocity = (5, 10), 
                Queue = queue 
            };
            var startCommand = new StartMoveCommand(startable);

            // Act
            startCommand.Execute();

            // Assert
            Assert.Equal(1, queue.Count);
        }

        [Fact]
        public void Execute_StartMoveCommand_CreatesBridgeCommand()
        {
            // Arrange
            var uObject = new object();
            var queue = new QueueAdapter<ICommand>();
            var startable = new TestMoveCommandStartable 
            { 
                UObject = uObject, 
                Velocity = (5, 10), 
                Queue = queue 
            };
            var startCommand = new StartMoveCommand(startable);

            // Act
            startCommand.Execute();
            var command = queue.Dequeue();

            // Assert
            Assert.IsType<BridgeCommand>(command);
        }
    }
}