using System.Windows;

namespace Variant6
{
    public partial class EditEmployeeWindow : Window
    {
        public Employee EditedEmployee { get; set; }

        public EditEmployeeWindow(Employee employee)
        {
            InitializeComponent();
            EditedEmployee = new Employee
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Position = employee.Position,
                WorkHours = employee.WorkHours,
                HireDate = employee.HireDate,
                EmploymentStatus = employee.EmploymentStatus
            };
            DataContext = this;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}