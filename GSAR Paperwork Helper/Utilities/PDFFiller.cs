using GSAR_Paperwork_Helper.Models;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using iText.IO.Source;
using Org.BouncyCastle.Asn1.Pkcs;
using System.Windows.Media.Animation;


namespace GSAR_Paperwork_Helper.Utilities
{
    public static class PDFFiller
    {
        public static byte[]? fillClassRoster(GSARProgram program, Course course)
        {
            string src = "BlankForms/3-GSAR Class Roster Form.pdf";
            if (System.IO.File.Exists(src))
            {
                FileStream sourceFileStream = System.IO.File.OpenRead(src);
                MemoryStream outputStream = new MemoryStream();
                PdfDocument pdf = new PdfDocument(new PdfReader(sourceFileStream), new PdfWriter(outputStream));
                PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
                if (form != null)
                {
                    IDictionary<String, PdfFormField> fields = form.GetAllFormFields();
                    PdfFormField toSet;

                    fields.TryGetValue("COURSE NAME & CODE", out toSet);
                    if (toSet != null) { toSet.SetValue(course.CourseCode + " " + course.CourseName); }

                    _ = fields.TryGetValue("SAR GROUP", out toSet);
                    if (toSet != null) { toSet.SetValue(program.SARGroupName); }

                    _ = fields.TryGetValue("DATES", out toSet);
                    if (toSet != null) { toSet.SetValue(course.CourseStart.ToString("yyyy-MMM-dd") + " - " + course.CourseEnd.ToString("yyyy-MMM-dd")); }


                    _ = fields.TryGetValue("INSTRUCTOR", out toSet);
                    if (toSet != null) { toSet.SetValue(course.LeadInstructor); }
                    _ = fields.TryGetValue("LOCATION", out toSet);
                    if (toSet != null) { toSet.SetValue(course.CourseLocation); }
                    _ = fields.TryGetValue("INSTRUCTORS", out toSet);
                    if (toSet != null) { toSet.SetValue(course.AssistantInstructors); }

                    _ = fields.TryGetValue("Date", out toSet);
                    if (toSet != null) { toSet.SetValue(DateTime.Now.ToString("yyyy-MMM-dd")); }


                    for (int x = 0; x < course.CourseRecords.Count && x < 26; x++)
                    {
                        Personnel p = program.Students[x];
                        fields.TryGetValue("LAST NAME" + (x + 1), out toSet);
                        if (toSet != null) { toSet.SetValue(p.LastName); }

                        fields.TryGetValue("FIRST NAME" + (x + 1), out toSet);
                        if (toSet != null) { toSet.SetValue(p.FirstName); }

                        fields.TryGetValue("EMAIL ADDRESS" + (x + 1), out toSet);
                        if (toSet != null) { toSet.SetValue(p.EmailAddress); }

                        fields.TryGetValue("EXAM " + (x + 1), out toSet);
                        if (toSet != null) { toSet.SetValue(course.CourseRecords[x].ExamResult.ToString()); }

                        fields.TryGetValue("P1" + (x + 1), out toSet);
                        if (course.CourseRecords[x].PassPractical1) { if (toSet != null) { toSet.SetValue("P"); } }
                        else if (course.PracticalCount > 0) { if (toSet != null) { toSet.SetValue("F"); } }

                        fields.TryGetValue("P2" + (x + 1), out toSet);
                        if (course.CourseRecords[x].PassPractical2) { if (toSet != null) { toSet.SetValue("P"); } }
                        else if (course.PracticalCount > 1) { if (toSet != null) { toSet.SetValue("F"); } }

                        fields.TryGetValue("P3" + (x + 1), out toSet);
                        if (course.CourseRecords[x].PassPractical3) { if (toSet != null) { toSet.SetValue("P"); } }
                        else if (course.PracticalCount > 2) { if (toSet != null) { toSet.SetValue("F"); } }

                        fields.TryGetValue("P4" + (x + 1), out toSet);
                        if (course.CourseRecords[x].PassPractical4) { if (toSet != null) { toSet.SetValue("P"); } }
                        else if (course.PracticalCount > 3) { if (toSet != null) { toSet.SetValue("F"); } }

                        fields.TryGetValue("FINAL GRADE PF" + (x + 1), out toSet);
                        if (course.CourseRecords[x].FinalPass) { if (toSet != null) { toSet.SetValue("P"); } }
                        else { if (toSet != null) { toSet.SetValue("F"); } }
                    }
                    pdf = pdf.RenameAllFields();

                    pdf.Close();
                    return outputStream.ToArray();

                }
            }
            return null;
        }


