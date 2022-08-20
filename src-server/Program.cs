using EliteAPI.Abstractions;
using EliteAPI.Server;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
await api.StartAsync();

await Task.Delay(-1);