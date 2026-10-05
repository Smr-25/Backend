namespace MiniAppApi.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entityName, int id)
        : base($"{entityName} with id {id} not found") { }

    public EntityNotFoundException(string message) : base(message) { }
}

