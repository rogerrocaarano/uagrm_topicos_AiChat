using Domain.Constants;

namespace Domain.Model;

public class Conversation
{
    public List<(Message, Message)> AnsweredQuestions { get; private set; }
    public Message? Question { get; set; }
    public Message Rules { get; private set; }

    public Conversation(List<(Message, Message)>? answeredQuestions, Message userQuestion)
    {
        AnsweredQuestions = answeredQuestions ?? new List<(Message, Message)>();
        Question = userQuestion;
        Rules = BuildRules();
    }
    
    public void AnswerQuestion(Message answer)
    {
        AnsweredQuestions.Add((Question, answer));
        Question = null;
    }

    private Message BuildRules()
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

        return new Message
        {
            Type = MessageType.Rule,
            Text = string.Join(", ", rules)
        };
    }
}