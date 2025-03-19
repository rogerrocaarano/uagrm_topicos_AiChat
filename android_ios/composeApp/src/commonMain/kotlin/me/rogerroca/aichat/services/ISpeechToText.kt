package me.rogerroca.aichat.services

interface ISpeechToText {
    fun startRecording()
    fun stopRecording()
    fun setOnResultListener(listener: (String) -> Unit)
}