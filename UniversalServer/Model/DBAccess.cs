using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace UniversalServer.Model
{
    public delegate EventHandler ErrorEventHandler(string msg);
    public class DBAccess
    {
        MySqlConnection sqlConnection;
        int idCount;

        public DBAccess()
        {
            sqlConnection = new MySqlConnection("server=localhost;database=smarthome;uid=root;pwd=;");
            sqlConnection.Open();
        }

        ///Diese Methode für den Datensatz in die Datenbank ein. Siehe Info-Pool      
        public void InsertData(TempValue t, HumidValue h, PressureValue p, DateTime dt, string ipa)
        {
            MySqlCommand mySqlCommand = new MySqlCommand("SELECT MAX(id) FROM wetterwerte", sqlConnection);

            if(mySqlCommand.ExecuteScalar().GetType() == typeof(int))
                idCount = (int)mySqlCommand.ExecuteScalar();

            idCount += 1;

            mySqlCommand.CommandType = System.Data.CommandType.Text;
            mySqlCommand.CommandText = "INSERT INTO wetterwerte(id, datetime, pressure, humidity, temperature, ipaddress)" + 
                                       "VALUES(@id, @datetime, @pressure, @humidity, @temperature, @ipaddress)";

            mySqlCommand.Parameters.AddWithValue("@id", idCount);
            mySqlCommand.Parameters.AddWithValue("@datetime", dt);
            mySqlCommand.Parameters.AddWithValue("@pressure", p.Value);
            mySqlCommand.Parameters.AddWithValue("@humidity", h.Value);
            mySqlCommand.Parameters.AddWithValue("@temperature", t.Value);
            mySqlCommand.Parameters.AddWithValue("@ipaddress", ipa);
            mySqlCommand.ExecuteNonQuery();
        }
    }
}
