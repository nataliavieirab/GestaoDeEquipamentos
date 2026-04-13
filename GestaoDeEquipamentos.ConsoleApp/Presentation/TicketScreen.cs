using GestaoDeEquipamentos.ConsoleApp.Domain;
using GestaoDeEquipamentos.ConsoleApp.Infra;

namespace GestaoDeEquipamentos.ConsoleApp.Presentation;

class TicketScreen
{
  public ScreenUtils screenUtils;
  public EquipmentScreen equipmentScreen;
  public EquipmentRepository equipmentRepository;
  public TicketRepository repository;

  public string GetMenuOption()
  {
    screenUtils.ShowMainHeader("Gestão de Chamados");
    Console.WriteLine("1 - Cadastrar chamado");
    Console.WriteLine("2 - Editar chamado");
    Console.WriteLine("3 - Excluir chamado");
    Console.WriteLine("4 - Visualizar chamados");
    Console.WriteLine("S - Sair");
    screenUtils.ShowUISimpleLine();
    Console.Write("> ");

    return Console.ReadLine()?.ToUpper()!;
  }

  public void Register()
  {
    screenUtils.ShowMainHeader("Gestão de Chamados");
    screenUtils.ShowOperationHeader("CADASTRO DE CHAMADO");

    equipmentScreen.ShowAllEquipments();

    Ticket? newTicket = GetNewTicket();

    if (newTicket == null)
    {
      screenUtils.ShowUISimpleLine();
      Console.WriteLine($"Não foi possível cadastrar o chamado.");
      screenUtils.ShowUISimpleLine();

      Console.WriteLine("Digite ENTER para continuar...");
      Console.ReadLine();

      return;
    }

    repository.Create(newTicket);

    screenUtils.ShowUISimpleLine();
    Console.WriteLine($"O registro \"{newTicket.id}\" foi cadastrado com sucesso.");
    screenUtils.ShowUISimpleLine();

    Console.WriteLine("Digite ENTER para continuar...");
    Console.ReadLine();
  }

  public void Edit()
  {
    screenUtils.ShowMainHeader("Gestão de Chamados");
    screenUtils.ShowOperationHeader("EDIÇÃO DE CHAMADO");

    ShowAllTickets();

    string? selectedId;

    do
    {

      Console.Write("\n>> Digite o id do chamado que deseja editar: ");
      selectedId = Console.ReadLine();

      if (!string.IsNullOrWhiteSpace(selectedId) && selectedId.Length == 7) break;

    } while (true);

    equipmentScreen.ShowAllEquipments();

    Ticket? newTicket = GetNewTicket();

    if (newTicket == null)
    {
      screenUtils.ShowUISimpleLine();
      Console.WriteLine($"Não foi possível editar o chamado.");
      screenUtils.ShowUISimpleLine();

      Console.WriteLine("Digite ENTER para continuar...");
      Console.ReadLine();

      return;
    }

    bool success = repository.Update(selectedId, newTicket);

    if (!success)
    {
      screenUtils.ShowUISimpleLine();
      Console.WriteLine($"Não foi possível editar o chamado informado.");
      screenUtils.ShowUISimpleLine();
      return;
    }

    screenUtils.ShowUISimpleLine();
    Console.WriteLine($"✅ O registro \"{selectedId}\" foi editado com sucesso.");
    screenUtils.ShowUISimpleLine();

    Console.WriteLine("Digite ENTER para continuar...");
    Console.ReadLine();
  }

  public void ShowAll()
  {
    screenUtils.ShowMainHeader("Gestão de Chamados");
    screenUtils.ShowOperationHeader("Visualização de Chamados");
    ShowAllTickets();

    Console.Write("\nDigite ENTER para continuar...");
    Console.ReadLine();
  }

  public void ShowAllTickets()
  {
    string line = screenUtils.GetUIDoubleLine();
    Console.WriteLine($"\n{line}");

    Console.WriteLine(
        "{0, -7} | {1, -30} | {2, -15} | {3, -22} | {4, -10}",
        "Id", "Título", "Equipamento", "Data de Abertura", "Dias desde abertura"
    );

    Ticket[] tickets = [.. repository.FindAll()];

    for (int i = 0; i < tickets.Length; i++)
    {
      Ticket? ticket = tickets[i];

      if (ticket == null)
        continue;

      Console.WriteLine(
          "{0, -7} | {1, -30} | {2, -15} | {3, -22} | {4, -10}",
          ticket.id, ticket.title, ticket.equipment!.name, ticket.openDate.ToShortDateString(), ticket.GetElapsedDays()
      );
    }

    Console.WriteLine(line);
  }

  public Ticket GetNewTicket()
  {
    string? selectedId;

    do
    {

      Console.Write("\n>> Digite o id do equipamento: ");
      selectedId = Console.ReadLine();

      if (!string.IsNullOrWhiteSpace(selectedId) && selectedId.Length == 7) break;

    } while (true);

    Equipment _equipment = equipmentRepository.FindById(selectedId);

    if (_equipment == null)
    {
      screenUtils.ShowUISimpleLine();
      Console.WriteLine($"Não foi possível encontrar o equipamento informado.");
      screenUtils.ShowUISimpleLine();

      return null;
    }

    Ticket newTicket = new Ticket
    {
      equipment = _equipment
    };

    do
    {
      Console.Write(">> Digite o título do chamado: ");
      newTicket.title = Console.ReadLine();

      if (isStringValid(newTicket.title)) break;

    } while (true);

    Console.Write(">> Digite a descrição do chamado: ");
    newTicket.description = Console.ReadLine();

    newTicket.openDate = DateTime.Now.AddDays(-3);

    return newTicket;
  }

  bool isStringValid(string? s)
  {
    bool isStringFilled = !string.IsNullOrWhiteSpace(s);
    bool isStringLengthValid = s!.Length >= 3;

    return isStringFilled && isStringLengthValid;
  }
}