        public static byte[]? fillProgramPlan(GSARProgram program)
        {
            string src = "BlankForms/1-GSAR Program Plan.pdf";
            if (System.IO.File.Exists(src))
            {
                FileStream sourceFileStream = System.IO.File.OpenRead(src);
                MemoryStream outputStream = new MemoryStream();
                PdfDocument pdf = new PdfDocument(new PdfReader(sourceFileStream), new PdfWriter(outputStream));
                PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
                if (form != null)
                {
                    IDictionary<String, PdfFormField> fields = form.GetAllFormFields();
                    PdfFormField toSet;

                    fields.TryGetValue("SAR GROUP", out toSet);
                    if (toSet != null) { toSet.SetValue(program.SARGroupName); }

                    _ = fields.TryGetValue("Instructor Name", out toSet);
                    if (toSet != null) { toSet.SetValue(program.LeadInstructor); }

                    _ = fields.TryGetValue("Address", out toSet);
                    if (toSet != null) { toSet.SetValue(program.MailingAddress); }
                    _ = fields.TryGetValue("DATE", out toSet);
                    if (toSet != null) { toSet.SetValue(program.ProgramPlanDate.ToString("yyyy-MMM-dd")); }
                    _ = fields.TryGetValue("email", out toSet);
                    if (toSet != null) { toSet.SetValue(program.Email); }
                    _ = fields.TryGetValue("Phone", out toSet);
                    if (toSet != null) { toSet.SetValue(program.Phone); }



                    _ = fields.TryGetValue("EMRG1200 Incident Command System Level 100 if reviewing as group", out toSet);
                    if (toSet != null && program.Courses.Any(o => o.CourseID == StaticValues.EMRG1200))
                    {
                        toSet.SetValue(program.Courses.First(o => o.CourseID == StaticValues.EMRG1200).CourseStart.ToString("yyyy-MMM-dd") 
                            + " to " + program.Courses.First(o => o.CourseID == StaticValues.EMRG1200).CourseEnd.ToString("yyyy-MMM-dd"));
                    }
                    _ = fields.TryGetValue("EMRG1701 GSAR Volunteer Orientation and Safety Guidelines", out toSet);
                    if (toSet != null && program.Courses.Any(o => o.CourseID == StaticValues.EMRG1701))
                    {
                        toSet.SetValue(program.Courses.First(o => o.CourseID == StaticValues.EMRG1701).CourseStart.ToString("yyyy-MMM-dd")
                            + " to " + program.Courses.First(o => o.CourseID == StaticValues.EMRG1701).CourseEnd.ToString("yyyy-MMM-dd"));
                    }
                    _ = fields.TryGetValue("EMRG1703 SAR in BC", out toSet);
                    if (toSet != null && program.Courses.Any(o => o.CourseID == StaticValues.EMRG1703))
                    {
                        toSet.SetValue(program.Courses.First(o => o.CourseID == StaticValues.EMRG1703).CourseStart.ToString("yyyy-MMM-dd")
                            + " to " + program.Courses.First(o => o.CourseID == StaticValues.EMRG1703).CourseEnd.ToString("yyyy-MMM-dd"));
                    }
                    _ = fields.TryGetValue("EMRG1702 Introduction to Search Management", out toSet);
                    if (toSet != null && program.Courses.Any(o => o.CourseID == StaticValues.EMRG1702))
                    {
                        toSet.SetValue(program.Courses.First(o => o.CourseID == StaticValues.EMRG1702).CourseStart.ToString("yyyy-MMM-dd")
                            + " to " + program.Courses.First(o => o.CourseID == StaticValues.EMRG1702).CourseEnd.ToString("yyyy-MMM-dd"));
                    }
                    _ = fields.TryGetValue("EMRG1705 Navigation Skills", out toSet);
                    if (toSet != null && program.Courses.Any(o => o.CourseID == StaticValues.EMRG1705))
                    {
                        toSet.SetValue(program.Courses.First(o => o.CourseID == StaticValues.EMRG1705).CourseStart.ToString("yyyy-MMM-dd")
                            + " to " + program.Courses.First(o => o.CourseID == StaticValues.EMRG1705).CourseEnd.ToString("yyyy-MMM-dd"));
                    }
                    _ = fields.TryGetValue("EMRG1706 Wilderness Survival for GSAR", out toSet);
                    if (toSet != null && program.Courses.Any(o => o.CourseID == StaticValues.EMRG1706))
                    {
                        toSet.SetValue(program.Courses.First(o => o.CourseID == StaticValues.EMRG1706).CourseStart.ToString("yyyy-MMM-dd")
                            + " to " + program.Courses.First(o => o.CourseID == StaticValues.EMRG1706).CourseEnd.ToString("yyyy-MMM-dd"));
                    }
                    _ = fields.TryGetValue("EMRG1704 Ground Search and Rescue Skills", out toSet);
                    if (toSet != null && program.Courses.Any(o => o.CourseID == StaticValues.EMRG1704))
                    {
                        toSet.SetValue(program.Courses.First(o => o.CourseID == StaticValues.EMRG1704).CourseStart.ToString("yyyy-MMM-dd")
                            + " to " + program.Courses.First(o => o.CourseID == StaticValues.EMRG1704).CourseEnd.ToString("yyyy-MMM-dd"));
                    }





                    for (int x = 0; x < program.Students.Count && x < 28; x++)
                    {
                        Personnel p = program.Students[x];
                        fields.TryGetValue("LAST NAME" + (x + 1), out toSet);
                        if (toSet != null) { toSet.SetValue(p.LastName); }
                        fields.TryGetValue("FIRST NAME" + (x + 1), out toSet);
                        if (toSet != null) { toSet.SetValue(p.FirstName); }
                        fields.TryGetValue("DATE OF BIRTH" + (x + 1), out toSet);
                        if (toSet != null) { toSet.SetValue(p.DateOfBirth.ToString("yyyy-MMM-dd")); }


                    }
                    pdf = pdf.RenameAllFields();
                    pdf.Close();
                    return outputStream.ToArray();

                }
            }
            return null;
        }

