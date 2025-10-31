using System;
using System.IO;

namespace Formulario_Cadastro_Cliente.Services
{
    public class LoggerService
    {
        private readonly string _logFilePath;

        public LoggerService()
        {
            _logFilePath = Path.Combine(AppContext.BaseDirectory, "logs.txt");
        }

        private void Log(string nivel, string mensagem, Exception ex = null)
        {
            var logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{nivel}] {mensagem}";
            if (ex != null)
            {
                logMessage += Environment.NewLine + $"Exception: {ex}";
            }

            Console.WriteLine(logMessage); // Log no console
            File.AppendAllText(_logFilePath, logMessage + Environment.NewLine); // Log no arquivo
        }

        public void LogInfo(string mensagem) => Log("INFO", mensagem);
        public void LogWarning(string mensagem) => Log("WARNING", mensagem);
        public void LogError(string mensagem, Exception ex = null) => Log("ERROR", mensagem, ex);
        public void LogCritical(string mensagem, Exception ex = null) => Log("CRITICAL", mensagem, ex);
    }
}
