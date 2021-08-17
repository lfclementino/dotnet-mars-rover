using MarsRover.Domain.Exceptions;
using MarsRover.Domain.Helper;
using System;
using System.ComponentModel.DataAnnotations;

namespace MarsRover.Domain.Models
{
    public class Floor
    {
        private int _width = 1;
        private int _height = 1;

        [Range(1, int.MaxValue)]
        public int Width 
        { 
            get => _width;
            set
            {
                _width = value;

                this.Validate<InvalidFloorSizeException>($"Width: {value}");
            } 
        }

        [Range(1, int.MaxValue)]
        public int Height 
        { 
            get => _height;
            set
            {
                _height = value;

                this.Validate<InvalidFloorSizeException>($"Height: {value}");
            }
        }

        public Floor(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
