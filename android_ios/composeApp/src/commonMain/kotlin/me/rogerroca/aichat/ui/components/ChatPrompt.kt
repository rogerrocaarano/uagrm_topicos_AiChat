package me.rogerroca.aichat.ui.components

import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.width
import androidx.compose.material3.Button
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalFocusManager
import androidx.compose.ui.platform.LocalSoftwareKeyboardController
import androidx.compose.ui.text.input.TextFieldValue
import androidx.compose.ui.unit.dp
import kotlinx.coroutines.launch
import me.rogerroca.aichat.repositories.MessageRepository

@Composable
fun ChatPrompt() {
    var userPrompt by remember { mutableStateOf(TextFieldValue("")) }
    var userIsSpeeching by remember { mutableStateOf(false) }
    var userIsWriting by remember { mutableStateOf(false) }
    val coroutineScope = rememberCoroutineScope()
    val keyboardController = LocalSoftwareKeyboardController.current
    val focusManager = LocalFocusManager.current

    Row(
        modifier = Modifier.padding(16.dp)
    ) {
        TextField(
            value = userPrompt,
            onValueChange = {
                userPrompt = it
                userIsWriting = it.text.isNotEmpty()
            },
            modifier = Modifier.weight(1f)
        )
        Spacer(
            modifier = Modifier.width(8.dp)
        )

        if (userIsWriting) {
            Button(
                onClick = {
                    val promptText = userPrompt.text
                    coroutineScope.launch { MessageRepository.addUserMessage(promptText) }
                    userPrompt = TextFieldValue("")
                    keyboardController?.hide()
                    focusManager.clearFocus()
                }
            ) {
                Text("Send")
            }
        } else {
            RecordButton(userPrompt)
        }
    }
}

