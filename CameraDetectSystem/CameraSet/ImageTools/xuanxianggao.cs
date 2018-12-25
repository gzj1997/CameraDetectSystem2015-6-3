using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
namespace CameraDetectSystem
{
    [Serializable]
    class xuanxianggao : ImageTools
    {
        #region ROI
        [NonSerialized]
        private HTuple Row1m = new HTuple();
        [NonSerialized]
        private HTuple Col1m = new HTuple();
        [NonSerialized]
        private HTuple Length1 = new HTuple();
        [NonSerialized]
        private HTuple Length2 = new HTuple();
        [NonSerialized]
        private HTuple Phi = new HTuple();

        public double DPhi { set; get; }
        public double DRow1m { set; get; }
        public double DCol1m { set; get; }
        public double DLength1 { set; get; }
        public double DLength2 { set; get; }

        #endregion
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public xuanxianggao()
        {
            //RegionToDisp = Image;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public xuanxianggao(HObject Image, Algorithm al)
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
            HTuple Row1m = null, Col1m = null, Length1 = null, Length2 = null, Phi = null;
            HObject ho_Rectangle;
            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.DrawRectangle2(this.LWindowHandle, out Row1m, out Col1m, out Phi, out Length1, out Length2);
            this.DRow1m = Row1m.D;
            this.DCol1m = Col1m.D;
            this.DPhi = Phi.D;
            this.DLength1 = Length1.D;
            this.DLength2 = Length2.D;

            //  HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            //  Dthv = thresholdValue.D;
            HOperatorSet.GenRectangle2(out ho_Rectangle, DRow1m, DCol1m, DPhi, DLength1, DLength2);
            ho_Rectangle.Dispose();//

        }
        // Main procedure 
        private void action()
        {
            // Local iconic variables 

            HObject ho_Rectangle;
            HObject ho_Region, ho_Circle;

            // Local control variables 

            HTuple hv_Row1 = null;
            HTuple hv_Column1 = null, hv_Radius = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            //HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {

                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle, DRow1m, DCol1m, DPhi, DLength1, DLength2);
                ho_Region.Dispose();
                HOperatorSet.Intersection(ho_Rectangle, this.algorithm.Region, out ho_Region);
                // ho_ImageMean.Dispose();
                //  HOperatorSet.MeanImage(ho_ImageReduced, out ho_ImageMean, 30, 30);
                // ho_RegionDynThresh.Dispose();
                //   HOperatorSet.DynThreshold(ho_ImageReduced, ho_ImageMean, out ho_RegionDynThresh,
                //       70, "dark");
                //ho_Region.Dispose();
                //HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, 0, 128);
                HOperatorSet.SmallestCircle(ho_Region, out hv_Row1, out hv_Column1, out hv_Radius);
                ho_Circle.Dispose();
                HOperatorSet.GenCircle(out ho_Circle, hv_Row1, hv_Column1, hv_Radius);
                HOperatorSet.Union1(ho_Region, out RegionToDisp);
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("直径");
                hv_result = hv_result.TupleConcat(hv_Radius.D * 2 * pixeldist);
                result = hv_result.Clone();
                ho_Rectangle.Dispose();
                //ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_Circle.Dispose();
                algorithm.Region.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("直径");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();
                ho_Rectangle.Dispose();
                //ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_Circle.Dispose();
                algorithm.Region.Dispose();

            }
            finally
            {
                ho_Rectangle.Dispose();
                //ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_Circle.Dispose();
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


