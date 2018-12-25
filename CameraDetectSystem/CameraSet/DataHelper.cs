using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Xml;
namespace CameraDetectSystem
{
    [Serializable]
    class DataHelper
    {
        static Dictionary<string, int> CameraID;
        public static void Initial()
        {
            CameraID = new Dictionary<string, int>();
            CameraID.Add("CCD1", 1);
            CameraID.Add("CCD2", 2);
            CameraID.Add("CCD3", 3);
            CameraID.Add("CCD4", 4);
            CameraID.Add("CCD5", 5);
            //CameraID.Add("CCD6", 6);
        }
        ~DataHelper()
        {
            conn.Close();
        }
        public HTuple selectMode = new HTuple();
        public HTuple selectAndOrOr = new HTuple();
        public HTuple selectMaxValue = new HTuple();
        public HTuple selectMinValue = new HTuple();
        //
        static SQLiteConnection conn = new SQLiteConnection("Data Source="+PathHelper.exepath+@"\db.db;");
        static public DataSet DataBaseOpen()
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            DataSet ds = new DataSet();
            if (conn != null)
            {
                string sql = "SELECT * FROM 筛选项";
                SQLiteTransaction ts = conn.BeginTransaction();
                SQLiteDataAdapter dta = new SQLiteDataAdapter(sql, conn);
                SQLiteCommandBuilder scb = new SQLiteCommandBuilder(dta);
                dta.InsertCommand = scb.GetInsertCommand();
                dta.FillSchema(ds, SchemaType.Source, "Temp");    //加载表架构 //注意
                dta.Fill(ds, "Temp");    //加载表数据
                return ds;
            }
            else return null;
        }
        public void GetSelectModeData(DataGridView dgv)
        {
            int row = dgv.Rows.Count;//得到总行数    
            int col = dgv.Columns.Count;//得到总列数    

            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if ((dgv.Rows[j].Cells[0]).Value != null && (dgv.Rows[j].Cells[1]).Value != null && (dgv.Rows[j].Cells[2]).Value != null)
                    {
                        switch (i)
                        {
                            case 0:
                                {
                                    selectMode[j] = (dgv.Rows[j].Cells[0]).Value.ToString();
                                    selectAndOrOr[j] = "and";
                                }
                                break;
                            case 1:
                                {
                                    selectMinValue[j] = StringToHTuple((dgv.Rows[j].Cells[1]).Value.ToString());
                                }
                                break;
                            case 2: selectMaxValue[j] = StringToHTuple((dgv.Rows[j].Cells[2]).Value.ToString());
                                break;
                        }
                    }
                }
            }
        }
        private HTuple StringToHTuple(string str)
        {
            HTuple temp = new HTuple();
            double value;
            bool ok = double.TryParse(str, out value);
            temp = value;
            return temp;
        }
        public static void CheckData(DataSet ds, string cameraName, CCamera instance)
        {
            Initial();
            int current = int.Parse(cameraName.Substring(3, 1));
            string nextCameraName = "CCD" + (current + 1).ToString();
            DataRowCollection drc = ds.Tables["detailTable"].Rows;
            int checkresult = (int)Holes.firstHole;
            List<int> CheckList = new List<int>();

            int index = 0;
            foreach (DataRow dr in drc)
            {
                if (dr[0].ToString() == cameraName)
                {
                    double xz = (double)dr[2];
                    double sx = (double)dr[3];
                    double xx = (double)dr[4];
                    double jg = (double)dr[5];

                    //良品
                    if (jg >= xx && jg <= sx)
                    {
                        checkresult = (int)Holes.secondHole;
                        instance.goodcountlist[index]++;
                    }
                    //未识别
                    else if (jg == xz)
                    {
                        checkresult = (int)Holes.thirdHole;

                    }
                    
                    //不良品
                    else
                    {
                        checkresult = (int)Holes.firstHole;
                        instance.badcountlist[index]++;
                    }
                    CheckList.Add(checkresult);

                    index++;
                }
            }
            
            //不良品
             if (CheckList.Contains((int)Holes.firstHole))
            {
                checkresult = (int)Holes.firstHole;

            }
            //未识别
            else if (CheckList.Contains((int)Holes.thirdHole))
            {
                checkresult = (int)Holes.thirdHole;
            }
            //良品
            else if (CheckList.Contains((int)Holes.secondHole))
            {
                checkresult = (int)Holes.secondHole;
            }
            else
            {
                checkresult = (int)Holes.thirdHole;
            }
            if (CheckList.Count != 0)
            {
                //Card.mu.WaitOne();
                lock (Card.lockobj)
                {
                    //10.28ch改为逆向查找

                    Nut nut;
                    for (int i = 0; i < Turntable.Instance.nutqueue.Count; i++)
                    {
                        CameraCheckOut cco = new CameraCheckOut();
                        nut = Turntable.Instance.nutqueue[i];
                        int length = nut.checkedResult.Count;
                      //  Console.WriteLine("" + nut.checkedResult[length - 1].nextCameraName + "ff" + instance.poscmin + "ww" + nut.initialPos);
                        if (nut.checkedResult[length - 1].nextCameraName == cameraName && instance.poscmin == nut.initialPos)
                            //if (nut.posNo == CameraID[cameraName])
                        {
                            cco.leadToTheHole = checkresult;
                            cco.nextCameraName = nextCameraName;
                         //   Console.WriteLine("" + cameraName + "" + nut.initialPos);
                            nut.checkedResult.Add(cco);
                            jiancejieguo jcjg1 = new jiancejieguo();
                            foreach (DataRow drx in drc)
                            {
                                if (drx[0].ToString() == cameraName)
                                {
                                    jcjg1 = new jiancejieguo();
                                    jcjg1.jiancexiangmu = drx[1].ToString();
                                    jcjg1.celiangjieguo = (double)drx[5];
                                    nut.jiance.Add(jcjg1);
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
    public class ProductNum
    {
        public  ProductNum()
        {
            goodNum = 0;
            badNum = 0;
            countPerMinitor=0;
            totalCount=0;
        }
        public int goodNum{set;get;}
        //public int mnum { set; get; }
        //public int nnum { set; get; }
        public int badNum { set { _badNum = value; } get { return _badNum; } }
        private int _badNum;
        public int totalCount
        {
            set;
            get;
        }
        public double countPerMinitor;
        public double goodPer()
        {
            if (totalCount > 0)
                return Math.Round((double)goodNum / totalCount,3);
            else
                return 0;
        }
    }
     public struct IData
     {
         public string name;
         public string data;
     }
     public struct DataSelected
     {
         public string name;
         public bool istoshow;
     }
     public class Result
     {
         //public List<IData> IDataResult;
         public HTuple HTupleResult;
         public List<bool> isShow = new List<bool>();
         public List<IData> resultToShow;
         public List<IData> resultToShowtemp;
         public Result()
         {
             resultToShow = new List<IData>();
             resultToShowtemp = new List<IData>();
         }
         public  void GetResult(HTuple tuple)
         {
             HTupleResult = tuple;
             string[] strs;
             //resultToShow = new List<IData>();
             resultToShowtemp = new List<IData>();
             strs = tuple.ToSArr();
             for (int i = 0; i < strs.Length / 2; i++)
             {
                 IData data=new IData();
                 ListViewItem item = new ListViewItem();
                 data.name = strs[i * 2];
                 data.data = strs[i * 2 + 1];
                 resultToShowtemp.Add(data);
             }   
         }
         public List<IData> GetResultToShow( List<DataSelected> data)
         {
             resultToShow = new List<IData>();
             for (int i = 0; i < data.Count; i++)
             {
                 if (data[i].istoshow)
                 {
                     
                     IData d = new IData();
                     d.name = data[i].name;
                     d.data = resultToShowtemp[i].data.ToString();
                     resultToShow.Add(d);
                     //if (resultToShowtemp.Count>i)
                     //resultToShowtemp.RemoveAt(0);
                 }
             }
             return resultToShow;
         }
         public void WriteToXML(string path,XmlDocument xmldoc)
         {
             //XmlDocument xmldoc = new XmlDocument();
             ////加入XML的声明段落,<?xml version="1.0" encoding="gb2312"?>
             XmlDeclaration xmldecl;
             xmldecl = xmldoc.CreateXmlDeclaration("1.0", "gb2312", null);
             
             ////加入一个根元素
             //XmlElement xroot = xmldoc.CreateElement("", "结果", "");
             //xmldoc.AppendChild(xroot);
             //XmlNode root = xmldoc.SelectSingleNode("结果");
             //foreach (IData data in result)
             //{
             //    XmlElement xe1 = xmldoc.CreateElement(data.name);//创建一个<Node>节点 
             //    xe1.SetAttribute("Data", data.data);//设置该节点genre属性 
             //    root.AppendChild(xe1);
             //}
             xmldoc.Save(path);
         }  
     }
     enum Holes { firstHole = 1, secondHole = 2, thirdHole = 3, fourthHole = 4 };
     //class DataToShow
     //{
     //    public List<IData> dataToShow;
     //    public void Add(List<IData> data)
     //    {
     //        dataToShow.AddRange(data);
     //    }
     //    public void Add(IData data)
     //    {
     //        dataToShow.Add(data);
     //    }

     //}
}
