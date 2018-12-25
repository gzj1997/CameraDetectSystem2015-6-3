using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
namespace CameraDetectSystem
{
    [Serializable]
    class queliao : ImageTools
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
        public queliao()
        {
            //RegionToDisp = Image;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public queliao(HObject Image, Algorithm al)
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
            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_Rectangle, ho_ImageReduced;
            HObject ho_Region, ho_ConnectedRegions, ho_Circle, ho_Circle1;
            HObject ho_RegionDifference, ho_ImageReduced1, ho_Region1;
            HObject ho_ConnectedRegions1, ho_SelectedRegions, ho_Circle2;
            HObject ho_Circle3, ho_RegionDifference1, ho_ImageReduced2;
            HObject ho_Region2, ho_ConnectedRegions2;

            // Local control variables 

            HTuple hv_Row1 = null, hv_Column1 = null, hv_Row2 = null;
            HTuple hv_Column2 = null, hv_Row = null, hv_Column = null;
            HTuple hv_Radius = null, hv_Area = null, hv_Row3 = null;
            HTuple hv_Column3 = null, hv_Area1 = null, hv_Row4 = null;
            HTuple hv_Column4 = null, hv_Sorted = null, hv_a = new HTuple();
            HTuple hv_b = new HTuple(), hv_c = new HTuple(), hv_d = new HTuple();
            HTuple hv_e = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_Circle1);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_Circle2);
            HOperatorSet.GenEmptyObj(out ho_Circle3);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference1);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced2);
            HOperatorSet.GenEmptyObj(out ho_Region2);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions2);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {

                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle, DRow1m, DCol1m, DRow2m, DCol2m);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Rectangle, out ho_ImageReduced);
                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, 0, 100);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
                HOperatorSet.SmallestCircle(ho_ConnectedRegions, out hv_Row, out hv_Column, out hv_Radius);
                ho_Circle.Dispose();
                HOperatorSet.GenCircle(out ho_Circle, hv_Row, hv_Column, hv_Radius);
                ho_Circle1.Dispose();
                HOperatorSet.GenCircle(out ho_Circle1, hv_Row, hv_Column, hv_Radius * 0.91);
                ho_RegionDifference.Dispose();
                HOperatorSet.Difference(ho_Circle, ho_Circle1, out ho_RegionDifference);
                ho_ImageReduced1.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_RegionDifference, out ho_ImageReduced1
                    );
                ho_Region1.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced1, out ho_Region1, 0, 100);
                ho_ConnectedRegions1.Dispose();
                HOperatorSet.Connection(ho_Region1, out ho_ConnectedRegions1);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions1, out ho_SelectedRegions, "area",
                    "and", 150, 99999000);
                HOperatorSet.AreaCenter(ho_SelectedRegions, out hv_Area, out hv_Row3, out hv_Column3);
                ho_Circle2.Dispose();
                HOperatorSet.GenCircle(out ho_Circle2, hv_Row, hv_Column, hv_Radius * 0.77);
                ho_Circle3.Dispose();
                HOperatorSet.GenCircle(out ho_Circle3, hv_Row, hv_Column, hv_Radius * 0.84);
                ho_RegionDifference1.Dispose();
                HOperatorSet.Difference(ho_Circle3, ho_Circle2, out ho_RegionDifference1);
                ho_ImageReduced2.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_RegionDifference1, out ho_ImageReduced2
                    );
                ho_Region2.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced2, out ho_Region2, 0, 100);
                ho_ConnectedRegions2.Dispose();
                HOperatorSet.Connection(ho_Region2, out ho_ConnectedRegions2);
                HOperatorSet.AreaCenter(ho_ConnectedRegions2, out hv_Area1, out hv_Row4, out hv_Column4);
                HOperatorSet.TupleSort(hv_Area, out hv_Sorted);
                if ((int)(new HTuple((new HTuple(hv_Area.TupleLength())).TupleGreater(3))) != 0)
                {
                    hv_a = hv_Sorted[(new HTuple(hv_Area.TupleLength())) - 1];
                    hv_b = hv_Sorted[(new HTuple(hv_Area.TupleLength())) - 2];
                    hv_c = hv_Sorted[(new HTuple(hv_Area.TupleLength())) - 3];
                    hv_d = hv_Sorted[(new HTuple(hv_Area.TupleLength())) - 4];
                }
                hv_e = hv_Area1.Clone();
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.Union2(ho_ConnectedRegions1, ho_ConnectedRegions2, out ExpTmpOutVar_0
                        );
                    ho_ConnectedRegions1.Dispose();
                    ho_ConnectedRegions1 = ExpTmpOutVar_0;
                }
                HOperatorSet.Union1(ho_ConnectedRegions1, out RegionToDisp);
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("面积1");
                hv_result = hv_result.TupleConcat(hv_a.D);
                hv_result = hv_result.TupleConcat("面积2");
                hv_result = hv_result.TupleConcat(hv_b.D);
                hv_result = hv_result.TupleConcat("面积3");
                hv_result = hv_result.TupleConcat(hv_c.D);
                hv_result = hv_result.TupleConcat("面积4");
                hv_result = hv_result.TupleConcat(hv_d.D);
                hv_result = hv_result.TupleConcat("面积5");
                hv_result = hv_result.TupleConcat(hv_e.D);
                result = hv_result.Clone();
                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_Circle.Dispose();
                ho_Circle1.Dispose();
                ho_RegionDifference.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions.Dispose();
                ho_Circle2.Dispose();
                ho_Circle3.Dispose();
                ho_RegionDifference1.Dispose();
                ho_ImageReduced2.Dispose();
                ho_Region2.Dispose();
                ho_ConnectedRegions2.Dispose();
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
                hv_result = hv_result.TupleConcat("面积5");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();
                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_Circle.Dispose();
                ho_Circle1.Dispose();
                ho_RegionDifference.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions.Dispose();
                ho_Circle2.Dispose();
                ho_Circle3.Dispose();
                ho_RegionDifference1.Dispose();
                ho_ImageReduced2.Dispose();
                ho_Region2.Dispose();
                ho_ConnectedRegions2.Dispose();
                algorithm.Region.Dispose();

            }
            finally
            {
                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_Circle.Dispose();
                ho_Circle1.Dispose();
                ho_RegionDifference.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions.Dispose();
                ho_Circle2.Dispose();
                ho_Circle3.Dispose();
                ho_RegionDifference1.Dispose();
                ho_ImageReduced2.Dispose();
                ho_Region2.Dispose();
                ho_ConnectedRegions2.Dispose();
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

