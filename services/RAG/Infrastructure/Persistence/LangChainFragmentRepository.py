# -*- coding: utf-8 -*-
import uuid

from torch.nn.functional import embedding

from Domain.Model.MetaTag import MetaTag
from Domain.Model.SimilarityResult import SimilarityResult
from Domain.Repository.IFragmentRepository import IFragmentRepository
from langchain.vectorstores import Chroma
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
        generated_embedding = self.__embedding_function.embed_query(content)
        doc_id = str(uuid.uuid4())
        metadata = {tag.key: tag.value for tag in tags}
        self.__vector_store._collection.add(
            embeddings=[embedding],
            ids=[doc_id],
            metadatas=[metadata]
        )
        return uuid.UUID(doc_id)

    def get_best_mach(self, text: str, tags: list[MetaTag]) -> SimilarityResult | None:
        # Generar embedding de la consulta
        query_embedding = self.__embedding_function.embed_query(text)

        # Filtrar por tags si es necesario
        filter = {"tags": {"$in": [tag.name for tag in tags]}} if tags else None

        # Buscar en Chroma
        results = self.__vector_store._collection.query(
            query_embeddings=[query_embedding],
            n_results=1,
            where=filter
        )

        if not results['ids'][0]:
            return None

        # Reconstruir el resultado
        result = SimilarityResult()
        result.fragmentId = results['ids'][0]
        result.score = results['distances'][0]
        return result

    def get_approximate_matches(self, text: str, tags: list[MetaTag], max_matches: int) -> list[SimilarityResult]:
        pass
