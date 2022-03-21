<h1 align="center">IMBox</h1>

## How to run IMBox

### With docker compose for development

#### API servers

```bash
docker-compose -f docker-compose.dev.yml up -d
```

```bash
dotnet restore
```

```bash
dotnet build
```

```bash
dotnet run --project ./src/Services/<service name>/<service name>.API/IMBox.<service name>.API.csproj --no-build
```

#### Frontends

```bash
cd ./src/Web
```

```bash
yarn install
```

```bash
yarn dev-admin // admin dashboard
```

```bash
yarn dev-public // IMBox public frontend
```

### With docker compose for production

```bash
docker-compose -f docker-compose.prod.yml up -d
```
