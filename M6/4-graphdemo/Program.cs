using Azure.Identity;
using Microsoft.Graph;

Console.WriteLine("Starging Graph Demo");

var tenantId = "YOUR_TENANT_ID";
var clientId = "YOUR_CLIENT_ID";

var credentials = new InteractiveBrowserCredential(tenantId, clientId);

var graphClient = new GraphServiceClient(credentials);

var stream = await graphClient.Me.Photo.Content.GetAsync();

using var fileStream = File.Create("myphoto.png");

await stream.CopyToAsync(fileStream);

Console.WriteLine("Done!");