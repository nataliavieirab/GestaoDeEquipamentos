using System.Security.Cryptography;
using GestaoDeEquipamentos.ConsoleApp.Domain;
namespace GestaoDeEquipamentos.ConsoleApp.Infra;

class EquipmentRepository
{
  public List<Equipment> equipments = new List<Equipment>();
  public void Create(Equipment newEquipment)
  {
    newEquipment.id = Convert
    .ToHexString(RandomNumberGenerator.GetBytes(20))
    .ToLower()
    .Substring(0, 7);

    equipments.Add(newEquipment);
  }

  public bool Update(string selectedId, string newName,
      string newManufacturer, decimal newPurchasePrice, DateTime newManufactoringDate)
  {
    Equipment equipment = equipments.Find(e => e.id == selectedId)!;

    if (equipment == null) return false;

    equipment.name = newName;
    equipment.manufacturer = newManufacturer;
    equipment.purchasePrice = newPurchasePrice;
    equipment.manufactoringDate = newManufactoringDate;

    return true;
  }

  public List<Equipment> GetAll()
  {
    return equipments;
  }
}