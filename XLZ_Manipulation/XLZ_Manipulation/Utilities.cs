using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;


/* OPENINING XLZ */

/* Description: ReadFileFromXLZ method is opening XLZ file on a given path by xlzFile argument and then searches for the file of the given extension by the passed argument searchExtension. As The XLZ file 
 * according to the standard, has to contain skeleton.skl and content.xlf files it allow to open them (and other files included in XLZ file like source file). The function then returns content of the file 
 * saved in the string variable. 
 * Usage: this method is used to get content.xlf file in our program.
 * 
 */

namespace XLZ_Manipulation
{
    public class Utilities
    {
        public static string ReadFileFromXLZ(string xlzFile, string searchExtension)
        {
            string searchFileString = String.Empty;

            try
            {
                using (ZipArchive xlzArchive = ZipFile.OpenRead(xlzFile))
                {

                    ZipArchiveEntry searchFile = xlzArchive.Entries.First(x => x.Name.ToLower().EndsWith("." + searchExtension.ToLower().Trim()));

                    if (searchFile != null)
                    {
                        using (Stream stream = searchFile.Open())
                        {
                            StreamReader reader = new StreamReader(stream);
                            searchFileString = reader.ReadToEnd();
                        }
                    }
                    else
                    {
                        throw new Exception(String.Format("Please check your file: {0} - file of the format {1} is missing.", Path.GetFileName(xlzFile), searchExtension));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Please check your file: {0} - this is not a valid archive - possibility to process file outside of TMS may be required:ex {1}", Path.GetFileName(xlzFile), ex.ToString()));
            }

            return searchFileString;
        }

        /* Description: ReadContentXLF is a method that uses ReadFileFromXLZ but with defined extension as "xlf" to return content.xlf file.
         * Usage: this method is used to get content.xlf file in our program.
         * 
         */
        public static string ReadContentXLF(string xlzFile)
        {
            return ReadFileFromXLZ(xlzFile, "xlf");
        }

        /* Description: ReadContentSKL is a method that uses ReadFileFromXLZ but with defined extension as "skl" to return skeleton.skl file.
         * Usage: this method is not used in our program yet.
         * 
         */
        public static string ReadSkeletonSKL(string xlzFile)
        {
            return ReadFileFromXLZ(xlzFile, "skl");
        }

        /* SAVING XLZ */

        /* Description: ReadFileFromXLZ method is opening XLZ file on a given path by xlzFile argument and then searches for the file of the given extension by the passed argument searchExtension. As The XLZ file 
         * according to the standard, has to contain skeleton.skl and content.xlf files it allow to open them (and other files included in XLZ file like source file). The function then returns content of the file 
         * saved in the string variable. 
         * Usage: this method is used to get content.xlf file in our program.
         * 
         */
        public static void SaveFileToXLZ(string xlzFile, string fileName, string fileContent)
        {
            try
            {
                using (ZipArchive xlzArchive = ZipFile.Open(xlzFile, ZipArchiveMode.Update))
                {

                    ZipArchiveEntry newFile = xlzArchive.CreateEntry(fileName);

                    using (StreamWriter writer = new StreamWriter(newFile.Open()))
                    {
                        writer.Write(fileContent);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Please check your file: {0} - this is not a valid archive - possibility to process file outside of TMS may be required:ex {1}", Path.GetFileName(xlzFile), ex.ToString()));
            }
        }

        /* Description: ReadContentXLF is a method that uses ReadFileFromXLZ but with defined extension as "xlf" to return content.xlf file.
         * Usage: this method is used to get content.xlf file in our program.
         * 
         */
        public static string SaveContentXLF(string xlzFile)
        {
            return ReadFileFromXLZ(xlzFile, "xlf");
        }

        /* Description: ReadContentSKL is a method that uses ReadFileFromXLZ but with defined extension as "skl" to return skeleton.skl file.
         * Usage: this method is not used in our program yet.
         * 
         */
        public static string SaveSkeletonSKL(string xlzFile)
        {
            return ReadFileFromXLZ(xlzFile, "skl");
        }

        /* DELETING FILE FROM XLZ */

        /* 
         * 
         */
        public static int DeleteFileFromXLZ(string xlzFile, string searchExtension)
        {
            try
            {
                using (ZipArchive xlzArchive = ZipFile.Open(xlzFile, ZipArchiveMode.Update))
                {

                    ZipArchiveEntry searchFile = xlzArchive.Entries.First(x => x.Name.ToLower().EndsWith("." + searchExtension.ToLower().Trim()));

                    if (searchFile != null)
                    {
                        using (Stream stream = searchFile.Open())
                        {
                            searchFile.Delete();
                            return 1;
                        }
                    }
                    else
                    {
                        throw new Exception(String.Format("Please check your file: {0} - file of the format {1} is missing.", Path.GetFileName(xlzFile), searchExtension));

                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Please check your file: {0} - this is not a valid archive - possibility to process file outside of TMS may be required:ex {1}", Path.GetFileName(xlzFile), ex.ToString()));
            }

            return 0;
        }

        /* Description: ReadContentXLF is a method that uses ReadFileFromXLZ but with defined extension as "xlf" to return content.xlf file.
         * Usage: this method is used to get content.xlf file in our program.
         * 
         */
        public static int DeleteContentXLF(string xlzFile)
        {
            return DeleteFileFromXLZ(xlzFile, "xlf");
        }

        /* Description: ReadContentSKL is a method that uses ReadFileFromXLZ but with defined extension as "skl" to return skeleton.skl file.
         * Usage: this method is not used in our program yet.
         * 
         */
        public static int DeleteSkeletonSKL(string xlzFile)
        {
            return DeleteFileFromXLZ(xlzFile, "skl");
        }

    }
}
