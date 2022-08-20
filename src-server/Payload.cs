using EliteAPI.Abstractions.Events;

namespace EliteAPI.Server;

public readonly struct Payload {
    public Payload(EventPaths paths, EventContext context) {
        Paths = paths;
        Context = context;
    }

    public EventPaths Paths { get; }
    public EventContext Context { get; }
}