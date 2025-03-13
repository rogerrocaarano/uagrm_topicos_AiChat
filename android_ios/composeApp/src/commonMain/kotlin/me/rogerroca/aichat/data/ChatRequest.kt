package me.rogerroca.aichat.data

import kotlinx.serialization.Serializable

@Serializable
data class ChatRequest(val messages: List<Message>)
