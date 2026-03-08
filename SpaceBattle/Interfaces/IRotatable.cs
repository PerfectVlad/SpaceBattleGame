using SpaceBattle.Exceptions;

namespace SpaceBattle.Interfaces
{
    public interface IRotatable
    {
        int GetAngle();
        int GetAngularVelocity(); 
        void SetAngle(int newAngle);
    }
}