from pydantic.v1 import BaseModel


# -*- coding: utf-8 -*-

class MetaTag(BaseModel):
    key: str
    value: str
