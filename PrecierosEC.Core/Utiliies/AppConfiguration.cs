namespace PrecierosEC.Core.Utiliies
{
    public class AppConfiguration
    {

        //name api execute
        public static string NameApi_Title { get; set; }
        public static string NameApi_Version { get; set; }
        public static string NameApi_Name { get; set; }
        public static string NameApi_Description { get; set; }
        public static string NameApi_TermsOfService { get; set; }
        public static string NameApi_ContactName { get; set; }
        public static string NameApi_ContactEmail { get; set; }
        public static string NameApi_ContactUrl { get; set; }


        public static string ConnectionString { get; set; }
        public static string ConnectionLogsString { get; set; }
        public static string RutaLogFichero { get; set; }
        public static string RutaLogDatabase { get; set; }
        public static string ConnectionAuditsString { get; set; }
        public static string RutaAuditFichero { get; set; }
        public static string RutaAuditDatabase { get; set; }




        public static string ApiData { get; set; }
        public static string NonUserLog { get; set; }
        public static string Custom { get; set; }
        
        public static string TrackingCode { get; set; }
    }
}
