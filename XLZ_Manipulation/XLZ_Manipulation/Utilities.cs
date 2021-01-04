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
        /*public static IEnumerable<ZipArchiveEntry> GetFilesByFullName(ZipArchive zipArchive)
        {
            return zipArchive.Entries.Where(x => x.Name.ToLower() == fileName.ToLower());
        }

        public static IEnumerable<ZipArchiveEntry> GetFilesByFullName(ZipArchive zipArchive)
        {

        }

        public static IEnumerable<ZipArchiveEntry> GetFilesByFullName(ZipArchive zipArchive)
        {

        }*/

        /* 
         * 
         */
        public static List<string> ReadFilesFromXLZByFullName(string xlzFile, string fileName)
        {
            List<string> foundFilesStringList = new List<string>();
            string foundFileString = String.Empty;

            try
            {
                using (ZipArchive xlzArchive = ZipFile.OpenRead(xlzFile))
                {

                    IEnumerable<ZipArchiveEntry> foundFiles = xlzArchive.Entries.Where(x => x.Name.ToLower() == fileName.ToLower());

                    if (foundFiles.Count() > 0)
                    {
                        foreach (ZipArchiveEntry foundFile in foundFiles)
                        {
                            using (Stream stream = foundFile.Open())
                            {
                                StreamReader reader = new StreamReader(stream);
                                foundFileString = reader.ReadToEnd();

                                foundFilesStringList.Add(foundFileString);
                            }
                        }                       
                    }
                    else
                    {
                        throw new Exception(String.Format("Please check your file: {0} - file of the name {1} is missing.", Path.GetFileName(xlzFile), fileName));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Please check your file: {0} - this is not a valid archive - possibility to process file outside of TMS may be required:ex {1}", Path.GetFileName(xlzFile), ex.ToString()));
            }

            return foundFilesStringList;
        }

        /* 
         * 
         */
        public static List<string> ReadFilesFromXLZByName(string xlzFile, string fileName)
        {
            List<string> foundFilesStringList = new List<string>();
            string foundFileString = String.Empty;

            try
            {
                using (ZipArchive xlzArchive = ZipFile.OpenRead(xlzFile))
                {

                    IEnumerable<ZipArchiveEntry> foundFiles = xlzArchive.Entries.Where(x => x.Name.ToLower().Contains(fileName.ToLower()));

                    if (foundFiles.Count() > 0)
                    {
                        foreach (ZipArchiveEntry foundFile in foundFiles)
                        {
                            using (Stream stream = foundFile.Open())
                            {
                                StreamReader reader = new StreamReader(stream);
                                foundFileString = reader.ReadToEnd();

                                foundFilesStringList.Add(foundFileString);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception(String.Format("Please check your file: {0} - file of the name containing {1} is missing.", Path.GetFileName(xlzFile), fileName));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Please check your file: {0} - this is not a valid archive - possibility to process file outside of TMS may be required:ex {1}", Path.GetFileName(xlzFile), ex.ToString()));
            }

            return foundFilesStringList;
        }

        /* 
         * 
         */
        public static List<string> ReadFilesFromXLZByExtension(string xlzFile, string fileExtension)
        {
            List<string> foundFilesStringList = new List<string>();
            string foundFileString = String.Empty;

            try
            {
                using (ZipArchive xlzArchive = ZipFile.OpenRead(xlzFile))
                {

                    IEnumerable<ZipArchiveEntry> foundFiles = xlzArchive.Entries.Where(x => x.Name.ToLower().EndsWith("." + fileExtension.ToLower().Trim()));

                    if (foundFiles.Count() > 0)
                    {
                        foreach (ZipArchiveEntry foundFile in foundFiles)
                        {
                            using (Stream stream = foundFile.Open())
                            {
                                StreamReader reader = new StreamReader(stream);
                                foundFileString = reader.ReadToEnd();

                                foundFilesStringList.Add(foundFileString);
                            }
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

            return foundFilesStringList;
        }

        /* 
         * 
         */
        public static string ReadFileFromXLZByFullName(string xlzFile, string fileName)
        {
            List<string> foundFiles = ReadFilesFromXLZByFullName(xlzFile, fileName);
            string foundFile = String.Empty;

            if (foundFiles.Count == 1)
            {
                foundFile = foundFiles.ElementAt(0);
            }

            return foundFile;
        }

        /* 
         * 
         */
        public static string ReadFileFromXLZByName(string xlzFile, string fileName)
        {
            List<string> foundFiles = ReadFilesFromXLZByName(xlzFile, fileName);
            string foundFile = String.Empty;

            if (foundFiles.Count == 1)
            {
                foundFile = foundFiles.ElementAt(0);
            }

            return foundFile;
        }

        /* 
         * 
         */
        public static string ReadFileFromXLZByExtension(string xlzFile, string fileExtension)
        {
            List<string> foundFiles = ReadFilesFromXLZByExtension(xlzFile, fileExtension);
            string foundFile = String.Empty;

            if (foundFiles.Count == 1)
            {
                foundFile = foundFiles.ElementAt(0);
            }

            return foundFile;
        }

        /* 
         * 
         */
        public static string ReadContentXLF(string xlzFile)
        {
            return ReadFileFromXLZByFullName(xlzFile, "content.xlf");
        }

        /* 
         * 
         */
        public static string ReadSkeletonSKL(string xlzFile)
        {
            return ReadFileFromXLZByFullName(xlzFile, "skeleton.skl");
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
