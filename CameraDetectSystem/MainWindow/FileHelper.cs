using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PylonC.NETSupportLibrary;
using System.Diagnostics;
using System.Drawing.Imaging;
using HalconDotNet;
using System.IO.Ports;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Timers;
using System.Data.SQLite;
using System.Xml;
using System.Collections;
namespace CameraDetectSystem
{
    class FileHelper
    {

        public static void LoadProductData(DevExpress.XtraGrid.Views.Grid.GridView gridview,DevExpress.XtraGrid.GridControl grid,DataSet ds,string path)
        {
            path += @"\Product.xml";
            ds.ReadXml(path);

            gridview.ExpandMasterRow(0);

            for (int i = 0; i < gridview.RowCount; i++)
            {
                gridview.ExpandMasterRow(i);
            }

        }

        public static void LoadCameraToolsData(List<CCamera> cameras, string path)
        {
            foreach (CCamera c in cameras)
            {
                c.imageTools.Clear();
                string spath = path + "\\" + c.logicName + ".dat";
                c.imageTools = ToolBox.ReadFromXml(spath);
            }
            //imageTools = ToolBox.ReadFromXml(imageTools, ".\\tools.dat");
        }

        public static void LoadCameraResultToShowData(List<CCamera> cameras, string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode xn;
            XmlNodeList xnl;
            foreach (CCamera c in cameras)
            {
                c.dataSelectedShowed.Clear();
                xmlDoc.Load(path + "\\" + c.logicName + ".xml");
                xn = xmlDoc.GetElementsByTagName("结果")[0];
                xnl = xn.ChildNodes;
                foreach (XmlNode x in xnl)
                {
                    DataSelected data = new DataSelected();
                    data.name = x.Name;
                    if (x.Attributes["IsToShow"].Value == "true")
                        data.istoshow = true;
                    else
                        data.istoshow = false;
                    c.dataSelectedShowed.Add(data);
                }
            }
        }

        public static void SaveProductData(DataSet ds,string path)
        {
            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(path+@"\Product.xml", false);
            ds.WriteXml(xmlSW);
            xmlSW.Close();
        }

        public static void SaveCameraToolsData(List<CCamera> cameras)
        {
            foreach (CCamera c in cameras)
            {
                ToolBox.SaveToXml(c.imageTools, PathHelper.currentProductPath + "\\" + c.logicName + ".dat");
            }
        }

        public static void SaveCameraResultToShowData(List<CCamera> cameras,string path)
            
        {
            
            foreach (CCamera c in cameras)
            {
                XmlDocument xmldoc = new XmlDocument();
                //加入一个根元素
                XmlElement xroot = xmldoc.CreateElement("", "结果", "");
                xmldoc.AppendChild(xroot);
                XmlNode root = xmldoc.SelectSingleNode("结果");

                foreach (DataSelected data in c.dataSelectedShowed)
                {
                    XmlElement xe1 = xmldoc.CreateElement(data.name);
                    if (data.istoshow)
                    {
                        xe1.SetAttribute("IsToShow", "true");
                    }
                    else
                    {
                        xe1.SetAttribute("IsToShow", "false");
                    }
                    root.AppendChild(xe1);
                }
                XmlDeclaration xmldecl;
                xmldecl = xmldoc.CreateXmlDeclaration("1.0", "gb2312", null);
                xmldoc.Save(path+@"\"+c.logicName+@".xml");
            }
          
            //MainForm frm1 = (MainForm)this.Owner;

            //string CCD=((DevExpress.XtraTab.XtraTabControl)frm1.Controls["xtraTabControl1"]).Text;
            //resultToShow.WriteToXML(PathHelper.currentProductPath + "\\" + frm1.CurrentCCD + ".xml", xmldoc);
        }
    }
}
