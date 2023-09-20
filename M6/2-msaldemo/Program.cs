using System.Runtime.InteropServices;
using Microsoft.Identity.Client;

Console.WriteLine("Starging MSAL demo!");

var tenantId = "YOUR_TENANT_ID";
string clientId = "YOUR_CLIENT_ID";

List<string> scopes = new List<string>
{ 
    "user.read",
    "https://management.azure.com/user_impersonation"
};

var app = PublicClientApplicationBuilder
    .Create(clientId)
    .WithAuthority(AzureCloudInstance.AzurePublic, tenantId)    
    .WithRedirectUri("http://localhost")
    .Build();

var result = await app
    .AcquireTokenInteractive(scopes)
    .ExecuteAsync();

if (result == null)
{
    Console.WriteLine("Could not acquired token!");
    return;
}   

Console.WriteLine($"Username: {result.Account.Username}");
Console.WriteLine($"Token: {result.AccessToken.Substring(0, result.AccessToken.Length - 10)}");
Console.WriteLine("Done!");