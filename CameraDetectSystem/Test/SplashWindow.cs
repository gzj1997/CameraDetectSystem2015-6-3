using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Threading;
using System.Management;
namespace CameraDetectSystem
{
    public partial class SplashWindow : Form
    {
        public SplashWindow()
        {
            InitializeComponent();
            //softReg = new SoftRegister();
            FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        
        private void SplashWindow_Load(object sender, EventArgs e)
        {
            //XmlDocument xmldoc = new XmlDocument();
            //xmldoc.Load(PathHelper.dataPath + @"/license.xml");
            //XmlNodeList xnl;

            //XmlNode root = xmldoc.SelectSingleNode("license");
            //xnl = root.ChildNodes;
            //registerKey = softReg.getRNum();
            //string key = xnl[0].Attributes["value"].Value;

            //if (registerKey == key)
            //{
            //    this.DialogResult = DialogResult.OK;
            //}
            //else
            //{
            //    this.DialogResult = DialogResult.Cancel;
            //}

        }


    }
    public partial class SplashWindow : Form
    {

        //关闭自身 
      
        public void KillMe(object o, EventArgs e)
        {
            this.Close();
        }
        /// <summary> 
        /// 加载并显示主窗体 
        /// </summary> 
        /// <param name="form">主窗体</param> 
        public static void LoadAndRun(Form form)
        {
            //订阅主窗体的句柄创建事件 
            form.HandleCreated += delegate
            {
                //启动新线程来显示Splash窗体 
                new Thread(new ThreadStart(delegate
                {
                    SplashWindow splash = new SplashWindow();
                    //订阅主窗体的Shown事件 
                    form.Shown += delegate
                    {
                        //通知Splash窗体关闭自身 
                        splash.Invoke(new EventHandler(splash.KillMe));
                        splash.Dispose();
                    };
                    //显示Splash窗体 
                    Application.Run(splash);
                })).Start();
            };
            string registerKey = "183470800830        ";
            //SoftRegister softReg=new SoftRegister();
            //  XmlDocument xmldoc = new XmlDocument();
            // xmldoc.Load(PathHelper.dataPath + @"/license.xml");
            // XmlNodeList xnl;

            // XmlNode root = xmldoc.SelectSingleNode("license");
            // xnl = root.ChildNodes;
            // registerKey = softReg.getMNum();
            // string key = xnl[0].Attributes["value"].Value;
            // string key = GetCPUSerialNumber();
            string key = GetHardID();
            //if (registerKey == key)
            //{

                Application.Run(form);

            //}
            //else
            //{

            //    DialogResult dr = MessageBox.Show("主机不支持");
            //}
        }
        static string GetHardID()
        {
            try
            {
                String Hid = "";
                ManagementClass mc = new ManagementClass("Win32_PhysicalMedia");
                //网上有提到，用Win32_DiskDrive，但是用Win32_DiskDrive获得的硬盘信息中并不包含SerialNumber属性。   
                ManagementObjectCollection moc = mc.GetInstances();
                string strID = null;
                foreach (ManagementObject mo in moc)
                {
                    Hid = mo.Properties["SerialNumber"].Value.ToString();
                    break;
                }
                moc = null;
                mc = null;
                return Hid;

            }
            catch
            {
                return "unknow";
            }
            finally
            {

            }
        }
    }
}