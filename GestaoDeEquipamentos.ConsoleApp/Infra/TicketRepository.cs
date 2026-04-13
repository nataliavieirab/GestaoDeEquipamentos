using System.Security.Cryptography;
using GestaoDeEquipamentos.ConsoleApp.Domain;

class TicketRepository
{
  public List<Ticket> tickets = new List<Ticket>();
  public void Create(Ticket newTicket)
  {
    newTicket.id = Convert
    .ToHexString(RandomNumberGenerator.GetBytes(20))
    .ToLower()
    .Substring(0, 7);

    tickets.Add(newTicket);
  }


}