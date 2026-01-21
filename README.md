/TriviaGame
│
├─ Controllers/             # Tus controladores MVC
│   ├─ HomeController.cs
│   ├─ GameController.cs    # Controlador que consumirá la API de Trivia
│
├─ Models/                  # Modelos para la vista y DTOs locales
│   ├─ GameSessionViewModel.cs
│   ├─ QuestionViewModel.cs
│   ├─ AnswerViewModel.cs
│   └─ UserAnswerViewModel.cs
│
├─ Views/
│   ├─ Shared/
│   │   ├─ _Layout.cshtml   # Layout principal
│   │   └─ _ValidationScriptsPartial.cshtml
│   ├─ Home/
│   │   └─ Index.cshtml
│   ├─ Game/
│   │   ├─ Start.cshtml
│   │   ├─ Questions.cshtml
│   │   ├─ Result.cshtml
│   │   └─ History.cshtml
│
├─ wwwroot/                 # Archivos estáticos: CSS, JS, imágenes
│   ├─ css/
│   ├─ js/
│   └─ images/
│
├─ Services/                # Aquí defines servicios para consumir tu API
│   ├─ IGameApiService.cs
│   └─ GameApiService.cs    # Implementa llamadas HTTP al backend
│
├─ Helpers/                 # Clases auxiliares
│   └─ ApiClient.cs         # Wrapper para HttpClient
│
├─ appsettings.json         # Configuración, por ejemplo URL del backend
├─ Program.cs / Startup.cs  # Configuración de DI, middlewares, etc.
