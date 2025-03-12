package me.rogerroca.aichat

import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.width
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.material3.Button
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.material3.TopAppBar
import androidx.compose.material3.TopAppBarDefaults
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.input.nestedscroll.nestedScroll
import androidx.compose.ui.text.input.TextFieldValue
import androidx.compose.ui.unit.dp

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun App() {
    MaterialTheme {
        Surface(modifier = Modifier.fillMaxSize()) {
            val scrollBehavior = TopAppBarDefaults.pinnedScrollBehavior()
            Scaffold(
                topBar = {
                    TopAppBar(
                        title = { Text("AI Chat") },
                        scrollBehavior = scrollBehavior
                    )
                },
                modifier = Modifier.nestedScroll(scrollBehavior.nestedScrollConnection)
            ) { padding ->
                Column(
                    modifier = Modifier.padding(padding)
                ) {
                    Box(
                        modifier = Modifier.weight(1f)
                    ) {
                        Messages(messages)
                    }
                    Prompt()
                }
            }
        }
    }
}
@Composable
fun Messages(messages: List<Message>) {
    LazyColumn {
        items(messages.size) { index ->
            val message = messages[index]
            if (message.isUser) {
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

@Composable
fun Prompt() {
    var text by remember { mutableStateOf(TextFieldValue("")) }
    Row(
        modifier = Modifier.padding(16.dp)
    ) {
        TextField(
            value = text,
            onValueChange = { text = it },
            modifier = Modifier.weight(1f)
        )
        Spacer(
            modifier = Modifier.width(8.dp)
        )
        Button(
            onClick = { TODO() }
        ) {
            Text("Send")
        }
    }
}