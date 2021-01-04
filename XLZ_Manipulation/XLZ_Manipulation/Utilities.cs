using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;


/* READING FILE FROM XLZ */

/* Description: ReadFileFromXLZ method is opening XLZ file on a given path by xlzFile argument and then searches for the file of the given extension by the passed argument searchExtension. As The XLZ file 
 * according to the standard, has to contain skeleton.skl and content.xlf files it allow to open them (and other files included in XLZ file like source file). The function then returns content of the file 
 * saved in the string variable. 
 * Usage: this method is used to get content.xlf file in our program.
 * 
 */

/* SAVING FILE TO XLZ */

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

/* UPDATING FILE IN XLZ */

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
        public static IEnumerable<ZipArchiveEntry> GetFilesByFullName(ZipArchive zipArchive, string search)
        {
            return zipArchive.Entries.Where(x => x.Name.ToLower() == search.ToLower());
        }

        public static IEnumerable<ZipArchiveEntry> GetFilesByName(ZipArchive zipArchive, string search)
        {
            return zipArchive.Entries.Where(x => x.Name.ToLower().Contains(search.ToLower()));
        }

        public static IEnumerable<ZipArchiveEntry> GetFilesByExtension(ZipArchive zipArchive, string search)
        {
            return zipArchive.Entries.Where(x => x.Name.ToLower().EndsWith("." + search.ToLower().Trim()));
        }

        public static IEnumerable<ZipArchiveEntry> GetFilesByExtension(ZipArchive zipArchive, string search, int searchOption)
        {
            IEnumerable<ZipArchiveEntry> foundFiles = null;

            switch (searchOption)
            {
                case 1:
                    foundFiles = GetFilesByFullName(zipArchive, search);
                    return foundFiles;
                case 2:
                    foundFiles = GetFilesByName(zipArchive, search);
                    return foundFiles;
                case 3:
                    foundFiles = GetFilesByExtension(zipArchive, search);
                    return foundFiles;
                default:
                    return foundFiles;
            }
        }

        /* READING FILE FROM XLZ */

        /* 
         * 
         */
        public static List<string> ReadFilesFromXLZ(string xlzFile, string search, int searchOption, bool ifDelete)
        {
            List<string> foundFilesStringList = new List<string>();
            string foundFileString = String.Empty;

            try
            {
                using (ZipArchive xlzArchive = ZipFile.Open(xlzFile, ZipArchiveMode.Update))
                {

                    IEnumerable<ZipArchiveEntry> foundFiles = GetFilesByExtension(xlzArchive, search, searchOption);                  

                    if (foundFiles.Count() > 0)
                    {   
                        if (foundFiles.Count() == 1)
                        {
                            using (Stream stream = foundFiles.ElementAt(0).Open())
                            {

                                StreamReader reader = new StreamReader(stream);
                                foundFileString = reader.ReadToEnd();

                                foundFilesStringList.Add(foundFileString);

                            }

                            if(ifDelete)
                            {
                                foundFiles.ElementAt(0).Delete();
                            }
                        }
                        else
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

                            if (ifDelete)
                            {
                                throw new Exception(String.Format("There are mutliple files found. The archive is not valid."));
                            }
                        }                            
                    }
                    else
                    {
                        switch (searchOption)
                        {
                            case 1:
                                throw new Exception(String.Format("Please check your file: {0} - file of the name {1} is missing.", Path.GetFileName(xlzFile), search));
                            case 2:
                                throw new Exception(String.Format("Please check your file: {0} - file containing a name {1} is missing.", Path.GetFileName(xlzFile), search));
                            case 3:
                                throw new Exception(String.Format("Please check your file: {0} - file of the extension {1} is missing.", Path.GetFileName(xlzFile), search));
                            default:
                                throw new Exception(String.Format("Search option not supported."));
                        }
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
        public static List<string> ReadFilesFromXLZByFullName(string xlzFile, string fileName)
        {
            return ReadFilesFromXLZ(xlzFile, fileName, 1, false);
        }

        /* 
         * 
         */
        public static List<string> ReadFilesFromXLZByName(string xlzFile, string fileName)
        {
            return ReadFilesFromXLZ(xlzFile, fileName, 2, false);
        }

        /* 
         * 
         */
        public static List<string> ReadFilesFromXLZByExtension(string xlzFile, string fileExtension)
        {
            return ReadFilesFromXLZ(xlzFile, fileExtension, 3, false);
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

        /* SAVE FILE TO XLZ */

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
            SaveFileToXLZ(xlzFile, "skeleton.skl", fileContent);
        }

        /* DELETING FILE FROM XLZ */

        /* 
         * 
         */
        public static string DeleteFileFromXLZByFullName(string xlzFile, string fileName)
        {
            List<string> foundFiles = ReadFilesFromXLZ(xlzFile, fileName, 1, true);
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
        public static string DeleteFileFromXLZByName(string xlzFile, string fileName)
        {
            List<string> foundFiles = ReadFilesFromXLZ(xlzFile, fileName, 2, true);
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
        public static string DeleteFileFromXLZByExtension(string xlzFile, string fileExtension)
        {
            List<string> foundFiles = ReadFilesFromXLZ(xlzFile, fileExtension, 3, true);
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
        public static string DeleteContentXLF(string xlzFile)
        {
            return DeleteFileFromXLZByFullName(xlzFile, "content.xlf");
        }

        /* 
         * 
         */
        public static string DeleteSkeletonSKL(string xlzFile)
        {
            return DeleteFileFromXLZByFullName(xlzFile, "skeleton.skl");
        }

        /* UPDATE FILE IN XLZ */

        /* 
         * 
         */
        public static string UpdateFileFromXLZByFullName(string xlzFile, string fileName, string updatedFileContent)
        {

            DeleteFileFromXLZByFullName(xlzFile, fileName);
            SaveFileToXLZ(xlzFile, fileName, updatedFileContent);

            return ReadFileFromXLZByFullName(xlzFile, fileName);
        }

        /* 
         * 
         */
        public static string UpdateFileFromXLZByName(string xlzFile, string fileName, string updatedFileContent)
        {

            DeleteFileFromXLZByName(xlzFile, fileName);
            SaveFileToXLZ(xlzFile, fileName, updatedFileContent);

            return ReadFileFromXLZByName(xlzFile, fileName);
        }

        /* 
         * 
         */
        public static string UpdateFileFromXLZByExtension(string xlzFile, string fileExtension, string updatedFileContent)
        {

            DeleteFileFromXLZByExtension(xlzFile, fileExtension);
            SaveFileToXLZ(xlzFile, fileExtension, updatedFileContent);

            return ReadFileFromXLZByExtension(xlzFile, fileExtension);
        }

        /* 
         * 
         */
        public static string UpdateContentXLF(string xlzFile, string updatedFileContent)
        {
            DeleteFileFromXLZByFullName(xlzFile, "content.xlf");
            SaveFileToXLZ(xlzFile, "content.xlf", updatedFileContent);

            return ReadFileFromXLZByFullName(xlzFile, "content.xlf");
        }

        /* 
         * 
         */
        public static string UpdateSkeletonSKL(string xlzFile, string updatedFileContent)
        {

            DeleteFileFromXLZByFullName(xlzFile, "skeleton.skl");
            SaveFileToXLZ(xlzFile, "skeleton.skl", updatedFileContent);

            return ReadFileFromXLZByFullName(xlzFile, "skeleton.skl");
        }
    }
}
