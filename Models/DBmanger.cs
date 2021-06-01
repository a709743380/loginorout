using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace WebApplication5.Models
{
    public class DBmanger
    {
        private readonly string connect = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=manager;User ID=QQ;Password=123456;Trusted_Connection=True";
        public List<account> getAccounts()
        {
            List<account> Maccounts = new List<account>();

            SqlConnection sqlConnection = new SqlConnection(connect);
            SqlCommand sqlCommand = new SqlCommand("select * from account");
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();

            SqlDataReader Reader = sqlCommand.ExecuteReader();
            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    account Maccount = new account()
                    {
                        fid = Reader.GetInt32(Reader.GetOrdinal("id")).ToString(),
                        UserId = Reader.GetString(Reader.GetOrdinal("userid")),
                        Passwd = Reader.GetString(Reader.GetOrdinal("passwd")),
                        UName = Reader.GetString(Reader.GetOrdinal("name")),
                    };
                    Maccounts.Add(Maccount);
                }
            }
            else
            {
                Console.WriteLine("null");
            }
            sqlConnection.Close();
            return Maccounts;
        }
        public void setAccounts(account user)
        {
            SqlConnection sqlconnection = new SqlConnection(connect);
            string Sql = @"INSERT INTO account(userid,passwd,name) VALUES(@username,@passwd,@name)";
            SqlCommand sqlcommand = new SqlCommand(Sql);
            sqlcommand.Connection = sqlconnection;

            sqlcommand.Parameters.Add(new SqlParameter("@username", user.UserId));
            sqlcommand.Parameters.Add(new SqlParameter("@passwd", user.Passwd));
            sqlcommand.Parameters.Add(new SqlParameter("@name", user.UName));

            sqlconnection.Open();
            sqlcommand.ExecuteNonQuery();
            sqlconnection.Close();

        }
    }
}

            
