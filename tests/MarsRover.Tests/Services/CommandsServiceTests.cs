using FluentAssertions;
using MarsRover.Domain.Interfaces;
using MarsRover.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
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
        [InlineData(null)]
        public void GetCommands_IsNull_ShouldReturnNull(string commands)
        {
            var commandsList = _commandsService.GetCommands(commands);

            commandsList.Should().BeNull();
        }

        [Theory]
        [InlineData("AALAARALA")]
        [InlineData("RAAALLAARR")]
        [InlineData("LARAAAARR")]
        [InlineData("arlAAlLL")]
        public void GetCommands_IsNotNullAndValidCommands_ShouldReturnCommandList_CountShouldEqualWithCommandsLength(string commands)
        {
            var commandsList = _commandsService.GetCommands(commands);

            commandsList.Count.Should().Equals(commands.Length);
        }

        [Theory]
        [InlineData("AWWALAARAFLA")]
        [InlineData("RAACALFLAARSAR")]
        [InlineData("rrrAACAdFLAARSAR")]
        [InlineData("")]
        public void GetCommands_IsNotNullAndValidAndInvalidCommands_ShouldReturnCommandList_CountShouldEqualWithValidCommandsLength(string commands)
        {
            var commandsList = _commandsService.GetCommands(commands);

            var countValid = commands.ToUpper().TakeWhile(o => o == 'A' ||
                                                     o == 'L' ||
                                                     o == 'R').Count();

            commandsList.Count.Should().Equals(countValid);
        }
    }
}
