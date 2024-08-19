using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecierosEC.Core.Utiliies
{
    public class ApiSettings
    {
        public Credentials Credentials { get; set; }
        public Auditaplications AuditAplications { get; set; }
        public LOG LOG { get; set; }
        public Errorlogsource ErrorLogSource { get; set; }
        public Nameapi NameApi { get; set; }
    }

    public class Credentials
    {
        public string ConectionDatabase { get; set; }
    }

    public class Auditaplications
    {
        public string Database { get; set; }
        public string RutaAuditFichero { get; set; }
        public string RutaAuditDatabase { get; set; }
    }

    public class LOG
    {
        public string Database { get; set; }
        public string RutaLogFichero { get; set; }
        public string RutaLogDatabase { get; set; }
    }

    public class Errorlogsource
    {
        public string ApiData { get; set; }
        public string NonUserLog { get; set; }
        public string Custom { get; set; }
    }

    public class Nameapi
    {
        public string Title { get; set; }
        public string Version { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TermsOfService { get; set; }
        public Contact Contact { get; set; }
    }

    public class Contact
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
    }
}