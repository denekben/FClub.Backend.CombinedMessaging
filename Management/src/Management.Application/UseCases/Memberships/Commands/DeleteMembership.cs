﻿using MediatR;

namespace Management.Application.UseCases.Memberships.Commands
{
    public sealed record DeleteMembership(
        Guid membershipId
    ) : IRequest;
}
