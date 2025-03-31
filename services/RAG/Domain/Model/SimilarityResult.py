import uuid

from Domain.Model.MetaTag import MetaTag


# -*- coding: utf-8 -*-

class SimilarityResult:
    fragmentId: uuid
    score: int
    tags: list[MetaTag]

    def __init__(self):
        self.fragmentId = None
        self.score = 0
        self.tags = []
