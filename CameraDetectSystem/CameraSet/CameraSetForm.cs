using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;
using System.Data.SQLite;
using System.Xml;
using System.IO;
namespace CameraDetectSystem
{
    public partial class CameraSetForm : Form
    {
        public HObject Image = null;
        DataSet ds = new DataSet("Temp");
        public List<ImageTools> imageTools = new List<ImageTools>();
        ThresholdSelectRegion tsrc;
        int thresholdvalue = 0;
        DataHelper dh = new DataHelper();
        Result resultToShow = new Result();
        HTuple window=new HTuple();
        bool ismodify;
        bool ondetect = false;
        public double pixelDist=1;
        public bool Ismodify
        {
            get { return ismodify; }
            set { ismodify = value; }
        }

        public CameraSetForm()
        {
            InitializeComponent();
            string path = @"c:\zyw.txt";
            string zyw = File.ReadAllText(path, Encoding.Default);
            if (zyw == "zhongwen")
            {
                groupBoxColor.Text = "颜色选择";
                radioButton_black.Text = "黑色";
                radioButton_white.Text = "白色";
                BtnInitialMeasure.Text = "检测初始化";
                BtnMulDetect.Text = "批量检测";
                BtnReadBatch.Text = "读取检测";
                BtnWriteBatch.Text = "保存检测";
                BtnReadImage.Text = "读取图片";
                BtnSaveShow.Text = "保存显示";
                BtnHMeasure.Text = "水平检测";
                BtnVMeasure.Text = "竖直检测";
                BtnCMeasure.Text = "外圆检测";
                BtnTMeasure.Text = "大小径测量";
                BtnScrewPitch.Text = "螺纹间距";
                BtnInnerCircle.Text = "内接圆测量";
                ScrewLength.Text = "螺纹长度";
                simpleButton8.Text = "螺纹条数";
                Btnlocate.Text = "十字槽";
                simpleButton10.Text = "乌木长度";
                simpleButton11.Text = "匹配";
                simpleButton12.Text = "台阶水平测量";
                simpleButton1.Text = "台阶竖直测量";
                simpleButton2.Text = "部分高度测量";
                button4.Text = "角度测量";
                button3.Text = "螺纹检测";
                button7.Text = "平行线距离";
                labelThreshold.Text = "阈值";
                button1.Text = "槽宽";
                button2.Text = "两线距离";
            }
            if (zyw == "yinwen")
            {
                groupBoxColor.Text = "ColorSelect";
                radioButton_black.Text = "Black";
                radioButton_white.Text = "White";
                BtnInitialMeasure.Text = "MeasureInitial";
                BtnMulDetect.Text = "BatchMeasure";
                BtnReadBatch.Text = "ReadMeasure";
                BtnWriteBatch.Text = "SaveMeasure";
                BtnReadImage.Text = "ReadPicture";
                BtnSaveShow.Text = "SaveShow";
                BtnHMeasure.Text = "HorizontalMeasure";
                BtnVMeasure.Text = "VerticalMeasure";
                BtnCMeasure.Text = "OutCircleMeasure";
                BtnTMeasure.Text = "BigSmallDiameterMeasure";
                BtnScrewPitch.Text = "ScrewPitch";
                BtnInnerCircle.Text = "InnerCircleMeas";
                ScrewLength.Text = "ScrewLength";
                simpleButton8.Text = "ScrewNum";
                Btnlocate.Text = "CrossRecessMeas";
                simpleButton10.Text = "Length";
                simpleButton11.Text = "Fit";
                simpleButton12.Text = "StepHorizontalMeasure";
                simpleButton1.Text = "StepVerticalMeasure";
                simpleButton2.Text = "SomeHeightMeasure";
                button4.Text = "AngleMeasure";
                button3.Text = "ScrewMeasure";
                button7.Text = "ParallelDistanceMeasure";
                labelThreshold.Text = "Threshold";
                button1.Text = "SlotWidth";
                button2.Text = "2LineDistance";
            }
            window = hWindowControlSet.HalconWindow;
            ismodify = false;
            ds = DataHelper.DataBaseOpen();
            特征.DataSource = ds.Tables["Temp"];
            特征.ValueMember = "筛选项";

            dataGridViewSelectMethod.Rows[0].Cells[0].Value = "area";
            dataGridViewSelectMethod.Rows[0].Cells[1].Value = 100;
            dataGridViewSelectMethod.Rows[0].Cells[2].Value = 300000;
            dh.GetSelectModeData(dataGridViewSelectMethod);
            radioButton_white.Checked = true;
            ondetect = false;
            //trackBar_Threshold.Value = 130;
            trackBar_Threshold.Maximum = 255;
            HOperatorSet.GenEmptyObj(out Image);
            Image.Dispose();
        }
        ~CameraSetForm()
        {
            //Image = null;
        }
        private void BtnVMeasure_Click(object sender, EventArgs e)
        {
            if (Image == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            myrake rake = new myrake(Image, tsrc);
            rake.vOrh = "v";
            rake.taijie = "false";
            rake.pixeldist = this.pixelDist;
            rake.LWindowHandle = window.L;
            hWindowControlSet.Focus();
            rake.draw();
            rake.method();
            HOperatorSet.DispObj(rake.RegionToDisp, window);
            HTuple h = rake.Result;
            HalconHelp.disp_message(window, h, "window", 10, 10, "green", "false");
            imageTools.Add(rake);
        }
        public static ThresholdSelectRegion tsr = new ThresholdSelectRegion();
        private void BtnHMeasure_Click(object sender, EventArgs e)
        {
            if (Image == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            myrake rake = new myrake(Image, tsrc);
            rake.vOrh = "h";
            rake.taijie = "false";
            rake.pixeldist = this.pixelDist;
            rake.LWindowHandle = window.L;
            hWindowControlSet.Focus();
            rake.draw();
            rake.method();
            HOperatorSet.DispObj(rake.RegionToDisp, window);
            HTuple h = rake.Result;
            HalconHelp.disp_message(window, h, "window", 10, 10, "green", "false");
            imageTools.Add(rake);

        }

        private void BtnCMeasure_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            OutCircletool circletool = new OutCircletool(Image, tsrc);
            circletool.LWindowHandle = window.L;
            circletool.pixeldist = this.pixelDist;
            hWindowControlSet.Focus();
            circletool.draw();
            circletool.method();
            HTuple h = circletool.Result;
            HalconHelp.disp_message(window, h, "window", 10, 10, "green", "false");
            imageTools.Add(circletool);
            HOperatorSet.DispObj(circletool.RegionToDisp, window);
        }

        private void BtnReadImage_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                string path = openFileDialog1.FileName;

                HOperatorSet.GenEmptyObj(out Image);
                Image.Dispose();
                HOperatorSet.ReadImage(out Image, new HTuple(path));
            }
            HalconHelp.InitialhWindow(Image, hWindowControlSet.HalconWindow);
            HOperatorSet.DispObj(Image, hWindowControlSet.HalconWindow);
        }

