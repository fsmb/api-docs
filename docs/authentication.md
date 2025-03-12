# Authentication

FSMB APIs require authentication before they can be used. Authentication helps identify the caller for security, auditing and data retrieval purposes. FSMB APIs use [OAuth2](https://oauth.net/2/) client credentials for authentication.

- [OAuth2](#oauth2)
  - [Client Credentials](#client-credentials)
  - [Scopes](#scopes)
- [FSMB Authentication](#fsmb-authentication)

## OAuth2

OAuth2 has become the standard for REST APIs and other peer-to-peer architectures. OAuth2 supports several different "grant types" for supporting different scenarios such as implicitly requiring login in a browser to implicit login using client credentials. Client credentials are designed for server to server communication such as for REST APIs.

## Client Credentials

Client credentials work by assigning each client an ID and secret. This is equivalent to a user's name and password but generally the values are more complex because they are designed to be stored securely somewhere and used programmatically. When a client wishes to connect to an API, they must first authenticate using the client ID and secret. The API will authenticate the credentials and return back a token response. 

The token response contains, amongst other things, the bearer access token needed by the client for subsequent calls and optionally a refresh value. The access token is used to authenticate the client on the rest of the API calls. The refresh value is used to periodically obtain a new access token. For security reasons, access tokens are only valid for a given length of time. Once the time has elapsed, the token is no longer valid. To avoid having to reauthenticate, a client may refresh the token using the refresh value. When refreshed, a new bearer token is generated and the client may use the updated access token.

One of the biggest advantages of client credentials is that the client does not need to make continual authentication requests and the server does not have to continually validate the access token. Combined with HTTPS, this proves to be a relatively secure approach to authentication.

## Scopes

Authentication is different than authorization. Authentication is the process of validating a client is who they say they are. Authorization is responsible for determining what an authenticated user can do. In OAuth2, authorization is handled by scopes. A scope is basically a permission. For example, an API may have `read`, `edit` and `delete` scopes. 

When a client authenticates, they must request the scope(s) they want in addition to the client ID and secret. This follows the [Principle of Least Privilege](https://en.wikipedia.org/wiki/Principle_of_least_privilege) in which a client only requests, and is granted, the rights it needs at this time. The authentication server will validate the client has access to all the scopes they request. If they do, then the access token is returned with only those rights. The client cannot use any rights they did not ask for even if they would have been granted it if requested.

You can learn more about OAuth2 [here](https://oauth.net/2/).

## FSMB Authentication

Before getting started, be sure to contact FSMB to get your client account set up. Refer to each API for specific contact information.

To authenticate with FSMB before making API calls, do the following.

1. Create the authentication URL based upon the information provided by each API. This is generally the URL to the API itself.
3. Create a request body (in x-www-form-urlencoded format) containing the following values.
   1. `client_id` set to the client ID that was assigned to you.
   1. `client_secret` set to the client secret that was assigned to you.
   1. `scope` set to the scope(s) that are being requested.
   1. `grant_type` set to `client_credentials`.
4. Set the `Content-Type` to `application/x-www-form-urlencoded`.
5. POST the request to the authentication endpoint provided by the API. This is generally `/connect/token`. For example UA API uses `https://services-ua-demo.fsmb.org/connect/token`.
6. If the call is successful, a JSON token response is returned. Extract the `access_token` from the body to use to authenticate subsequent API calls.

```shell
curl -X POST \
  {url}/connect/token \
  -H 'Content-Type: application/x-www-form-urlencoded' \
  -H 'cache-control: no-cache' \
  -d 'client_id={id}&client_secret={secret}&scope={scope}&grant_type=client_credentials'
```

For each call to an FSMB API, ensure the `Authorization` header is set. The value will be of the form `Bearer {access_token}` where `{access_token}` is the token obtained earlier. Do not include the brackets in the value.

```shell
curl -X GET \
  '{url}' \
  -H 'Authorization: Bearer {access_token}' \  
  -H 'cache-control: no-cache'
```

*Note: The access token is valid only for a fixed time. If API calls start returning 401, then you will need to authenticate again.*
