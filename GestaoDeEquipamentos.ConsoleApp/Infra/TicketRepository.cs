using System.Security.Cryptography;
using GestaoDeEquipamentos.ConsoleApp.Domain;

public class TicketRepository
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

  public bool Update(string selectedId, Ticket newTicket)
  {
    Ticket ticket = tickets.Find(t => t.id == selectedId)!;

    if (ticket == null) return false;

    ticket.title = newTicket.title;
    ticket.description = newTicket.description;
    ticket.equipment = newTicket.equipment;

    return true;
  }

  public bool Delete(string ticketId)
  {
    Ticket ticket = tickets.Find(t => t.id == ticketId)!;

    if (ticket == null)
      return false;

    tickets.Remove(ticket);

    return true;
  }
  public List<Ticket> FindAll()
  {
    return tickets;
  }

}