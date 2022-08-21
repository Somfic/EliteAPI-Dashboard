using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EliteAPI.Server.WebSocket;

public record WebSocketMessage
{
    public WebSocketMessage(MessageType Type, object Payload)
    {
        this.Type = Type;
        this.Payload = Payload;
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public MessageType Type { get; init; }
    public object Payload { get; init; }
    
}