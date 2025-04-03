namespace Application.Dto;

public record MessageDto(
    bool IsUser,
    string Content
);