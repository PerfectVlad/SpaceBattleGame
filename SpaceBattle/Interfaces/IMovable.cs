using SpaceBattle.Exceptions;

namespace SpaceBattle.Interfaces
{
    public interface IMovable
    {
        (int x, int y) GetPosition();

        (int x, int y) GetVelocity();

        void SetPosition((int x, int y) newPosition);
    }
}