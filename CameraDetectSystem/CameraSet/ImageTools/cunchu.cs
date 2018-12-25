using System.Text;
using HalconDotNet;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System;
namespace CameraDetectSystem
{
    [Serializable]
    class cunchu : ImageTools
    {
        #region ROI
        [NonSerialized]
        private HTuple dRow = new HTuple();
        [NonSerialized]
        private HTuple dColumn = new HTuple();
        [NonSerialized]
        private HTuple dRadius1 = new HTuple();
        [NonSerialized]
        private HTuple dRadius2 = new HTuple();
        [NonSerialized]
        private HTuple dPhi = new HTuple();
        [field: NonSerializedAttribute()]
        HTuple hv_ModelID = null;
        #endregion
        public double hv_Row { set; get; }
        public double hv_Column { set; get; }
        public double hv_Radius { set; get; }
        public double hv_Radius1 { set; get; }
        public double hv_Radius2 { set; get; }
        public double hv_Phi { set; get; }

        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public cunchu()
        {
            RegionToDisp = Image;
        }
        public cunchu(HObject Image, Algorithm al)
        {
            gex = 0;
            this.algorithm = al;

            this.Image = Image;
            RegionToDisp = Image;
            pixeldist = 1;
        }
        public override void draw()
        {
            HObject ho_Circle, ho_ImageReduced;
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            HOperatorSet.DrawEllipse(this.LWindowHandle, out dRow, out dColumn,
        out dPhi, out dRadius1, out dRadius2);
            ho_Circle.Dispose();
            HOperatorSet.GenEllipse(out ho_Circle, dRow, dColumn, dPhi, dRadius1,
                dRadius2);
            ho_ImageReduced.Dispose();
            HOperatorSet.ReduceDomain(Image, ho_Circle, out ho_ImageReduced);
            HOperatorSet.CreateNccModel(ho_ImageReduced, "auto", -3.14, 3.14, "auto", "use_polarity",
                out hv_ModelID);
            HOperatorSet.WriteNccModel(hv_ModelID, PathHelper.currentProductPath + @"\xzk.ncm");
            this.hv_Row = dRow.D;
            this.hv_Column = dColumn.D;
            this.hv_Radius = dRadius1.D;
            this.hv_Radius = dRadius2.D;
            this.hv_Radius = dPhi.D;
            ho_Circle.Dispose();
            ho_ImageReduced.Dispose();
        }
        //DateTime t1, t2, t3, t4,t5,t6,t7;
        private void action()
        {
            HObject ho_Circle;
            HOperatorSet.GenEmptyObj(out ho_Circle);
            //t3 = DateTime.Now;
            try
            {
                string mz = PathHelper.currentProductPath + @"\" + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ".bmp";
                HOperatorSet.WriteImage(Image, "bmp", 0, mz);

                HOperatorSet.GenRectangle2(out ho_Circle, 20, 20, 0.5, 10, 40);
                HOperatorSet.Union1(ho_Circle, out RegionToDisp);
                ho_Circle.Dispose();
            }
            catch
            {
                ho_Circle.Dispose();
            }

        }
        public override bool method()
        {

            try
            {
                if (base.method())
                {
                    action();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                return false;
            }
        }
    }
}