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
    class dingmianzifu : ImageTools
    {
        #region ROI
        [NonSerialized]
        private HTuple dPhi = new HTuple();
        [NonSerialized]
        private HTuple dLength1 = new HTuple();
        [NonSerialized]
        private HTuple dLength2 = new HTuple();
        [NonSerialized]
        private HTuple dRow = new HTuple();
        [NonSerialized]
        private HTuple dColumn = new HTuple();
        [NonSerialized]
        private HTuple ddPhi = new HTuple();
        [NonSerialized]
        private HTuple ddLength1 = new HTuple();
        [NonSerialized]
        private HTuple ddLength2 = new HTuple();
        [NonSerialized]
        private HTuple ddRow = new HTuple();
        [NonSerialized]
        private HTuple ddColumn = new HTuple();
        [NonSerialized]
        private HTuple hv_Radiusd = new HTuple();

        [NonSerialized]

        HTuple hv_Col = new HTuple(), hv_dis = new HTuple(), hv_dxi = new HTuple(), hv_Exception = new HTuple(), hv_dn = new HTuple(),
        hv_dxr = new HTuple(), hv_dxc = new HTuple(), hv_Row2 = new HTuple(), hv_dnr = new HTuple(),
        hv_dnc = new HTuple(), hv_dni = new HTuple(), hv_Ix = new HTuple(), hv_In, hv_dx = new HTuple();
        [NonSerialized]
        HTuple hv_djd = new HTuple(), hv_djdr = new HTuple(), hv_djdc = new HTuple(), hv_djx = new HTuple(),
        hv_djxr = new HTuple(), hv_jh = new HTuple(), hv_j = new HTuple(), hv_djxc = new HTuple(), hv_xjd = new HTuple(),
        hv_xjdr = new HTuple(), hv_xjdc = new HTuple(), hv_xjx = new HTuple(), hv_xjxr = new HTuple(),
        hv_xjxc = new HTuple(), hv_ljd = new HTuple(), hv_ljx = new HTuple();

        [NonSerialized]
        private HTuple thresholdValue = new HTuple();
        [field: NonSerializedAttribute()]
        HTuple hv_ModelID = null;
        #endregion
        public double hv_Row { set; get; }
        public double hv_Column { set; get; }
        public double hv_Phi { set; get; }
        public double thv { set; get; }
        public double hv_Length1 { set; get; }
        public double hv_Length2 { set; get; }
        public double hv1_Row { set; get; }
        public double hv1_Column { set; get; }
        public double hv1_Phi { set; get; }
        public double hv1_Length1 { set; get; }
        public double hv1_Length2 { set; get; }
        public double hv_Radius { set; get; }
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public dingmianzifu()
        {
            RegionToDisp = Image;
        }
        public dingmianzifu(HObject Image, Algorithm al)
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
            HOperatorSet.DrawCircle(this.LWindowHandle, out dRow, out dColumn, out dPhi);
            this.hv_Radius = dPhi;
            ho_Circle.Dispose();
            HOperatorSet.GenCircle(out ho_Circle,dRow,dColumn,dPhi);
            ho_ImageReduced.Dispose();
            HOperatorSet.ReduceDomain(Image, ho_Circle, out ho_ImageReduced);
            HOperatorSet.CreateNccModel(ho_ImageReduced, 0, -3.14, 6.29, 0.0175, "use_polarity",
                out hv_ModelID);
            HOperatorSet.WriteNccModel(hv_ModelID,PathHelper.currentProductPath + @"\zifu.ncm");
            ho_Circle.Dispose();
            ho_ImageReduced.Dispose();
        }
        //DateTime t1, t2, t3, t4,t5,t6,t7;
        private void action()
        {
            HTuple hv_Row1=null,hv_Column1=null, hv_Angle=null, hv_Score=null;
            HObject ho_RegionClosing, ho_RegionErosion, ho_ImageReduced, ho_Circle, ho_Contours, ho_Region1;
            HOperatorSet.GenEmptyObj(out ho_RegionClosing);
            HOperatorSet.GenEmptyObj(out ho_RegionErosion);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_Contours);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            //t3 = DateTime.Now;
            try
            {
                if (hv_ModelID == null)
                {
                    HOperatorSet.ReadNccModel(PathHelper.currentProductPath + @"\zifu.ncm", out hv_ModelID);
                }
                ho_RegionClosing.Dispose();
                HOperatorSet.ClosingCircle(algorithm.Region, out ho_RegionClosing, hv_Radius);
                ho_RegionErosion.Dispose();
                HOperatorSet.ErosionCircle(ho_RegionClosing, out ho_RegionErosion, 10.5);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_RegionErosion, out ho_ImageReduced);
                HOperatorSet.FindNccModel(ho_ImageReduced, hv_ModelID, -3.14, 6.29, 0.3, 1,
                    0.7, "false", 0, out hv_Row1, out hv_Column1, out hv_Angle, out hv_Score);
                if ((int)(new HTuple((new HTuple(hv_Row1.TupleLength())).TupleEqual(1))) != 0)
                {
                    ho_Circle.Dispose();
                    HOperatorSet.GenCircle(out ho_Circle, hv_Row1, hv_Column1, hv_Radius);
                    ho_Contours.Dispose();
                    HOperatorSet.GenContourRegionXld(ho_Circle, out ho_Contours, "border");
                    HOperatorSet.GetContourXld(ho_Contours, out hv_Row2, out hv_Col);
                    ho_Region1.Dispose();
                    HOperatorSet.GenRegionPoints(out ho_Region1, hv_Row2, hv_Col);
                    HOperatorSet.Union1(ho_Region1, out RegionToDisp);
                    HTuple hv_result = GetHv_result();
                    hv_result = hv_result.TupleConcat("相似度");
                    hv_result = hv_result.TupleConcat(hv_Score);
                    result = hv_result.Clone();
                }
                else
                {
                    HTuple hv_result = GetHv_result();
                    hv_result = hv_result.TupleConcat("相似度");
                    hv_result = hv_result.TupleConcat(0);
                    result = hv_result.Clone();
                }
                ho_RegionClosing.Dispose();
                ho_RegionErosion.Dispose();
                ho_ImageReduced.Dispose();
                ho_Circle.Dispose();
                ho_Contours.Dispose();
                ho_Region1.Dispose();
                algorithm.Region.Dispose();
                //t4 = DateTime.Now;
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("相似度");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();
                ho_RegionClosing.Dispose();
                ho_RegionErosion.Dispose();
                ho_ImageReduced.Dispose();
                ho_Circle.Dispose();
                ho_Contours.Dispose();
                ho_Region1.Dispose();
                algorithm.Region.Dispose();

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