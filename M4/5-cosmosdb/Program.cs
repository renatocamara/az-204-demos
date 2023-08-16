// See https://aka.ms/new-console-template for more information
using Microsoft.Azure.Cosmos;

Console.WriteLine("Starting Cosmos DB Demo");

var uri = "YOUR_URI";
var key = "YOUR_KEY";

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

var newEmployee = new Employee() {
    Id = "1004",
    FirstName = "Grace",
    LastName = "Hopper",
    Salary = 120000
};

await container.CreateItemAsync(newEmployee, new PartitionKey(newEmployee.Id));

var query = new QueryDefinition("SELECT * FROM employee e");

var iterator = container.GetItemQueryIterator<Employee>(query);

while (iterator.HasMoreResults)
{
    var results = await iterator.ReadNextAsync();
    foreach (var employee in results)
    {
        Console.WriteLine($"{employee.FirstName} {employee.LastName}: {employee.Salary}");
    }
}

Console.WriteLine("Done");