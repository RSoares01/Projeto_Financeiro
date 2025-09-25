Projeto_Financeiro
🚀 Tecnologias Utilizadas

 -ASP.NET Core 8

 -Entity Framework Core

 -SQL Server (LocalDB ou SQL Express)

 -JWT Authentication (Autorização por Roles)

 -xUnit + Moq (testes)

🛡️ Validações de Negócio

 -As seguintes regras de negócio foram implementadas e validadas diretamente no banco de dados, garantindo maior segurança e integridade dos dados:

 -Valor deve ser maior que zero

 -Data não pode ser futura

 -Categoria deve existir e estar ativa

 -Descrição é obrigatória

🔧 Como rodar localmente
 -Pré-requisitos:

 -.NET 8 SDK

 -SQL Server Express LocalDB

 -Visual Studio 2022 ou superior (ou VS Code com extensão C#)

 -Git instalado

 -Passos para rodar o projeto:

 Clone o repositório:

 -git clone https://github.com/RSoares01/Projeto_Financeiro.git
 -cd Projeto_Financeiro


Verifique a connection string do banco de dados no arquivo appsettings.json na raiz do projeto (ou na pasta da API).
 -Exemplo de connection string local para SQL Server Express LocalDB:

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ProjetoFinanceiroDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}


Ajuste o nome do banco ou o servidor se necessário, de acordo com sua configuração local.

Rode a API de autenticação primeiro, que é responsável por gerar o token JWT:

cd Projeto_Financeiro/AuthApi
dotnet run


Isso vai iniciar a API de auth, normalmente no endereço https://localhost:5001 (confirme na saída do terminal).

No Postman (ou outro cliente), faça a autenticação usando a API de auth para obter o token JWT.
Use esse token para acessar as outras APIs, configurando o cabeçalho Authorization com:

Bearer <seu_token_jwt_aqui>


Rode a API principal:

cd ../MainApi  # ou a pasta onde está a API principal
dotnet run


Para gerar relatórios em Excel, altere o caminho onde o arquivo será salvo no appsettings.json:

  "FileSettings": {
    "SavePath": "C:\\Users\\Desktop\\Repository"
}


Recomendamos definir um caminho dentro da pasta do seu projeto para facilitar o acesso aos arquivos gerados.

📦 Rodando os testes

Para executar os testes automatizados (xUnit + Moq), use:

 -dotnet test
