using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UniversalServer.Model
{
    public delegate EventHandler ErrorEventHandler(string msg);
    public class DBAccess
    {
        MySqlConnection mySqlConnection;
        //int idCount;

        public DBAccess()
        {
            mySqlConnection = new MySqlConnection("server=localhost;database=smarthome;uid=root;pwd=;");
            mySqlConnection.Open();
        }

        ///Diese Methode für den Datensatz in die Datenbank ein. Siehe Info-Pool      
        public void InsertData(TempValue t, HumidValue h, PressureValue p, DateTime dt, string ipa)
        {
            MySqlCommand mySqlCommand = new MySqlCommand("SELECT MAX(id) FROM wetterwerte", mySqlConnection);

            /*
            if (mySqlCommand.ExecuteScalar().GetType() != typeof(int))
                idCount = 1;

            else
            {
                idCount = (int)mySqlCommand.ExecuteScalar();
                idCount++;
            }
            */

            mySqlCommand.CommandType = System.Data.CommandType.Text;
            mySqlCommand.CommandText = "INSERT INTO wetterwerte(temperature, pressure, humidity, ipaddress)" +
                                       "VALUES(@temperature, @pressure, @humidity, @ipaddress)";

            //mySqlCommand.Parameters.AddWithValue("@id", idCount);
            //mySqlCommand.Parameters.AddWithValue("@datetime", dt);
            mySqlCommand.Parameters.AddWithValue("@temperature", t.Value);
            mySqlCommand.Parameters.AddWithValue("@pressure", p.Value);
            mySqlCommand.Parameters.AddWithValue("@humidity", h.Value);
            mySqlCommand.Parameters.AddWithValue("@ipaddress", ipa);
            mySqlCommand.ExecuteNonQuery();
        }
    }
}
