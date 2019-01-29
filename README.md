# FSMB API Documentation

This documentation provides a general discussion of how the FSMB APIs work and how to use them in a client application. Unless otherwise stated all FSMB APIs follow the guidelines given here. 

For information on a specific API please refer to its documentation. For information and help using an API please contact FSMB using the contact information provided for the specific API you wish to use.

- [Rest APIs](#rest-apis)
  - [Versioning](#versioning)
  - [Supported Data Formats](#supported-data-formats)
  
- [Authentication](#authentication)
- [Paging and Sorting](#paging-and-sorting)


## REST APIs

APIs provided by FSMB are REST-based. REST provides a consistent, simple format that clients can use to programmatically call FSMB. 

REST is based upon the HTTP standard and is supported in any programming language that supports HTTP calls such as C#, Java, Ruby and Python without the need for complex types. Many languages have implicit support for REST APIs to make it even easier to use them. Refer to your specific language's documentation for more information on consuming REST APIs.

You can learn more about REST APIs [here](docs/rest.md).

### Versioning

FSMB currently versions APIs at the API or resource level in the URL. FSMB is committed to not making breaking changes in existing APIs. If a breaking change is needed then a new version will be introduced. In some cases a "preview" version may be available for clients to try out an API. 

**Note: Preview versions should not be used in production as they can be deprecated, changed or removed without warning.**

You can learn more about versioning [here](docs/rest.md#versioning).

### Supported Data Formats

At this time FSMB supports the following formats for requests and responses.

- [JSON](https://www.json.org/)

## Authentication

To help protect physician information and to ensure that requests are properly audited, all FSMB APIs require authentication to be used.  FSMB uses OAuth2 with client credentials for authentication. Clients must contact FSMB to be given a client ID and secret to authenticate. Clients will be granted access to scopes based upon their needs. Refer to the documentation for each API for more information. 

You can learn more about authentication [here](docs/authentication.md).

## Paging/Sorting

Paging is an important technique for managing requests for large sets of data such as lists of users. REST APIs will generally return back a 

## Error Reporting

## Using Postman
