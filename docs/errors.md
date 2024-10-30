# Errors

Error handling is important when calling an API. APIs can fail for a variety of reasons including bad inputs, server errors or network outages. Clients should always ensure they are validating the response from an API. 

```csharp
using (var response = await client.SendAsync(message, cancellationToken))
{
   response.EnsureSuccessStatusCode();
   
   //Process response
};
```

In addition to the HTTP status code, some errors such as those from bad inputs or server errors will return a detailed error object in the response body. Currently there is no standard for reporting errors in REST APIs, but [RFC7807 Problem Details](https://tools.ietf.org/html/rfc7807) has received a lot of support.

Newer FSMB APIs implement the Problem Details pattern for errors.
```json
{
   "type": " A URI that identifies the specific error type",
   "title ": "A short, human-readable summary of the problem",
   "status": "The HTTP status code generated for the problem",
   "detail": "A more detailed, human-readable explanation of the problem.",
   "errors": { }
}
```

If the error stems from argument exception(s), the `errors` property contains a dictionary for the argument error messages.


Other FSMB APIs not implementing Problem Details will return the following error object when returning 4xx or 5xx errors in most cases.

```json
{
   "code": "Error code",
   "message": "Descriptive message",
   "target": "Optional target of the error",
   "logId": "The ID of the log entry associated with the error, if any",
   "innerError": { },
   "data": { }
}
```

If the error was caused by a lower level error, then `innerError` contains the error detail of the child error. Some errors may return additional data in the `data` property. The `code` field identifies the underlying error that occurred while the `message` field provides a developer friendly message.

