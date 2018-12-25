using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PylonC.NETSupportLibrary;
using PylonC.NET;
using System.Diagnostics;
using HalconDotNet;
namespace CameraDetectSystem
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //string productPath;
            try
            {
                bool runone;
                System.Threading.Mutex run = new System.Threading.Mutex(true, "xinbiao_a_test", out runone);
                if (runone)
                {
                    run.ReleaseMutex();
#if DEBUG
                    /* This is a special debug setting needed only for GigE cameras.
                See 'Building Applications with pylon' in the Programmer's Guide. */
                    Environment.SetEnvironmentVariable("PYLON_GIGE_HEARTBEAT", "300000" /*ms*/);
#endif
                    //Pylon.Initialize();
                    int cards = 0;

                    cards = PCI408.PCI408_card_init();
                    if (cards == 0)
                    {
                        MessageBox.Show("运动控制卡丢失");
                    }
                    else if(cards>8)
                    {
                        MessageBox.Show("运动控制卡初始化错误 错误号: "+cards.ToString());
                    }
                    HOperatorSet.SetSystem("do_low_error", "false");
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    //Application.Run(new MainForm());
                    SplashWindow.LoadAndRun(new MainForm());
                    //Pylon.Terminate();

                }
                else
                {
                    MessageBox.Show("视觉软件已经打开。");
                }                                                                                                 
            }
            catch (Exception ex)
            {
                var trace = new StackTrace(ex, true).GetFrame(0);
                //LOG log = new LOG();
                string s = string.Format("文件名:{0},行号:{1}, 函数名 :{2}", trace.GetFileName(), trace.GetFileLineNumber(), trace.GetMethod());
                //log.WriteLogFile(s);
                Debug.Print(ex.Message);
                Debug.Print(s);
            }
            finally
            {
               // Pylon.Terminate();
                PCI408.PCI408_board_close();
            }
        }
    }
}
