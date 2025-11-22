using SpaceBattle.Interfaces;
using SpaceBattle.Exceptions;

namespace SpaceBattle.Commands
{
    public class RotateCommand : ICommand
    {
        private readonly IRotatable rotatable;

        public RotateCommand(IRotatable rotatable)
        {
            this.rotatable = rotatable ?? throw new ArgumentNullException(nameof(rotatable));
        }

        public void Execute()
        {
            try
            {
                var angle = rotatable.GetAngle();
                var angularVelocity = rotatable.GetAngularVelocity();
                
                var newAngle = (angle + angularVelocity) % 360;
                
                if (newAngle < 0)
                {
                    newAngle += 360;
                }
                
                rotatable.SetAngle(newAngle);
            }
            catch (UnableToGetAngleException)
            {
                throw;
            }
            catch (UnableToGetAngularVelocityException)
            {
                throw;
            }
            catch (UnableToSetAngleException)
            {
                throw;
            }
        }
    }
}