using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Core.Exceptions
{
    public sealed class InvalidCapacityException : CustomException
    {
        public int Value { get; }
        public InvalidCapacityException(int capacity) 
            : base($"Invalid capacity {capacity} value. It must be greater that 0 or or less than 4")
        {
            Value = capacity;
        }
    }
}
