# Sorting

- [How It Works](#how-it-works)
   
Sorting is useful when returning lists of items. Sorting is often combined with paging to allow for loading only the desired items without the need for enumerating all the items. Each API and resource will define if it supports sorting and what fields can be sorted.

*Note: Clients should be careful about making assumptions about sort order. Most APIs will return data in a default order but unless documented by the API do not assume the order is consistent. If a client needs items in a specific order then always use the sorting options available.*

# How It Works

A client indicates a desired sort order by passing a parameter to the request, generally in the URL. The parameter used to indicate sorting vary by API but is generally something like `$orderBy` or `$sort` (e.g. `https://tempuri.org/blogs?$orderBy=`). 

Resources have to identify which fields can be sorted and sorting is generally defined per resource. For example blog posts might offer sorting by title or publish date but users would use user name or create date. The client specifies the field to sort by using the sort parameter (e.g. `https://tempuri.org/blogs?$orderBy=published`). 

Most APIs allow either ascending or descending order by use of some modifier. In some cases it is a negative sign on the field (e.g. `https://tempuri.org/blogs?$orderBy=-published`) while others use a keyword (e.g. `https://tempuri.org/blogs?$orderBy=published desc`).

In some cases a resource might allow multiple sorting fields. This can be handled either by allowing multiple values in the sorting parameter (e.g. `https://tempuri.org/blogs?$orderBy=published,title`) or by using multiple parameters with the same name (e.g. `https://tempuri.org/blogs?$orderBy=published&$orderBy=title`).
