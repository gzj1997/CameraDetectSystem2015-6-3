using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Xml;
using System.Threading;
using System.IO;
namespace CameraDetectSystem
{
    public partial class ControlSet : DevExpress.XtraEditors.XtraForm
    {
        public ControlSet()
        {
            InitializeComponent();
            string path = @"c:\zyw.txt";
            string zyw = File.ReadAllText(path, Encoding.Default);
            if (zyw == "zhongwen")
            {
                groupControl2.Text = "转盘控制";
                labelControl1.Text = "当前速度";
                BtnPanCtrl.Text="转盘启动测试";
                groupControl1.Text="控制测试";
                BtnPan.Text = "启动振动盘";
                simpleButton2.Text = "电磁阀一测试";
                simpleButton3.Text = "电磁阀二测试";
                BtnSaveC1Param.Text = "保存设定";
                labelControl5.Text = "像素比";
                labelControl4.Text = "曝光";
                labelControl3.Text = "增益";
                labelControl8.Text = "像素比";
                labelControl7.Text = "曝光";
                labelControl6.Text = "增益";
                labelControl11.Text = "像素比";
                labelControl10.Text = "曝光";
                labelControl9.Text = "增益";
                labelControl14.Text = "像素比";
                labelControl13.Text = "曝光";
                labelControl12.Text = "增益";
                labelControl17.Text = "像素比";
                labelControl16.Text = "曝光";
                labelControl15.Text = "增益";
            }
            if (zyw == "yinwen")
            {
                groupControl2.Text = " TurntableControl";
                labelControl1.Text = "Speed";
                BtnPanCtrl.Text = " TurntableStartTest";
                groupControl1.Text = "ControlTest";
                BtnPan.Text = "VibratingDiskStart";
                simpleButton2.Text = "Radiotube1Test";
                simpleButton3.Text = "Radiotube2Test";
                BtnSaveC1Param.Text = "SaveSet";
                labelControl5.Text = "PixelRatio";
                labelControl4.Text = "ExposureTime";
                labelControl3.Text = "CaremaGain";
                labelControl8.Text = "PixelRatio";
                labelControl7.Text = "ExposureTime";
                labelControl6.Text = "CaremaGain";
                labelControl11.Text = "PixelRatio";
                labelControl10.Text = "ExposureTime";
                labelControl9.Text = "CaremaGain";
                labelControl14.Text = "PixelRatio";
                labelControl13.Text = "ExposureTime";
                labelControl12.Text = "CaremaGain";
                labelControl17.Text = "PixelRatio";
                labelControl16.Text = "ExposureTime";
                labelControl15.Text = "CaremaGain";
            }
            this.trackBarControl1.Properties.Maximum = 80000;
            this.trackBarControl1.Properties.Minimum = 0;
            trackBarControl1.Value = Card.maxspeed;

            //if (PCI408.PCI408_read_SEVON_PIN(Card.cardNo)==1)
            //{
            //    Panstart = true;
            //}
            //else
            //{
            //    Panstart = false;
            //}
        }

        int speed = 0;
        bool Panstart = false;
        private void BtnPanCtrl_Click(object sender, EventArgs e)
        {
            if (!Panstart)
            {
                Turntable.Instance.Start();
                

                speed = speed < 1000 ? 1000 : speed;
                PCI408.PCI408_write_SEVON_PIN(Card.cardNo, Card.On);
                PCI408.PCI408_set_profile(Card.cardNo, 1000, speed, Card.acc, Card.acc);
                PCI408.PCI408_vmove(Card.cardNo, 0, Card.maxspeed);
                PCI408.PCI408_write_outbit(Card.cardNo, Card.chuiqizongkaiguan, Card.On);
                BtnPanCtrl.Text = "停止运动";
            }
            else
            {
                Turntable.Instance.Stop();
                
                PCI408.PCI408_decel_stop(Card.cardNo, Card.acc);
                PCI408.PCI408_write_outbit(Card.cardNo, Card.chuiqizongkaiguan, Card.Off);
                Thread.Sleep(5000);
                BtnPanCtrl.Text = "转盘启动测试";
            }
            Panstart = !Panstart;
        }
        

        private void trackBarControl1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                speed = trackBarControl1.Value;
                lblSpeed.Text = trackBarControl1.Value.ToString();
                XmlDocument xd = new XmlDocument();
                string path=PathHelper.dataPath + @"\Position.xml";
                xd.Load(path);
                
                XmlNode xn;
                xn = xd.GetElementsByTagName("速度")[0];
                
