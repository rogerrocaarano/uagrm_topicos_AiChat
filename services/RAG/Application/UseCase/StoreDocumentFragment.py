from Application.Dto.FragmentDto import FragmentDto
from Domain.Model.MetaTag import MetaTag
from Domain.Repository.IFragmentRepository import IFragmentRepository


class StoreDocumentFragment:
    __repository: IFragmentRepository

    def __init__(self, repository: IFragmentRepository):
        self.__repository = repository

    def execute(self, content: str, document_name: str) -> FragmentDto:
        document_name_tag = MetaTag()
        document_name_tag.name = "document_name"
        document_name_tag.value = document_name

        tags = [document_name_tag]
        fragment_id = self.__repository.add(content, tags)
        fragment = FragmentDto()
        fragment.id = fragment_id
        fragment.collection = "stored_documents"
        return fragment
