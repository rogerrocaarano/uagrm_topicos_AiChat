from dependency_injector import containers, providers

from Infrastructure.Persistence.ChromaDbFragmentRepository import ChromaDbFragmentRepository


class Container(containers.DeclarativeContainer):
    fragments_repository = providers.Singleton(ChromaDbFragmentRepository)
