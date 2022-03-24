using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UniversalServer.Model
{
    /// <summary>
    /// Dies ist ein Simulator, der die eingehenden Daten (Temperaturwerte, Feuchtigkeit usw.) 
    /// simuliert, die ansonsten vom ESP8266 kommen.
    /// </summary>
    public class ServerMockUp : IServerContract
    {

        public event StatusChangedEventHandler StatusPropertyChanged;
        public event MessageReceivedEventHandler MessageReceived;

        Timer _tmr;
        
        

        public void Start()
        {
            StatusPropertyChanged("Starting Server...");
            Thread.Sleep(500); //Verzögerung simulieren, wenn wir später auf echte Sockets gehen.

            //Timer starten, der uns zyklisch Werte liefert.
            _tmr = new Timer(new TimerCallback(TimerProc));
            _tmr.Change(1000, 2000);
            
            StatusPropertyChanged("Waiting for Connection...");
        }

        private void TimerProc(object state)
        {
            // The state object is the Timer object.
            Timer t = (Timer)state;
            //t.Dispose();

            //Protokoll to simulate: Temperatur;Luftfeuchte;Luftdruck;LUX;IR
            //String dataSend = String((_temperatur + _temp) / 2) + ";" + String(_humidity) + ";" + String(_press) + ";" + String(tsl.calculateLux(_full, _ir)) + ";" + String(_ir);
            Random rndm = new Random();

            double temp = 22 + rndm.NextDouble() - rndm.NextDouble();
            double hum = 50 + rndm.Next(-5, 5);
            int press = 1024 + rndm.Next(-20, 20);
            string ip = "192.168.1.145";

            string data =
                String.Format("{0:0.00}", temp) + ";" +
                String.Format("{0:00.00}", hum) + ";" +
                String.Format("{0:0000}", press) + ";" +
                ip;

            MessageReceived(data);
        }

    }
}
