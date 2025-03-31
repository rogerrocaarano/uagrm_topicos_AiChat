import uuid

from Domain.Repository.IFragmentRepository import IFragmentRepository


# -*- coding: utf-8 -*-

class StoreDocumentFragment:
    repository: IFragmentRepository

    def __init__(self):
        self.repository = None

    def execute(self, content: str, document_name: str) -> uuid:
        pass
