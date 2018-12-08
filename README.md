# aspnet-core-webapi

Esse é um projeto para futuras consultas de técnicas empregadas com ASP.NET Core 2.2.

Técnicas

- [x] RESTfull
- [x] Autenticação JWT
- [x] Swagger
- [x] Versionamento de API
- [x] Docker
- [x] HealthCheck
- [x] FluentValidation
- [x] GraphQL
    - Acessar via /playground
- [x] EF
    - [x] MySQL 

# Docker

O arquivo `Dockerfile` já contém as instruções necessárias para serem buildadas, vamos executar o comando abaixo para compilar uma imagem.

```bash
docker build -t aspnet-core-webapi .
```

## Docker-compose

O arquivo `docker-compose.yml` já contém as imagens necessárias para rodar a aplicação, basta executar o comando abaixo:

```bash
docker-compose up
```

A porta do container é 80, porém estará pública na porta 8181.

E acessar a página [http://localhost:8181/swagger/index.html](http://localhost:8181/swagger/index.html)