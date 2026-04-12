using GestaoDeEquipamentos.ConsoleApp.Presentation;

EquipmentScreen equipmentScreen = new EquipmentScreen();

while (true)
{

  string menuOption = equipmentScreen.GetMainMenuOption();

  if (menuOption == "S")
  {
    Console.Clear();
    break;
  }

  if (menuOption == "1") equipmentScreen.Register();

  else if (menuOption == "2") equipmentScreen.Edit();

  else if (menuOption == "3") ;

  else if (menuOption == "4") ;
}