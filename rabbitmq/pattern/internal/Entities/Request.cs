namespace Entities;
public enum Status
{
    New,
    Processing,
    Completed,
    Failed
}

public sealed class Request
{
    public Guid Id { get; set; }
    public Status Status { get; set; } 
}