        private void BtnInitialMeasure_Click(object sender, EventArgs e)
        {
            imageTools.Clear();
        }
        private void BtnReadBatch_Click(object sender, EventArgs e)
        {
            
            MainForm frm1 = (MainForm)this.Owner;

            imageTools = ToolBox.ReadFromXml(PathHelper.currentProductPath + "\\" + frm1.CurrentCCD + ".dat"); ;
        }

        private void BtnWriteBatch_Click(object sender, EventArgs e)
        {
            MainForm frm1 = (MainForm)this.Owner;
            if (PathHelper.currentProductPath == null)
            {
                MessageBox.Show("未建立新产品");
                return;
            }
            ToolBox.SaveToXml(imageTools, PathHelper.currentProductPath + "\\" + frm1.CurrentCCD + ".dat");
        }

        private void trackBar_Threshold_Scroll(object sender, EventArgs e)
        {
            HOperatorSet.SetDraw(hWindowControlSet.HalconWindow, "fill");
            HOperatorSet.ClearWindow(hWindowControlSet.HalconWindow);
            HOperatorSet.DispObj(Image, hWindowControlSet.HalconWindow);
            HOperatorSet.SetColor(hWindowControlSet.HalconWindow, "red");
            ThresholdSelectRegion tsr = new ThresholdSelectRegion();
            //dh=new DataHelper();
            int i = imageTools.Count;
            tsr.Image = Image;
            if (radioButton_black.Checked)
            {
                tsr.whiteOrBlack = "black";
                tsr.thresholdValue = trackBar_Threshold.Value;
            }
            else if (radioButton_white.Checked)
            {
                tsr.whiteOrBlack = "white";
                tsr.thresholdValue = trackBar_Threshold.Value ;
            }
            
            labelThreshold.Text = tsr.thresholdValue.ToString();
            if (labelThreshold.Text == null)
            {
                tsr.thresholdValue = 130;
            }
            HOperatorSet.WriteTuple(tsr.thresholdValue, PathHelper.currentProductPath + @"\thresholdValue");

            tsr.selectMethod = dh.selectMode.Clone().ToSArr();
            tsr.selectAndOrOr = "and";

            tsr.selectMethodMinValue = dh.selectMinValue.Clone().ToDArr();
            tsr.selectMethodMaxValue = dh.selectMaxValue.Clone().ToDArr();
            HOperatorSet.WriteTuple(tsr.selectMethodMinValue, PathHelper.currentProductPath + @"\mianjixx");
            HOperatorSet.WriteTuple(tsr.selectMethodMaxValue, PathHelper.currentProductPath + @"\mianjisx");
            tsr.ThresholdMethod();
            tsr.SelectMethod();
            if (tsr.Region != null)
                HOperatorSet.SetColored(hWindowControlSet.HalconWindow, 12);
            HOperatorSet.DispObj(tsr.Region, hWindowControlSet.HalconWindow);
            tsrc = tsr;
        }

