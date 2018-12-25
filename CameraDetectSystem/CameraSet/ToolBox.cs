using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace CameraDetectSystem
{
    public class ToolBox
    {
        //static string path=".\\ToolsInfo.dat";

        static public  void SaveToXml(List<ImageTools> list,string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            //BinaryFormatter bf = new BinaryFormatter(); 
            bf.Serialize(fs, list);
            fs.Close();
        }
        static public List<ImageTools> ReadFromXml(string path)
        {
            List<ImageTools> list=new List<ImageTools>();
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            //XmlSerializer xs = new XmlSerializer(typeof(List<ImageTools>));
            BinaryFormatter bf = new BinaryFormatter();
            
            list= (List<ImageTools>)bf.Deserialize(fs);
            fs.Close();
            return list;
            
        }
    }
}
