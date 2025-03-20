package me.rogerroca.aichat.utils.permissions

import android.app.Activity
import android.content.Context
import android.content.pm.PackageManager
import androidx.core.app.ActivityCompat.requestPermissions
import androidx.core.content.ContextCompat.checkSelfPermission

abstract class PermissionHandler(
    private val context: Context,
    private val activity: Activity,
    private val permission: String,
    private val requestCode: Int
) {

    fun isGranted(): Boolean {
        return checkSelfPermission(context, permission) == PackageManager.PERMISSION_GRANTED
    }

    fun request() {
        requestPermissions(activity, arrayOf(permission), requestCode)
    }
}