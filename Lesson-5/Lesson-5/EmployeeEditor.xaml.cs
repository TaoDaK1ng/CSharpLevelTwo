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
                employee.EmployeeUpdate(FirstNameTxt.Text, LastNameTxt.Text, MiddleNameTxt.Text, DepartmentsList.SelectedValue as DataRowView, dataRow);
                this.Close();
            };
        }
    }
}
