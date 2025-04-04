﻿using AnimalVolunteer.Core.Abstractions;
using System.Threading.Channels;

namespace AnimalVolunteer.Core.MessageQueues;

public class InMemoryMessageQueue<TMessage> : IMessageQueue<TMessage>
{
    private readonly Channel<TMessage> _channel = Channel.CreateUnbounded<TMessage>();

    public async Task WriteAsync(TMessage paths, CancellationToken cancellationToken = default)
    {
        await _channel.Writer.WriteAsync(paths, cancellationToken);
    }

    public async Task<TMessage> ReadAsync(CancellationToken cancellationToken = default)
    {
        return await _channel.Reader.ReadAsync(cancellationToken);
    }
}
