# Consuming REST APIs

Given the popularity of REST APIs it is no surprise that there are many different ways to consume an API. Most languages either have direct support or third party libraries that can be used to communicate with a REST API. Clients should always prefer to use the available tools rather than writing their own.

When first learning an API it is often useful to be able to send requests and view the responses without writing any code. There are several tools available for this. FSMB APIs all expose a [Postman](https://www.getpostman.com/) collection that you can use to learn the API. Refer to the documentation for each API to get a link to the collection.

FSMB APIs can be consumed in a variety of ways depending upon need.

[Postman](https://www.getpostman.com/) is a useful tool for testing REST APIs. Postman can be used to make just about any call to an API. To simplify using Postman with FSMB APIs, each API provides a link to the Postman collection (a set of API requests) demonstrating the API. Refer to each API for a link to the Postman collection.

[OpenAPI](https://github.com/OAI/OpenAPI-Specification) (formerly known as Swagger) is a specification that APIs can provide that programmatically descripe the API and its data. OpenAPI allows the specification to be imported into a supported tools and auto-generate a client that can call the API. OpenAPI is supported in many common REST API tools and many tools can generate code for different languages. Before manually writing code check if your language can import the OpenAPI specification. All FSMB APIs provide a link to their OpenAPI specification in the documentation.

Each API provides sample code, in different languages when possible, that can be used as a starting point for calling FSMB APIs. Refer to each API's documentation for the samples.
