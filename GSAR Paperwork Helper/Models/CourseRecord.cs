using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace GSAR_Paperwork_Helper.Models
{
    public class CourseRecord : INotifyPropertyChanged
    {
        private Guid _RecordID;
        private Guid _PersonnelID;
        private Guid _CourseID;
        private string? _StudentName;
        private double _ExamResult;
        private bool _PassPractical1;

        private bool _PassPractical2;
        private bool _PassPractical3;
        private bool _PassPractical4;
        private int _RelevantPracticals;









        public Guid RecordID { get => _RecordID; set { _RecordID = value; OnPropertyChanged("RecordID"); } }
        public Guid PersonnelID { get => _PersonnelID; set { _PersonnelID = value; OnPropertyChanged("PersonnelID"); } }
        public Guid CourseID { get => _CourseID; set { _CourseID = value; OnPropertyChanged("CourseID"); } }
        public string? StudentName { get => _StudentName; set { _StudentName = value; OnPropertyChanged("StudentName"); } }
        public double ExamResult { get => _ExamResult; set { _ExamResult = value; OnPropertyChanged("ExamResult"); } }
        public bool PassPractical1 { get => _PassPractical1; set { _PassPractical1 = value; OnPropertyChanged("PassPractical1"); } }
        public bool PassPractical2 { get => _PassPractical2; set { _PassPractical2 = value; OnPropertyChanged("PassPractical2"); } }
        public bool PassPractical3 { get => _PassPractical3; set { _PassPractical3 = value; OnPropertyChanged("PassPractical3"); } }
        public bool PassPractical4 { get => _PassPractical4; set { _PassPractical4 = value; OnPropertyChanged("PassPractical4"); } }
        public int RelevantPracticals { get => _RelevantPracticals; set { _RelevantPracticals = value; OnPropertyChanged("RelevantPracticals"); } }
        public bool FinalPass
        {
            get
            {
                if (ExamResult < 70) { return false; }
                if (RelevantPracticals > 0 && !PassPractical1) { return false; }
                if (RelevantPracticals > 1 && !PassPractical2) { return false; }
                if (RelevantPracticals > 2 && !PassPractical3) { return false; }
                if (RelevantPracticals > 3 && !PassPractical4) { return false; }
                return true;
            }
        }

        public CourseRecord() { RecordID = Guid.NewGuid(); }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
