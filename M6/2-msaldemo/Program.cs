using System.Runtime.InteropServices;
using Microsoft.Identity.Client;

Console.WriteLine("Starging MSAL demo!");

var tenantId = "c00a40e2-f70e-4610-bc25-de4da1104546";
string clientId = "a50e17a4-001d-422c-80d2-db068625aa6d";

List<string> scopes = new List<string>
{ 
    "user.read",
    "https://management.azure.com/user_impersonation"
};

var app = PublicClientApplicationBuilder
    .Create(clientId)
    .WithAuthority(AzureCloudInstance.AzurePublic, tenantId)    
    .WithDefaultRedirectUri()
    .Build();

var result = await app
    .AcquireTokenInteractive(scopes)
    .ExecuteAsync();

if (result == null)
{
    Console.WriteLine("Could not acquired token!");
    return;
}   

var token = result.AccessToken.Substring(0, result.AccessToken.Length - 10);

Console.WriteLine($"Username: {result.Account.Username}");

Console.WriteLine($"Token: {token}");

Console.WriteLine("Done!");