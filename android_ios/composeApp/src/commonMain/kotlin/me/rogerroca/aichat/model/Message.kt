package me.rogerroca.aichat.model

data class Message(
    val text: String,
    val isUser: Boolean
)

val messages = listOf(
    Message("¿Cuáles son las coordenadas de la Torre Eifel?", true),
    Message("La Torre Eiffel se encuentra en París, Francia, y sus coordenadas geográficas son aproximadamente:\\n\\n- **Latitud:** 48.8584° N\\n- **Longitud:** 2.2945° E\\n\\nEstas coordenadas te llevarán al centro de la base de la Torre Eiffel.", false),
    Message("¿Y las de la Estatua de la Libertad?", true),
    Message("La Estatua de la Libertad se encuentra en la Isla de la Libertad (Liberty Island) en el puerto de Nueva York, Estados Unidos. Sus coordenadas geográficas aproximadas son:\\n\\n- **Latitud:** 40.6892° N\\n- **Longitud:** 74.0445° O\\n\\nEstas coordenadas te llevarán a la base de la Estatua de la Libertad.", false),
//    Message("¿Cuáles son las coordenadas de la Torre Eifel?", true),
//    Message("La Torre Eiffel se encuentra en París, Francia, y sus coordenadas geográficas son aproximadamente:\\n\\n- **Latitud:** 48.8584° N\\n- **Longitud:** 2.2945° E\\n\\nEstas coordenadas te llevarán al centro de la base de la Torre Eiffel.", false),
//    Message("¿Y las de la Estatua de la Libertad?", true),
//    Message("La Estatua de la Libertad se encuentra en la Isla de la Libertad (Liberty Island) en el puerto de Nueva York, Estados Unidos. Sus coordenadas geográficas aproximadas son:\\n\\n- **Latitud:** 40.6892° N\\n- **Longitud:** 74.0445° O\\n\\nEstas coordenadas te llevarán a la base de la Estatua de la Libertad.", false),
)