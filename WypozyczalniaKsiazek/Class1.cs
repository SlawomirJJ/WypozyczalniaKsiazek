using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WypozyczalniaKsiazek.services;

namespace WypozyczalniaKsiazek
{
    // Daty zapisujemy "YYYY-MM-DD"
    public static class Biblioteka
    {
        public static void UsunUzytkownika(string PESEL)
        {
            BibliotekaDAO DAO = new BibliotekaDAO();
            DAO.DeleteUser(PESEL);

        }

        public static void DodajUzytkownika(string PESEL, string Imie, string Nazwisko, string Stanowisko)
        {
            int dl = PESEL.Length;
            bool flag=false;
            if (dl==11)
            {
                for (int i = 0; i < 11; i++)
                {
                    if (!(PESEL[i] >= '0' && PESEL[i] <= '9'))
                    {
                        flag = true;
                    }
                }
                if (flag==false)
                {
                    BibliotekaDAO DAO = new BibliotekaDAO();
                    DAO.InsertUser(PESEL, Imie, Nazwisko, Stanowisko);
                }
                else
                    Console.WriteLine("Zły pesel!");
            }
            else
                Console.WriteLine("Zły pesel!");
        }

        public static void AktualizujUzytkownika(string PESEL, string Stanowisko)
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

        public static void WyporzyczKsiazke(int Id_ksiazki, string PESEL)
        {
            BibliotekaDAO DAO = new BibliotekaDAO();
            string Id_Ksiazki = DAO.SelectBook(Id_ksiazki);
            if (Id_Ksiazki != null)
            {
                DAO.InsertWypozyczenie(Id_Ksiazki, PESEL);
            }

        }

        public static void UpdateDataWypozyczenia(int Id_ksiazki,string Data_wypozyczenia, string PESEL)
        {
            BibliotekaDAO DAO = new BibliotekaDAO();
            DAO.UpdateData_wypozyczenia(Data_wypozyczenia, PESEL, Id_ksiazki);
        }

        public static void UpdateDataZwrotu(int Id_ksiazki, string Data_zwrotu, string PESEL)
        {
            BibliotekaDAO DAO = new BibliotekaDAO();
            DAO.UpdateData_zwrotu(Data_zwrotu, PESEL, Id_ksiazki);
        }



        public static int ObliczKare(string PESEL)
        {
            BibliotekaDAO DAO = new BibliotekaDAO();
            int kara=0;
            const int CZASNAODDANIE = 30; // w dniach
            var LiczbaDniIStanowisko = DAO.SprawdzZwroty(PESEL);
            string Stanowisko = LiczbaDniIStanowisko[0].Stanowisko;


            for (int i = 0; i < LiczbaDniIStanowisko.Count; i++)
            {
                int LiczbaDni = LiczbaDniIStanowisko[i].LiczbaDni;               

                if (LiczbaDni > CZASNAODDANIE)
                {
                    LiczbaDni = LiczbaDni- CZASNAODDANIE;
                    if (Stanowisko == "Wykładowca")
                    {
                        for (int j = 1; j <= LiczbaDni; j++)
                        {
                            if (j > 3 && j <= 14)
                            {
                                kara = kara + 2;
                            }
                            if (j > 14 && j <= 28)
                            {
                                kara = kara + 5;
                            }
                            if (j > 28)
                            {
                                kara = kara + 10;
                            }
                        }
                    }
                    else if (Stanowisko == "Student")
                    {
                        for (int j = 1; j <= LiczbaDni; j++)
                        {
                            if (j <= 7)
                            {
                                kara = kara + 1;
                            }
                            if (j > 7 && j <= 14)
                            {
                                kara = kara + 2;
                            }
                            if (j > 14 && j <= 28)
                            {
                                kara = kara + 5;
                            }
                            if (j > 28)
                            {
                                kara = kara + 10;
                            }
                        }
                    }
                    else if (Stanowisko == "Pracownik")
                    {
                        for (int j = 1; j <= LiczbaDni; j++)
                        {
                            if (j > 28)
                            {
                                kara = kara + 5;
                            }
                        }

                    }
                }
            }
            return kara;
        }






    }
}
