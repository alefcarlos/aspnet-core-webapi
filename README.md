# aspnet-core-webapi

Esse é um projeto para futuras consultas de técnicas empregadas com ASP.NET Core 2.2.

Técnicas

- [x] RESTfull
- [x] Autenticação JWT
- [x] Swagger
- [x] Versionamento de API
- [x] Docker
- [x] HealthCheck
  - Acessar a url /healthz no browser
- [x] FluentValidation
- [x] GraphQL
    - Acessar a url /ui/playground no browser
- [x] EF
    - [x] MySQL 
	- [x] Migrations
- [ ] Resiliência requisições Http(utilizando Polly)
  - [x] Retry
  - [x] Timeout
  - [ ] CircuitBreaker
- [x] Caching
  - [x] Redis - Baseado [nessa](https://ruhul.wordpress.com/2014/07/23/use-redis-as-cache-provider/ ) implementação
- SonarQube

# GraphQL

Para utilizar o GraphQL é necessário ter um token de autenticação Bearer.
Acessando a url `https://localhost:5001/ui/playground` terá disponível uma UI para realizar alguns testes ;)

> Para adicionar novos personagens basta realizar as operações do contorller Dragon Ball.

Tipos disponívels:

```graphql
type Character {
    id: int,
    name: string!,
    bithDate: string!,
    relatives: [Relative!],
    kind: Kind!
}

type Relative {
    id: int!,
    name: string!,
    bithDate: string!,
    relatives: [Relative!],
    relativeKind: RelativeKind!
}

enum Kind {
    HUMAN = 1,
    SAYAJIN = 2
}

enum RelativeKind {
    Brother = 1,
    Sister = 2,
    Son = 3,
    Daugther = 4,
    Spouse = 5,
    Father = 6 ,
    Mother = 7
}
```

Queries disponíveis

```graphql
characters
character(id: int)
```

Mutations disponíveis

```
createCharacter(character: Character)
```
# Docker

O arquivo `Dockerfile` já contém as instruções necessárias para serem buildadas, vamos executar o comando abaixo para compilar uma imagem.

```bash
docker build -t aspnet-core-webapi .
```

## Docker-compose

O arquivo `docker-compose.yml` já contém as imagens necessárias para rodar a aplicação, basta executar o comando abaixo:

```bash
docker-compose build
docker-compose up
```

A porta do container é 80, porém estará pública na porta 8181.

E acessar a página [http://localhost:8181/swagger/index.html](http://localhost:8181/swagger/index.html)

## SonarQube