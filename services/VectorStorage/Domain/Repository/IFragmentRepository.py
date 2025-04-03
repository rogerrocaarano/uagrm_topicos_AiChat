# -*- coding: utf-8 -*-

import abc
import uuid

from Domain.Model.MetaTag import MetaTag
from Domain.Model.SimilarityResult import SimilarityResult


class IFragmentRepository(metaclass=abc.ABCMeta):

    @abc.abstractmethod
    def add(self, content: str, tags: list[MetaTag]) -> uuid:
        raise NotImplementedError

    @abc.abstractmethod
    def get_best_match(self, text: str, tags: list[MetaTag]) -> SimilarityResult | None:
        raise NotImplementedError

    @abc.abstractmethod
    def get_approximate_matches(self,
                                text: str,
                                tags: list[MetaTag] = None,
                                max_matches: int = 30) -> list[SimilarityResult]:
        raise NotImplementedError
