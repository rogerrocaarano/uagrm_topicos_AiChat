from pydantic import BaseModel


# -*- coding: utf-8 -*-

class PostFragmentIngest(BaseModel):
    fragment: str
    documentName: str
