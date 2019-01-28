# FSMB API Documentation

This documentation provides a general discussion of how the FSMB APIs work and how to use them in a client application. Unless otherwise stated all FSMB APIs follow the general guidelines given here. Refer to the documentation for each API for any differences that may exist.

## REST

All FSMB APIs are [REST](https://en.wikipedia.org/wiki/Representational_state_transfer) based. This provides a consistent, simple format that clients can use to programmatically call FSMB. REST is based upon the HTTP standard and is supported in any programming language that supports HTTP calls such as C#, Java, Ruby and Python without the need for complex types. Many languages have implicit support for REST APIs to make it even easier to use REST APIs. Refer to your specific language's documentation for more information on how to make REST calls.

## Authentication

To help protect physician information and to ensure that requests are properly audited, all FSMB APIs require authentication to be used.  FSMB uses OAuth2 with client credentials for authentication. Refer to the section on [Authentication](docs/authentication.md) for more information.

## HTTP Status Codes

HTTP has well known [status codes](https://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html) for indicating success and failure 

## HTTP Verbs

## Errors

## Data Formats

## Postman

## 



