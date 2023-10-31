namespace CinemaTicketBooking.Common.Exceptions;
public class NotFoundException : Exception
{
    public NotFoundException(int id, string entityName) : this($"{entityName} with ID {id} is not found") { }
    public NotFoundException(string message) : base(message) { }
}