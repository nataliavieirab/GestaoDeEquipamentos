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
    screenUtils.ShowMainHeader("Gestão de Cadastros");
    screenUtils.ShowOperationHeader("Cadastro de Chamado");

    equipmentScreen.ShowAllEquipments();

    Ticket? newTicket = GetNewTicket();

    if (newTicket == null)
    {
      screenUtils.ShowUISimpleLine();
      Console.WriteLine($"Não foi possível encontrar o equipamento informado.");
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

  public Ticket GetNewTicket()
  {
    string? selectedId;

    do
    {
      Console.Write("Digite o id do equipamento que deseja selecionar: ");
      selectedId = Console.ReadLine();

      if (!string.IsNullOrWhiteSpace(selectedId) && selectedId.Length == 7)
      {
        screenUtils.ShowUISimpleLine();
        break;
      }

    } while (true);

    Equipment _equipment = equipmentRepository.FindById(selectedId);

    if (_equipment == null) return null;

    Ticket newTicket = new Ticket
    {
      equipment = _equipment
    };

    do
    {
      Console.Write("Digite o título do chamado: ");
      newTicket.title = Console.ReadLine();

      if (isStringValid(newTicket.title)) break;

    } while (true);

    Console.Write("Digite a descrição do chamado: ");
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