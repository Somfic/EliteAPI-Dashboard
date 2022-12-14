using EliteAPI.Abstractions.Events;
using Newtonsoft.Json;

namespace EliteAPI.Server.Payloads;

public record PathsPayload([JsonProperty("Paths")] IReadOnlyCollection<EventPath> paths, [JsonProperty("Context")] EventContext context);