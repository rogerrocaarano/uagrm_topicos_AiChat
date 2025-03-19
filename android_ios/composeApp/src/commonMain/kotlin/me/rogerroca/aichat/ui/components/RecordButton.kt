package me.rogerroca.aichat.ui.components

import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Mic
import androidx.compose.material3.Icon
import androidx.compose.material3.IconButton
import androidx.compose.runtime.Composable
import androidx.compose.ui.text.input.TextFieldValue

@Composable
fun RecordButton(onResult: (String) -> Unit) {
    IconButton(onClick = {
        onResult("Hola")
    }) {
        Icon(
            imageVector = Icons.Filled.Mic,
            contentDescription = "Record"
        )
    }
}