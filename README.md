## How to run

Add migration:
```bash
dotnet ef migrations add InitialCreate --project src/TelegramBridge.Infrastructure --startup-project src/TelegramBridge.Api --output-dir Persistence/Migrations
```

Apply migrations:
```bash
dotnet ef database update  --project src/TelegramBridge.Infrastructure --startup-project src/TelegramBridge.Api
```