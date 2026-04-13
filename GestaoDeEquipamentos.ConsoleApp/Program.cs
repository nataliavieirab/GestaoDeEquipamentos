using GestaoDeEquipamentos.ConsoleApp.Presentation;

ScreenUtils screenUtils = new ScreenUtils();
EquipmentScreen equipmentScreen = new EquipmentScreen();
TicketScreen ticketScreen = new TicketScreen();

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

    // if (menuOption == "1") ticketScreen.Register();

    // else if (menuOption == "2") ticketScreen.Edit();

    // else if (menuOption == "3") ticketScreen.Delete();

    // else if (menuOption == "4") ticketScreen.ShowAll();

  }
}

