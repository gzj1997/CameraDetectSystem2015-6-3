using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
namespace CameraDetectSystem
{
    [Serializable]
    class chilunbiaomian : ImageTools
    {
        #region ROI
        [NonSerialized]
        private HTuple Row1m = new HTuple();
        [NonSerialized]
        private HTuple Col1m = new HTuple();
        [NonSerialized]
        private HTuple Radiusm = new HTuple();
        //[NonSerialized]
        //private HTuple Row2m = new HTuple();
        //[NonSerialized]
        //private HTuple Col2m = new HTuple();
        //[NonSerialized]
        //private HTuple thresholdValue = new HTuple();

        //public double Dthv { set; get; }
        public double DRow1m { set; get; }
        public double DCol1m { set; get; }

        public double DRadiusm { set; get; }
        #endregion
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public chilunbiaomian()
        {
            //RegionToDisp = Image;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public chilunbiaomian(HObject Image, Algorithm al)
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
            HTuple Row1m = null, Col1m = null, Radiusm = null;
            HObject ho_Circle;
            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.DrawCircle(this.LWindowHandle, out Row1m, out Col1m, out Radiusm);
            this.DRow1m = Row1m.D;
            this.DCol1m = Col1m.D;
            this.DRadiusm = Radiusm.D;
            //this.DRow2m = Row2m.D;
            //this.DCol2m = Col2m.D;
            //  HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            //  Dthv = thresholdValue.D;
            HOperatorSet.GenCircle(out ho_Circle, DRow1m, DCol1m, DRadiusm);
            ho_Circle.Dispose();//

        }
        // Main procedure 
        private void action()
        {
            // Local iconic variables 

            HObject ho_Circle, ho_ImageReduced;
            HObject ho_Region, ho_ConnectedRegions, ho_SelectedRegions;
            HObject ho_Circle1, ho_Circle2, ho_RegionDifference, ho_ImageReduced1;
            HObject ho_Region1, ho_ConnectedRegions1, ho_SelectedRegions1;
            HObject ho_RegionIntersection;

            // Local control variables 

            HTuple hv_Row = null, hv_Column = null, hv_Radius = null;
            HTuple hv_Row1 = null, hv_Column1 = null, hv_Radius1 = null;
            HTuple hv_Area = null, hv_Row2 = null, hv_Column2 = null;
            HTuple hv_Number = null, hv_Area1 = null, hv_Row3 = null;
            HTuple hv_Column3 = null, hv_Sorted = null, hv_a = null;
            HTuple hv_b = null, hv_c = null, hv_d = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_Circle1);
            HOperatorSet.GenEmptyObj(out ho_Circle2);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {

                ho_Circle.Dispose();
                HOperatorSet.GenCircle(out ho_Circle, DRow1m, DCol1m, DRadiusm);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Circle, out ho_ImageReduced);
                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, 200, 255);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                    "and", 15000, 99999000);
                HOperatorSet.SmallestCircle(ho_SelectedRegions, out hv_Row1, out hv_Column1,
                    out hv_Radius1);
                ho_Circle1.Dispose();
                HOperatorSet.GenCircle(out ho_Circle1, hv_Row1, hv_Column1, hv_Radius1);
                ho_Circle2.Dispose();
                HOperatorSet.GenCircle(out ho_Circle2, hv_Row1, hv_Column1, hv_Radius1 * 0.9);
                ho_RegionDifference.Dispose();
                HOperatorSet.Difference(ho_Circle1, ho_Circle2, out ho_RegionDifference);
                ho_RegionIntersection.Dispose();
                HOperatorSet.Intersection(ho_SelectedRegions, ho_RegionDifference, out ho_RegionIntersection
                    );
                ho_ConnectedRegions1.Dispose();
                HOperatorSet.Connection(ho_RegionIntersection, out ho_ConnectedRegions1);
                HOperatorSet.AreaCenter(ho_ConnectedRegions1, out hv_Area, out hv_Row2, out hv_Column2);
                ho_SelectedRegions1.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions1, out ho_SelectedRegions1, "area",
                    "and", (hv_Area.TupleMax()) / 3, (hv_Area.TupleMax()) + 1);
                HOperatorSet.CountObj(ho_SelectedRegions1, out hv_Number);
                HOperatorSet.AreaCenter(ho_SelectedRegions1, out hv_Area1, out hv_Row3, out hv_Column3);
                HOperatorSet.TupleSort(hv_Area1, out hv_Sorted);
                hv_a = hv_Sorted[0];
                hv_b = hv_Sorted[1];
                hv_c = hv_Sorted[2];
                hv_d = hv_Sorted[3];
                HOperatorSet.Union1(ho_SelectedRegions1, out RegionToDisp);
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("数量");
                hv_result = hv_result.TupleConcat(hv_Number.D);
                hv_result = hv_result.TupleConcat("面积1");
                hv_result = hv_result.TupleConcat(hv_a.D);
                hv_result = hv_result.TupleConcat("面积2");
                hv_result = hv_result.TupleConcat(hv_b.D);
                hv_result = hv_result.TupleConcat("面积3");
                hv_result = hv_result.TupleConcat(hv_c.D);
                hv_result = hv_result.TupleConcat("面积4");
                hv_result = hv_result.TupleConcat(hv_d.D);
                result = hv_result.Clone();
                ho_Circle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_Circle1.Dispose();
                ho_Circle2.Dispose();
                ho_RegionDifference.Dispose();
                ho_RegionIntersection.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                algorithm.Region.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("数量");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("面积1");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("面积2");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("面积3");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("面积4");
                hv_result = hv_result.TupleConcat(0);

                result = hv_result.Clone();
                ho_Circle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_Circle1.Dispose();
                ho_Circle2.Dispose();
                ho_RegionDifference.Dispose();
                ho_RegionIntersection.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                algorithm.Region.Dispose();

            }
            finally
            {
                ho_Circle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_Circle1.Dispose();
                ho_Circle2.Dispose();
                ho_RegionDifference.Dispose();
                ho_RegionIntersection.Dispose();
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


