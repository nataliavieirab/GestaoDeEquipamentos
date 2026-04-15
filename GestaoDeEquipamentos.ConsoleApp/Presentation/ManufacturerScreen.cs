using GestaoDeEquipamentos.ConsoleApp.Domain;
using GestaoDeEquipamentos.ConsoleApp.Infra;

namespace GestaoDeEquipamentos.ConsoleApp.Presentation;

public class ManufacturerScreen
{
  public ManufacturerRepository repository;
  public EquipmentRepository equipmentRepository;

  public ManufacturerScreen(ManufacturerRepository _repository, EquipmentRepository _equipmentRepository)
  {
    repository = _repository;
    equipmentRepository = _equipmentRepository;
  }

  public string GetMenuOption()
  {
    ScreenUtils.ShowMainHeader("Gestão de Fornecedores");
    Console.WriteLine("\n1 - Cadastrar Fornecedor");
    Console.WriteLine("2 - Editar Fornecedor");
    Console.WriteLine("3 - Excluir Fornecedor");
    Console.WriteLine("4 - Visualizar Fornecedores");
    Console.WriteLine("S - Sair");
    Console.Write("\n> ");

    return Console.ReadLine()?.ToUpper()!;
  }

  public void Register()
  {
    ScreenUtils.ShowMainHeader("Gestão de Fabricantes");
    ScreenUtils.ShowOperationHeader("CADASTRO DE FABRICANTE");

    Manufacturer newManufacturer = GetManufacturerData();

    repository.Create(newManufacturer);

    Console.WriteLine();
    ScreenUtils.ShowUISimpleLine();
    Console.WriteLine($"✅ O fabricante \"{newManufacturer.id}\" foi cadastrado com sucesso.");
    ScreenUtils.ShowUISimpleLine();

    Console.Write("\nDigite ENTER para continuar...");
    Console.ReadLine();
  }

  public void Edit()
  {
    ScreenUtils.ShowMainHeader("Gestão de Fabricantes");
    ScreenUtils.ShowOperationHeader("EDIÇÃO DE FABRICANTE");

    ShowAllManufacturers();

    string? selectedId;

    do
    {
      Console.Write("• Digite o id do fabricante que deseja editar: ");
      selectedId = Console.ReadLine();

      if (!string.IsNullOrWhiteSpace(selectedId) && selectedId.Length == 7) break;

    } while (true);

    Manufacturer newManufacturer = GetManufacturerData();

    bool success = repository.Update(selectedId, newManufacturer);

    if (success)
    {
      Console.WriteLine();
      ScreenUtils.ShowUISimpleLine();
      Console.WriteLine($"✅ O fabricante \"{selectedId}\" foi editado com sucesso.");
      ScreenUtils.ShowUISimpleLine();
    }
    else
    {
      Console.WriteLine();
      ScreenUtils.ShowUISimpleLine();
      Console.WriteLine($"❌ Não foi possível encontrar o fabricante informado.");
      ScreenUtils.ShowUISimpleLine();
    }

    Console.Write("\nDigite ENTER para continuar...");
    Console.ReadLine();
  }

  public void Delete()
  {
    ScreenUtils.ShowMainHeader("Gestão de Fabricantes");
    ScreenUtils.ShowOperationHeader("EXCLUSÃO DE FABRICANTE");

    ShowAllManufacturers();

    string? selectedId;

    do
    {
      Console.Write("• Digite o id do fabricante que deseja excluir: ");
      selectedId = Console.ReadLine();

      if (!string.IsNullOrWhiteSpace(selectedId) && selectedId.Length == 7)
        break;

    } while (true);

    bool success = repository.Delete(selectedId);

    if (success)
    {
      Console.WriteLine();
      ScreenUtils.ShowUISimpleLine();
      Console.WriteLine($"✅ O fabricante \"{selectedId}\" foi excluído com sucesso.");
      ScreenUtils.ShowUISimpleLine();
    }
    else
    {
      Console.WriteLine();
      ScreenUtils.ShowUISimpleLine();
      Console.WriteLine($"❌ Não foi possível encontar o fabricante \"{selectedId}\".");
      ScreenUtils.ShowUISimpleLine();
    }

    Console.Write("\nDigite ENTER para continuar...");
    Console.ReadLine();
  }

  public void ShowAll()
  {
    ScreenUtils.ShowMainHeader("Gestão de Fabricantes");
    ScreenUtils.ShowOperationHeader("VISUALIZAÇÃO DE FABRICANTES");
    ShowAllManufacturers();

    Console.Write("\nDigite ENTER para continuar...");
    Console.ReadLine();
  }

  public Manufacturer GetManufacturerData()
  {
    Manufacturer newManufacturer = new();

    do
    {
      Console.Write("\n>> Digite o nome do fabricante: ");
      newManufacturer.name = Console.ReadLine();

      if (isStringValid(newManufacturer.name!)) break;

    } while (true);

    do
    {
      Console.Write(">> Digite o e-mail do fabricante: ");
      newManufacturer.email = Console.ReadLine();

      if (isStringValid(newManufacturer.email!)) break;

    } while (true);

    Console.Write(">> Digite o número de telefone do fabricante: ");
    newManufacturer.number = Console.ReadLine();

    return newManufacturer;
  }

  public void ShowAllManufacturers()
  {
    string line = ScreenUtils.GetUIDoubleLine();
    Console.WriteLine($"\n{line}");

    Console.WriteLine(
    "{0, -7} | {1, -15} | {2, -15} | {3, -22} | {4, -10}",
    "ID", "Nome", "Email", "Telefone", "Qtd Equipamentos");

    Manufacturer[] manufacturers = [.. repository!.GetAll()];

    for (int i = 0; i < manufacturers.Length; i++)
    {
      Manufacturer? m = manufacturers[i];

      if (m == null)
        continue;

      repository.GetEquipmentCount(m, equipmentRepository);

      Console.WriteLine(
          "{0, -7} | {1, -15} | {2, -15} | {3, -22} | {4, -10}",
          m.id, m.name, m.email, m.number, m.equipmentsQuantity
      );
    }

    Console.WriteLine(line);
  }

  bool isStringValid(string s)
  {
    bool isStringFilled = !string.IsNullOrWhiteSpace(s);
    bool isStringLengthValid = s?.Length >= 2;

    return isStringFilled && isStringLengthValid;
  }
}