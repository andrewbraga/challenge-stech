﻿ # API de distribui��o de lucros

Essa solu��o foi criada para atender o desafio da Stone Co., para resolver o c�lculo da distribui��o do lucros entre os funcion�rios cadastrados na base de dados.


## Tecnologias

* .NET 5.0.x
* MediatR
* StackExchange.Redis
* xUnit, NSubstitute e AutoFixture
* Elasticsearch, Serilog, Kibana


#### Pr�-requisitos

* Baixe e instale a �ltima vers�o do [.NET SDK](https://dotnet.microsoft.com/download)
* Baixe e instale o [Docker Desktop](https://www.docker.com/products/docker-desktop)
* Baixe e instale a �ltima vers�o do [Visual Studio 2019](https://visualstudio.microsoft.com/pt-br/downloads)


## Subir a aplica��o no Docker Compose

Abra o CLI na pasta do projeto e execute o coment�rio abaixo. 

```powershell
PS stech-challenge> docker-compose up
```
`docker-compose.yml` extrai e executa as imagens do Redis, ElasticSearch e Kibana.

Se voc� estiver executando o Windows 10 pela primeira vez [WSL 2 (Windows Subsystem for Linux)](https://docs.microsoft.com/en-us/windows/wsl/install-win10) Linux Container for Docker, voc� provavelmente obter� o seguinte erro no docker

`Erro:` max virtual memory areas vm.max_map_count [65530] is too low, increase to at least [262144]

`Solu��o:` Abra o Linux WSL 2 terminal e execute o comando `sudo sysctl -w vm.max_map_count=262144`para mudar a mem�ria virtual do Linux.


## Debugar no Docker Compose (Utilizando inst�ncia local do Redis)

1. Clone o reposit�rio em uma pasta local de sua prefer�ncia
2. Abra o arquivo **Stech.Challenge.sln** na pasta local, para abrir a solution no Visual Studio
3. Abrir a janela de propriedade do projeto **WebApi** e clicar na aba **Debug**
4. Alterar o valor da vari�vel de ambiente **ASPNETCORE_ENVIRONMENT** para **Local**
5. Na aba **Solution Explorer**, escolher **docker-compose** como inicializa��o do projeto
6. Clicar no bot�o **Docker Compose** no barra de ferramentas acima
7. Abrir no browser o endere�o https://localhost:5005/swagger para visualizar o Swagger


## Debugar no IIS Express (Utilizando Redis Cloud)

1. Clone o reposit�rio em uma pasta local de prefer�ncia
2. Abra o arquivo **Stech.Challenge.sln** na pasta local, para abrir a solution no Visual Studio
3. Abrir a janela de propriedade do projeto **WebApi** e clicar na aba **Debug**
4. Alterar o valor da vari�vel de ambiente **ASPNETCORE_ENVIRONMENT** para **Development**
5. Na aba **Solution Explorer**, escolher **WebApi** como inicializa��o do projeto
6. Clicar no bot�o **IIS Express** no barra de ferramentas acima
7. Abrir no browser o endere�o https://localhost:5005/swagger para visualizar o Swagger


### Popular o banco de dados

Existe uma rotina sendo chamada dentro da classe **Program.cs** do projeto **WebApi**, que verifica se o banco de dados est� populado toda vez que a aplica��o � inicializada. Caso n�o esteja, a rotina ir� popular. 


## Vis�o Geral

### Domain

Essa camada cont�m todas as entidades e l�gicas espec�ficas da aplica��o.

### Application

Esta camada cont�m toda a l�gica da aplica��o. � dependente da camada de dom�nio, mas n�o tem depend�ncias de nenhuma outra camada ou projeto. Esta camada define interfaces que s�o implementadas por camadas externas. Por exemplo, se a aplica��o precisar acessar um servi�o de notifica��o, uma nova interface seria adicionada a aplica��o e uma implementa��o seria criada dentro da infraestrutura.

### Infrastructure

Essa camada cont�m classes para acessar recursos externos, como banco de dados, entre outros. Essas classes devem ser baseadas em interfaces definidas na camada da aplica��o.

### WebApi

Esta camada � um aplicativo de API da Web baseado em ASP.NET 5.0.x. Essa camada depende das camadas da aplica��o e infraestrutura, no entanto, a depend�ncia da infraestrutura � apenas para oferecer suporte � inje��o de depend�ncia. Portanto, apenas **Startup.cs** deve fazer refer�ncia a Infraestrutura.


### Logs

Logs no Elasticsearch usando Serilog e visualiza��o de logs no Kibana.


## Suporte

Se voc� estiver tendo problemas, informe-nos [criando uma nova issue](https://github.com/andrewbraga/stech-challenge/issues/new/choose).

