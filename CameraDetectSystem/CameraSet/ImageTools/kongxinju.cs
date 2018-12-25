using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
    [Serializable]
    class kongxinju : ImageTools
    {
        #region ROI





        [NonSerialized]
        private HTuple nu = new HTuple();
        [NonSerialized]
        private HTuple nu1 = new HTuple();
        [NonSerialized]
        private HTuple nu2 = new HTuple();
        [NonSerialized]
        private HTuple nu3 = new HTuple();
        [NonSerialized]
        private HTuple nu4 = new HTuple();
        [NonSerialized]
        private HTuple nu5 = new HTuple();
        public double mj { set; get; }
        public double cd { set; get; }

        public double yd { set; get; }

        public double bjz { set; get; }
        public double kdz { set; get; }
        public double pzz { set; get; }







        #endregion
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public kongxinju()
        {
            //RegionToDisp = Image;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public kongxinju(HObject Image, Algorithm al)
        {
            gexxs = 1;
            gex = 0;
            this.Image = Image;
            this.algorithm.Image = Image;
            this.algorithm = al;
            pixeldist = 1;
        }
        //}
        public void disp_message(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem,
       HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_Red = null, hv_Green = null, hv_Blue = null;
            HTuple hv_Row1Part = null, hv_Column1Part = null, hv_Row2Part = null;
            HTuple hv_Column2Part = null, hv_RowWin = null, hv_ColumnWin = null;
            HTuple hv_WidthWin = null, hv_HeightWin = null, hv_MaxAscent = null;
            HTuple hv_MaxDescent = null, hv_MaxWidth = null, hv_MaxHeight = null;
            HTuple hv_R1 = new HTuple(), hv_C1 = new HTuple(), hv_FactorRow = new HTuple();
            HTuple hv_FactorColumn = new HTuple(), hv_UseShadow = null;
            HTuple hv_ShadowColor = null, hv_Exception = new HTuple();
            HTuple hv_Width = new HTuple(), hv_Index = new HTuple();
            HTuple hv_Ascent = new HTuple(), hv_Descent = new HTuple();
            HTuple hv_W = new HTuple(), hv_H = new HTuple(), hv_FrameHeight = new HTuple();
            HTuple hv_FrameWidth = new HTuple(), hv_R2 = new HTuple();
            HTuple hv_C2 = new HTuple(), hv_DrawMode = new HTuple();
            HTuple hv_CurrentColor = new HTuple();
            HTuple hv_Box_COPY_INP_TMP = hv_Box.Clone();
            HTuple hv_Color_COPY_INP_TMP = hv_Color.Clone();
            HTuple hv_Column_COPY_INP_TMP = hv_Column.Clone();
            HTuple hv_Row_COPY_INP_TMP = hv_Row.Clone();
            HTuple hv_String_COPY_INP_TMP = hv_String.Clone();

            // Initialize local and output iconic variables 
            //This procedure displays text in a graphics window.
            //
            //Input parameters:
            //WindowHandle: The WindowHandle of the graphics window, where
            //   the message should be displayed
            //String: A tuple of strings containing the text message to be displayed
            //CoordSystem: If set to 'window', the text position is given
            //   with respect to the window coordinate system.
            //   If set to 'image', image coordinates are used.
            //   (This may be useful in zoomed images.)
            //Row: The row coordinate of the desired text position
            //   If set to -1, a default value of 12 is used.
            //Column: The column coordinate of the desired text position
            //   If set to -1, a default value of 12 is used.
            //Color: defines the color of the text as string.
            //   If set to [], '' or 'auto' the currently set color is used.
            //   If a tuple of strings is passed, the colors are used cyclically
            //   for each new textline.
            //Box: If Box[0] is set to 'true', the text is written within an orange box.
            //     If set to' false', no box is displayed.
            //     If set to a color string (e.g. 'white', '#FF00CC', etc.),
            //       the text is written in a box of that color.
            //     An optional second value for Box (Box[1]) controls if a shadow is displayed:
            //       'true' -> display a shadow in a default color
            //       'false' -> display no shadow (same as if no second value is given)
            //       otherwise -> use given string as color string for the shadow color
            //
            //Prepare window
            HOperatorSet.GetRgb(hv_WindowHandle, out hv_Red, out hv_Green, out hv_Blue);
            HOperatorSet.GetPart(hv_WindowHandle, out hv_Row1Part, out hv_Column1Part, out hv_Row2Part,
                out hv_Column2Part);
            HOperatorSet.GetWindowExtents(hv_WindowHandle, out hv_RowWin, out hv_ColumnWin,
                out hv_WidthWin, out hv_HeightWin);
            HOperatorSet.SetPart(hv_WindowHandle, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
            //
            //default settings
            if ((int)(new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Row_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Column_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
            {
                hv_Color_COPY_INP_TMP = "";
            }
            //
            hv_String_COPY_INP_TMP = ((("" + hv_String_COPY_INP_TMP) + "")).TupleSplit("\n");
            //
            //Estimate extentions of text depending on font size.
            HOperatorSet.GetFontExtents(hv_WindowHandle, out hv_MaxAscent, out hv_MaxDescent,
                out hv_MaxWidth, out hv_MaxHeight);
            if ((int)(new HTuple(hv_CoordSystem.TupleEqual("window"))) != 0)
            {
                hv_R1 = hv_Row_COPY_INP_TMP.Clone();
                hv_C1 = hv_Column_COPY_INP_TMP.Clone();
            }
            else
            {
                //Transform image to window coordinates
                hv_FactorRow = (1.0 * hv_HeightWin) / ((hv_Row2Part - hv_Row1Part) + 1);
                hv_FactorColumn = (1.0 * hv_WidthWin) / ((hv_Column2Part - hv_Column1Part) + 1);
                hv_R1 = ((hv_Row_COPY_INP_TMP - hv_Row1Part) + 0.5) * hv_FactorRow;
                hv_C1 = ((hv_Column_COPY_INP_TMP - hv_Column1Part) + 0.5) * hv_FactorColumn;
            }
            //
            //Display text box depending on text size
            hv_UseShadow = 1;
            hv_ShadowColor = "gray";
            if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleEqual("true"))) != 0)
            {
                if (hv_Box_COPY_INP_TMP == null)
                    hv_Box_COPY_INP_TMP = new HTuple();
                hv_Box_COPY_INP_TMP[0] = "#fce9d4";
                hv_ShadowColor = "#f28d26";
            }
            if ((int)(new HTuple((new HTuple(hv_Box_COPY_INP_TMP.TupleLength())).TupleGreater(
                1))) != 0)
            {
                if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual("true"))) != 0)
                {
                    //Use default ShadowColor set above
                }
                else if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual(
                    "false"))) != 0)
                {
                    hv_UseShadow = 0;
                }
                else
                {
                    hv_ShadowColor = hv_Box_COPY_INP_TMP[1];
                    //Valid color?
                    try
                    {
                        HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(
                            1));
                    }
                    // catch (Exception) 
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        hv_Exception = "Wrong value of control parameter Box[1] (must be a 'true', 'false', or a valid color string)";
                        throw new HalconException(hv_Exception);
                    }
                }
            }
            if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleNotEqual("false"))) != 0)
            {
                //Valid color?
                try
                {
                    HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(0));
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    hv_Exception = "Wrong value of control parameter Box[0] (must be a 'true', 'false', or a valid color string)";
                    throw new HalconException(hv_Exception);
                }
                //Calculate box extents
                hv_String_COPY_INP_TMP = (" " + hv_String_COPY_INP_TMP) + " ";
                hv_Width = new HTuple();
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    HOperatorSet.GetStringExtents(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
                        hv_Index), out hv_Ascent, out hv_Descent, out hv_W, out hv_H);
                    hv_Width = hv_Width.TupleConcat(hv_W);
                }
                hv_FrameHeight = hv_MaxHeight * (new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    ));
                hv_FrameWidth = (((new HTuple(0)).TupleConcat(hv_Width))).TupleMax();
                hv_R2 = hv_R1 + hv_FrameHeight;
                hv_C2 = hv_C1 + hv_FrameWidth;
                //Display rectangles
                HOperatorSet.GetDraw(hv_WindowHandle, out hv_DrawMode);
                HOperatorSet.SetDraw(hv_WindowHandle, "fill");
                //Set shadow color
                HOperatorSet.SetColor(hv_WindowHandle, hv_ShadowColor);
                if ((int)(hv_UseShadow) != 0)
                {
                    HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1 + 1, hv_C1 + 1, hv_R2 + 1, hv_C2 + 1);
                }
                //Set box color
                HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(0));
                HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1, hv_C1, hv_R2, hv_C2);
                HOperatorSet.SetDraw(hv_WindowHandle, hv_DrawMode);
            }
            //Write text.
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                hv_CurrentColor = hv_Color_COPY_INP_TMP.TupleSelect(hv_Index % (new HTuple(hv_Color_COPY_INP_TMP.TupleLength()
                    )));
                if ((int)((new HTuple(hv_CurrentColor.TupleNotEqual(""))).TupleAnd(new HTuple(hv_CurrentColor.TupleNotEqual(
                    "auto")))) != 0)
                {
                    HOperatorSet.SetColor(hv_WindowHandle, hv_CurrentColor);
                }
                else
                {
                    HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
                }
                hv_Row_COPY_INP_TMP = hv_R1 + (hv_MaxHeight * hv_Index);
                HOperatorSet.SetTposition(hv_WindowHandle, hv_Row_COPY_INP_TMP, hv_C1);
                HOperatorSet.WriteString(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
                    hv_Index));
            }
            //Reset changed window settings
            HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
            HOperatorSet.SetPart(hv_WindowHandle, hv_Row1Part, hv_Column1Part, hv_Row2Part,
                hv_Column2Part);

            return;
        }
        public void FitCicrle(HObject ho_Image, HTuple hv_WindowHandle, HTuple hv_CircleRow,
             HTuple hv_CircleColumn, HTuple hv_CircleRadius, HTuple hv_RectRow, HTuple hv_RectColumn,
             HTuple hv_RectPhi, HTuple hv_RectLength1, HTuple hv_RectLength2, HTuple hv_Transition,
             HTuple hv_Sigma, HTuple hv_Threshold, out HTuple hv_CenterRow, out HTuple hv_CenterColumn,
             out HTuple hv_CenterRaduis, out HTuple hv_StartPhi, out HTuple hv_EndPhi, out HTuple hv_Circularity)
        {




            // Local iconic variables 

            HObject ho_Rectangle, ho_Contour;

            // Local control variables 

            HTuple hv_Width = null, hv_Height = null, hv_sr = null;
            HTuple hv_sc = null, hv_lenth1 = null, hv_lenth2 = null;
            HTuple hv_val = null, hv_PointNum = null, hv_mr = null;
            HTuple hv_mc = null, hv_ag = null, hv_i = null, hv_lr = null;
            HTuple hv_lc = null, hv_j = null, hv_MeasureHandle = new HTuple();
            HTuple hv_RowEdge = new HTuple(), hv_ColumnEdge = new HTuple();
            HTuple hv_Amplitude = new HTuple(), hv_Distance = new HTuple();
            HTuple hv_RowEdgeCount = new HTuple(), hv_RowEdgeVal = new HTuple();
            HTuple hv_Indices = new HTuple(), hv_RowEdgeValMin = new HTuple();
            HTuple hv_rc = null, hv_zr = null, hv_zc = null, hv_PointOrder = null;
            HTuple hv_CircleColumn_COPY_INP_TMP = hv_CircleColumn.Clone();
            HTuple hv_CircleRow_COPY_INP_TMP = hv_CircleRow.Clone();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_Contour);
            hv_CenterRow = new HTuple();
            hv_CenterColumn = new HTuple();
            hv_CenterRaduis = new HTuple();
            hv_StartPhi = new HTuple();
            hv_EndPhi = new HTuple();
            hv_Circularity = new HTuple();
            try
            {
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetDraw(HDevWindowStack.GetActive(), "margin");
                }
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetColor(HDevWindowStack.GetActive(), "green");
                }
                HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                //
                //tuple_deg (RectPhi, Deg)
                //
                //gen_circle_contour_xld (ContCircle, CircleRow, CircleColumn, CircleRadius, 0, 6.28318/2, 'positive', 1)
                //
                hv_sr = hv_CircleRow_COPY_INP_TMP - hv_CircleRadius;
                hv_sc = hv_CircleColumn_COPY_INP_TMP.Clone();
                //
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetColor(HDevWindowStack.GetActive(), "red");
                }
                //
                hv_lenth1 = hv_CircleRadius / 2.5;
                hv_lenth2 = hv_CircleRadius / 8;
                //lenth1 := RectLength1
                //lenth2 := RectLength2
                hv_val = 5;
                hv_PointNum = (6.28318 * hv_CircleRadius) / ((hv_lenth2 + hv_val) * 2);
                //
                hv_mr = hv_sr.Clone();
                hv_mc = hv_sc.Clone();
                //
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle, hv_mr, hv_mc, (new HTuple(0 + 90)).TupleRad()
                    , hv_lenth1, hv_lenth2);
                //
                hv_ag = 10;
                //
                HOperatorSet.DispImage(ho_Image, 3600);
                //tuple_rad (ag, rd)
                //
                HTuple end_val30 = 360 / hv_ag;
                HTuple step_val30 = 1;
                for (hv_i = 0; hv_i.Continue(end_val30, step_val30); hv_i = hv_i.TupleAdd(step_val30))
                {
                    hv_mr = hv_sr + (hv_CircleRadius * (1 - ((((hv_ag.TupleRad()) * hv_i)).TupleCos()
                        )));
                    hv_mc = hv_sc - (hv_CircleRadius * ((((hv_ag.TupleRad()) * hv_i)).TupleSin()));
                    ho_Rectangle.Dispose();
                    HOperatorSet.GenRectangle2(out ho_Rectangle, hv_mr, hv_mc, (((hv_ag * hv_i) + 90)).TupleRad()
                        , hv_lenth1, hv_lenth2);
                }
                //
                //stop ()
                //
                hv_lr = new HTuple();
                hv_lc = new HTuple();
                //
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
                }
                hv_j = 0;
                if (HDevWindowStack.IsOpen())
                {
                    //dev_set_draw ('fill')
                }
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetLineWidth(HDevWindowStack.GetActive(), 1);
                }
                //
                HTuple end_val46 = 360 / hv_ag;
                HTuple step_val46 = 1;
                for (hv_i = 0; hv_i.Continue(end_val46, step_val46); hv_i = hv_i.TupleAdd(step_val46))
                {
                    if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.SetColor(HDevWindowStack.GetActive(), "green");
                    }
                    //
                    hv_mr = hv_sr + (hv_CircleRadius * (1 - ((((hv_ag.TupleRad()) * hv_i)).TupleCos()
                        )));
                    hv_mc = hv_sc - (hv_CircleRadius * ((((hv_ag.TupleRad()) * hv_i)).TupleSin()));
                    //
                    ho_Rectangle.Dispose();
                    HOperatorSet.GenRectangle2(out ho_Rectangle, hv_mr, hv_mc, (((hv_ag * hv_i) + 90)).TupleRad()
                        , hv_lenth1, hv_lenth2);
                    //
                    HOperatorSet.GenMeasureRectangle2(hv_mr, hv_mc, (((hv_ag * hv_i) + 90)).TupleRad()
                        , hv_lenth1, hv_lenth2, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandle);
                    //Transition = 'positive' :dark-to-light;  Transition = 'negative': light-to-dark
                    HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle, hv_Sigma, hv_Threshold,
                        hv_Transition, "all", out hv_RowEdge, out hv_ColumnEdge, out hv_Amplitude,
                        out hv_Distance);
                    //
                    if ((int)(new HTuple((new HTuple(hv_RowEdge.TupleLength())).TupleEqual(0))) != 0)
                    {
                        if (HDevWindowStack.IsOpen())
                        {
                            HOperatorSet.SetColor(HDevWindowStack.GetActive(), "red");
                        }
                        HOperatorSet.DispCross(hv_WindowHandle, hv_mr, hv_mc, 20, (new HTuple(45)).TupleRad()
                            );
                        //stop ()
                    }
                    else if ((int)(new HTuple((new HTuple(hv_RowEdge.TupleLength())).TupleEqual(
                        1))) != 0)
                    {
                        if (HDevWindowStack.IsOpen())
                        {
                            HOperatorSet.SetColor(HDevWindowStack.GetActive(), "green");
                        }
                        HOperatorSet.DispCross(hv_WindowHandle, hv_RowEdge, hv_ColumnEdge, 20,
                            (new HTuple(45)).TupleRad());
                        if (hv_lr == null)
                            hv_lr = new HTuple();
                        hv_lr[hv_j] = hv_RowEdge.TupleSelect(0);
                        if (hv_lc == null)
                            hv_lc = new HTuple();
                        hv_lc[hv_j] = hv_ColumnEdge.TupleSelect(0);
                        hv_j = hv_j + 1;
                    }
                    else if ((int)(new HTuple((new HTuple(hv_RowEdge.TupleLength())).TupleNotEqual(
                        1))) != 0)
                    {
                        //By position near DrawLine
                        hv_RowEdgeCount = new HTuple(hv_RowEdge.TupleLength());
                        hv_RowEdgeVal = hv_RowEdge - hv_mr;
                        HOperatorSet.TupleAbs(hv_RowEdgeVal, out hv_RowEdgeVal);
                        //tuple_sort_index (RowEdgeVal, Indices)
                        HOperatorSet.TupleMin(hv_RowEdgeVal, out hv_RowEdgeValMin);
                        HOperatorSet.TupleFind(hv_RowEdgeVal, hv_RowEdgeValMin, out hv_Indices);
                        if ((int)(new HTuple((new HTuple(hv_Indices.TupleLength())).TupleEqual(
                            1))) != 0)
                        {
                            if (hv_lr == null)
                                hv_lr = new HTuple();
                            hv_lr[hv_j] = hv_RowEdge.TupleSelect(hv_Indices);
                            if (hv_lc == null)
                                hv_lc = new HTuple();
                            hv_lc[hv_j] = hv_ColumnEdge.TupleSelect(hv_Indices);
                        }
                        else
                        {
                            if (hv_lr == null)
                                hv_lr = new HTuple();
                            hv_lr[hv_j] = hv_RowEdge.TupleSelect(hv_Indices.TupleSelect(0));
                            if (hv_lc == null)
                                hv_lc = new HTuple();
                            hv_lc[hv_j] = hv_ColumnEdge.TupleSelect(hv_Indices.TupleSelect(0));
                        }
                        hv_j = hv_j + 1;
                        if (HDevWindowStack.IsOpen())
                        {
                            HOperatorSet.SetColor(HDevWindowStack.GetActive(), "blue");
                        }
                        HOperatorSet.DispCross(hv_WindowHandle, hv_RowEdge.TupleSelect(hv_Indices),
                            hv_ColumnEdge.TupleSelect(hv_Indices), 20, (new HTuple(45)).TupleRad()
                            );
                        //
                        //By Threshold
                    }
                    HOperatorSet.CloseMeasure(hv_MeasureHandle);
                }
                //
                //stop ()
                //
                hv_rc = new HTuple();
                hv_zr = new HTuple();
                hv_zc = new HTuple();
                //
                hv_CircleRow_COPY_INP_TMP = hv_lr.Clone();
                hv_CircleColumn_COPY_INP_TMP = hv_lc.Clone();
                //
                //
                if ((int)(new HTuple((new HTuple(hv_CircleRow_COPY_INP_TMP.TupleLength())).TupleLess(
                    4))) != 0)
                {
                    disp_message(hv_WindowHandle, "FitLine Fail", "image", 10, 10, "black", "true");
                    ho_Rectangle.Dispose();
                    ho_Contour.Dispose();

                    return;
                }
                ho_Contour.Dispose();
                HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_CircleRow_COPY_INP_TMP,
                    hv_CircleColumn_COPY_INP_TMP);
                //
                HOperatorSet.FitCircleContourXld(ho_Contour, "algebraic", -1, 0, 0, 3, 2, out hv_CenterRow,
                    out hv_CenterColumn, out hv_CenterRaduis, out hv_StartPhi, out hv_EndPhi,
                    out hv_PointOrder);
                HOperatorSet.CircularityXld(ho_Contour, out hv_Circularity);
                //
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetColor(HDevWindowStack.GetActive(), "blue");
                }
                HOperatorSet.DispCircle(hv_WindowHandle, hv_CenterRow, hv_CenterColumn, hv_CenterRaduis);
                //
                ho_Rectangle.Dispose();
                ho_Contour.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_Rectangle.Dispose();
                ho_Contour.Dispose();

                throw HDevExpDefaultException;
            }
        }

        public override void draw()
        {

            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            //HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            //thv = thresholdValue.D;
            //HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\mianjixx", out mianjixx);
            //mjxx = mianjixx.D;
            //HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\mianjisx", out mianjisx);
            //mjsx = mianjisx.D;
            //disp_message(this.LWindowHandle, "请依次在右，左，上，下镜子中绘制检测区域", "window", 12, 12, "black", "true");

            HTuple hv_qxmj = new HTuple(), hv_qxcd = new HTuple(), hv_ydz = new HTuple(), hv_bydz = new HTuple(), hv_kddz = new HTuple(), hv_pzzz = new HTuple();



            disp_message(this.LWindowHandle, "请输入白色阈值（参考值100）,以回车键结束", "window", 10, 10, "black", "true");
            HOperatorSet.SetTposition(this.LWindowHandle, 80, 150);
            HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_qxmj);

            try
            {
                HOperatorSet.TupleNumber(hv_qxmj, out nu1);
                this.mj = nu1.D;
            }
            catch
            {
                this.mj = 100;
            }


            disp_message(this.LWindowHandle, "请输入灰度差（参考值40）,以回车键结束", "window", 45, 10, "black", "true");
            HOperatorSet.SetTposition(this.LWindowHandle, 160, 150);
            HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_qxcd);

            try
            {
                HOperatorSet.TupleNumber(hv_qxcd, out nu);
                this.cd = nu.D;
            }
            catch
            {
                this.cd = 40;
            }


            //disp_message(this.LWindowHandle, "请输入模糊值（参考值13）,以回车键结束", "window", 80, 10, "black", "true");
            //HOperatorSet.SetTposition(this.LWindowHandle, 240, 150);
            //HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_ydz);

            //try
            //{
            //    HOperatorSet.TupleNumber(hv_ydz, out nu2);
            //    this.yd = nu2.D;
            //}
            //catch
            //{
            //    this.yd = 13;
            //}

            //disp_message(this.LWindowHandle, "请输入边界值（参考值25）,以回车键结束", "window", 115, 10, "black", "true");
            //HOperatorSet.SetTposition(this.LWindowHandle, 320, 150);
            //HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_bydz);

            //try
            //{
            //    HOperatorSet.TupleNumber(hv_bydz, out nu3);
            //    this.bjz = nu3.D;
            //}
            //catch
            //{
            //    this.bjz = 25;
            //}

            //disp_message(this.LWindowHandle, "请输入凸性最小值（参考值0.1）,以回车键结束", "window", 150, 10, "black", "true");
            //HOperatorSet.SetTposition(this.LWindowHandle, 400, 150);
            //HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_kddz);

            //try
            //{
            //    HOperatorSet.TupleNumber(hv_kddz, out nu4);
            //    this.kdz = nu4.D;
            //}
            //catch
            //{
            //    this.kdz = 0.1;
            //}

            //disp_message(this.LWindowHandle, "请输入凸性最大值（参考值0.8）,以回车键结束", "window", 185, 10, "black", "true");
            //HOperatorSet.SetTposition(this.LWindowHandle, 480, 150);
            //HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_pzzz);

            //try
            //{
            //    HOperatorSet.TupleNumber(hv_pzzz, out nu5);
            //    this.pzz = nu5.D;
            //}
            //catch
            //{
            //    this.pzz = 0.8;
            //}









            




        }

        private void action()
        {

            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_Region, ho_Region1, ho_ConnectedRegions;
            HObject ho_SelectedRegions, ho_ObjectSelected = null, ho_ObjectSelected1 = null;
            HObject ho_Circle1 = null, ho_Circle2 = null;

            // Local control variables 

            HTuple hv_Area = null, hv_Row = null, hv_Column = null;
            HTuple hv_Number = null, hv_m = null, hv_r1 = null, hv_r2 = null;
            HTuple hv_Row1 = new HTuple(), hv_Column1 = new HTuple();
            HTuple hv_Radius1 = new HTuple(), hv_Row2 = new HTuple();
            HTuple hv_Column2 = new HTuple(), hv_Radius2 = new HTuple();
            HTuple hv_Transition = new HTuple(), hv_Sigma = new HTuple();
            HTuple hv_Threashold = new HTuple(), hv_CenterRowc1 = new HTuple();
            HTuple hv_CenterColumnc1 = new HTuple(), hv_CenterRaduisc1 = new HTuple();
            HTuple hv_StartPhic1 = new HTuple(), hv_EndPhic1 = new HTuple();
            HTuple hv_Circularityc1 = new HTuple(), hv_CenterRowc2 = new HTuple();
            HTuple hv_CenterColumnc2 = new HTuple(), hv_CenterRaduisc2 = new HTuple();
            HTuple hv_StartPhic2 = new HTuple(), hv_EndPhic2 = new HTuple();
            HTuple hv_Circularityc2 = new HTuple(), hv_Distance = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected1);
            HOperatorSet.GenEmptyObj(out ho_Circle1);
            HOperatorSet.GenEmptyObj(out ho_Circle2);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {
                ho_Region.Dispose();
                HOperatorSet.Threshold(Image, out ho_Region, this.mj, 255);
      HOperatorSet.AreaCenter(ho_Region, out hv_Area, out hv_Row, out hv_Column);
      ho_Region1.Dispose();
      HOperatorSet.GenRegionPoints(out ho_Region1, hv_Row, hv_Column);
      ho_ConnectedRegions.Dispose();
      HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
      ho_SelectedRegions.Dispose();
      HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, ((new HTuple("circularity")).TupleConcat(
          "area")).TupleConcat("outer_radius"), "and", ((new HTuple(0.8)).TupleConcat(
          500)).TupleConcat(50), ((new HTuple(1)).TupleConcat(9999999)).TupleConcat(
          800));
      HOperatorSet.CountObj(ho_SelectedRegions, out hv_Number);


      hv_m = 0;
      hv_r1 = 0;
      hv_r2 = 0;
      if ((int)(new HTuple(hv_Number.TupleEqual(2))) != 0)
      {
        ho_ObjectSelected.Dispose();
        HOperatorSet.SelectObj(ho_SelectedRegions, out ho_ObjectSelected, 1);
        HOperatorSet.SmallestCircle(ho_ObjectSelected, out hv_Row1, out hv_Column1, 
            out hv_Radius1);
        ho_ObjectSelected1.Dispose();
        HOperatorSet.SelectObj(ho_SelectedRegions, out ho_ObjectSelected1, 2);
        HOperatorSet.SmallestCircle(ho_ObjectSelected1, out hv_Row2, out hv_Column2, 
            out hv_Radius2);
        //Transition = 'positive' :dark-to-light;  Transition = 'negative': light-to-dark
        hv_Transition = "negative";
        hv_Sigma = 0.6;
        hv_Threashold = this.cd;


        FitCicrle(Image, 3600, hv_Row1, hv_Column1, hv_Radius1+5, hv_Row1, (hv_Column1+(hv_Radius1/2))+5, 
            0, (hv_Radius1/2)+10, 2, hv_Transition, hv_Sigma, hv_Threashold, out hv_CenterRowc1, 
            out hv_CenterColumnc1, out hv_CenterRaduisc1, out hv_StartPhic1, out hv_EndPhic1, 
            out hv_Circularityc1);
        ho_Circle1.Dispose();
        HOperatorSet.GenCircle(out ho_Circle1, hv_CenterRowc1, hv_CenterColumnc1, 
            hv_CenterRaduisc1);


        FitCicrle(Image, 3600, hv_Row2, hv_Column2, hv_Radius2+5, hv_Row2, (hv_Column2+(hv_Radius2/2))+5, 
            0, (hv_Radius2/2)+10, 2, hv_Transition, hv_Sigma, hv_Threashold, out hv_CenterRowc2, 
            out hv_CenterColumnc2, out hv_CenterRaduisc2, out hv_StartPhic2, out hv_EndPhic2, 
            out hv_Circularityc2);
        ho_Circle2.Dispose();
        HOperatorSet.GenCircle(out ho_Circle2, hv_CenterRowc2, hv_CenterColumnc2, 
            hv_CenterRaduisc2);
        HOperatorSet.DistancePp(hv_CenterRowc2, hv_CenterColumnc2, hv_CenterRowc1, 
            hv_CenterColumnc1, out hv_Distance);
        hv_m = hv_Distance.Clone();
        hv_r1 = hv_CenterRaduisc1.Clone();
        hv_r2 = hv_CenterRaduisc2.Clone();

        {
        HObject ExpTmpOutVar_0;
        HOperatorSet.Union2(ho_Region1, ho_Circle2, out ExpTmpOutVar_0);
        ho_Region1.Dispose();
        ho_Region1 = ExpTmpOutVar_0;
        }
        {
        HObject ExpTmpOutVar_0;
        HOperatorSet.Union2(ho_Region1, ho_Circle1, out ExpTmpOutVar_0);
        ho_Region1.Dispose();
        ho_Region1 = ExpTmpOutVar_0;
        }
      }
    
                //HOperatorSet.ClearShapeModel(hv_ModelID);
                HOperatorSet.Union1(ho_Region1, out RegionToDisp);

                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("孔心距");
                hv_result = hv_result.TupleConcat(hv_m.D * pixeldist);
                result = hv_result.Clone();
                ho_Region.Dispose();
                ho_Region1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_ObjectSelected.Dispose();
                ho_ObjectSelected1.Dispose();
                ho_Circle1.Dispose();
                ho_Circle2.Dispose();
                algorithm.Region.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("孔心距");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();
                ho_Region.Dispose();
                ho_Region1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_ObjectSelected.Dispose();
                ho_ObjectSelected1.Dispose();
                ho_Circle1.Dispose();
                ho_Circle2.Dispose();
                algorithm.Region.Dispose();

            }
            finally
            {
                ho_Region.Dispose();
                ho_Region1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_ObjectSelected.Dispose();
                ho_ObjectSelected1.Dispose();
                ho_Circle1.Dispose();
                ho_Circle2.Dispose();
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




