using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TRD_Splitter
{
    class TRD_File
    {
        private byte[] contents;
        private string name;
        private UInt16 length;

        public TRD_File(string name)
        {
            this.name = name;
        }

        public byte[] Contents
        {
            get
            {
                return this.contents;
            }
            set
            {
                this.contents = value;
            }
        }

        public void WriteContentsToFile()
        {
            try
            {
                BinaryWriter output = new BinaryWriter(File.OpenWrite(this.name));
                output.Write(this.contents);
                output.Close();
            }
            catch (Exception excep)
            {
                Console.WriteLine(excep.Message);
            }
        }
    }
}
