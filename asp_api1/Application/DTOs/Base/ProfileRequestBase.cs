namespace App.Application.DTOs.Base;

public abstract record ProfileRequestBase
{
    public required string Name { get; init; }
    public required string Career { get; init; }
    public required string StudentId { get; init; }
}
