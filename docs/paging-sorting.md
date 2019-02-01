# Paging and Sorting

Paging and sorting are important techniques for managing large data sets for both the client and server. When working with large data sets it is important that the client does not run out of memory before the data can be retrieved nor wastes time reading data it does not care about. On the server side a "bad" request should not negatively impact the performance. Paging provides clients the ability to request just the information they need and for the server to limit how much is returned.

Sorting controls the order in which items are returned. Sorting is often combined with paging because a change in the sort order would impact the paged results.

- [Paging](#paging)
- [Paging Metadata](#paging-metadata)
- [Client Side Paging](#client-side-paging)
- [Sorting](#sorting)

## Paging

FSMB APIs that return lists of items will usually implement paging. The APIs use the `Page-Offset` model for paging. In this model the client specifies the number of items to return at a time (the page size) and the current "page" to return (the offset). The paging information is passed as part of the query string.

| Parameter | Description |
| - | - |
| $pageSize | The number of items to return in a page. |
| $page | The page to retrieve. Page numbers start at 1. |

`$page` is the one-based page to retrieve. If no page information is specified then it is assumed to be 1.

`$pageSize` is the number of items to return. If not specified then the default page size is used. The default page size can vary by API and resource. The default page size is determined based upon how the resource is used and average items that may be returned.

```shell
curl -X GET \
   '{url}?$page=2&$pageSize=20`
```

Each API and resource will also have a maximum page size. The maximum page size limits how many items can be returned at once and may be dynamically determined based upon how much is available, current load on the server and other attributes. clients should never make assumptions about the maximum page size. If a client requests a page size larger than the maximum page size then the maximum page size will be used.

## Paging Metadata

When paging is used it is often important to know how many total items are available. FSMB APIs return metadata in the HTTP header related to paging.

| Header | Description |
| - | - |
| Link | Provides URLs to other pages. |
| X-Page-Count | The total pages in the data set. |
| X-Total-Count | The total items in the data set. |

In adddition to the count headers an API can return links to the next, previous, first and last page of data. This reduces the need of a client to calculate the information itself. The URL can be used in subsequent calls to move to other pages. The following `Link` headers may be available.

| Link | Description |
| - | - |
| first | The first page |
| last | The last page |
| next | The next page |
| prev | The previous page |

```http
X-Total-Count 150
X-Page-Count 15
Link <{url}?$page=1&$pageSize=10>; rel="first", <{url}?$page=5&$pageSize=10>; rel="next", <{url}?$page=3&$pageSize=10>; rel="prev", <{url}?$page=15&$pageSize=10>; rel="last"
```

## Client Side Paging

Paging is straightforward to implement on the client side. The client first decides how many items to return at once. The client initializes a variable to represent the current page and sets it to 1. Each time the client requests the next page of data it increments the current page variable by 1. The client continues to read pages of data until all the data has been read.

```csharp
var currentPage = 1;
var totalCount = 0;
var pageSize = 25;
do
{
   //Assume the client has a method that returns the next page of items
   var results = client.GetItems(pageSize, currentPage);      
   if (!results.Any())
      break;
    
   //Process the next page of items
  
   //Move to the next page
   ++currentPage;
} while (true);
```

*Note: Do not change the page size while paging data. Doing so may cause items to be skipped or duplicated.*

An alternative approach which is better suited for a paging UI is to use the `Link` header to get the direct URLs for getting the desired pages.

## Sorting

In general data returned from a REST API is in an undefined order. Clients should not make assumptions about ordering. When paging data ordering becomes important so sorting is useful. Sorting is specified in the URL as part of the query string.

| Parameter | Description |
| - | - |
| $orderBy | The field(s) to sort by and whether it is ascending or descending.

The `$orderBy` parameter contains one or more fields separated by commas. Each field may optionally specify ascending (`asc`) or descending (`desc`) order. The fields that can be sorted by and the supported ordering is defined by the resource so refer to the documentation for which fields are allowed.

```shell
curl -X GET \
   '{url}?$page=2&$pageSize=20&$orderBy=field1, field2 desc`
```

*Note: Do not change the sort order while paging data. Doing so may cause items to be skipped or duplicated.*
