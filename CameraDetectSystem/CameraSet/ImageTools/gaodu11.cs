using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
    [Serializable]
    class gaodu11 : ImageTools
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

        [NonSerialized]
        private HTuple Row1n = new HTuple();
        [NonSerialized]
        private HTuple Col1n = new HTuple();
        [NonSerialized]
        private HTuple Row2n = new HTuple();
        [NonSerialized]
        private HTuple Col2n = new HTuple();
        [NonSerialized]
        private HTuple thresholdValue = new HTuple();

        public double Dthv { set; get; }
        public double DRow1m { set; get; }
        public double DCol1m { set; get; }
        public double DRow2m { set; get; }
        public double DCol2m { set; get; }


        public double DRow1n { set; get; }
        public double DCol1n { set; get; }
        public double DRow2n { set; get; }
        public double DCol2n { set; get; }

        #endregion
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public gaodu11()
        {
            //RegionToDisp = Image;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public gaodu11(HObject Image, Algorithm al)
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
        public override void draw()
        {

            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            HOperatorSet.DrawRectangle1(this.LWindowHandle, out Row1m, out Col1m, out Row2m, out Col2m);
            this.DRow1m = Row1m.D;
            this.DCol1m = Col1m.D;
            this.DRow2m = Row2m.D;
            this.DCol2m = Col2m.D;
            HOperatorSet.DrawRectangle1(this.LWindowHandle, out Row1n, out Col1n, out Row2n, out Col2n);
            this.DRow1n = Row1n.D;
            this.DCol1n = Col1n.D;
            this.DRow2n = Row2n.D;
            this.DCol2n = Col2n.D;


            HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            Dthv = thresholdValue.D;

        }

        private void action()
        {
            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_Rectangle, ho_Rectangle1,ho_UnionContours1;
            HObject ho_Region, ho_ContoursL = null, ho_Contour1 = null;
            HObject ho_ContoursSplit2 = null, ho_RegionLines2 = null, ho_Contour = null;
            HObject ho_ImageReduced1 = null, ho_Edges1 = null, ho_ContoursSplit1 = null;
            HObject ho_UnionContours = null, ho_RegionLines1 = null, ho_Contour2 = null;

            // Local control variables 

            HTuple hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(), hv_shapeParam = new HTuple();
            HTuple hv_MetrologyHandleL = new HTuple(), hv_Index = new HTuple();
            HTuple hv_RowL = new HTuple(), hv_ColumnL = new HTuple();
            HTuple hv_RowBegin2 = new HTuple(), hv_ColBegin2 = new HTuple();
            HTuple hv_RowEnd2 = new HTuple(), hv_ColEnd2 = new HTuple();
            HTuple hv_Nr2 = new HTuple(), hv_Nc2 = new HTuple(), hv_Dist2 = new HTuple();
            HTuple hv_dis2 = new HTuple(), hv_Indices2 = new HTuple();
            HTuple hv_R = new HTuple(), hv_C = new HTuple(), hv_RowBegin1 = new HTuple();
            HTuple hv_ColBegin1 = new HTuple(), hv_RowEnd1 = new HTuple();
            HTuple hv_ColEnd1 = new HTuple(), hv_Nr1 = new HTuple();
            HTuple hv_Nc1 = new HTuple(), hv_Dist1 = new HTuple();
            HTuple hv_dis1 = new HTuple(), hv_Indices1 = new HTuple();
            HTuple hv_R1 = new HTuple(), hv_C1 = new HTuple(), hv_DistanceMin1 = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ContoursL);
            HOperatorSet.GenEmptyObj(out ho_UnionContours1);
            HOperatorSet.GenEmptyObj(out ho_Contour1);
            HOperatorSet.GenEmptyObj(out ho_ContoursSplit2);
            HOperatorSet.GenEmptyObj(out ho_RegionLines2);
            HOperatorSet.GenEmptyObj(out ho_Contour);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
            HOperatorSet.GenEmptyObj(out ho_Edges1);
            HOperatorSet.GenEmptyObj(out ho_ContoursSplit1);
            HOperatorSet.GenEmptyObj(out ho_UnionContours);
            HOperatorSet.GenEmptyObj(out ho_Contour2);
            HOperatorSet.GenEmptyObj(out ho_RegionLines1);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {
                HOperatorSet.GetImageSize(Image, out hv_Width, out hv_Height);
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle, DRow1m, DCol1m, DRow2m, DCol2m);
                ho_Rectangle1.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle1, DRow1n, DCol1n, DRow2n, DCol2n);

                //*
                //找左侧点
                hv_shapeParam = new HTuple();
                hv_shapeParam = hv_shapeParam.TupleConcat(DRow1m);
                hv_shapeParam = hv_shapeParam.TupleConcat((DCol1m + DCol2m) / 2);
                hv_shapeParam = hv_shapeParam.TupleConcat(DRow2m);
                hv_shapeParam = hv_shapeParam.TupleConcat((DCol1m + DCol2m) / 2);

                HOperatorSet.CreateMetrologyModel(out hv_MetrologyHandleL);
                HOperatorSet.SetMetrologyModelImageSize(hv_MetrologyHandleL, hv_Width, hv_Height);
                HOperatorSet.AddMetrologyObjectGeneric(hv_MetrologyHandleL, "line", hv_shapeParam,
                    (DCol2m - DCol1m) / 2, 1, 1, 50, new HTuple(), new HTuple(), out hv_Index);
                //设置参数
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandleL, "all", "measure_transition",
                    "positive");
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandleL, "all", "num_measures",
                    100);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandleL, "all", "num_instances",
                    1);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandleL, "all", "measure_select",
                    "last");

                //开始找边缘
                HOperatorSet.ApplyMetrologyModel(Image, hv_MetrologyHandleL);
                ho_ContoursL.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_ContoursL, hv_MetrologyHandleL,
                    "all", "all", out hv_RowL, out hv_ColumnL);
                //gen_cross_contour_xld (CrossL, RowL, ColumnL, 6, 0)
                ///释放测量句柄
                HOperatorSet.ClearMetrologyModel(hv_MetrologyHandleL);
                ho_Contour1.Dispose();
                HOperatorSet.GenContourPolygonXld(out ho_Contour1, hv_RowL, hv_ColumnL);
                ho_ContoursSplit2.Dispose();
                HOperatorSet.SegmentContoursXld(ho_Contour1, out ho_ContoursSplit2, "lines_circles",
                    5, 4, 2);
                ho_UnionContours1.Dispose();
                HOperatorSet.UnionCollinearContoursXld(ho_ContoursSplit2, out ho_UnionContours1,
                    10000, 10000, 2, 0.1, "attr_keep");

                HOperatorSet.FitLineContourXld(ho_UnionContours1, "tukey", -1, 0, 5, 2, out hv_RowBegin2,
                    out hv_ColBegin2, out hv_RowEnd2, out hv_ColEnd2, out hv_Nr2, out hv_Nc2,
                    out hv_Dist2);
                hv_dis2 = ((((hv_RowBegin2 - hv_RowEnd2) * (hv_RowBegin2 - hv_RowEnd2)) + ((hv_ColBegin2 - hv_ColEnd2) * (hv_ColBegin2 - hv_ColEnd2)))).TupleSqrt()
                    ;
                HOperatorSet.TupleFind(hv_dis2, hv_dis2.TupleMax(), out hv_Indices2);
                ho_RegionLines2.Dispose();
                HOperatorSet.GenRegionLine(out ho_RegionLines2, hv_RowBegin2.TupleSelect(hv_Indices2),
                    hv_ColBegin2.TupleSelect(hv_Indices2), hv_RowEnd2.TupleSelect(hv_Indices2),
                    hv_ColEnd2.TupleSelect(hv_Indices2));

                hv_R = new HTuple();
                hv_R = hv_R.TupleConcat(hv_RowBegin2.TupleSelect(
                    hv_Indices2));
                hv_R = hv_R.TupleConcat(hv_RowEnd2.TupleSelect(hv_Indices2));
                hv_C = new HTuple();
                hv_C = hv_C.TupleConcat(hv_ColBegin2.TupleSelect(
                    hv_Indices2));
                hv_C = hv_C.TupleConcat(hv_ColEnd2.TupleSelect(hv_Indices2));
                ho_Contour.Dispose();
                HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_R, hv_C);
                //2
                ho_ImageReduced1.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Rectangle1, out ho_ImageReduced1);
                ho_Edges1.Dispose();
                HOperatorSet.EdgesSubPix(ho_ImageReduced1, out ho_Edges1, "canny", 1, 20, 40);
                ho_ContoursSplit1.Dispose();
                HOperatorSet.SegmentContoursXld(ho_Edges1, out ho_ContoursSplit1, "lines_circles",
                    5, 4, 2);
                ho_UnionContours.Dispose();
                HOperatorSet.UnionCollinearContoursXld(ho_ContoursSplit1, out ho_UnionContours,
                    10000, 10000, 2, 0.1, "attr_keep");
                HOperatorSet.FitLineContourXld(ho_UnionContours, "tukey", -1, 0, 5, 2, out hv_RowBegin1,
                    out hv_ColBegin1, out hv_RowEnd1, out hv_ColEnd1, out hv_Nr1, out hv_Nc1,
                    out hv_Dist1);
                hv_dis1 = ((((hv_RowBegin1 - hv_RowEnd1) * (hv_RowBegin1 - hv_RowEnd1)) + ((hv_ColBegin1 - hv_ColEnd1) * (hv_ColBegin1 - hv_ColEnd1)))).TupleSqrt()
                    ;
                HOperatorSet.TupleFind(hv_dis1, hv_dis1.TupleMax(), out hv_Indices1);
                ho_RegionLines1.Dispose();
                HOperatorSet.GenRegionLine(out ho_RegionLines1, hv_RowBegin1.TupleSelect(hv_Indices1),
                    hv_ColBegin1.TupleSelect(hv_Indices1), hv_RowEnd1.TupleSelect(hv_Indices1),
                    hv_ColEnd1.TupleSelect(hv_Indices1));

                //distance_cc_min (UnionContours, UnionContours, 'fast_point_to_segment', DistanceMin)

                //distance_pl ((RowBegin2[Indices2]+RowEnd2[Indices2])/2, (ColBegin2[Indices2]+ColEnd2[Indices2]), RowBegin1[Indices1], ColBegin1[Indices1], RowEnd1[Indices1], ColEnd1[Indices1], Distance)
                hv_R1 = new HTuple();
                hv_R1 = hv_R1.TupleConcat(hv_RowBegin1.TupleSelect(
                    hv_Indices1));
                hv_R1 = hv_R1.TupleConcat(hv_RowEnd1.TupleSelect(hv_Indices1));
                hv_C1 = new HTuple();
                hv_C1 = hv_C1.TupleConcat(hv_ColBegin1.TupleSelect(
                    hv_Indices1));
                hv_C1 = hv_C1.TupleConcat(hv_ColEnd1.TupleSelect(hv_Indices1));
                ho_Contour2.Dispose();
                HOperatorSet.GenContourPolygonXld(out ho_Contour2, hv_R1, hv_C1);

                HOperatorSet.DistanceCcMin(ho_Contour, ho_Contour2, "fast_point_to_segment",
                    out hv_DistanceMin1);
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.Union2(ho_Region, ho_RegionLines1, out ExpTmpOutVar_0);
                    ho_Region.Dispose();
                    ho_Region = ExpTmpOutVar_0;
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.Union2(ho_Region, ho_RegionLines2, out ExpTmpOutVar_0);
                    ho_Region.Dispose();
                    ho_Region = ExpTmpOutVar_0;
                }
                HOperatorSet.Union1(ho_Region, out RegionToDisp);

                HTuple hv_result = GetHv_result();

                hv_result = hv_result.TupleConcat("高度");
                hv_result = hv_result.TupleConcat(hv_DistanceMin1.D * pixeldist);
                result = hv_result.Clone();
                ho_Rectangle.Dispose();
                ho_Rectangle1.Dispose();
                ho_Region.Dispose();
                ho_ContoursL.Dispose();
                ho_Contour1.Dispose();
                ho_ContoursSplit2.Dispose();
                ho_UnionContours1.Dispose();
                ho_RegionLines2.Dispose();
                ho_Contour.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Edges1.Dispose();
                ho_ContoursSplit1.Dispose();
                ho_UnionContours.Dispose();
                ho_RegionLines1.Dispose();
                ho_Contour2.Dispose();
                algorithm.Region.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("高度");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();
                ho_Rectangle.Dispose();
                ho_Rectangle1.Dispose();
                ho_Region.Dispose();
                ho_ContoursL.Dispose();
                ho_Contour1.Dispose();
                ho_ContoursSplit2.Dispose();
                ho_UnionContours1.Dispose();
                ho_RegionLines2.Dispose();
                ho_Contour.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Edges1.Dispose();
                ho_ContoursSplit1.Dispose();
                ho_UnionContours.Dispose();
                ho_RegionLines1.Dispose();
                ho_Contour2.Dispose();
                algorithm.Region.Dispose();

            }
            finally
            {
                ho_Rectangle.Dispose();
                ho_Rectangle1.Dispose();
                ho_Region.Dispose();
                ho_ContoursL.Dispose();
                ho_Contour1.Dispose();
                ho_ContoursSplit2.Dispose();
                ho_UnionContours1.Dispose();
                ho_RegionLines2.Dispose();
                ho_Contour.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Edges1.Dispose();
                ho_ContoursSplit1.Dispose();
                ho_UnionContours.Dispose();
                ho_RegionLines1.Dispose();
                ho_Contour2.Dispose();
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





