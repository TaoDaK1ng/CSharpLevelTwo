using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_5
{
   public class Department
    {
        //public int DepartmentID { get; set; }
        //public string Title { get; set; }
        Connection _connection;
        SqlDataAdapter _adapter;
        public DataTable Table { get; set; }

        public Department()
        {
            Table = new DataTable();
            _adapter = new SqlDataAdapter();
            _connection = new Connection();
            Table = EmployeesConclusion(Table, _adapter);
        }
        public void DepartmentAdd(string title)
        {
            _connection.ConnectionOpen();
            SqlCommand command = new SqlCommand("INSERT INTO Departments (Title) VALUES (@Title)", _connection.SqlConnection);
            command.Parameters.Add("@Title", System.Data.SqlDbType.NVarChar, 50, "Title");
            _adapter.InsertCommand = command;
            DataRow newRow = Table.NewRow();
            newRow["Title"] = title;
            Table.Rows.Add(newRow);
            _adapter.Update(Table);
            _connection.ConnectionClose();
        }
        public void DepartmentUpdate(string title, DataRowView dataRow)
        {
            _connection.ConnectionOpen();
            SqlCommand command = new SqlCommand("UPDATE Departments SET Title=@Title WHERE ID=@ID", _connection.SqlConnection);
            command.Parameters.Add("@Title", System.Data.SqlDbType.NVarChar, 50, "Title");
            command.Parameters.Add("@ID", System.Data.SqlDbType.NVarChar, 50, "ID");
            _adapter.UpdateCommand = command;
            dataRow.BeginEdit();
            dataRow.Row["Title"] = title;
            dataRow.EndEdit();
            _adapter.Update(Table);
            _connection.ConnectionClose();
        }
        public void DepartmentDelete(DataRowView dataRow)
        {
            _connection.ConnectionOpen();
            SqlCommand command = new SqlCommand("DELETE FROM Departments WHERE ID=@ID", _connection.SqlConnection);
            command.Parameters.Add("@ID", System.Data.SqlDbType.BigInt, 0, "ID");
            _adapter.DeleteCommand = command;
            dataRow.Row.Delete();
            _adapter.Update(Table);
            _connection.ConnectionClose();
        }

        public DataTable EmployeesConclusion(DataTable table, SqlDataAdapter adapter)
        {
            _connection.ConnectionOpen();
            SqlCommand command = new SqlCommand("SELECT * FROM Departments", _connection.SqlConnection);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            _connection.ConnectionClose();
            return table;
        }
    }
}
