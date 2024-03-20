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


namespace GSAR_Paperwork_Helper.Utilities
{
    public static class PDFFiller
    {
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

                    for(int x= 0; x < program.students.Count && x < 28; x++)
                    {
                        Personnel p = program.students[x];
                        fields.TryGetValue("LAST NAME" + (x + 1), out toSet);
                        if (toSet != null) { toSet.SetValue(p.LastName); }
                        fields.TryGetValue("FIRST NAME" + (x + 1), out toSet);
                        if (toSet != null) { toSet.SetValue(p.FirstName); }
                        fields.TryGetValue("DATE OF BIRTH" + (x + 1), out toSet);
                        if (toSet != null) { toSet.SetValue(p.DateOfBirth.ToString("yyyy-MMM-dd")); }
                       

                    }

                    pdf.Close();
                    return outputStream.ToArray();

                }
            }
            return null;
        }

    }
}
