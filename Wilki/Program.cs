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
            Wilk w1 = new Wilk("W1");
            Wilk w2 = new Wilk("W2");
            Wilk w3 = new Wilk("W3");
            w1.RunWilk();
            w2.RunWilk();
            w3.RunWilk();
        }
            
    }
}