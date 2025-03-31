from datetime import datetime
from pydantic import BaseModel


class ApiResponse(BaseModel):
    content: object = None
    dateTime: datetime = datetime.now()
    isException: bool = False
