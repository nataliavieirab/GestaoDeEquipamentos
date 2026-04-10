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
}