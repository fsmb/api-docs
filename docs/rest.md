# Rest

REST (Representational State Transfer) is a commonly used architecture for communicating between clients (consumers) and servers. Most APIs today are REST-based.

- [How It Works](#how-it-works)
- [Requests](#requests)
  - [Resources](#resources)
  - [HTTP Verbs](#http-verbs)
  - [Request Data](#request-data)
- [Responses](#responses)
  - [HTTP Status Codes](#http-status-codes)
  - [Response Body](#response-body)
- [Versioning](#versioning)
  
You may learn more about REST [here](https://en.wikipedia.org/wiki/Representational_state_transfer).

## How It Works

REST uses the HTTP protocol to communicate between the client and server. This allows REST to be used on any network that supports web browsing without the need for additional security changes. Any HTTP client can be used to talk to a REST API. Because of this REST is supported in any language that allows web browsing such as C#, Java and Python. Since REST uses HTTP it does not require any extra code to configure and call a REST endpoint making it easier to learn, implement, test and maintain.

The HTTP protocol separates communication between the client (local) and server (remote) into a request and response. The request consists of the endpoint (the URL and verb) and the optional request body. The server provides a response which includes the status code and optional response body. 

Typically a REST API requires HTTPS. This helps to secure the communication between the client and server. Using SSL eliminates the need for more complex encryption approaches such as client certificates which helps make it easier to use in different environments.

## Requests

### Resources

A resource, in REST, is an object that a client may be interested in such as a user, document or blog post. Each resource has a unique URL. A typical REST URL would look something like `https://tempuri.org/users` or `https://tempuri.org/documents`. The base URL generally retrieves the set of resources of that type. Additional URLs are provided under the resource URL to access additional information such as a specific user (e.g. `https://tempuri.org/users/123`) or the comments associated with a blog post (e.g. `https://tempuri.org/posts/456/comments`).

Resources may have child resources (subresources) as in the example of the comments of a post. Each API, and each resource, determines what will and will not be exposed based upon the usage of the API.

### HTTP Verbs

HTTP verbs play a critical role in REST. In a typical web browsing experience the user requests a web page. This is a GET request in HTTP. The verb and the URL combine to identify where to go and what to do when you get there.  The following are the common HTTP verbs used in REST.

| Verb | Description |
| - | - |
| GET | Read |
| POST | Create or Update |
| PUT | Update or Replace |
| PATCH | Update |
| DELETE | Delete |

A single URL can have different behavior depending upon the verb being used. For example GET to a specific `user` resource may retrieve the user's information. A POST to that same URL would update the existing user. By using verbs in addition to the URL it reduces the number of URLs that need to be used and helps keep them from being cluttered with meaning words (e.g. `UpdateUser`, `DeletePost`).

### Request Data

Some requests require additional data such as a user's ID or the publish date of a post. In many cases the URL is built in such a way that the data is passed as part of the URL. This is especially true for GET requests since the results can be cached by servers. 

In some cases, such as updates, there is too much data to be placed in the URL so the request body must be used instead. Each API can define its own format for the request body but typically a REST API supports JSON. [JSON](https://www.json.org/) is a simple text-based format that allows arbitrarily complex data to be passed back and forth. Most languages support JSON either directly or through libraries making it easy to build requests and consume responses.

*Note: GET requests cannot have a request body. This is defined by the HTTP Specification but some clients may or may not enforce it. Data for GET requests must always be specified in the URL.*

## Responses

### HTTP Status Codes

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

Like the request, a response can have a body. Most API calls will return a body with the data relevant for the request (e.g. user information, comments in a post, etc). Like the request, JSON is the typical format used but some APIs support other formats such as XML or BSON. Typically the client specifies what formats it would accept when sending the request and the server responds with the format it supports.

The response body may not always be the expected data. In the case of an error, in addition to the status code, most servers will also return an error object indicating what failed. The [RFC7807](https://tools.ietf.org/html/rfc7807) proposal is currently being developed to provide consistency for error reporting in REST APIs but has not yet been approved. Some APIs already support this proposal but others have not. Refer to the documentation for a specific API to learn how it reports errors. 

## Versioning

APIs form a contract between the client and server. A client and server must agree on what data will be sent and received for each request. Changes to the contract may break older clients so each change has to be evaluated for breaking changes. Some examples of breaking changes include the following.

- Adding a required input to a request.
- Removing or renaming fields in a request or response.
- Changing the URL of a resource.

Versioning is important to help manage the changes. There are different ways to version a REST API. Some common approaches include in the URL, as part of the query string or in the request header. Each have their own advantages and disadvantages. It is important for clients to use the latest version available and to periodically update their code to use the newer versions. There is no standard for when a versioned API will go away. Some companies keep only the current and last version while other companies keep all previous versions. 
