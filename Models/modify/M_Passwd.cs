using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{

    public class M_Passwd
    {
        private readonly string connect = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=manager;User ID=QQ;Password=123456;Trusted_Connection=True";
        public void modify_passwd(Modify Muser)
        {
            SqlConnection sqlconnection = new SqlConnection(connect);
            string Sql = "select * from account where userid='" + Muser.UserId + "'";
            SqlCommand sqlCommand = new SqlCommand(Sql);
            sqlconnection.Open();
            sqlCommand.Connection = sqlconnection;
            SqlDataReader Reader = sqlCommand.ExecuteReader();
            if (Reader.HasRows)
            {
                if (Reader.Read())
                {
                    if( Reader["passwd"].ToString()== Muser.Oldpasswd)
                    {
                        Reader.Close();
                        Sql = $"UPDATE account SET passwd='{Muser.NewPasswd}' WHERE userid='{Muser.UserId}'";
                        SqlCommand sqlcommand = new SqlCommand(Sql);
                        sqlcommand.Connection = sqlconnection;
                        sqlcommand.ExecuteNonQuery();
                        sqlconnection.Close();
                    }

                }
            }

        }
    }
    
}
