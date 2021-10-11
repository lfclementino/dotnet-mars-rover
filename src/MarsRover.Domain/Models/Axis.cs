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

                this.Validate<InvalidAxisValueException>($"{value}");
            }
        }
        public Axis(int value)
        {
            Value = value;
        }
        public void Increase(int step)
        {
            Value += step;
        }
        public void Decrease(int step)
        {
            Value -= step;
        }
    }
}
