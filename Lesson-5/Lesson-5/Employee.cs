using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_5
{
    public class Employee
    {
        //public int EmployeeId { get; set; }
        //public int DepartmentId { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string MiddleName { get; set; }
        Connection _connection;
        public SqlDataAdapter _adapter;
        public DataTable Table { get; set; }

        public Employee()
        {
            Table = new DataTable();
            _adapter = new SqlDataAdapter();
            _connection = new Connection();
            Table = EmployeesConclusion(Table, _adapter);
        }
        public void EmployeeAdd(string firstName, string lastName, string middleName, int departmentId)
        {
            _connection.ConnectionOpen();
            SqlCommand command = new SqlCommand("INSERT INTO Employees (FirstName, LastName, MiddleName, DepartmentId) VALUES (@FirstName, @LastName, @MiddleName, @DepartmentId)", _connection.SqlConnection);
            command.Parameters.Add("@FirstName", System.Data.SqlDbType.NVarChar, 50, "FirstName");
            command.Parameters.Add("@LastName", System.Data.SqlDbType.NVarChar, 50, "LastName");
            command.Parameters.Add("@MiddleName", System.Data.SqlDbType.NVarChar, 50, "MiddleName");
            command.Parameters.Add("@DepartmentId", System.Data.SqlDbType.BigInt, 0, "DepartmentId");
            _adapter.InsertCommand = command;
            DataRow newRow = Table.NewRow();
            newRow["DepartmentId"] = departmentId;
            newRow["FirstName"] = firstName;
            newRow["LastName"] = lastName;
            newRow["MiddleName"] = middleName;
            Table.Rows.Add(newRow);
            _adapter.Update(Table);
            _connection.ConnectionClose();
        }
        public void EmployeeUpdate(string firstName, string lastName, string middleName, DataRowView departmentId, DataRowView dataRow)
        {
            _connection.ConnectionOpen();
            SqlCommand command = new SqlCommand("UPDATE Employees SET FirstName=@FirstName, LastName=@LastName, MiddleName=@MiddleName, DepartmentId=@DepartmentId WHERE ID=@ID", _connection.SqlConnection);
            command.Parameters.Add("@FirstName", System.Data.SqlDbType.NVarChar, 50, "FirstName");
            command.Parameters.Add("@LastName", System.Data.SqlDbType.NVarChar, 50, "LastName");
            command.Parameters.Add("@MiddleName", System.Data.SqlDbType.NVarChar, 50, "MiddleName");
            command.Parameters.Add("@DepartmentId", System.Data.SqlDbType.BigInt, 0, "DepartmentId");
            command.Parameters.Add("@ID", System.Data.SqlDbType.BigInt, 0, "ID");
            _adapter.UpdateCommand = command;       
            dataRow.BeginEdit();
            dataRow.Row["FirstName"] = firstName;
            dataRow.Row["LastName"] = lastName;
            dataRow.Row["MiddleName"] = middleName;
            dataRow.Row["DepartmentId"] = departmentId.Row.Field<Int64>("ID");
            dataRow.CancelEdit();
            _adapter.Update(Table);
            _connection.ConnectionClose();
        }
        public void EmployeeDelete(DataRowView dataRow)
        {
            _connection.ConnectionOpen();
            SqlCommand command = new SqlCommand("DELETE FROM Employees WHERE ID=@ID", _connection.SqlConnection);
            command.Parameters.Add("@ID", System.Data.SqlDbType.BigInt, 0, "ID");
            _adapter.DeleteCommand = command;
            dataRow.Row.Delete();
            _adapter.Update(Table);
            _connection.ConnectionClose();
        }

        public DataTable EmployeesConclusion(DataTable table, SqlDataAdapter adapter)
        {
            _connection.ConnectionOpen();
            SqlCommand command = new SqlCommand("SELECT * FROM Employees", _connection.SqlConnection);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            _connection.ConnectionClose();
            return table;
        }   
    }
}
