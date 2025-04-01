from uuid import UUID

from pydantic import BaseModel


# -*- coding: utf-8 -*-

class Fragment(BaseModel):
    id: UUID
    content: str
    collection: str

