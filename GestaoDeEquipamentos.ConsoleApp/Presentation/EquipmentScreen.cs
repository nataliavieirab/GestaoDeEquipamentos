using GestaoDeEquipamentos.ConsoleApp.Domain;
using GestaoDeEquipamentos.ConsoleApp.Infra;
namespace GestaoDeEquipamentos.ConsoleApp.Presentation;

public class EquipmentScreen
{
  public EquipmentRepository repository;
  public ManufacturerScreen manufacturerScreen;
  public ManufacturerRepository manufacturerRepository;

  public EquipmentScreen(EquipmentRepository _repository, ManufacturerScreen _manufacturerScreen, ManufacturerRepository _manufacturerRepository)
  {
    repository = _repository;
    manufacturerScreen = _manufacturerScreen;
    manufacturerRepository = _manufacturerRepository;
  }

  public string GetMenuOption()
  {
    ScreenUtils.ShowMainHeader("Gestão de Equipamentos");
    Console.WriteLine("\n1 - Cadastrar equipamento");
    Console.WriteLine("2 - Editar equipamento");
    Console.WriteLine("3 - Excluir equipamento");
    Console.WriteLine("4 - Visualizar equipamentos");
    Console.WriteLine("S - Sair");
    Console.Write("\n> ");

    return Console.ReadLine()?.ToUpper()!;
  }

  public void Register()
  {
    ScreenUtils.ShowMainHeader("Gestão de Equipamentos");
    ScreenUtils.ShowOperationHeader("CADASTRO DE EQUIPAMENTO");

    Equipment? newEquipment = GetEquipmentData();

    if (newEquipment == null)
    {
      Console.WriteLine();
      ScreenUtils.ShowUISimpleLine();
      Console.WriteLine($"❌ Não foi possível encontrar o fabricante informado.");
      ScreenUtils.ShowUISimpleLine();

      Console.WriteLine("\nDigite ENTER para continuar...");
      Console.ReadLine();
      return;
    }

    repository!.Create(newEquipment);

    Console.WriteLine();
    ScreenUtils.ShowUISimpleLine();
    Console.WriteLine($"✅ O equipamento \"{newEquipment.id}\" foi cadastrado com sucesso.");
    ScreenUtils.ShowUISimpleLine();

    Console.Write("\nDigite ENTER para continuar...");
    Console.ReadLine();
  }

  public void Edit()
  {
    ScreenUtils.ShowMainHeader("Gestão de Equipamentos");
    ScreenUtils.ShowOperationHeader("EDIÇÃO DE EQUIPAMENTO");

    ShowAllEquipments();

    string? selectedId;

    do
    {
      Console.Write("• Digite o id do equipamento que deseja editar: ");
      selectedId = Console.ReadLine();

      if (!string.IsNullOrWhiteSpace(selectedId) && selectedId.Length == 7) break;

    } while (true);

    Equipment? newEquipment = GetEquipmentData();

    if (newEquipment == null)
    {
      Console.WriteLine();
      ScreenUtils.ShowUISimpleLine();
      Console.WriteLine($"❌ Não foi possível encontrar o fabricante informado.");
      ScreenUtils.ShowUISimpleLine();

      Console.WriteLine("\nDigite ENTER para continuar...");
      Console.ReadLine();
      return;
    }

    bool success = repository.Update(selectedId, newEquipment);

    if (success)
    {
      Console.WriteLine();
      ScreenUtils.ShowUISimpleLine();
      Console.WriteLine($"✅ O equipamento \"{selectedId}\" foi editado com sucesso.");
      ScreenUtils.ShowUISimpleLine();
    }
    else
    {
      Console.WriteLine();
      ScreenUtils.ShowUISimpleLine();
      Console.WriteLine($"❌ Não foi possível encontrar o equipamento informado.");
      ScreenUtils.ShowUISimpleLine();
    }

    Console.Write("\nDigite ENTER para continuar...");
    Console.ReadLine();
  }

  public void Delete()
  {
    ScreenUtils.ShowMainHeader("Gestão de Equipamentos");
    ScreenUtils.ShowOperationHeader("EXCLUSÃO DE EQUIPAMENTO");

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
      Console.WriteLine();
      ScreenUtils.ShowUISimpleLine();
      Console.WriteLine($"✅ O registro \"{selectedId}\" foi excluído com sucesso.");
      ScreenUtils.ShowUISimpleLine();
    }
    else
    {
      Console.WriteLine();
      ScreenUtils.ShowUISimpleLine();
      Console.WriteLine($"❌ Não foi possível encontar o registro \"{selectedId}\".");
      ScreenUtils.ShowUISimpleLine();
    }

    Console.Write("\nDigite ENTER para continuar...");
    Console.ReadLine();
  }

  public void ShowAll()
  {
    ScreenUtils.ShowMainHeader("Gestão de Equipamentos");
    ScreenUtils.ShowOperationHeader("VISUALIZAÇÃO DE EQUIPAMENTOS");
    ShowAllEquipments();

    Console.Write("\nDigite ENTER para continuar...");
    Console.ReadLine();
  }

  public void ShowAllEquipments()
  {
    string line = ScreenUtils.GetUIDoubleLine();
    Console.WriteLine($"\n{line}");

    Console.WriteLine(
    "{0, -7} | {1, -15} | {2, -15} | {3, -22} | {4, -10}",
    "ID", "Nome", "Fabricante", "Preço de Aquisição", "Data de Fabricação");

    Equipment[] equipments = [.. repository.GetAll()];

    for (int i = 0; i < equipments.Length; i++)
    {
      Equipment? e = equipments[i];

      if (e == null)
        continue;

      Console.WriteLine(
          "{0, -7} | {1, -15} | {2, -15} | {3, -22} | {4, -10}",
          e.id, e.name, e.manufacturer?.name, e.purchasePrice.ToString("C2"), e.manufacturingDate.ToShortDateString()
      );
    }

    Console.WriteLine(line);
  }

  public Equipment? GetEquipmentData()
  {
    Equipment newEquipment = new Equipment();

    do
    {
      Console.Write("\n>> Digite o nome do equipamento: ");
      newEquipment.name = Console.ReadLine();

      if (isStringValid(newEquipment.name!)) break;

    } while (true);

    manufacturerScreen!.ShowAllManufacturers();

    string? selectedId;

    do
    {
      Console.Write("\n>> Digite o id do fabricante: ");
      selectedId = Console.ReadLine();

      if (!string.IsNullOrWhiteSpace(selectedId) && selectedId.Length == 7) break;

    } while (true);

    if (manufacturerRepository == null) return null;

    Manufacturer? selectedManufacturer = manufacturerRepository.FindById(selectedId);

    if (selectedManufacturer == null) return null;

    newEquipment.manufacturer = selectedManufacturer;

    Console.Write(">> Digite o preço de aquisição do equipamento: ");
    newEquipment.purchasePrice = Convert.ToDecimal(Console.ReadLine());

    Console.Write(">> Digite a data de fabricação do equipamento: ");
    newEquipment.manufacturingDate = Convert.ToDateTime(Console.ReadLine());

    return newEquipment;
  }

  bool isStringValid(string s)
  {
    bool isStringFilled = !string.IsNullOrWhiteSpace(s);
    bool isStringLengthValid = s?.Length > 3;

    return isStringFilled && isStringLengthValid;
  }
}