        private void dataGridViewSelectMethod_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dh.GetSelectModeData(dataGridViewSelectMethod);
        }

        private void BtnSaveShow_Click(object sender, EventArgs e)
        {
           
            XmlDocument xmldoc = new XmlDocument();
            //加入一个根元素
            XmlElement xroot = xmldoc.CreateElement("", "结果", "");
            xmldoc.AppendChild(xroot);
            XmlNode root = xmldoc.SelectSingleNode("结果");
            
            //foreach (IData data in resultToShow.result)
            //{
            //    XmlElement xe1 = xmldoc.CreateElement(data.name);//创建一个<Node>节点 
                
            //    xe1.SetAttribute("IsToShow", data.data);//设置该节点genre属性 
            //    root.AppendChild(xe1);
            //}
            foreach (ListViewItem item in listViewResult.Items)
            {
                XmlElement xe1 = xmldoc.CreateElement(item.Text);
                if (item.Checked)
                {
                    xe1.SetAttribute("IsToShow", "true");
                }
                else
                {
                    xe1.SetAttribute("IsToShow", "false");
                }
                root.AppendChild(xe1);
            }
            MainForm frm1=(MainForm)this.Owner;
            
            //string CCD=((DevExpress.XtraTab.XtraTabControl)frm1.Controls["xtraTabControl1"]).Text;
            resultToShow.WriteToXML(PathHelper.currentProductPath+"\\"+frm1.CurrentCCD+".xml", xmldoc);
        }

        private void CameraSetForm_Load(object sender, EventArgs e)
        {
            window = hWindowControlSet.HalconWindow;
            if (Image != null)
            {
                try
                {
                    MainForm frm1 = (MainForm)this.Owner;
                    
                    HTuple w=new HTuple(), h=new HTuple();
                    HOperatorSet.GetImageSize(Image, out w, out h);
                    HOperatorSet.SetPart(hWindowControlSet.HalconWindow, 0, 0, h, w);
                    HOperatorSet.SetDraw(hWindowControlSet.HalconWindow, "margin");
                    HOperatorSet.DispObj(Image, hWindowControlSet.HalconWindow);
                }
                catch
                {
                }
            }
        }

        private void CameraSetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Image!=null)
            Image.Dispose();
        }

        private void BtnMulDetect_Click(object sender, EventArgs e)
        {
            HTuple result = new HTuple();
            foreach (ImageTools it in imageTools)
            {
                HOperatorSet.SetColor(hWindowControlSet.HalconWindow, "green");
                HOperatorSet.SetDraw(hWindowControlSet.HalconWindow, "margin");
                it.Image = Image;
                it.method();
                HTuple h = it.Result;
                HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
                result = result.TupleConcat(it.Result);

                if (it.RegionToDisp.IsInitialized())
                    HOperatorSet.DispObj(it.RegionToDisp, hWindowControlSet.HalconWindow);
            }
            string[] strs;
            strs = result.ToSArr();
            listViewResult.Items.Clear();
            listViewResult.BeginUpdate();

            resultToShow.GetResult(result);

            foreach (IData data in resultToShow.resultToShowtemp)
            {
                ListViewItem item = new ListViewItem();
                item.Text = data.name;
                item.SubItems.Add(data.data);
                listViewResult.Items.Add(item);
            }
            listViewResult.EndUpdate();
            ismodify = true;
        }

        private void BtnTMeasure_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            BigSmallDiameter bigSmallDiameter = new BigSmallDiameter(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            bigSmallDiameter.LWindowHandle = window.L;
            bigSmallDiameter.pixeldist = pixelDist;
            //bigSmallDiameter.LWindowHandle = window.L;
            hWindowControlSet.Focus();
            bigSmallDiameter.draw();
            bigSmallDiameter.method();
            HTuple h = bigSmallDiameter.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(bigSmallDiameter);
            HOperatorSet.DispObj(bigSmallDiameter.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;

        }

        private void BtnScrewPitch_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            ScrewPitchTool bigSmallDiameter = new ScrewPitchTool(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            bigSmallDiameter.LWindowHandle = window.L;
            bigSmallDiameter.pixeldist = pixelDist;
            //bigSmallDiameter.LWindowHandle = window.L;
            hWindowControlSet.Focus();
            bigSmallDiameter.draw();
            bigSmallDiameter.method();
            HTuple h = bigSmallDiameter.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(bigSmallDiameter);
            HOperatorSet.DispObj(bigSmallDiameter.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;

        }

        private void BtnInnerCircle_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            InnerCircletool bigSmallDiameter = new InnerCircletool(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            bigSmallDiameter.LWindowHandle = window.L;
            bigSmallDiameter.pixeldist = pixelDist;
           
            hWindowControlSet.Focus();
            bigSmallDiameter.draw();
            bigSmallDiameter.method();
            HTuple h = bigSmallDiameter.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(bigSmallDiameter);
            HOperatorSet.DispObj(bigSmallDiameter.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;

        }

        private void ScrewLength_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            ScrewLength bigSmallDiameter = new ScrewLength(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            bigSmallDiameter.LWindowHandle = window.L;
            bigSmallDiameter.pixeldist = pixelDist;
            //bigSmallDiameter.LWindowHandle = window.L;
            hWindowControlSet.Focus();
            bigSmallDiameter.hv_vOrh = "h";
            bigSmallDiameter.draw();
            bigSmallDiameter.method();
            HTuple h = bigSmallDiameter.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(bigSmallDiameter);
            HOperatorSet.DispObj(bigSmallDiameter.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;

        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            RegionCounts bigSmallDiameter = new RegionCounts(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            bigSmallDiameter.LWindowHandle = window.L;
            bigSmallDiameter.pixeldist = pixelDist;
            //bigSmallDiameter.LWindowHandle = window.L;
            hWindowControlSet.Focus();
             
            bigSmallDiameter.draw();
            bigSmallDiameter.method();
            HTuple h = bigSmallDiameter.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(bigSmallDiameter);
            HOperatorSet.DispObj(bigSmallDiameter.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;

        }

        private void Btnlocate_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            RegionArea bigSmallDiameter = new RegionArea(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            bigSmallDiameter.LWindowHandle = window.L;
            bigSmallDiameter.pixeldist = pixelDist;
            //bigSmallDiameter.LWindowHandle = window.L;
            hWindowControlSet.Focus();

            bigSmallDiameter.draw();
            bigSmallDiameter.method();
            HTuple h = bigSmallDiameter.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(bigSmallDiameter);
            HOperatorSet.DispObj(bigSmallDiameter.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;

        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            if (Image == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            XldLength rake = new XldLength(Image, tsrc);
            rake.vOrh = "h";
            rake.pixeldist = this.pixelDist;
            rake.LWindowHandle = window.L;
            hWindowControlSet.Focus();
            rake.draw();
            rake.method();
            HOperatorSet.DispObj(rake.RegionToDisp, window);
            HTuple h = rake.Result;
            HalconHelp.disp_message(window, h, "window", 10, 10, "green", "false");
            imageTools.Add(rake);
            ondetect = false;

        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            if (Image == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            ShapeMatch rake = new ShapeMatch(Image, tsrc);
            rake.vOrh = "h";
            rake.pixeldist = this.pixelDist;
            rake.LWindowHandle = window.L;
            hWindowControlSet.Focus();
            rake.draw();
            rake.method();
            HOperatorSet.DispObj(rake.RegionToDisp, window);
            HTuple h = rake.Result;
            HalconHelp.disp_message(window, h, "window", 10, 10, "green", "false");
            imageTools.Add(rake);
            ondetect = false;

        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            if (Image == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            myrake rake = new myrake(Image, tsrc);
            rake.vOrh = "h";
            rake.taijie = "true";
            rake.pixeldist = this.pixelDist;
            rake.LWindowHandle = window.L;
            hWindowControlSet.Focus();
            rake.draw();
            rake.method();
            HOperatorSet.DispObj(rake.RegionToDisp, window);
            HTuple h = rake.Result;
            HalconHelp.disp_message(window, h, "window", 10, 10, "green", "false");
            imageTools.Add(rake);
            ondetect = false;

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Image == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            myrake rake = new myrake(Image, tsrc);
            rake.vOrh = "v";
            rake.taijie = "true";
            rake.pixeldist = this.pixelDist;
            rake.LWindowHandle = window.L;
            hWindowControlSet.Focus();
            rake.draw();
            rake.method();
            HOperatorSet.DispObj(rake.RegionToDisp, window);
            HTuple h = rake.Result;
            HalconHelp.disp_message(window, h, "window", 10, 10, "green", "false");
            imageTools.Add(rake);

            ondetect = false;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (Image == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }

            PartHeight rake = new PartHeight(Image, tsrc);
            //rake.vOrh = "v";
            //rake.taijie = "true";
            rake.pixeldist = this.pixelDist;
            rake.LWindowHandle = window.L;
            hWindowControlSet.Focus();
            rake.draw();
            rake.method();
            HOperatorSet.DispObj(rake.RegionToDisp, window);
            HTuple h = rake.Result;
            HalconHelp.disp_message(window, h, "window", 10, 10, "green", "false");
            imageTools.Add(rake);

            ondetect = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            CCD1 ccd1 = new CCD1(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            ccd1.LWindowHandle = window.L;
            ccd1.pixeldist = pixelDist;

            hWindowControlSet.Focus();
            ccd1.method();
            HTuple h = ccd1.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(ccd1);
            HOperatorSet.DispObj(ccd1.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            CCD2 ccd2 = new CCD2(Image,tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            ccd2.LWindowHandle = window.L;
            ccd2.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            ccd2.method();
            HTuple h = ccd2.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            
            imageTools.Add(ccd2);
            HOperatorSet.DispObj(ccd2.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }

            HTuple yac = null;
            CameraDetectSystem.CameraSet.lwowenshizhe lwsz = new CameraDetectSystem.CameraSet.lwowenshizhe();
            lwsz.ShowDialog(this);
            HOperatorSet.ReadTuple(PathHelper.currentProductPath + "/yac.tup", out yac);
            luowenchushi(yac);
            CCD2 ccd2 = new CCD2(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            ccd2.LWindowHandle = window.L;
            ccd2.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            ccd2.method();
            HTuple h = ccd2.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(ccd2);
            HOperatorSet.DispObj(ccd2.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }
        private void luowenchushi(int yac)
        {


            // Local iconic variables 

            HObject ho_Rectanglemx, ho_Rectanglemxtx;
            HObject ho_Region, ho_Border, ho_ContoursSplit, ho_SelectedContours;
            HObject ho_UnionContours, ho_SelectedContours1, ho_Rectangle;
            HObject ho_ImageReduced, ho_Region1, ho_Region2;

            // Local control variables 

            HTuple hv_mxr = null, hv_mxc = null;
            HTuple hv_mxa = null, hv_mxch = null, hv_mxk = null, hv_ModelID = null;
            HTuple hv_Row = null, hv_Column = null, hv_Phi = null;
            HTuple hv_Length1 = null, hv_Length2 = null, hv_Row2 = null;
            HTuple hv_Column1 = null, hv_Phi1 = null, hv_Length11 = null;
            HTuple hv_Length21 = null, hv_Phi2 = null, hv_Area = null;
            HTuple hv_Row1 = null, hv_Column2 = null, hv_mxjj = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectanglemx);
            HOperatorSet.GenEmptyObj(out ho_Rectanglemxtx);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_Border);
            HOperatorSet.GenEmptyObj(out ho_ContoursSplit);
            HOperatorSet.GenEmptyObj(out ho_SelectedContours);
            HOperatorSet.GenEmptyObj(out ho_UnionContours);
            HOperatorSet.GenEmptyObj(out ho_SelectedContours1);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_Region2);

            HOperatorSet.DrawRectangle2(hWindowControlSet.HalconWindow, out hv_mxr, out hv_mxc, out hv_mxa, out hv_mxch,
                out hv_mxk);
            ho_Rectanglemx.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectanglemx, hv_mxr, hv_mxc, hv_mxa, hv_mxch,
                hv_mxk);
            ho_Rectanglemxtx.Dispose();
            HOperatorSet.ReduceDomain(Image, ho_Rectanglemx, out ho_Rectanglemxtx);
            HOperatorSet.CreateNccModel(ho_Rectanglemxtx, "auto", 0, 6.29, 0.0175, "use_polarity",
                out hv_ModelID);
            HOperatorSet.WriteNccModel(hv_ModelID, PathHelper.currentProductPath + "/dan.ncm");
            ho_Region.Dispose();
            HOperatorSet.Threshold(Image, out ho_Region, 0, 128);
            HOperatorSet.SmallestRectangle2(ho_Region, out hv_Row, out hv_Column, out hv_Phi,
                out hv_Length1, out hv_Length2);
            ho_Border.Dispose();
            HOperatorSet.ThresholdSubPix(Image, out ho_Border, 128);
            ho_ContoursSplit.Dispose();
            HOperatorSet.SegmentContoursXld(ho_Border, out ho_ContoursSplit, "lines_circles",
                5, 4, 2);
            ho_SelectedContours.Dispose();
            HOperatorSet.SelectContoursXld(ho_ContoursSplit, out ho_SelectedContours, "contour_length",
                0,yac, -0.5, 0.5);
            ho_UnionContours.Dispose();
            HOperatorSet.UnionAdjacentContoursXld(ho_SelectedContours, out ho_UnionContours,
                10, 1, "attr_keep");
            ho_SelectedContours1.Dispose();
            HOperatorSet.SelectContoursXld(ho_UnionContours, out ho_SelectedContours1, "contour_length",
                300, 20000, -0.5, 0.5);
            HOperatorSet.SmallestRectangle2Xld(ho_SelectedContours1, out hv_Row2, out hv_Column1,
                out hv_Phi1, out hv_Length11, out hv_Length21);
            ho_Rectangle.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangle, ((hv_Row2.TupleSelect(0)) + (hv_Row2.TupleSelect(
                1))) / 2, ((hv_Column1.TupleSelect(0)) + (hv_Column1.TupleSelect(1))) / 2, ((hv_Phi1.TupleSelect(
                0)) + (hv_Phi1.TupleSelect(0))) / 2, (((hv_Length11.TupleSelect(0)) + (hv_Length11.TupleSelect(
                1))) / 2) * 0.8, hv_Length2);
            ho_ImageReduced.Dispose();
            HOperatorSet.ReduceDomain(Image, ho_Rectangle, out ho_ImageReduced);
            ho_Region1.Dispose();
            HOperatorSet.Threshold(ho_ImageReduced, out ho_Region1, 0, 128);
            HOperatorSet.OrientationRegion(ho_Region1, out hv_Phi2);
            ho_Region2.Dispose();
            HOperatorSet.Threshold(ho_Rectanglemxtx, out ho_Region2, 0, 128);
            HOperatorSet.AreaCenter(ho_Region2, out hv_Area, out hv_Row1, out hv_Column2);
            hv_mxjj = (hv_mxa - hv_Phi2) - ((new HTuple(90)).TupleRad());
            while ((int)(new HTuple(hv_mxjj.TupleLess((new HTuple(-180)).TupleRad()))) != 0)
            {
                hv_mxjj = hv_mxjj + ((new HTuple(180)).TupleRad());
            }
            while ((int)(new HTuple(hv_mxjj.TupleGreater((new HTuple(180)).TupleRad()))) != 0)
            {
                hv_mxjj = hv_mxjj - ((new HTuple(180)).TupleRad());
            }
            HOperatorSet.WriteTuple(hv_mxjj, PathHelper.currentProductPath + "/mxjj.tup");
            HOperatorSet.WriteTuple(hv_mxa, PathHelper.currentProductPath + "/Phi3.tup");
            HOperatorSet.WriteTuple(hv_mxr, PathHelper.currentProductPath + "/Row1.tup");
            HOperatorSet.WriteTuple(hv_mxc, PathHelper.currentProductPath + "/Column2.tup");
            HOperatorSet.WriteTuple(hv_mxch, PathHelper.currentProductPath + "/mxch.tup");
            HOperatorSet.WriteTuple(hv_mxk, PathHelper.currentProductPath + "/mxk.tup");

            ho_Rectanglemx.Dispose();
            ho_Rectanglemxtx.Dispose();
            ho_Region.Dispose();
            ho_Border.Dispose();
            ho_ContoursSplit.Dispose();
            ho_SelectedContours.Dispose();
            ho_UnionContours.Dispose();
            ho_SelectedContours1.Dispose();
            ho_Rectangle.Dispose();
            ho_ImageReduced.Dispose();
            ho_Region1.Dispose();
            ho_Region2.Dispose();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            jiaoduceliang jdcl = new jiaoduceliang(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            jdcl.LWindowHandle = window.L;
            jdcl.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            jdcl.draw();
            jdcl.method();
            HTuple h = jdcl.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(jdcl);
            HOperatorSet.DispObj(jdcl.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            liujiaoceliang ljcl = new liujiaoceliang(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            ljcl.LWindowHandle = window.L;
            ljcl.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            ljcl.draw();
            ljcl.method();
            HTuple h = ljcl.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(ljcl);
            HOperatorSet.DispObj(ljcl.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            xiaoziceliang xzcl = new xiaoziceliang(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzcl.LWindowHandle = window.L;
            xzcl.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            //xzcl.draw();
            xzcl.method();
            HTuple h = xzcl.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzcl);
            HOperatorSet.DispObj(xzcl.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            pingxingxianjuli pxljl = new pingxingxianjuli(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            pxljl.LWindowHandle = window.L;
            pxljl.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            pxljl.draw();
            pxljl.method();
            HTuple h = pxljl.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(pxljl);
            HOperatorSet.DispObj(pxljl.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void listViewResult_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            caokuang pxljl = new caokuang(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            pxljl.LWindowHandle = window.L;
            pxljl.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            pxljl.draw();
            pxljl.method();
            HTuple h = pxljl.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(pxljl);
            HOperatorSet.DispObj(pxljl.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            liangxianjuli lxjl = new liangxianjuli(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            lxjl.LWindowHandle = window.L;
            lxjl.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            lxjl.draw();
            lxjl.method();
            HTuple h = lxjl.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(lxjl);
            HOperatorSet.DispObj(lxjl.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            CCD2 lwcl = new CCD2(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            lwcl.LWindowHandle = window.L;
            lwcl.pixeldist = pixelDist;
            hWindowControlSet.Focus();

            lwcl.draw();
            lwcl.method();
            HTuple h = lwcl.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(lwcl);
            HOperatorSet.DispObj(lwcl.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            luowenyouwu jdcl = new luowenyouwu(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            jdcl.LWindowHandle = window.L;
            jdcl.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            jdcl.draw();
            jdcl.method();
            HTuple h = jdcl.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(jdcl);
            HOperatorSet.DispObj(jdcl.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            cunchu xzk = new cunchu(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            //xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            dajingpingjun xzk = new dajingpingjun(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            fengbiyuan xzk = new fengbiyuan(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            lw1031 xzk = new lw1031(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            dingmianzifu xzk = new dingmianzifu(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            gunhuaweizhi xzk = new gunhuaweizhi(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            luomudianpian xzk = new luomudianpian(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            mianjiceliang xzk = new mianjiceliang(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            gunhuaweizhi1 xzk = new gunhuaweizhi1(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            luomudianpianheise xzk = new luomudianpianheise(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            xiangpihei1 xzk = new xiangpihei1(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            xiangpihong1 xzk = new xiangpihong1(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            xiangpihong2 xzk = new xiangpihong2(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            //xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            hsxp xzk = new hsxp(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            //xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            hongsexiangpi xzk = new hongsexiangpi(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            //xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            hongsexiangpi2 xzk = new hongsexiangpi2(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            //
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            heisexiangpi5 xzk = new heisexiangpi5(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            //xzk.draw();诶
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            ts122 xzk = new ts122(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            ts399 xzk = new ts399(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            luowen lw = new luowen(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            lw.LWindowHandle = window.L;
            lw.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            lw.draw();
            lw.method();
            HTuple h = lw.Result;
            HalconHelp.disp_message(window, h, "window", 10, 10, "green", "false");
            imageTools.Add(lw);
            HOperatorSet.DispObj(lw.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;

            //myrake rake = new myrake(Image, tsrc);
            //rake.vOrh = "v";
            //rake.taijie = "true";
            //rake.pixeldist = this.pixelDist;
            //rake.LWindowHandle = window.L;
            //hWindowControlSet.Focus();
            //rake.draw();
            //rake.method();
            //HOperatorSet.DispObj(rake.RegionToDisp, window);
            //HTuple h = rake.Result;
            //HalconHelp.disp_message(window, h, "window", 10, 10, "green", "false");
            //imageTools.Add(rake);

        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            kakou kk = new kakou(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            kk.LWindowHandle = window.L;
            kk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            kk.draw();
            kk.method();
            HTuple h = kk.Result;
            HalconHelp.disp_message(window, h, "window", 10, 10, "green", "false");
            imageTools.Add(kk);
            HOperatorSet.DispObj(kk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            lwceliang lwcl = new lwceliang(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            lwcl.LWindowHandle = window.L;
            lwcl.pixeldist = pixelDist;
            hWindowControlSet.Focus();

            lwcl.draw();
            lwcl.method();
            HTuple h = lwcl.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(lwcl);
            HOperatorSet.DispObj(lwcl.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button30_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            shizicaohezhijing xzk = new shizicaohezhijing(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            zigongluosishizicao xzk = new zigongluosishizicao(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            //xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            shizicaoheibai xzk = new shizicaoheibai(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            //xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            xuanxiang xzk = new xuanxiang(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;

        }

        private void button29_Click_1(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            shangcengbm xzk = new shangcengbm(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button34_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            xuanxianggao xzk = new xuanxianggao(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button35_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            queliao xzk = new queliao(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button36_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            quechi xzk = new quechi(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button37_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            chilunbiaomian xzk = new chilunbiaomian(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button38_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            chilungaodu xzk = new chilungaodu(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button39_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            chilunbiaomian2 xzk = new chilunbiaomian2(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button40_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            chilunbiaomian3 xzk = new chilunbiaomian3(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;

        }

        private void button41_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            chilunbiaomian4 xzk = new chilunbiaomian4(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button42_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            luowenbiaomian1 xzk = new luowenbiaomian1(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button43_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            luowenbiaomian2 xzk = new luowenbiaomian2(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;

        }

        private void button44_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            luowenbiaomian3 xzk = new luowenbiaomian3(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button45_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            lvneiluowen xzk = new lvneiluowen(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button46_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            cekong ck = new cekong(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            ck.LWindowHandle = window.L;
            ck.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            ck.draw();
            ck.method();
            HTuple h = ck.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(ck);
            HOperatorSet.DispObj(ck.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;

        }

        private void button47_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            xbgy xk = new xbgy(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xk.LWindowHandle = window.L;
            xk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xk.draw();
            xk.method();
            HTuple h = xk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xk);
            HOperatorSet.DispObj(xk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;

        }

        private void button48_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            xbcc xk = new xbcc(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xk.LWindowHandle = window.L;
            xk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xk.draw();
            xk.method();
            HTuple h = xk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xk);
            HOperatorSet.DispObj(xk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button49_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            luowenbiaomian4 xzk = new luowenbiaomian4(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;

        }

        private void button50_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            heipi hp = new heipi(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            hp.LWindowHandle = window.L;
            hp.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            hp.draw();
            hp.method();
            HTuple h = hp.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(hp);
            HOperatorSet.DispObj(hp.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;

        }

        private void button51_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            luowenbiaomian5 xzk = new luowenbiaomian5(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button52_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            luowencc xzk = new luowencc(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button53_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            luowenbiaomian6 xzk = new luowenbiaomian6(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;

        }

        private void button54_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            dipanhuokou xzk = new dipanhuokou(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;

        }

        private void button55_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            heipiwaiquan hp = new heipiwaiquan(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            hp.LWindowHandle = window.L;
            hp.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            hp.draw();
            hp.method();
            HTuple h = hp.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(hp);
            HOperatorSet.DispObj(hp.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button56_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            cekong1 ck = new cekong1(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            ck.LWindowHandle = window.L;
            ck.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            ck.draw();
            ck.method();
            HTuple h = ck.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(ck);
            HOperatorSet.DispObj(ck.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button57_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            cekong2 ck = new cekong2(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            ck.LWindowHandle = window.L;
            ck.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            ck.draw();
            ck.method();
            HTuple h = ck.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(ck);
            HOperatorSet.DispObj(ck.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button58_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            dajing ck = new dajing(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            ck.LWindowHandle = window.L;
            ck.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            ck.draw();
            ck.method();
            HTuple h = ck.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(ck);
            HOperatorSet.DispObj(ck.RegionToDisp, hWindowControlSet.HalconWindow);

            ondetect = false;
        }

        private void button59_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            gaodu11 xzk = new gaodu11(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            xzk.LWindowHandle = window.L;
            xzk.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            xzk.draw();
            xzk.method();
            HTuple h = xzk.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(xzk);
            HOperatorSet.DispObj(xzk.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button60_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            waibi1 ck = new waibi1(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            ck.LWindowHandle = window.L;
            ck.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            ck.draw();
            ck.method();
            HTuple h = ck.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(ck);
            HOperatorSet.DispObj(ck.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button61_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            zcdmian1 ck = new zcdmian1(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            ck.LWindowHandle = window.L;
            ck.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            ck.draw();
            ck.method();
            HTuple h = ck.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(ck);
            HOperatorSet.DispObj(ck.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button62_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            nbzcdmian1 ck = new nbzcdmian1(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            ck.LWindowHandle = window.L;
            ck.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            ck.draw();
            ck.method();
            HTuple h = ck.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(ck);
            HOperatorSet.DispObj(ck.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button63_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            dengzuoz ck = new dengzuoz(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            ck.LWindowHandle = window.L;
            ck.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            ck.draw();
            ck.method();
            HTuple h = ck.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(ck);
            HOperatorSet.DispObj(ck.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button64_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            nbzcdmian2 ck = new nbzcdmian2(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            ck.LWindowHandle = window.L;
            ck.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            ck.draw();
            ck.method();
            HTuple h = ck.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(ck);
            HOperatorSet.DispObj(ck.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button65_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            nbzcdmian3 ck = new nbzcdmian3(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            ck.LWindowHandle = window.L;
            ck.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            ck.draw();
            ck.method();
            HTuple h = ck.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(ck);
            HOperatorSet.DispObj(ck.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button66_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            nbzcdmian4 ck = new nbzcdmian4(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            ck.LWindowHandle = window.L;
            ck.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            ck.draw();
            ck.method();
            HTuple h = ck.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(ck);
            HOperatorSet.DispObj(ck.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button67_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            nbzcdmian5 ck = new nbzcdmian5(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            ck.LWindowHandle = window.L;
            ck.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            ck.draw();
            ck.method();
            HTuple h = ck.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(ck);
            HOperatorSet.DispObj(ck.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button68_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            xiaomaoding ck = new xiaomaoding(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            ck.LWindowHandle = window.L;
            ck.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            ck.draw();
            ck.method();
            HTuple h = ck.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(ck);
            HOperatorSet.DispObj(ck.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button69_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            kongxinju ck = new kongxinju(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            ck.LWindowHandle = window.L;
            ck.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            ck.draw();
            ck.method();
            HTuple h = ck.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(ck);
            HOperatorSet.DispObj(ck.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button70_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            xiaomaoding1 ck = new xiaomaoding1(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            ck.LWindowHandle = window.L;
            ck.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            ck.draw();
            ck.method();
            HTuple h = ck.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(ck);
            HOperatorSet.DispObj(ck.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button71_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            waiguan1 ck = new waiguan1(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            ck.LWindowHandle = window.L;
            ck.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            ck.draw();
            ck.method();
            HTuple h = ck.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(ck);
            HOperatorSet.DispObj(ck.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }

        private void button72_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            waiguan2 ck = new waiguan2(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            ck.LWindowHandle = window.L;
            ck.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            ck.draw();
            ck.method();
            HTuple h = ck.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(ck);
            HOperatorSet.DispObj(ck.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;

        }

        private void button73_Click(object sender, EventArgs e)
        {
            if (Image as HObject == null)
            {
                MessageBox.Show("请先读取图像");
                return;
            }
            if (ondetect == false)
            {
                ondetect = true;
            }
            else
            {
                return;

            }
            waiguan3 ck = new waiguan3(Image, tsrc);
            HTuple window = hWindowControlSet.HalconWindow;
            ck.LWindowHandle = window.L;
            ck.pixeldist = pixelDist;
            hWindowControlSet.Focus();
            ck.draw();
            ck.method();
            HTuple h = ck.Result;
            HalconHelp.disp_message(hWindowControlSet.HalconWindow, h, "window", 10, 10, "green", "false");
            imageTools.Add(ck);
            HOperatorSet.DispObj(ck.RegionToDisp, hWindowControlSet.HalconWindow);
            ondetect = false;
        }        
    }
}
