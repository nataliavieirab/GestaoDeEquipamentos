using System.Security.Cryptography;
using GestaoDeEquipamentos.ConsoleApp.Domain;

namespace GestaoDeEquipamentos.ConsoleApp.Infra;

public class ManufacturerRepository
{
  public List<Manufacturer> manufacturers = new List<Manufacturer>();

  public void Create(Manufacturer newManufacturer)
  {
    newManufacturer.id = Convert
    .ToHexString(RandomNumberGenerator.GetBytes(20))
    .ToLower()
    .Substring(0, 7);

    manufacturers.Add(newManufacturer);
  }

  public bool Update(string manufacturerId, Manufacturer newManufacturer)
  {
    Manufacturer? manufacturer = manufacturers.Find(m => m.id == manufacturerId);

    if (manufacturer == null) return false;

    manufacturer.name = newManufacturer.name;
    manufacturer.email = newManufacturer.email;
    manufacturer.number = newManufacturer.number;

    return true;
  }

  public bool Delete(string manufacturerId)
  {
    Manufacturer? manufacturer = FindById(manufacturerId);

    if (manufacturer == null) return false;

    manufacturers.Remove(manufacturer);

    return true;
  }

  public List<Manufacturer> GetAll()
  {
    return manufacturers;
  }

  public Manufacturer? FindById(string manufacturerId)
  {
    return manufacturers.Find(m => m.id == manufacturerId)!;
  }

  public void GetEquipmentCount(Manufacturer manufacturer, EquipmentRepository equipmentRepository)
  {
    List<Equipment> equipments = equipmentRepository.GetAll();

    manufacturer.equipmentsQuantity =
        equipments.Count(e => e.manufacturer?.id == manufacturer.id);
  }
}

