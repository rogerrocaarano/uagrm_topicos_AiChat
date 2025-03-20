package me.rogerroca.aichat.ui.screens

import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Delete
import androidx.compose.material3.AlertDialog
import androidx.compose.material3.Button
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.Icon
import androidx.compose.material3.IconButton
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.material3.TopAppBar
import androidx.compose.material3.TopAppBarDefaults
import androidx.compose.runtime.Composable
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.ui.Modifier
import androidx.compose.ui.input.nestedscroll.nestedScroll
import me.rogerroca.aichat.repositories.MessageRepository
import me.rogerroca.aichat.utils.getPlatform
import me.rogerroca.aichat.ui.components.Chat

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun ChatScreen(title: String) {
    val scrollBehavior = TopAppBarDefaults.pinnedScrollBehavior()
    val showDiscardChatConfirmation = remember { mutableStateOf(false) }

    if (showDiscardChatConfirmation.value) {
        AlertDialog(
            onDismissRequest = { showDiscardChatConfirmation.value = false },
            title = { Text("Discard Chat Messages") },
            text = { Text("Are you sure you want to discard all chat messages?") },
            confirmButton = {
                Button(onClick = {
                    MessageRepository.clearMessages()
                    showDiscardChatConfirmation.value = false
                }) {
                    Text("Discard")
                }
            },
            dismissButton = {
                Button(onClick = { showDiscardChatConfirmation.value = false }) {
                    Text("Cancel")
                }
            }
        )
    }

    Scaffold(
        topBar = {
            TopAppBar(
                title = { Text(title + " ${getPlatform().name}") },
                actions = {
                    IconButton(onClick = { showDiscardChatConfirmation.value = true }) {
                        Icon(
                            imageVector = Icons.Default.Delete,
                            contentDescription = "Discard Chat Messages"
                        )
                    }
                },
                scrollBehavior = scrollBehavior
            )
        },
        modifier = Modifier.nestedScroll(scrollBehavior.nestedScrollConnection)
    ) { padding ->
        Chat(padding, messages = MessageRepository.messages)
    }
}