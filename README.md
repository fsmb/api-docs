# Getting Started with FSMB APIs

This documentation provides information on getting started using FSMB APIs. Refer to the specific API you intend to use for more specific information and for differences that may exist. For information and help using an API please contact FSMB using the contact information provided for the specific API you wish to use.

- [Rest APIs](#rest-apis)
  - [Versioning](#versioning)
  - [Supported Data Formats](#supported-data-formats)  
- [Authentication](#authentication)
- [Paging and Sorting](#paging-and-sorting)
- [Consuming an API](#consuming-an-api)

## REST APIs

APIs provided by FSMB are REST-based. REST provides a consistent, simple format that clients can use to programmatically call FSMB. 

REST is based upon the HTTP standard and is supported in any programming language that supports HTTP calls such as C#, Java, Ruby and Python without the need for complex types. Many languages have implicit support for REST APIs to make it even easier to use them. Refer to your specific language's documentation for more information on consuming REST APIs.

You can learn more about REST APIs [here](docs/rest.md).

### Versioning

FSMB currently versions APIs at the API or resource level in the URL (e.g. `https://tempuri.org/v1/resources`). FSMB is committed to not making breaking changes in existing APIs. If a breaking change is needed then a new version will be introduced. In some cases a "preview" version may be available for clients to try out an API. 

**Note: Preview versions should not be used in production as they can be deprecated, changed or removed without warning.**

You can learn more about versioning [here](docs/rest.md#versioning).

### Supported Data Formats

At this time FSMB supports the following formats for requests and responses.

- [JSON](https://www.json.org/)

## Authentication

Authentication URLs:
 - Demo: https://demo-services.fsmb.org/authorization/v1
 - Production: https://services.fsmb.org/authorization/v1

To help protect physician information and to ensure that requests are properly audited, all FSMB APIs require authentication to be used.  FSMB uses OAuth2 with client credentials for authentication. Clients must contact FSMB to be given a client ID and secret to authenticate. Clients will be granted access to scopes based upon their needs. Refer to the documentation for each API for more information on the available scopes.

To authenticate, POST to the authentication URL. Pass the client ID, secret and desired scopes in the request body. 

```shell
curl -X POST \
  {url}/connect/token \
  -H 'Content-Type: application/x-www-form-urlencoded' \
  -H 'cache-control: no-cache' \
  -d 'client_id={id}&client_secret={secret}&scope={scope}&grant_type=client_credentials'
```
```csharp
var client = new HttpClient() { BaseAddress = new Uri(url) };
client.DefaultRequestHeaders.Add("Accept", "application/json");

var id = "";
var secret = "";
var scope = "";

var request = new List<KeyValuePair<string, string>>() {
    new KeyValuePair<string, string>("client_id", id),
    new KeyValuePair<string, string>("client_secret", secret),                
    new KeyValuePair<string, string>("scope", scope),
    new KeyValuePair<string, string>("grant_type", "client_credentials")
};
var content = new FormUrlEncodedContent(request);

using (var response = client.PostAsync("connect/token", content).Result)
{
    response.EnsureSuccessStatusCode();
                
    var body = response.Content.ReadAsStringAsync().Result;

    var token = JsonConvert.DeserializeObject<BearerToken>(body);
};
```

If successful then the bearer token is returned with the access token in it. The access token must be passed to subsequent calls to APIs to authenticate the client.

```shell
curl -X GET \
  '{url}' \
  -H 'Authorization: Bearer {access_token}' \  
  -H 'cache-control: no-cache'
```
```csharp
var client = new HttpClient() { BaseAddress = new Uri(url) };
client.DefaultRequestHeaders.Add("Accept", "application/json");
client.DefaultRequestHeaders.Add("Authorization", "Bearer " + access_token);
```

Refer to the [Samples](samples) for examples of how to use this in code.

You can learn more about authentication [here](docs/authentication.md).

## Paging/Sorting

Resources that return lists of items support paging and sorting. FSMB APIs use the Page-Offset model for paging in most cases. Refer to the specific API documentation to verify. The following parameters control paging options and are specified in the query string.

| Parameter | Value | Description |
| - | - | - |
| $pageSize | Number | The number of items to return in a page. |
| $page | Number | The page to retrieve. Page numbers start at 1. |

```shell
curl -X GET \
   '{url}?$page=2&$pageSize=20`
```

Sorting is implemented using the `$orderBy` parameter in the query string. When supported, multiple fields can be sorted by separating them with commas. Each field can be sorted ascending or descending by adding the `asc` or `desc` suffix to the field.

```shell
curl -X GET \
    '{url}?$orderBy=field1,field2 desc`
```

You can learn more about paging [here](docs/paging.md). You can learn more about sorting [here](docs/sorting.md).

## Error Reporting

If a call to an API fails then the server will return back the appropriate HTTP status code. For bad requests or 5xx errors the server will generally return an error detail in the body. The error detail provides additional information about what went wrong. Clients can use this information to diagnose the cause of the failure. The following information is returned.

```json
{
   "code": "Error code",
   "message": "Descriptive message",
   "target": "Optional target of the error",
   "logId": "The ID of the log entry associated with the error, if any",
   "innerError": { },
   "data": { }
}
```

If the error was caused by a lower level error then `innerError` contains the error detail of the child error. Some errors may return additional data in the `data` property.

You can learn more about error handling [here](docs/errors.md).

## Consuming an API

FSMB APIs can be consumed in a variety of ways depending upon need.

[Postman](https://www.getpostman.com/) is a useful tool for testing REST APIs. Postman can be used to make just about any call to an API. To simplify using Postman with FSMB APIs, each API provides a link to the Postman collection (a set of API requests) demonstrating the API. Refer to each API for a link to the Postman collection.

[OpenAPI](https://github.com/OAI/OpenAPI-Specification) (formerly known as Swagger) is a specification that APIs can provide that programmatically descripe the API and its data. OpenAPI allows the specification to be imported into a supported tools and auto-generate a client that can call the API. OpenAPI is supported in many common REST API tools and many tools can generate code for different languages. Before manually writing code check if your language can import the OpenAPI specification. All FSMB APIs provide a link to their OpenAPI specification in the documentation.

Each API provides sample code, in different languages when possible, that can be used as a starting point for calling FSMB APIs. Refer to each API's documentation for the samples.
