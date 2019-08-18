using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Lesson_5
{
    class Connection
    {
        string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;
        Initial Catalog=Lesson7;
        Integrated Security=True;
        Pooling=False;";
        public string ConnectionString => _connectionString;
        public SqlConnection SqlConnection { get; set; }
        public Connection()
        {
            SqlConnection = new SqlConnection(ConnectionString);
        }
        public void ConnectionOpen()
        {
            try
            {
                SqlConnection.Open();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }           
        }
        public void ConnectionClose()
        {
            try
            {
                SqlConnection.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message); 
            }         
        } 
    }
}
