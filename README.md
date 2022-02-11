![example workflow](https://github.com/viiparente/DevFitness/actions/workflows/build.yml/badge.svg)

# DevFitness 
API REST em .NET de gerenciamento dos dados de saúde e alimentação do usuário.

### Tecnologias e práticas utilizadas
* SQL Server Express
* ASP.NET Core com .NET 5
* Entity Framework Core
* Dapper
* Swagger
* AutoMapper
* Injeção de Dependência
* Programação Orientada a Objetos
* Publicação na nuvem

### Principais Funcionalidades
* Cadastro, Listagem, Detalhes, Atualização e Remoção de Refeição.
* Cadastro e atualização de Usuário
* Geração de dados para relatório de balanço energético

## Executando o projeto em sua maquina local
-  Clone o projeto
```console
git clone https://github.com/viiparente/DevFitness.git
```
Caso não tenha uma instancia local do SQL Server Express, siga o seguinte passo:
 * Abra o arquivo Program.cs 
 * Comente a linha onde tem o UseSqlServer e descomente o UseInMemoryDatabase
 ```cs
//services.AddDbContext<DevFitnessDbContext>(options => options.UseSqlServer(connectionString));
```
```cs
services.AddDbContext<DevFitnessDbContext>(options => options.UseInMemoryDatabase(connectionString));
```

* Agora vá na pasta onde está o arquivo de solução do projeto (DevFitness.sln) e rode no terminal:
```console
dotnet restore
```
```console
dotnet build --no-restore
```
```console
cd .\DevFitness.API\
```
Caso tenha o SQL
```console
dotnet ef migrations add InitialMigration -o Persistence/Migrations
```
Caso tenha o SQL
```console
dotnet ef database update
```
Finalmente rode o Projeto caso esteja no Visual Studio Code
```console
dotnet run
```

##

Ministrado pelo instrutor [Luis Felipe](https://www.linkedin.com/in/luisdeol/)
