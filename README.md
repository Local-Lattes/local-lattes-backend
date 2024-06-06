# Local Lattes Backend

## Get Started

### Local Development

For local development you can create a local docker postgres database

```sh
docker compose up -d
```

This is start a postgres server at port `20012`

### Start dotnet server

To start the project run

```sh
dotnet run
```

### Creating database secrets

It is essential to keep database credentials hidden from the project.

```cs
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<YOUR_DB_CONNECTION>"
```

This is override the appSettings.json database connection string

### Database Migrations

Create new migration

```sh
dotnet ef migrations add <MIGRATION_NAME>
```

Apply migrations to database

```sh
dotnet ef database update
```