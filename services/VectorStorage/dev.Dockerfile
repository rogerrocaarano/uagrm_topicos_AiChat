FROM python:3.11

WORKDIR /src

COPY requirements.txt /src/requirements.txt
RUN pip install --no-cache-dir -r requirements.txt

ENV CHROMADB_PERSISTENCE_PATH="/data/chroma"
EXPOSE 5000
CMD ["uvicorn", "Presentation.main:app", "--host", "0.0.0.0", "--port", "5000"]