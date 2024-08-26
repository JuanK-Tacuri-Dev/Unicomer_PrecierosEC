namespace PrecierosEC.Core.Utiliies
{
    public static class ApplicationService
    {
        public static void Configure(string nameFile)
        {
            string mensaje = null;

            var settings = Utilities.LeerAppSettings<ApiSettings>(typeof(ApiSettings), ref mensaje, nameFile);

            AppConfiguration.NameApi_Title = settings.NameApi.Title;
            AppConfiguration.NameApi_Version = settings.NameApi.Version;
            AppConfiguration.NameApi_Name = settings.NameApi.Name;
            AppConfiguration.NameApi_Description = settings.NameApi.Description;
            AppConfiguration.NameApi_TermsOfService = settings.NameApi.TermsOfService;
            AppConfiguration.NameApi_ContactName = settings.NameApi.Contact.Name;
            AppConfiguration.NameApi_ContactEmail = settings.NameApi.Contact.Email;
            AppConfiguration.NameApi_ContactUrl = settings.NameApi.Contact.Url;


            AppConfiguration.ApiData = settings.ErrorLogSource.ApiData;
            AppConfiguration.NonUserLog = settings.ErrorLogSource.NonUserLog;
            AppConfiguration.Custom = settings.ErrorLogSource.Custom;

            AppConfiguration.ConnectionString = ConcectionString(settings.Credentials.ConectionDatabase);
            AppConfiguration.NameDatabaseLogs = settings.LOG.Database;
            AppConfiguration.NameDatabaseAudit = settings.AuditAplications.Database;
            
            AppConfiguration.RutaLogDatabase = settings.LOG.RutaLogDatabase;
            AppConfiguration.RutaLogFichero = settings.LOG.RutaLogFichero;

            AppConfiguration.RutaAuditDatabase = settings.AuditAplications.RutaAuditDatabase;
            AppConfiguration.RutaAuditFichero = settings.AuditAplications.RutaAuditFichero;

        }

        private static string ConcectionString(string Conectionstring)=>DbConnectionHelper.fnDesencripta(Conectionstring);
    }
}
