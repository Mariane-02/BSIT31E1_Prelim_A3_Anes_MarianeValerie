using System.Text;

using var client = new HttpClient();
HttpResponseMessage? response = null;

string baseUrl = "https://jsonplaceholder.typicode.com/posts";
string specificUrl = $"{baseUrl}/1";
string targetUrl = "";
string verb = "";

var requestBody = new StringContent(
    """
    {
        "title": "API Assignment",
        "body": "Demonstrating HTTP Verbs",
        "userId": 1
    }
    """,
    Encoding.UTF8,
    "application/json"
);

Console.Write("Enter choice (A-GET, B-POST, C-PUT, D-DELETE): ");
string choice = Console.ReadLine()?.Trim().ToUpper() ?? "";

if (choice == "A")
{
    verb = "GET";
    targetUrl = specificUrl;
    // GET - Retrieves existing data from the API.
    response = await client.GetAsync(targetUrl);
}
else if (choice == "B")
{
    verb = "POST";
    targetUrl = baseUrl;
    // POST - Creates a new resource in the API.
    response = await client.PostAsync(targetUrl, requestBody);
}
else if (choice == "C")
{
    verb = "PUT";
    targetUrl = specificUrl;
    // PUT - Updates an existing resource in the API.
    response = await client.PutAsync(targetUrl, requestBody);
}
else if (choice == "D")
{
    verb = "DELETE";
    targetUrl = specificUrl;
    // DELETE - Removes an existing resource from the API.
    response = await client.DeleteAsync(targetUrl);
}

if (response != null)
{
    Console.WriteLine($"\nVerb: {verb}");
    Console.WriteLine($"URL: {targetUrl}");
    Console.WriteLine($"Status: {(int)response.StatusCode} {response.StatusCode}");

    if (response.IsSuccessStatusCode)
    {
        if (verb == "POST" || verb == "PUT")
        {
            string body = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Body:\n{body}");
        }
        else if (verb == "GET")
        {
            string body = await response.Content.ReadAsStringAsync();

            Console.WriteLine("\nResponse Body:");
            Console.WriteLine(body);
        }
    }
    else
    {
        Console.WriteLine("Request failed.");
    }
}
else
{
    Console.WriteLine("Invalid selection.");
}