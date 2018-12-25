using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
namespace CameraDetectSystem
{
    [Serializable]
    class xbgy : ImageTools
    {
        #region ROI
        [NonSerialized]
        private HTuple Row1m = new HTuple();
        [NonSerialized]
        private HTuple Col1m = new HTuple();
        [NonSerialized]
        private HTuple Row2m = new HTuple();
        [NonSerialized]
        private HTuple Col2m = new HTuple();
        //[NonSerialized]
        //private HTuple thresholdValue = new HTuple();

        //public double Dthv { set; get; }
        public double DRow1m { set; get; }
        public double DCol1m { set; get; }
        public double DRow2m { set; get; }
        public double DCol2m { set; get; }

        #endregion
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public xbgy()
        {
            //RegionToDisp = Image;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public xbgy(HObject Image, Algorithm al)
        {
            gexxs = 1;
            gex = 0;
            //Initial();
            this.Image = Image;
            this.algorithm.Image = Image;
            this.algorithm = al;
            //HOperatorSet.GenEmptyObj(out RegionToDisp);
            //RegionToDisp.Dispose();
            pixeldist = 1;
        }

        // double rr1, rr2, rc1, rc2;
        public override void draw()
        {
            HTuple Row1m = null, Col1m = null, Row2m = null, Col2m = null;
            HObject ho_Rectangle;
            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.DrawRectangle1(this.LWindowHandle, out Row1m, out Col1m, out Row2m, out Col2m);
            this.DRow1m = Row1m.D;
            this.DCol1m = Col1m.D;
            this.DRow2m = Row2m.D;
            this.DCol2m = Col2m.D;
            //  HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            //  Dthv = thresholdValue.D;
            HOperatorSet.GenRectangle1(out ho_Rectangle, DRow1m, DCol1m, DRow2m, DCol2m);
            ho_Rectangle.Dispose();//

        }
        // Main procedure 
        private void action()
        {
            // Local iconic variables 

            HObject ho_Rectangle, ho_ImageReduced2;
            HObject ho_Region, ho_ConnectedRegions, ho_SelectedRegions;
            HObject ho_RegionFillUp, ho_RegionTrans, ho_Circle, ho_RegionDifference;
            HObject ho_ConnectedRegions1, ho_SelectedRegions1;

            // Local control variables 

            HTuple  hv_Row = null;
            HTuple hv_Column = null, hv_Radius = null, hv_Number = null;
            HTuple hv_a = null, hv_Area = new HTuple(), hv_Row1 = new HTuple();
            HTuple hv_Column1 = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced2);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans);
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {

                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle, DRow1m, DCol1m, DRow2m, DCol2m);
                ho_Region.Dispose();
                HOperatorSet.Intersection(ho_Rectangle, this.algorithm.Region, out ho_Region);
                // ho_ImageMean.Dispose();
                //  HOperatorSet.MeanImage(ho_ImageReduced, out ho_ImageMean, 30, 30);
                // ho_RegionDynThresh.Dispose();
                //   HOperatorSet.DynThreshold(ho_ImageReduced, ho_ImageMean, out ho_RegionDynThresh,
                //       70, "dark");
                ho_RegionFillUp.Dispose();
                HOperatorSet.FillUp(ho_Region, out ho_RegionFillUp);
                ho_RegionTrans.Dispose();
                HOperatorSet.ShapeTrans(ho_RegionFillUp, out ho_RegionTrans, "convex");
                HOperatorSet.SmallestCircle(ho_RegionTrans, out hv_Row, out hv_Column, out hv_Radius);
                ho_Circle.Dispose();
                HOperatorSet.GenCircle(out ho_Circle, hv_Row, hv_Column, hv_Radius*0.95);
                ho_RegionDifference.Dispose();
                HOperatorSet.Difference(ho_Circle, ho_RegionTrans, out ho_RegionDifference);
                ho_ConnectedRegions1.Dispose();
                HOperatorSet.Connection(ho_RegionDifference, out ho_ConnectedRegions1);
                ho_SelectedRegions1.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions1, out ho_SelectedRegions1, "area",
                    "and", 30, 99999);
                HOperatorSet.CountObj(ho_SelectedRegions1, out hv_Number);
                hv_a = 0;
                if ((int)(new HTuple(hv_Number.TupleEqual(0))) != 0)
                {
                    hv_a = 0;

                }
                else if ((int)(new HTuple(hv_Number.TupleGreater(0))) != 0)
                {
                    HOperatorSet.AreaCenter(ho_SelectedRegions1, out hv_Area, out hv_Row1, out hv_Column1);
                    hv_a = hv_Area.TupleSum();
                }
                HOperatorSet.Union1(ho_SelectedRegions1, out RegionToDisp);
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("数量");
                hv_result = hv_result.TupleConcat(hv_Number.D);
                hv_result = hv_result.TupleConcat("面积");
                hv_result = hv_result.TupleConcat(hv_a.D);
                
                result = hv_result.Clone();
                ho_Rectangle.Dispose();
                ho_ImageReduced2.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_RegionTrans.Dispose();
                ho_Circle.Dispose();
                ho_RegionDifference.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                algorithm.Region.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("数量");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("面积");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();
                ho_Rectangle.Dispose();
                ho_ImageReduced2.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_RegionTrans.Dispose();
                ho_Circle.Dispose();
                ho_RegionDifference.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                algorithm.Region.Dispose();

            }
            finally
            {
                ho_Rectangle.Dispose();
                ho_ImageReduced2.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_RegionTrans.Dispose();
                ho_Circle.Dispose();
                ho_RegionDifference.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                algorithm.Region.Dispose();
            }

        }
        public override bool method()
        {
            if (base.method())
            {
                action();
                return true;
            }
            else
            {
                return true;
            }
        }
    }
}


