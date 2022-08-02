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
            DAO.DeleteUser(PESEL);

        }

        public static void DodajUzytkownika(long PESEL, string Imie, string Nazwisko, string Stanowisko)
        {
            int dl = PESEL.ToString().Length;
            if(dl == 11)
            {
                BibliotekaDAO DAO = new BibliotekaDAO();
                DAO.InsertUser(PESEL, Imie, Nazwisko, Stanowisko);
            }
            else
                Console.WriteLine("Zły pesel!");
        }

        public static void AktualizujUzytkownika(long PESEL, string Stanowisko)
        {
            BibliotekaDAO DAO = new BibliotekaDAO();
            string obecneStanowisko=DAO.SelectUser(PESEL);

            if (Stanowisko== "Wykładowca")
            {
                if (obecneStanowisko == "Student")
                {
                    DAO.UpdateUser(PESEL, Stanowisko);
                }
            }
            else if (Stanowisko =="Pracownik")
            {
                if (obecneStanowisko == "Student")
                {
                    DAO.UpdateUser(PESEL, Stanowisko);
                }
                else if(obecneStanowisko =="Wykładowca")
                {
                    DAO.UpdateUser(PESEL, Stanowisko);
                }
            }
            else
            {
                Console.WriteLine("Nie można dokonać zmiany.");
            }         
        }

        //      ZARZĄDZANIE KSIĘGOZBIOREM       //
        public static void DodajKsiazke(string Tytul, string Autor, int Rok_wydania)
        {
            BibliotekaDAO DAO = new BibliotekaDAO();
            DAO.InsertBook(Tytul, Autor, Rok_wydania);
        }

        public static void UsunKsiazke(int Id_ksiazki)
        {
            BibliotekaDAO DAO = new BibliotekaDAO();
            DAO.DeleteBook(Id_ksiazki);
        }


        public static void WyporzyczKsiazke(string Tytul, string Autor,long PESEL)
        {
            BibliotekaDAO DAO = new BibliotekaDAO();
            string Id_ksiazki = DAO.SelectBook(Tytul, Autor);
            if (Id_ksiazki != null)
            {
                DAO.InsertWypozyczenie(Id_ksiazki, PESEL);
            }
            
        }

        public static void UpdateDataWypozyczenia(int Id_ksiazki,string Data_wypozyczenia, long PESEL)
        {
            BibliotekaDAO DAO = new BibliotekaDAO();
            DAO.UpdateData_wypozyczenia(Data_wypozyczenia, PESEL, Id_ksiazki);
        }

        public static void UpdateDataZwrotu(int Id_ksiazki, string Data_zwrotu, long PESEL)
        {
            BibliotekaDAO DAO = new BibliotekaDAO();
            DAO.UpdateData_zwrotu(Data_zwrotu, PESEL, Id_ksiazki);
        }
    }
}
