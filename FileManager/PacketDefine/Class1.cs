using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PacketDefine
{
    public enum PacketType
    {
        init = 0,
        beforeSelect,
        beforeExpand,
        fileTransfer,
        exitConnection
    }

    public enum PacketSendERROR
    {
        normal = 0,
        error
    }

    [Serializable]
    public class Packet
    {
        public int Length;
        public int Type;

        public Packet()
        {
            this.Length = 0;
            this.Type = 0;
        }

        public static byte[] Serialize(Object o)
        {
            MemoryStream ms = new MemoryStream(1024 * 4);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, o);
            return ms.ToArray();
        }

        public static Object Desserialize(byte[] bt)
        {
            MemoryStream ms = new MemoryStream(1024 * 4);
            foreach (byte b in bt)
            {
                ms.WriteByte(b);
            }

            ms.Position = 0;
            BinaryFormatter bf = new BinaryFormatter();
            Object obj = bf.Deserialize(ms);
            ms.Close();
            return obj;
        }
    }

    [Serializable]
    public class Initialize : Packet
    {
        public string path = "";
    }

    [Serializable]
    public class BeforeSelect : Packet
    {
        public string path = "";
        public DirectoryInfo[] diArray;
        public FileInfo[] fiArray;
    }

    [Serializable]
    public class BeforeExpand : Packet
    {
        public string path = "";
        public DirectoryInfo[] diArray;
        public Dictionary<string, int> diAdd;
    }

    [Serializable]
    public class FileTransfer : Packet
    {
        public string path;
        public long size;
    }

    [Serializable]
    public class ExitConnection : Packet
    {
        public string path;
    }
}
