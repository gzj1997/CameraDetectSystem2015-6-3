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
using Basler.Pylon;
using System.Collections;
using System.Runtime.InteropServices;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Text;
using DevExpress.XtraGrid.Views.Grid;
using CameraDetectSystem;




namespace CameraDetectSystem
{
    
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        //public List<ImageTools> imageTools1, imageTools2, imageTools3, imageTools4;

        System.Windows.Forms.Timer MSStartTimer = new System.Windows.Forms.Timer();

        System.Windows.Forms.Timer MSStoptTimer = new System.Windows.Forms.Timer();

        Turntable table;

        public List<CCamera> ccameras;
        DataSet ds;
        DataTable dt;

        ProductNum pn;
        public string prpnm = "";
        public string zyw;
        bool isfirstrun;

        public MainForm()
        {
            //将当前线程绑定到指定的cpu核心上
            GetCpu.SetThreadAffinityMask(GetCpu.GetCurrentThread(), new UIntPtr(0x0008));

            InitializeComponent();
            //Control.CheckForIllegalCrossThreadCalls = true;
            pn = new ProductNum();
            
            isfirstrun = true;
            //HOperatorSet.GenEmptyObj(out Image1);
            //HOperatorSet.GenEmptyObj(out Image2);
            //HOperatorSet.GenEmptyObj(out Image3);
            //HOperatorSet.GenEmptyObj(out Image4);
            
            //Image1.Dispose();
            //Image2.Dispose();
            //Image3.Dispose();
            //Image4.Dispose();

            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(gridView1_RowCellStyle);
            
            HOperatorSet.GenEmptyObj(out ImageTemp);
            ImageTemp.Dispose();

            ccameras = new List<CCamera>();
            CCD = new Dictionary<string, CCamera>();
            ds = new DataSet();
            dt = new DataTable("datacopy");
            CurrentCCD = "CCD1";
            MSStartTimer.Tick += new EventHandler(MSStartTimer_OnTime);
            MSStoptTimer.Tick += new EventHandler(MSStoptTimer_OnTime);
            InitialDataGrid(gridControlResult, ds);
            ReadCameraSetting();
           
            FormLoadProduct f = new FormLoadProduct();
            
            //Thread th = new Thread(new ThreadStart(DoSplash));
            //th.Start();
            //Thread.Sleep(3000);
            //th.Abort();
            //Thread.Sleep(1000);
            //if (splashResult == DialogResult.Cancel)
            //{
            //    this.Close();
            //}
            DialogResult dialogr=f.ShowDialog();
            if (dialogr== DialogResult.OK)
            {
                try
                {
                    FileHelper.LoadCameraResultToShowData(ccameras, PathHelper.currentProductPath);
                    FileHelper.LoadProductData(gridView1, gridControlResult, ds, PathHelper.currentProductPath);
                    FileHelper.LoadCameraToolsData(ccameras, PathHelper.currentProductPath);
                    labelpname.Text = PathHelper.currentProduct;
                }
                catch
                {
                    MessageBox.Show("相机或产品信息加载错误");
                }
            }
            else if (dialogr==DialogResult.Cancel)
            {
                this.Close();
            }
            
            string path = @"c:\zyw.txt";
            if (!File.Exists(path))
            {
                File.AppendAllText(path, "zhongwen", Encoding.Default);
            }
            zyw = File.ReadAllText(path, Encoding.Default);
            if (zyw == "zhongwen")
            {
                label1.Text = "产品名称：";
                simpleButton1.Text = "生成报表";
                BtnSaveParam.Text = "保存参数";
                BtnSaveNewProduct.Text = "保存新产品";
                BtnControlSet.Text = "控制设定";
                BtnLogin.Text = "登录";
                BtnLoadProduct.Text = "计数清零";
                BtnSetting.Text = "设定";
                BtnStop.Text = "停止";
                BtnStart.Text = "启动";
            }
            if (zyw == "yinwen")
            {
                label1.Text = "productname:";
                simpleButton1.Text = "GenerateReport";
                BtnSaveParam.Text = "SaveParameter";
                BtnSaveNewProduct.Text = "SaveNewProduct";
                BtnControlSet.Text = "ControlSet";
                BtnLogin.Text = "Login";
                BtnLoadProduct.Text = "ResetAllCounters";
                BtnSetting.Text = "Setting";
                BtnStop.Text = "Stop";
                BtnStart.Text = "Start";
            }
            Turntable.Instance.ReadPos(PathHelper.dataPath+@"\Position.xml");
           // Turntable.Instance.OnUpdateProductInfoEvent += new Turntable.UpdateProductInfo(UpdateProInfo);
            Turntable.Instance.StrongPressEventHandler+=new Turntable.StrongPress(Instance_StrongPressEventHandler);
            Turntable.Instance.wuliao += new Turntable.wuliaohandler(wuliaotingzhiEventHandler);
            
        }
        
