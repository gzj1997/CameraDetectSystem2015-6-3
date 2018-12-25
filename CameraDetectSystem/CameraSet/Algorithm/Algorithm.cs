using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace CameraDetectSystem
{
    [Serializable]
    public class Algorithm
    {
        [NonSerialized]
        HObject _image = null;
        [NonSerialized]
        protected HObject _region = null;
        public Algorithm()
        {
            HOperatorSet.GenEmptyObj(out _image);
            HOperatorSet.GenEmptyObj(out _region);
            _image.Dispose();
            _region.Dispose();
        }
        public HObject Image
        {
            set
            {
                _image = value;
            }
            get
            {
                return _image;
            }
        }
        public HObject Region
        {
            set
            {
                _region = value;
            }
            get
            {
                return _region;
            }
        }
        public void Method()
        {
            try
            {
                ThresholdMethod();
                SelectMethod();
            }
            catch (Exception ex)
            {
                MyDebug.ShowMessage(ex, "请读入一图片Algorithm");
            }
        }
        public virtual void ThresholdMethod()
        {
        }
        public virtual void SelectMethod()
        {
        }
    }
}