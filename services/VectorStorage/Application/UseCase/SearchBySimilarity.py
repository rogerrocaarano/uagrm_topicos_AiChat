import uuid

from Application.Dto.FragmentDto import FragmentDto
from Domain.Repository.IFragmentRepository import IFragmentRepository


# -*- coding: utf-8 -*-

class SearchBySimilarity:
    __repository: IFragmentRepository

    def __init__(self, repository: IFragmentRepository):
        self.__repository = repository

    def execute(self, content: str, max_results: int) -> list[FragmentDto]:
        similarity_results = self.__repository.get_approximate_matches(
            text=content,
            max_matches=max_results
        )
        fragments = []
        for result in similarity_results:
            fragment = FragmentDto(
                id=uuid.UUID(result.id),
                collection=result.collection,
            )
            fragments.append(fragment)
        return fragments
