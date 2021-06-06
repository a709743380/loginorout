using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public class DB_person
    {
        private readonly string connect = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=account;User ID=QQ;Password=123456;Trusted_Connection=True";
        public List<Person> getinformation()
        {
            List<Person> information = new List<Person>();

            SqlConnection sqlConnection = new SqlConnection(connect);
            SqlCommand sqlCommand = new SqlCommand("select * from [dbo].[SetUser]");
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();

            SqlDataReader Reader = sqlCommand.ExecuteReader();
            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    Person person = new Person()
                    {
                        NewAddress = Reader.GetString(Reader.GetOrdinal("Address")),
                        User_tel = Reader.GetString(Reader.GetOrdinal("tel")),
                    };
                    information.Add(person);
                }
            }
            else
            {
                Console.WriteLine("null");
            }
            sqlConnection.Close();
            return information;
        }
        public void SetUserid(string userid)
        {
            SqlConnection sqlconnection = new SqlConnection(connect);
            string Sql = "select * from [dbo].[SetUser] where userid='" + userid + "'";
            SqlCommand sqlCommand = new SqlCommand(Sql);
            sqlCommand.Connection = sqlconnection;
            sqlconnection.Open();

            SqlDataReader Reader = sqlCommand.ExecuteReader();

            if (Reader.HasRows == false)
            {
                if (Reader.Read() == false)
                {
                    Reader.Close();

                    Sql = @"INSERT INTO [dbo].[SetUser](userid)
                            VALUES(@Userid)";
                    SqlCommand sqlcommand = new SqlCommand(Sql);
                    sqlcommand.Connection = sqlconnection;

                    sqlcommand.Parameters.Add(new SqlParameter("@Userid", userid));
                    sqlcommand.ExecuteNonQuery();
                    sqlconnection.Close();
                }
            }

        }
        public void Setinf(Person person)//更新
        {
            SqlConnection sqlconnection = new SqlConnection(connect);
            string Sql = "select * from [dbo].[SetUser] where userid='" + person.userid + "'";
            SqlCommand sqlCommand = new SqlCommand(Sql);
            sqlconnection.Open();
            sqlCommand.Connection = sqlconnection;
            SqlDataReader Reader = sqlCommand.ExecuteReader();
            if (Reader.HasRows)
            {
                if (Reader.Read())
                {
                       Reader.Close();
                    if (person.NewAddress != null)
                    {
                        Sql = $"UPDATE [dbo].[SetUser] SET Address=N'{person.NewAddress}' WHERE userid='{person.userid}'";
                        SqlCommand sqlcommand1 = new SqlCommand(Sql);
                        sqlcommand1.Connection = sqlconnection;
                        sqlcommand1.ExecuteNonQuery();
                    
                    }
                    if (person.User_tel != null)
                    {
                        Sql = $"UPDATE [dbo].[SetUser] SET tel=N'{person.User_tel}' WHERE userid='{person.userid}'";
                        SqlCommand sqlcommand2 = new SqlCommand(Sql);
                        sqlcommand2.Connection = sqlconnection;
                        sqlcommand2.ExecuteNonQuery();
                    }
                    sqlconnection.Close();
                }
            }
        }
    }
}
