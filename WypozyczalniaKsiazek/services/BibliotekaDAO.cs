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
        public int DeleteUser(long PESEL)
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
        public int InsertUser(long PESEL, string Imie, string Nazwisko, string Stanowisko)
        {
            int newPESEL = -1;

            string sqlStatement = "INSERT INTO Osoby VALUES (@PESEL, @Imie, @Nazwisko, @Stanowisko);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@PESEL", SqlDbType.BigInt);
                command.Parameters["@PESEL"].Value = PESEL;
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

        public int UpdateUser(long PESEL, string Stanowisko)
        {
            int newPESEL = -1;

            string sqlStatement = "UPDATE Osoby SET Stanowisko = @Stanowisko WHERE PESEL=@PESEL;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@PESEL", SqlDbType.BigInt);
                command.Parameters["@PESEL"].Value = PESEL;
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

        public string SelectUser(long PESEL)
        {
            string stanowisko = null;

            string sqlStatement = "SELECT Stanowisko FROM Osoby WHERE PESEL=@PESEL;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@PESEL", SqlDbType.BigInt);
                command.Parameters["@PESEL"].Value = PESEL;

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

        public int InsertBook(string Tytul, string Autor, string Rok_wydania)
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
                command.Parameters.Add("@PESEL", SqlDbType.Int);
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

        public string SelectBook(string Tytul, string Autor)
        {
            string Id_ksiazki =null;

            string sqlStatement = "SELECT Id_ksiazki FROM Ksiazki LEFT JOIN Wypozyczenia ON ksiazki.Id_ksiazki = Wypozyczenia.Id_Ksiazki WHERE Ksiazki.Tytul=@Tytul AND Ksiazki.Autor=@Autor AND Wypozyczenia.Data_zwrotu IS NOT NULL";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters["@Tytul"].Value = Tytul;
                command.Parameters["@Autor"].Value = Autor;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Id_ksiazki = (string)reader[0];
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return Id_ksiazki;
            }

        }

        public int InsertWypozyczenie(string Id_ksiazki, long PESEL)
        {
            int newPESEL = -1;

            string sqlStatement = "INSERT INTO Wypozyczenia (Id_osoby, Id_Ksiazki) VALUES(@PESEL, @Id_ksiazki);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@PESEL", SqlDbType.BigInt);
                command.Parameters["@PESEL"].Value = PESEL;
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

        public int UpdateWypozyczenie(string Data_wypozyczenia, string Data_zwrotu, Id_osoby, Id_ksiazki )
        {
            int newPESEL = -1;

            string sqlStatement = "UPDATE Wypozyczenia SET Data_wypozyczenia = @Data_wypozyczenia, Data_zwrotu = @Data_zwrotu, WHERE Id_ksiazki=@Id_ksiazki AND Id_osoby=@Id_osoby AND (Data_wypozyczenia=NULL OR data_zwrotu=NULL; ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@PESEL", SqlDbType.BigInt);
                command.Parameters["@PESEL"].Value = PESEL;
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







    }
}
