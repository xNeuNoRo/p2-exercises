namespace App.Application.DTOs.Profile;

public record ProfileResponseDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Career { get; init; }
    public required string StudentId { get; init; }
}
