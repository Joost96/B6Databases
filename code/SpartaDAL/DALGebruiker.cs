using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Sparta.Model;

namespace Sparta.Dal
{
    public static class DALGebruiker
    {
        public static Persoon checkCredentials(Login user)
        {
            return new Persoon();
        }

        public static void RegistreerPersoon(DeelnemerCategorie categorie,
            string naam, string achternaam,DateTime gebdatum, Login aanmeld)
        {

        }

        public static void UpdatePwd(int loginid, string pwdhash)
        {

        }

        public static int GetLoginId(int persoonid, string pwdhash)
        {
            return 0;
        }

        public static void voegtoeContactInfo(Contact info)
        {

        }

        public static void vernieuwContactInfo(Contact info)
        {

        }
    }
}
