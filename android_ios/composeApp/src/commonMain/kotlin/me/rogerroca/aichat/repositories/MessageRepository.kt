package me.rogerroca.aichat.repositories

import androidx.compose.runtime.State
import androidx.compose.runtime.mutableStateOf
import me.rogerroca.aichat.data.ApiResponse
import me.rogerroca.aichat.data.ChatRequest
import me.rogerroca.aichat.data.Message
import me.rogerroca.aichat.services.ApiClient

object MessageRepository {
    private val _messages = mutableStateOf<List<Message>>(emptyList())
    val messages : State<List<Message>> = _messages

    fun getMessages() : List<Message> {
        return _messages.value
    }

    fun clearMessages() {
        _messages.value = emptyList()
    }

    suspend fun addUserMessage(message: String) {
        _messages.value += Message(message, true)


        val apiRequest = ChatRequest(_messages.value)
        val apiResponse : ApiResponse<Message> = ApiClient.postChat(apiRequest)
        if (!apiResponse.success) {
            _messages.value += Message("Algo sucedió y no pude responderte.", false)
            return
        }

        _messages.value += Message(apiResponse.data.text, false)
    }
}