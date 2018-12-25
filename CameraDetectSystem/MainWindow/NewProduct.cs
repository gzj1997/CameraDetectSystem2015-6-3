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
    public partial class FormNewProduct : Form
    {
        public string ProductName;
        public FormNewProduct(Form f)
        {
            InitializeComponent();
            string path = @"c:\zyw.txt";
            string zyw = File.ReadAllText(path, Encoding.Default);
            if (zyw == "zhongwen")
            {
                BtnSave.Text = "确定";
                BtnCancel.Text = "关闭";
            }
            if (zyw == "yinwen")
            {
                BtnSave.Text = "Save";
                BtnCancel.Text = "Cancle";
            }
            BtnSave.DialogResult = DialogResult.OK;

            BtnCancel.DialogResult = DialogResult.Cancel;

            //path = Application.StartupPath + "\\Data\\Product\\";
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            //string spath = path + EditProductName.Text;
            if(EditProductName.Text!=null&&EditProductName.Text!="")
            ProductName = EditProductName.Text;
            //if (Directory.Exists(spath))
            //{
            //    if (MessageBox.Show("产品已经存在") == DialogResult.OK)
            //    {
            //        Directory.Delete(spath, true);
            //        Directory.CreateDirectory(path);
            //    }
            //}
            //else
            //{
            //    Directory.CreateDirectory(spath);
            //}
        }
        private void SaveFiles()
        {

        }
        private void BtnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