        public string CurrentCCD;

        Dictionary<string, CCamera> CCD;

         static HObject ImageTemp = null;

         HObject Temp;
        
        private void BtnSetting_Click(object sender, EventArgs e)
         {
             if (PathHelper.currentProductPath == null)
             {
                 if (zyw == "zhongwen")
                 {
                     MessageBox.Show("未建立新产品");
                     return;
                 }
                 else
                 {
                     MessageBox.Show("No New Product");
                     return;
                 }
             }
            try
            {
                CameraSetForm CSF = new CameraSetForm();

                //HOperatorSet.CopyImage(ImageTemp, out CSF.Image);


                HOperatorSet.GenEmptyObj(out Temp);

                Temp.Dispose();

                //if (ImageTemp == null)
                //{
                //    return;
                //}
                //HOperatorSet.CopyImage(ImageTemp, out Temp);
                if (CCD.Count>0)
                {
                    HOperatorSet.CopyImage(CCD[CurrentCCD].tempImage, out Temp);
                    CCD[CurrentCCD].dataSelectedShowed.Clear();

                    CSF.Image = Temp;
                }

                CSF.pixelDist = CCD[CurrentCCD].PixelDist;
                CSF.ShowDialog(this);
                if (CSF.Ismodify && PathHelper.currentProductPath!=null)
                {
                   
                    CCD[CurrentCCD].imageTools = CSF.imageTools;
                    UpdataDataSettings(gridControlResult, ds);
                    FileHelper.SaveCameraResultToShowData(ccameras, PathHelper.currentProductPath);
                    FileHelper.SaveProductData(ds, PathHelper.currentProductPath);
                    FileHelper.LoadCameraResultToShowData(ccameras, PathHelper.currentProductPath);
                }
            }
            catch (Exception ex)
            {
                MyDebug.ShowMessage("CameraSetFormError:="+ex.Message);
            }
           // ReadCameraSetting();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            BtnStart.Enabled = false;
            try
            {
                DataRowCollection drc = ds.Tables["detailTable"].Rows;

                foreach (CCamera c in ccameras)
                {
                    if (c.isfirst == true)
                    {

                        c.Open();
                        //c.isfirst = false;

                    }
                    //c.SetModeFeature("TriggerMode", "On");
                    if (c.isopen)
                    {
                        c.StartGrab();
                        int count = 0;
                        int tempcount = c.goodcountlist.Count;
                         foreach (DataRow dr in drc)
                            {
                               if (dr[0].ToString() == c.logicName)
                                 {
                                     count++;
                                     if (count > tempcount)
                                     {
                                         c.goodcountlist.Add(0);
                                         c.badcountlist.Add(0);
                                     
                                     }
                                  
                                  }
                            }
                    }
                    else
                    {
                        ccameras.Remove(c);
                    }
                }
                //玻璃盘启动
                if (isfirstrun)
                {
                PCI408.PCI408_set_encoder(0,0);
                Turntable.Instance.isStart = false;
                isfirstrun = false;
                }
                timer1.Interval = 1000;
                timer1.Enabled = true;
                timer2.Interval = 50;
                timer2.Enabled = true;
                MSStartTimer.Interval = 5000;
                MSStartTimer.Start();
                Turntable.Instance.Start();
              //  PCI408.PCI408_write_outbit(Card.cardNo, 5, Card.On);
            }
            catch (Exception ex)
            {
                MyDebug.ShowMessage(ex, "start error");
            }
        }

        private void MSStartTimer_OnTime(object sender, EventArgs e)
        {
            
            //振动盘直震启动
            Turntable.Instance.startShakePan();
            MSStartTimer.Enabled = false;
            MSStartTimer.Enabled = false;
            PCI408.PCI408_write_outbit(Card.cardNo, Card.hd, Card.Off);
            PCI408.PCI408_write_outbit(Card.cardNo, Card.ld, Card.On);
            //PCI408.PCI408_write_outbit(Card.cardNo, Card.ssd, Card.On);
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            Turntable.Instance.stopShakePan();
           // timer1.Enabled = false;
            timer1.Enabled = false;
            MSStoptTimer.Interval = 20000;
            MSStoptTimer.Enabled = true;
            MSStartTimer.Enabled = false;
            
            
        }

