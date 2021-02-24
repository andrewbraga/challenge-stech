 # API de distribuição de lucros

Essa solução foi criada para atender o desafio da Stone Co., para resolver o cálculo da distribuição do lucros entre os funcionários cadastrados na base de dados.


## Tecnologias

* .NET 5.0.x
* MediatR
* StackExchange.Redis
* xUnit, NSubstitute e AutoFixture
* Elasticsearch, Serilog, Kibana


#### Pré-requisitos

* Baixe e instale a última versão do [.NET SDK](https://dotnet.microsoft.com/download)
* Baixe e instale o [Docker Desktop](https://www.docker.com/products/docker-desktop)
* Baixe e instale a última versão do [Visual Studio 2019](https://visualstudio.microsoft.com/pt-br/downloads)


## Subir a aplicação no Docker Compose

Abra o CLI na pasta do projeto e execute o comentário abaixo. 

```powershell
PS stech-challenge> docker-compose up
```
`docker-compose.yml` extrai e executa as imagens do Redis, ElasticSearch e Kibana.

Se você estiver executando o Windows 10 pela primeira vez [WSL 2 (Windows Subsystem for Linux)](https://docs.microsoft.com/en-us/windows/wsl/install-win10) Linux Container for Docker, você provavelmente obterá o seguinte erro no docker

`Erro:` max virtual memory areas vm.max_map_count [65530] is too low, increase to at least [262144]

`Solução:` Abra o Linux WSL 2 terminal e execute o comando `sudo sysctl -w vm.max_map_count=262144`para mudar a memória virtual do Linux.


## Debugar no Docker Compose (Utilizando instância local do Redis)

1. Clone o repositório em uma pasta local de sua preferência
2. Abra o arquivo **Stech.Challenge.sln** na pasta local, para abrir a solution no Visual Studio
3. Abrir a janela de propriedade do projeto **WebApi** e clicar na aba **Debug**
4. Alterar o valor da variável de ambiente **ASPNETCORE_ENVIRONMENT** para **Local**
5. Na aba **Solution Explorer**, escolher **docker-compose** como inicialização do projeto
6. Clicar no botão *Docker Compose* no barra de ferramentas acima
7. Abrir no browser o endereço https://localhost:5005/swagger para visualizar o Swagger


## Debugar no IIS Express (Utilizando Redis Cloud)

1. Clone o repositório em uma pasta local de preferência
2. Abra o arquivo **Stech.Challenge.sln** na pasta local, para abrir a solution no Visual Studio
3. Abrir a janela de propriedade do projeto **WebApi** e clicar na aba **Debug**
4. Alterar o valor da variável de ambiente **ASPNETCORE_ENVIRONMENT** para **Development**
5. Na aba **Solution Explorer**, escolher **WebApi** como inicialização do projeto
6. Clicar no botão *IIS Express* no barra de ferramentas acima
7. Abrir no browser o endereço https://localhost:5005/swagger para visualizar o Swagger


### Popular o banco de dados

Existe uma rotina sendo chamada dentro da classe **Program.cs** do projeto **WebApi**, que verifica se o banco de dados está populado toda vez que a aplicação é inicializada. Caso não esteja, a rotina irá popular. 


## Visão Geral

### Domain

Essa camada contém todas as entidades e lógicas específicas da aplicação.

### Application

Esta camada contém toda a lógica da aplicação. É dependente da camada de domínio, mas não tem dependências de nenhuma outra camada ou projeto. Esta camada define interfaces que são implementadas por camadas externas. Por exemplo, se a aplicação precisar acessar um serviço de notificação, uma nova interface seria adicionada a aplicação e uma implementação seria criada dentro da infraestrutura.

### Infrastructure

Essa camada contém classes para acessar recursos externos, como banco de dados, entre outros. Essas classes devem ser baseadas em interfaces definidas na camada da aplicação.

### WebApi

Esta camada é um aplicativo de API da Web baseado em ASP.NET 5.0.x. Essa camada depende das camadas da aplicação e infraestrutura, no entanto, a dependência da infraestrutura é apenas para oferecer suporte à injeção de dependência. Portanto, apenas * Startup.cs * deve fazer referência a Infraestrutura.


### Logs

Logs no Elasticsearch usando Serilog e visualização de logs no Kibana.


## Suporte

Se você estiver tendo problemas, informe-nos [criando uma nova issue](https://github.com/andrewbraga/stech-challenge/issues/new/choose).

