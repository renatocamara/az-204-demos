using Microsoft.Azure.Cosmos;

Console.WriteLine("Cosmos DB Example");

using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;

var builder = new ConfigurationBuilder().AddUserSecrets<Program>();
var config = builder.Build();

Console.WriteLine("Cosmos DB Example");

var uri = config["COSMOS_DB_URI"];
var key = config["COSMOS_DB_KEY"];
/*
dotnet user-secrets set "COSMOS_DB_URI" "your_cosmos_db_uri"
dotnet user-secrets set "COSMOS_DB_KEY" "your_cosmos_db_key"
*/
var client = new CosmosClient(
    uri, 
    key, 
    new CosmosClientOptions() {
        SerializerOptions = new CosmosSerializationOptions() {
            PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
        }
    }
);

var database = await client.CreateDatabaseIfNotExistsAsync("hr");
var container = (await database.Database.CreateContainerIfNotExistsAsync("employees", "/id")).Container;

// Create an employee
var newEmployee = new Employee() {
    Id = Guid.NewGuid().ToString(),
    FirstName = "John",
    LastName = "Doe",
    Department = "Sales",
    Salary = 300000
};

var response = await container.CreateItemAsync(newEmployee, new PartitionKey(newEmployee.Id));

Console.WriteLine($"Status code: {response.StatusCode}");

var query = new QueryDefinition("SELECT * FROM employee e");

var iterator = container.GetItemQueryIterator<Employee>(query);

while (iterator.HasMoreResults)
{
    var results = await iterator.ReadNextAsync();
    foreach (var employee in results)
    {
        Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.Department} - {employee.Salary}");
    }
}

Console.WriteLine("Done");