# ASP.NET Core PDF Preview and Download

This is a sample application that demonstrates the difference between in-browser previews for PDFs and triggering the download of a PDF File.

TL;DR: It's all about providing the correct content type and filename vs. not providing the filename.

## Preview In-browser Code

You need to provide the correct content type so the browser can understand the bytes being streamed from the server. In the case of PDFs, it's `application/pdf`.

```csharp
public IActionResult Show()
{
    var pdf = GetPdfDocuments();
    // the lack of a filename tells the browser
    // to open up the file if the mime-type is associated
    // with an application.
    // Most browsers can render PDF natively, so it
    // just opens in the current browser session.
    return File(pdf, "application/pdf");
}
```

This produces the following response headers.

```text
Content-Length: 78127
Content-Type: application/pdf
Date: Wed, 06 Sep 2023 14:17:30 GMT
Server: Kestrel
```

## Download Code

To trigger a download on the client, you only need to provide a filename, along with the content type.

```csharp
public IActionResult Download()  
{  
    var pdf = GetPdfDocuments();  
    return File(pdf, "application/pdf", "download.pdf");  
}
```

This produces the following response headers.

```text
Content-Disposition: attachment; filename=download.pdf; filename*=UTF-8''download.pdf
Content-Length: 78109
Content-Type: application/pdf
Date: Wed, 06 Sep 2023 14:17:30 GMT
Server: Kestrel
```

## Conclusion

This should work in most browsers. The only caveat is that users can change the preferences in their browser to behave differently, so it's not always guaranteed to get you the behavior you expect 100% of the time.
