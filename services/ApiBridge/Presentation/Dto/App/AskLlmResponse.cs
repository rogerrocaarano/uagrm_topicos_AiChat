namespace Presentation.Dto.App;

public class AskLlmResponse
{
    public Domain.Model.Message Question { get; set; }
    public Domain.Model.Message Answer { get; set; }

}