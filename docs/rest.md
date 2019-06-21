# Rest

[REST](https://en.wikipedia.org/wiki/Representational_state_transfer) (Representational State Transfer) is a commonly used architecture for communicating between clients (consumers) and servers. Most APIs today are REST-based.

- [Calling a REST API](#calling-a-rest-api)
- [HTTP Verbs](#http-request-header)
- [HTTP Status Codes](#http-status-code)
- [Data Formats](#data-formats)
- [Versioning](#versioning)  
  
## Calling a REST API

A REST API consists of a request/response pair. The request is sent by the client and contains the request being made. The response comes from the server and indicates the success or failure of a request and any relevant data.

A request consists of the following components.
1. Request URI.
1. HTTP request header.
1. Optional request body.

A response consists of the following components.
1. HTTP status code.
1. Optional response body.

### Request URI

The request URI identifies the resource being accessed and the version being used. The URI is generally of the form:

```shell
VERB https://{url}/{version}/{resource}
```

The `url` is the URL to the API. The `version` is the version of the API being used (e.g. `v1`). Some APIs version at the API level while others version at the resource level. Versions may be a single digit (e.g. `v1`) or a major.minor number (e.g. `v1.1`). The `resource` is the item being accessed or modified. After the `resource` is any additional path and query string information needed by the resource to process the request.

The `resource`, in REST, is an object that a client may be interested in such as a user, document or blog post. Each resource has a unique URL. A typical REST URL would look something like `/users` or `/documents`. The base URL generally retrieves the set of resources of that type. Additional URLs are provided under the resource URL to access additional information such as a specific user (e.g. `users/123`) or the comments associated with a blog post (e.g. `/posts/456/comments`).

Resources may have child resources (subresources) as in the example of the comments of a post. Each API, and each resource, determines what will and will not be exposed based upon the usage of the API.

### HTTP Request Header

The HTTP request header contains metadata that may be need by the server to process the request. At a minimum the [HTTP method](https://developer.mozilla.org/en-US/docs/Web/HTTP/Methods) must be specified. The HTTP method identifies what type of request is being made. Some common methods include:

| Verb | Description |
| - | - |
| GET | Read |
| POST | Create or Update |
| PUT | Update or Replace |
| PATCH | Update |
| DELETE | Delete |

### Request Body

Some requests require additional data such as a user's ID or the publish date of a post. In many cases the URL is built in such a way that the data is passed as part of the URL. This is especially true for GET requests since the results can be cached by servers. 

In some cases, such as updates, there is too much data to be placed in the URL so the request body must be used instead. Each resource will define the structure of the data it is returning. In some cases the data may be a subset of the full data.

When sending data to the server the client must specify the format of the data using the `Content-Type` header.

*Note: GET requests cannot have a request body. This is defined by the HTTP Specification but some clients may or may not enforce it. Data for GET requests must always be specified in the URL.*

### HTTP Status Code

HTTP defines specific [status codes](https://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html) for indicating success and failure of a request. Status codes are used to indicate whether a request has been successful or not and is the first thing that clients should use when looking at a response. 

HTTP separates status codes into some common ranges.

| Range | Description |
| - | - |
| 2xx | These status codes indicate success. |
| 3xx | These status codes indicate redirection to another URL. |
| 4xx | These status codes indicate a client request error such as bad data. Clients should resolve the error before trying again. |
| 5xx | These status codes indicate server errors. Trying again later may be successful. |

These are some of the common codes that APIs may return.

| HTTP Status Code | Description | Notes
| - | - | - |
| 200 | OK | The request was successful. |
| 204 | No Content | The request was successful but no data is being returned. |
| 400 | Bad Request | The client request is bad. |
| 401 | Unauthorized | The client has not been authenticated or the authentication has expired. |
| 403 | Forbidden | The client does not have access to the requested resource. |
| 404 | Not Found | The resource cannot be found. Either the client does not have access or the URL is incorrect. |
| 500 | Server Error | An error occurred processing the request. |

### Response Body

Like the request, a response can have a body. Most API calls will return a body with the data relevant for the request (e.g. user information, comments in a post, etc). Like the request, the client and server must agree on the format to be used for the response. 

The response body may not always be the expected data. In the case of an error, in addition to the status code, most servers will also return an error object indicating what failed. The [RFC7807](https://tools.ietf.org/html/rfc7807) proposal is currently being developed to provide consistency for error reporting in REST APIs but has not yet been approved. Some APIs already support this proposal but others have not. Refer to the documentation for a specific API to learn how it reports errors. 

## Data Formats

Clients and servers use [content negotiation](https://developer.mozilla.org/en-US/docs/Web/HTTP/Content_negotiation) to agree on the format that request and response bodies are sent in. Clients specify the acceptable list of formats as part of the HTTP header. The server will return the response in one of the formats. If the server does not support any of the acceptable formats then an error will occur (status code 406).

FSMB APIs support the following formats.

- [x-www-form-urlencoded](https://developer.mozilla.org/en-US/docs/Web/HTTP/Methods/POST) *Note: Used for authentication only*
- [JSON](https://www.json.org/)

In some cases other formats may be supported. For example an API that returns a file will use the binary format.

## Versioning

APIs form a contract between the client and server. A client and server must agree on what data will be sent and received for each request. Changes to the contract may break older clients so each change has to be evaluated for impact on existing clients. 

Some examples of breaking changes include the following:

- Adding a required input to a request.
- Removing or renaming fields in a request or response.
- Changing or removing the URL of a resource.
- Changing the mean or purpose of a resource.
- Changing the format of the request or response.

FSMB currently uses versioning through the URL at either the API (e.g. `/v1/users`) or resource (e.g. `/users/v1`) level. URL versioning is the easiest approach to write and consume while having few limitations beyond semantics. 

FSMB is committed to not breaking clients when new versions are released. For purposes of discussion `current` represents the latest version (e.g. `v2`), `previous` represents the immediately previous version (e.g. `v1.1`) and `older` represents any versions prior to that (e.g. `v1`).

FSMB adheres to the following policies related to versioning.

- Any breaking change will result in a new `current` version.
- The `previous` version will be deprecated. Only security fixes will be applied to deprecated versions.
- `older` versions will be removed at a future date but not sooner than one year from time of deprecation.

A preview version (e.g. `v2-preview`) may be made available to allow for reviewing upcoming changes to an API. Preview versions can be used to evaluate potential changes in the API. Preview versions may change without notice, do not follow the versioning policies given earlier and are removed when the next version is released.

**Note: Preview versions should not be used in production as they can be deprecated, changed or removed without warning.**
