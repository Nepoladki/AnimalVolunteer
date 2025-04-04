﻿namespace AnimalVolunteer.Core.Abstractions;

public interface IMessageQueue<TMessage>
{
    Task WriteAsync(TMessage paths, CancellationToken cancellationToken);
    Task<TMessage> ReadAsync(CancellationToken cancellationToken);
}
