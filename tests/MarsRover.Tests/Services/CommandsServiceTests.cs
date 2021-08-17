using FluentAssertions;
using MarsRover.Domain.Commands;
using MarsRover.Domain.Interfaces;
using MarsRover.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace MarsRover.Tests.Services
{
    public class CommandsServiceTests
    {
        private readonly Mock<ILogger<ICommandsService>> _mockLogger;
        private readonly CommandsService _commandsService;

        public CommandsServiceTests()
        {
            _mockLogger = new Mock<ILogger<ICommandsService>>();
            _commandsService = new CommandsService(_mockLogger.Object);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GetCommands_CommandsIsNullorEmpty_ShouldThrowArgumentNullException(string commands)
        {
            Assert.Throws<ArgumentNullException>(() => _commandsService.GetCommands(commands));
        }

        [Theory]
        [InlineData("AALAARALA")]
        [InlineData("RAAALLAARR")]
        [InlineData("LARAAAARR")]
        [InlineData("arlAAlLL")]
        public void GetCommands_CommandsIsNotNullAndValidCommands_ShouldReturnCommandList_CountShouldEqualWithCommandsLength(string commands)
        {
            var commandsList = _commandsService.GetCommands(commands);

            commandsList.Count.Should().Equals(commands.Length);
        }

        [Theory]
        [InlineData("AWWALAARAFLA")]
        [InlineData("RAACALFLAARSAR")]
        [InlineData("rrrAACAdFLAARSAR")]
        public void GetCommands_CommandsIsNotNullAndValidAndInvalidCommands_ShouldReturnCommandList_CountShouldEqualWithValidCommandsLength(string commands)
        {
            var commandsList = _commandsService.GetCommands(commands);

            var countValid = commands.ToUpper().TakeWhile(o => o == 'A' ||
                                                     o == 'L' ||
                                                     o == 'R').Count();

            commandsList.Count.Should().Equals(countValid);
        }

        [Theory]
        [InlineData("A", typeof(AdvanceCommand))]
        [InlineData("R", typeof(TurnRightCommand))]
        [InlineData("L", typeof(TurnLeftCommand))]
        public void GetCommands_IsNotNullAndValidSingleCommand_ShouldReturnCorrectCommand(string commandLetter, Type commandType)
        {
            var commandsList = _commandsService.GetCommands(commandLetter);

            commandsList.Count.Should().Equals(commandLetter.Length);

            var command = commandsList.FirstOrDefault();

            command.GetType().Should().Be(commandType);
        }
    }
}
