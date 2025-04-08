using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Newtonsoft.Json;
using DocumentStorage.Data;
using DocumentStorage.Models;

namespace DocumentStorage.Services
{
    public class DocumentService
    {
        private readonly AppDbContext _context;

        public DocumentService(AppDbContext context)
        {
            _context = context;
        }


        private readonly string _pythonPath = Environment.GetEnvironmentVariable("PYTHON_PATH") ?? 
                                              throw new InvalidOperationException("PYTHON_PATH environment variable is not set.");

        private readonly string _scriptPath =
            Path.Combine(Directory.GetCurrentDirectory(), "utilities", "process_text.py");


        public async Task<List<DocumentJsonModel>> ProcesarArchivosTxtAsync()
        {
            
            try
            {
                Console.WriteLine(_pythonPath);
                var psi = new ProcessStartInfo
                {
                    FileName = _pythonPath,
                    Arguments = $"\"{_scriptPath}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = new Process { StartInfo = psi })
                {
                    process.Start();

                    string output = await process.StandardOutput.ReadToEndAsync();
                    string error = await process.StandardError.ReadToEndAsync();

                    await process.WaitForExitAsync();

                    if (!string.IsNullOrEmpty(error))
                    {
                        throw new Exception($"Error al ejecutar Python: {error}");
                    }

                    string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "output.json");
                    if (!File.Exists(jsonPath))
                    {
                        throw new FileNotFoundException("No se encontró el archivo JSON generado.");
                    }

                    string jsonData = await File.ReadAllTextAsync(jsonPath);

                    var documentosProcesados = JsonConvert.DeserializeObject<List<DocumentJsonModel>>(jsonData);

                    if (documentosProcesados == null || documentosProcesados.Count == 0)
                    {
                        throw new Exception("La deserialización devolvió null o una lista vacía.");
                    }

                    return documentosProcesados;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ProcesarArchivosTxtAsync: {ex.Message}");
            }
        }

        public async Task GuardarDocumentosEnBD(List<DocumentJsonModel> documentos)
        {
            foreach (var doc in documentos)
            {
                DateTime uploadDateTime;
                if (!DateTime.TryParse(doc.Document.UploadDateTime, null,
                        System.Globalization.DateTimeStyles.RoundtripKind, out uploadDateTime))
                {
                    throw new Exception($"La fecha de carga no es válida: {doc.Document.UploadDateTime}");
                }

                if (uploadDateTime.Kind == DateTimeKind.Unspecified)
                {
                    uploadDateTime = DateTime.SpecifyKind(uploadDateTime, DateTimeKind.Utc);
                }

                var documento = new Document
                {
                    Id = Guid.Parse(doc.Document.Id),
                    Name = doc.Document.Name,
                    Description = doc.Document.Description,
                    UploadDateTime = uploadDateTime
                };

                _context.Documents.Add(documento);
                await _context.SaveChangesAsync();

                foreach (var frag in doc.Fragments)
                {
                    var fragmento = new Fragment
                    {
                        Id = Guid.Parse(frag.Id),
                        VectorId = null,
                        Content = frag.Content,
                        DocumentId = documento.Id,
                        SequenceId = frag.SequenceId
                    };
                    _context.Fragments.Add(fragmento);
                }
            }

            await _context.SaveChangesAsync();
        }

          public async Task<List<DocumentModel>> GetAllDocumentsAsync()
        {
            return await _context.Documents
                .Select(doc => new DocumentModel
                {
                    Document = new DocumentData
                    {
                        Id = doc.Id.ToString(),
                        Name = doc.Name,
                        Description = doc.Description,
                        UploadDateTime = doc.UploadDateTime.ToString()
                    }
                }).ToListAsync();
        }

        public async Task<DocumentJsonModel> GetDocumentByIdAsync(string documentId)
        {
            return await _context.Documents
                .Where(doc => doc.Id.ToString() == documentId)
                .Select(doc => new DocumentJsonModel
                {
                    Document = new DocumentData
                    {
                        Id = doc.Id.ToString(),
                        Name = doc.Name,
                        Description = doc.Description,
                        UploadDateTime = doc.UploadDateTime.ToString()
                    },
                    Fragments = doc.Fragments.Select(frag => new FragmentJsonModel
                    {
                        Id = frag.Id.ToString(),
                        VectorId = frag.VectorId.ToString(),
                        Content = frag.Content,
                        DocumentId = frag.DocumentId.ToString(),
                        SequenceId = frag.SequenceId
                    }).ToList()
                }).FirstOrDefaultAsync();
        }

        public async Task<FragmentJsonModel> GetFragmentByIdAsync(string fragmentId)
        {
            return await _context.Fragments
                .Where(frag => frag.Id.ToString() == fragmentId)
                .Select(frag => new FragmentJsonModel
                {
                    Id = frag.Id.ToString(),
                    VectorId = frag.VectorId.ToString(),
                    Content = frag.Content,
                    DocumentId = frag.DocumentId.ToString(),
                    SequenceId = frag.SequenceId
                }).FirstOrDefaultAsync();
        }

        public async Task<string> GetFragmentByEmbeddedIdAsync(Guid embeddedId)
        {
            return await _context.Fragments
                .Where(f => f.VectorId == embeddedId)
                .Select(f => f.Content)
                .FirstOrDefaultAsync();
        }

        public async Task<List<string>> GetFragmentsByEmbeddedIdsAsync(List<Guid> embeddedIds)
        {
            return await _context.Fragments
                .Where(f => embeddedIds.Contains((Guid)f.VectorId))
                .Select(f => f.Content)
                .ToListAsync();
        }

        public async Task<bool> SetVectorIdAsync(Guid fragmentId, Guid vectorId)
        {
            var fragment = await _context.Fragments.FindAsync(fragmentId);
            if (fragment == null)
            {
                return false;
            }

            fragment.VectorId = vectorId;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}