using GestaoDeEquipamentos.ConsoleApp.Domain;
using GestaoDeEquipamentos.ConsoleApp.Infra;
namespace GestaoDeEquipamentos.ConsoleApp.Presentation;

class EquipmentScreen
{
  EquipmentRepository repository = new EquipmentRepository();
  public void Register()
  {
    ShowOperationHeader("Cadastro de Equipamento");

    Equipment newEquipment = new Equipment();

    do
    {
      Console.Write(">> Digite o nome do equipamento: ");
      newEquipment.name = Console.ReadLine();

      if (isStringValid(newEquipment.name!)) break;
    } while (true);

    do
    {
      Console.Write(">> Digite o fabricante do equipamento: ");
      newEquipment.manufacturer = Console.ReadLine();

      if (isStringValid(newEquipment.manufacturer!)) break;
    } while (true);

    Console.Write(">> Digite o preço de aquisição do equipamento: ");
    newEquipment.purchasePrice = Convert.ToDecimal(Console.ReadLine());

    Console.Write(">> Digite a data de fabricação do equipamento: ");
    newEquipment.manufactoringDate = Convert.ToDateTime(Console.ReadLine());

    repository.Create(newEquipment);

    Console.WriteLine("-------------------------------------------------------------------");
    Console.WriteLine($"✅ O equipamento \"{newEquipment.id}\" foi cadastrado com sucesso.");
    Console.WriteLine("-------------------------------------------------------------------");
    Console.WriteLine("Digite ENTER para continuar...");
    Console.ReadLine();
  }
  public void Edit()
  {
    ShowOperationHeader("Edição de Equipamento");

    List<Equipment> _equipments = repository.GetAll();

    ShowAll(_equipments);

    string? selectedId, name, manufacturer;

    do
    {
      Console.Write("\nDigite o id do equipamento que deseja editar: ");
      selectedId = Console.ReadLine();

      if (!string.IsNullOrWhiteSpace(selectedId) && selectedId.Length == 7)
        break;
    } while (true);

    do
    {
      Console.Write(">> Digite o nome do equipamento: ");
      name = Console.ReadLine();

      if (isStringValid(name!)) break;
    } while (true);

    do
    {
      Console.Write(">> Digite o fabricante do equipamento: ");
      manufacturer = Console.ReadLine();

      if (isStringValid(manufacturer!)) break;
    } while (true);

    Console.Write(">> Digite o preço de aquisição do equipamento: ");
    decimal purchasePrice = Convert.ToDecimal(Console.ReadLine());

    Console.Write(">> Digite a data de fabricação do equipamento: ");
    DateTime manufactoringDate = Convert.ToDateTime(Console.ReadLine());

    bool success = repository.Update(selectedId, name!, manufacturer!, purchasePrice, manufactoringDate);

    if (!success)
    {
      Console.WriteLine("-------------------------------------------------------------------");
      Console.WriteLine($"Não foi possível encontrar o equipamento informado.");
      Console.WriteLine("-------------------------------------------------------------------");
      Console.WriteLine("-------------------------------------------------------------------");
      Console.WriteLine("Digite ENTER para continuar...");
      Console.ReadLine();
      return;
    }

    Console.WriteLine("-------------------------------------------------------------------");
    Console.WriteLine($"O registro \"{selectedId}\" foi editado com sucesso.");
    Console.WriteLine("-------------------------------------------------------------------");
    Console.WriteLine("Digite ENTER para continuar...");
    Console.ReadLine();


  }

  public string GetMainMenuOption()
  {
    ShowMainHeader();
    Console.WriteLine("1 - Cadastrar equipamento");
    Console.WriteLine("2 - Editar equipamento");
    Console.WriteLine("3 - Excluir equipamento");
    Console.WriteLine("4 - Visualizar equipamentos");
    Console.WriteLine("S - Sair");
    Console.WriteLine("-------------------------------------------------------------------");
    Console.Write("> ");

    return Console.ReadLine()?.ToUpper()!;
  }

  bool isStringValid(string s)
  {
    bool isStringFilled = !string.IsNullOrWhiteSpace(s);
    bool isStringLengthValid = s?.Length > 3;

    return isStringFilled && isStringLengthValid;
  }

  void ShowOperationHeader(string operation)
  {
    ShowMainHeader();

    string linha = GetUILine();
    int largura = linha.Length;

    int espacos = (largura - operation.Length) / 2;
    string textoCentralizado = new string(' ', espacos) + operation;

    Console.WriteLine(textoCentralizado);
    Console.WriteLine("-------------------------------------------------------------------");
  }

  void ShowMainHeader()
  {
    Console.Clear();

    string line = GetUILine();

    Console.WriteLine(line);
    Console.WriteLine("--------------------- Gestão de Equipamentos ----------------------");
    Console.WriteLine(line);
  }

  string GetUILine()
  {
    return "===================================================================";
  }

  void ShowAll(List<Equipment> equipments)
  {
    Console.WriteLine(
    "\n{0, -7} | {1, -15} | {2, -15} | {3, -22} | {4, -10}",
    "Id", "Nome", "Fabricante", "Preço de Aquisição", "Data de Fabricação");

    Equipment[] _equipments = equipments.ToArray();

    for (int i = 0; i < _equipments.Length; i++)
    {
      Equipment? e = equipments[i];

      if (e == null)
        continue;

      Console.WriteLine(
          "{0, -7} | {1, -15} | {2, -15} | {3, -22} | {4, -10}",
          e.id, e.name, e.manufacturer, e.purchasePrice.ToString("C2"), e.manufactoringDate.ToShortDateString()
      );
    }

    Console.WriteLine("-------------------------------------------------------------------");
  }
}