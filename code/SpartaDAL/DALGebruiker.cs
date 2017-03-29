using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Sparta.Model;
using System.Data;

namespace Sparta.Dal
{
    public static class DALGebruiker
    {
        /**
         * door Joost
         * het controlleren van credentials met de persoon als return value
        **/
        public static Persoon checkCredentials(Login user)
        {
            Persoon persoon;
            int id = getPersoonId(user);
            //openen van sql connection
            SqlConnection conn = DALConnection.GetConnectionByName("Reader");
            conn.Open();

            //sql query klaarzetten
            string sql = "SELECT PersoonId,Naam,Achternaam,Categorie,GeboorteDatum " +
                "FROM dbo.Persoon WHERE PersoonId = @id";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            SqlParameter idParam = new SqlParameter("@id", SqlDbType.Int);
            idParam.Value = id;
            sqlcmd.Parameters.Add(idParam);
            sqlcmd.Prepare();

            //query uitvoeren en resultaat lezen
            SqlDataReader reader = sqlcmd.ExecuteReader();
            if (reader.Read())
            {
                int persoonId = reader.GetInt32(0);
                string naam = reader.GetString(1);
                string achternaam = reader.GetString(2);
                DeelnemerCategorie categorie = (DeelnemerCategorie) reader.GetInt16(3);
                DateTime gebDatum = reader.GetDateTime(4);
                persoon = new Persoon(persoonId, naam, achternaam, gebDatum, categorie);
            } else
            {
                throw new Exception("Persoon not found");
            }
            reader.Close();
            conn.Close();
            return persoon;
        }

        /**
         * door Joost
        **/
        private static int getPersoonId(Login user)
        {
            int id = 0;
            //open connection
            SqlConnection conn = DALConnection.GetConnectionByName("Reader");
            conn.Open();
            
            //set up sql query
            string sql = "SELECT PersoonId FROM dbo.Login " +
                "WHERE AanmeldNaam = @naam AND pwdhash = @pwd";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);

            //maken parameters
            SqlParameter naamParam = new SqlParameter("@naam", SqlDbType.NVarChar, 50);
            SqlParameter pwdParam = new SqlParameter("@pwd", SqlDbType.NChar, 32);

            //waarde geven aan parameters
            naamParam.Value = user.Naam;
            pwdParam.Value = user.Pwdhash;

            //parameters toevoegen
            sqlcmd.Parameters.Add(naamParam);
            sqlcmd.Parameters.Add(pwdParam);
            sqlcmd.Prepare();

            SqlDataReader reader = sqlcmd.ExecuteReader();
            if(reader.Read())
            {
                id = reader.GetInt32(0);
            } else
            {
                throw new Exception("Login not found");
            }

            reader.Close();
            conn.Close();
            return id;
        }

        public static void RegistreerPersoon(DeelnemerCategorie categorie,
            string naam, string achternaam,DateTime gebdatum, Login aanmeld)
        {
            int persoonId = voegPersoonToe(categorie, naam, achternaam, gebdatum);
            voegLoginToe(persoonId, aanmeld);
        }
        private static int voegPersoonToe(DeelnemerCategorie categorie,
            string naam, string achternaam, DateTime gebdatum)
        {
            int id = 0;
            SqlConnection conn = DALConnection.GetConnectionByName("Reader");
            conn.Open();

            string sql = "INSERT INTO dbo.Persoon " + 
                "(Naam, Achternaam, Categorie, GeboorteDatum) VALUES" + 
                "(@naam, @achternaam, @categorie, @GeboorteDatum)";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);

            //reader.Close();
            conn.Close();
            return id;
        }
        private static void voegLoginToe(int persoonId ,Login aanmeld)
        {

        }

        public static void UpdatePwd(int loginid, string pwdhash)
        {
            string updateQuery = "UPDATE dbo.Login SET PwdHash = " + pwdhash + " WHERE LoginId = " + loginid;
            SqlConnection sqlConn = DALConnection.GetConnectionByName("Writer");
            sqlConn.Open();
            SqlCommand sqlCmnd = new SqlCommand(updateQuery, sqlConn);
            sqlCmnd.Prepare();

            sqlCmnd.ExecuteNonQuery();

            sqlConn.Close();
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
