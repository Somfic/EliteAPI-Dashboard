using EliteAPI.Abstractions.Events;
using Newtonsoft.Json;

namespace EliteAPI.Server.Payloads;

public record EventPayload([JsonProperty("Event")] IEvent Event, [JsonProperty("Context")] EventContext Context);