using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TRD_Splitter
{
    class MainClass
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                try
                {
                    BinaryReader TRD_FileToSplit = new BinaryReader(File.OpenRead(args[0]));
                    Directory.CreateDirectory("ExtractedFiles/");
                    Directory.SetCurrentDirectory("ExtractedFiles/");
                    byte[] diskInfo = new byte[255];
                    TRD_FileToSplit.BaseStream.Position = 8 * 256;
                    TRD_FileToSplit.Read(diskInfo, 0, 255);

                    int numberOfFiles = diskInfo[228];

                    byte[] fileInfo = new byte[16];

                    byte[] contents;
                    string name;
                    UInt16 length;

                    TRD_File[] files = new TRD_File[numberOfFiles];

                    for (int i = 0; i < files.Length; i++)
                    {
                        TRD_FileToSplit.BaseStream.Position = i * 16;
                        TRD_FileToSplit.Read(fileInfo, 0, 16);

                        name = Encoding.UTF8.GetString(fileInfo, 0, 8).Trim();
                        name += "." + Encoding.UTF8.GetString(fileInfo, 8, 1);

                        length = (UInt16)(fileInfo[11] | (fileInfo[12] << 8));

                        files[i] = new TRD_File(name);

                        contents = new byte[length];
                        TRD_FileToSplit.BaseStream.Position = fileInfo[15] * 16 * 256 + fileInfo[14] * 256;
                        TRD_FileToSplit.Read(contents, 0, length);

                        files[i].Contents = contents;

                        files[i].WriteContentsToFile();
                    }

                    Console.WriteLine("Files read successfully!");

                    TRD_FileToSplit.Close();

                }
                catch (Exception excep)
                {
                    Console.WriteLine(excep.Message);
                }
            }
            else
            {
                Console.WriteLine("Cannot open the file!");
            }
            Console.ReadKey(true);
        }
    }
}
