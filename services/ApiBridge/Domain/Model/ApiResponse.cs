namespace Domain.Model;

public class ApiResponse(object content)
{
    public object Content { get; private set; } = content;
    public DateTime DateTime { get; private set; } = DateTime.UtcNow;
}