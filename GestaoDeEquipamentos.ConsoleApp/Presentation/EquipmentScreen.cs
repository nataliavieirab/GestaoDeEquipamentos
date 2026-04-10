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

    Console.WriteLine(linha);
    Console.WriteLine("--------------------- Gestão de Equipamentos ----------------------");
    Console.WriteLine(linha);

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
}