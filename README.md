# Currency Calculator
> It is a .Net Web API MVC app in .net core 3.1.1

## Extra Nugets Components
> EntityFrameworkCore 3.1.1

> Microsoft.EntityFrameworkCore.SqlServer 3.1.1

> Microsoft.AspNetCore.Identity.EntityFrameworkCore 3.1.1

> EntityFrameworkCore.InMemory 3.1.1

> Microsoft.AspNetCore.Authentication.JwtBearer 3.1.1

> Newtonsoft.Json 12.0.3

> Swashbuckle.AspNetCore 5.0.0


## Developer enviroment
> Run with `dotnet run`

> Documentation generates with Swagger and is at /index.html


## instructions
> At the start will be automate created a database will be created and some sample data (/App_Data/seed.sql) will be seeding.

> Three users will be create (admin@admin.com, trader@trader.com, test@test.com) with the rolls (Admin, Trader, User) respectively.

> In order to create a bearer token to have the access for trader and admin requests you must go to POST /api/tokes and complete a suitable email from the above and for the password complete with P@sw0rd.
