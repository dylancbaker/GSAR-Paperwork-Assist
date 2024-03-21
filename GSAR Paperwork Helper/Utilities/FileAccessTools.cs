using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Layout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSAR_Paperwork_Helper.Utilities
{
    internal static class FileAccessTools
    {
     
        public static bool checkWriteAccess(string dirPath, bool throwIfFails = false)
        {
            try
            {
                using (FileStream fs = File.Create(
                    Path.Combine(
                        dirPath,
                        Path.GetRandomFileName()
                    ),
                    1,
                    FileOptions.DeleteOnClose)
                )
                { }
                return true;
            }
            catch
            {
                if (throwIfFails)
                    throw;
                else
                    return false;
            }
        }

        public static byte[] concatAndAddContent(List<byte[]> pdfByteContent)
        {
            using (var writerMemoryStream = new MemoryStream())
            {
                using (var writer = new PdfWriter(writerMemoryStream))
                {
                    using (var mergedDocument = new PdfDocument(writer))
                    {
                        var merger = new PdfMerger(mergedDocument);

                        foreach (var pdfBytes in pdfByteContent)
                        {
                            using (var copyFromMemoryStream = new MemoryStream(pdfBytes))
                            {
                                
                                    using (var copyFromDocument = new PdfDocument(new PdfReader(copyFromMemoryStream)))
                                    {

                                        merger.Merge(copyFromDocument, 1, copyFromDocument.GetNumberOfPages());
                                    }
                                
                            }
                        }
                    }
                }

                return writerMemoryStream.ToArray();
            }




        }

        public static void SimpleMerge(string[] pdfFiles, string mergedPdfFileName)
        {
            using var writer = new PdfWriter(mergedPdfFileName);
            using var mergedPdfDocument = new PdfDocument(writer);
            var pdfMerger = new PdfMerger(mergedPdfDocument);
            foreach (var file in pdfFiles)
            {
                using var reader = new PdfReader(file);
                using var srcPdfDocument = new PdfDocument(reader);
                pdfMerger.Merge(srcPdfDocument, 1, srcPdfDocument.GetNumberOfPages());
            }

            pdfMerger.SetCloseSourceDocuments(true);
        }
    }
}
