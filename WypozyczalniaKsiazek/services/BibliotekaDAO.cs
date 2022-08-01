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
        public int Delete(long PESEL)
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
        public int Insert(long PESEL, string Imie, string Nazwisko, string Stanowisko)
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

        public int Update(long PESEL,string Stanowisko)
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

        public string Select(long PESEL)
        {
            string stanowisko=null;

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
                    while(reader.Read())
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
    }
}
