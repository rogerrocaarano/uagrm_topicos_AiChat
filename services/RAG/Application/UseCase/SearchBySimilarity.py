import uuid

from Domain.Repository.IFragmentRepository import IFragmentRepository


# -*- coding: utf-8 -*-

class SearchBySimilarity:
    repository: IFragmentRepository

    def __init__(self):
        self.repository = None

    def execute(self, content: str, max_results: int) -> uuid:
        pass
