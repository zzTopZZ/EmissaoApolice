# IntroduÇâo
O projeto consiste em cadastrar um cliente, com o cliente cadastrado criar uma proposta, apos analise, e a proposta for aprovada, gerar
uma apolice, senão estiver nesta situação gerara uma mensagem.

# Primeiros Passos

1. Fazer a alteração do "appsettings.json" do Projeto API, colocando o endereço do seu banco de dados SQLServer
      "ConnectionStrings": {
                "DefaultConnection": "Colocar o seu endereço de Banco de dados"
        },
2. Fazer os Migrations
3. Executar o Projeto API
4. Cadatrar um Cliente
5. Cadastrar uma proposta (Status => Criada = 5)
6. Alterar uma proposta para Aprovada (Status => Aprovada = 2)
7. Processar a proposta passando o numero da proposta (propostaId = 1)

# Compilação e Teste
Os testes foram usando NUnit com Moq.

# Contribuição