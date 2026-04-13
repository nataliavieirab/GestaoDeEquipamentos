namespace GestaoDeEquipamentos.ConsoleApp.Domain;

public class Ticket
{
  public string? id;
  public string? title;
  public string? description;
  public DateTime openDate;
  public Equipment? equipment;

  public int GetElapsedDays()
  {
    TimeSpan timeDifference = DateTime.Now.Subtract(openDate);

    return timeDifference.Days;
  }
}