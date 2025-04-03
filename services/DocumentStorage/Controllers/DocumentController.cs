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

        [HttpGet]
        [Route("GetAllDocuments")]
        public async Task<IActionResult> GetAllDocuments()
        {
            var documents = await _documentService.GetAllDocumentsAsync();
            return Ok(documents);
        }

        [HttpGet]
        [Route("GetDocumentById")]
        public async Task<IActionResult> GetDocumentById(string documentId)
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
        public async Task<IActionResult> GetFragmentById(string fragmentId)
        {
            var fragment = await _documentService.GetFragmentByIdAsync(fragmentId);
            if (fragment == null)
            {
                return NotFound("Fragmento no encontrado.");
            }
            return Ok(fragment);
        }

        [HttpPost]
        [Route("UpdateFragmentVectorId")]
        public async Task<IActionResult> UpdateFragmentVectorId(string fragmentId, string vectorId)
        {
            if (vectorId == null)
            {
                return BadRequest("Se debe proporcionar un nuevo VectorId para actualizar.");
            }

            var result = await _documentService.UpdateFragmentVectorIdAsync(fragmentId, vectorId);

            if (!result)
            {
                return NotFound("Fragmento no encontrado.");
            }

            return Ok("VectorId actualizado correctamente.");
        }

        public class UpdateFragmentRequest
        {
            public string VectorId { get; set; }
        }
    }

}
