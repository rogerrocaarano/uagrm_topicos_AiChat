import uuid
import chromadb
from chromadb import QueryResult
from sentence_transformers import SentenceTransformer

from Domain.Model.MetaTag import MetaTag
from Domain.Model.SimilarityResult import SimilarityResult
from Domain.Repository.IFragmentRepository import IFragmentRepository


class ChromaDbFragmentRepository(IFragmentRepository):
    def __init__(self, collection_name: str):
        self.__embedding_model = SentenceTransformer('all-MiniLM-L6-v2')
        self.__chroma_client = chromadb.Client()
        self.__collection = self.__chroma_client.get_or_create_collection(name=collection_name)

    def add(self, content: str, tags: list[MetaTag]) -> uuid:
        # Prepare fragment for storage
        fragment_id: uuid = uuid.uuid4()
        tags_dict = self.__tags_to_dict(tags)
        embeddings = self.__embedding_model.encode(content)

        # Add the fragment to the collection
        self.__collection.add(ids=fragment_id.__str__(),
                              embeddings=embeddings,
                              metadatas=tags_dict)
        print(f"Fragment added with ID: {fragment_id.__str__()}")
        return fragment_id

    def get_best_match(self, text: str, tags: list[MetaTag]) -> SimilarityResult | None:
        embeddings = self.__embedding_model.encode(text)
        tags_dict = self.__tags_to_dict(tags)
        results: QueryResult = self.__collection.query(
            query_embeddings=embeddings,
            n_results=1,
            where=tags_dict)

        if not results or not results.get('ids'):
            return None

        return SimilarityResult(
            fragmentId=uuid.UUID(results.get('ids')[0][0]),
            score=results.get('distances')[0][0],
            tags=[MetaTag(key=k, value=v) for k, v in results.get('metadatas')[0]]
        )

    def get_approximate_matches(self, text: str, tags: list[MetaTag], max_matches: int) -> list[SimilarityResult]:
        pass

    @staticmethod
    def __tags_to_dict(tags: list[MetaTag]) -> dict | None:
        if not tags:
            return None
        return {tag.key: tag.value for tag in tags}
