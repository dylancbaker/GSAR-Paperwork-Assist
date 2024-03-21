using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GSAR_Paperwork_Helper.Models
{
    public class Personnel : INotifyPropertyChanged
    {
        public Guid ID { get; set; }

        private string? _FirstName;
        private string? _LastName;
        private string? _EmailAddress;
        private DateTime _DateOfBirth;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string? FirstName { get => _FirstName; set { _FirstName = value; OnPropertyChanged("FirstName"); } }
        public string? LastName { get => _LastName; set { _LastName = value; OnPropertyChanged("LastName"); } }
        public DateTime DateOfBirth { get => _DateOfBirth; set { _DateOfBirth = value; OnPropertyChanged("DateOfBirth"); } }
        public string? EmailAddress { get => _EmailAddress; set { _EmailAddress = value; OnPropertyChanged("EmailAddress"); } }


        public Personnel() { ID = Guid.NewGuid(); DateOfBirth = DateTime.Now.AddYears(-18); }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
