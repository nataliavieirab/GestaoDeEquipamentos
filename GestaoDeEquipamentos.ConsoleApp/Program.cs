using GestaoDeEquipamentos.ConsoleApp.Domain;
using GestaoDeEquipamentos.ConsoleApp.Infra;
using GestaoDeEquipamentos.ConsoleApp.Presentation;

ManufacturerRepository manufacturerRepository = new();
EquipmentRepository equipmentRepository = new();
TicketRepository ticketRepository = new();

ManufacturerScreen manufacturerScreen = new(manufacturerRepository, equipmentRepository);
EquipmentScreen equipmentScreen = new(equipmentRepository, manufacturerScreen, manufacturerRepository);
TicketScreen ticketScreen = new(equipmentScreen, equipmentRepository, ticketRepository);

//Dados Teste
Manufacturer manufacturer = new()
{
  name = "Dell",
  email = "dell@dell.com",
  number = "08009966"
};

Manufacturer manufacturer2 = new()
{
  name = "Acer",
  email = "acer@acer.com",
  number = "08008876"
};

Equipment equipment = new()
{
  name = "Notebook",
  manufacturer = manufacturer2,
  purchasePrice = 2000,
  manufacturingDate = DateTime.Now.AddYears(-5)
};

Equipment equipment2 = new()
{
  name = "Monitor",
  manufacturer = manufacturer,
  purchasePrice = 1200,
  manufacturingDate = DateTime.Now.AddYears(-4)
};

Ticket ticket = new()
{
  title = "Quebrou o display",
  description = "Está com deadpixel",
  openDate = DateTime.Now.AddDays(-7),
  equipment = equipment,
};

manufacturerRepository.Create(manufacturer);
manufacturerRepository.Create(manufacturer2);

equipmentRepository.Create(equipment);
equipmentRepository.Create(equipment2);

ticketRepository.Create(ticket);

while (true)
{

  string mainMenuOption = ScreenUtils.GetMainMenuOption();

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

    else if (menuOption == "2") ticketScreen.Edit();

    else if (menuOption == "3") ticketScreen.Delete();

    else if (menuOption == "4") ticketScreen.ShowAll();
  }

  else if (mainMenuOption == "3")
  {

    string menuOption = manufacturerScreen.GetMenuOption();

    if (menuOption == "S")
    {
      Console.Clear();
      break;
    }

    if (menuOption == "1") manufacturerScreen.Register();

    else if (menuOption == "2") manufacturerScreen.Edit();

    else if (menuOption == "3") manufacturerScreen.Delete();

    else if (menuOption == "4") manufacturerScreen.ShowAll();
  }
}