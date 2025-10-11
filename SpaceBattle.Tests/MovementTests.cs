using Xunit;
using SpaceBattle.Interfaces;
using SpaceBattle.Commands;
using SpaceBattle.Exceptions;

namespace SpaceBattle.Tests
{
    // Тестовый класс для имитации движущегося объекта
    public class TestMovable : IMovable
    {
        private (int x, int y) position;
        private (int x, int y) velocity;
        private readonly bool canGetPosition;
        private readonly bool canGetVelocity;
        private readonly bool canSetPosition;

        public TestMovable((int x, int y) pos, (int x, int y) vel, 
                          bool canGetPos = true, bool canGetVel = true, bool canSetPos = true)
        {
            position = pos;
            velocity = vel;
            canGetPosition = canGetPos;
            canGetVelocity = canGetVel;
            canSetPosition = canSetPos;
        }

        public (int x, int y) GetPosition()
        {
            if (!canGetPosition)
                throw new UnableToGetPositionException();
            return position;
        }

        public (int x, int y) GetVelocity()
        {
            if (!canGetVelocity)
                throw new UnableToGetVelocityException();
            return velocity;
        }

        public void SetPosition((int x, int y) newPosition)
        {
            if (!canSetPosition)
                throw new UnableToSetPositionException();
            position = newPosition;
        }

        // Вспомогательное свойство для проверки в тестах
        public (int x, int y) CurrentPosition => position;
    }

    public class MovementTests
    {
        [Fact]
        public void Execute_MoveObject_ChangesPositionCorrectly()
        {
            // Arrange
            var movable = new TestMovable((12, 5), (-7, 3));
            var moveCommand = new MoveCommand(movable);

            // Act
            moveCommand.Execute();

            // Assert
            Assert.Equal((5, 8), movable.CurrentPosition);
        }

        [Fact]
        public void Execute_WhenCannotGetPosition_ThrowsUnableToGetPositionException()
        {
            // Arrange
            var movable = new TestMovable((0, 0), (0, 0), canGetPos: false);
            var moveCommand = new MoveCommand(movable);

            // Act & Assert
            Assert.Throws<UnableToGetPositionException>(() => moveCommand.Execute());
        }

        [Fact]
        public void Execute_WhenCannotGetVelocity_ThrowsUnableToGetVelocityException()
        {
            // Arrange
            var movable = new TestMovable((0, 0), (0, 0), canGetVel: false);
            var moveCommand = new MoveCommand(movable);

            // Act & Assert
            Assert.Throws<UnableToGetVelocityException>(() => moveCommand.Execute());
        }

        [Fact]
        public void Execute_WhenCannotSetPosition_ThrowsUnableToSetPositionException()
        {
            // Arrange
            var movable = new TestMovable((0, 0), (0, 0), canSetPos: false);
            var moveCommand = new MoveCommand(movable);

            // Act & Assert
            Assert.Throws<UnableToSetPositionException>(() => moveCommand.Execute());
        }

        [Fact]
        public void Constructor_WhenMovableIsNull_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new MoveCommand(null));
        }
    }
}