namespace PrecierosEC.APi.Extensions
{
    public class AuditMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _logFilePath;

        public AuditMiddleware(RequestDelegate next, string logFilePath)
        {
            _next = next;
            _logFilePath = logFilePath;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalResponseBodyStream = context.Response.Body;
            using var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            var request = context.Request;
            var requestBody = string.Empty;
            if (request.ContentLength > 0)
            {
                request.EnableBuffering();
                using var reader = new StreamReader(request.Body, leaveOpen: true);

                requestBody = await reader.ReadToEndAsync();
                request.Body.Seek(0, SeekOrigin.Begin);

            }


            try
            {
                // Continúa con la ejecución del pipeline
                await _next(context);
            }
            finally
            {
                // Captura y guarda el cuerpo de la respuesta
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                var responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                var clientIp = context.Connection.RemoteIpAddress?.ToString();
                // Crea el log de auditoría
                var auditLog = new
                {
                    RequestMethod = request.Method,
                    RequestPath = request.Path,
                    RequestBody = requestBody,
                    ResponseBody = responseBody,
                    ClientIp = clientIp,
                    context.Response.StatusCode,
                    Timestamp = DateTime.UtcNow
                };

                // Guarda el log de auditoría en un archivo
                try
                {
                    await WriteLogToFileAsync(auditLog);
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones opcional o logueo
                    throw;
                }

                // Restaura el stream original y escribe la respuesta
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                await responseBodyStream.CopyToAsync(originalResponseBodyStream);
            }
        }

        private async Task WriteLogToFileAsync(object log)
        {
            var logEntry = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {log}\n";

            try
            {
                //await File.AppendAllTextAsync(_logFilePath, logEntry);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones opcional o logueo
                throw;
            }
        }
    }


}
