package me.rogerroca.aichat.ui.components

import androidx.compose.runtime.Composable

@Composable
expect fun RecordButton(onResult: (String) -> Unit)