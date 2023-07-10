# Medicar

API de agendamento de consultas médicas desenvolvida com .NET 6

## tecnologias contidas:

- CQRS pattern com MediatR
- DependencyInjection
- EntityFrameworkCore
- AutoMapper
- Swagger Documentation
- Identity
- ApiVersioning
- Integração com o Trello


## Author

- [@leonardolealdev](https://github.com/leonardolealdev)

# Para rodar o projeto siga as intruções abaixo:

## 1. Abrir o Package Manager Console do Visual Studio(Tools -> NuGet Package Manager -> Package Manager Console)

## 2. Apontar o Default Project para Medicar

## 3. Rodar comando "update-database -context ApplicationDbContext

## 4. Apontar o Default Project para Medicar.Infra

## 5. Rodar comando "update-database -context MedicarDbContext

## 6. Na Solution explorer do Visual Studio selecione Medicar como Startup project e rode o projeto

## 7. Projeto funcionando!

Obs 1: Necessário ter PostgreSql intalado com versão maior ou igual a 10

Obs 2: Link do board do trello -> https://trello.com/b/43p4oI0Z/medicar


