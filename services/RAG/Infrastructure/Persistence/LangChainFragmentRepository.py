# -*- coding: utf-8 -*-
import uuid

from torch.nn.functional import embedding

from Domain.Model.MetaTag import MetaTag
from Domain.Model.SimilarityResult import SimilarityResult
from Domain.Repository.IFragmentRepository import IFragmentRepository
from langchain_community.vectorstores import Chroma
from langchain.embeddings.base import Embeddings


class LangChainFragmentRepository(IFragmentRepository):

    def __init__(self, collection_name: str, embedding_function: Embeddings):
        self.__collection_name = collection_name
        self.__embedding_function = embedding_function
        self.__vector_store: Chroma = Chroma(
            collection_name=collection_name,
            embedding_function=embedding_function,
            persist_directory=f"./{collection_name}",
        )

    def add(self, content: str, tags: list[MetaTag]) -> uuid:
        pass

    def get_best_match(self, text: str, tags: list[MetaTag]) -> SimilarityResult | None:
        pass

    def get_approximate_matches(self, text: str, tags: list[MetaTag], max_matches: int) -> list[SimilarityResult]:
        pass

    @staticmethod
    def __tags_to_dict(tags: list[MetaTag]) -> dict | None:
        if not tags:
            return None
        return {tag.key: tag.value for tag in tags}
