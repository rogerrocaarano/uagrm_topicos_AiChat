import uuid

from Application.Dto.FragmentDto import FragmentDto
from Domain.Repository.IFragmentRepository import IFragmentRepository


# -*- coding: utf-8 -*-

class SearchBySimilarity:
    __repository: IFragmentRepository

    def __init__(self, repository: IFragmentRepository):
        self.__repository = repository

    def execute(self, content: str, max_results: int) -> list[FragmentDto]:
        pass
