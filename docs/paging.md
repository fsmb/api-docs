# Paging

- [How It Works](#how-it-works)
- [Paging Models](#paging-models)
  - [Page Offset Model](#page-offset-model)

Paging is an important technique for managing large sets of data so that the client and server do not run out of resources. The client will request the number of items to return and, after the first page, where to resume from as part of the request. The server will use the provided information to return just the requested number of items. This helps ensure that the client does not receive more data than they can process.

## How It Works

When a client makes a request to the server for data it may specify the number of results to return (e.g. the page size or top). The server will return up to the requested number of items. To read subsequent "pages" of data the client can also send data for how many items to skip (e.g. the index or offset).

In addition to the data itself servers generally send metadata either as part of the response body or in the HTTP header. The metadata often includes the total number of items, the current paging options and information about how to get the next and previous items.

Servers generally implement a default paging size so a client who does not specify any paging options does not get back all the data. The default size is generally based upon how many times may be typically returned and the size of the response. Servers will generally also implement a maximum page size. This prevents a client from accidentally requesting more data than the server can handle and negatively impacting performance. Generally the maximum size is not documented because it can change based upon server load or configuration. 

When using paging it is important for a client to set the paging parameters appropriately so it is possible to retrieve all data if needed. Clients need to be careful about calculating paging offsets and sizes so items are not skipped. This becomes harder if the items are dynamically changing while being paged. 

Clients should not make assumptions about how many items are returned or when all items have been read. The server may return less items than requested for a variety of reasons including insufficient items to meet the request, the specified paging size is too large or the items are not yet ready. Clients should rely on the returned metadata for determining how many items were returned and whether more data is available.

## Paging Models

There are several different approaches to paging that can be used. Each API determines the paging model it will use.

### Page Offset Model

In the page offset model the pages are identified by their size and offset. As an example the page size may be 10 and the offset 2. The server would skip the first 2 pages (20 items) and then return the next 10. This model is simple to implement, easy to calculate on the client side and mimics how a UI may display results.

For this model to work the client and server must agree upon the starting point for offset (e.g. 0, 1, -1). Additionally the page size must remain the same for the life of the paging request otherwise items may be skipped or returned multiple times. Page size must also be agreed upon. Some APIs will return the default number of items if page size is 0 while others may return no items but include the metadata for how many are available. The client and server must also agree on what happens if invalid pages are requested.

In a typical paging implementation the paging parameters are passed as part of the query string (e.g. `$pageSize=10&$pageOffset=2`). Page sizes greater than the maximum supported by the server usually are reset to the maximum. Specifying no page size (or a value of 0) will use the default page size. The page offset, when excluded, assumes the first page.

Enumerating the list of items in this model is pretty straightforward.

```text

Send the initial request with no page offset and the optional page size desired.
Do
  Enumerate the response body to determine how many items were sent.
  Read the count from the metadata to determine how many items are available.
  If the metadata includes the URL to the next page then use the provided URL to load the next set of data.
  Otherwise increment the offset by 1 and send the request again.
While count is greater than items read
```   
