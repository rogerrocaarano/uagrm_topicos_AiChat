package me.rogerroca.aichat.data

import kotlinx.serialization.Serializable

@Serializable
data class Message(
    val text: String,
    val isUserMessage: Boolean
)
