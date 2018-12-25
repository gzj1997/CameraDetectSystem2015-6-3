using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
namespace CameraDetectSystem
{
    [Serializable]
    class xbcc : ImageTools
    {
        #region ROI
        //[NonSerialized]
        //private HTuple Row1m = new HTuple();
        //[NonSerialized]
        //private HTuple Col1m = new HTuple();
        //[NonSerialized]
        //private HTuple Row2m = new HTuple();
        //[NonSerialized]
        //private HTuple Col2m = new HTuple();
        [NonSerialized]
        private HTuple thresholdValue = new HTuple();
        [NonSerialized]
        private HTuple mianjisx = new HTuple();
        [NonSerialized]
        private HTuple mianjixx = new HTuple();
        public double Dthv { set; get; }
        //public double DRow1m { set; get; }
        //public double DCol1m { set; get; }
        //public double DRow2m { set; get; }
        //public double DCol2m { set; get; }
        public double mjsx { set; get; }
        public double mjxx { set; get; }
        #endregion
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public xbcc()
        {
            //RegionToDisp = Image;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public xbcc(HObject Image, Algorithm al)
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
            //HTuple Row1m = null, Col1m = null, Row2m = null, Col2m = null;
            //HObject ho_Rectangle;
            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            //HOperatorSet.GenEmptyObj(out ho_Rectangle);
            //HOperatorSet.DrawRectangle1(this.LWindowHandle, out Row1m, out Col1m, out Row2m, out Col2m);
            //this.DRow1m = Row1m.D;
            //this.DCol1m = Col1m.D;
            //this.DRow2m = Row2m.D;
            //this.DCol2m = Col2m.D;
            HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            Dthv = thresholdValue.D;
            HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\mianjixx", out mianjixx);
            mjxx = mianjixx.D;
            HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\mianjisx", out mianjisx);
            mjsx = mianjisx.D;
            //HOperatorSet.GenRectangle1(out ho_Rectangle, DRow1m, DCol1m, DRow2m, DCol2m);
            //ho_Rectangle.Dispose();//

        }
        // Main procedure 
        private void action()
        {

            // Local iconic variables 

            HObject ho_Region, ho_ConnectedRegions;
            HObject ho_SelectedRegions, ho_RegionFillUp, ho_ImageReduced;
            HObject ho_Region1, ho_SelectedRegions1, ho_RegionTrans;
            HObject ho_Rectangle, ho_Rectangle1, ho_RegionUnion;

            // Local control variables 

            HTuple hv_Row1 = null, hv_Column1 = null, hv_Phi = null;
            HTuple hv_Length1 = null, hv_Length2 = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {
                ho_Region.Dispose();
                HOperatorSet.Threshold(Image, out ho_Region, 200, 255);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShapeStd(ho_ConnectedRegions, out ho_SelectedRegions, "max_area",
                    70);
                ho_RegionFillUp.Dispose();
                HOperatorSet.FillUp(ho_SelectedRegions, out ho_RegionFillUp);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_RegionFillUp, out ho_ImageReduced);
                ho_Region1.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region1, 0, Dthv);
                ho_SelectedRegions1.Dispose();
                HOperatorSet.SelectShape(ho_Region1, out ho_SelectedRegions1, "area", "and",
                    mjxx, mjsx);
                ho_RegionTrans.Dispose();
                HOperatorSet.ShapeTrans(ho_SelectedRegions1, out ho_RegionTrans, "convex");
                HOperatorSet.SmallestRectangle2(ho_RegionTrans, out hv_Row1, out hv_Column1,
                    out hv_Phi, out hv_Length1, out hv_Length2);
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle, hv_Row1, hv_Column1, hv_Phi, hv_Length1,
                    0.1);
                ho_Rectangle1.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle1, hv_Row1, hv_Column1, hv_Phi, 0.1,
                    hv_Length2);
                ho_RegionUnion.Dispose();
                HOperatorSet.Union2(ho_Rectangle, ho_Rectangle1, out ho_RegionUnion);
                ///
                HOperatorSet.Union1(ho_RegionUnion, out RegionToDisp);
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("长度1");
                hv_result = hv_result.TupleConcat(hv_Length1.D * 2 * pixeldist);
                hv_result = hv_result.TupleConcat("长度2");
                hv_result = hv_result.TupleConcat(hv_Length2.D * 2 * pixeldist);

                result = hv_result.Clone();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionTrans.Dispose();
                ho_Rectangle.Dispose();
                ho_Rectangle1.Dispose();
                ho_RegionUnion.Dispose();
                algorithm.Region.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("长度1");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("长度2");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionTrans.Dispose();
                ho_Rectangle.Dispose();
                ho_Rectangle1.Dispose();
                ho_RegionUnion.Dispose();
                algorithm.Region.Dispose();

            }
            finally
            {
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionTrans.Dispose();
                ho_Rectangle.Dispose();
                ho_Rectangle1.Dispose();
                ho_RegionUnion.Dispose();
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


