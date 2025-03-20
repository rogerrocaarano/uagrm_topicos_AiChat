package me.rogerroca.aichat.services

import android.content.Context
import android.content.Intent
import android.os.Bundle
import android.speech.RecognitionListener
import android.speech.RecognizerIntent
import android.speech.SpeechRecognizer
import android.util.Log

class AndroidSpeechToText(private val context: Context) : ISpeechToText {
    private var listener: ((String) -> Unit)? = null
    private var errorListener: ((String) -> Unit)? = null
    private val speechRecognizer = SpeechRecognizer.createSpeechRecognizer(context)

    init {
        speechRecognizer.setRecognitionListener(object : RecognitionListener {
            override fun onReadyForSpeech(params: Bundle?) {
                Log.d("AndroidSpeechToText", "Listo para escuchar 🎤")
            }

            override fun onBeginningOfSpeech() {
                Log.d("AndroidSpeechToText", "Comenzando reconocimiento de voz")
            }

            override fun onRmsChanged(rmsdB: Float) {
                // Opcional: Se puede usar para mostrar visualmente la intensidad del sonido
                Log.d("AndroidSpeechToText", "Nivel de volumen: $rmsdB dB")
            }

            override fun onBufferReceived(buffer: ByteArray?) {
                Log.d("AndroidSpeechToText", "Datos de audio recibidos")
            }

            override fun onEndOfSpeech() {
                Log.d("AndroidSpeechToText", "Fin del reconocimiento de voz")
            }

            override fun onError(error: Int) {
                val errorMessage = when (error) {
                    SpeechRecognizer.ERROR_AUDIO -> "Error de audio"
                    SpeechRecognizer.ERROR_CLIENT -> "Error del cliente"
                    SpeechRecognizer.ERROR_INSUFFICIENT_PERMISSIONS -> "Permiso denegado"
                    SpeechRecognizer.ERROR_NETWORK -> "Error de red"
                    SpeechRecognizer.ERROR_NETWORK_TIMEOUT -> "Tiempo de espera de red"
                    SpeechRecognizer.ERROR_NO_MATCH -> "No se encontró coincidencia"
                    SpeechRecognizer.ERROR_RECOGNIZER_BUSY -> "El reconocimiento ya está en uso"
                    SpeechRecognizer.ERROR_SERVER -> "Error del servidor"
                    SpeechRecognizer.ERROR_SPEECH_TIMEOUT -> "No se detectó habla"
                    else -> "Error desconocido"
                }
                Log.e("AndroidSpeechToText", "Error en reconocimiento de voz: $errorMessage")
                errorListener?.invoke(errorMessage)
            }

            override fun onResults(results: Bundle?) {
                val matches = results?.getStringArrayList(SpeechRecognizer.RESULTS_RECOGNITION)
                val text = matches?.joinToString(" ") ?: "No se reconoció nada"
                Log.d("AndroidSpeechToText", "Texto reconocido: $text")
                listener?.invoke(text)
            }

            override fun onPartialResults(partialResults: Bundle?) {
                val matches = partialResults?.getStringArrayList(SpeechRecognizer.RESULTS_RECOGNITION)
                val partialText = matches?.joinToString(" ") ?: ""
                Log.d("AndroidSpeechToText", "Reconocimiento parcial: $partialText")
            }

            override fun onEvent(eventType: Int, params: Bundle?) {
                Log.d("AndroidSpeechToText", "Evento recibido: $eventType")
            }
        })
    }

    override fun startRecording() {
        Log.d("AndroidSpeechToText", "Iniciando reconocimiento de voz")
        val intent = Intent(RecognizerIntent.ACTION_RECOGNIZE_SPEECH).apply {
            putExtra(RecognizerIntent.EXTRA_LANGUAGE_MODEL, RecognizerIntent.LANGUAGE_MODEL_FREE_FORM)
            putExtra(RecognizerIntent.EXTRA_PARTIAL_RESULTS, true) // Para recibir resultados parciales
            putExtra(RecognizerIntent.EXTRA_LANGUAGE, "es-ES") // Cambia el idioma si es necesario
        }
        speechRecognizer.startListening(intent)
    }

    override fun stopRecording() {
        Log.d("AndroidSpeechToText", "Deteniendo reconocimiento de voz")
        speechRecognizer.stopListening()
    }

    override fun setOnResultListener(listener: (String) -> Unit) {
        this.listener = listener
    }

    fun setOnErrorListener(listener: (String) -> Unit) {
        this.errorListener = listener
    }
}
