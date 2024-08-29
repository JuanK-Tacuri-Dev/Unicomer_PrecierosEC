using Microsoft.Data.Sqlite;
using PrecierosEC.Core.Utiliies;
using System.Data;

namespace PrecierosEC.APi.Extensions
{
    public class AuditMiddleware
    {
        private readonly RequestDelegate _next;

        protected SqliteConnection Connection;
        public void SetearConexion()
        {
            SQLitePCL.Batteries.Init();
            Connection = new SqliteConnection($"data source= {Path.Combine(AppConfiguration.RutaAuditDatabase, AppConfiguration.NameDatabaseAudit)}");
            Connection.Open();
        }
        public void CerrarConexion()
        {

            if (Connection is not null)
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.CloseAsync();

                Connection.DisposeAsync();
                Connection = null;
            }

        }

        public AuditMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var RequestDate = DateTime.UtcNow;
            var originalResponseBodyStream = context.Response.Body;
            using var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            var request = context.Request;
            var requestBody = string.Empty;

            // Manejo de Request Body solo para métodos distintos de GET
            if (request.Method != HttpMethods.Get && request.ContentLength > 0)
            {
                request.EnableBuffering();
                using var reader = new StreamReader(request.Body, leaveOpen: true);
                requestBody = await reader.ReadToEndAsync();
                request.Body.Seek(0, SeekOrigin.Begin);
            }

            // Construir el RequestPath incluyendo los parámetros de la consulta
            var fullRequestPath = request.Path + request.QueryString;

