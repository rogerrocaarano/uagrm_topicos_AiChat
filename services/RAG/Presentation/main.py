from datetime import datetime

from fastapi import FastAPI

from Application.UseCase.SearchBySimilarity import SearchBySimilarity
from Application.UseCase.StoreDocumentFragment import StoreDocumentFragment
from Presentation.di.Container import Container
from Domain.Model.ApiResponse import ApiResponse
from Presentation.Documents.PostFragmentCompare import PostFragmentCompare
from Presentation.Documents.PostFragmentIngest import PostFragmentIngest

di = Container()
app = FastAPI()


@app.get("/")
async def root():
    return {"message": "Heartbeat OK"}


@app.post("/documents/fragment-ingest")
async def post_documents_fragment_ingest(request: PostFragmentIngest) -> ApiResponse:
    fragment_dto = StoreDocumentFragment(di.fragments_repository).execute(
        request.fragment,
        request.documentName)
    response = ApiResponse()
    response.content = fragment_dto
    response.dateTime = datetime.now()
    response.isException = False
    return response


@app.post("/documents/fragment-compare")
async def post_documents_fragment_compare(request: PostFragmentCompare) -> ApiResponse:
    fragment_dto = SearchBySimilarity(di.fragments_repository).execute(
        request.fragment,
        request.max_matches)
    response = ApiResponse()
    response.content = fragment_dto
    response.dateTime = datetime.now()
    response.isException = False
    return response
