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
    public static class DALRooster
    {
        /**
         * door Joost en Davut
        **/
        public static List<RoosterDag> GetRoosterInfo()
        {
            List<RoosterInfo> infoLijst = GetRooster();
            return Transformeer(infoLijst);
        }
        /**
         * door Joost en Davut
        **/
        public static List<RoosterDag> GetRoosterInfo(int persoonid)
        {
            List<RoosterInfo> infoLijst = GetRooster(persoonid);
            return Transformeer(infoLijst);
        }

        /**
         * door Joost
         * haalt roosterInfo uit de database
        **/
        private static List<RoosterInfo> GetRooster()
        {
            List<RoosterInfo> infoLijst = new List<RoosterInfo>();
            //openen van sql connection
            SqlConnection conn = DALConnection.GetConnectionByName("Reader");
            conn.Open();

            //sql query klaarzetten
            string sql = "SELECT DagId,BlokId,CursusId,LocatieId FROM dbo.Rooster";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            sqlcmd.Prepare();

            //query uitvoeren en resultaat lezen
            SqlDataReader reader = sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                int dagId = reader.GetInt32(0);
                int blokId = reader.GetInt32(1);
                int cursusId = reader.GetInt32(2);
                int locatieId = reader.GetInt32(3);
                Cursus cursus = GetCursusById(cursusId);
                Locatie locatie = GetLocatieById(locatieId);
                RoosterInfo info = new RoosterInfo(dagId, blokId, locatie, cursus);
            }
            reader.Close();
            conn.Close();
            return infoLijst;
        }

        /**
         * door Joost
         * haalt roosterInfo uit de database op basis van persoonId
        **/
        private static List<RoosterInfo> GetRooster(int persoonid)
        {
            List<RoosterInfo> infoLijst = new List<RoosterInfo>();
            //openen van sql connection
            SqlConnection conn = DALConnection.GetConnectionByName("Reader");
            conn.Open();

            //sql query klaarzetten
            string sql = "SELECT r.DagId,r.BlokId,r.CursusId,r.LocatieId FROM dbo.Rooster r " +
                "JOIN dbo.Inschrijving i on r.CursusId = i.CursusId " +
                "WHERE i.PersoonId = 10";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            sqlcmd.Prepare();

            //query uitvoeren en resultaat lezen
            SqlDataReader reader = sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                int dagId = reader.GetInt32(0);
                int blokId = reader.GetInt32(1);
                int cursusId = reader.GetInt32(2);
                int locatieId = reader.GetInt32(3);
                Cursus cursus = GetCursusById(cursusId);
                Locatie locatie = GetLocatieById(locatieId);
                RoosterInfo info = new RoosterInfo(dagId, blokId, locatie, cursus);
            }
            reader.Close();
            conn.Close();
            return infoLijst;
        }

        /**
         * door Joost Kingma
         * cursus op ID
        **/
        public static Cursus GetCursusById(int curusId)
        {
            Cursus cursus = new Cursus();
            //openen van sql connection
            SqlConnection conn = DALConnection.GetConnectionByName("Reader");
            conn.Open();

            //sql query klaarzetten
            string sql = "SELECT Naam,Niveau,Toelichting,Categorie FROM dbo.Cursus " +
                "WHERE CursusId = @id";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);

            SqlParameter idParam = new SqlParameter("@id", SqlDbType.Int);
            idParam.Value = curusId;
            sqlcmd.Parameters.Add(idParam);
            sqlcmd.Prepare();

            //query uitvoeren en resultaat lezen
            SqlDataReader reader = sqlcmd.ExecuteReader();
            if (reader.Read())
            {
                string naam = reader.GetString(0);
                int niveau = reader.GetInt16(1);
                string toelichting = reader.GetString(2);
                DeelnemerCategorie categorie = (DeelnemerCategorie)reader.GetInt16(3);
                cursus = new Cursus(curusId, naam, niveau, toelichting, categorie);
            }
            reader.Close();
            conn.Close();
            return cursus;
        }

        /**
         * door Joost
         * haalt locatie op met id
        **/
        public static Locatie GetLocatieById(int locatieId)
        {
            Locatie locatie = new Locatie();
            //openen van sql connection
            SqlConnection conn = DALConnection.GetConnectionByName("Reader");
            conn.Open();

            //sql query klaarzetten
            string sql = "SELECT Gebouw, Zaal, Omschrijving FROM dbo.Locatie " +
                "WHERE LocatieId = @id";
            SqlCommand sqlcmd = new SqlCommand(sql, conn);

            SqlParameter idParam = new SqlParameter("@id", SqlDbType.Int);
            idParam.Value = locatieId;
            sqlcmd.Parameters.Add(idParam);
            sqlcmd.Prepare();

            //query uitvoeren en resultaat lezen
            SqlDataReader reader = sqlcmd.ExecuteReader();
            while (reader.Read())
            {
                string gebouw = reader.GetString(0);
                string zaal = reader.GetString(1);
                string omschrijving = reader.GetString(2);

                locatie = new Locatie(locatieId, gebouw, zaal, omschrijving);

            }
            reader.Close();
            conn.Close();

            return locatie;
        }


        private static List<RoosterDag> Transformeer(List<RoosterInfo> infoLijst)
        {
            List<RoosterDag> roosterDag = new List<RoosterDag>();


            for (int i = 0; i <= 6; i++)
            {
                roosterDag.Add(new RoosterDag(i + 1, new List<RoosterBlok>()));
                for (int j = 1; j <= 7; j++)
                {
                    roosterDag[i].Lijst.Add(new RoosterBlok(j, new List<RoosterItem>()));
                }
            }

            foreach(RoosterInfo info in infoLijst)
            {
                roosterDag[info.Dag].Lijst[info.Blok].Lijst.Add(new RoosterItem(info.Locatie, info.Cursus));
            }

            return roosterDag;
        }
    }
}
