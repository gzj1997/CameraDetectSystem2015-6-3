using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
namespace CameraDetectSystem
{
    class MyDebug
    {
        static public void ShowMessage(Exception ex,string message)
        {
            var trace = new StackTrace(ex, true).GetFrame(0);
            //LOG log = new LOG();
            string s = string.Format("文件名:{0},行号:{1}, 函数名 :{2}", trace.GetFileName(), trace.GetFileLineNumber(), trace.GetMethod());
            //log.WriteLogFile(s);
            Debug.Print(ex.Message);
            Debug.Print(s);
            Debug.Print(message);
        }
        static public void ShowMessage(string message)
        {
            Debug.Print(message);
        }
    }
}
