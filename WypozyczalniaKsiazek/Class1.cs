using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WypozyczalniaKsiazek.services;

namespace WypozyczalniaKsiazek
{
    public static class Biblioteka
    {
        public static void UsunUzytkownika(long PESEL)
        {
            BibliotekaDAO DAO = new BibliotekaDAO();
            DAO.Delete(PESEL);

        }

        public static void DodajUzytkownika(long PESEL, string Imie, string Nazwisko, string Stanowisko)
        {
            int dl = PESEL.ToString().Length;
            if(dl == 11)
            {
                BibliotekaDAO DAO = new BibliotekaDAO();
                DAO.Insert(PESEL, Imie, Nazwisko, Stanowisko);
            }
            else
                Console.WriteLine("Zły pesel!");
        }

        public static void AktualizujUzytkownika(long PESEL, string Stanowisko)
        {
            BibliotekaDAO DAO = new BibliotekaDAO();
            string obecneStanowisko=DAO.Select(PESEL);

            if (Stanowisko== "Wykładowca")
            {
                if (obecneStanowisko == "Student")
                {
                    DAO.Update(PESEL, Stanowisko);
                }
            }
            else if (Stanowisko =="Pracownik")
            {
                if (obecneStanowisko == "Student")
                {
                    DAO.Update(PESEL, Stanowisko);
                }
                else if(obecneStanowisko =="Wykładowca")
                {
                    DAO.Update(PESEL, Stanowisko);
                }
            }


           
        }
    }
}
