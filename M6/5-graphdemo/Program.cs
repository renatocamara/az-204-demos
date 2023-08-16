// See https://aka.ms/new-console-template for more information
using Azure.Identity;
using Microsoft.Graph;

Console.WriteLine("Starging Graph Demo");

var tenantId = "YOUR_TENANT_ID";
var clientId = "YOUR_CLIENT_ID";

var scopes = new string[] { 
    "user.read",
};

// var options = new TokenCredentialOptions
// {
//     AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
// };

var options = new DeviceCodeCredentialOptions {
    ClientId = clientId,
    TenantId = tenantId,
    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
    DeviceCodeCallback = async (deviceCodeResult, cancellationToken) =>
    {
        Console.WriteLine($"Message: {deviceCodeResult.Message}");
        Console.WriteLine($"User Code: {deviceCodeResult.UserCode}");
        Console.WriteLine($"Device Code: {deviceCodeResult.DeviceCode}");
        Console.WriteLine($"Verification Uri: {deviceCodeResult.VerificationUri}");
        await Task.FromResult(0);
    }
};

var credential = new DeviceCodeCredential(options);

var graphClient = new GraphServiceClient(credential, scopes);

var resultStream = await graphClient.Me.Photo.Content.GetAsync();

if (resultStream != null) {
    var fileStream = File.Create("profile.jpg");

    await resultStream.CopyToAsync(fileStream);
} else {
    Console.WriteLine("No photo found");
}

Console.WriteLine("Done!");
