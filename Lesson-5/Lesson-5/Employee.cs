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
        public void EmployeeUpdate(string firstName, string lastName, string middleName, int departmentId, DataRowView dataRow)
        {
            _connection.ConnectionOpen();
            SqlCommand command = new SqlCommand("UPDATE Employees SET FirstName=@FirstName, LastName=@LastName, MiddleName=@MiddleName, DepartmentId=@DepartmentId WHERE ID=@ID", _connection.SqlConnection);
            command.Parameters.Add("@FirstName", System.Data.SqlDbType.NVarChar, 50, "FirstName");
            command.Parameters.Add("@LastName", System.Data.SqlDbType.NVarChar, 50, "LastName");
            command.Parameters.Add("@MiddleName", System.Data.SqlDbType.NVarChar, 50, "MiddleName");
            command.Parameters.Add("@DepartmentId", System.Data.SqlDbType.BigInt, 0, "DepartmentId");
            _adapter.UpdateCommand = command;
            DataRow newDataRow = dataRow.Row;
            dataRow.BeginEdit();
            newDataRow["FirstName"] = firstName;
            newDataRow["LastName"] = lastName;
            newDataRow["MiddleName"] = middleName;
            newDataRow["DepartmentId"] = departmentId;
            dataRow.CancelEdit();
            _adapter.Update(Table);
            _connection.ConnectionClose();
        }
        public void EmployeeDelete(int id, DataRow dataRow)
        {
            _connection.ConnectionOpen();
            SqlCommand command = new SqlCommand("DELETE FROM Employees WHERE ID=@ID", _connection.SqlConnection);
            command.Parameters.Add("@ID", System.Data.SqlDbType.BigInt, 0, "ID");
            _adapter.DeleteCommand = command;
            Table.Rows.Remove(dataRow);
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
    //    peopleDataGrid.DataContext = dt.DefaultView;

    //        cBox.ItemsSource = dt.DefaultView;

    //    }
    //private void addButton_Click(object sender, RoutedEventArgs e)
    //{
    //    // добавим новую строку
    //    DataRow newRow = dt.NewRow();
    //    EditWindow editWindow = new EditWindow(newRow);
    //    editWindow.ShowDialog();

    //    if (editWindow.DialogResult.Value)
    //    {
    //        dt.Rows.Add(editWindow.resultRow);
    //        adapter.Update(dt);
    //    }
    //}
    //private void updateButton_Click(object sender, RoutedEventArgs e)
    //{
    //    DataRowView newRow = (DataRowView)peopleDataGrid.SelectedItem;
    //    newRow.BeginEdit();

    //    EditWindow editWindow = new EditWindow(newRow.Row);
    //    editWindow.ShowDialog();

    //    if (editWindow.DialogResult.HasValue && editWindow.DialogResult.Value)
    //    {
    //        newRow.EndEdit();
    //        adapter.Update(dt);
    //    }
    //    else
    //    {
    //        newRow.CancelEdit();
    //    }
    //}
    //private void deleteButton_Click(object sender, RoutedEventArgs e)
    //{
    //    DataRowView newRow = (DataRowView)peopleDataGrid.SelectedItem;

    //    newRow.Row.Delete();
    //    adapter.Update(dt);
    //}
}
}
