using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Diagnostics;

namespace Lesson_5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Employee worker;
        public Department department;
        DataTable EmployeeTable;
        DataTable DepartmentTable;
        public MainWindow()
        {
            InitializeComponent();
            worker = new Employee();
            department = new Department();
            EmployeeTable = worker.Table;
            DepartmentTable = department.Table;
            EmployeeGrid.DataContext = EmployeeTable.DefaultView;
            DepartmentGrid.DataContext = DepartmentTable.DefaultView;
            ChangeEmployeeBtn.Click += (sender, e) => new EmployeeEditor((DataRowView)EmployeeGrid.SelectedItem, worker, department).Show();
            //ChangeDepartmentBtn.Click += (sender, e) => new DepartmentEditor(DepartmentList.SelectedItem as Department).Show();
            CreateEmployeeBtn.Click += (sender, e) => new CreateEmployee().Show();
            CreateDepartmentBtn.Click += (sender, e) => new CreateDepartment().Show();
            //this.Activated += (sender, e) => {
            //    EmployeeGrid.DataContext = EmployeeTable.DefaultView;
            //    DepartmentGrid.DataContext = DepartmentTable.DefaultView;
            //};
        }

        private void DepartmentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //EmployeeList.ItemsSource = Db.Employees.Where(o => o.DepartmentId == DepartmentList.SelectedIndex);
        }

        //private void Window_Activated(object sender, EventArgs e)
        //{
        //    EmployeeList.ItemsSource = Db.Employees.Where(o => o.DepartmentId == DepartmentList.SelectedIndex);
        //}
    }
}
