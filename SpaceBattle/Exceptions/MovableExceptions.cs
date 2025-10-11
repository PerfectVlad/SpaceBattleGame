using System;

namespace SpaceBattle.Exceptions
{
    public class UnableToGetPositionException : Exception
    {
        public UnableToGetPositionException() : base("Unable to get position") { }
    }

    public class UnableToGetVelocityException : Exception
    {
        public UnableToGetVelocityException() : base("Unable to get velocity") { }
    }

    public class UnableToSetPositionException : Exception
    {
        public UnableToSetPositionException() : base("Unable to set position") { }
    }
}