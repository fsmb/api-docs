# Authentication

APIs typically require authentication before they can be used. Authentication helps identify the caller for security, auditing and data retrieval purposes. There are different ways to authenticat a client ranging from basic user name and password combinations to client certificates.

- [OAuth2](#oauth2)
  - [Client Credentials](#client-credentials)
  - [Scopes](#scopes)

## OAuth2

OAuth2 has become the standard for REST APIs and other peer-to-peer architectures. OAuth2 supports several different "grant types" for supporting different scenarios such as implicitly requiring login in a browser to implicit login using client credentials. Client credentials are designed for server to server communication such as for REST APIs.

## Client Credentials

Client credentials work by assigning each client and ID and secret. This is equivalent to a user's name and password but generally the values are more complex because they are designed to be stored securely somewhere and used programmatically. When a client wishes to connect to an API they must first authenticate using the client ID and secret. The API will authenticate the credentials and return back a bearer token. 

The bearer token contains, amongst other things, the access token needed by the client for subsequent calls and optionally a refresh value. The access token is used to authenticate the client on the rest of the API calls. The refresh value is used to periodically update the bearer token. For security reasons bearer tokens are only valid for a given length of time. Once the time has elapsed the token is no longer valid. To avoid having to issue a new token a client may refresh the token using the refresh value. When refreshed a new bearer token is generated and the client may use the updated access token.

One of the biggest advantages of client credentials is that the client does not need to make continual authentication requests and the server does not have to continually validate the access token. Combined with HTTPS this proves to be a relatively secure approach to authentication.

## Scopes

Authentication is different than authorization. Authentication is the process of validating a client is who they say they are. Authorization is responsible for determining what an authenticated user can do. In OAuth2 authorization is handled by scopes. A scope is basically a permission. For example an API may have `read`, `edit` and `delete` scopes. 

When a client authenticates they request the scope(s) they want in addition to the client ID and secret. This follows the [Principle of Least Privilege](https://en.wikipedia.org/wiki/Principle_of_least_privilege) in which a client only requests, and is granted, the rights it needs at this time. The authentication server will validate the client has access to all the scopes they request. If they do then the access token is returned with only those rights. The client cannot use any rights they did not ask for even if they would have been granted it if requested.

You can learn more about OAuth2 [here](https://oauth.net/2/).
