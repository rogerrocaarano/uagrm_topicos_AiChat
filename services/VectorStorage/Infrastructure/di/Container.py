import os
from dependency_injector import containers, providers

from Infrastructure.Persistence.ChromaDbFragmentRepository import ChromaDbFragmentRepository


class Container(containers.DeclarativeContainer):
    # environment variables
    chromadb_persistence_path = os.getenv(
        key="CHROMADB_PERSISTENCE_PATH",
        default="Infrastructure/Persistence/chroma_db"
    )
    embedding_model = os.getenv(
        key="EMBEDDING_MODEL",
        default="all-MiniLM-L6-v2"
    )

    # Repository
    fragments_repository = providers.Singleton(
        ChromaDbFragmentRepository,
        collection_name="ingested_docs",
        persistence_path=chromadb_persistence_path,
        embedding_model=embedding_model
    )
