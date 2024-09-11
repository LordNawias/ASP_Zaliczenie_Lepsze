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

        public int findZajac(Zajac zajac)
        {
            return listaZajacy.FindIndex(f => f.koordynaty.getKoordynaty() == zajac.koordynaty.getKoordynaty());
        }

        public void removeZajac(int index)
        {
            listaZajacy.RemoveAt(index);
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
