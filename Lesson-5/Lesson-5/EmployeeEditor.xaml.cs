using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lesson_5
{
    /// <summary>
    /// Логика взаимодействия для EmployeeEditor.xaml
    /// </summary>
    public partial class EmployeeEditor : Window
    {
        public EmployeeEditor()
        {
            InitializeComponent();
        }
        public EmployeeEditor(DataRowView dataRow, Employee employee, Department department) : this()
        {
            DepartmentsList.ItemsSource = department.Table.DefaultView;       
            FirstNameTxt.Text = dataRow.Row["FirstName"].ToString();
            LastNameTxt.Text = dataRow.Row["LastName"].ToString();
            MiddleNameTxt.Text = dataRow.Row["MiddleName"].ToString();
            ApplyBtn.Click += (sender, e) =>
            {
                Connection connection = new Connection();
                connection.ConnectionOpen();
                SqlCommand command = new SqlCommand("UPDATE Employees SET FirstName=@FirstName, LastName=@LastName, MiddleName=@MiddleName, DepartmentId=@DepartmentId WHERE ID=@ID", connection.SqlConnection);
                command.Parameters.Add("@FirstName", System.Data.SqlDbType.NVarChar, 50, "FirstName");
                command.Parameters.Add("@LastName", System.Data.SqlDbType.NVarChar, 50, "LastName");
                command.Parameters.Add("@MiddleName", System.Data.SqlDbType.NVarChar, 50, "MiddleName");
                command.Parameters.Add("@DepartmentId", System.Data.SqlDbType.BigInt, 0, "DepartmentId");
                employee._adapter.UpdateCommand = command;
                DataRow newDataRow = dataRow.Row;
                dataRow.BeginEdit();
                newDataRow["FirstName"] = FirstNameTxt.Text;
                newDataRow["LastName"] = LastNameTxt.Text;
                newDataRow["MiddleName"] = MiddleNameTxt.Text;
                newDataRow["DepartmentId"] = Int64.Parse(DepartmentsList.SelectedValue.ToString()); 
                dataRow.CancelEdit();
                employee._adapter.Update(employee.Table);
                connection.ConnectionClose(); 
                this.Close();
            };
        }
    }
}
