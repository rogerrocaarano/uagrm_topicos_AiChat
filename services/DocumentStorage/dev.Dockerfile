FROM python:3.11

WORKDIR /src

# Instala wget y dependencias mínimas
RUN apt-get update && apt-get install -y wget unzip \
    && rm -rf /var/lib/apt/lists/*

# Instalar dotnet sdk
RUN wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh && \
    chmod +x dotnet-install.sh && \
    ./dotnet-install.sh --channel 6.0 && \
    rm dotnet-install.sh

# Setea variables de entorno para que dotnet y dotnet-ef funcionen
ENV DOTNET_ROOT="/root/.dotnet"
ENV PATH="/root/.dotnet:/root/.dotnet/tools:${PATH}"

# Instala dotnet-ef globalmente
RUN dotnet tool install --global dotnet-ef --version 6.0.0

# Instala dependencias de Python
COPY Utilities/requirements.txt /src/requirements.txt
RUN pip install --no-cache-dir -r requirements.txt
RUN python -m spacy download es_dep_news_trf

EXPOSE 5000