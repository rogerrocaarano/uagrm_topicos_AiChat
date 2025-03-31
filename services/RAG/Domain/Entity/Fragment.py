import uuid


# -*- coding: utf-8 -*-

class Fragment:
    id: uuid
    content: str
    collection: str

    def __init__(self):
        self.id = None
        self.content = ""
        self.collection = ""