        private void MSStoptTimer_OnTime(object sender, EventArgs e)
        {
           
            //振动盘直震停止
            BtnStart.Enabled = true;
            //玻璃盘停止
            Turntable.Instance.Stop();
            MSStoptTimer.Enabled = false;
            for (int xjh = 0; xjh < Turntable.Instance.PosArray.Count(); xjh++)
            {
                PCI408.PCI408_compare_clear_points_Extern(0, (ushort)(xjh + 1));
                PCI408.PCI408_compare_config_Extern(Card.cardNo, (ushort)(xjh + 1), 1, Card.zhouhao, Turntable.Instance.IOs[xjh]);
                PCI408.PCI408_compare_set_pulsetimes_Extern(0, (ushort)(xjh + 1), 20);
            }
            foreach (var c in ccameras)
            {
                if (c.isopen)
                {
                    c.StopGrabbing();
                    c.isfirst = false;
                }
            }
            Turntable.Instance.isStart = false;
            Turntable.Instance.chuchushuju();
        
            PCI408.PCI408_write_outbit(Card.cardNo, Card.hd, Card.On);
            PCI408.PCI408_write_outbit(Card.cardNo, Card.ld, Card.Off);
            //PCI408.PCI408_write_outbit(Card.cardNo, Card.ssd, Card.Off);
        }

        private void ReadCameraSetting()
        {
            //cameras.Clear();
            try
            {
                XmlDocument xmlDocCamera = new XmlDocument();
                xmlDocCamera.Load("./Data/" + "CameraSettings.xml");
                XmlNodeList xnl;
                XmlNode xn;
                xn = xmlDocCamera.GetElementsByTagName("Camera")[0];
                xnl = xn.ChildNodes;
                int i = 1;
                if (ccameras.Count == 0)
                {
                    foreach (XmlNode x in xnl)
                    {
                        CCamera c = new CCamera(x.Attributes["name"].Value);
                        c.sortnum = i - 1;
                        c.logicName = "CCD" + i;
                        c.name = x.Attributes["name"].Value;
                        c.ExposureTime = int.Parse(x.Attributes["ExposureTime"].Value);
                        c.Gain = int.Parse(x.Attributes["Gain"].Value);
                        c.PixelDist = double.Parse(x.Attributes["PixelDist"].Value);
                        c.OnImageProcessedEvent += new CCamera.OnImageProcessedEventHandler(OnImageProcessedEvent);
                        ccameras.Add(c);
                        i++;
                    }
                }
                for (int ci = 0; ci < ccameras.Count; ci++)
                {
                    CCD.Add("CCD" + (ci + 1).ToString(), ccameras[ci]);
                }
            }
            catch
            {
                MessageBox.Show("相机加载错误");
            }
        }

