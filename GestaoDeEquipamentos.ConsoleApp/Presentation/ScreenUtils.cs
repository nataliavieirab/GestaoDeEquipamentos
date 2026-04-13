namespace GestaoDeEquipamentos.ConsoleApp.Presentation;

class ScreenUtils
{
  public string GetMainMenuOption()
  {
    ShowMainHeader("Gestão de Equipamentos");
    Console.WriteLine("1 - Gerenciar equipamentos");
    Console.WriteLine("2 - Gerenciar chamados");
    Console.WriteLine("S - Sair");
    ShowUISimpleLine();
    Console.Write("> ");
    return Console.ReadLine()?.ToUpper()!;
  }

  public void ShowMainHeader(string title)
  {
    string line = GetUIDoubleLine();

    Console.Clear();
    Console.WriteLine(line);
    Console.WriteLine($"--------------------------------- {title} -----------------------------------");
    Console.WriteLine(line);
  }

  public void ShowOperationHeader(string operation)
  {
    ShowMainHeader("Gestão de Equipamentos");

    string linha = GetUIDoubleLine();
    int largura = linha.Length;

    int espacos = (largura - operation.Length) / 2;
    string textoCentralizado = new string(' ', espacos) + operation;

    Console.WriteLine(textoCentralizado);
    ShowUISimpleLine();
  }

  public string GetUIDoubleLine()
  {
    return "============================================================================================";
  }

  public void ShowUISimpleLine()
  {
    Console.WriteLine("--------------------------------------------------------------------------------------------");
  }
}