using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
    [Serializable]
    class fengbiyuan : ImageTools
    {
        #region ROI
        [NonSerialized]
        private HTuple dPhi = new HTuple();
        [NonSerialized]
        private HTuple dLength1 = new HTuple();
        [NonSerialized]
        private HTuple dLength2 = new HTuple();
        [NonSerialized]
        private HTuple thresholdValue = new HTuple();
        [NonSerialized]

        private HTuple dcenterRow = new HTuple();
        [NonSerialized]
        private HTuple dcenterColumn = new HTuple();
        #endregion
        public double thv { set; get; }
        public double hv_Length1m { set; get; }
        public double hv_Length2m { set; get; }
        public double hv_Phim { set; get; }
        public double hv_centerRowm { set; get; }
        public double hv_centerColumnm { set; get; }
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public fengbiyuan()
        {
            RegionToDisp = Image;
        }
        public fengbiyuan(HObject Image, Algorithm al)
        {
            gexxs = 1;
            gex = 0;
            this.Image = Image;
            this.algorithm.Image = Image;
            this.algorithm = al;
            pixeldist = 1;
        }
        public override void draw()
        {
            HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            thv = thresholdValue.D;
            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            //HOperatorSet.DrawRectangle2(this.LWindowHandle, out dcenterRow, out dcenterColumn,
    //out dPhi, out dLength1, out dLength2);
            HOperatorSet.DrawCircle(this.LWindowHandle, out dcenterRow, out dcenterColumn,
    out dPhi);
            //this.hv_Length1m = dLength1.D;
            //this.hv_Length2m = dLength2.D;
            this.hv_Phim = dPhi.D;
            this.hv_centerRowm = dcenterRow.D;
            this.hv_centerColumnm = dcenterColumn.D;
        }
        private void action()
        {

            HObject ho_Rectangle = null;
            HObject ho_Region = null, ho_ConnectedRegions = null, ho_RegionFillUp = null,tt=null;

            // Local control variables 
            HTuple hv_Mean = new HTuple();
            HTuple hv_Deviation = new HTuple(), hv_Area = new HTuple();
            HTuple hv_Row = new HTuple(), hv_Column = new HTuple();
            HTuple hv_mianji = new HTuple(), hv_Indices = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out tt);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {
                ho_Rectangle.Dispose();
                HOperatorSet.GenCircle(out ho_Rectangle, hv_centerRowm, hv_centerColumnm, hv_Phim);
                tt.Dispose();
                HOperatorSet.ReduceDomain(Image,ho_Rectangle,out tt);
                HOperatorSet.Intensity(ho_Rectangle, tt, out hv_Mean, out hv_Deviation);
                ho_Region.Dispose();
                HOperatorSet.Threshold(tt, out ho_Region, ((((hv_Mean + (hv_Deviation * 1.5))).TupleConcat(
                    250))).TupleMin(), 255);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
                ho_RegionFillUp.Dispose();
                HOperatorSet.FillUp(ho_ConnectedRegions, out ho_RegionFillUp);
                HOperatorSet.AreaCenter(ho_RegionFillUp, out hv_Area, out hv_Row, out hv_Column);
                hv_mianji = hv_Area.TupleMax();
                HOperatorSet.TupleFind(hv_Area, hv_mianji, out hv_Indices);
                HOperatorSet.SelectObj(ho_ConnectedRegions, out RegionToDisp, hv_Indices + 1);

                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("封闭圆");
                hv_result = hv_result.TupleConcat(hv_mianji.D);
                result = hv_result.Clone();

                ho_Rectangle.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                algorithm.Region.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("封闭圆");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();

                ho_Rectangle.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
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