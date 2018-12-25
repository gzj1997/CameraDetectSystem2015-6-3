using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
    [Serializable]
    class luowenbiaomian5 : ImageTools
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
        public luowenbiaomian5()
        {
            //RegionToDisp = Image;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public luowenbiaomian5(HObject Image, Algorithm al)
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
            HObject ho_Region, ho_RegionLines, ho_RegionUnion1, ho_RegionUnion2;
            HObject ho_RegionUnion3, ho_Region1, ho_ConnectedRegions;
            HObject ho_RegionFillUp, ho_SelectedRegions, ho_SortedRegions;
            HObject ho_ObjectSelected = null, ho_Rectangle1 = null, ho_ImageReduced1 = null;
            HObject ho_Circle1 = null, ho_Circle2 = null, ho_Circle3 = null;
            HObject ho_Circle4 = null, ho_RegionUnionc1 = null, ho_RegionUnionc2 = null;
            HObject ho_RegionUnionc3 = null, ho_ConnectedRegionsc = null;
            HObject ho_SortedRegions1 = null, ho_ContoursL = null, ho_RegionLinesL = null;
            HObject ho_RegionLinesL1 = null, ho_RegionUnionL = null, ho_ConnectedRegionsL = null;
            HObject ho_SelectedRegionsL = null, ho_RegionFillUpL = null;
            HObject ho_ContoursSplitL = null, ho_SelectedContoursL = null;
            HObject ho_UnionContoursL = null, ho_RegionL = null, ho_RegionDifferenceL = null;
            HObject ho_ContoursR = null, ho_RegionLinesR = null, ho_RegionLinesR1 = null;
            HObject ho_RegionUnionR = null, ho_ConnectedRegionsR = null;
            HObject ho_SelectedRegionsR = null, ho_RegionFillUpR = null;
            HObject ho_ContoursSplitR = null, ho_SelectedContoursR = null;
            HObject ho_UnionContoursR = null, ho_RegionR = null, ho_RegionDifferenceR = null;
            HObject ho_ConnectedRegions1, ho_ObjectSelected1=null;

            // Local control variables 

            HTuple hv_Width = null, hv_Height = null, hv_Row11 = null;
            HTuple hv_Column11 = null, hv_Row21 = null, hv_Column21 = null;
            HTuple hv_Row12 = null, hv_Column12 = null, hv_Row22 = null;
            HTuple hv_Column22 = null, hv_Number = null, hv_h = null;
            HTuple hv_w = null, hv_i = null, hv_Row = new HTuple();
            HTuple hv_Column = new HTuple(), hv_Phi = new HTuple();
            HTuple hv_Length1 = new HTuple(), hv_Length2 = new HTuple();
            HTuple hv_Cos = new HTuple(), hv_Sin = new HTuple(), hv_RT_X = new HTuple();
            HTuple hv_RT_Y = new HTuple(), hv_AX = new HTuple(), hv_AY = new HTuple();
            HTuple hv_RB_X = new HTuple(), hv_RB_Y = new HTuple();
            HTuple hv_BX = new HTuple(), hv_BY = new HTuple(), hv_LB_X = new HTuple();
            HTuple hv_LB_Y = new HTuple(), hv_CX = new HTuple(), hv_CY = new HTuple();
            HTuple hv_LT_X = new HTuple(), hv_LT_Y = new HTuple();
            HTuple hv_DX = new HTuple(), hv_DY = new HTuple(), hv_Number1 = new HTuple();
            HTuple hv_Area = new HTuple(), hv_Row1 = new HTuple();
            HTuple hv_Column1 = new HTuple(), hv_Distance = new HTuple();
            HTuple hv_Distance1 = new HTuple(), hv_shapeParam = new HTuple();
            HTuple hv_MetrologyHandleL = new HTuple(), hv_Index = new HTuple();
            HTuple hv_RowL = new HTuple(), hv_ColumnL = new HTuple();
            HTuple hv_l = new HTuple(), hv_NumberL = new HTuple();
            HTuple hv_MetrologyHandleR = new HTuple(), hv_RowR = new HTuple();
            HTuple hv_ColumnR = new HTuple(), hv_r = new HTuple();
            HTuple hv_NumberR = new HTuple(), hv_AreaL = new HTuple();
            HTuple hv_AreaR = new HTuple(), hv_Area1 = null, hv_Row2 = null;
            HTuple hv_Column2 = null, hv_Area2 = null, hv_Row3 = null;
            HTuple hv_Column3 = null, hv_hm = null, hv_wm = null, hv_Row13 = null;
            HTuple hv_Column13 = null, hv_Row23 = null, hv_Column23 = null;
            HTuple hv_zh = null, hv_w1 = new HTuple(),hv_Row4= new HTuple(), hv_Column4= new HTuple();
            HTuple hv_Phi1 = new HTuple(), hv_Length11 = new HTuple(), hv_Length21 = new HTuple(), hv_Concat = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_RegionLines);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion1);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion2);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion3);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SortedRegions);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected);
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
            HOperatorSet.GenEmptyObj(out ho_Circle1);
            HOperatorSet.GenEmptyObj(out ho_Circle2);
            HOperatorSet.GenEmptyObj(out ho_Circle3);
            HOperatorSet.GenEmptyObj(out ho_Circle4);
            HOperatorSet.GenEmptyObj(out ho_RegionUnionc1);
            HOperatorSet.GenEmptyObj(out ho_RegionUnionc2);
            HOperatorSet.GenEmptyObj(out ho_RegionUnionc3);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegionsc);
            HOperatorSet.GenEmptyObj(out ho_SortedRegions1);
            HOperatorSet.GenEmptyObj(out ho_ContoursL);
            HOperatorSet.GenEmptyObj(out ho_RegionLinesL);
            HOperatorSet.GenEmptyObj(out ho_RegionLinesL1);
            HOperatorSet.GenEmptyObj(out ho_RegionUnionL);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegionsL);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsL);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUpL);
            HOperatorSet.GenEmptyObj(out ho_ContoursSplitL);
            HOperatorSet.GenEmptyObj(out ho_SelectedContoursL);
            HOperatorSet.GenEmptyObj(out ho_UnionContoursL);
            HOperatorSet.GenEmptyObj(out ho_RegionL);
            HOperatorSet.GenEmptyObj(out ho_RegionDifferenceL);
            HOperatorSet.GenEmptyObj(out ho_ContoursR);
            HOperatorSet.GenEmptyObj(out ho_RegionLinesR);
            HOperatorSet.GenEmptyObj(out ho_RegionLinesR1);
            HOperatorSet.GenEmptyObj(out ho_RegionUnionR);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegionsR);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsR);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUpR);
            HOperatorSet.GenEmptyObj(out ho_ContoursSplitR);
            HOperatorSet.GenEmptyObj(out ho_SelectedContoursR);
            HOperatorSet.GenEmptyObj(out ho_UnionContoursR);
            HOperatorSet.GenEmptyObj(out ho_RegionR);
            HOperatorSet.GenEmptyObj(out ho_RegionDifferenceR);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);

            HOperatorSet.GenEmptyObj(out ho_ObjectSelected1);


            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle, DRow1m, DCol1m, DRow2m, DCol2m);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Rectangle, out ho_ImageReduced);
                ho_Region.Dispose();
                HOperatorSet.GetImageSize(Image, out hv_Width, out hv_Height);
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, 128, 255);
                HOperatorSet.SmallestRectangle1(ho_Region, out hv_Row12, out hv_Column12, out hv_Row22,
                    out hv_Column22);

                ho_RegionLines.Dispose();
                HOperatorSet.GenRegionLine(out ho_RegionLines, (hv_Row12 + hv_Row22) / 2, hv_Column12,
                    ((hv_Row12 + hv_Row22) / 2) + 1, hv_Column12);

                ho_RegionUnion1.Dispose();
                HOperatorSet.Union1(ho_RegionLines, out ho_RegionUnion1);

                ho_RegionUnion2.Dispose();
                HOperatorSet.Union1(ho_RegionLines, out ho_RegionUnion2);

                ho_RegionUnion3.Dispose();
                HOperatorSet.Union1(ho_RegionLines, out ho_RegionUnion3);


                ho_Region1.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region1, 128, 255);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Region1, out ho_ConnectedRegions);

                ho_RegionFillUp.Dispose();
                HOperatorSet.FillUp(ho_ConnectedRegions, out ho_RegionFillUp);

                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_RegionFillUp, out ho_SelectedRegions, "area", "and",
                    500, 99999000);

                ho_SortedRegions.Dispose();
                HOperatorSet.SortRegion(ho_SelectedRegions, out ho_SortedRegions, "upper_left",
                    "true", "column");

                HOperatorSet.CountObj(ho_SortedRegions, out hv_Number);

                // stop(); only in hdevelop
                // stop(); only in hdevelop
                hv_h = new HTuple();
                hv_w = new HTuple();
                hv_w1 = new HTuple();
                ho_ObjectSelected1.Dispose();
                HOperatorSet.SelectObj(ho_SortedRegions, out ho_ObjectSelected1, 1);
                HOperatorSet.SmallestRectangle2(ho_ObjectSelected1, out hv_Row4, out hv_Column4,
                    out hv_Phi1, out hv_Length11, out hv_Length21);
                hv_w1 = hv_w1.TupleConcat(hv_Length21 * 2);
                HTuple end_val40 = hv_Number - 1;
                HTuple step_val40 = 1;
                for (hv_i = 2; hv_i.Continue(end_val40, step_val40); hv_i = hv_i.TupleAdd(step_val40))
                {
                    ho_ObjectSelected.Dispose();
                    HOperatorSet.SelectObj(ho_SortedRegions, out ho_ObjectSelected, hv_i);
                    HOperatorSet.SmallestRectangle2(ho_ObjectSelected, out hv_Row, out hv_Column,
                        out hv_Phi, out hv_Length1, out hv_Length2);
                    //if (Length1>(Row22-Row12)*0.25)
                    ho_Rectangle1.Dispose();
                    HOperatorSet.GenRectangle2(out ho_Rectangle1, hv_Row, hv_Column, hv_Phi,
                        hv_Length1 * 0.85, hv_Length2);
                    ho_ImageReduced1.Dispose();
                    HOperatorSet.ReduceDomain(ho_ImageReduced, ho_Rectangle1, out ho_ImageReduced1
                        );
                    //*矩形4个角点
                    HOperatorSet.TupleCos(hv_Phi, out hv_Cos);
                    HOperatorSet.TupleSin(hv_Phi, out hv_Sin);
                    //*dev_set_color('green')
                    hv_RT_X = (((-hv_Length1) * 0.85) * hv_Cos) - (hv_Length2 * hv_Sin);
                    hv_RT_Y = (((-hv_Length1) * 0.85) * hv_Sin) + (hv_Length2 * hv_Cos);
                    ho_Circle1.Dispose();
                    HOperatorSet.GenCircle(out ho_Circle1, hv_Row - hv_RT_Y, hv_Column + hv_RT_X,
                        0.5);
                    //*最小矩形的顶点A
                    hv_AX = (((-hv_Length1) * 0.85) * hv_Cos) - (hv_Length2 * hv_Sin);
                    hv_AY = (((-hv_Length1) * 0.85) * hv_Sin) + (hv_Length2 * hv_Cos);
                    //*dev_set_color('red')
                    hv_RB_X = ((hv_Length1 * 0.85) * hv_Cos) - (hv_Length2 * hv_Sin);
                    hv_RB_Y = ((hv_Length1 * 0.85) * hv_Sin) + (hv_Length2 * hv_Cos);
                    ho_Circle2.Dispose();
                    HOperatorSet.GenCircle(out ho_Circle2, hv_Row - hv_RB_Y, hv_Column + hv_RB_X,
                        0.5);
                    //*最小矩形的顶点B
                    hv_BX = ((hv_Length1 * 0.85) * hv_Cos) - (hv_Length2 * hv_Sin);
                    hv_BY = ((hv_Length1 * 0.85) * hv_Sin) + (hv_Length2 * hv_Cos);

                    //*dev_set_color('yellow')
                    hv_LB_X = ((hv_Length1 * 0.85) * hv_Cos) + (hv_Length2 * hv_Sin);
                    hv_LB_Y = ((hv_Length1 * 0.85) * hv_Sin) - (hv_Length2 * hv_Cos);
                    ho_Circle3.Dispose();
                    HOperatorSet.GenCircle(out ho_Circle3, hv_Row - hv_LB_Y, hv_Column + hv_LB_X,
                        0.5);
                    //*最小矩形的顶点C
                    hv_CX = ((hv_Length1 * 0.85) * hv_Cos) + (hv_Length2 * hv_Sin);
                    hv_CY = ((hv_Length1 * 0.85) * hv_Sin) - (hv_Length2 * hv_Cos);

                    //*dev_set_color('pink')
                    hv_LT_X = (((-hv_Length1) * 0.85) * hv_Cos) + (hv_Length2 * hv_Sin);
                    hv_LT_Y = (((-hv_Length1) * 0.85) * hv_Sin) - (hv_Length2 * hv_Cos);
                    ho_Circle4.Dispose();
                    HOperatorSet.GenCircle(out ho_Circle4, hv_Row - hv_LT_Y, hv_Column + hv_LT_X,
                        0.5);
                    //*最小矩形的顶点D
                    hv_DX = (((-hv_Length1) * 0.85) * hv_Cos) + (hv_Length2 * hv_Sin);
                    hv_DY = (((-hv_Length1) * 0.85) * hv_Sin) - (hv_Length2 * hv_Cos);

                    //stop ()

                    ho_RegionUnionc1.Dispose();
                    HOperatorSet.Union2(ho_Circle1, ho_Circle2, out ho_RegionUnionc1);
                    ho_RegionUnionc2.Dispose();
                    HOperatorSet.Union2(ho_RegionUnionc1, ho_Circle3, out ho_RegionUnionc2);
                    ho_RegionUnionc3.Dispose();
                    HOperatorSet.Union2(ho_RegionUnionc2, ho_Circle4, out ho_RegionUnionc3);
                    ho_ConnectedRegionsc.Dispose();
                    HOperatorSet.Connection(ho_RegionUnionc3, out ho_ConnectedRegionsc);
                    //count_obj (RegionUnionc3, Number1)
                    ho_SortedRegions1.Dispose();
                    HOperatorSet.SortRegion(ho_ConnectedRegionsc, out ho_SortedRegions1, "upper_left",
                        "true", "row");

                    HOperatorSet.AreaCenter(ho_SortedRegions1, out hv_Area, out hv_Row1, out hv_Column1);
                    HOperatorSet.DistancePp(hv_Row1.TupleSelect(0), hv_Column1.TupleSelect(
                        0), hv_Row1.TupleSelect(1), hv_Column1.TupleSelect(1), out hv_Distance);
                    HOperatorSet.DistancePl(hv_Row1.TupleSelect(0), hv_Column1.TupleSelect(
                        0), hv_Row1.TupleSelect(2), hv_Column1.TupleSelect(2), hv_Row1.TupleSelect(
                        3), hv_Column1.TupleSelect(3), out hv_Distance1);
                    //if (i>1)
                    hv_h = hv_h.TupleConcat((hv_Distance1 * 4) / 3);
                    //endif
                    hv_w = hv_w.TupleConcat(hv_Distance);

                    //gen_region_line (RegionLines2, (Row1[0]+Row1[1])/2, (Column1[0]+Column1[1])/2, (Row1[2]+Row1[3])/2, (Column1[2]+Column1[3])/2)
                    hv_shapeParam = new HTuple();
                    hv_shapeParam = hv_shapeParam.TupleConcat(((hv_Row1.TupleSelect(
                        0)) + (hv_Row1.TupleSelect(1))) / 2);
                    hv_shapeParam = hv_shapeParam.TupleConcat(((hv_Column1.TupleSelect(
                        0)) + (hv_Column1.TupleSelect(1))) / 2);
                    hv_shapeParam = hv_shapeParam.TupleConcat(((hv_Row1.TupleSelect(
                        2)) + (hv_Row1.TupleSelect(3))) / 2);
                    hv_shapeParam = hv_shapeParam.TupleConcat(((hv_Column1.TupleSelect(
                        2)) + (hv_Column1.TupleSelect(3))) / 2);

                    //shapeParam := [Row12,(Column22+Column12)/2,Row22,(Column22+Column12)/2]
                    //找左侧点
                    HOperatorSet.CreateMetrologyModel(out hv_MetrologyHandleL);
                    HOperatorSet.SetMetrologyModelImageSize(hv_MetrologyHandleL, hv_Width,
                        hv_Height);
                    HOperatorSet.AddMetrologyObjectGeneric(hv_MetrologyHandleL, "line", hv_shapeParam,
                        hv_Distance / 2, 1, 1, 60, new HTuple(), new HTuple(), out hv_Index);
                    //设置参数
                    HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandleL, "all", "measure_transition",
                        "negative");
                    HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandleL, "all", "num_measures",
                        150);
                    HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandleL, "all", "num_instances",
                        1);
                    HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandleL, "all", "measure_select",
                        "last");

                    //开始找边缘
                    HOperatorSet.ApplyMetrologyModel(ho_ImageReduced1, hv_MetrologyHandleL);
                    ho_ContoursL.Dispose();
                    HOperatorSet.GetMetrologyObjectMeasures(out ho_ContoursL, hv_MetrologyHandleL,
                        "all", "all", out hv_RowL, out hv_ColumnL);

                    if (HDevWindowStack.IsOpen())
                    {
                        //dev_set_color ('red')
                    }
                    //disp_cross (3600, RowL, ColumnL, 6, Phi)

                    //左侧点，用一Row,只取最左边点
                    //gen_cross_contour_xld (CrossL, RowL, ColumnL, 6, 0)
                    ///释放测量句柄
                    HOperatorSet.ClearMetrologyModel(hv_MetrologyHandleL);
                    //gen_region_line (RegionLines, Row11, Column11, Row11+1, Column11)
                    //union1 (RegionLines, RegionUnion1)

                    for (hv_l = 0; (int)hv_l <= (int)((new HTuple(hv_RowL.TupleLength())) - 2); hv_l = (int)hv_l + 1)
                    {
                        ho_RegionLinesL.Dispose();
                        HOperatorSet.GenRegionLine(out ho_RegionLinesL, hv_RowL.TupleSelect(hv_l),
                            hv_ColumnL.TupleSelect(hv_l), hv_RowL.TupleSelect(hv_l + 1), hv_ColumnL.TupleSelect(
                            hv_l + 1));
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.Union2(ho_RegionUnion1, ho_RegionLinesL, out ExpTmpOutVar_0
                                );
                            ho_RegionUnion1.Dispose();
                            ho_RegionUnion1 = ExpTmpOutVar_0;
                        }
                    }

                    ho_RegionLinesL1.Dispose();
                    HOperatorSet.GenRegionLine(out ho_RegionLinesL1, hv_RowL.TupleSelect(0),
                        hv_ColumnL.TupleSelect(0), hv_RowL.TupleSelect((new HTuple(hv_RowL.TupleLength()
                        )) - 1), hv_ColumnL.TupleSelect((new HTuple(hv_RowL.TupleLength())) - 1));
                    ho_RegionUnionL.Dispose();
                    HOperatorSet.Union2(ho_RegionUnion1, ho_RegionLinesL1, out ho_RegionUnionL
                        );
                    ho_ConnectedRegionsL.Dispose();
                    HOperatorSet.Connection(ho_RegionUnionL, out ho_ConnectedRegionsL);
                    ho_SelectedRegionsL.Dispose();
                    HOperatorSet.SelectShapeStd(ho_ConnectedRegionsL, out ho_SelectedRegionsL,
                        "max_area", 70);
                    ho_RegionFillUpL.Dispose();
                    HOperatorSet.FillUp(ho_SelectedRegionsL, out ho_RegionFillUpL);
                    ho_ContoursL.Dispose();
                    HOperatorSet.GenContourRegionXld(ho_RegionFillUpL, out ho_ContoursL, "border");
                    ho_ContoursSplitL.Dispose();
                    HOperatorSet.SegmentContoursXld(ho_ContoursL, out ho_ContoursSplitL, "lines_circles",
                        5, 8, 3.5);
                    ho_SelectedContoursL.Dispose();
                    HOperatorSet.SelectContoursXld(ho_ContoursSplitL, out ho_SelectedContoursL,
                        "contour_length", 70, 2000, -0.5, 0.5);
                    //***
                    ho_UnionContoursL.Dispose();
                    HOperatorSet.UnionAdjacentContoursXld(ho_SelectedContoursL, out ho_UnionContoursL,
                        10, 1, "attr_keep");
                    ho_RegionL.Dispose();
                    HOperatorSet.GenRegionContourXld(ho_UnionContoursL, out ho_RegionL, "filled");

                    ho_RegionDifferenceL.Dispose();
                    HOperatorSet.Difference(ho_RegionFillUpL, ho_RegionL, out ho_RegionDifferenceL
                        );
                    HOperatorSet.CountObj(ho_RegionDifferenceL, out hv_NumberL);

                    //*
                    //找右侧点
                    HOperatorSet.CreateMetrologyModel(out hv_MetrologyHandleR);
                    HOperatorSet.SetMetrologyModelImageSize(hv_MetrologyHandleR, hv_Width,
                        hv_Height);
                    HOperatorSet.AddMetrologyObjectGeneric(hv_MetrologyHandleR, "line", hv_shapeParam,
                        hv_Distance / 2, 1, 1, 60, new HTuple(), new HTuple(), out hv_Index);
                    //设置参数
                    HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandleR, "all", "measure_transition",
                        "positive");
                    HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandleR, "all", "num_measures",
                        100);
                    HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandleR, "all", "num_instances",
                        1);
                    HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandleR, "all", "measure_select",
                        "first");

                    //开始找边缘
                    HOperatorSet.ApplyMetrologyModel(ho_ImageReduced1, hv_MetrologyHandleR);
                    ho_ContoursR.Dispose();
                    HOperatorSet.GetMetrologyObjectMeasures(out ho_ContoursR, hv_MetrologyHandleR,
                        "all", "all", out hv_RowR, out hv_ColumnR);

                    //disp_obj (Image, 3600)
                    if (HDevWindowStack.IsOpen())
                    {
                        //dev_set_color ('red')
                    }
                    //disp_cross (3600, RowR, ColumnR, 6, Phi)
                    //*右侧点，用一Row,只取最右边点
                    //gen_cross_contour_xld (CrossR, RowR, ColumnR, 6, 0)

                    ///释放测量句柄
                    HOperatorSet.ClearMetrologyModel(hv_MetrologyHandleR);
                    //close_measure (MeasureHandle1)
                    //stop ()

                    //gen_region_line (RegionLines, Row11, Column11, Row11+1, Column11)
                    //union1 (RegionLines, RegionUnion1)

                    for (hv_r = 0; (int)hv_r <= (int)((new HTuple(hv_RowR.TupleLength())) - 2); hv_r = (int)hv_r + 1)
                    {
                        ho_RegionLinesR.Dispose();
                        HOperatorSet.GenRegionLine(out ho_RegionLinesR, hv_RowR.TupleSelect(hv_r),
                            hv_ColumnR.TupleSelect(hv_r), hv_RowR.TupleSelect(hv_r + 1), hv_ColumnR.TupleSelect(
                            hv_r + 1));
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.Union2(ho_RegionUnion2, ho_RegionLinesR, out ExpTmpOutVar_0
                                );
                            ho_RegionUnion2.Dispose();
                            ho_RegionUnion2 = ExpTmpOutVar_0;
                        }
                    }

                    ho_RegionLinesR1.Dispose();
                    HOperatorSet.GenRegionLine(out ho_RegionLinesR1, hv_RowR.TupleSelect(0),
                        hv_ColumnR.TupleSelect(0), hv_RowR.TupleSelect((new HTuple(hv_RowR.TupleLength()
                        )) - 1), hv_ColumnR.TupleSelect((new HTuple(hv_RowR.TupleLength())) - 1));
                    ho_RegionUnionR.Dispose();
                    HOperatorSet.Union2(ho_RegionUnion2, ho_RegionLinesR1, out ho_RegionUnionR
                        );
                    ho_ConnectedRegionsR.Dispose();
                    HOperatorSet.Connection(ho_RegionUnionR, out ho_ConnectedRegionsR);
                    ho_SelectedRegionsR.Dispose();
                    HOperatorSet.SelectShapeStd(ho_ConnectedRegionsR, out ho_SelectedRegionsR,
                        "max_area", 70);
                    ho_RegionFillUpR.Dispose();
                    HOperatorSet.FillUp(ho_SelectedRegionsR, out ho_RegionFillUpR);
                    ho_ContoursR.Dispose();
                    HOperatorSet.GenContourRegionXld(ho_RegionFillUpR, out ho_ContoursR, "border");
                    ho_ContoursSplitR.Dispose();
                    HOperatorSet.SegmentContoursXld(ho_ContoursR, out ho_ContoursSplitR, "lines_circles",
                        5, 8, 3.5);
                    ho_SelectedContoursR.Dispose();
                    HOperatorSet.SelectContoursXld(ho_ContoursSplitR, out ho_SelectedContoursR,
                        "contour_length", 70, 2000, -0.5, 0.5);
                    ho_UnionContoursR.Dispose();
                    HOperatorSet.UnionAdjacentContoursXld(ho_SelectedContoursR, out ho_UnionContoursR,
                        10, 1, "attr_keep");
                    ho_RegionR.Dispose();
                    HOperatorSet.GenRegionContourXld(ho_UnionContoursR, out ho_RegionR, "filled");

                    ho_RegionDifferenceR.Dispose();
                    HOperatorSet.Difference(ho_RegionFillUpR, ho_RegionR, out ho_RegionDifferenceR
                        );
                    HOperatorSet.CountObj(ho_RegionDifferenceR, out hv_NumberR);

                    //stop ()

                    if ((int)(new HTuple(hv_NumberL.TupleGreater(0))) != 0)
                    {
                        HOperatorSet.AreaCenter(ho_RegionDifferenceL, out hv_AreaL, out hv_RowL,
                            out hv_ColumnL);
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.Union2(ho_RegionUnion3, ho_RegionDifferenceL, out ExpTmpOutVar_0
                                );
                            ho_RegionUnion3.Dispose();
                            ho_RegionUnion3 = ExpTmpOutVar_0;
                        }
                    }
                    if ((int)(new HTuple(hv_NumberR.TupleGreater(0))) != 0)
                    {
                        HOperatorSet.AreaCenter(ho_RegionDifferenceR, out hv_AreaR, out hv_RowR,
                            out hv_ColumnR);
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.Union2(ho_RegionUnion3, ho_RegionDifferenceR, out ExpTmpOutVar_0
                                );
                            ho_RegionUnion3.Dispose();
                            ho_RegionUnion3 = ExpTmpOutVar_0;
                        }
                    }
                    //union2 (RegionDifferenceL, RegionDifferenceR, RegionUnionLR)
                    //union2 (RegionUnion3, RegionUnionLR, RegionUnion3)
                    // stop(); only in hdevelop
                    //endif
                }

                HOperatorSet.AreaCenter(ho_RegionUnion3, out hv_Area1, out hv_Row2, out hv_Column2);
                ho_ConnectedRegions1.Dispose();
                HOperatorSet.Connection(ho_RegionUnion3, out ho_ConnectedRegions1);
                HOperatorSet.AreaCenter(ho_ConnectedRegions1, out hv_Area2, out hv_Row3, out hv_Column3);
                HOperatorSet.TupleConcat(hv_w1, hv_w, out hv_Concat);
                hv_hm = (hv_h.TupleMin()) / (hv_h.TupleMean());
                hv_wm = (hv_Concat.TupleMax()) / (hv_Concat.TupleMean());
                ho_ObjectSelected.Dispose();
                HOperatorSet.SelectObj(ho_SortedRegions, out ho_ObjectSelected, hv_Number);
                HOperatorSet.SmallestRectangle1(ho_ObjectSelected, out hv_Row13, out hv_Column13,
                    out hv_Row23, out hv_Column23);
                hv_zh = hv_Column23 - hv_Column13;
                HOperatorSet.Union1(ho_ConnectedRegions1, out RegionToDisp);

                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("最短比值");
                hv_result = hv_result.TupleConcat(hv_hm.D);
                hv_result = hv_result.TupleConcat("最宽比值");
                hv_result = hv_result.TupleConcat(hv_wm.D);
                hv_result = hv_result.TupleConcat("缺陷总面积");
                hv_result = hv_result.TupleConcat(hv_Area1.D);
                hv_result = hv_result.TupleConcat("最大缺陷面积");
                hv_result = hv_result.TupleConcat(hv_Area2.D);
                hv_result = hv_result.TupleConcat("宽度");
                hv_result = hv_result.TupleConcat(hv_zh.D);


                result = hv_result.Clone();
                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_RegionLines.Dispose();
                ho_RegionUnion1.Dispose();
                ho_RegionUnion2.Dispose();
                ho_RegionUnion3.Dispose();
                ho_Region1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_SelectedRegions.Dispose();
                ho_SortedRegions.Dispose();
                ho_ObjectSelected.Dispose();
                ho_Rectangle1.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Circle1.Dispose();
                ho_Circle2.Dispose();
                ho_Circle3.Dispose();
                ho_Circle4.Dispose();
                ho_RegionUnionc1.Dispose();
                ho_RegionUnionc2.Dispose();
                ho_RegionUnionc3.Dispose();
                ho_ConnectedRegionsc.Dispose();
                ho_SortedRegions1.Dispose();
                ho_ContoursL.Dispose();
                ho_RegionLinesL.Dispose();
                ho_RegionLinesL1.Dispose();
                ho_RegionUnionL.Dispose();
                ho_ConnectedRegionsL.Dispose();
                ho_SelectedRegionsL.Dispose();
                ho_RegionFillUpL.Dispose();
                ho_ContoursSplitL.Dispose();
                ho_SelectedContoursL.Dispose();
                ho_UnionContoursL.Dispose();
                ho_RegionL.Dispose();
                ho_RegionDifferenceL.Dispose();
                ho_ContoursR.Dispose();
                ho_RegionLinesR.Dispose();
                ho_RegionLinesR1.Dispose();
                ho_RegionUnionR.Dispose();
                ho_ConnectedRegionsR.Dispose();
                ho_SelectedRegionsR.Dispose();
                ho_RegionFillUpR.Dispose();
                ho_ContoursSplitR.Dispose();
                ho_SelectedContoursR.Dispose();
                ho_UnionContoursR.Dispose();
                ho_RegionR.Dispose();
                ho_RegionDifferenceR.Dispose();
                ho_ConnectedRegions1.Dispose();

                ho_ObjectSelected1.Dispose();
                algorithm.Region.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("最短比值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("最宽比值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("缺陷总面积");
                hv_result = hv_result.TupleConcat(999999);
                hv_result = hv_result.TupleConcat("最大缺陷面积");
                hv_result = hv_result.TupleConcat(999999);
                hv_result = hv_result.TupleConcat("宽度");
                hv_result = hv_result.TupleConcat(999999);
                result = hv_result.Clone();
                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_RegionLines.Dispose();
                ho_RegionUnion1.Dispose();
                ho_RegionUnion2.Dispose();
                ho_RegionUnion3.Dispose();
                ho_Region1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_SelectedRegions.Dispose();
                ho_SortedRegions.Dispose();
                ho_ObjectSelected.Dispose();
                ho_Rectangle1.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Circle1.Dispose();
                ho_Circle2.Dispose();
                ho_Circle3.Dispose();
                ho_Circle4.Dispose();
                ho_RegionUnionc1.Dispose();
                ho_RegionUnionc2.Dispose();
                ho_RegionUnionc3.Dispose();
                ho_ConnectedRegionsc.Dispose();
                ho_SortedRegions1.Dispose();
                ho_ContoursL.Dispose();
                ho_RegionLinesL.Dispose();
                ho_RegionLinesL1.Dispose();
                ho_RegionUnionL.Dispose();
                ho_ConnectedRegionsL.Dispose();
                ho_SelectedRegionsL.Dispose();
                ho_RegionFillUpL.Dispose();
                ho_ContoursSplitL.Dispose();
                ho_SelectedContoursL.Dispose();
                ho_UnionContoursL.Dispose();
                ho_RegionL.Dispose();
                ho_RegionDifferenceL.Dispose();
                ho_ContoursR.Dispose();
                ho_RegionLinesR.Dispose();
                ho_RegionLinesR1.Dispose();
                ho_RegionUnionR.Dispose();
                ho_ConnectedRegionsR.Dispose();
                ho_SelectedRegionsR.Dispose();
                ho_RegionFillUpR.Dispose();
                ho_ContoursSplitR.Dispose();
                ho_SelectedContoursR.Dispose();
                ho_UnionContoursR.Dispose();
                ho_RegionR.Dispose();
                ho_RegionDifferenceR.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_ObjectSelected1.Dispose();
                algorithm.Region.Dispose();

            }
            finally
            {
                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_RegionLines.Dispose();
                ho_RegionUnion1.Dispose();
                ho_RegionUnion2.Dispose();
                ho_RegionUnion3.Dispose();
                ho_Region1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_SelectedRegions.Dispose();
                ho_SortedRegions.Dispose();
                ho_ObjectSelected.Dispose();
                ho_Rectangle1.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Circle1.Dispose();
                ho_Circle2.Dispose();
                ho_Circle3.Dispose();
                ho_Circle4.Dispose();
                ho_RegionUnionc1.Dispose();
                ho_RegionUnionc2.Dispose();
                ho_RegionUnionc3.Dispose();
                ho_ConnectedRegionsc.Dispose();
                ho_SortedRegions1.Dispose();
                ho_ContoursL.Dispose();
                ho_RegionLinesL.Dispose();
                ho_RegionLinesL1.Dispose();
                ho_RegionUnionL.Dispose();
                ho_ConnectedRegionsL.Dispose();
                ho_SelectedRegionsL.Dispose();
                ho_RegionFillUpL.Dispose();
                ho_ContoursSplitL.Dispose();
                ho_SelectedContoursL.Dispose();
                ho_UnionContoursL.Dispose();
                ho_RegionL.Dispose();
                ho_RegionDifferenceL.Dispose();
                ho_ContoursR.Dispose();
                ho_RegionLinesR.Dispose();
                ho_RegionLinesR1.Dispose();
                ho_RegionUnionR.Dispose();
                ho_ConnectedRegionsR.Dispose();
                ho_SelectedRegionsR.Dispose();
                ho_RegionFillUpR.Dispose();
                ho_ContoursSplitR.Dispose();
                ho_SelectedContoursR.Dispose();
                ho_UnionContoursR.Dispose();
                ho_RegionR.Dispose();
                ho_RegionDifferenceR.Dispose();
                ho_ConnectedRegions1.Dispose();
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



