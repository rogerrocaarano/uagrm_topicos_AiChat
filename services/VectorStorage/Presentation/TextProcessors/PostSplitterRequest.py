from pydantic import BaseModel

class PostSplitterRequest(BaseModel):
    text: str