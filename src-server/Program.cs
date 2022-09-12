using EliteAPI.Abstractions;
using EliteAPI.Server;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EliteAPI.Server.Payloads;
using EliteAPI.Server.WebSocket;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services => services
        .AddEliteApi()
        .AddEliteApiServer())
    .Build();

var server = host.Services.GetRequiredService<EliteDangerousApiServer>();
var api = host.Services.GetRequiredService<IEliteDangerousApi>();

if (args.Length == 0 || !int.TryParse(args[0], out var port))
    port = 51555;

await server.StartAsync(port);
await api.InitialiseAsync();

api.OnError += async (_, ex) => {
    await server.WriteAsync(MessageType.Error, new ErrorPayload(ex));
};

await api.StartAsync();

await Task.Delay(-1);