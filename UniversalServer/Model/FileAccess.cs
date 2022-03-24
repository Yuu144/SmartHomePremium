using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalServer.Model
{
    public delegate void SettingsSavedEventHandler(string msg);


    public class FileAccess
    {
        public event SettingsSavedEventHandler DataSaved;

        public void SaveSettings(string setting)
        {
            //Filestream öffnen
            //reinschreiben
            //zu machen
            //usw.......

            //Wenn das Schreiben geklappt hat, Meldung zurückgeben.
            DataSaved("Die Daten wurden erfolgreich gespeichert.");
        }
    }
}
