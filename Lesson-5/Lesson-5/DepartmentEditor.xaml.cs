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
    /// Логика взаимодействия для DepartmentEditor.xaml
    /// </summary>
    public partial class DepartmentEditor : Window
    {
        public DepartmentEditor()
        {
            InitializeComponent();
        }
        public DepartmentEditor(DataRowView dataRow, Department department): this()
        {
            DepartmentTxt.Text = dataRow.Row["Title"].ToString();
            CreateButton.Click += (sender, e) =>
            {
                department.DepartmentUpdate(DepartmentTxt.Text, dataRow);
                this.Close();
            };
        }
    }
}
