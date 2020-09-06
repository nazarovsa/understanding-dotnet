Examples of using http client in ASP.NET Core
=============================================

Samples from [Microsoft Docs](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests)

You can run sample in docker.
Build it from solution directory and run: 
```
docker build -t http-client-sample . -f ./UnderstandingDotNet.IHttpClientFactory.AspNet/Dockerfile
docker run -p8080:80 --name hcs http-client-sample
```

Then access api by localhost:8080
