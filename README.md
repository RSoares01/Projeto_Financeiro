📊 Projeto Financeiro
Um sistema para gerenciamento financeiro com autenticação JWT, relatórios e validações de negócio.

🚀 Tecnologias Utilizadas
	•	ASP.NET Core 8
	•	Entity Framework Core
	•	SQL Server (LocalDB ou SQL Express)
	•	JWT Authentication (com roles)
	•	xUnit + Moq (testes automatizados)


🛡️ Validações de Negócio

As seguintes regras foram implementadas diretamente no banco, garantindo segurança e integridade dos dados:
	•	✅ Valor deve ser maior que zero
	•	✅ Data não pode ser futura
	•	✅ Categoria deve existir e estar ativa
	•	✅ Descrição é obrigatória


🔧 Como Rodar Localmente

📌 Pré-requisitos
	•	.NET 8 SDK
	•	SQL Server Express LocalDB
	•	Visual Studio 2022 ou VS Code com extensão C#
	•	Git

📥 Clonando o Repositório
git clone https://github.com/RSoares01/Projeto_Financeiro.git
cd Projeto_Financeiro


⚙️ Configurando o Banco de Dados

Edite o arquivo appsettings.json e ajuste a connection string se necessário

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ProjetoFinanceiroDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}


▶️ Executando as APIs

1. API de Autenticação

cd Projeto_Financeiro/AuthApi
dotnet run

🔑 Isso vai iniciar a API em https://localhost:5001.

Use o endpoint de auth para gerar seu token JWT.
Adicione o token nos requests das demais APIs:

Authorization: Bearer <seu_token_jwt_aqui>


📊 Relatórios em Excel

Defina no appsettings.json o caminho onde os relatórios serão salvos:


"FileSettings": {
  "SavePath": "C:\\Users\\Desktop\\Repository"
}


📦 Rodando os Testes

Execute os testes automatizados (xUnit + Moq):

dotnet test














