from datetime import datetime

from fastapi import FastAPI, HTTPException

from Application.UseCase.SearchBySimilarity import SearchBySimilarity
from Application.UseCase.StoreDocumentFragment import StoreDocumentFragment
from Infrastructure.di.Container import Container
from Domain.Model.ApiResponse import ApiResponse
from Presentation.Documents.PostFragmentCompare import PostFragmentCompare
from Presentation.Documents.PostFragmentIngest import PostFragmentIngest

di = Container()
app = FastAPI()


@app.get("/")
async def root():
    """
    Root endpoint for the API. Returns a simple message indicating that the API is running.
    :return: ApiResponse: A simple message indicating that the API is running.
    """
    return ApiResponse(content="VectorStorage API is running.")


@app.post("/documents/fragment-ingest")
async def post_documents_fragment_ingest(request: PostFragmentIngest) -> ApiResponse:
    """
    Endpoint to ingest a document fragment into the system.
    :param request: PostFragmentIngest: The request object containing the fragment and relevant tags.
    :return: ApiResponse: The response object containing the result of the ingestion.
    """
    try:
        content = StoreDocumentFragment(di.fragments_repository).execute(
            content=request.fragment,
            document_name=request.documentName
        )
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))

    return ApiResponse(content=content)


@app.post("/documents/fragment-compare")
async def post_documents_fragment_compare(request: PostFragmentCompare) -> ApiResponse:
    """
    Endpoint to compare a document fragment with existing fragments in the system.
    :param request: PostFragmentCompare: The request object containing the fragment and maximum matches to find.
    :return: ApiResponse: The response object containing the best match or matches ids found.
    """
    try:
        content = SearchBySimilarity(di.fragments_repository).execute(
            content=request.fragment,
            max_results=request.max_matches
        )
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))

    if not content:
        raise HTTPException(status_code=404, detail="No matches found")

    return ApiResponse(content=content)
