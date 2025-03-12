package me.rogerroca.aichat.repositories

import androidx.compose.runtime.State
import androidx.compose.runtime.mutableStateOf
import kotlinx.coroutines.delay
import me.rogerroca.aichat.model.ApiResponse
import me.rogerroca.aichat.model.Message

object MessageRepository {
    private val _messages = mutableStateOf<List<Message>>(emptyList())
    val messages : State<List<Message>> = _messages

    fun getMessages() : List<Message> {
        return _messages.value
    }

    suspend fun addUserMessage(message: String) {
        _messages.value += Message(message, true)


        val apiResponse : ApiResponse = sendMessagesToApi()
        if (apiResponse.isError) {
            _messages.value += Message("Algo sucedió y no pude responderte.", false)
            return
        }

        _messages.value += Message(apiResponse.content, false)
    }

    private suspend fun sendMessagesToApi() : ApiResponse {
        delay(3000)
        return ApiResponse(false, "Hola, soy un mensaje de prueba")
    }
}