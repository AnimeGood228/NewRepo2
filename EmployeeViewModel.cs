// EmployeeViewModel.cs
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;

namespace Variant6
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context;
        private ObservableCollection<Employee> _employees;
        private Employee _selectedEmployee;

        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }

        public ICommand AddEmployeeCommand { get; }
        public ICommand EditEmployeeCommand { get; }
        public ICommand GenerateReportCommand { get; }
        public ICommand SaveChangesCommand { get; }

        public EmployeeViewModel()
        {
            _context = new AppDbContext();
            _context.Database.EnsureCreated();
            LoadEmployees();

            AddEmployeeCommand = new RelayCommand(AddEmployee);
            EditEmployeeCommand = new RelayCommand(EditEmployee);
            GenerateReportCommand = new RelayCommand(GenerateReport);
            SaveChangesCommand = new RelayCommand(SaveChanges);
        }

        private void LoadEmployees()
        {
            Employees = new ObservableCollection<Employee>(_context.Employees.ToList());
        }

        private void AddEmployee(object parameter)
        {
            var employee = new Employee
            {
                FullName = "Новый сотрудник",
                Position = "Должность",
                HireDate = DateTime.Now,
                WorkHours = 0,
                EmploymentStatus = "Активен"
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();
            Employees.Add(employee);
            SelectedEmployee = employee;
        }

        private void EditEmployee(object parameter)
        {
            if (SelectedEmployee != null)
            {
                var editWindow = new EditEmployeeWindow(SelectedEmployee);
                if (editWindow.ShowDialog() == true)
                {
                    _context.Entry(SelectedEmployee).CurrentValues.SetValues(editWindow.EditedEmployee);
                    _context.SaveChanges();
                    LoadEmployees(); // Обновляем список
                }
            }
        }

        private void GenerateReport(object parameter)
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                FileName = "EmployeeReport.csv"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                using (var writer = new System.IO.StreamWriter(saveFileDialog.FileName))
                {
                    writer.WriteLine("FullName,Position,WorkHours,HireDate,EmploymentStatus");
                    foreach (var employee in Employees)
                    {
                        writer.WriteLine($"{employee.FullName},{employee.Position},{employee.WorkHours},{employee.HireDate.ToShortDateString()},{employee.EmploymentStatus}");
                    }
                }
            }
        }

        private void SaveChanges(object parameter)
        {
            _context.SaveChanges();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}