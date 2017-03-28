using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Sparta.Model;

namespace Sparta.Dal
{
    public static class DALOverzicht
    {
        /**
         * door Joost Kingma
         * overzicht van cursussen
        **/
        public static List<Cursus> GetCursussen()
        {
            List<Cursus> curussen = new List<Cursus>();
            //openen van sql connection 
            SqlConnection conn = DALConnection.GetConnectionByName("Reader");
            conn.Open();

            //sql query klaarzetten
            string sql = "SELECT CursusId,Naam,Niveau,Toelichting,Categorie FROM dbo.Cursus";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            sqlcmd.Prepare();

            //query uitvoeren en resultaat lezen
            SqlDataReader reader = sqlcmd.ExecuteReader();
            while(reader.Read())
            {
                int Cursusid = reader.GetInt32(0);
                string naam = reader.GetString(1);
                int niveau = reader.GetInt16(2);
                string toelichting = reader.GetString(3);
                DeelnemerCategorie categorie = (DeelnemerCategorie)reader.GetInt16(4);
                Cursus cursus = new Cursus(Cursusid, naam, niveau, toelichting,categorie);
                curussen.Add(cursus);
            }
            reader.Close();
            conn.Close();
            return curussen;
        }
    }
}
