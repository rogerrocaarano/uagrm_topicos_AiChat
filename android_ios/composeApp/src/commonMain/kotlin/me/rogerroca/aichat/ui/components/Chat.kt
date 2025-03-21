package me.rogerroca.aichat.ui.components

import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.PaddingValues
import androidx.compose.foundation.layout.padding
import androidx.compose.runtime.Composable
import androidx.compose.runtime.State
import androidx.compose.ui.Modifier
import me.rogerroca.aichat.data.Message

@Composable
fun Chat(padding: PaddingValues, messages: State<List<Message>>) {
    Column(
        modifier = Modifier.padding(padding)
    ) {
        Box(
            modifier = Modifier.weight(1f)
        ) {
            ChatMessages(messages.value)
        }
        ChatPrompt()
    }
}