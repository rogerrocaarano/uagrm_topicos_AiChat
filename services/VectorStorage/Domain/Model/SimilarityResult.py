from uuid import UUID

from pydantic import BaseModel

from Domain.Model.MetaTag import MetaTag


# -*- coding: utf-8 -*-

class SimilarityResult(BaseModel):
    fragmentId: UUID
    score: float
    tags: list[MetaTag]

