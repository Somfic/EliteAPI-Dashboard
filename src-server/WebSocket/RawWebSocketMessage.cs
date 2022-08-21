namespace EliteAPI.Server.WebSocket;

public record RawWebSocketMessage(Opcode Type, string? Payload = null);