namespace Presentation.Dto.App;

public class AskLlmRequest
{
    public List<Domain.Model.Message>? Messages { get; set; }
    public required Domain.Model.Message Question { get; set; }
}