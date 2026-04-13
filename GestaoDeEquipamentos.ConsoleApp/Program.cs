using GestaoDeEquipamentos.ConsoleApp.Domain;
using GestaoDeEquipamentos.ConsoleApp.Infra;
using GestaoDeEquipamentos.ConsoleApp.Presentation;

ScreenUtils screenUtils = new();

EquipmentRepository equipmentRepository = new();
EquipmentScreen equipmentScreen = new()
{
  repository = equipmentRepository,
  screenUtils = screenUtils
};

TicketScreen ticketScreen = new()
{
  screenUtils = screenUtils,
  equipmentScreen = equipmentScreen,
  equipmentRepository = equipmentRepository,
  repository = new TicketRepository(),
};

//Dados Teste
Equipment equipment = new()
{
  name = "Notebook",
  manufacturer = "Acer",
  purchasePrice = 2000,
  manufactoringDate = DateTime.Now.AddYears(-5)
};

Equipment equipment2 = new()
{
  name = "Monitor",
  manufacturer = "LG",
  purchasePrice = 1200,
  manufactoringDate = DateTime.Now.AddYears(-4)
};

equipmentRepository.Create(equipment);
equipmentRepository.Create(equipment2);

while (true)
{

  string mainMenuOption = screenUtils.GetMainMenuOption();

  if (mainMenuOption == "S")
  {
    Console.Clear();
    break;
  }

  if (mainMenuOption == "1")
  {
    string menuOption = equipmentScreen.GetMenuOption();

    if (menuOption == "S")
    {
      Console.Clear();
      break;
    }

    if (menuOption == "1") equipmentScreen.Register();

    else if (menuOption == "2") equipmentScreen.Edit();

    else if (menuOption == "3") equipmentScreen.Delete();

    else if (menuOption == "4") equipmentScreen.ShowAll();
  }

  else if (mainMenuOption == "2")
  {
    string menuOption = ticketScreen.GetMenuOption();

    if (menuOption == "S")
    {
      Console.Clear();
      break;
    }

    if (menuOption == "1") ticketScreen.Register();

    // else if (menuOption == "2") ticketScreen.Edit();

    // else if (menuOption == "3") ticketScreen.Delete();

    // else if (menuOption == "4") ticketScreen.ShowAll();

  }
}

