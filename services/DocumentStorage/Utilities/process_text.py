import spacy
import os
import uuid
import json
from datetime import datetime

nlp = spacy.load("en_core_web_sm")

def process_document(file_path):
    with open(file_path, "r", encoding="utf-8") as f:
        text = f.read()
    
    doc_id = str(uuid.uuid4())
    fragments = []
    doc = nlp(text)

    for i, sent in enumerate(doc.sents):
        fragments.append({
            "id": str(uuid.uuid4()),
            "vectorid": None,
            "content": sent.text,
            "documentid": doc_id,
            "sequenceid": i + 1
        })
    
    return {
        "document": {
            "id": doc_id,
            "name": os.path.basename(file_path),
            "description": "Procesado con spaCy",
            "uploaddatetime": str(datetime.utcnow())
        },
        "fragments": fragments
    }

folder = "C:\\Users\\Nathalia\\source\\repos\\PuebaTopicosSpacy\\DocPenales"

documents = []
for file_name in os.listdir(folder):
    if file_name.endswith(".txt"):
        doc_data = process_document(os.path.join(folder, file_name))
        documents.append(doc_data)

with open("output.json", "w", encoding="utf-8") as f:
    json.dump(documents, f, indent=4, ensure_ascii=False)