        private void InitialDataGrid(DevExpress.XtraGrid.GridControl grid,DataSet ds)
        {
            // 定义主表
            DataTable masterTable = new DataTable("CameraTable");
            DataTable detailTable = new DataTable("DetailTable");
            DataColumn parentColumn = new DataColumn("CameraID", typeof(string));
            masterTable.Columns.Add(parentColumn);
   
            DataColumn childrenColumn = new DataColumn("CCD", typeof(string));
            detailTable.Columns.Add(childrenColumn);

            //DataColumn[] cols = new DataColumn[] { childrenColumn };

            //detailTable.PrimaryKey = cols;
            detailTable.Columns.AddRange(new DataColumn[] {  
                new DataColumn("检测项（Feature Item）",typeof(string)),  
             new DataColumn("修正（Calibration）",typeof(double)),  
             new DataColumn("上限（Max）",typeof(double)),  
             new DataColumn("下限（Min）",typeof(double)),
             new DataColumn("测量（Measure）",typeof(double)),  
              new DataColumn("OK/NG",typeof(string)),
             new DataColumn("良率",typeof(string)),
            });
            //masterTable.Rows.Add("CCD1");
            //masterTable.Rows.Add("CCD2");
            //masterTable.Rows.Add("CCD3");

            for (int i = 0; i < ccameras.Count; i++)
            {
                masterTable.Rows.Add(ccameras[i].logicName);
            }
            // 定义子表
         
            //detailTable.Rows.Add("CCD1", 1, 1, 1);
            //detailTable.Rows.Add("CCD2", 2, 2, 2);
            //detailTable.Rows.Add("CCD2", 3, 3, 3);
            //detailTable.Rows.Add("CCD2", 4, 4, 4);
            //detailTable.Rows.Add("CCD2", 5, 5, 5);
            //detailTable.Rows.Add("CCD3", 6, 6, 6);
            //detailTable.Rows.Add("CCD3", 7, 7, 7);
            //detailTable.Rows.Add("CCD3", 8, 8, 8);

            for (int i = 0; i < ccameras.Count; i++)
            {
                foreach (DataSelected data in ccameras[i].dataSelectedShowed)
                {
                    if (data.istoshow)
                        detailTable.Rows.Add(ccameras[i].logicName, data.name, 0, 0, 0, 0, "", "", "");
                }
            }

            
            {
               
                ds.Tables.AddRange(new DataTable[] { masterTable.Copy(), detailTable.Copy() });
                // 创建表关系  
                DataRelation relation = new DataRelation("detailTable", ds.Tables[0].Columns[0], ds.Tables[1].Columns[0], false);
                
                ds.Relations.Add(relation); // 添加  

                grid.DataSource = ds.Tables[1];  // 指定数据源  
                ds.Clear();
                //LoadProductData(grid, ds,);
                //ds.Clear();
                //ds.ReadXml("Data/data1.xml");
                //grid.DataSource = ds.Tables[0];

                gridView1.ExpandMasterRow(0);

                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    gridView1.ExpandMasterRow(i);
                }

            }
            this.gridView1.Columns[0].Width = 50;
            this.gridView1.Columns[1].Width = 200;
            this.gridView1.Columns[2].Width = 70;
            this.gridView1.Columns[3].Width = 100;
            this.gridView1.Columns[4].Width = 100;
            this.gridView1.Columns[5].Width = 100;
            this.gridView1.Columns[6].Width = 40;
            this.gridView1.Columns[7].Width = 70;
        }

        private void UpdataDataSettings(DevExpress.XtraGrid.GridControl grid, DataSet ds)
        {
            DataTable ser = ds.Tables["detailTable"].Copy();
            DataRowCollection drcv = ser.Rows;
            ds.Clear();
            FileHelper.LoadCameraResultToShowData(ccameras, PathHelper.currentProductPath);
            DataTable masterTable = new DataTable("CameraTable");
            DataTable detailTable = new DataTable("DetailTable");
            masterTable = ds.Tables["CameraTable"];
            detailTable = ds.Tables["DetailTable"];
            for (int i = 0; i < ccameras.Count; i++)
            {
                masterTable.Rows.Add(ccameras[i].logicName);
            }
            for (int i = 0; i < ccameras.Count; i++)
            {
                int j = i + 1;
                if ("CCD" + j.ToString() == CurrentCCD)
                {
                    foreach (DataSelected data in ccameras[i].dataSelectedShowed)
                    {
                        if (data.istoshow)
                            detailTable.Rows.Add(ccameras[i].logicName, data.name, 0, 0, 0, 0);
                    }
                }
                else
                {
                    foreach (DataRow dr in drcv)
                    {
                        string ste = dr[0].ToString();
                        if (ste == "CCD" + j.ToString())
                        {
                            detailTable.Rows.Add(ccameras[i].logicName, dr[1].ToString(), dr[2], dr[3], dr[4], dr[5]);
                        }
                    }
                }
            }

            gridView1.ExpandMasterRow(0);

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                gridView1.ExpandMasterRow(i);
            }
        }
        private void SaveDataSettings(DevExpress.XtraGrid.GridControl grid,string path)
        {
            System.IO.StreamWriter xmlSW = new System.IO.StreamWriter(path,false);
            ds.WriteXml(xmlSW);
            xmlSW.Close();
        }
        //object obj = new object();
        int kk = 0;
        private void OnImageProcessedEvent(CCamera instance, HObject ho_Image)
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new CCamera.OnImageProcessedEventHandler(OnImageProcessedEvent), new object[] { instance, ho_Image });
                    return;
                }
                kk++;
                 //Console.WriteLine("kk:" +kk);
                    instance.tempImage.Dispose();
                    HOperatorSet.CopyImage(instance.Image, out instance.tempImage);

                    HTuple cwindow = new HTuple();
                    switch (instance.logicName)
                    {
                        case "CCD1": cwindow = hWindowControl1.HalconWindow;
                            break;
                        case "CCD2": cwindow = hWindowControl2.HalconWindow;
                            break;
                        case "CCD3": cwindow = hWindowControl3.HalconWindow;
                            break;
                        case "CCD4": cwindow = hWindowControl4.HalconWindow;
                            break;
                        case "CCD5": cwindow = hWindowControl5.HalconWindow;
                            break;
                        //case "CCD6": cwindow = hWindowControl6.HalconWindow;
                        //    break;
                    }
                    
                    for (int i = 0; i < 6; i++)
                    {
                        if (instance.logicName == "CCD" + i.ToString())
                        {
                            HOperatorSet.SetColor(cwindow, "green");
                            HOperatorSet.SetDraw(cwindow, "margin");
                            HTuple w, h;
                            HOperatorSet.GetImageSize(ho_Image, out w, out h);

                            HOperatorSet.SetPart(cwindow, 0, 0, h, w);

                            HOperatorSet.DispObj(ho_Image, cwindow);

                            if (instance.RegionToDisp.IsInitialized() && instance.resultHTuple.Length>0)
                            {
                                HOperatorSet.DispObj(instance.RegionToDisp, cwindow);
                                HalconHelp.set_display_font(cwindow, 14, "courier", "false", "false");
                                HalconHelp.disp_message(cwindow, instance.resultHTuple, "window", 10, 10, "green", "false"); 
                            }
                        }
                    }
                    switch (instance.logicName)
                    {
                        case "CCD1": cwindow = hWindowControl6.HalconWindow;
                            break;
                        case "CCD2": cwindow = hWindowControl7.HalconWindow;
                            break;
                        case "CCD3": cwindow = hWindowControl8.HalconWindow;
                            break;
                        case "CCD4": cwindow = hWindowControl9.HalconWindow;
                            break;
                        case "CCD5": cwindow = hWindowControl10.HalconWindow;
                            break;
                    }

                    for (int i = 0; i < 6; i++)
                    {
                        if (instance.logicName == "CCD" + i.ToString())
                        {
                            HOperatorSet.SetColor(cwindow, "green");
                            HOperatorSet.SetDraw(cwindow, "margin");
                            HTuple w, h;
                            HOperatorSet.GetImageSize(ho_Image, out w, out h);

                            HOperatorSet.SetPart(cwindow, 0, 0, h, w);

                            HOperatorSet.DispObj(ho_Image, cwindow);

                            if (instance.RegionToDisp.IsInitialized() && instance.resultHTuple.Length > 0)
                            {
                                HOperatorSet.DispObj(instance.RegionToDisp, cwindow);
                                //if (instance.logicName == "CCD1")
                                //{
                                //    HalconHelp.set_display_font(cwindow, 14, "courier", "false", "false");
                                //    HalconHelp.disp_message(cwindow, instance.resultHTuple, "window", 2, 2, "green", "false");
                                //}
                            }
                        }
                    }
                    DataRowCollection drc = ds.Tables["detailTable"].Rows;

                    int rs = 0;
                    string shijian=DateTime.Now.Year.ToString()+DateTime.Now.Month.ToString()+DateTime.Now.Day.ToString();
                    string path = PathHelper.currentProductPath + @"\" + shijian + ".txt";
                    foreach (DataRow dr in drc)
                    {
                        string s = dr[0].ToString();
                        
                        if (dr[0].ToString() == instance.logicName)
                        {
                            if (instance.result.resultToShow.Count > rs)
                            {
                                double cl = double.Parse(instance.result.resultToShow[rs].data);
                                double xz = (double)dr[2];
                                dr[5] = Math.Round((cl + xz), 3);
                                if (double.Parse(dr[5].ToString()) <= double.Parse(dr[3].ToString()) && double.Parse(dr[5].ToString()) >= double.Parse(dr[4].ToString()))
                                {
                                    dr[6] = "OK";
                                }
                                else
                                {
                                    dr[6] = "NG";


                                }
                                if (instance.goodcountlist.Count()!=0)
                                {
                                dr[7] = Math.Round((double)(instance.goodcountlist[rs] / (double)Turntable.Instance.pn.totalCount * 100), 2).ToString() + "%";
                                //Console.WriteLine("" + instance.goodcountlist[rs] + "---" + (double)Turntable.Instance.pn.totalCount);
                                }
                                rs++;
                            }
                            else
                            {
                                dr[5] = 0;
                            }
                           // File.AppendAllText(path,dr[1].ToString() + "  " + dr[5].ToString(), Encoding.Default);
                        }
                    }
                    
                    //File.AppendAllText(path,Environment.NewLine, Encoding.Default);
                    DataHelper.CheckData(ds, instance.logicName, instance);
                }
                catch (Exception e)
                {
                   
                    MyDebug.ShowMessage("界面图像处"+e.Message);
                }
                finally
                {
                    ho_Image.Dispose();
                    instance.RegionToDisp.Dispose();
                    instance.Image.Dispose();
                    instance.resultHTuple = new HTuple();
                    //UpdateProInfo();
                }
        }

        //private void UpdateProInfo()
        //{
        //    if (InvokeRequired)
        //    {
        //        Invoke(new Turntable.UpdateProductInfo(UpdateProInfo));
        //        return;
        //    }
        //    //HTuple  windows = new HTuple();
        //    //switch (CurrentCCD)
        //    //{
        //    //    case "CCD1": windows = hWindowControl1.HalconWindow;
        //    //        break;
        //    //    case "CCD2": windows = hWindowControl2.HalconWindow;
        //    //        break;
        //    //    case "CCD3": windows = hWindowControl3.HalconWindow;
        //    //        break;
        //    //    case "CCD4": windows = hWindowControl4.HalconWindow;
        //    //        break;
        //    //    case "CCD5": windows = hWindowControl5.HalconWindow;
        //    //        break;
        //    //    case "All": ;
        //    //        break;

        //    //}
        //    //if (zyw == "zhongwen")
        //    //{
        //    //    label1.Text = "产品：" + PathHelper.currentProduct + "  速度：" + Math.Round(Turntable.Instance.pn.countPerMinitor * 60, 0).ToString() + "个/分钟" + "  产量：" + Turntable.Instance.pn.totalCount.ToString() + "\n" +
        //    //        "良率:" + (Turntable.Instance.pn.goodPer() * 100).ToString() + "%" + "  良品：" + Turntable.Instance.pn.goodNum.ToString() ;
        //    //}
        //    //else
        //    //{
        //    //    label1.Text = "product：" + PathHelper.currentProduct + "  speed：" + Math.Round(Turntable.Instance.pn.countPerMinitor * 60, 0).ToString() + "per miniter  " + "  total：" + Turntable.Instance.pn.totalCount.ToString() + "\n" +
        //    //                       "OK rate:" + (Turntable.Instance.pn.goodPer() * 100).ToString() + "%" + "  OK：" + Turntable.Instance.pn.goodNum.ToString() ;
        //    //}
            
             
        //}

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (var c in ccameras)
            {
                if (c.isopen)
                {
                    c.StopGrabbing();
                    c.CloseCam();
                }
              
            }
            cardInitial();//20170621
        }
        public void cardInitial()//20170621
        {
            for (ushort i = 0; i < 32; i++)
            {
                PCI408.PCI408_write_outbit(Card.cardNo, (ushort)(Card.Out1 + i), Card.Off);
            }
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!BtnStart.Enabled)
            {
                e.Cancel = true;
                MessageBox.Show("请先停止运转");

            }
           
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Thread.Sleep(2000);
            //this.Show();
        }

        private void BtnLoadProduct_Click(object sender, EventArgs e)
        {
            //FormLoadProduct f = new FormLoadProduct();
            //if (f.ShowDialog() == DialogResult.OK)
            //{
            //    FileHelper.LoadProductData(gridView1,gridControlResult, ds, PathHelper.currentProductPath);
            //    FileHelper.LoadCameraToolsData(cameras, PathHelper.currentProductPath);
            //    FileHelper.LoadCameraResultToShowData(cameras, PathHelper.currentProductPath);
            //}
            //Card.mu.WaitOne();
            Turntable.Instance.pn = new ProductNum();
            Turntable.Instance.pn.totalCount = Turntable.Instance.nutqueue.Count;
            Turntable.Instance.pn.badNum = Turntable.Instance.nutqueue.Count;
            foreach (CCamera c in ccameras)
            {
                for (int i = 0; i < c.goodcountlist.Count(); i++)
                {
                    c.goodcountlist[i] = 0;
                }
            }
            //Card.mu.ReleaseMutex();
        }

        private void BtnSaveNewProduct_Click(object sender, EventArgs e)
        {
            FormNewProduct f = new FormNewProduct(this);
            f.ShowDialog();
            PathHelper.currentProductPath = PathHelper.productPath + @"\" + f.ProductName;
            PathHelper.currentProduct = f.ProductName;
            PathHelper.CreateNewProduct(f.ProductName);

            //SaveDataSettings(gridControlResult, PathHelper.currentProductPath+@"\Product.xml");
            FileHelper.SaveCameraResultToShowData(ccameras, PathHelper.currentProductPath);
            FileHelper.SaveCameraToolsData(ccameras);
            FileHelper.SaveProductData(ds, PathHelper.currentProductPath);
            labelpname.Text = PathHelper.currentProduct;
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            CurrentCCD = xtraTabControl1.SelectedTabPage.Text;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            //SplashWindow sp = new SplashWindow();
            //splashResult = sp.ShowDialog();
        }

        private void BtnSaveParam_Click(object sender, EventArgs e)
        {
            if (PathHelper.currentProductPath == "" || PathHelper.currentProductPath == null)
            {
                MessageBox.Show("产品没有起名，请先保存新产品");
            }
            else
            {
                FileHelper.SaveProductData(ds, PathHelper.currentProductPath);
            }
        }

        private void BtnControlSet_Click(object sender, EventArgs e)
        {
            if (!BtnStart.Enabled)
            {
                MessageBox.Show("请先停止运转");
                return;
            }
            ControlSet f = new ControlSet();
            f.ShowDialog(this);
        }
        void Instance_StrongPressEventHandler()
        {
            if (InvokeRequired)
            {
                Invoke(new Turntable.StrongPress(Instance_StrongPressEventHandler));
                return;
            }
            if (MSStoptTimer.Enabled)
                MSStoptTimer.Enabled = false;
            Turntable.Instance.stopShakePan();
            Turntable.Instance.Stop();
            BtnStart.Enabled = true;
            DialogResult dr= MessageBox.Show("卡料报警，点击确定，解除报警，重新启动");
            if (dr == DialogResult.OK)
            {
                Turntable.Instance.servoAlarmClear();
            }
        }
        void wuliaotingzhiEventHandler()
        {
            if (InvokeRequired)
            {
                Invoke(new Turntable.StrongPress(wuliaotingzhiEventHandler));
                return;
            }
            if (MSStoptTimer.Enabled)
                MSStoptTimer.Enabled = false;
            Turntable.Instance.stopShakePan();
            Turntable.Instance.Stop();
            PCI408.PCI408_write_outbit(Card.cardNo,Card.hd,Card.On);
            PCI408.PCI408_write_outbit(Card.cardNo, Card.ld, Card.Off);
            //PCI408.PCI408_write_outbit(Card.cardNo, Card.ssd, Card.Off);
            foreach (var c in ccameras)
            {
                if (c.isopen)
                {
                    c.StopGrabbing();
                    c.isfirst = false;
                }
            }
            BtnStart.Enabled = true;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {

            //读取各相机下的检测项目，读取各相机的统计结果
            try
            {

                HSSFWorkbook wk = new HSSFWorkbook();
                //创建一个Sheet  
                ISheet sheet = wk.CreateSheet("检测结果统计");
                //在第一行创建行  
                IRow row = sheet.CreateRow(0);
                //在第一行的第一列创建单元格  
                ICell cell = row.CreateCell(0);
                cell.SetCellValue("检测项（Feature Item）");
                cell = row.CreateCell(1);
                cell.SetCellValue("修正(Calibration)");
                cell = row.CreateCell(2);
                cell.SetCellValue("上限(Max)");
                cell = row.CreateCell(3);
                cell.SetCellValue("下限(Min)");
                cell = row.CreateCell(4);
                cell.SetCellValue("OK");
                cell = row.CreateCell(5);
                cell.SetCellValue("NG");
                cell = row.CreateCell(6);
                cell.SetCellValue("合格率");




                DataRowCollection drc = ds.Tables["detailTable"].Rows;

                int totalindex = 0;
                foreach (CCamera c in ccameras)
                {
                    //获取c的检测项个数
                    int itemcount = c.goodcountlist.Count();
                    for (int i = 0; i < itemcount; i++)
                    {
                        //写入excel

                        int currentgood = c.goodcountlist[i];
                        int currentbad = c.badcountlist[i];

                        DataRow dr = drc[totalindex];
                        string s = dr[1].ToString();//检测项名称
                        double xz = (double)dr[2];
                        double sx = (double)dr[3];
                        double xx = (double)dr[4];

                        row = sheet.CreateRow(totalindex+1);
                        cell = row.CreateCell(0);
                        cell.SetCellValue(s);
                        cell = row.CreateCell(1);
                        cell.SetCellValue(xz);
                        cell = row.CreateCell(2);
                        cell.SetCellValue(sx);
                        cell = row.CreateCell(3);
                        cell.SetCellValue(xx);
                        cell = row.CreateCell(4);
                        cell.SetCellValue(currentgood);
                        cell = row.CreateCell(5);
                        cell.SetCellValue(currentbad);
                        cell = row.CreateCell(6);
                        cell.SetCellValue(((double)currentgood / (double)Turntable.Instance.pn.totalCount));


                        totalindex++;


                    }




                }
                row = sheet.CreateRow(totalindex + 1);
                row = sheet.CreateRow(totalindex + 2);
                cell = row.CreateCell(0);
                cell.SetCellValue("总产量");
                cell = row.CreateCell(1);
                cell.SetCellValue("OK");
                cell = row.CreateCell(2);
                cell.SetCellValue("NG");
                cell = row.CreateCell(3);
                cell.SetCellValue("合格率");
                row = sheet.CreateRow(totalindex + 3);
                cell = row.CreateCell(0);
                cell.SetCellValue(Turntable.Instance.pn.totalCount);
                cell = row.CreateCell(1);
                cell.SetCellValue(Turntable.Instance.pn.goodNum);
                cell = row.CreateCell(2);
                cell.SetCellValue(Turntable.Instance.pn.badNum);
                cell = row.CreateCell(3);
                cell.SetCellValue(((double)Turntable.Instance.pn.goodNum / (double)Turntable.Instance.pn.totalCount));


                using (FileStream fs = File.OpenWrite("统计报表.xls"))
                {
                    wk.Write(fs);//向打开的这个xls文件中写入并保存。  
                }
            }
            catch
            {
                MessageBox.Show("报表生成发生异常");
            }
        }

        private void gridControlResult_Click(object sender, EventArgs e)
        {

        }
        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            {

                int hand1 = e.RowHandle;
                if (hand1 < 0) return;
                GridView View = sender as GridView;
                DevExpress.XtraGrid.Columns.GridColumnCollection dr = gridView1.Columns;
                //string ss = gridView1.GetRowCellDisplayText(hand1, gridView1.Columns[0]);
                string[] b = new string[8];
                int t = 0;
                foreach (DevExpress.XtraGrid.Columns.GridColumn c in dr)
                {
                    if (t == 0)
                    {
                        b[t] = gridView1.GetRowCellValue(hand1, c).ToString();

                    }
                    t++;
                }
                if (int.Parse(b[0].Substring(3, 1)) % 2 == 1)
                {
                    //Random ran = new Random();
                    e.Appearance.BackColor = Color.FromArgb(255, 200, 255);
                    //e.Appearance
                    //DataGridViewRow 
                    //gridviewr
                    //row.BackColor = Color.Red;
                }
            }
            //if (e.Column.FieldName == "CCD")//20170701
            //{
                
            //    int hand1 = e.RowHandle;
            //    if (hand1 < 0) return;
            //    string ss = gridView1.GetRowCellDisplayText(hand1, gridView1.Columns[0]);
            //    if (int.Parse(ss.Substring(3, 1)) % 2 == 1)
            //    {
            //        e.Appearance.BackColor = Color.FromArgb(255, 200, 255);
            //        //e.Appearance
            //        //DataGridViewRow 
            //        //gridviewr
            //        //row.BackColor = Color.Red;
            //    }
            //}
            if (e.Column.FieldName == "测量（Measure）")//设背景  
            {
                int hand = e.RowHandle;
                if (hand < 0) return;
                GridView View = sender as GridView;
                DevExpress.XtraGrid.Columns.GridColumnCollection dr = gridView1.Columns;
                double[] a = new double[8];
                int t = 0;
                foreach (DevExpress.XtraGrid.Columns.GridColumn c in dr)
                {
                    if (t > 1 && t < 6)
                    {
                        a[t] = double.Parse(gridView1.GetRowCellValue(hand, c).ToString());

                    }
                    t++;
                }
                if (a[4] <= a[5] && a[5] <= a[3])
                {
                    e.Appearance.BackColor = Color.Transparent;
                }
                else
                {
                    e.Appearance.BackColor = Color.Red;
                }
            }
            //if (e.Column.FieldName == "OK/NG")
            //{
            //    if (gridView1.GetRowCellValue(e.RowHandle, e.Column) == "OK")
            //    {
            //        e.Appearance.BackColor = Color.Transparent;
            //    }
            //    else if (gridView1.GetRowCellValue(e.RowHandle, e.Column) == "NG")
            //    {
            //        e.Appearance.BackColor = Color.Red;
            //    }
            //}
        }
        private int prenum = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            int a = Turntable.Instance.pn.totalCount;
            int b = Turntable.Instance.pn.goodNum;
            double c = 100 * (double)b / a;
            double d = (a - prenum)*60;
            speed.Text = d.ToString();
            yeild.Text = string.Format("{0:f6}", c);
            total.Text = a.ToString();
            good.Text = b.ToString();
            //    label1.Text ="speed:"+d +"total :"+ a + "good "+b +" yeild:"+c;
            prenum = a;
        }
         private void timer2_Tick(object sender, EventArgs e)
        {

            int  k  = Dmc2210.d2210_read_inbit(Card.cardNo, Card.jiting);
            //int kk = PCI408.PCI408_check_done(Card.zhouhao);
            if (   k == 0 )
            {
                //if (kk == 0)
                //{
                    //Dmc2210.d2210_write_outbit(Card.cardNo, Card.shusongdai, Card.Off);
                    PCI408.PCI408_write_outbit(Card.cardNo, Card.hd, Card.On);
                    PCI408.PCI408_write_outbit(Card.cardNo, Card.shakePan, Card.Off);
                    PCI408.PCI408_write_outbit(Card.cardNo, Card.ld, Card.Off);
                //}

            }
            //else if (kk ==1 &&BtnStart.Enabled == false &&getup ==false )
            //{
            //    getup = true;
            //    Console.WriteLine("ssss");
            //    Thread.Sleep(5000);
            //    //Dmc2210.d2210_write_outbit(Card.cardNo, Card.shusongdai, Card.On);
            //    PCI408.PCI408_write_outbit(Card.cardNo, Card.ld, Card.On);
            //    PCI408.PCI408_write_outbit(Card.cardNo, Card.hd, Card.Off);
            //    PCI408.PCI408_write_outbit(Card.cardNo, Card.shakePan, Card.On);
            //    getup = false;

            //}     
          
        }

        //void reup()
        //{
        //    Thread.Sleep(1000);
        //    //Dmc2210.d2210_write_outbit(Card.cardNo, Card.shusongdai, Card.On);
        //    Dmc2210.d2210_write_outbit(Card.cardNo, Card.ld, Card.On);
        //    PCI408.PCI408_write_outbit(Card.cardNo, Card.shakePan, Card.On);
        //    Dmc2210.d2210_write_outbit(Card.cardNo, Card.hd, Card.Off);
        //}

        bool getup = false;
        private void label1_Click(object sender, EventArgs e)
        {

        }
        //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        if (e.Row.Cells[0].Text == "0")
        //            e.Row.BackColor = Color.Red;
        //    }
        //}
        }
    }
