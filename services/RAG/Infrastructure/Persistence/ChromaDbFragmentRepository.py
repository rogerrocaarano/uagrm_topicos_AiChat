import uuid
import chromadb
import hashlib
from chromadb import QueryResult
from sentence_transformers import SentenceTransformer

from Domain.Entity.Fragment import Fragment
from Domain.Model.MetaTag import MetaTag
from Domain.Model.SimilarityResult import SimilarityResult
from Domain.Repository.IFragmentRepository import IFragmentRepository


class ChromaDbFragmentRepository(IFragmentRepository):
    def __init__(self, collection_name: str):
        self.__embedding_model = SentenceTransformer('all-MiniLM-L6-v2')
        self.__chroma_client = chromadb.PersistentClient()
        self.__collection = self.__chroma_client.get_or_create_collection(name=collection_name)

    def add(self, content: str, tags: list[MetaTag]) -> str:
        """
        Adds a document fragment to the collection. If the fragment already exists, it skips insertion.
        :param content: the text content of the fragment
        :param tags: a list of MetaTag objects associated with the fragment
        :return: str: the unique identifier of the fragment
        """
        fragment_id: str = self.__generate_hash_id(content)

        # Skip insertion if the fragment already exists
        existing_fragment = self.__collection.get(ids=[fragment_id])
        if existing_fragment and existing_fragment["ids"]:
            print(f"Fragment with ID {fragment_id} already exists. Skipping insertion.")
            return fragment_id

        tags_dict = self.__tags_to_dict(tags)
        embeddings = self.__embedding_model.encode(content)

        # Add the fragment to the collection
        self.__collection.add(ids=fragment_id,
                              embeddings=embeddings,
                              metadatas=tags_dict)
        print(f"Fragment added with ID: {fragment_id}")
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
        """
        Converts a list of MetaTag objects to a dictionary.
        :param tags: list[MetaTag]: A list of MetaTag objects.
        :return: dict | None: A dictionary with keys as tag keys and values as tag values, or None if the list is empty.
        """
        if not tags:
            return None
        return {tag.key: tag.value for tag in tags}

    @staticmethod
    def __generate_hash_id(text: str) -> str:
        """
        Generates a unique hash ID for the given text.
        :param text: str: The text to hash.
        :return: str: The generated hash ID.
        """
        return hashlib.md5(text.encode()).hexdigest()
