﻿using System;
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
        // Door: Davut Demir
        // Haalt alle informatie van de locaties in de database en stuurt deze door naar de applicatie
        public static List<Locatie> GetLocaties()
        {
            // de sql query die gebruikt wordt om alle informatie te vinden
            string sqlLocatie = "SELECT LocatieId, Gebouw, Zaal, Omschrijving FROM dbo.Locatie";

            // creëert een connectie met de database
            SqlConnection connection = DALConnection.GetConnectionByName("Reader");

            // hier opent hij de database
            connection.Open();

            // vuurt een query af op de database
            SqlCommand sqlCmnd = new SqlCommand(sqlLocatie, connection);

            // met deze functie legt hij de query klaar voor gebruik
            sqlCmnd.Prepare();

            // met de ExecuteReader haalt hij records uit de database
            SqlDataReader dataReader = sqlCmnd.ExecuteReader();

            // hier stoppen we alle informatie in die we krijgen uit de database
            List<Locatie> lijstLocaties = new List<Locatie>();

            while (dataReader.Read())
            {
                // zet de informatie in de database om in informatie die C# wel kan lezen
                int LocatieId = dataReader.GetInt32(0);
                string Gebouw = dataReader.GetString(1);
                string Zaal = dataReader.GetString(2);
                string Omschrijving = dataReader.GetString(3);

                // daarna maakt het hier een Locatie variabele ervan met de geconverteerde informatie
                Locatie locatie = new Locatie(LocatieId, Gebouw, Zaal, Omschrijving);

                // en daarna stopt de locatie variabele in de List
                lijstLocaties.Add(locatie);
            }
            connection.Close();
            dataReader.Close();

            return lijstLocaties;
        }

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
            while (reader.Read())
            {
                int Cursusid = reader.GetInt32(0);
                string naam = reader.GetString(1);
                int niveau = reader.GetInt16(2);
                string toelichting = reader.GetString(3);
                DeelnemerCategorie categorie = (DeelnemerCategorie)reader.GetInt16(4);
                Cursus cursus = new Cursus(Cursusid, naam, niveau, toelichting, categorie);
                curussen.Add(cursus);
            }
            reader.Close();
            conn.Close();
            return curussen;
        }
        public static List<Persoon> GetPersonen()
        {
            SqlConnection connection = DALConnection.GetConnectionByName("Reader");
            connection.Open();

            string infoPersoon = "SELECT PersoonId, Naam, Achternaam, Categorie, GeboorteDatum" +
                                 " FROM Persoon";

            SqlCommand command = new SqlCommand(infoPersoon, connection);
            SqlDataReader reader = command.ExecuteReader();

            List<Persoon> list = new List<Persoon>();

            while (reader.Read())
            {
                int PersoonId = reader.GetInt32(0);
                string Naam = reader.GetString(1);
                string Achternaam = reader.GetString(2);
                DeelnemerCategorie Categorie = (DeelnemerCategorie)reader.GetInt16(3);
                DateTime GeboorteDatum = reader.GetDateTime(4);

                Persoon pers = new Persoon(PersoonId, Naam, Achternaam, GeboorteDatum, Categorie);
                list.Add(pers);
            }

            connection.Close();
            return list;
        }

    }
}
