namespace Presentation.Constant;

public static class Endpoint
{
    public const string AskLlm = "/app/ask-llm";
    public const string HeartbeatVectorStorage = "/system/telemetry/heartbeat/vector-storage";
    public const string HeartbeatDocumentStorage = "/system/telemetry/heartbeat/document-storage";
    public const string BuildVectorStorage = "/system/db/vector-storage/build";
}