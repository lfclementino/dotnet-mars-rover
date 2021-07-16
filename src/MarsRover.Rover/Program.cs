using Microsoft.Extensions.DependencyInjection;
using System;

namespace MarsRover.Rover
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging();
        }
    }
}
