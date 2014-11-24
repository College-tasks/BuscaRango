using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Xml.Linq;
    
namespace BuscaRangoCode
{
    public class GoogleService
    {
        private string latitude = "";
        private string Longitude = "";

        public void setLongitude(string pLongitude)
        {
            this.Longitude = pLongitude;
        }

        public string getLongitude()
        {
            return this.Longitude;
        }

        public void setLatitude(string platitude)
        {
            this.latitude = platitude;
        }

        public string getLatitude()
        {
            return this.latitude;
        }

        public void PegarLatitudeLongitude(string endereco)
        {

            //Criando as variáveis que irá receber os valores
            string latitude = "0";
            string longitude = "0";

            try
            {
                string APIUrlLatLongFromAddress = "http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false";
                XDocument doc = XDocument.Load(string.Format(APIUrlLatLongFromAddress, endereco));
                var els = doc.Descendants("result").Descendants("geometry").Descendants("location").First();

                if (null != els)
                {
                    latitude = (els.Nodes().First() as XElement).Value;
                    this.latitude = latitude;
                    longitude = (els.Nodes().ElementAt(1) as XElement).Value;
                    this.Longitude = longitude;
                }
            }
            catch (Exception ex)
            { }
        }
    }
}
