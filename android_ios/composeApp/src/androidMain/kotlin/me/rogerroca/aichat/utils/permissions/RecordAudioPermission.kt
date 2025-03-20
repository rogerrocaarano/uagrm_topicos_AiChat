package me.rogerroca.aichat.utils.permissions

import android.app.Activity
import android.content.Context

class RecordAudioPermission(context: Context, activity: Activity) : PermissionHandler(
    permission = android.Manifest.permission.RECORD_AUDIO,
    requestCode = REQUEST_RECORD_AUDIO_PERMISSION,
    context = context,
    activity = activity
) {
    companion object {
        const val REQUEST_RECORD_AUDIO_PERMISSION = 100
    }
}