# aspnet-core-webapi

Esse é um projeto para futuras consultas de técnicas empregadas com ASP.NET Core 2.2 aplicando [API-Guidelines](https://github.com/Microsoft/api-guidelines) da Microsoft.

Técnicas

- [x] RESTfull
- [x] Autenticação JWT
- [x] Swagger
- [x] Versionamento de API
- [x] Docker
- [x] HealthCheck - Utilizando a lib [Xabaril/AspNetCore.Diagnostics.HealthChecks](https://github.com/xabaril/AspNetCore.Diagnostics.HealthChecks)
  - Acessar o endpoint /healthz da api no browser
  - Acessar a url http://localhost:8003/healthchecks-ui para visualizar um painel com o monitoramento, disponível somente com docker-compose
- [x] App.Metrics + Prometheus + Grafana
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
- [x] MessageBrokers 
  - [x] RabbitMQ
    - [x] - Criação customizada de exchanges/queue por Atributos 
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

E acessar a página [http://localhost:8181/index.html](http://localhost:8181/index.html)

## k8s

Para o exemplo vamos utilizar o `docker stack` que tem suporte ao Kubernetes.

> Docker stack somente suporte imagens já buildadas

Vamos buildar todos os nossos softwares:

```bash
docker-compose build
```

e começar a brincadeira criando o namespace que vamos utilizar:

```bash
kubectl create namespace demo
```

e então criar o stack:

```bash
docker stack deploy --namespace demo -c docker-compose.yml appstack
```

e vamos validar nosso deployment:

```bash
kubectl get all -n demo

NAME                             READY     STATUS    RESTARTS   AGE
pod/healthapp-78959fcf7b-5xj6b   1/1       Running   0          12s
pod/mongodb-d5cc949f5-pqjqs      1/1       Running   0          12s
pod/mysqldb-5c8dccbd78-w5f8l     1/1       Running   0          12s
pod/rabbitmq-544d746545-hpm7d    1/1       Running   0          12s
pod/redis-d56fc6bdf-bmmds        1/1       Running   0          12s
pod/web-78bc8955d4-2sjkq         0/1       Error     0          12s

NAME                          TYPE           CLUSTER-IP       EXTERNAL-IP   PORT(S)                          AGE
service/healthapp             ClusterIP      None             <none>        55555/TCP                        12s
service/healthapp-published   LoadBalancer   10.97.148.148    localhost     8083:30610/TCP                   9s
service/mongodb               ClusterIP      None             <none>        55555/TCP                        9s
service/mongodb-published     LoadBalancer   10.100.145.160   localhost     27017:30231/TCP                  12s
service/mysqldb               ClusterIP      None             <none>        55555/TCP                        8s
service/mysqldb-published     LoadBalancer   10.105.66.236    localhost     3306:30969/TCP                   12s
service/rabbitmq              ClusterIP      None             <none>        55555/TCP                        12s
service/rabbitmq-published    LoadBalancer   10.104.157.0     localhost     5672:31170/TCP,15672:31195/TCP   7s
service/redis                 ClusterIP      None             <none>        55555/TCP                        7s
service/redis-published       LoadBalancer   10.104.50.187    localhost     6379:30003/TCP                   12s
service/web                   ClusterIP      None             <none>        55555/TCP                        12s
service/web-published         LoadBalancer   10.104.37.39     localhost     8181:30589/TCP                   8s

NAME                        DESIRED   CURRENT   UP-TO-DATE   AVAILABLE   AGE
deployment.apps/healthapp   1         1         1            1           13s
deployment.apps/mongodb     1         1         1            1           13s
deployment.apps/mysqldb     1         1         1            1           12s
deployment.apps/rabbitmq    1         1         1            1           13s
deployment.apps/redis       1         1         1            1           13s
deployment.apps/web         1         1         1            0           13s

NAME                                   DESIRED   CURRENT   READY     AGE
replicaset.apps/healthapp-78959fcf7b   1         1         1         13s
replicaset.apps/mongodb-d5cc949f5      1         1         1         12s
replicaset.apps/mysqldb-5c8dccbd78     1         1         1         12s
replicaset.apps/rabbitmq-544d746545    1         1         1         13s
replicaset.apps/redis-d56fc6bdf        1         1         1         13s
replicaset.apps/web-78bc8955d4         1         1         0         13s
```

Para remover a stack:

```bash
docker stack rm appstack --namespace demo
```

## SonarQube

### Configuração

### Subindo Análise