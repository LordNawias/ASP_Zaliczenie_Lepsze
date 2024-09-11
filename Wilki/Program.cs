using System;

namespace Wilki
{
    class Program
    {
        static void Main(string[] args)
        {
            ListaZajacy listaZajacy = new ListaZajacy();
            for(int i=0; i<20; i++)
            {
                Zajac zajac = new Zajac();
                zajac.runZajace();
            }
            Wilk w1 = new Wilk();
            Wilk w2 = new Wilk();
            Wilk w3 = new Wilk();
            w1.RunWilk();
            w2.RunWilk();
            w3.RunWilk();
        }
            
    }
}