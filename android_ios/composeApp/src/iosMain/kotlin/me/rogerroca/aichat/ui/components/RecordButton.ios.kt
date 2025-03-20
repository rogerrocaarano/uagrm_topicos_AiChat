package me.rogerroca.aichat.ui.components

import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Mic
import androidx.compose.material3.Icon
import androidx.compose.material3.IconButton
import androidx.compose.runtime.Composable

@Composable
actual fun RecordButton(onResult: (String) -> Unit) {
    IconButton(onClick = {
        onResult("Hola")
    }) {
        Icon(
            imageVector = Icons.Filled.Mic,
            contentDescription = "Record"
        )
    }
}