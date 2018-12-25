using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace CameraDetectSystem
{
    class PathHelper
    {
        public static string exepath;

        public static string productPath;

        public static string dataPath;

        public static List<string> productNames;

        public static Dictionary<string, string> pathDic;

        public static string currentProduct;
        public static string currentProductPath;
        public static void  initial()
        {
            exepath = Application.StartupPath  ;

            dataPath = exepath + @"\Data";

            productPath = dataPath + @"\Product";
            
        }
        public static void GetFiles()
        {
            initial();
            pathDic = new Dictionary<string, string>();

            DirectoryInfo di = new DirectoryInfo(productPath);

            DirectoryInfo[] Dis = di.GetDirectories();//获取子文件夹列表

            if (Dis.Length > 0)
            {
                productNames = new List<string>();
                foreach (DirectoryInfo d in Dis)
                {
                    pathDic.Add(d.Name, d.FullName);
                    productNames.Add(d.Name);
                }
            }
        }
        public static void CreateNewProduct(string ProductName)
        {
            initial();
            DirectoryInfo di = new DirectoryInfo(productPath);

            DirectoryInfo[] Dis = di.GetDirectories();
            currentProduct = ProductName;
            for(int i=0;i<Dis.Length;i++ )
            {
                if (Dis[i].Name == ProductName)
                {
                    MessageBox.Show("产品已存在");
                    MyDebug.ShowMessage("产品已存在");
                    return;
                }
            }
            Directory.CreateDirectory(productPath+@"\"+ProductName);
        }
     }
}
