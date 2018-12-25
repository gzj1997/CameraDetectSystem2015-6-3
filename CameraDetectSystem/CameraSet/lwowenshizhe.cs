using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CameraDetectSystem.CameraSet
{
    public partial class lwowenshizhe : Form
    {
        public lwowenshizhe()
        {
            InitializeComponent();
        }

        private void lwowenshizhe_Load(object sender, EventArgs e)
        {

        }
        int yac;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                yac = int.Parse(textBox1.Text);
                HOperatorSet.WriteTuple(yac, PathHelper.currentProductPath + "/yac.tup");
            }
            catch
            {
                MessageBox.Show("输入错误！");
            }
        }
    }
}
