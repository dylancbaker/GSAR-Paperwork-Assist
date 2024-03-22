using GSAR_Paperwork_Helper.Models;
using GSAR_Paperwork_Helper.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GSAR_Paperwork_Helper.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly Guid BCSARA = new Guid("CC3A9DC9-01A3-4D39-B806-2128B51120BC");
        public List<Organization> BCSARGroups { get { return OrganizationTools.GetOrganizations(BCSARA); } }
     


        private static GSARProgram? _currentProgram = new GSARProgram();
        public GSARProgram currentProgram { get { return _currentProgram; } private set { _currentProgram = value;  } }

        public void SetNewGSARProgram(GSARProgram newGSARProgram)
        {
            currentProgram = newGSARProgram;
            studentList = new BindingList<Personnel>(currentProgram.students);
            studentList.ListChanged += StudentList_ListChanged;

            OnPropertyChanged(nameof(currentProgram));
            OnPropertyChanged(nameof(Emrg1701)); OnPropertyChanged(nameof(Emrg1701.CourseRecords));
            OnPropertyChanged(nameof(Emrg1702)); OnPropertyChanged(nameof(Emrg1702.CourseRecords));
            OnPropertyChanged(nameof(Emrg1703)); OnPropertyChanged(nameof(Emrg1703.CourseRecords));
            OnPropertyChanged(nameof(Emrg1704)); OnPropertyChanged(nameof(Emrg1704.CourseRecords));
            OnPropertyChanged(nameof(Emrg1705)); OnPropertyChanged(nameof(Emrg1705.CourseRecords));
            OnPropertyChanged(nameof(Emrg1706)); OnPropertyChanged(nameof(Emrg1706.CourseRecords));
            OnPropertyChanged(nameof(Emrg1200)); OnPropertyChanged(nameof(Emrg1200.CourseRecords));
            OnPropertyChanged(nameof(studentList));

        }

        private BindingList<Personnel> _studentList = null;
        public BindingList<Personnel> studentList { get => _studentList; set => _studentList = value; }
       
        public Course Emrg1701 { get { if (currentProgram.courses.Any(o => o.CourseID == StaticValues.EMRG1701)) { return currentProgram.courses.First(o => o.CourseID == StaticValues.EMRG1701); } return null; } }
        public Course Emrg1702 { get { if (currentProgram.courses.Any(o => o.CourseID == StaticValues.EMRG1702)) { return currentProgram.courses.First(o => o.CourseID == StaticValues.EMRG1702); } return null; } }
        public Course Emrg1703 { get { if (currentProgram.courses.Any(o => o.CourseID == StaticValues.EMRG1703)) { return currentProgram.courses.First(o => o.CourseID == StaticValues.EMRG1703); } return null; } }
        public Course Emrg1704 { get { if (currentProgram.courses.Any(o => o.CourseID == StaticValues.EMRG1704)) { return currentProgram.courses.First(o => o.CourseID == StaticValues.EMRG1704); } return null; } }
        public Course Emrg1705 { get { if (currentProgram.courses.Any(o => o.CourseID == StaticValues.EMRG1705)) { return currentProgram.courses.First(o => o.CourseID == StaticValues.EMRG1705); } return null; } }
        public Course Emrg1706 { get { if (currentProgram.courses.Any(o => o.CourseID == StaticValues.EMRG1706)) { return currentProgram.courses.First(o => o.CourseID == StaticValues.EMRG1706); } return null; } }
        public Course Emrg1200 { get { if (currentProgram.courses.Any(o => o.CourseID == StaticValues.EMRG1200)) { return currentProgram.courses.First(o => o.CourseID == StaticValues.EMRG1200); } return null; } }

        public MainWindowViewModel()
        {
            currentProgram.SetUpNewProgram();
            studentList = new BindingList<Personnel>(currentProgram.students);
            studentList.ListChanged += StudentList_ListChanged;
            currentProgram.PropertyChanged += CurrentProgram_PropertyChanged;

        }

        private void CurrentProgram_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof
                (currentProgram.LeadInstructor))
            {
                if (Properties.Settings.Default.DefaultInstructor != null) { Properties.Settings.Default.DefaultInstructor = currentProgram.LeadInstructor; Properties.Settings.Default.Save(); }
            }
            else if (e.PropertyName == nameof(currentProgram.SARGroupID))
            {
                Properties.Settings.Default.DefaultGroup = currentProgram.SARGroupID; Properties.Settings.Default.Save();
            }
        }

        private void StudentList_ListChanged(object? sender, ListChangedEventArgs e)
        {
            Personnel p = studentList[e.NewIndex];
            foreach (Course c in currentProgram.courses)
            {
                if (c.CourseRecords.Any(o => o.PersonnelID == p.ID))
                {
                    c.CourseRecords.First(o => o.PersonnelID == p.ID).StudentName = p.LastName + ", " + p.FirstName;
                }
                else
                {
                    CourseRecord record = new CourseRecord();
                    record.CourseID = c.CourseID;
                    record.PersonnelID = p.ID;
                    record.StudentName = p.LastName + ", " + p.FirstName;
                    c.CourseRecords.Add(record);
                }
            }
        }

      
    }
}
