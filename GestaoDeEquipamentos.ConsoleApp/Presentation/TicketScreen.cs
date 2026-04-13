namespace GestaoDeEquipamentos.ConsoleApp.Presentation;

class TicketScreen
{
  ScreenUtils screenUtils = new ScreenUtils();
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

}