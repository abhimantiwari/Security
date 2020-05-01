## Authenticate users with WS-Federation in ASP.NET Core 3.1
###### Contains the security and authorization middleware samples code for ASP.NET Core 3.1.

##### Add reference of these packages from Nuget
```
Microsoft.AspNetCore.Authentication.WsFederation
Microsoft.AspNetCore.DataProtectin.Abstraction
```

#### Call the Authorize on top of controller where auth should be called.
```C#
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
```
#### Update properties Wtrealm & MetadataAddress in Startup.ConfigureServices
```
options.Wtrealm = "https:localhost:44307"; //Your application's URL
options.MetadataAddress = "https://YOUR_ADFS_DOMAIN/federationmetadata/2007-06/federationmetadata.xml";
```
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/ws-federation?view=aspnetcore-3.1
