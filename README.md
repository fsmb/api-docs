# Getting Started with FSMB APIs

This documentation provides information on getting started using FSMB APIs. Refer to the documentation for the API you intend to use for more specific information and for differences that may exist. 

For information and help using an API please contact FSMB using the contact information provided for the specific API you wish to use.

- [Definitions](docs/definitions.md)
- [Rest APIs](docs/rest.md)  
- [Authentication](docs/authentication.md)
  - [Authentication URLs](#authentication-urls)
- [Paging and Sorting](docs/paging-sorting.md)
- [Errors](docs/errors.md)
- [Consuming an API](docs/consuming.md)

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