                if (xn != null)
                {
                    XmlNodeList xnl;
                    xnl = xn.ChildNodes;
                    foreach (XmlNode x in xnl)
                    {
                        x.Attributes["Value"].Value = speed.ToString();
                        Card.maxspeed = speed;
                    }
                    
                }
                xd.Save(path);
            }
            catch 
            {
                MyDebug.ShowMessage("速度保存错误");
            }
          
            
            
        }

        private void BtnSaveC1Param_Click(object sender, EventArgs e)
        {
            double pixelDist=0;
            //bool ok=false;
            int exposuretime=0;
            int gain=0;

            XmlDocument xd = new XmlDocument();
            string path = PathHelper.dataPath + @"\CameraSettings.xml";
            xd.Load(path);
            XmlNode xn;

            xn = xd.GetElementsByTagName("Camera")[0];

            if (xn != null)
            {
                XmlNodeList xnl;
                xnl = xn.ChildNodes;
                foreach (XmlNode x in xnl)
                {
                    switch (tab.SelectedTabPage.Text)
                    {
                        case "CCD1": {
                            double.TryParse(C1PixelDist.Text,out pixelDist);
                            int.TryParse(C1ExposureTime.Text, out exposuretime);
                            int.TryParse(C1Gain.Text,out gain);
                        }
                            break;
                        case "CCD2":
                            {
                                double.TryParse(C2PixelDist.Text, out pixelDist);
                                int.TryParse(C2ExposureTime.Text, out exposuretime);
                                int.TryParse(C2Gain.Text, out gain);
                            }
                            break;
                        case "CCD3":
                            {
                                double.TryParse(C3PixelDist.Text, out pixelDist);
                                int.TryParse(C3ExposureTime.Text, out exposuretime);
                                int.TryParse(C3Gain.Text, out gain);
                            }
                            break;
                        case "CCD4":
                            {
                                double.TryParse(C4PixelDist.Text, out pixelDist);
                                int.TryParse(C4ExposureTime.Text, out exposuretime);
                                int.TryParse(C4Gain.Text, out gain);
                            }
                            break;
                        case "CCD5":
                            {
                                double.TryParse(C5PixelDist.Text, out pixelDist);
                                int.TryParse(C5ExposureTime.Text, out exposuretime);
                                int.TryParse(C5Gain.Text, out gain);
                            }
                            break;
                    }
                    //if (gain < 280)
                    //{
                    //    gain = 280;
                    //}

                    if (x.Name == tab.SelectedTabPage.Text)
                    {
                        x.Attributes["ExposureTime"].Value = exposuretime.ToString();
                        x.Attributes["PixelDist"].Value = pixelDist.ToString();
                        x.Attributes["Gain"].Value = gain.ToString();
                     //   MainForm frm1 = (MainForm)this.Owner;
                    //    foreach (Camera c in frm1.cameras)
                    //    {
                    //        if (c.logicName == tab.SelectedTabPage.Text)
                    //        {
                    //            c.ExposureTime = exposuretime;
                    //            c.PixelDist = pixelDist;
                    //            c.Gain = gain;
                    //        }
                    //    }
                    }
                }
            }
            xd.Save(path);

            
            
        }

        private void ControlSet_Load(object sender, EventArgs e)
        {
            

            XmlDocument xd = new XmlDocument();
            string path = PathHelper.dataPath + @"\CameraSettings.xml";
            xd.Load(path);
            XmlNode xn;

            xn = xd.GetElementsByTagName("Camera")[0];
            string exposureTime;
            string pixelDist;
            string gain;
            if (xn != null)
            {
                XmlNodeList xnl;
                xnl = xn.ChildNodes;
                foreach (XmlNode x in xnl)
                {
                    exposureTime = x.Attributes["ExposureTime"].Value;
                    pixelDist = x.Attributes["PixelDist"].Value;
                    gain = x.Attributes["Gain"].Value;

                    if (x.Name == "CCD1")
                    {
                        C1ExposureTime.Text = exposureTime;
                        C1PixelDist.Text = pixelDist;
                        C1Gain.Text = gain;
                    }
                    if (x.Name == "CCD2")
                    {
                        C2ExposureTime.Text = exposureTime;
                        C2PixelDist.Text = pixelDist;
                        C2Gain.Text = gain;
                    }
                    if (x.Name == "CCD3")
                    {
                        C3ExposureTime.Text = exposureTime;
                        C3PixelDist.Text = pixelDist;
                        C3Gain.Text = gain;
                    }
                    if (x.Name == "CCD4")
                    {
                        C4ExposureTime.Text = exposureTime;
                        C4PixelDist.Text = pixelDist;
                        C4Gain.Text = gain;
                    }
                    if (x.Name == "CCD5")
                    {
                        C5ExposureTime.Text = exposureTime;
                        C5PixelDist.Text = pixelDist;
                        C5Gain.Text = gain;
                    }
                }
            }
            xd.Save(path);

            //for (ushort i = 0; i < 32; i++)
            //{
            //    PCI408.PCI408_write_outbit(Card.cardNo, (ushort)(Card.Out1 + i), Card.Off);
            //}
            
        }
    }
}