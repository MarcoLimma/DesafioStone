# DesafioStone
Solução para monitorar a variação de temperatura de determinadas cidades

# Sobre
A solução foi desenvolvida utilizando tecbologias como: Web API do ASP.NET, Entity Framework, MSTest e SQL Server. 
Está organizada em 5 projetos:
- API:
  É a camada mais externa, onde as funcionalidades da aplicação poderão ser acessadas.
- DATA:
  É a camada de acesso aos dados da aplicação.
- DOMAIN:
  É a camada de que define o domínio da aplicação.
- TASKTEMPERATURE:
  É o projeto da tarefa responsável por consumir os dados da API de clima e popular o banco de dados com as temperaturas.
- TESTS:
  É o projeto onde são realizado os testes da aplicação.
  
# Observações
  A task deve ser cadastrada no Task Scheduler do Windows, utilizando definições de acordo com o proposto no desafio (tempo de repetição igual a uma hora). 
  A escolha por task no lugar de um serviço foi meramente com intuito de simplificar o processo de desenvolvimento.   
