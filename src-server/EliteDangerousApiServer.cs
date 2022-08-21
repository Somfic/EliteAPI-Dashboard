using System.Net;
using System.Net.Sockets;
using EliteAPI.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using EliteAPI.Abstractions.Events;
using EliteAPI.Server.Payloads;
using EliteAPI.Server.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace EliteAPI.Server;

public class EliteDangerousApiServer
{
    private readonly IServiceProvider _services;
    private readonly ILogger<EliteDangerousApiServer>? _log;
    private readonly IEliteDangerousApi _api;
    private readonly IConfiguration _config;
    public bool IsRunning { get; private set; }

    public EliteDangerousApiServer(IServiceProvider services)
    {
        _services = services;
        _log = services.GetService<ILogger<EliteDangerousApiServer>>();
        _api = services.GetRequiredService<IEliteDangerousApi>();
        _config = services.GetRequiredService<IConfiguration>();

        _api.Events.OnAny(OnAny);
    }

    private readonly List<Client> _clients = new();
    private readonly List<Task> _clientTasks = new();
    private Task _mainTask;

    public Task StartAsync(int port)
    {
        var host = _config.GetValue("EliteAPI:Host", "127.0.0.1");
        if (!IPAddress.TryParse(host, out var hostIp))
        {
            _log?.LogWarning("Invalid host address: {Host}", host);
            hostIp = IPAddress.Parse("127.0.0.1");
            _log?.LogDebug("Using default host address: {Host}", hostIp);
        }

        _log?.LogInformation("Starting EliteAPI Server v{Version}",
            typeof(EliteDangerousApiServer).Assembly.GetName().Version);

        var listener = new TcpListener(hostIp, port);
        listener.Start();

        var ip = listener.LocalEndpoint as IPEndPoint;

        _log?.LogInformation("EliteAPI Server listening on https://{Ip}:{Port}", ip!.Address, ip.Port);

        _mainTask = Task.Run(async () =>
        {
            IsRunning = true;

            while (IsRunning)
            {
                var tcp = await listener.AcceptTcpClientAsync();
                var client = ActivatorUtilities.CreateInstance<Client>(_services, tcp);
                _clients.Add(client);
                _clientTasks.Add(Task.Run(async () => await client.Handle()));

                await Task.Delay(500);
            }
        });

        return Task.CompletedTask;
    }

    public async Task StopAsync()
    {
        _log?.LogDebug("Stopping EliteAPI Server");

        IsRunning = false;

        foreach (var client in _clients.Where(x => x.IsOpen))
        {
            await client.CloseAsync();
        }

        await _mainTask;

        _log?.LogInformation("Stopped EliteAPI Server");
    }

    private async Task OnAny(IEvent e, EventContext context)
    {
        await WriteAsync(MessageType.Event, new EventPayload(e, context));
        await WriteAsync(MessageType.Paths, new PathsPayload(_api.Parser.ToPaths(e).ToList(), context));
    }
    
    private readonly IList<(MessageType type, object payload)> backlog = new List<(MessageType type, object payload)>();
    
    private async Task WriteAsync(MessageType type, object payload)
    {
        backlog.Add((type, payload));
        
        var message = new WebSocketMessage(type, payload);
        foreach (var client in _clients.Where(x => x.IsOpen && x.IsAccepted && x.IsAvailable))
        {
            await client.WriteAsync(message);
        }
    }
}