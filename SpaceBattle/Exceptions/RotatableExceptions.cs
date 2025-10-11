using System;

namespace SpaceBattle.Exceptions
{
    public class UnableToGetAngleException : Exception
    {
        public UnableToGetAngleException() : base("Unable to get angle") { }
    }

    public class UnableToGetAngularVelocityException : Exception
    {
        public UnableToGetAngularVelocityException() : base("Unable to get angular velocity") { }
    }

    public class UnableToSetAngleException : Exception
    {
        public UnableToSetAngleException() : base("Unable to set angle") { }
    }
}