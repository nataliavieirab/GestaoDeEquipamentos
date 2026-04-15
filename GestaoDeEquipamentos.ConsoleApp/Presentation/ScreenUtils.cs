namespace GestaoDeEquipamentos.ConsoleApp.Presentation;

static class ScreenUtils
{
  public static string GetMainMenuOption()
  {
    ShowMainHeader("Gestão de Equipamentos");
    Console.WriteLine("\n1 - Gerenciar Equipamentos");
    Console.WriteLine("2 - Gerenciar Chamados");
    Console.WriteLine("3 - Gerenciar Fornecedores");
    Console.WriteLine("S - Sair");
    Console.Write("\n> ");
    return Console.ReadLine()?.ToUpper()!;
  }

  public static void ShowMainHeader(string title)
  {
    string line = GetUIDoubleLine();

    Console.Clear();
    Console.WriteLine(line);
    Console.WriteLine($"--------------------------------------- {title} ------------------------------------------");
    Console.WriteLine(line);
  }

  public static void ShowOperationHeader(string operation)
  {
    string textoCentralizado = new string(' ', 41) + operation;

    Console.WriteLine($"\n{textoCentralizado}");
  }

  public static string GetUIDoubleLine()
  {
    return "=========================================================================================================";
  }

  public static void ShowUISimpleLine()
  {
    Console.WriteLine("---------------------------------------------------------------------------------------------------------");
  }
}