using Moq;
using ViaEventAssociation.Core.Domain.Contracts.Repositories;
using ViaEventAssociation.Core.Domain.CreatorAgg;
using ViaEventAssociation.Core.Domain.LocationAgg;
using ViaEventsAssociation.Core.Application.CommandHandler.Commands.Location;
using ViaEventsAssociation.Core.Application.CommandHandler.Common.Base;
using ViaEventsAssociation.Core.Application.CommandHandler.Handlers.LocationHandlers;
using VIAEventsAssociation.Core.Tools.OperationResult.Error;
using VIAEventsAssociation.Core.Tools.OperationResult.Helpers;
using VIAEventsAssociation.Core.Tools.OperationResult.OperationResult;

namespace UnitTests.Features.LocationFeatures.SetLocationMaxGuestsTests;

public class SetLocationMaxGuestsHandlerTest
{
    private readonly Mock<ILocationRepository> _locationRepositoryMock;
    private readonly ICommandHandler<SetLocationMaxGuestsCommand> _handler;
    private readonly SetLocationMaxGuestsCommand _validCommand;
    private static readonly Location TestLocation = Location.Create(LocationName.Create("Valid Location Name").Value!, new CreatorId()).Value!;

    public SetLocationMaxGuestsHandlerTest()
    {
        _locationRepositoryMock = new Mock<ILocationRepository>();
        _handler = new SetLocationMaxGuestsHandler(_locationRepositoryMock.Object);
        _validCommand = SetLocationMaxGuestsCommand.Create(6, Guid.NewGuid().ToString()).Value!;
    }

    [Fact]
    public async Task SetLocationMaxGuests_AllValuesValid_Success()
    {
        // Arrange
        _locationRepositoryMock
            .Setup(repo => repo.FindAsync(It.IsAny<LocationId>()))
            .ReturnsAsync(new Result<Location>(TestLocation));

        _locationRepositoryMock
            .Setup(repo => repo.UpdateAsync(It.IsAny<Location>()))
            .ReturnsAsync(new Result<Location>(null));

        // Act
        var result = await _handler.HandleAsync(_validCommand);

        // Assert
        Assert.False(result.IsErrorResult());
        _locationRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Location>()), Times.Once);
    }

    [Fact]
    public async Task SetLocationMaxGuests_RepoError_Failure()
    {
        // Arrange
        var repoResult = new Result<Location>(null);
        repoResult.CollectError(ErrorHelper.CreateVeaError("Repository Error", ErrorType.Unknown));
        _locationRepositoryMock
            .Setup(repo => repo.FindAsync(It.IsAny<LocationId>()))
            .ReturnsAsync(repoResult);

        // Act
        var result = await _handler.HandleAsync(_validCommand);

        // Assert
        Assert.Null(repoResult.Value);
        Assert.True(result.IsErrorResult());
        Assert.Contains(result.Errors, e => e.Message.Message.Contains("Repository Error"));
        _locationRepositoryMock.Verify(repo => repo.FindAsync(It.IsAny<LocationId>()), Times.Once);
    }
}