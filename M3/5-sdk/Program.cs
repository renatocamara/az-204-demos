using System.Text;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;

Console.WriteLine("Blob demo");

// define env. variable.
// $env:STORAGE_CONNECTION_STRING="

// Get the connection string from the environment variable
var connectionString = Environment.GetEnvironmentVariable("STORAGE_CONNECTION_STRING");

if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("Environment variable 'STORAGE_CONNECTION_STRING' is not set.");
    return;
}

var containerName = "demo";

var container = new BlobContainerClient(connectionString, containerName);

await container.CreateIfNotExistsAsync();

Console.WriteLine("Container created");

Console.WriteLine("Uploading block blob");
{
     var blobName = "blockBlob.txt";
     var content = "Hello from my code!";

     var bytes = Encoding.UTF8.GetBytes(content);
     var stream = new MemoryStream(bytes);

     await container.UploadBlobAsync(blobName, stream);

     Console.WriteLine("Block blob uploaded");
}

Console.WriteLine("Uploading page blob");
{
    var blobName = "pageBlob.txt";
    var content = "Hello from my code!";

    var pageClient = container.GetPageBlobClient(blobName);

    Console.WriteLine($"Page size: {pageClient.PageBlobPageBytes} bytes");

    var page = new byte[pageClient.PageBlobPageBytes];

    var bytes = Encoding.UTF8.GetBytes(content);

    Array.Copy(bytes, page, bytes.Length);

    await pageClient.CreateIfNotExistsAsync(pageClient.PageBlobPageBytes);

    await pageClient.UploadPagesAsync(new MemoryStream(page), 0);    

    Console.WriteLine("Page blob uploaded");
}

Console.WriteLine("Done!");