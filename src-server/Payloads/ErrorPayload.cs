using Newtonsoft.Json;
namespace EliteAPI.Server.Payloads;

public record ErrorPayload([JsonProperty("Exception")] Exception exception);