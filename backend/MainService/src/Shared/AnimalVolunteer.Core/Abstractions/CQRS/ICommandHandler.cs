using AnimalVolunteer.SharedKernel;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Core.Abstractions.CQRS;

public interface ICommandHandler<TResponse, in TCommand> where TCommand : ICommand
{
    Task<Result<TResponse, ErrorList>> Handle(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    Task<UnitResult<ErrorList>> Handle(TCommand command, CancellationToken cancellationToken);
}
