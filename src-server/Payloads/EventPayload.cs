using EliteAPI.Abstractions.Events;

namespace EliteAPI.Server.Payloads;

public record EventPayload(IEvent Event, EventContext Context);