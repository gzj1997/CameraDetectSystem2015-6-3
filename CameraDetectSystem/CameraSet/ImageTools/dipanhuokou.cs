using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
    [Serializable]
    class dipanhuokou : ImageTools
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
        public dipanhuokou()
        {
            //RegionToDisp = Image;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public dipanhuokou(HObject Image, Algorithm al)
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
            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 




            HObject ho_Rectangle, ho_ImageReduced;
            HObject ho_Region, ho_ConnectedRegions1, ho_SelectedRegions1;
            HObject ho_ContoursL, ho_RegionLines, ho_RegionUnion1, ho_RegionLines1 = null;
            HObject ho_RegionLines2, ho_RegionUnion2, ho_ConnectedRegions;
            HObject ho_SelectedRegions, ho_RegionFillUp, ho_Contours;
            HObject ho_ContoursSplit, ho_SelectedContours, ho_UnionContours;
            HObject ho_RegionDifference;

            // Local control variables 

            HTuple hv_Width = null, hv_Height = null, hv_Row11 = null;
            HTuple hv_Column11 = null, hv_Row21 = null, hv_Column21 = null;
            HTuple hv_Mean = null, hv_Deviation = null, hv_Row12 = null;
            HTuple hv_Column12 = null, hv_Row22 = null, hv_Column22 = null;
            HTuple hv_shapeParam = null, hv_MetrologyHandleL = null;
            HTuple hv_Index = null, hv_RowL = null, hv_ColumnL = null;
            HTuple hv_cm = null, hv_Reducedc = null, hv_Reducedr = null;
            HTuple hv_j = null, hv_i = null, hv_a = null, hv_Area = null;
            HTuple hv_Row = null, hv_Column = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_ContoursL);
            HOperatorSet.GenEmptyObj(out ho_RegionLines);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion1);
            HOperatorSet.GenEmptyObj(out ho_RegionLines1);
            HOperatorSet.GenEmptyObj(out ho_RegionLines2);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion2);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_Contours);
            HOperatorSet.GenEmptyObj(out ho_ContoursSplit);
            HOperatorSet.GenEmptyObj(out ho_SelectedContours);
            HOperatorSet.GenEmptyObj(out ho_UnionContours);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle, DRow1m, DCol1m, DRow2m, DCol2m);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Rectangle, out ho_ImageReduced);
                ho_Region.Dispose();
                HOperatorSet.GetImageSize(Image, out hv_Width, out hv_Height);
                HOperatorSet.Intensity(ho_Rectangle, ho_ImageReduced, out hv_Mean, out hv_Deviation);

                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, hv_Mean + (hv_Deviation / 2),
                    255);
                ho_ConnectedRegions1.Dispose();
                HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions1);
                ho_SelectedRegions1.Dispose();
                HOperatorSet.SelectShapeStd(ho_ConnectedRegions1, out ho_SelectedRegions1, "max_area",
                    70);


                HOperatorSet.SmallestRectangle1(ho_SelectedRegions1, out hv_Row12, out hv_Column12,
                    out hv_Row22, out hv_Column22);

                //找左侧点
                hv_shapeParam = new HTuple();
                hv_shapeParam = hv_shapeParam.TupleConcat(hv_Row12);
                hv_shapeParam = hv_shapeParam.TupleConcat((hv_Column22 + hv_Column12) / 2);
                hv_shapeParam = hv_shapeParam.TupleConcat(hv_Row22);
                hv_shapeParam = hv_shapeParam.TupleConcat((hv_Column22 + hv_Column12) / 2);

                HOperatorSet.CreateMetrologyModel(out hv_MetrologyHandleL);
                HOperatorSet.SetMetrologyModelImageSize(hv_MetrologyHandleL, hv_Width, hv_Height);
                HOperatorSet.AddMetrologyObjectGeneric(hv_MetrologyHandleL, "line", hv_shapeParam,
                    ((hv_Column22 - hv_Column12) / 2) + 1, 1, 1, 30, new HTuple(), new HTuple(), out hv_Index);
                //设置参数
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandleL, "all", "measure_transition",
                    "negative");
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandleL, "all", "num_measures",
                    100);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandleL, "all", "num_instances",
                    1);
                HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandleL, "all", "measure_select",
                    "first");

                //开始找边缘
                HOperatorSet.ApplyMetrologyModel(ho_ImageReduced, hv_MetrologyHandleL);
                ho_ContoursL.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_ContoursL, hv_MetrologyHandleL,
                    "all", "all", out hv_RowL, out hv_ColumnL);
                //gen_cross_contour_xld (CrossL, RowL, ColumnL, 6, 0)
                // stop(); only in hdevelop
                HOperatorSet.ClearMetrologyModel(hv_MetrologyHandleL);
                ho_RegionLines.Dispose();
                HOperatorSet.GenRegionLine(out ho_RegionLines, hv_Row12, hv_Column12, hv_Row12 + 1,
                    hv_Column12);
                ho_RegionUnion1.Dispose();
                HOperatorSet.Union1(ho_RegionLines, out ho_RegionUnion1);
                hv_cm = (((hv_ColumnL.TupleSelect(0)) + (hv_ColumnL.TupleSelect((new HTuple(hv_RowL.TupleLength()
                    )) - 1))) / 2) + 1;
                hv_Reducedc = new HTuple();
                hv_Reducedr = new HTuple();
                for (hv_j = 0; (int)hv_j <= (int)((new HTuple(hv_RowL.TupleLength())) - 1); hv_j = (int)hv_j + 1)
                {
                    if ((int)(new HTuple(((hv_ColumnL.TupleSelect(hv_j))).TupleLessEqual(hv_cm))) != 0)
                    {
                        hv_Reducedc = hv_Reducedc.TupleConcat(hv_ColumnL.TupleSelect(hv_j));
                        hv_Reducedr = hv_Reducedr.TupleConcat(hv_RowL.TupleSelect(hv_j));
                    }
                }
                //gen_cross_contour_xld (Cross, Reducedr, Reducedc, 6, 0.785398)


                // stop(); only in hdevelop
                for (hv_i = 0; (int)hv_i <= (int)((new HTuple(hv_Reducedr.TupleLength())) - 2); hv_i = (int)hv_i + 1)
                {
                    ho_RegionLines1.Dispose();
                    HOperatorSet.GenRegionLine(out ho_RegionLines1, hv_Reducedr.TupleSelect(hv_i),
                        hv_Reducedc.TupleSelect(hv_i), hv_Reducedr.TupleSelect(hv_i + 1), hv_Reducedc.TupleSelect(
                        hv_i + 1));
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.Union2(ho_RegionUnion1, ho_RegionLines1, out ExpTmpOutVar_0);
                        ho_RegionUnion1.Dispose();
                        ho_RegionUnion1 = ExpTmpOutVar_0;
                    }
                }
                // stop(); only in hdevelop
                ho_RegionLines2.Dispose();
                HOperatorSet.GenRegionLine(out ho_RegionLines2, hv_RowL.TupleSelect(0), hv_ColumnL.TupleSelect(
                    0), hv_RowL.TupleSelect((new HTuple(hv_RowL.TupleLength())) - 1), hv_ColumnL.TupleSelect(
                    (new HTuple(hv_RowL.TupleLength())) - 1));
                ho_RegionUnion2.Dispose();
                HOperatorSet.Union2(ho_RegionUnion1, ho_RegionLines2, out ho_RegionUnion2);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_RegionUnion2, out ho_ConnectedRegions);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShapeStd(ho_ConnectedRegions, out ho_SelectedRegions, "max_area",
                    70);
                ho_RegionFillUp.Dispose();
                HOperatorSet.FillUp(ho_SelectedRegions, out ho_RegionFillUp);
                ho_Contours.Dispose();
                HOperatorSet.GenContourRegionXld(ho_RegionFillUp, out ho_Contours, "border");
                ho_ContoursSplit.Dispose();
                HOperatorSet.SegmentContoursXld(ho_Contours, out ho_ContoursSplit, "lines_circles",
                    5, 8, 3.5);
                ho_SelectedContours.Dispose();
                HOperatorSet.SelectContoursXld(ho_ContoursSplit, out ho_SelectedContours, "contour_length",
                    70, 2000, -0.5, 0.5);
                ho_UnionContours.Dispose();
                HOperatorSet.UnionAdjacentContoursXld(ho_SelectedContours, out ho_UnionContours,
                    10, 1, "attr_keep");
                ho_Region.Dispose();
                HOperatorSet.GenRegionContourXld(ho_UnionContours, out ho_Region, "filled");

                ho_RegionDifference.Dispose();
                HOperatorSet.Difference(ho_RegionFillUp, ho_Region, out ho_RegionDifference);
                hv_a = 0;
                HOperatorSet.AreaCenter(ho_RegionDifference, out hv_Area, out hv_Row, out hv_Column);
                if ((int)(new HTuple((new HTuple(hv_Area.TupleLength())).TupleGreater(0))) != 0)
                {
                    hv_a = hv_Area.TupleSum();
                    HOperatorSet.Union1(ho_RegionDifference, out RegionToDisp);
                }
                else
                {
                    hv_a = 0;
                }
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("缺陷面积");
                hv_result = hv_result.TupleConcat(hv_a.D);
                result = hv_result.Clone();

                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_ContoursL.Dispose();
                ho_RegionLines.Dispose();
                ho_RegionUnion1.Dispose();
                ho_RegionLines1.Dispose();
                ho_RegionLines2.Dispose();
                ho_RegionUnion2.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_Contours.Dispose();
                ho_ContoursSplit.Dispose();
                ho_SelectedContours.Dispose();
                ho_UnionContours.Dispose();
                ho_RegionDifference.Dispose();
                algorithm.Region.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("缺陷面积");
                hv_result = hv_result.TupleConcat(0);


                result = hv_result.Clone();
                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_ContoursL.Dispose();
                ho_RegionLines.Dispose();
                ho_RegionUnion1.Dispose();
                ho_RegionLines1.Dispose();
                ho_RegionLines2.Dispose();
                ho_RegionUnion2.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_Contours.Dispose();
                ho_ContoursSplit.Dispose();
                ho_SelectedContours.Dispose();
                ho_UnionContours.Dispose();
                ho_RegionDifference.Dispose();
                algorithm.Region.Dispose();

            }
            finally
            {

                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_ContoursL.Dispose();
                ho_RegionLines.Dispose();
                ho_RegionUnion1.Dispose();
                ho_RegionLines1.Dispose();
                ho_RegionLines2.Dispose();
                ho_RegionUnion2.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_Contours.Dispose();
                ho_ContoursSplit.Dispose();
                ho_SelectedContours.Dispose();
                ho_UnionContours.Dispose();
                ho_RegionDifference.Dispose();
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



