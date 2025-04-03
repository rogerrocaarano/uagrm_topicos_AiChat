from uuid import UUID

from pydantic import BaseModel


# -*- coding: utf-8 -*-

class FragmentDto(BaseModel):
    id: UUID
    collection: str
