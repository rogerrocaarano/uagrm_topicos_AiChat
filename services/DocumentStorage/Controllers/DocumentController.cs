using DocumentStorage.Models;
using DocumentStorage.Services;
using Microsoft.AspNetCore.Mvc;

namespace DocumentStorage.Controllers
{
    public class DocumentController : Controller
    {
        private readonly DocumentService _documentService;

        public DocumentController(DocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<IActionResult> ProcesarArchivos()
        {
            List<DocumentJsonModel> documentos = await _documentService.ProcesarArchivosTxtAsync();

            await _documentService.GuardarDocumentosEnBD(documentos);

            return Ok(new { message = "Documentos procesados y guardados con éxito." });
        }

        [HttpGet("GetFragmentById")]
        public async Task<ActionResult<string>> GetFragmentById([FromQuery] Guid fragmentId)
        {
            var fragment = await _documentService.GetFragmentByIdAsync(fragmentId);
            if (fragment == null)
            {
                return NotFound("Fragmento no encontrado.");
            }
            return Ok(fragment);
        }

        [HttpPost("GetFragmentsByIds")]
        public async Task<ActionResult<List<string>>> GetFragmentsByIds([FromBody] List<Guid> fragmentIds)
        {
            var fragments = await _documentService.GetFragmentsByIdsAsync(fragmentIds);
            return Ok(fragments);
        }

        [HttpGet("GetFragmentByEmbeddedId")]
        public async Task<ActionResult<string>> GetFragmentByEmbeddedId([FromQuery] Guid embeddedId)
        {
            var fragment = await _documentService.GetFragmentByEmbeddedIdAsync(embeddedId);
            if (fragment == null)
            {
                return NotFound("Fragmento no encontrado.");
            }
            return Ok(fragment);
        }

        [HttpPost("GetFragmentsByEmbeddedIds")]
        public async Task<ActionResult<List<string>>> GetFragmentsByEmbeddedIds([FromBody] List<Guid> embeddedIds)
        {
            var fragments = await _documentService.GetFragmentsByEmbeddedIdsAsync(embeddedIds);
            return Ok(fragments);
        }

        [HttpGet("GetAllDocuments")]
        public async Task<ActionResult<List<Guid>>> GetAllDocuments()
        {
            var documentIds = await _documentService.GetAllDocumentsAsync();
            return Ok(documentIds);
        }

        [HttpGet("GetDocumentById")]
        public async Task<ActionResult<List<string>>> GetDocumentById([FromQuery] Guid documentId)
        {
            var fragments = await _documentService.GetDocumentByIdAsync(documentId);
            if (fragments == null || fragments.Count == 0)
            {
                return NotFound("Documento no encontrado.");
            }
            return Ok(fragments);
        }

        [HttpGet("GetDocumentFragments")]
        public async Task<ActionResult<List<string>>> GetDocumentFragments([FromQuery] Guid documentId)
        {
            var fragments = await _documentService.GetDocumentFragmentsAsync(documentId);
            if (fragments == null || fragments.Count == 0)
            {
                return NotFound("Fragmentos no encontrados.");
            }
            return Ok(fragments);
        }

        [HttpGet("GetDocumentFragmentIds")]
        public async Task<ActionResult<List<Guid>>> GetDocumentFragmentIds([FromQuery] Guid documentId)
        {
            var fragmentIds = await _documentService.GetDocumentFragmentIdsAsync(documentId);
            if (fragmentIds == null || fragmentIds.Count == 0)
            {
                return NotFound("Fragmentos no encontrados.");
            }
            return Ok(fragmentIds);
        }

            public class UpdateFragmentRequest
            {
                public string VectorId { get; set; }
            }
        }

        [HttpPut("SetVectorId")]
        public async Task<IActionResult> SetVectorId([FromQuery] Guid fragmentId, [FromQuery] Guid vectorId)
        {
            if (fragmentId == Guid.Empty || vectorId == Guid.Empty)
            {
                return BadRequest("Los valores de fragmentId y vectorId no pueden estar vacíos.");
            }

            bool success = await _documentService.SetVectorIdAsync(fragmentId, vectorId);
            if (!success)
            {
                return NotFound("Fragmento no encontrado.");
            }

            return Ok("VectorId asignado correctamente.");
        }

}
