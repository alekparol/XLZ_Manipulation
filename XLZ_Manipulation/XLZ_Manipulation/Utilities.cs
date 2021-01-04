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

/* SAVING XLZ */

/* Description: ReadFileFromXLZ method is opening XLZ file on a given path by xlzFile argument and then searches for the file of the given extension by the passed argument searchExtension. As The XLZ file 
 * according to the standard, has to contain skeleton.skl and content.xlf files it allow to open them (and other files included in XLZ file like source file). The function then returns content of the file 
 * saved in the string variable. 
 * Usage: this method is used to get content.xlf file in our program.
 * 
 */

/* DELETING FILE FROM XLZ */

/* 
 * 
 */

/* UPDATING XLZ FILE */

/* 
 * 
 */

namespace XLZ_Manipulation
{
    public class Utilities
    {
        /* 
         * 
         */
        public static string ReadFileFromXLZ(string xlzFile, string fileName, string fileExtension)
        {
            string searchFileString = String.Empty;

            try
            {
                using (ZipArchive xlzArchive = ZipFile.OpenRead(xlzFile))
                {

                    ZipArchiveEntry searchFile = xlzArchive.Entries.First(x => x.Name.ToLower() == fileName.ToLower());

                    if (searchFile != null && searchFile.Name.EndsWith("." + fileExtension.ToLower().Trim()))
                    {
                        using (Stream stream = searchFile.Open())
                        {
                            StreamReader reader = new StreamReader(stream);
                            searchFileString = reader.ReadToEnd();
                        }
                    }
                    else
                    {
                        throw new Exception(String.Format("Please check your file: {0} - file of the format {1} is missing.", Path.GetFileName(xlzFile), fileExtension));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Please check your file: {0} - this is not a valid archive - possibility to process file outside of TMS may be required:ex {1}", Path.GetFileName(xlzFile), ex.ToString()));
            }

            return searchFileString;
        }

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

        /* 
         * 
         */
        public static string ReadContentXLF(string xlzFile)
        {
            return ReadFileFromXLZ(xlzFile, "xlf");
        }

        /* 
         * 
         */
        public static string ReadSkeletonSKL(string xlzFile)
        {
            return ReadFileFromXLZ(xlzFile, "skl");
        }

        /* 
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

        /* 
         * 
         */
        public static void SaveContentXLF(string xlzFile, string fileContent)
        {
            SaveFileToXLZ(xlzFile, "content.xlf", fileContent);
        }

        /* 
         * 
         */
        public static void SaveSkeletonSKL(string xlzFile, string fileContent)
        {
            SaveFileToXLZ(xlzFile, "skl", fileContent);
        }

        /* DELETING FILE FROM XLZ */

        /* 
         * 
         */
        public static void DeleteFileFromXLZ(string xlzFile, string searchExtension)
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
        }

        /* 
         * 
         */
        public static void DeleteContentXLF(string xlzFile)
        {
            DeleteFileFromXLZ(xlzFile, "xlf");
        }

        /* 
         * 
         */
        public static void DeleteSkeletonSKL(string xlzFile)
        {
            DeleteFileFromXLZ(xlzFile, "skl");
        }

    }
}
