package me.rogerroca.aichat.ui.components

import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.unit.dp
import me.rogerroca.aichat.data.Message

@Composable
fun ChatMessages(messages: List<Message>) {
    LazyColumn {
        items(messages) { message ->
            if (message.isUserMessage) {
                Row(
                    modifier = Modifier
                        .padding(16.dp)
                ) {
                    Spacer(
                        modifier = Modifier.weight(1f)
                    )
                    Text(
                        modifier = Modifier
                            .background(
                                color = Color.LightGray,
                                shape = MaterialTheme.shapes.medium
                            )
                            .padding(12.dp),
                        text = message.text
                    )
                }
            } else {
                Text(
                    message.text,
                    modifier = Modifier.padding(16.dp)
                )
            }
        }
    }
}