        public static byte[]? fillCompletion(GSARProgram program)
        {
            string src = "BlankForms/4-GSAR Program Completion Roster-1.pdf";
            if (System.IO.File.Exists(src))
            {
                FileStream sourceFileStream = System.IO.File.OpenRead(src);
                MemoryStream outputStream = new MemoryStream();
                PdfDocument pdf = new PdfDocument(new PdfReader(sourceFileStream), new PdfWriter(outputStream));
                PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
                if (form != null)
                {
                    IDictionary<String, PdfFormField> fields = form.GetAllFormFields();
                    PdfFormField toSet;

                    fields.TryGetValue("GROUP", out toSet);
                    if (toSet != null) { toSet.SetValue(program.SARGroupName); }

                    _ = fields.TryGetValue("LEAD INSTRUCTOR", out toSet);
                    if (toSet != null) { toSet.SetValue(program.LeadInstructor); }

                    _ = fields.TryGetValue("MAILING ADDRESS", out toSet);
                    if (toSet != null) { toSet.SetValue(program.MailingAddress); }
                    _ = fields.TryGetValue("Date", out toSet);
                    if (toSet != null) { toSet.SetValue(DateTime.Now.ToString("yyyy-MMM-dd")); }
                    _ = fields.TryGetValue("PHONE", out toSet);
                    if (toSet != null) { toSet.SetValue(program.Phone); }

                    for (int x = 0; x < program.Students.Count && x < 28; x++)
                    {
                        Personnel p = program.Students[x];
                        fields.TryGetValue("LAST NAME" + (x + 1), out toSet);
                        if (toSet != null) { toSet.SetValue(p.LastName); }
                        fields.TryGetValue("FIRST NAME" + (x + 1), out toSet);
                        if (toSet != null) { toSet.SetValue(p.FirstName); }



                        if (program.StudentPassedCourse(StaticValues.EMRG1701, p.ID))
                        {
                            fields.TryGetValue("1701-" + (x + 1), out toSet);
                            if (toSet != null) { string[] appearances = toSet.GetAppearanceStates(); toSet.SetValue(appearances[appearances.Length - 1]); }
                        }
                        if (program.StudentPassedCourse(StaticValues.EMRG1702, p.ID))
                        {
                            fields.TryGetValue("1702-" + (x + 1), out toSet);
                            if (toSet != null) { string[] appearances = toSet.GetAppearanceStates(); toSet.SetValue(appearances[appearances.Length - 1]); }
                        }
                        if (program.StudentPassedCourse(StaticValues.EMRG1703, p.ID))
                        {
                            fields.TryGetValue("1703-" + (x + 1), out toSet);
                            if (toSet != null) { string[] appearances = toSet.GetAppearanceStates(); toSet.SetValue(appearances[appearances.Length - 1]); }
                        }
                        if (program.StudentPassedCourse(StaticValues.EMRG1704, p.ID))
                        {
                            fields.TryGetValue("1704-" + (x + 1), out toSet);
                            if (toSet != null) { string[] appearances = toSet.GetAppearanceStates(); toSet.SetValue(appearances[appearances.Length - 1]); }
                        }
                        if (program.StudentPassedCourse(StaticValues.EMRG1705, p.ID))
                        {
                            fields.TryGetValue("1705-" + (x + 1), out toSet);
                            if (toSet != null) { string[] appearances = toSet.GetAppearanceStates(); toSet.SetValue(appearances[appearances.Length - 1]); }
                        }
                        if (program.StudentPassedCourse(StaticValues.EMRG1706, p.ID))
                        {
                            fields.TryGetValue("1706-" + (x + 1), out toSet);
                            if (toSet != null) { string[] appearances = toSet.GetAppearanceStates(); toSet.SetValue(appearances[appearances.Length - 1]); }
                        }
                        if (program.StudentPassedCourse(StaticValues.EMRG1200, p.ID))
                        {
                            fields.TryGetValue("1200-" + (x + 1), out toSet);
                            if (toSet != null) { string[] appearances = toSet.GetAppearanceStates(); toSet.SetValue(appearances[appearances.Length - 1]); }
                        }







                    }
                    pdf = pdf.RenameAllFields();
                    pdf.Close();
                    return outputStream.ToArray();

                }
            }
            return null;
        }

        

        public static PdfDocument RenameAllFields(this PdfDocument pdfDoc)
        {
            Guid unique = Guid.NewGuid();
            PdfAcroForm pdfForm = PdfAcroForm.GetAcroForm(pdfDoc, true);
            pdfDoc.GetWriter().SetCloseStream(true);

            var Fields = pdfForm.GetAllFormFields().Select(x => x.Key).ToArray();

            for (int i = 0; i < Fields.Length; i++)
            {
                pdfForm.RenameField(Fields[i], Fields[i] + "-" + unique);
            }

            return pdfDoc;
        }
    }
}
