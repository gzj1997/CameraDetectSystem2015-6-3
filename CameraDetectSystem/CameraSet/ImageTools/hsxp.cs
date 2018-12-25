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
    class hsxp : ImageTools
    {
        //#region ROI
        //[NonSerialized]
        //private HTuple dPhi = new HTuple();
        //[NonSerialized]
        //private HTuple mianjisx = new HTuple();
        //[NonSerialized]
        //private HTuple mianjixx = new HTuple();
        //[NonSerialized]
        //private HTuple dLength1 = new HTuple();
        //[NonSerialized]
        //private HTuple dLength2 = new HTuple();
        //[NonSerialized]
        //private HTuple dRow = new HTuple();
        //[NonSerialized]
        //private HTuple dColumn = new HTuple();
        //[NonSerialized]
        //private HTuple ddPhi = new HTuple();
        //[NonSerialized]
        //private HTuple ddLength1 = new HTuple();
        //[NonSerialized]
        //private HTuple ddLength2 = new HTuple();
        //[NonSerialized]
        //private HTuple ddRow = new HTuple();
        //[NonSerialized]
        //private HTuple ddColumn = new HTuple();
        //[NonSerialized]
        //private HTuple hv_Radiusd = new HTuple();

        //[NonSerialized]

        //HTuple hv_Col = new HTuple(), hv_dis = new HTuple(), hv_dxi = new HTuple(), hv_Exception = new HTuple(), hv_dn = new HTuple(),
        //hv_dxr = new HTuple(), hv_dxc = new HTuple(), hv_Row2 = new HTuple(), hv_dnr = new HTuple(),
        //hv_dnc = new HTuple(), hv_dni = new HTuple(), hv_Ix = new HTuple(), hv_In, hv_dx = new HTuple();
        //[NonSerialized]
        //HTuple hv_djd = new HTuple(), hv_djdr = new HTuple(), hv_djdc = new HTuple(), hv_djx = new HTuple(),
        //hv_djxr = new HTuple(), hv_jh = new HTuple(), hv_j = new HTuple(), hv_djxc = new HTuple(), hv_xjd = new HTuple(),
        //hv_xjdr = new HTuple(), hv_xjdc = new HTuple(), hv_xjx = new HTuple(), hv_xjxr = new HTuple(),
        //hv_xjxc = new HTuple(), hv_ljd = new HTuple(), hv_ljx = new HTuple();

        //[NonSerialized]
        //private HTuple thresholdValue = new HTuple();
        //[field: NonSerializedAttribute()]
        //HTuple hv_ModelID = null;
        //#endregion
        //public double hv_Row { set; get; }
        //public double hv_Column { set; get; }
        //public double hv_Phi { set; get; }
        //public double thv { set; get; }
        //public double hv_Length1 { set; get; }
        //public double hv_Length2 { set; get; }
        //public double hv1_Row { set; get; }
        //public double hv1_Column { set; get; }
        //public double hv1_Phi { set; get; }
        //public double hv1_Length1 { set; get; }
        //public double hv1_Length2 { set; get; }
        //public double hv_Radius { set; get; }
        //public double mjsx { set; get; }
        //public double mjxx { set; get; }
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public hsxp()
        {
            RegionToDisp = Image;
        }
        public hsxp(HObject Image, Algorithm al)
        {
            gex = 0;
            this.algorithm = al;

            this.Image = Image;
            RegionToDisp = Image;
            pixeldist = 1;
        }
        public override void draw()
        {
            //HObject ho_Circle, ho_ImageReduced;
            //HOperatorSet.GenEmptyObj(out ho_Circle);
            //HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            //HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            //HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            //HOperatorSet.DrawCircle(this.LWindowHandle, out dRow, out dColumn, out dPhi);
            //this.hv_Radius = dPhi;
            //HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            //thv = thresholdValue.D;
            //HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\mianjixx", out mianjixx);
            //mjxx = mianjixx.D;
            //HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\mianjisx", out mianjisx);
            //mjsx = mianjisx.D;
            ////ho_Circle.Dispose();
            ////HOperatorSet.GenCircle(out ho_Circle, dRow, dColumn, dPhi);
            ////ho_ImageReduced.Dispose();
            ////HOperatorSet.ReduceDomain(Image, ho_Circle, out ho_ImageReduced);
            ////HOperatorSet.CreateNccModel(ho_ImageReduced, 0, -3.14, 6.29, 0.0175, "use_polarity",
            ////    out hv_ModelID);
            ////HOperatorSet.WriteNccModel(hv_ModelID, PathHelper.currentProductPath + @"\zifu.ncm");
            //ho_Circle.Dispose();
            //ho_ImageReduced.Dispose();
        }
        //DateTime t1, t2, t3, t4,t5,t6,t7;
        private void action()
        {
            HObject ho_GrayImage, ho_Region,regd,regd2,regd3;
            HObject ho_ConnectedRegions, ho_SelectedRegions, ho_Circle;
            HObject ho_ImageReduced, ho_Region1, ho_RegionClosing, ho_ConnectedRegions1;
            HObject ho_SelectedRegions1;

            // Local control variables 

            HTuple hv_Row2 = null, hv_Column2 = null, hv_Radius1 = null,hd=null;
            HTuple hv_Row = null, hv_Column = null, hv_Radius = null;
            HTuple hv_Mean = null, hv_Deviation = null, hv_Circularity = null;
            HTuple hv_Area = null, hv_Row1 = null, hv_Column1 = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_GrayImage);
            HOperatorSet.GenEmptyObj(out regd);
            HOperatorSet.GenEmptyObj(out regd3);
            HOperatorSet.GenEmptyObj(out regd2);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            //t3 = DateTime.Now;
            try
            {
                ho_GrayImage.Dispose();
                HOperatorSet.Rgb1ToGray(Image, out ho_GrayImage);
                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_GrayImage, out ho_Region, 40, 255);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShapeStd(ho_ConnectedRegions, out ho_SelectedRegions, "max_area",
                    70);
                regd.Dispose();
                HOperatorSet.FillUp(ho_SelectedRegions, out regd);
                HOperatorSet.InnerCircle(regd, out hv_Row2, out hv_Column2, out hv_Radius1);
                HOperatorSet.SmallestCircle(ho_SelectedRegions, out hv_Row, out hv_Column, out hv_Radius);
                ho_Circle.Dispose();
                HOperatorSet.GenCircle(out ho_Circle, hv_Row, hv_Column, hv_Radius / 1.7);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(ho_GrayImage, ho_Circle, out ho_ImageReduced);
                HOperatorSet.Intensity(ho_Circle, ho_ImageReduced, out hv_Mean, out hv_Deviation);
                ho_Region1.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region1, hv_Mean - 10, 255);
                ho_RegionClosing.Dispose();
                HOperatorSet.ClosingCircle(ho_Region1, out ho_RegionClosing, 3.5);
                ho_ConnectedRegions1.Dispose();
                HOperatorSet.Connection(ho_RegionClosing, out ho_ConnectedRegions1);
                ho_SelectedRegions1.Dispose();
                HOperatorSet.SelectShapeStd(ho_ConnectedRegions1, out ho_SelectedRegions1, "max_area",
                    70);
                regd2.Dispose();
                HOperatorSet.FillUp(ho_SelectedRegions1, out regd2);
                HOperatorSet.Circularity(regd2, out hv_Circularity);
                HOperatorSet.AreaCenter(regd2, out hv_Area, out hv_Row1, out hv_Column1);
                HOperatorSet.Union1(regd2, out RegionToDisp);
                HOperatorSet.ReduceDomain(ho_GrayImage,regd2,out regd3);
                HOperatorSet.Intensity(regd2, regd3, out hd, out hv_Row1);
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("橡皮内孔圆度");
                hv_result = hv_result.TupleConcat(hv_Circularity.D);
                hv_result = hv_result.TupleConcat("橡皮内孔面积");
                hv_result = hv_result.TupleConcat(hv_Area.D);
                hv_result = hv_result.TupleConcat("金属外圆");
                hv_result = hv_result.TupleConcat(hv_Radius.D * pixeldist * 2);
                hv_result = hv_result.TupleConcat("金属内圆");
                hv_result = hv_result.TupleConcat(hv_Radius1.D * pixeldist *2 );
                hv_result = hv_result.TupleConcat("内孔灰度");
                hv_result = hv_result.TupleConcat(hd.D);
                result = hv_result.Clone();

                //t4 = DateTime.Now;
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("橡皮内孔圆度");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("橡皮内孔面积");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("金属外圆");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("金属内圆");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("内孔灰度");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();



            }
            finally
            {
                regd.Dispose();
                regd2.Dispose();
                regd3.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_Circle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region1.Dispose();
                ho_RegionClosing.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
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