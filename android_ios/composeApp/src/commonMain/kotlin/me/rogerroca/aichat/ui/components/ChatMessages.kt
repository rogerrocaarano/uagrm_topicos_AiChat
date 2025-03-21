package me.rogerroca.aichat.ui.components

import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.foundation.lazy.rememberLazyListState
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.unit.dp
import com.mikepenz.markdown.m3.Markdown
import me.rogerroca.aichat.data.Message

@Composable
fun ChatMessages(messages: List<Message>) {
    // Remember the state of the list, so we can scroll to the bottom when a new message is added
    val listState = rememberLazyListState()
    LaunchedEffect(messages.size) {
        if (messages.size > 2) {
            listState.animateScrollToItem(messages.size - 2)
        }
    }
    LazyColumn(
        state = listState
    ) {
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
                Markdown(
                    message.text,
                    modifier = Modifier.padding(16.dp)
                )
            }
        }
    }
}