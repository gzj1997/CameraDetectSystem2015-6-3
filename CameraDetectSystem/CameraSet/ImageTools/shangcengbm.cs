using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
namespace CameraDetectSystem
{
    [Serializable]
    class shangcengbm : ImageTools
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
        public shangcengbm()
        {
            //RegionToDisp = Image;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public shangcengbm(HObject Image, Algorithm al)
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

            HObject ho_Rectangle, ho_Region1;
            HObject ho_ImageMean, ho_RegionDynThresh, ho_RegionTrans;
            HObject ho_Circle, ho_Circle1, ho_RegionDifference, ho_ImageReduced1;
            HObject ho_Region, ho_ConnectedRegions, ho_RegionFillUp;
            HObject ho_SelectedRegions, ho_ObjectSelected = null;

            // Local control variables 

            HTuple hv_Row = null, hv_Column = null;
            HTuple hv_Radius = null, hv_Number = null, hv_a = null;
            HTuple hv_r = null, hv_c = null, hv_i = null, hv_Area = new HTuple();
            HTuple hv_Row3 = new HTuple(), hv_Column3 = new HTuple();
            HTuple hv_b = null, hv_b1 = new HTuple(), hv_b2 = new HTuple();
            HTuple hv_b3 = new HTuple(), hv_b4 = new HTuple(), hv_b5 = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            //HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_ImageMean);
            HOperatorSet.GenEmptyObj(out ho_RegionDynThresh);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans);
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_Circle1);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {

                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle, DRow1m, DCol1m, DRow2m, DCol2m);
                ho_Region1.Dispose();
                HOperatorSet.Intersection(ho_Rectangle, this.algorithm.Region, out ho_Region1);
                // ho_ImageMean.Dispose();
                //  HOperatorSet.MeanImage(ho_ImageReduced, out ho_ImageMean, 30, 30);
                // ho_RegionDynThresh.Dispose();
                //   HOperatorSet.DynThreshold(ho_ImageReduced, ho_ImageMean, out ho_RegionDynThresh,
                //       70, "dark");
                ho_RegionTrans.Dispose();
                HOperatorSet.ShapeTrans(ho_Region1, out ho_RegionTrans, "outer_circle");
                HOperatorSet.SmallestCircle(ho_RegionTrans, out hv_Row, out hv_Column, out hv_Radius);
                ho_Circle.Dispose();
                HOperatorSet.GenCircle(out ho_Circle, hv_Row, hv_Column, hv_Radius - 60);
                //ho_Circle1.Dispose();
                //HOperatorSet.GenCircle(out ho_Circle1, hv_Row, hv_Column, hv_Radius);
                ho_RegionDifference.Dispose();
                HOperatorSet.Difference(ho_Region1, ho_Circle, out ho_RegionDifference);
                ho_ImageReduced1.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_RegionDifference, out ho_ImageReduced1
                    );
                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced1, out ho_Region, 180, 255);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
                ho_RegionFillUp.Dispose();
                HOperatorSet.FillUp(ho_ConnectedRegions, out ho_RegionFillUp);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_RegionFillUp, out ho_SelectedRegions, "area", "and",
                    150, 9999900);
                HOperatorSet.CountObj(ho_SelectedRegions, out hv_Number);
                hv_a = new HTuple();
                hv_r = new HTuple();
                hv_c = new HTuple();
                HTuple end_val22 = hv_Number;
                HTuple step_val22 = 1;
                for (hv_i = 1; hv_i.Continue(end_val22, step_val22); hv_i = hv_i.TupleAdd(step_val22))
                {
                    ho_ObjectSelected.Dispose();
                    HOperatorSet.SelectObj(ho_SelectedRegions, out ho_ObjectSelected, hv_i);
                    HOperatorSet.AreaCenter(ho_ObjectSelected, out hv_Area, out hv_Row3, out hv_Column3);
                    hv_a = hv_a.TupleConcat(hv_Area);
                    hv_r = hv_r.TupleConcat(hv_Row3);
                    hv_c = hv_c.TupleConcat(hv_Column3);
                }
                HOperatorSet.TupleSort(hv_a, out hv_b);
                if ((int)(new HTuple((new HTuple(hv_b.TupleLength())).TupleGreater(3))) != 0)
                {
                    hv_b1 = hv_b[(new HTuple(hv_b.TupleLength())) - 1];
                    hv_b2 = hv_b[(new HTuple(hv_b.TupleLength())) - 2];
                    hv_b3 = hv_b[(new HTuple(hv_b.TupleLength())) - 3];
                    hv_b4 = hv_b[(new HTuple(hv_b.TupleLength())) - 4];
                    //hv_b5 = hv_b[(new HTuple(hv_b.TupleLength())) - 8];
                }
                HOperatorSet.Union1(ho_SelectedRegions, out RegionToDisp);
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("面积1");
                hv_result = hv_result.TupleConcat(hv_b1.D);
                hv_result = hv_result.TupleConcat("面积2");
                hv_result = hv_result.TupleConcat(hv_b2.D);
                hv_result = hv_result.TupleConcat("面积3");
                hv_result = hv_result.TupleConcat(hv_b3.D);
                hv_result = hv_result.TupleConcat("面积4");
                hv_result = hv_result.TupleConcat(hv_b4.D);
                //hv_result = hv_result.TupleConcat("面积5");
                //hv_result = hv_result.TupleConcat(hv_b5.D);
                result = hv_result.Clone();
                ho_Rectangle.Dispose();
                ho_Region1.Dispose();
                ho_ImageMean.Dispose();
                ho_RegionDynThresh.Dispose();
                ho_RegionTrans.Dispose();
                ho_Circle.Dispose();
                ho_Circle1.Dispose();
                ho_RegionDifference.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_SelectedRegions.Dispose();
                ho_ObjectSelected.Dispose();
                algorithm.Region.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("面积1");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("面积2");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("面积3");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("面积4");
                hv_result = hv_result.TupleConcat(0);
                //hv_result = hv_result.TupleConcat("面积5");
                //hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();
                ho_Rectangle.Dispose();
                ho_Region1.Dispose();
                ho_ImageMean.Dispose();
                ho_RegionDynThresh.Dispose();
                ho_RegionTrans.Dispose();
                ho_Circle.Dispose();
                ho_Circle1.Dispose();
                ho_RegionDifference.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_SelectedRegions.Dispose();
                ho_ObjectSelected.Dispose();
                algorithm.Region.Dispose();

            }
            finally
            {
                ho_Rectangle.Dispose();
                ho_Region1.Dispose();
                ho_ImageMean.Dispose();
                ho_RegionDynThresh.Dispose();
                ho_RegionTrans.Dispose();
                ho_Circle.Dispose();
                ho_Circle1.Dispose();
                ho_RegionDifference.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_SelectedRegions.Dispose();
                ho_ObjectSelected.Dispose();
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

