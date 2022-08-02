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

        public string SelectBook(string Tytul, string Autor)
        {
            string Id_ksiazki =null;

            string sqlStatement = "SELECT TOP 1 Id_ksiazki FROM Ksiazki WHERE Tytul=@Tytul AND Autor=@Autor EXCEPT SELECT Id_ksiazki FROM Wypozyczenia Where Data_zwrotu is NULL";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Tytul", Tytul);
                command.Parameters.AddWithValue("@Autor", Autor);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int Id_Ksiazki = (int)reader[0];
                        Id_ksiazki = Id_Ksiazki.ToString();
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

        public int UpdateData_wypozyczenia(string Data_wypozyczenia, long Id_osoby, int Id_ksiazki)
        {
            int newPESEL = -1;

            string sqlStatement = "UPDATE Wypozyczenia SET Data_wypozyczenia = @Data_wypozyczenia WHERE Id_ksiazki=@Id_ksiazki AND Id_osoby=@Id_osoby;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@Id_osoby", SqlDbType.BigInt);
                command.Parameters["@Id_osoby"].Value = Id_osoby;
                command.Parameters.Add("@Id_ksiazki", SqlDbType.Int);
                command.Parameters["@Id_ksiazki"].Value = Id_ksiazki;
                //command.Parameters.Add("@Data_wypozyczenia", SqlDbType.Date);
                //command.Parameters["@Data_wypozyczenia"].Value = Data_wypozyczenia.ToString();
                //command.Parameters.AddWithValue("@Data_wypozyczenia",  Data_wypozyczenia);
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

        public int UpdateData_zwrotu(string Data_zwrotu, long Id_osoby, int Id_ksiazki)
        {
            int newPESEL = -1;

            string sqlStatement = "UPDATE Wypozyczenia SET Data_zwrotu = @Data_zwrotu WHERE Id_ksiazki=@Id_ksiazki AND Id_osoby=@Id_osoby;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.Add("@Id_osoby", SqlDbType.BigInt);
                command.Parameters["@Id_osoby"].Value = Id_osoby;
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





    }
}
