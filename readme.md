# quick-start-presentation 

Sistema básico que inclui autenticação de usuário

Este projeto é parte do curso que pretendo lançar: **full cicle prototype, years in minutes**

## get started

```ps
cd Presentation

dotnet run
```

## fundação instantanea

Para reproduzir esté repositório siga as etapas a seguir:

- [1] Criar o projeto a partir do template oficial da microsoft.

```ps

mkdir Presentation
cd Presentation

#https://docs.microsoft.com/pt-br/dotnet/core/tools/dotnet-new-sdk-templates#web-options

dotnet new webapp --auth Individual

dotnet tool install -g dotnet-ef

dotnet ef database update

```

- [2] Desativar confirmação de e-mail para facilitar o login

```csharp

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false) // desabilitando confirmação de email
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

```

- [3] Gerar páginas de login e registro de usuário para estilização

```ps

#https://docs.microsoft.com/pt-br/aspnet/core/security/authentication/scaffold-identity?view=aspnetcore-6.0&tabs=netcore-cli#scaffold-identity-into-a-razor-project-with-authorization

dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design

dotnet tool install -g dotnet-aspnet-codegenerator

dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Microsoft.AspNetCore.Identity.UI
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools

dotnet aspnet-codegenerator identity -dc Presentation.Data.ApplicationDbContext --files "Account.Register;Account.Login"

```
