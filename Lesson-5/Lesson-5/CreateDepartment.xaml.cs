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
    /// Логика взаимодействия для CreateDepartment.xaml
    /// </summary>
    public partial class CreateDepartment : Window
    {
        Department department = new Department();
        public CreateDepartment()
        {
            InitializeComponent();
            CreateBtn.Click += (sender, e) =>
            {
                department.DepartmentAdd(DepartmentTitleTxt.Text);
                this.Close();
            };
        }         
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
