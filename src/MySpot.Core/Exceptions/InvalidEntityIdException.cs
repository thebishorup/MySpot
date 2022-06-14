﻿namespace MySpot.Core.Exceptions
{
    public class InvalidEntityIdException : CustomException
    {
        public Guid Id { get; }
        public InvalidEntityIdException(Guid id) :
            base($"Invalid entity id - {id}.")
        {
            Id = id;
        }
    }
}
