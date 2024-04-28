using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Domain.Contracts.UnitOfWork;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Common;
using ViaEventsAssociation.Core.Application.AppEntry;
using ViaEventsAssociation.Core.Application.CommandHandler.Commands.Event;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Events;

public class ActivateEventEndpoint(ICommandDispatcher dispatcher, IUnitOfWork unitOfWork) : ApiEndpoint.WithRequest<ActivateEventRequest>.WithoutResponse
{
    [HttpPost("events/{EventId}/activate")]
    public override async Task<ActionResult> HandleAsync([FromRoute]ActivateEventRequest activateEventRequest)
    {
        var commandResult = ActivateEventCommand.Create(activateEventRequest.EventId);
        if (commandResult.IsErrorResult())
        {
            return BadRequest(commandResult.Errors);
        }

        var saveDispatcher = new UowSaveDispatcher(dispatcher, unitOfWork);
        var dispatchResult = await saveDispatcher.Dispatch(commandResult.Value!);
        return !dispatchResult.IsErrorResult() ? Ok() : BadRequest(dispatchResult.Errors);
    }
}

public record ActivateEventRequest([FromRoute] string EventId);