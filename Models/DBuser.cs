using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace WebApplication5.Models
{
    public class DBuser
    {
        private readonly string connect = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=account;User ID=QQ;Password=123456;Trusted_Connection=True";
        public List<account> getAccounts()
        {
            List<account> accounts = new List<account>();

            SqlConnection sqlConnection = new SqlConnection(connect);
            SqlCommand sqlCommand = new SqlCommand("select * from account");
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();

            SqlDataReader Reader = sqlCommand.ExecuteReader();
            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    account account = new account()
                    {
                        fid = Reader.GetInt32(Reader.GetOrdinal("id")).ToString(),
                        UserId = Reader.GetString(Reader.GetOrdinal("userid")),
                        Passwd = Reader.GetString(Reader.GetOrdinal("passwd")),
                        UName = Reader.GetString(Reader.GetOrdinal("name")),
                    };
                    accounts.Add(account);
                }
            }
            else
            {
                Console.WriteLine("null");
            }
            sqlConnection.Close();
            return accounts;
        }
        public bool setAccounts(account user)
        {
            SqlConnection sqlconnection = new SqlConnection(connect);
            string Sql = "select * from account where userid='" + user.UserId + "'";
            SqlCommand sqlCommand = new SqlCommand(Sql);
            sqlCommand.Connection = sqlconnection;
            sqlconnection.Open();

            SqlDataReader Reader = sqlCommand.ExecuteReader();

            if (Reader.HasRows==false)
            {
                if (Reader.Read()==false)
                {
                    Reader.Close();

                    Sql = @"INSERT INTO account(userid,passwd,name) VALUES(@Userid,@Passwd,@Uname)";
                    SqlCommand sqlcommand = new SqlCommand(Sql);
                    sqlcommand.Connection = sqlconnection;

                    sqlcommand.Parameters.Add(new SqlParameter("@Userid", user.UserId));
                    sqlcommand.Parameters.Add(new SqlParameter("@Passwd", user.Passwd));
                    sqlcommand.Parameters.Add(new SqlParameter("@Uname", user.UName));

                    sqlcommand.ExecuteNonQuery();
                    sqlconnection.Close();
                    return true;
                }
            }
            return false;
        }
        public string Login(account user)
        {
            SqlConnection sqlConnection = new SqlConnection(connect);
            SqlCommand sqlCommand = new SqlCommand("select * from account where userid='" + user.UserId + "'");
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();

            SqlDataReader Reader = sqlCommand.ExecuteReader();
            if (Reader.HasRows)
            {
                if (Reader.Read())
                {
                    if (Reader["passwd"].ToString() == user.Passwd)
                    {
                        return Reader["name"].ToString();
                    }
                    else
                    {
                        return "No_Passwd";
                    }
                }
            }
            sqlConnection.Close();
            return "No_UserId";
        }
    }
}

            
