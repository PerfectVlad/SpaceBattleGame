using Xunit;
using SpaceBattle.Interfaces;
using SpaceBattle.Commands;
using SpaceBattle.Exceptions;

namespace SpaceBattle.Tests
{
    // Тестовый класс для имитации поворачивающегося объекта
    public class TestRotatable : IRotatable
    {
        private int angle;
        private int angularVelocity;
        private readonly bool canGetAngle;
        private readonly bool canGetAngularVelocity;
        private readonly bool canSetAngle;

        public TestRotatable(int angle, int angularVelocity, 
                           bool canGetAngle = true, bool canGetAngularVelocity = true, bool canSetAngle = true)
        {
            this.angle = angle;
            this.angularVelocity = angularVelocity;
            this.canGetAngle = canGetAngle;
            this.canGetAngularVelocity = canGetAngularVelocity;
            this.canSetAngle = canSetAngle;
        }

        public int GetAngle()
        {
            if (!canGetAngle)
                throw new UnableToGetAngleException();
            return angle;
        }

        public int GetAngularVelocity()
        {
            if (!canGetAngularVelocity)
                throw new UnableToGetAngularVelocityException();
            return angularVelocity;
        }

        public void SetAngle(int newAngle)
        {
            if (!canSetAngle)
                throw new UnableToSetAngleException();
            angle = newAngle;
        }

        // Вспомогательное свойство для проверки в тестах
        public int CurrentAngle => angle;
    }

    public class RotationTests
    {
        [Fact]
        public void Execute_RotateObject_ChangesAngleCorrectly()
        {
            // Arrange: корабль под углом 45° с угловой скоростью 90°
            var rotatable = new TestRotatable(45, 90);
            var rotateCommand = new RotateCommand(rotatable);

            // Act: выполняем поворот
            rotateCommand.Execute();

            // Assert: проверяем что угол стал 135°
            Assert.Equal(135, rotatable.CurrentAngle);
        }

        [Fact]
        public void Execute_RotateWithOverflow_WrapsCorrectly()
        {
            // Arrange: угол 300° + скорость 100° = 400° → 40°
            var rotatable = new TestRotatable(300, 100);
            var rotateCommand = new RotateCommand(rotatable);

            // Act
            rotateCommand.Execute();

            // Assert
            Assert.Equal(40, rotatable.CurrentAngle);
        }

        [Fact]
        public void Execute_RotateWithNegativeAngle_WrapsCorrectly()
        {
            // Arrange: угол 30° + скорость -50° = -20° → 340°
            var rotatable = new TestRotatable(30, -50);
            var rotateCommand = new RotateCommand(rotatable);

            // Act
            rotateCommand.Execute();

            // Assert
            Assert.Equal(340, rotatable.CurrentAngle);
        }

        [Fact]
        public void Execute_WhenCannotGetAngle_ThrowsUnableToGetAngleException()
        {
            // Arrange
            var rotatable = new TestRotatable(0, 0, canGetAngle: false);
            var rotateCommand = new RotateCommand(rotatable);

            // Act & Assert
            Assert.Throws<UnableToGetAngleException>(() => rotateCommand.Execute());
        }

        [Fact]
        public void Execute_WhenCannotGetAngularVelocity_ThrowsUnableToGetAngularVelocityException()
        {
            // Arrange
            var rotatable = new TestRotatable(0, 0, canGetAngularVelocity: false);
            var rotateCommand = new RotateCommand(rotatable);

            // Act & Assert
            Assert.Throws<UnableToGetAngularVelocityException>(() => rotateCommand.Execute());
        }

        [Fact]
        public void Execute_WhenCannotSetAngle_ThrowsUnableToSetAngleException()
        {
            // Arrange
            var rotatable = new TestRotatable(0, 0, canSetAngle: false);
            var rotateCommand = new RotateCommand(rotatable);

            // Act & Assert
            Assert.Throws<UnableToSetAngleException>(() => rotateCommand.Execute());
        }

        [Fact]
        public void Constructor_WhenRotatableIsNull_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new RotateCommand(null));
        }
    }
}