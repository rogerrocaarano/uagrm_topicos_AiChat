package me.rogerroca.aichat.ui.components

import android.annotation.SuppressLint
import android.app.Activity
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Mic
import androidx.compose.material3.Icon
import androidx.compose.material3.IconButton
import androidx.compose.runtime.Composable
import androidx.compose.ui.platform.LocalContext
import me.rogerroca.aichat.utils.permissions.RecordAudioPermission

@SuppressLint("ContextCastToActivity")
@Composable
actual fun RecordButton(onResult: (String) -> Unit) {
    val context = LocalContext.current
    val activity = LocalContext.current as Activity
    val recordAudioPermission = RecordAudioPermission(context, activity)

    IconButton(onClick = {
        val granted = recordAudioPermission.isGranted()
        if (!granted) {
            recordAudioPermission.request()
        } else {
            onResult("Hola")
        }
    }) {
        Icon(
            imageVector = Icons.Filled.Mic,
            contentDescription = "Record"
        )
    }
}