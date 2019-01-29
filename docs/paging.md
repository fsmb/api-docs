# Paging

- [Paging Models](#paging-models)
  - [Page Offset Model](#page-offset-model)

Paging is an important technique for managing large sets of data. When dealing with lists of items paging is generally implemented. The client will request the number of items to return and, after the first page, where to resume from as part of the request. The server will use the provided information to return just the requested number of items. This helps ensure that the client does not receive more data than they can process.

Servers generally implement a default paging size so a client who does not specify any paging options does not get back all the data. The default size is generally based upon how many times may be typically returned and the size of the response. Servers will generally also implement a maximum page size. This prevents a client from accidentally requesting more data than the server can handle and negatively impacting performance. Generally the maximum size is not documented because it can change based upon server load or configuration. 

When using paging it is important for a client to set the paging parameters appropriately so it is possible to retrieve all data if needed. Clients need to be careful about calculating paging offsets and sizes so items are not skipped. This becomes harder if the items are dynamically changing while being paged. 

Clients should not make assumptions about how many items are returned or when all items have been read. The server may return less items than requested for a variety of reasons including insufficient items to meet the request, the specified paging size is too large or the items are not yet ready. Clients should rely on the returned metadata (generally in the HTTP header) to know much data, if any, remains. The server is responsible for reporting whether more information is available and, optionally, how many items are available total.

## Paging Models

There are several different approaches to paging that can be used. Each API determines the paging model to use.

### Page Offset Model

In the page offset model the pages are identified by their size and offset (generally 0 or 1). As an example the page size may be 10 and the offset 2. The server would skip the first 2 pages (20 items) and then return the next 10. This model is simple to implement, easy to calculate on the client side and easily lines up with how a UI may display results.

For this model to work the client and server must agree upon the starting point for offset (e.g. 0, 1, -1). Additionally the page size must remain the same for the life of the paging request otherwise items may be skipped or returned multiple times.

In a typical paging implementation the paging parameters are passed as part of the query string (e.g. `$pageSize=10&$pageOffset=2`).
