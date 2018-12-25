using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
    [Serializable]
    class luowenbiaomian1 : ImageTools
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
        public luowenbiaomian1()
        {
            //RegionToDisp = Image;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public luowenbiaomian1(HObject Image, Algorithm al)
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
      //}
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
            HOperatorSet.GenRectangle1(out ho_Rectangle, DRow1m, DCol1m, DRow2m, DCol2m);
            ho_Rectangle.Dispose();
        }

        private void action()
        {
            // Local iconic variables 

            HObject ho_Rectangle, ho_ImageReduced;
            HObject ho_Region, ho_Rectangle1, ho_Region1, ho_ConnectedRegions;
            HObject ho_RegionFillUp, ho_SelectedRegions, ho_SortedRegions;
            HObject ho_ObjectSelected = null, ho_ObjectSelected1;

            // Local control variables 

            HTuple hv_Row1 = null, hv_Column1 = null, hv_Row2 = null;
            HTuple hv_Column2 = null, hv_Row11 = null, hv_Column11 = null;
            HTuple hv_Row21 = null, hv_Column21 = null, hv_Mean = null;
            HTuple hv_Deviation = null, hv_Number = null, hv_a = null;
            HTuple hv_l1 = null, hv_l2 = null, hv_i = null, hv_Area = new HTuple();
            HTuple hv_Row = new HTuple(), hv_Column = new HTuple();
            HTuple hv_Row3 = new HTuple(), hv_Column3 = new HTuple();
            HTuple hv_Phi = new HTuple(), hv_Length1 = new HTuple();
            HTuple hv_Length2 = new HTuple(), hv_Indices = null, hv_cl0 = null;
            HTuple hv_cl1 = null, hv_cl2 = null, hv_cl3 = null, hv_cl4 = null;
            HTuple hv_cl5 = null, hv_cl6 = null, hv_cl7 = null, hv_cl8 = null;
            HTuple hv_cl9 = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SortedRegions);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected1);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {
                HOperatorSet.GenRectangle1(out ho_Rectangle, DRow1m, DCol1m, DRow2m, DCol2m);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Rectangle, out ho_ImageReduced);
                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, 128, 255);
                HOperatorSet.SmallestRectangle1(ho_Region, out hv_Row11, out hv_Column11, out hv_Row21,
                    out hv_Column21);
                ho_Rectangle1.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle1, hv_Row11, hv_Column11, hv_Row21,
                    hv_Column21);
                HOperatorSet.Intensity(ho_Rectangle1, ho_ImageReduced, out hv_Mean, out hv_Deviation);
                ho_Region1.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region1, hv_Mean, 255);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Region1, out ho_ConnectedRegions);
                ho_RegionFillUp.Dispose();
                HOperatorSet.FillUp(ho_ConnectedRegions, out ho_RegionFillUp);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_RegionFillUp, out ho_SelectedRegions, "area", "and",
                    200, 99999);
                ho_SortedRegions.Dispose();
                HOperatorSet.SortRegion(ho_SelectedRegions, out ho_SortedRegions, "upper_left",
                    "true", "column");
                HOperatorSet.CountObj(ho_SortedRegions, out hv_Number);
                hv_a = new HTuple();
                hv_l1 = new HTuple();
                hv_l2 = new HTuple();
                HTuple end_val20 = hv_Number - 2;
                HTuple step_val20 = 1;
                for (hv_i = 1; hv_i.Continue(end_val20, step_val20); hv_i = hv_i.TupleAdd(step_val20))
                {
                    ho_ObjectSelected.Dispose();
                    HOperatorSet.SelectObj(ho_SortedRegions, out ho_ObjectSelected, hv_i + 1);
                    HOperatorSet.AreaCenter(ho_ObjectSelected, out hv_Area, out hv_Row, out hv_Column);
                    HOperatorSet.SmallestRectangle2(ho_ObjectSelected, out hv_Row3, out hv_Column3,
                        out hv_Phi, out hv_Length1, out hv_Length2);
                    hv_a = hv_a.TupleConcat(hv_Area);
                    hv_l1 = hv_l1.TupleConcat(hv_Length1);
                    hv_l2 = hv_l2.TupleConcat(hv_Length2);
                }
                HOperatorSet.TupleFind(hv_a, hv_a.TupleMin(), out hv_Indices);
                ho_ObjectSelected1.Dispose();
                HOperatorSet.SelectObj(ho_SortedRegions, out ho_ObjectSelected1, hv_Indices + 2);
                hv_cl0 = hv_Number - 2;
                hv_cl1 = hv_a.TupleMean();
                hv_cl2 = (hv_a.TupleMin()) / (hv_a.TupleMean());
                hv_cl3 = (hv_a.TupleMax()) / (hv_a.TupleMean());
                hv_cl4 = hv_l1.TupleMean();
                hv_cl5 = (hv_l1.TupleMin()) / (hv_l1.TupleMean());
                hv_cl6 = (hv_l1.TupleMax()) / (hv_l1.TupleMean());
                hv_cl7 = hv_l2.TupleMean();
                hv_cl8 = (hv_l2.TupleMin()) / (hv_l2.TupleMean());
                hv_cl9 = (hv_l2.TupleMax()) / (hv_l2.TupleMean());
                HOperatorSet.Union1(ho_ObjectSelected1, out RegionToDisp);

                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("数量");
                hv_result = hv_result.TupleConcat(hv_cl0.D);
                hv_result = hv_result.TupleConcat("面积平均");
                hv_result = hv_result.TupleConcat(hv_cl1.D * pixeldist);
                hv_result = hv_result.TupleConcat("面积最小比值");
                hv_result = hv_result.TupleConcat(hv_cl2.D);
                hv_result = hv_result.TupleConcat("面积最大比值");
                hv_result = hv_result.TupleConcat(hv_cl3.D);
                hv_result = hv_result.TupleConcat("长平均");
                hv_result = hv_result.TupleConcat(hv_cl4.D * pixeldist);
                hv_result = hv_result.TupleConcat("长最小比值");
                hv_result = hv_result.TupleConcat(hv_cl5.D);
                hv_result = hv_result.TupleConcat("长最大比值");
                hv_result = hv_result.TupleConcat(hv_cl6.D);
                hv_result = hv_result.TupleConcat("宽平均");
                hv_result = hv_result.TupleConcat(hv_cl7.D * pixeldist);
                hv_result = hv_result.TupleConcat("宽最小比值");
                hv_result = hv_result.TupleConcat(hv_cl8.D);
                hv_result = hv_result.TupleConcat("宽最大比值");
                hv_result = hv_result.TupleConcat(hv_cl9.D);
                result = hv_result.Clone();
                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_Rectangle1.Dispose();
                ho_Region1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_SelectedRegions.Dispose();
                ho_SortedRegions.Dispose();
                ho_ObjectSelected.Dispose();
                ho_ObjectSelected1.Dispose();
                algorithm.Region.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("数量");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("面积平均");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("面积最小比值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("面积最大比值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("长平均");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("长最小比值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("长最大比值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("宽平均");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("宽最小比值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("宽最大比值");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();
                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_Rectangle1.Dispose();
                ho_Region1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_SelectedRegions.Dispose();
                ho_SortedRegions.Dispose();
                ho_ObjectSelected.Dispose();
                ho_ObjectSelected1.Dispose();
                algorithm.Region.Dispose();

            }
            finally
            {
                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_Rectangle1.Dispose();
                ho_Region1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_SelectedRegions.Dispose();
                ho_SortedRegions.Dispose();
                ho_ObjectSelected.Dispose();
                ho_ObjectSelected1.Dispose();
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


