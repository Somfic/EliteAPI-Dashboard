using EliteAPI.Abstractions.Events;

namespace EliteAPI.Server.Payloads;

public record PathsPayload(IReadOnlyCollection<EventPath> paths, EventContext context);