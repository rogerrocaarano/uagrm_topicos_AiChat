using DocumentStorage.Services;
using Microsoft.AspNetCore.Mvc;
using DocumentStorage.Models;

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

        [HttpGet]
        [Route("GetAllDocuments")]
        public async Task<IActionResult> GetAllDocumentsAsync()
        {
            var documents = await _documentService.GetAllDocumentsAsync();
            return Ok(documents);
        }

        [HttpGet]
        [Route("GetDocumentById")]
        public async Task<IActionResult> GetDocumentByIdAsync(string documentId)
        {
            var document = await _documentService.GetDocumentByIdAsync(documentId);
            if (document == null)
            {
                return NotFound("Documento no encontrado.");
            }
            return Ok(document);
        }

        [HttpGet]
        [Route("GetFragmentById")]
        public async Task<IActionResult> GetFragmentByIdAsync(string fragmentId)
        {
            var fragment = await _documentService.GetFragmentByIdAsync(fragmentId);
            if (fragment == null)
            {
                return NotFound("Fragmento no encontrado.");
            }
            return Ok(fragment);
        }

        [HttpGet("GetFragmentByEmbeddedId")]
        public async Task<IActionResult> GetFragmentByEmbeddedId(Guid embeddedId)
        {
            var content = await _documentService.GetFragmentByEmbeddedIdAsync(embeddedId);
            if (content == null) return NotFound("Fragmento no encontrado.");
            return Ok(content);
        }

        [HttpGet("GetFragmentsByEmbeddedIds")]
        public async Task<IActionResult> GetFragmentsByEmbeddedIds(List<Guid> embeddedIds)
        {
            var fragments = await _documentService.GetFragmentsByEmbeddedIdsAsync(embeddedIds);
            return Ok(fragments);
        }

        [HttpPost]
        [Route("UpdateFragmentVectorId")]
        public async Task<IActionResult> UpdateFragmentVectorIdAsync(Guid fragmentId, Guid vectorId)
        {
            if (vectorId == null)
            {
                return BadRequest("Se debe proporcionar un nuevo VectorId para actualizar.");
            }

            var result = await _documentService.SetVectorIdAsync(fragmentId, vectorId);

            if (!result)
            {
                return NotFound("Fragmento no encontrado.");
            }

            return Ok("VectorId actualizado correctamente.");
        }

    }

}