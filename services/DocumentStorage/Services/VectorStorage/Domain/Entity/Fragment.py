from pydantic import BaseModel


# -*- coding: utf-8 -*-

class Fragment(BaseModel):
    id: str
    content: str
    collection: str

