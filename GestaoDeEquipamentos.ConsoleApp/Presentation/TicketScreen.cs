using GestaoDeEquipamentos.ConsoleApp.Domain;
using GestaoDeEquipamentos.ConsoleApp.Infra;

namespace GestaoDeEquipamentos.ConsoleApp.Presentation;

public class TicketScreen
{
  public EquipmentScreen equipmentScreen;
  public EquipmentRepository equipmentRepository;
  public TicketRepository repository;

  public TicketScreen(EquipmentScreen _equipmentScreen, EquipmentRepository _equipmentRepository, TicketRepository _repository)
  {
    equipmentScreen = _equipmentScreen;
    equipmentRepository = _equipmentRepository;
    repository = _repository;
  }

  public string GetMenuOption()
  {
    ScreenUtils.ShowMainHeader("Gestão de Chamados");
    Console.WriteLine("\n1 - Cadastrar chamado");
    Console.WriteLine("2 - Editar chamado");
    Console.WriteLine("3 - Excluir chamado");
    Console.WriteLine("4 - Visualizar chamados");
    Console.WriteLine("S - Sair");
    Console.Write("\n> ");

    return Console.ReadLine()?.ToUpper()!;
  }

  public void Register()
  {
    ScreenUtils.ShowMainHeader("Gestão de Chamados");
    ScreenUtils.ShowOperationHeader("CADASTRO DE CHAMADO");

    equipmentScreen.ShowAllEquipments();

    Ticket? newTicket = GetNewTicket();

    if (newTicket == null)
    {
      Console.WriteLine();
      ScreenUtils.ShowUISimpleLine();
      Console.WriteLine($"❌ Não foi possível cadastrar o chamado.\nO equipamento informado não foi encontrado.");
      ScreenUtils.ShowUISimpleLine();

      Console.WriteLine("\nDigite ENTER para continuar...");
      Console.ReadLine();

      return;
    }

    repository.Create(newTicket);

    Console.WriteLine();
    ScreenUtils.ShowUISimpleLine();
    Console.WriteLine($"✅ O chamado \"{newTicket.id}\" foi cadastrado com sucesso.");
    ScreenUtils.ShowUISimpleLine();

    Console.WriteLine("\nDigite ENTER para continuar...");
    Console.ReadLine();
  }

  public void Edit()
  {
    ScreenUtils.ShowMainHeader("Gestão de Chamados");
    ScreenUtils.ShowOperationHeader("EDIÇÃO DE CHAMADO");

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
      Console.WriteLine();
      ScreenUtils.ShowUISimpleLine();
      Console.WriteLine($"❌ Não foi possível editar o chamado.");
      ScreenUtils.ShowUISimpleLine();

      Console.WriteLine("\nDigite ENTER para continuar...");
      Console.ReadLine();

      return;
    }

    bool success = repository.Update(selectedId, newTicket);

    if (success)
    {
      Console.WriteLine();
      ScreenUtils.ShowUISimpleLine();
      Console.WriteLine($"✅ O chamado \"{selectedId}\" foi editado com sucesso.");
      ScreenUtils.ShowUISimpleLine();
    }
    else
    {
      ScreenUtils.ShowUISimpleLine();
      Console.WriteLine($"❌ Não foi possível editar o chamado \"{selectedId}\".");
      ScreenUtils.ShowUISimpleLine();
      return;
    }

    Console.WriteLine("\nDigite ENTER para continuar...");
    Console.ReadLine();
  }

  public void Delete()
  {
    ScreenUtils.ShowMainHeader("Gestão de Chamados");
    ScreenUtils.ShowOperationHeader("EXCLUSÃO DE CHAMADO");

    ShowAllTickets();

    string? selectedId;

    do
    {

      Console.Write("\n>> Digite o id do chamado que deseja excluir: ");
      selectedId = Console.ReadLine();

      if (!string.IsNullOrWhiteSpace(selectedId) && selectedId.Length == 7) break;

    } while (true);

    bool success = repository.Delete(selectedId);

    if (success)
    {
      Console.WriteLine();
      ScreenUtils.ShowUISimpleLine();
      Console.WriteLine($"✅ O chamado \"{selectedId}\" foi excluído com sucesso.");
      ScreenUtils.ShowUISimpleLine();
    }
    else
    {
      Console.WriteLine();
      ScreenUtils.ShowUISimpleLine();
      Console.WriteLine($"❌ Não foi possível excluir o chamado \"{selectedId}\".");
      ScreenUtils.ShowUISimpleLine();
    }

    Console.Write("\nDigite ENTER para continuar...");
    Console.ReadLine();
  }

  public void ShowAll()
  {
    ScreenUtils.ShowMainHeader("Gestão de Chamados");
    ScreenUtils.ShowOperationHeader("VISUALIZAÇÃO DE CHAMADOS");
    ShowAllTickets();

    Console.Write("\nDigite ENTER para continuar...");
    Console.ReadLine();
  }

  public void ShowAllTickets()
  {
    string line = ScreenUtils.GetUIDoubleLine();
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

    if (_equipment == null) return null;

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