using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wilki
{
    class ListaZajacy
    {
        public static List<Zajac> listaZajacy = new List<Zajac>();
        Random random = new Random();
        public ListaZajacy() { }

        public void dodajZajaca(Zajac zajac)
        {
            listaZajacy.Add(zajac);
        }

        public int iloscZajacy()
        {
            return listaZajacy.Count;
        }

        public int randomZajac()
        {
            int tmp = random.Next(listaZajacy.Count);
            return tmp;
        }
        public void removeZajac(Zajac zajac)
        {
            listaZajacy.Remove(zajac);
        }

        public Zajac returnZajac(int index)
        {
            return listaZajacy[index];
        }

        public List<Zajac> lista()
        {
            return listaZajacy;
        }
    }
}
