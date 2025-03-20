package me.rogerroca.aichat.utils

interface Platform {
    val name: String
}

expect fun getPlatform(): Platform
