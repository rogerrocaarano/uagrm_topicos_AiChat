import uuid

from pydantic import BaseModel

from Domain.Model.MetaTag import MetaTag


# -*- coding: utf-8 -*-

class SimilarityResult(BaseModel):
    fragmentId: uuid
    score: float
    tags: list[MetaTag]

