using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
namespace CameraDetectSystem
{
    [Serializable]
    class chilunbiaomian3 : ImageTools
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
        public chilunbiaomian3()
        {
            //RegionToDisp = Image;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public chilunbiaomian3(HObject Image, Algorithm al)
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
            HObject ho_Region1;

            // Local control variables 

            HTuple hv_Row = null, hv_Column = null, hv_Radius = null;
            HTuple hv_Row1 = null, hv_Column1 = null, hv_Radius1 = null;
            HTuple hv_Area = null, hv_Row2 = null, hv_Column2 = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_Circle1);
            HOperatorSet.GenEmptyObj(out ho_Circle2);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {

                ho_Circle.Dispose();
                HOperatorSet.GenCircle(out ho_Circle, DRow1m, DCol1m, DRadiusm);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Circle, out ho_ImageReduced);
                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, 150, 255);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                    "and", 15000, 99999000);
                HOperatorSet.SmallestCircle(ho_SelectedRegions, out hv_Row1, out hv_Column1,
                    out hv_Radius1);
                ho_Circle1.Dispose();
                HOperatorSet.GenCircle(out ho_Circle1, hv_Row1, hv_Column1, hv_Radius1 * 0.65);
                ho_Circle2.Dispose();
                HOperatorSet.GenCircle(out ho_Circle2, hv_Row1, hv_Column1, hv_Radius1 * 0.35);
                ho_RegionDifference.Dispose();
                HOperatorSet.Difference(ho_Circle1, ho_Circle2, out ho_RegionDifference);
                //*intersection(SelectedRegions, RegionDifference, RegionIntersection)
                ho_ImageReduced1.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_RegionDifference, out ho_ImageReduced1
                    );
                ho_Region1.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced1, out ho_Region1, 0, 150);
                //*connection(Region1, ConnectedRegions1)
                HOperatorSet.AreaCenter(ho_Region1, out hv_Area, out hv_Row2, out hv_Column2);
                HOperatorSet.Union1(ho_Region1, out RegionToDisp);
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("面积");
                hv_result = hv_result.TupleConcat(hv_Area.D);
                result = hv_result.Clone();
                ho_Circle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_Circle1.Dispose();
                ho_Circle2.Dispose();
                ho_RegionDifference.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region1.Dispose();
                algorithm.Region.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("面积");
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
                ho_ImageReduced1.Dispose();
                ho_Region1.Dispose();
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
                ho_ImageReduced1.Dispose();
                ho_Region1.Dispose();
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


