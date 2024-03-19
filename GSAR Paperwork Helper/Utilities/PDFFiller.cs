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
using Microsoft.AspNetCore.Mvc;


namespace GSAR_Paperwork_Helper.Utilities
{
    public static class PDFFiller
    {
        public static byte[]? fillProgramPlan(GSARProgram program)
        {
            string src = "/BlankForms/1-GSAR Program Plan.pdf";
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
                    toSet.SetValue("James Bond");

                    fields.TryGetValue("Instructor Name", out toSet);
                    toSet.SetValue("English");

                    pdf.Close();
                    return outputStream.ToArray();

                }
            }
            return null;
        }

    }
}
