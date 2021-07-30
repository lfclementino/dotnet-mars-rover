using MarsRover.Domain.Exceptions;
using MarsRover.Domain.Helper;
using System;
using System.ComponentModel.DataAnnotations;

namespace MarsRover.Domain.Models
{
    public class Axis
    {
        private int _value;

        [Range(0, int.MaxValue)]
        public int Value { 
            get => _value;
            set
            {
                _value = value;

                this.Validate(new OutOfBoundsException());
            }
        }
        public Axis(int value)
        {
            Value = value;
        }
        public void Increase()
        {
            Value += 1;
        }
        public void Decrease()
        {
            Value -= 1;
        }
    }
}
