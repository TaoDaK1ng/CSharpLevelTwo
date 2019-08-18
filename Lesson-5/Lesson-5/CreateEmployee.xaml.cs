using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для CreateEmployee.xaml
    /// </summary>
    public partial class CreateEmployee : Window
    {
        Employee employee = new Employee();
        Department department = new Department();
        public CreateEmployee()
        {
            InitializeComponent();
           
            DepartmentsList.ItemsSource = department.Table.DefaultView;
            CreateBtn.Click += (sender, e) => {
                employee.EmployeeAdd(FirstNameTxt.Text, LastNameTxt.Text, MiddleNameTxt.Text, Convert.ToInt32(DepartmentsList.SelectedValue));
                this.Close();
                };
            }
        //public CreateEmployee(MainWindow mainWindow) : this()
        //{
        //    //MainGrid.DataContext = mainWindow.Db;
        //    //CreateBtn.Click += delegate {
        //    //    mainWindow.Db.Employees.Add(new Employee(FirstNameTxt.Text, LastNameTxt.Text, MiddleNameTxt.Text, mainWindow.Db.Employees.Count + 1, DepartmentsList.SelectedIndex));
        //    //    this.Close();
        //    //};
        //}
        }
}
