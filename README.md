/TriviaGame
│
├─ Controllers/             
│   ├─ HomeController.cs
│   ├─ GameController.cs    
│
├─ Models/                
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
├─ wwwroot/                 
│   ├─ css/
│   ├─ js/
│   └─ images/
│
├─ Services/               
│   ├─ IGameApiService.cs
│   └─ GameApiService.cs    
│
├─ Helpers/               
│   └─ ApiClient.cs         
│
├─ appsettings.json         
├─ Program.cs / Startup.cs 
