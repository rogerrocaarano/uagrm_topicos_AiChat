from dependency_injector import containers, providers

from Infrastructure.Persistence.LangChainFragmentRepository import LangChainFragmentRepository


class Container(containers.DeclarativeContainer):
    fragments_repository = providers.Singleton(LangChainFragmentRepository)
