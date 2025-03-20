package me.rogerroca.aichat.ui.components

import android.annotation.SuppressLint
import android.app.Activity
import android.widget.Toast
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Mic
import androidx.compose.material3.Icon
import androidx.compose.material3.IconButton
import androidx.compose.runtime.Composable
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.platform.LocalContext
import me.rogerroca.aichat.services.AndroidSpeechToText
import me.rogerroca.aichat.utils.permissions.RecordAudioPermission

@SuppressLint("ContextCastToActivity")
@Composable
actual fun RecordButton(onResult: (String) -> Unit) {
    val context = LocalContext.current
    val activity = LocalContext.current as Activity
    val recordAudioPermission = RecordAudioPermission(context, activity)
    val speechToText = AndroidSpeechToText(context)
    var isRecording = false

    speechToText.setOnResultListener(onResult)

    IconButton(onClick = {
        val granted = recordAudioPermission.isGranted()
        if (!granted) {
            recordAudioPermission.request()
            return@IconButton
        }

        if (isRecording) {
            speechToText.stopRecording()
            isRecording = false
            Toast.makeText(context, "Recording stopped", Toast.LENGTH_SHORT).show()
        } else {
            speechToText.startRecording()
            isRecording = true
            Toast.makeText(context, "Recording started", Toast.LENGTH_SHORT).show()
        }
    }) {
        Icon(
            imageVector = Icons.Filled.Mic,
            contentDescription = "Record",
        )
    }
}