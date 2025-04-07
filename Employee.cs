using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Variant6
{
    public class Employee : INotifyPropertyChanged
    {
        [Key]
        public int Id { get; set; }

        private string _fullName;
        [Required]
        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        private string _position;
        public string Position
        {
            get => _position;
            set
            {
                _position = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        private int _workHours;
        public int WorkHours
        {
            get => _workHours;
            set
            {
                _workHours = value;
                OnPropertyChanged(nameof(WorkHours));
            }
        }

        private DateTime _hireDate;
        public DateTime HireDate
        {
            get => _hireDate;
            set
            {
                _hireDate = value;
                OnPropertyChanged(nameof(HireDate));
            }
        }

        private string _employmentStatus;
        public string EmploymentStatus
        {
            get => _employmentStatus;
            set
            {
                _employmentStatus = value;
                OnPropertyChanged(nameof(EmploymentStatus));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}