using GestaoDeEquipamentos.ConsoleApp.Domain;
using GestaoDeEquipamentos.ConsoleApp.Infra;
namespace GestaoDeEquipamentos.ConsoleApp.Presentation;

class EquipmentScreen
{
  EquipmentRepository repository = new EquipmentRepository();
  ScreenUtils screenUtils = new ScreenUtils();
  public string GetMenuOption()
  {
    screenUtils.ShowMainHeader("Gestão de Equipamentos");
    Console.WriteLine("1 - Cadastrar equipamento");
    Console.WriteLine("2 - Editar equipamento");
    Console.WriteLine("3 - Excluir equipamento");
    Console.WriteLine("4 - Visualizar equipamentos");
    Console.WriteLine("S - Sair");
    screenUtils.ShowUISimpleLine();
    Console.Write("> ");

    return Console.ReadLine()?.ToUpper()!;
  }

  public void Register()
  {
    screenUtils.ShowOperationHeader("Cadastro de Equipamento");

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

    screenUtils.ShowUISimpleLine();
    Console.WriteLine($"✅ O equipamento \"{newEquipment.id}\" foi cadastrado com sucesso.");
    screenUtils.ShowUISimpleLine();

    Console.Write("\nDigite ENTER para continuar...");
    Console.ReadLine();
  }

  public void Edit()
  {
    screenUtils.ShowOperationHeader("Edição de Equipamento");

    ShowAllEquipments();

    string? selectedId, name, manufacturer;

    do
    {
      Console.Write("• Digite o id do equipamento que deseja editar: ");
      selectedId = Console.ReadLine();

      if (!string.IsNullOrWhiteSpace(selectedId) && selectedId.Length == 7)
      {
        screenUtils.ShowUISimpleLine();
        ;
        break;
      }
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

    if (success)
    {
      screenUtils.ShowUISimpleLine();
      Console.WriteLine($"✅ O registro \"{selectedId}\" foi editado com sucesso.");
      screenUtils.ShowUISimpleLine();
    }
    else
    {
      screenUtils.ShowUISimpleLine();
      Console.WriteLine($"Não foi possível encontrar o equipamento informado.");
      screenUtils.ShowUISimpleLine();
    }

    Console.Write("\nDigite ENTER para continuar...");
    Console.ReadLine();
  }

  public void Delete()
  {
    screenUtils.ShowOperationHeader("Exclusão de Equipamento");

    ShowAllEquipments();

    string? selectedId;

    do
    {
      Console.Write("• Digite o id do equipamento que deseja excluir: ");
      selectedId = Console.ReadLine();

      if (!string.IsNullOrWhiteSpace(selectedId) && selectedId.Length == 7)
        break;
    } while (true);

    bool success = repository.Delete(selectedId);

    if (success)
    {
      screenUtils.ShowUISimpleLine();
      Console.WriteLine($"✅ O registro \"{selectedId}\" foi excluído com sucesso.");
      screenUtils.ShowUISimpleLine();
    }
    else
    {
      screenUtils.ShowUISimpleLine();
      Console.WriteLine($"Não foi possível encontar o registro \"{selectedId}\".");
      screenUtils.ShowUISimpleLine();
    }

    Console.Write("\nDigite ENTER para continuar...");
    Console.ReadLine();
  }

  public void ShowAll()
  {
    screenUtils.ShowOperationHeader("Visualização de Equipamentos");
    ShowAllEquipments();

    Console.Write("\nDigite ENTER para continuar...");
    Console.ReadLine();
  }

  public void ShowAllEquipments()
  {
    string line = screenUtils.GetUIDoubleLine();

    Console.WriteLine(line);
    Console.WriteLine(
    "{0, -7} | {1, -15} | {2, -15} | {3, -22} | {4, -10}",
    "Id", "Nome", "Fabricante", "Preço de Aquisição", "Data de Fabricação");

    Equipment[] equipments = repository.GetAll().ToArray();

    for (int i = 0; i < equipments.Length; i++)
    {
      Equipment? e = equipments[i];

      if (e == null)
        continue;

      Console.WriteLine(
          "{0, -7} | {1, -15} | {2, -15} | {3, -22} | {4, -10}",
          e.id, e.name, e.manufacturer, e.purchasePrice.ToString("C2"), e.manufactoringDate.ToShortDateString()
      );
    }

    Console.WriteLine(line);
  }

  bool isStringValid(string s)
  {
    bool isStringFilled = !string.IsNullOrWhiteSpace(s);
    bool isStringLengthValid = s?.Length > 3;

    return isStringFilled && isStringLengthValid;
  }

}