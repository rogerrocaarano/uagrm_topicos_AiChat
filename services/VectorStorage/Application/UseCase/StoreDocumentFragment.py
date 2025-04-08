from dependency_injector import providers

from Application.Dto.FragmentDto import FragmentDto
from Domain.Model.MetaTag import MetaTag
from Domain.Repository.IFragmentRepository import IFragmentRepository


class StoreDocumentFragment:
    def __init__(self, repository: IFragmentRepository):
        self.__repository: IFragmentRepository = repository

    def execute(self, content: str, document_name: str) -> FragmentDto:
        document_name_tag = MetaTag(
            key="document_name",
            value=document_name,
        )
        tags = [document_name_tag]
        fragment_id = self.__repository.add(content, tags)
        fragment = FragmentDto(
            id=fragment_id,
            collection="test"
        )
        return fragment
