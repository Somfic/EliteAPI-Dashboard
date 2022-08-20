const port = 51555;
let socket: WebSocket | null = null;
let isConnected = false;

export function connect() {
  if (isConnected) return;

  socket = new WebSocket(`ws://localhost:${port}`);
  socket.onopen = () => {
    console.log("WebSocket connected");
    isConnected = true;
  };

  socket.onmessage = (message) => {
    console.log("Incoming WebSocket message", message);
  };

  socket.onerror = (error) => {
    console.log("WebSocket error", error);
  };

  socket.onclose = () => {
    console.log("WebSocket closed");
    isConnected = false;

    setTimeout(function () {
      connect();
    }, 1000);
  };
}
