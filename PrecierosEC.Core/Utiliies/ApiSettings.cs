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
        public LOG LOG { get; set; }
        public Errorlogsource ErrorLogSource { get; set; }
        public Nameapi NameApi { get; set; }
    }

    public class Credentials
    {
        public bool Habilitar { get; set; }
        public int DefaultConexion { get; set; }
        public int Destino { get; set; }
        public Variablelocal VariableLocal { get; set; }
        public Appsettings Appsettings { get; set; }
    }

    public class Variablelocal
    {
        public string Conection { get; set; }
    }

    public class Appsettings
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Pwd { get; set; }
    }

    public class LOG
    {
        public bool Habilitar { get; set; }
        public int DefaultLogs { get; set; }
        public int Destino { get; set; }
        public Database DataBase { get; set; }
    }

    public class Database
    {
        public Credentials1 Credentials { get; set; }
    }

    public class Credentials1
    {
        public bool Habilitar { get; set; }
        public int DefaultConexion { get; set; }
        public int Destino { get; set; }
        public Variablelocal1 VariableLocal { get; set; }
        public Appsettings1 Appsettings { get; set; }
    }

    public class Variablelocal1
    {
        public string Conection { get; set; }
    }

    public class Appsettings1
    {
        public string Provider { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Pwd { get; set; }
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
