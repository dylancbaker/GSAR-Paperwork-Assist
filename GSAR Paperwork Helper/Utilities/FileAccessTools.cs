using iText.Kernel.Pdf;
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
    }
}
