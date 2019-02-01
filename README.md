# Getting Started with FSMB APIs

This documentation provides information on getting started using FSMB APIs. Refer to the documentation for the API you intend to use for more specific information and for differences that may exist. 

For information and help using an API please contact FSMB using the contact information provided for the specific API you wish to use.

- [Definitions](docs/definitions.md)
- [Rest APIs](docs/rest.md)  
- [Authentication](docs/authentication.md)
  - [Authentication URLs](#authentication-urls)
- [Paging and Sorting](docs/paging.md)
- [Consuming an API](#consuming-an-api)

## Authentication URLs

The following URLs are used for authentication for FSMB APIs.

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

If successful then the bearer token is returned with the access token in it. The access token must be passed to subsequent calls to APIs to authenticate the client.

```shell
curl -X GET \
  '{url}' \
  -H 'Authorization: Bearer {access_token}' \  
  -H 'cache-control: no-cache'
```

Refer to the [Samples](samples/readme.md) for examples of how to use this in code.

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