            try
            {
                await _next(context);
            }
            finally
            {
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                var responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                var hostName = context.Connection.LocalIpAddress?.ToString();
                var hostAddress = context.Connection.RemoteIpAddress?.ToString();
                var ResponseDate = DateTime.UtcNow;
                var Timestamp = (ResponseDate - RequestDate).TotalSeconds;

                var auditLog = new
                {
                    RequestMethod = request.Method,
                    RequestDate = RequestDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    RequestPath = fullRequestPath, // Usar el RequestPath completo con parámetros
                    RequestBody = requestBody,
                    ResponseBody = responseBody,
                    ResponseDate = ResponseDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    HostName = hostName,
                    HostAddress = hostAddress,
                    StatusCode = context.Response.StatusCode,
                    Timestamp = Timestamp.ToString()
                };

                try
                {
                    CreateDatabaseDefaultIfNotExist();
                    SaveAuditLog(auditLog);
                }
                catch (Exception ex)
                {
                    WriteLogToFileAsync(ex);
                }

                responseBodyStream.Seek(0, SeekOrigin.Begin);
                await responseBodyStream.CopyToAsync(originalResponseBodyStream);
            }
        }

        //public async Task InvokeAsync(HttpContext context)
        //{
        //    var RequestDate = DateTime.UtcNow;
        //    var originalResponseBodyStream = context.Response.Body;
        //    using var responseBodyStream = new MemoryStream();
        //    context.Response.Body = responseBodyStream;

        //    var request = context.Request;
        //    var requestBody = string.Empty;
        //    if (request.ContentLength > 0)
        //    {
        //        request.EnableBuffering();
        //        using var reader = new StreamReader(request.Body, leaveOpen: true);

        //        requestBody = await reader.ReadToEndAsync();
        //        request.Body.Seek(0, SeekOrigin.Begin);

        //    }

        //    try
        //    {
        //        await _next(context);
        //    }
        //    finally
        //    {
        //        responseBodyStream.Seek(0, SeekOrigin.Begin);
        //        var responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();
        //        responseBodyStream.Seek(0, SeekOrigin.Begin);
        //        var hostName = context.Connection.LocalIpAddress?.ToString();
        //        var hostAddress = context.Connection.RemoteIpAddress?.ToString();
        //        var ResponseDate = DateTime.UtcNow;
        //        var Timestamp = (ResponseDate - RequestDate).TotalSeconds;

        //        var auditLog = new
        //        {
        //            RequestMethod = request.Method,
        //            RequestDate = RequestDate.ToString("yyyy-MM-dd HH:mm:ss"),
        //            RequestPath = request.Path.ToString(),
        //            RequestBody = requestBody,
        //            ResponseBody = responseBody,
        //            ResponseDate = ResponseDate.ToString("yyyy-MM-dd HH:mm:ss"),
        //            HostName = hostName,
        //            HostAddress = hostAddress,
        //            StatusCode = context.Response.StatusCode,
        //            Timestamp = Timestamp.ToString()
        //        };


        //        try
        //        {
        //            CreateDatabaseDefaultIfNotExist();
        //            SaveAuditLog(auditLog);
        //        }
        //        catch (Exception ex)
        //        {
        //            WriteLogToFileAsync(ex);
        //        }
        //        responseBodyStream.Seek(0, SeekOrigin.Begin);
        //        await responseBodyStream.CopyToAsync(originalResponseBodyStream);
        //    }
        //}


        public void SaveAuditLog(object model)
        {
            try
            {
                var DateIng = DateTime.Now.Date.ToString("yyyy-MM-dd");
                SetearConexion();
                dynamic audit = model;
                string SQL = @"INSERT INTO tbApplicationLog 
       ( HosteName,  HostAddress,  AppSource,  User,  StatusCode,  RequestDate,  RequestMetod,  RequestPath,  RequestBody,  ResponseDate,  ResponseBody,  Timestamp,  TrackingCode, DateIng) 
VALUES (@HosteName, @HostAddress, @AppSource, @User, @StatusCode, @RequestDate, @RequestMetod, @RequestPath, @RequestBody, @ResponseDate, @ResponseBody, @Timestamp, @TrackingCode, @DateIng);";

                using var command = new SqliteCommand(SQL, Connection);

                // Agregar parámetros al comando
                command.Parameters.Add(new SqliteParameter("@HosteName", DbType.String) { Value = audit.HostName });
                command.Parameters.Add(new SqliteParameter("@HostAddress", DbType.String) { Value = audit.HostAddress });
                command.Parameters.Add(new SqliteParameter("@AppSource", DbType.String) { Value = AppConfiguration.ApiData });
                command.Parameters.Add(new SqliteParameter("@User", DbType.String) { Value = AppConfiguration.NonUserLog });
                command.Parameters.Add(new SqliteParameter("@StatusCode", DbType.Int32) { Value = audit.StatusCode });
                command.Parameters.Add(new SqliteParameter("@RequestDate", DbType.String) { Value = audit.RequestDate });
                command.Parameters.Add(new SqliteParameter("@RequestMetod", DbType.String) { Value = audit.RequestMethod });
                command.Parameters.Add(new SqliteParameter("@RequestPath", DbType.String) { Value = audit.RequestPath });
                command.Parameters.Add(new SqliteParameter("@RequestBody", DbType.String) { Value = audit.RequestBody });
                command.Parameters.Add(new SqliteParameter("@ResponseDate", DbType.String) { Value = audit.ResponseDate });
                command.Parameters.Add(new SqliteParameter("@ResponseBody", DbType.String) { Value = audit.ResponseBody });
                command.Parameters.Add(new SqliteParameter("@Timestamp", DbType.String) { Value = audit.Timestamp });
                command.Parameters.Add(new SqliteParameter("@TrackingCode", DbType.String) { Value = AppConfiguration.TrackingCode });
                command.Parameters.Add(new SqliteParameter("@DateIng", DbType.String) { Value = DateIng });
                command.ExecuteNonQueryAsync();
            }
            finally
            {
                CerrarConexion();
                AppConfiguration.TrackingCode = "";
            }
        }

        private static async void WriteLogToFileAsync(Exception ex)
        {

            try
            {
                var filePath = $"{AppConfiguration.RutaAuditFichero}\\{DateTime.UtcNow:yyyy-MM-dd}";
                var logEntry = $"{Environment.NewLine}{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} {Utilities.GetExcepcion(ex)}";

                string directoryPath = Path.GetDirectoryName(filePath);

                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                await File.AppendAllTextAsync(filePath, logEntry);

            }
            catch (Exception)
            {

            }
        }

        private void CreateDatabaseDefaultIfNotExist()
        {


            Utilities.CreateDataBaseAuditIfNotExist();
            {
                try
                {
                    SetearConexion();
                    bool exisTable = false;
                    string tableName = "tbApplicationLog";
                    string cmdText = $"SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='{tableName}';";

                    using (SqliteCommand command = new(cmdText, Connection))
                    {
                        exisTable = Convert.ToBoolean(command.ExecuteScalar());
                    }
                    // Si la tabla no existe, la crea
                    if (!exisTable)
                    {
                        string sentenciaTablaLog = @"
                    CREATE TABLE IF NOT EXISTS tbApplicationLog (
                        [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        [HosteName] TEXT,
                        [HostAddress] TEXT,
                        [AppSource] TEXT,
                        [User] TEXT,
                        [StatusCode] INTEGER,
                        [RequestDate] TEXT,
                        [RequestMetod] TEXT,
                        [RequestPath] TEXT,
                        [RequestBody] TEXT,
                        [ResponseDate] TEXT,
                        [ResponseBody] TEXT,
                        [Timestamp] TEXT,
                        [TrackingCode] TEXT,
                        [DateIng] TEXT
                    );";
                        using SqliteCommand com2 = new(sentenciaTablaLog, Connection);
                        com2.ExecuteNonQueryAsync();
                    }
                }
                catch (Exception e)
                {

                }
                finally
                {
                    CerrarConexion();
                }
            }

        }
    }


}
