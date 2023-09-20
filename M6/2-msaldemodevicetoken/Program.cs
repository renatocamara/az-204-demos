﻿using Microsoft.Identity.Client;

Console.WriteLine("MSAL Demo");

var clientId = "41725e58-9f23-4dbf-b4e0-6768accb7251";
var tenantId = "14f08b43-3b8c-4f1c-87c5-71e2bb2177f3";

var app = PublicClientApplicationBuilder
    .Create(clientId)
    .WithTenantId(tenantId)
    .WithDefaultRedirectUri()
    .Build();

var scopes = new[] {
    "user.read" // Get user profile
};

var result = await app.AcquireTokenWithDeviceCode(
    scopes,
    deviceCodeResult => {
    Console.WriteLine("----------------------");
    Console.WriteLine($"URL: {deviceCodeResult.VerificationUrl}, Code: {deviceCodeResult.UserCode}");
    Console.WriteLine($"Message: {deviceCodeResult.Message}");
    Console.WriteLine("----------------------");
    return Task.FromResult(0);
}).ExecuteAsync();

var account = result.Account;

Console.WriteLine($"Username: {account.Username}");
Console.WriteLine($"Token: {result.AccessToken.Substring(0, result.AccessToken.Length - 10)}");

Console.WriteLine("Done!");