import uuid


# -*- coding: utf-8 -*-

class FragmentDto:
    id: uuid
    collection: str

    def __init__(self):
        self.id = None
        self.collection = ""
