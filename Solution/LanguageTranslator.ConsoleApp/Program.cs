using LanguageTranslator.ClassLibrary;

Console.WriteLine("Welcome to the Speech Recognition App");
Console.WriteLine("Please enter Language:TextToTranslate to begin. Exit to enter.");

bool isRunning = true;

while (isRunning)
{
    string? inputText = Console.ReadLine().Trim();
    if (inputText.ToLower().Trim() == "exit")
    {
        isRunning = false;
    }
    else
    {
        string[] toLanguage = inputText.Split(':');
        LanguageTranslatorApp app  = new LanguageTranslatorApp();
        app.TranslateText(inputText, toLanguage[0]);
        Console.WriteLine(app.TranslatedText);
    }
}