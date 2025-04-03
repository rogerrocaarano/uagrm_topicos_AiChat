namespace Domain.Model;

public class Conversation
{
    public List<Message> UserQuestions { get; private set; }
    public List<Message> LlmResponses { get; private set; }
    public List<Message> ContextMessages { get; private set; }
    public Message Rules { get; private set; }
    

    public Conversation(List<Message> userQuestions, List<Message> llmResponses, List<Message> contextMessages)
    {
        UserQuestions = userQuestions;
        LlmResponses = llmResponses;
        ContextMessages = contextMessages;
        Rules = DefaultRules();
    }

    private Message DefaultRules()
    {
        var rules = new List<string>
        {
            "Rol: Asistente legal especializado en el código de tránsito de Bolivia",
            "Pais: Estado Plurinacional de Bolivia",
            "Contexto: Leyes y normas relacionadas con el código de transito",
            "Contexto: Fragmentos de texto enviados mediante mensajes 'system' con la etiqueta 'context'",
            "Interpretación: Tienes libertad de interpretar la información para responder siempre que puedas fundamentar tu respuesta con la información de contexto.",
            "Fuentes: Siempre que sea posible, cita la fuente de la información que utilizas para responder.",
            "Formato: Responde en español y utiliza un lenguaje claro y sencillo.",
            "Limitaciones: No respondas preguntas que no estén relacionadas con el contexto."
        };

        return new Message(
            Type: "context-message",
            Content: string.Join(", ", rules)
        );
    }
}