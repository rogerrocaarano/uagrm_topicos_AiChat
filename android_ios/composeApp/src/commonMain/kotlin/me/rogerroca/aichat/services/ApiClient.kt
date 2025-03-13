package me.rogerroca.aichat.services

import io.ktor.client.HttpClient
import io.ktor.client.call.body
import io.ktor.client.plugins.DefaultRequest
import io.ktor.client.plugins.HttpTimeout
import io.ktor.client.plugins.contentnegotiation.ContentNegotiation
import io.ktor.client.request.post
import io.ktor.client.request.setBody
import io.ktor.http.ContentType
import io.ktor.http.URLProtocol
import io.ktor.http.contentType
import io.ktor.serialization.kotlinx.json.json
import kotlinx.serialization.json.Json
import me.rogerroca.aichat.data.ApiResponse
import me.rogerroca.aichat.data.ChatRequest
import me.rogerroca.aichat.data.Message


object ApiClient {
    private val client = HttpClient {
        install(ContentNegotiation) {
            json(Json {
                ignoreUnknownKeys = true
            })
        }
        install(DefaultRequest){
            url {
                protocol = URLProtocol.HTTPS
                host = "uagrm-topicos-apichat.runasp.net"
                port = 443
            }
        }
        install(HttpTimeout) {
            requestTimeoutMillis = 120_000
            connectTimeoutMillis = 30_000
            socketTimeoutMillis = 120_000
        }
    }

    suspend fun postChat(request: ChatRequest) : ApiResponse<Message> {
        val response = client.post("/chat") {
            contentType(ContentType.Application.Json)
            setBody(request)
        }
        return response.body()
    }
}

