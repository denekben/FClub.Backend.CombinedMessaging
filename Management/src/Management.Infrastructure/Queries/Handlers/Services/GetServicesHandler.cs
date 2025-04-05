using Management.Application.UseCases.Services.Queries;
using MediatR;

namespace Management.Infrastructure.Queries.Handlers.Services
{
    public sealed class GetServicesHandler : IRequestHandler<GetServices,>
    {
    }
}
