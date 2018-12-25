using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Windows.Forms;
using System.Xml.Serialization;
namespace CameraDetectSystem
{
    [Serializable]
    public class ImageTools
    {
        [NonSerialized]
        HObject _Image;
        [NonSerialized]
        public HTuple result = new HTuple();
        //[NonSerialized]
        //public HObject _region;
        [NonSerialized]
        public HObject RegionToDisp;
        public int gex = 0;
        public int gexxs = 0;
        public double pixeldist;
        public HTuple Result
        {
            set { result = value; }
            get { return result; }
        }

        public HObject Image
        {
            set
            {
                _Image = value;
                if (algorithm != null)
                    algorithm.Image = value;
            }
            get
            {
                return _Image;
            }
        }

        //public HObject Region { set { _region = value; } get { return _region; } }

        public Algorithm algorithm;

        public long LWindowHandle { set; get; }

        public ImageTools()
        {
            algorithm = new ThresholdSelectRegion();
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }

        public ImageTools(HObject ho_Image)
        {
            this.Image = ho_Image;
            algorithm = new ThresholdSelectRegion();
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }

        ~ImageTools()
        {
            this.Image = null;
        }

        public virtual void draw() { }

        public virtual bool method()
        {

            if (algorithm != null)
            {
                algorithm.ThresholdMethod();
                if (gex == 0)
                {
                    algorithm.SelectMethod();
                }
            }

            else
            {
                MyDebug.ShowMessage("请先进行二值化图像");
                return false;
            }
            if (algorithm.Region == null)
            {
                MyDebug.ShowMessage("请先进行二值化图像");
                return false;
            }
            if (this.Image == null)
            {
                MyDebug.ShowMessage("请先选择图像");
                return false;
            }
            if (gexxs == 0)
            {
                RegionToDisp = algorithm.Region;
            }
            return true;
        }

        public void InitHalcon()
        {
            // Default settings used in HDevelop 
            HOperatorSet.SetSystem("do_low_error", "false");
            if (this.Image == null)
            {
                MyDebug.ShowMessage("初始化失败");
                return;
            }
            HTuple width = new HTuple(), height = new HTuple();
            HOperatorSet.GetImageSize(Image, out width, out height);
            HOperatorSet.SetPart(this.LWindowHandle, 0, 0, height, width);
            HOperatorSet.DispObj(Image, this.LWindowHandle);
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            HalconHelp.set_display_font(this.LWindowHandle, 16, "mono", "true", "false");
            HOperatorSet.SetColor(this.LWindowHandle, "green");
        }
    }
}
