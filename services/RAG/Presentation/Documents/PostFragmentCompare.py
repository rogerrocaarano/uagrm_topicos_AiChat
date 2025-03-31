from pydantic import BaseModel


# -*- coding: utf-8 -*-

class PostFragmentCompare(BaseModel):
    fragment: str = ""
    max_matches: int = 1
