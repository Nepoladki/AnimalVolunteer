﻿using AnimalVolunteer.Core.Abstractions.CQRS;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.TakeRequestForConsideration;
public record TakeRequestForConsiderationCommand(Guid RequestId, Guid AdminId) : ICommand;
