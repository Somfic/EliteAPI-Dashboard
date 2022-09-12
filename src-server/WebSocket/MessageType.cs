namespace EliteAPI.Server.WebSocket;

public enum MessageType {
    Authentication,
    Handshake,
    Paths,
    Event,
    Error,
    Backlog
}