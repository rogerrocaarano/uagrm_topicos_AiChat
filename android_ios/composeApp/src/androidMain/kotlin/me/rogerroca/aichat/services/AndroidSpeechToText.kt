package me.rogerroca.aichat.services

import android.app.Activity
import android.content.Context
import android.widget.Toast
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat

class AndroidSpeechToText(private val context: Context) : ISpeechToText {

    companion object {
        private const val REQUEST_RECORD_AUDIO_PERMISSION = 100
        private const val USER_GRANTED_RECORD_AUDIO_PERMISSION_MSG =
            "User granted record audio permission"
        private const val USER_DENIED_RECORD_AUDIO_PERMISSION_MSG =
            "User denied record audio permission"
    }

    override fun startRecording() {
        // Verify that the user has granted permission to record audio
        if (!userHasGrantedRecordAudioPermission()) {
            requestRecordAudioPermission()
        }

        if (userHasGrantedRecordAudioPermission()) {
            Toast.makeText(
                context,
                USER_GRANTED_RECORD_AUDIO_PERMISSION_MSG,
                Toast.LENGTH_SHORT
            ).show()
        } else {
            Toast.makeText(
                context,
                USER_DENIED_RECORD_AUDIO_PERMISSION_MSG,
                Toast.LENGTH_SHORT
            ).show()
        }

    }

    override fun stopRecording() {
        // TODO("Not yet implemented")
    }

    override fun setOnResultListener(listener: (String) -> Unit) {
        // TODO("Not yet implemented")
    }

    private fun userHasGrantedRecordAudioPermission(): Boolean {
        return ContextCompat.checkSelfPermission(
            context,
            android.Manifest.permission.RECORD_AUDIO
        ) == android.content.pm.PackageManager.PERMISSION_GRANTED
    }

    private fun requestRecordAudioPermission() {
        ActivityCompat.requestPermissions(
            context as Activity,
            arrayOf(android.Manifest.permission.RECORD_AUDIO),
            REQUEST_RECORD_AUDIO_PERMISSION
        )
    }
}