using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
namespace CameraDetectSystem
{
    public partial class FormLoadProduct : DevExpress.XtraEditors.XtraForm
    {
        public FormLoadProduct()
        {
            InitializeComponent();
            string path = @"c:\zyw.txt";
            if (!File.Exists(path))
            {
                File.AppendAllText(path, "zhongwen", Encoding.Default);
            }
            string zyw = File.ReadAllText(path, Encoding.Default);
            if (zyw == "zhongwen")
            {
                BtnOK.Text = "确定";
                BtnCancel.Text = "关闭";
                simpleButton1.Text = "新产品";
            }
            if (zyw == "yinwen")
            {
                BtnOK.Text = "Comfirm";
                BtnCancel.Text = "Close";
                simpleButton1.Text = "NewProduct";
            }
            this.BtnOK.DialogResult = DialogResult.OK;
            this.BtnCancel.DialogResult = DialogResult.Cancel;
            simpleButton1.DialogResult = DialogResult.Ignore;
            PathHelper.GetFiles();
            if (PathHelper.productNames != null)
            {
                this.comboBoxEditProduct.Properties.Items.AddRange(PathHelper.productNames);
            }

        }
        //Dictionary<string, string> pathDic;
        private void BtnOK_Click(object sender, EventArgs e)
        {
          string  s = comboBoxEditProduct.SelectedItem.ToString();
          PathHelper.currentProduct=s;
          PathHelper.currentProductPath= PathHelper.pathDic[s];
          
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = @"c:\zyw.txt";
            string zyw = File.ReadAllText(path, Encoding.Default);
            if (zyw == "zhongwen")
            {
                zyw = "yinwen";
            }
            else
            {
                zyw = "zhongwen";
            }
            if (File.Exists(path))
            {
                File.Delete(path);
                File.AppendAllText(path,zyw,Encoding.Default);
            }
            if (zyw == "zhongwen")
            {
                BtnOK.Text = "确定";
                BtnCancel.Text = "关闭";
                simpleButton1.Text = "新产品";
            }
            if (zyw == "yinwen")
            {
                BtnOK.Text = "Comfirm";
                BtnCancel.Text = "Close";
                simpleButton1.Text = "NewProduct";
            }
        }
    }
}