using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Sparta.Model;
using System.IO;

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
    }
}
