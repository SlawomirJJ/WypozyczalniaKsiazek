using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaKsiazek.services
{
    internal class BibliotekaDAO
    {
        readonly string connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Biblioteka; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
        public int DeleteUser(string PESEL)
        {
            int newPESEL = -1;
            string sqlStatement = "DELETE FROM dbo.Osoby WHERE PESEL=@PESEL";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@PESEL", PESEL);

                try
                {
                    connection.Open();
                    newPESEL = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return newPESEL;
            }
        }
        public int InsertUser(string PESEL, string Imie, string Nazwisko, string Stanowisko)
        {
            int newPESEL = -1;

            string sqlStatement = "INSERT INTO Osoby VALUES (@PESEL, @Imie, @Nazwisko, @Stanowisko);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@PESEL", PESEL);
                command.Parameters.AddWithValue("@Imie", Imie);
                command.Parameters.AddWithValue("@Nazwisko", Nazwisko);
                command.Parameters.AddWithValue("@Stanowisko", Stanowisko);

                try
                {
                    connection.Open();
                    newPESEL = Convert.ToInt32(command.ExecuteScalar());

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return newPESEL;
            }

        }

        public int UpdateUser(string PESEL, string Stanowisko)
        {
            int newPESEL = -1;

            string sqlStatement = "UPDATE Osoby SET Stanowisko = @Stanowisko WHERE PESEL=@PESEL;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@PESEL", PESEL);
                command.Parameters.AddWithValue("@Stanowisko", Stanowisko);

                try
                {
                    connection.Open();
                    newPESEL = Convert.ToInt32(command.ExecuteScalar());

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return newPESEL;
            }
        }

        public string SelectUser(string PESEL)
        {
            string stanowisko = null;

            string sqlStatement = "SELECT LTRIM(RTRIM(Stanowisko)) AS Stanowisko FROM Osoby WHERE PESEL=@PESEL;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@PESEL", PESEL);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        stanowisko = (string)reader[0];
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return stanowisko;
            }
        }

        //////    ZARZĄDZANIE KSIĘGOZBIOREM            ////////////////////////////////////////////

        public int InsertBook(string Tytul, string Autor, int Rok_wydania)
        {
            int newPESEL = -1;

            string sqlStatement = "INSERT INTO Ksiazki VALUES (@Tytul, @Autor, @Rok_wydania);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Tytul", Tytul);
                command.Parameters.AddWithValue("@Autor", Autor);
                command.Parameters.AddWithValue("@Rok_wydania", Rok_wydania);

                try
                {
                    connection.Open();
                    newPESEL = Convert.ToInt32(command.ExecuteScalar());

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return newPESEL;
            }
        }

        public int DeleteBook(int Id_ksiazki)
        {
            int newPESEL = -1;
            string sqlStatement = "DELETE FROM dbo.Ksiazki WHERE Id_ksiazki=@Id_ksiazki";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@Id_ksiazki", SqlDbType.Int);
                command.Parameters["@Id_ksiazki"].Value = Id_ksiazki;

                try
                {
                    connection.Open();
                    newPESEL = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return newPESEL;
            }
        }
        
        public string SelectBook(int Id_ksiazki)
        {
            string Id_Ksiazki = null;

            string sqlStatement = "SELECT TOP 1 Id_ksiazki FROM Ksiazki WHERE Id_ksiazki=@Id_ksiazki EXCEPT SELECT Id_ksiazki FROM Wypozyczenia Where Data_zwrotu is NULL";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@Id_ksiazki", SqlDbType.Int);
                command.Parameters["@Id_ksiazki"].Value = Id_ksiazki;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Id_ksiazki = (int)reader[0];
                        Id_Ksiazki = Id_ksiazki.ToString();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return Id_Ksiazki;
            }

        }

        public int InsertWypozyczenie(string Id_ksiazki, string PESEL)
        {
            int newPESEL = -1;

            string sqlStatement = "INSERT INTO Wypozyczenia (Id_osoby, Id_Ksiazki) VALUES(@PESEL, @Id_ksiazki);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@PESEL", PESEL);
                command.Parameters.Add("@Id_ksiazki", SqlDbType.Int);
                command.Parameters["@Id_ksiazki"].Value = Id_ksiazki;

                try
                {
                    connection.Open();
                    newPESEL = Convert.ToInt32(command.ExecuteScalar());

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return newPESEL;
            }
        }

        public int UpdateData_wypozyczenia(string Data_wypozyczenia, string Id_osoby, int Id_ksiazki)
        {
            int newPESEL = -1;

            string sqlStatement = "UPDATE Wypozyczenia SET Data_wypozyczenia = @Data_wypozyczenia WHERE Id_ksiazki=@Id_ksiazki AND Id_osoby=@Id_osoby AND Data_wypozyczenia IS NULL;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Id_osoby", Id_osoby);
                command.Parameters.Add("@Id_ksiazki", SqlDbType.Int);
                command.Parameters["@Id_ksiazki"].Value = Id_ksiazki;
                command.Parameters.AddWithValue("@Data_wypozyczenia", DateTimeOffset.Parse(Data_wypozyczenia));
                try
                {
                    connection.Open();
                    newPESEL = Convert.ToInt32(command.ExecuteScalar());

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return newPESEL;
            }
        }

        public int UpdateData_zwrotu(string Data_zwrotu, string Id_osoby, int Id_ksiazki)
        {
            int newPESEL = -1;

            string sqlStatement = "UPDATE Wypozyczenia SET Data_zwrotu = @Data_zwrotu WHERE Id_ksiazki=@Id_ksiazki AND Id_osoby=@Id_osoby AND Data_zwrotu IS NULL;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Id_osoby", Id_osoby);
                command.Parameters.Add("@Id_ksiazki", SqlDbType.Int);
                command.Parameters["@Id_ksiazki"].Value = Id_ksiazki;
                command.Parameters.AddWithValue("@Data_zwrotu", DateTimeOffset.Parse(Data_zwrotu));

                try
                {
                    connection.Open();
                    newPESEL = Convert.ToInt32(command.ExecuteScalar());

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return newPESEL;
            }
        }

        public struct Wypozyczenie
        {
            public int LiczbaDni;
            public string Stanowisko;
        }
        public List<Wypozyczenie> SprawdzZwroty(string PESEL)
        {
            List<Wypozyczenie> LiczbaDniIStanowisko = new List<Wypozyczenie>();

            string sqlStatement = "SELECT CASE WHEN Data_wypozyczenia IS NULL THEN 0 WHEN Data_wypozyczenia IS NOT NULL AND Data_zwrotu IS NULL THEN datediff(day, Data_wypozyczenia, GETDATE()) WHEN Data_wypozyczenia IS NOT NULL AND Data_zwrotu IS NOT NULL THEN datediff(day, Data_wypozyczenia, Data_zwrotu) END AS LiczbaDni,LTRIM(RTRIM(Stanowisko)) AS Stanowisko FROM Wypozyczenia RIGHT JOIN Osoby ON Wypozyczenia.Id_osoby = Osoby.PESEL WHERE PESEL = @PESEL; ";

            

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@PESEL", PESEL);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        LiczbaDniIStanowisko.Add(new Wypozyczenie { LiczbaDni = (int)reader[0], Stanowisko= (string)reader[1]});
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return LiczbaDniIStanowisko;
            }
        }



    }
}
