using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
    [Serializable]
    class dajing : ImageTools
    {
        #region ROI
        [NonSerialized]
        private HTuple centerRow = new HTuple();
        [NonSerialized]
        private HTuple centerColumn = new HTuple();
        [NonSerialized]
        private HTuple radius = new HTuple();
        [NonSerialized]
        private HTuple thresholdValue = new HTuple();
        [NonSerialized]
        private HTuple mianjisx = new HTuple();
        [NonSerialized]
        private HTuple mianjixx = new HTuple();
        public double DCenterRow { set; get; }
        public double DCenterColumn { set; get; }
        public double DRadius { set; get; }

        [NonSerialized]
        private HTuple nu = new HTuple();
        [NonSerialized]
        private HTuple nu1 = new HTuple();
        [NonSerialized]
        private HTuple nu2 = new HTuple();
        [NonSerialized]
        private HTuple nu3 = new HTuple();
        public double mj { set; get; }
        public double cd { set; get; }
        public double y1 { set; get; }
        public double y2{ set; get; }
        public double Dthv { set; get; }
        public double mjsx { set; get; }
        public double mjxx { set; get; }
        #endregion
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public dajing()
        {
            //RegionToDisp = Image;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public dajing(HObject Image, Algorithm al)
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
        public override void draw()
        {

            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            HOperatorSet.DrawCircle(this.LWindowHandle, out centerRow, out centerColumn, out radius);
            this.DCenterRow = centerRow.D;
            this.DCenterColumn = centerColumn.D;
            this.DRadius = radius.D;
            HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            Dthv = thresholdValue.D;
            HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\mianjixx", out mianjixx);
            mjxx = mianjixx.D;
            HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\mianjisx", out mianjisx);
            mjsx = mianjisx.D;
            HTuple hv_qxmj = new HTuple(), hv_qxcd = new HTuple(), hv_ydzd = new HTuple(), hv_ydzx = new HTuple();
            int xx = 0;
            HTuple nu1 = new HTuple();
            while (xx == 0)
            {
                disp_message(this.LWindowHandle, "请输入最小尺寸（参考值9.8）,以回车键结束", "window", 10, 10, "black", "true");
                HOperatorSet.SetTposition(this.LWindowHandle, 60, 150);
                HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_qxmj);

                try
                {
                    HOperatorSet.TupleNumber(hv_qxmj, out nu1);
                    xx = 1;
                    this.mj = nu1.D;
                }
                catch
                {
                }
            }
            //
            int xy = 0;
            HTuple nu = new HTuple();
            while (xy == 0)
            {
                disp_message(this.LWindowHandle, "请输入最大尺寸（参考值15）,以回车键结束", "window", 30, 10, "black", "true");
                HOperatorSet.SetTposition(this.LWindowHandle, 100, 150);
                HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_qxcd);

                try
                {
                    HOperatorSet.TupleNumber(hv_qxcd, out nu);
                    xy = 1;
                    this.cd = nu.D;
                }
                catch
                {
                }
            }
            int xz = 0;
            HTuple nu2 = new HTuple();
            while (xz == 0)
            {
                disp_message(this.LWindowHandle, "请输入圆度最小值（参考值0.985）,以回车键结束", "window", 50, 10, "black", "true");
                HOperatorSet.SetTposition(this.LWindowHandle, 140, 150);
                HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_ydzx);

                try
                {
                    HOperatorSet.TupleNumber(hv_ydzx, out nu2);
                    xz = 1;
                    this.y1 = nu2.D;
                }
                catch
                {
                }
            }
            int xz1 = 0;
            HTuple nu3 = new HTuple();
            while (xz1 == 0)
            {
                disp_message(this.LWindowHandle, "请输入圆度最大值（参考值1）,以回车键结束", "window", 70, 10, "black", "true");
                HOperatorSet.SetTposition(this.LWindowHandle, 180, 150);
                HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_ydzd);

                try
                {
                    HOperatorSet.TupleNumber(hv_ydzd, out nu3);
                    xz1 = 1;
                    this.y2 = nu3.D;
                }
                catch
                {
                }
            }
        }
      
        private void action()
        {
            // Local iconic variables 

            HObject ho_Circle, ho_Region,ho_ImageReduced, ho_ConnectedRegions, ho_RegionDilation, ho_RegionErosion, ho_RegionDilation1;
            HObject ho_SelectedRegions, ho_RegionIntersection = null;
            HObject ho_RegionFillUp = null, ho_RegionTrans = null, ho_Circle1 = null;

            // Local control variables 

            HTuple hv_ir = null, hv_yy = null, hv_Distance=null, hv_Sigma=null, hv_Roundness=null, hv_Sides=null;
            HTuple hv_Number = null, hv_Row1 = new HTuple(), hv_Column1 = new HTuple();
            HTuple hv_Radius1 = new HTuple(), hv_Circularity = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_RegionErosion);
            HOperatorSet.GenEmptyObj(out ho_RegionDilation);
            HOperatorSet.GenEmptyObj(out ho_RegionDilation1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans);
            HOperatorSet.GenEmptyObj(out ho_Circle1);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {
                ho_Circle.Dispose();
                HOperatorSet.GenCircle(out ho_Circle, DCenterRow, DCenterColumn, DRadius);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Circle, out ho_ImageReduced);
                ho_Region.Dispose();

                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, Dthv, 255);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                    "and", mjxx, mjsx);
                 hv_ir = -1;
                 hv_yy = -1;
                HOperatorSet.CountObj(ho_SelectedRegions, out hv_Number);
                if ((int)(new HTuple(hv_Number.TupleEqual(1))) != 0)
                {
                    //ho_RegionIntersection.Dispose();
                    //HOperatorSet.Intersection(ho_Circle, ho_SelectedRegions, out ho_RegionIntersection
                    //    );
                    ho_RegionFillUp.Dispose();
                    HOperatorSet.FillUp(ho_SelectedRegions, out ho_RegionFillUp);
                    ho_RegionTrans.Dispose();
                    HOperatorSet.ShapeTrans(ho_RegionFillUp, out ho_RegionTrans, "inner_circle");
                    HOperatorSet.SmallestCircle(ho_RegionTrans, out hv_Row1, out hv_Column1,
                        out hv_Radius1);
                    ho_RegionErosion.Dispose();
                    ho_RegionDilation.Dispose();
                    ho_RegionDilation1.Dispose();
                    HOperatorSet.Circularity(ho_RegionFillUp, out hv_Circularity);
                    HOperatorSet.DilationCircle(ho_RegionFillUp, out ho_RegionDilation1, 15);
                    HOperatorSet.ErosionCircle(ho_RegionDilation1, out ho_RegionErosion, 40);
                    HOperatorSet.DilationCircle(ho_RegionErosion, out ho_RegionDilation, 25);
                    HOperatorSet.Roundness(ho_RegionDilation, out hv_Distance, out hv_Sigma, out hv_Roundness, out hv_Sides);

                    //shape_trans (RegionFillUp, RegionTrans1, 'outer_circle')
                    //*     smallest_circle (RegionTrans1, Row, Column, Radius)

                    //*     gen_region_line (RegionLines, Row, Column, Row1, Column1)
                    //*     distance_pp (Row1, Column1, Row, Column, Distance1)
                    //*     m := Radius-Radius1
                    //*     n := Distance1
                    ho_Circle1.Dispose();
                    HOperatorSet.GenCircle(out ho_Circle1, hv_Row1, hv_Column1, hv_Radius1);
                    if (hv_Radius1 * 2 * pixeldist >= this.mj && hv_Radius1 * 2 * pixeldist <= this.cd)
                    {
                        hv_ir = hv_Radius1 * 2 * pixeldist;
                    }
                    else
                    {
                        hv_ir = -1;
                    }
                    if (hv_Roundness >= this.y1 && hv_Roundness <= this.y2)
                    {
                        hv_yy = hv_Roundness;
                    }
                    else
                    {
                        hv_yy = -1;
                    }

                }

                HOperatorSet.Union1(ho_Circle1, out RegionToDisp);

                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("内圆直径");
                hv_result = hv_result.TupleConcat(hv_ir);
                hv_result = hv_result.TupleConcat("圆度");
                hv_result = hv_result.TupleConcat(hv_yy);
                result = hv_result.Clone();
                ho_Circle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionIntersection.Dispose();
                ho_RegionFillUp.Dispose();
                ho_RegionTrans.Dispose();
                ho_Circle1.Dispose();
                ho_RegionErosion.Dispose();
                ho_RegionDilation.Dispose();
                ho_RegionDilation1.Dispose();
                algorithm.Region.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("内圆直径");
                hv_result = hv_result.TupleConcat(-1);
                hv_result = hv_result.TupleConcat("圆度");
                hv_result = hv_result.TupleConcat(-1);
                result = hv_result.Clone();

                ho_Circle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionIntersection.Dispose();
                ho_RegionFillUp.Dispose();
                ho_RegionTrans.Dispose();
                ho_Circle1.Dispose();
                ho_RegionErosion.Dispose();
                ho_RegionDilation.Dispose();
                ho_RegionDilation1.Dispose();
                algorithm.Region.Dispose();
                

            }
            finally
            {
                ho_Circle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionIntersection.Dispose();
                ho_RegionFillUp.Dispose();
                ho_RegionTrans.Dispose();
                ho_Circle1.Dispose();
                ho_RegionErosion.Dispose();
                ho_RegionDilation.Dispose();
                ho_RegionDilation1.Dispose();
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



