
using Microsoft.Data.Sqlite;
using PrecierosEC.Core.Interface.Service;
using PrecierosEC.Core.Models;
using PrecierosEC.Core.Utiliies;
using System.Data;
using static PrecierosEC.Core.Utiliies.Constants;

namespace PrecierosEC.Core.Service
{
    public class ServiceErrorLog : IServiceErrorLog
    {
        protected SqliteConnection Connection;
        private string CodigoSeguimiento;
        public void SetearConexion()
        {
            SQLitePCL.Batteries.Init();
            Connection = new SqliteConnection($"data source={AppConfiguration.RutaLogDatabase}");
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

        private void SaveErrorLog(ErrorLogModel error)
        {
            try
            {
                SetearConexion();
                string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                string cmdText = @"
            INSERT INTO tbErrorLog ( Date,  LogonName,  AppSource,  Type,  Message,  AdditionalInformation,  TrackingCode) 
                            VALUES (@Date, @LogonName, @AppSource, @Type, @Message, @AdditionalInformation, @TrackingCode);";

                using var command = new SqliteCommand(cmdText, Connection);
                // Add parameters to the command
                command.Parameters.Add(new SqliteParameter("@LogonName", DbType.String) { Value = error.LogonName?.Replace("'", "\"") });
                command.Parameters.Add(new SqliteParameter("@Date", DbType.String) { Value = date });
                command.Parameters.Add(new SqliteParameter("@AppSource", DbType.String) { Value = error.ApplicationSource?.Replace("'", "\"") });
                command.Parameters.Add(new SqliteParameter("@Type", DbType.Int32) { Value = error.Type });
                command.Parameters.Add(new SqliteParameter("@Message", DbType.String) { Value = error.Message?.Replace("'", "\"") });
                command.Parameters.Add(new SqliteParameter("@AdditionalInformation", DbType.String) { Value = Conversions.NothingToDBNULL(error.AdditionalInformation?.Replace("'", "\"")) });
                command.Parameters.Add(new SqliteParameter("@TrackingCode", DbType.String) { Value = Conversions.NothingToDBNULL(error.CodigoSeguimiento?.Replace("'", "\"")) });

                command.ExecuteNonQueryAsync();
            }
            finally
            {
                CerrarConexion();
            }

        }

        [Obsolete]
        public string SaveErrorlog(Exception Exception)
        {
            try
            {
                string Mensaje = Utilities.GenerateLineLog(Exception, ref CodigoSeguimiento);
                SaveErrorLog(new ErrorLogModel(AppConfiguration.ApiData, Exception, MessageType.Error, Mensaje, Exception.StackTrace, AppConfiguration.NonUserLog, CodigoSeguimiento));
            }
            catch (Exception ex)
            {
                WriteLogToFileAsync(ex);
            }

            AppConfiguration.TrackingCode = CodigoSeguimiento;
            return CodigoSeguimiento;
        }


        private async void WriteLogToFileAsync(Exception ex)
        {

            try
            {
                var filePath = $"{AppConfiguration.RutaLogFichero}\\{DateTime.UtcNow:yyyy-MM-dd}";
                var logEntry = $"{Environment.NewLine}{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} {this.CodigoSeguimiento} => {Utilities.GetExcepcion(ex)}";

                string directoryPath = Path.GetDirectoryName(filePath);

                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                await File.AppendAllTextAsync(filePath, logEntry);

            }
            catch (Exception)
            {

            }
        }
    }
}
