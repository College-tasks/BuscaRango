using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscaRangoCode
{
    public class ItemHistorico
    {
        private string caracteristica;
        public string Caracteristica
        {
            get { return caracteristica; }
            set { caracteristica = value; }
        }

        private int nota;
        public int Nota
        {
            get { return nota; }
            set { nota = value; }
        }

        private string img;
        public string Img
        {
            get { return img; }
            set { img = value; }
        }

        private DateTime timeStamp;
        public DateTime TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }

        private bool isPrato;
        public bool IsPrato
        {
            get { return isPrato; }
            set { isPrato = value; }
        }

        private string nome;
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
    }
}
