using GSAR_Paperwork_Helper.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSAR_Paperwork_Helper.ViewModels
{
    public class ProgramRosterViewModel : BaseViewModel
    {
        public ProgramRosterViewModel() { currentProgram = new GSARProgram(); }


        private readonly Guid BCSARA = new Guid("CC3A9DC9-01A3-4D39-B806-2128B51120BC");
        public List<Organization> BCSARGroups { get { return OrganizationTools.GetOrganizations(BCSARA); } }

        private GSARProgram? _currentProgram = null;
        public GSARProgram currentProgram { get { return _currentProgram; } private set { _currentProgram = value; } }

        public string TitleText { get => "JIBC GSAR Program Paperwork Assist"; }




    }
}
