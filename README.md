# Gestão de Equipamentos

Junior cuida do estoque de equipamentos na empresa onde trabalha. E sempre controla o inventário dos seus equipamentos e as manutenções que eles já sofreram em uma planilha do Excel.

Desta forma, ele resolveu pedir ajuda do pessoal da Academia do Programador no desenvolvimento de um Software para automatizar o seu serviço.

## 1. Controle de Equipamentos

#### Requisito 1.1:

Como funcionário, Junior quer ter a possibilidade de registrar equipamentos

- Deve ter identificador único (id)
- Deve ter um nome com no mínimo 6 caracteres;
- Deve ter um preço de aquisição;
- Deve ter uma fabricante;
- Deve ter uma data de fabricação;

#### Requisito 1.2:

Como funcionário, Junior quer ter a possibilidade de visualizar todos os equipamentos registrados em seu inventário.

- Deve mostrar o id;
- Deve mostrar o nome;
- Deve mostrar o preço de aquisição;
- Deve mostrar a fabricante;
- Deve mostrar a data de fabricação;

#### Requisito 1.3:

Como funcionário, Junior quer ter a possibilidade de editar um equipamento, sendo que ele possa editar todos os campos.

- Deve ter os mesmos critérios que o Requisito 1.1.

#### Requisito 1.4:

Como funcionário, Junior quer ter a possibilidade de excluir um equipamento que esteja registrado.

- A lista de equipamentos deve ser atualizada

## Como utilizar

1. Clone o repositório ou baixe o código fonte.
2. Abra o terminal ou o prompt de comando e navegue até a pasta raiz
3. Utilize o comando abaixo para restaurar as dependências do projeto.

   ```bash
   dotnet restore
   ```

4. Para executar o projeto compilando em tempo real

   ```bash
   dotnet run --project GestaoDeEquipamentos.ConsoleApp
   ```

## Requisitos

- .NET 10.0 SDK
