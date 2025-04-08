import spacy


def split_text(text: str) -> list[str]:
    model = "es_dep_news_trf"
    nlp = spacy.load(model)
    doc = nlp(text)
    processed = list(doc.sents)
    sentences = []
    for sentence in processed:
        sentences.append(sentence.text)
    return sentences
