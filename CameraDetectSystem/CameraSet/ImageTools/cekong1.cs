using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
    [Serializable]
    class cekong1 : ImageTools
    {
        #region ROI
        [NonSerialized]
        private HTuple Rowy = new HTuple();
        [NonSerialized]
        private HTuple Columny = new HTuple();
        [NonSerialized]
        private HTuple Phiy = new HTuple();
        [NonSerialized]
        private HTuple Length1y = new HTuple();
        [NonSerialized]
        private HTuple Length2y = new HTuple();


        public double DRowy { set; get; }
        public double DColumny { set; get; }
        public double DPhiy { set; get; }
        public double DLength1y { set; get; }
        public double DLength2y { set; get; }

        [NonSerialized]
        private HTuple Rowz = new HTuple();
        [NonSerialized]
        private HTuple Columnz = new HTuple();
        [NonSerialized]
        private HTuple Phiz = new HTuple();
        [NonSerialized]
        private HTuple Length1z = new HTuple();
        [NonSerialized]
        private HTuple Length2z = new HTuple();


        public double DRowz { set; get; }
        public double DColumnz { set; get; }
        public double DPhiz { set; get; }
        public double DLength1z { set; get; }
        public double DLength2z { set; get; }


        [NonSerialized]
        private HTuple Rows = new HTuple();
        [NonSerialized]
        private HTuple Columns = new HTuple();
        [NonSerialized]
        private HTuple Phis = new HTuple();
        [NonSerialized]
        private HTuple Length1s = new HTuple();
        [NonSerialized]
        private HTuple Length2s = new HTuple();


        public double DRows { set; get; }
        public double DColumns { set; get; }
        public double DPhis { set; get; }
        public double DLength1s { set; get; }
        public double DLength2s { set; get; }


        [NonSerialized]
        private HTuple Rowx = new HTuple();
        [NonSerialized]
        private HTuple Columnx = new HTuple();
        [NonSerialized]
        private HTuple Phix = new HTuple();
        [NonSerialized]
        private HTuple Length1x = new HTuple();
        [NonSerialized]
        private HTuple Length2x = new HTuple();


        public double DRowx { set; get; }
        public double DColumnx { set; get; }
        public double DPhix { set; get; }
        public double DLength1x { set; get; }
        public double DLength2x { set; get; }


        //[NonSerialized]
        //private HTuple nu = new HTuple();
        //[NonSerialized]
        //private HTuple nu1 = new HTuple();
        //[NonSerialized]
        //private HTuple nu2 = new HTuple();
        //[NonSerialized]
        //private HTuple nu3 = new HTuple();


        //public double mj { set; get; }
        //public double cd { set; get; }
        //public double jl { set; get; }
        //public double yd { set; get; }

        #endregion
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public cekong1()
        {
            //RegionToDisp = Image;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public cekong1(HObject Image, Algorithm al)
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
       // public void disp_message(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem,
       //HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
       // {



       //     // Local iconic variables 

       //     // Local control variables 

       //     HTuple hv_Red = null, hv_Green = null, hv_Blue = null;
       //     HTuple hv_Row1Part = null, hv_Column1Part = null, hv_Row2Part = null;
       //     HTuple hv_Column2Part = null, hv_RowWin = null, hv_ColumnWin = null;
       //     HTuple hv_WidthWin = null, hv_HeightWin = null, hv_MaxAscent = null;
       //     HTuple hv_MaxDescent = null, hv_MaxWidth = null, hv_MaxHeight = null;
       //     HTuple hv_R1 = new HTuple(), hv_C1 = new HTuple(), hv_FactorRow = new HTuple();
       //     HTuple hv_FactorColumn = new HTuple(), hv_UseShadow = null;
       //     HTuple hv_ShadowColor = null, hv_Exception = new HTuple();
       //     HTuple hv_Width = new HTuple(), hv_Index = new HTuple();
       //     HTuple hv_Ascent = new HTuple(), hv_Descent = new HTuple();
       //     HTuple hv_W = new HTuple(), hv_H = new HTuple(), hv_FrameHeight = new HTuple();
       //     HTuple hv_FrameWidth = new HTuple(), hv_R2 = new HTuple();
       //     HTuple hv_C2 = new HTuple(), hv_DrawMode = new HTuple();
       //     HTuple hv_CurrentColor = new HTuple();
       //     HTuple hv_Box_COPY_INP_TMP = hv_Box.Clone();
       //     HTuple hv_Color_COPY_INP_TMP = hv_Color.Clone();
       //     HTuple hv_Column_COPY_INP_TMP = hv_Column.Clone();
       //     HTuple hv_Row_COPY_INP_TMP = hv_Row.Clone();
       //     HTuple hv_String_COPY_INP_TMP = hv_String.Clone();

       //     // Initialize local and output iconic variables 
       //     //This procedure displays text in a graphics window.
       //     //
       //     //Input parameters:
       //     //WindowHandle: The WindowHandle of the graphics window, where
       //     //   the message should be displayed
       //     //String: A tuple of strings containing the text message to be displayed
       //     //CoordSystem: If set to 'window', the text position is given
       //     //   with respect to the window coordinate system.
       //     //   If set to 'image', image coordinates are used.
       //     //   (This may be useful in zoomed images.)
       //     //Row: The row coordinate of the desired text position
       //     //   If set to -1, a default value of 12 is used.
       //     //Column: The column coordinate of the desired text position
       //     //   If set to -1, a default value of 12 is used.
       //     //Color: defines the color of the text as string.
       //     //   If set to [], '' or 'auto' the currently set color is used.
       //     //   If a tuple of strings is passed, the colors are used cyclically
       //     //   for each new textline.
       //     //Box: If Box[0] is set to 'true', the text is written within an orange box.
       //     //     If set to' false', no box is displayed.
       //     //     If set to a color string (e.g. 'white', '#FF00CC', etc.),
       //     //       the text is written in a box of that color.
       //     //     An optional second value for Box (Box[1]) controls if a shadow is displayed:
       //     //       'true' -> display a shadow in a default color
       //     //       'false' -> display no shadow (same as if no second value is given)
       //     //       otherwise -> use given string as color string for the shadow color
       //     //
       //     //Prepare window
       //     HOperatorSet.GetRgb(hv_WindowHandle, out hv_Red, out hv_Green, out hv_Blue);
       //     HOperatorSet.GetPart(hv_WindowHandle, out hv_Row1Part, out hv_Column1Part, out hv_Row2Part,
       //         out hv_Column2Part);
       //     HOperatorSet.GetWindowExtents(hv_WindowHandle, out hv_RowWin, out hv_ColumnWin,
       //         out hv_WidthWin, out hv_HeightWin);
       //     HOperatorSet.SetPart(hv_WindowHandle, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
       //     //
       //     //default settings
       //     if ((int)(new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
       //     {
       //         hv_Row_COPY_INP_TMP = 12;
       //     }
       //     if ((int)(new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
       //     {
       //         hv_Column_COPY_INP_TMP = 12;
       //     }
       //     if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
       //     {
       //         hv_Color_COPY_INP_TMP = "";
       //     }
       //     //
       //     hv_String_COPY_INP_TMP = ((("" + hv_String_COPY_INP_TMP) + "")).TupleSplit("\n");
       //     //
       //     //Estimate extentions of text depending on font size.
       //     HOperatorSet.GetFontExtents(hv_WindowHandle, out hv_MaxAscent, out hv_MaxDescent,
       //         out hv_MaxWidth, out hv_MaxHeight);
       //     if ((int)(new HTuple(hv_CoordSystem.TupleEqual("window"))) != 0)
       //     {
       //         hv_R1 = hv_Row_COPY_INP_TMP.Clone();
       //         hv_C1 = hv_Column_COPY_INP_TMP.Clone();
       //     }
       //     else
       //     {
       //         //Transform image to window coordinates
       //         hv_FactorRow = (1.0 * hv_HeightWin) / ((hv_Row2Part - hv_Row1Part) + 1);
       //         hv_FactorColumn = (1.0 * hv_WidthWin) / ((hv_Column2Part - hv_Column1Part) + 1);
       //         hv_R1 = ((hv_Row_COPY_INP_TMP - hv_Row1Part) + 0.5) * hv_FactorRow;
       //         hv_C1 = ((hv_Column_COPY_INP_TMP - hv_Column1Part) + 0.5) * hv_FactorColumn;
       //     }
       //     //
       //     //Display text box depending on text size
       //     hv_UseShadow = 1;
       //     hv_ShadowColor = "gray";
       //     if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleEqual("true"))) != 0)
       //     {
       //         if (hv_Box_COPY_INP_TMP == null)
       //             hv_Box_COPY_INP_TMP = new HTuple();
       //         hv_Box_COPY_INP_TMP[0] = "#fce9d4";
       //         hv_ShadowColor = "#f28d26";
       //     }
       //     if ((int)(new HTuple((new HTuple(hv_Box_COPY_INP_TMP.TupleLength())).TupleGreater(
       //         1))) != 0)
       //     {
       //         if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual("true"))) != 0)
       //         {
       //             //Use default ShadowColor set above
       //         }
       //         else if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual(
       //             "false"))) != 0)
       //         {
       //             hv_UseShadow = 0;
       //         }
       //         else
       //         {
       //             hv_ShadowColor = hv_Box_COPY_INP_TMP[1];
       //             //Valid color?
       //             try
       //             {
       //                 HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(
       //                     1));
       //             }
       //             // catch (Exception) 
       //             catch (HalconException HDevExpDefaultException1)
       //             {
       //                 HDevExpDefaultException1.ToHTuple(out hv_Exception);
       //                 hv_Exception = "Wrong value of control parameter Box[1] (must be a 'true', 'false', or a valid color string)";
       //                 throw new HalconException(hv_Exception);
       //             }
       //         }
       //     }
       //     if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleNotEqual("false"))) != 0)
       //     {
       //         //Valid color?
       //         try
       //         {
       //             HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(0));
       //         }
       //         // catch (Exception) 
       //         catch (HalconException HDevExpDefaultException1)
       //         {
       //             HDevExpDefaultException1.ToHTuple(out hv_Exception);
       //             hv_Exception = "Wrong value of control parameter Box[0] (must be a 'true', 'false', or a valid color string)";
       //             throw new HalconException(hv_Exception);
       //         }
       //         //Calculate box extents
       //         hv_String_COPY_INP_TMP = (" " + hv_String_COPY_INP_TMP) + " ";
       //         hv_Width = new HTuple();
       //         for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
       //             )) - 1); hv_Index = (int)hv_Index + 1)
       //         {
       //             HOperatorSet.GetStringExtents(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
       //                 hv_Index), out hv_Ascent, out hv_Descent, out hv_W, out hv_H);
       //             hv_Width = hv_Width.TupleConcat(hv_W);
       //         }
       //         hv_FrameHeight = hv_MaxHeight * (new HTuple(hv_String_COPY_INP_TMP.TupleLength()
       //             ));
       //         hv_FrameWidth = (((new HTuple(0)).TupleConcat(hv_Width))).TupleMax();
       //         hv_R2 = hv_R1 + hv_FrameHeight;
       //         hv_C2 = hv_C1 + hv_FrameWidth;
       //         //Display rectangles
       //         HOperatorSet.GetDraw(hv_WindowHandle, out hv_DrawMode);
       //         HOperatorSet.SetDraw(hv_WindowHandle, "fill");
       //         //Set shadow color
       //         HOperatorSet.SetColor(hv_WindowHandle, hv_ShadowColor);
       //         if ((int)(hv_UseShadow) != 0)
       //         {
       //             HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1 + 1, hv_C1 + 1, hv_R2 + 1, hv_C2 + 1);
       //         }
       //         //Set box color
       //         HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(0));
       //         HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1, hv_C1, hv_R2, hv_C2);
       //         HOperatorSet.SetDraw(hv_WindowHandle, hv_DrawMode);
       //     }
       //     //Write text.
       //     for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
       //         )) - 1); hv_Index = (int)hv_Index + 1)
       //     {
       //         hv_CurrentColor = hv_Color_COPY_INP_TMP.TupleSelect(hv_Index % (new HTuple(hv_Color_COPY_INP_TMP.TupleLength()
       //             )));
       //         if ((int)((new HTuple(hv_CurrentColor.TupleNotEqual(""))).TupleAnd(new HTuple(hv_CurrentColor.TupleNotEqual(
       //             "auto")))) != 0)
       //         {
       //             HOperatorSet.SetColor(hv_WindowHandle, hv_CurrentColor);
       //         }
       //         else
       //         {
       //             HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
       //         }
       //         hv_Row_COPY_INP_TMP = hv_R1 + (hv_MaxHeight * hv_Index);
       //         HOperatorSet.SetTposition(hv_WindowHandle, hv_Row_COPY_INP_TMP, hv_C1);
       //         HOperatorSet.WriteString(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
       //             hv_Index));
       //     }
       //     //Reset changed window settings
       //     HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
       //     HOperatorSet.SetPart(hv_WindowHandle, hv_Row1Part, hv_Column1Part, hv_Row2Part,
       //         hv_Column2Part);

       //     return;
       // }
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
            //HOperatorSet.GenRectangle1(out ho_Rectangle, DRow1m, DCol1m, DRow2m, DCol2m);
            //ho_Rectangle.Dispose();
            //HTuple hv_qxmj = new HTuple(), hv_qxcd = new HTuple(), hv_ydz = new HTuple(), hv_ydjl = new HTuple();


            ////
            //int xx = 0;
            //HTuple nu1 = new HTuple();
            //while (xx == 0)
            //{
            //    disp_message(this.LWindowHandle, "请输入最小面积（参考值75）,以回车键结束", "window", 10, 10, "black", "true");
            //    HOperatorSet.SetTposition(this.LWindowHandle, 120, 150);
            //    HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_qxmj);

            //    try
            //    {
            //        HOperatorSet.TupleNumber(hv_qxmj, out nu1);
            //        xx = 1;
            //        this.mj = nu1.D;
            //    }
            //    catch
            //    {
            //    }
            //    //
            //    int xy = 0;
            //    HTuple nu = new HTuple();
            //    while (xy == 0)
            //    {
            //        disp_message(this.LWindowHandle, "请输入最小半径（参考值6）,以回车键结束", "window", 50, 10, "black", "true");
            //        HOperatorSet.SetTposition(this.LWindowHandle, 260, 150);
            //        HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_qxcd);

            //        try
            //        {
            //            HOperatorSet.TupleNumber(hv_qxcd, out nu);
            //            xy = 1;
            //            this.cd = nu.D;
            //        }
            //        catch
            //        {
            //        }
            //    }
            //    int xz = 0;
            //    HTuple nu2 = new HTuple();
            //    while (xz == 0)
            //    {
            //        disp_message(this.LWindowHandle, "请输入分割值（参考值128）,以回车键结束", "window", 90, 10, "black", "true");
            //        HOperatorSet.SetTposition(this.LWindowHandle, 400, 150);
            //        HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_ydz);

            //        try
            //        {
            //            HOperatorSet.TupleNumber(hv_ydz, out nu2);
            //            xz = 1;
            //            this.yd = nu2.D;
            //        }
            //        catch
            //        {
            //        }
            //    }
            //    int xz1 = 0;
            //    HTuple nu3 = new HTuple();
            //    while (xz1 == 0)
            //    {
            //        disp_message(this.LWindowHandle, "请输入最小距离（参考值55）,以回车键结束", "window", 130, 10, "black", "true");
            //        HOperatorSet.SetTposition(this.LWindowHandle, 540, 150);
            //        HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_ydjl);

            //        try
            //        {
            //            HOperatorSet.TupleNumber(hv_ydjl, out nu3);
            //            xz1 = 1;
            //            this.jl = nu3.D;
            //        }
            //        catch
            //        {
            //        }
            //    }
            //    disp_message(this.LWindowHandle, "请依次在右，左，上，下镜子中绘制检测区域", "window", 12, 12, "black", "true");

                HTuple Rowy = null, Columny = null, Phiy = null, Length1y = null, Length2y = null;
                HObject ho_Rectangley;
                HOperatorSet.GenEmptyObj(out ho_Rectangley);
                HOperatorSet.DrawRectangle2(this.LWindowHandle, out Rowy, out Columny,
        out Phiy, out Length1y, out Length2y);
                this.DRowy = Rowy.D;
                this.DColumny = Columny.D;
                this.DPhiy = Phiy.D;
                this.DLength1y = Length1y.D;
                this.DLength2y = Length2y.D;
                HOperatorSet.GenRectangle2(out ho_Rectangley, DRowy, DColumny, DPhiy, DLength1y, DLength2y);
                ho_Rectangley.Dispose();


                HTuple Rowz = null, Columnz = null, Phiz = null, Length1z = null, Length2z = null;
                HObject ho_Rectanglez;
                HOperatorSet.GenEmptyObj(out ho_Rectanglez);
                HOperatorSet.DrawRectangle2(this.LWindowHandle, out Rowz, out Columnz,
        out Phiz, out Length1z, out Length2z);
                this.DRowz = Rowz.D;
                this.DColumnz = Columnz.D;
                this.DPhiz = Phiz.D;
                this.DLength1z = Length1z.D;
                this.DLength2z = Length2z.D;
                HOperatorSet.GenRectangle2(out ho_Rectanglez, DRowz, DColumnz, DPhiz, DLength1z, DLength2z);
                ho_Rectanglez.Dispose();


                HTuple Rows = null, Columns = null, Phis = null, Length1s = null, Length2s = null;
                HObject ho_Rectangles;
                HOperatorSet.GenEmptyObj(out ho_Rectangles);
                HOperatorSet.DrawRectangle2(this.LWindowHandle, out Rows, out Columns,
        out Phis, out Length1s, out Length2s);
                this.DRows = Rows.D;
                this.DColumns = Columns.D;
                this.DPhis = Phis.D;
                this.DLength1s = Length1s.D;
                this.DLength2s = Length2s.D;
                HOperatorSet.GenRectangle2(out ho_Rectangles, DRows, DColumns, DPhis, DLength1s, DLength2s);
                ho_Rectangles.Dispose();


                HTuple Rowx = null, Columnx = null, Phix = null, Length1x = null, Length2x = null;
                HObject ho_Rectanglex;
                HOperatorSet.GenEmptyObj(out ho_Rectanglex);
                HOperatorSet.DrawRectangle2(this.LWindowHandle, out Rowx, out Columnx,
        out Phix, out Length1x, out Length2x);
                this.DRowx = Rowx.D;
                this.DColumnx = Columnx.D;
                this.DPhix = Phix.D;
                this.DLength1x = Length1x.D;
                this.DLength2x = Length2x.D;
                HOperatorSet.GenRectangle2(out ho_Rectanglex, DRowx, DColumnx, DPhix, DLength1x, DLength2x);
                ho_Rectanglex.Dispose();





            //}
        }

        private void action()
        {
            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_Regionzx, ho_Rectangley;
            HObject ho_ImageReducedy, ho_Regiony, ho_ConnectedRegionsy;
            HObject ho_SelectedRegionsy, ho_RegionFillUpy, ho_RegionDifferencey;
            HObject ho_ConnectedRegionsy1, ho_SelectedRegionsyy, ho_Circleyy1 = null;
            HObject ho_Rectanglez, ho_ImageReducedz, ho_Regionz, ho_ConnectedRegionsz;
            HObject ho_SelectedRegionsz, ho_RegionFillUpz, ho_RegionDifferencez;
            HObject ho_ConnectedRegionsz1, ho_SelectedRegionszz, ho_Circlezz1 = null;
            HObject ho_Rectangles, ho_ImageReduceds, ho_Regions, ho_ConnectedRegionss;
            HObject ho_SelectedRegionss, ho_RegionFillUps, ho_RegionDifferences;
            HObject ho_ConnectedRegionss1, ho_SelectedRegionsss, ho_Circless1 = null;
            HObject ho_Rectanglex, ho_ImageReducedx, ho_Regionx, ho_ConnectedRegionsx;
            HObject ho_SelectedRegionsx, ho_RegionFillUpx, ho_RegionDifferencex;
            HObject ho_ConnectedRegionsx1, ho_SelectedRegionsxx, ho_Circlexx1 = null;
            HObject ho_ConnectedRegions, ho_SelectedRegions;

            // Local control variables 

            HTuple hv_Areayy = null, hv_Rowyy = null;
            HTuple hv_Columnyy = null, hv_Rowyy1 = new HTuple(), hv_Columnyy1 = new HTuple();
            HTuple hv_Radiusyy1 = new HTuple();
            HTuple hv_Areazz = null, hv_Rowzz = null, hv_Columnzz = null;
            HTuple hv_Rowzz1 = new HTuple(), hv_Columnzz1 = new HTuple();
            HTuple hv_Radiuszz1 = new HTuple();
            HTuple hv_Areass = null, hv_Rowss = null, hv_Columnss = null;
            HTuple hv_Rowss1 = new HTuple(), hv_Columnss1 = new HTuple();
            HTuple hv_Radiusss1 = new HTuple();
            HTuple hv_Areaxx = null, hv_Rowxx = null, hv_Columnxx = null;
            HTuple hv_Rowxx1 = new HTuple(), hv_Columnxx1 = new HTuple();
            HTuple hv_Radiusxx1 = new HTuple(), hv_n = null, hv_Number = null;
            HTuple hv_Area = null, hv_Row = null, hv_Column = null;
            HTuple hv_Distance = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Regionzx);
            HOperatorSet.GenEmptyObj(out ho_Rectangley);
            HOperatorSet.GenEmptyObj(out ho_ImageReducedy);
            HOperatorSet.GenEmptyObj(out ho_Regiony);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegionsy);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsy);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUpy);
            HOperatorSet.GenEmptyObj(out ho_RegionDifferencey);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegionsy1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsyy);
            HOperatorSet.GenEmptyObj(out ho_Circleyy1);
            HOperatorSet.GenEmptyObj(out ho_Rectanglez);
            HOperatorSet.GenEmptyObj(out ho_ImageReducedz);
            HOperatorSet.GenEmptyObj(out ho_Regionz);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegionsz);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsz);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUpz);
            HOperatorSet.GenEmptyObj(out ho_RegionDifferencez);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegionsz1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionszz);
            HOperatorSet.GenEmptyObj(out ho_Circlezz1);
            HOperatorSet.GenEmptyObj(out ho_Rectangles);
            HOperatorSet.GenEmptyObj(out ho_ImageReduceds);
            HOperatorSet.GenEmptyObj(out ho_Regions);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegionss);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionss);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUps);
            HOperatorSet.GenEmptyObj(out ho_RegionDifferences);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegionss1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsss);
            HOperatorSet.GenEmptyObj(out ho_Circless1);
            HOperatorSet.GenEmptyObj(out ho_Rectanglex);
            HOperatorSet.GenEmptyObj(out ho_ImageReducedx);
            HOperatorSet.GenEmptyObj(out ho_Regionx);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegionsx);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsx);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUpx);
            HOperatorSet.GenEmptyObj(out ho_RegionDifferencex);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegionsx1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsxx);
            HOperatorSet.GenEmptyObj(out ho_Circlexx1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {
                ho_Rectangley.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangley, DRowy, DColumny, DPhiy, DLength1y, DLength2y);
                ho_Regionzx.Dispose();
                HOperatorSet.GenRegionPoints(out ho_Regionzx, DRowy, DColumny);
                ho_ImageReducedy.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Rectangley, out ho_ImageReducedy);
                ho_Regiony.Dispose();
                HOperatorSet.Threshold(ho_ImageReducedy, out ho_Regiony, 128, 255);
                ho_ConnectedRegionsy.Dispose();
                HOperatorSet.Connection(ho_Regiony, out ho_ConnectedRegionsy);
                ho_SelectedRegionsy.Dispose();
                HOperatorSet.SelectShapeStd(ho_ConnectedRegionsy, out ho_SelectedRegionsy, "max_area",
                    70);
                ho_RegionFillUpy.Dispose();
                HOperatorSet.FillUp(ho_SelectedRegionsy, out ho_RegionFillUpy);
                ho_RegionDifferencey.Dispose();
                HOperatorSet.Difference(ho_RegionFillUpy, ho_SelectedRegionsy, out ho_RegionDifferencey
                    );
                ho_ConnectedRegionsy1.Dispose();
                HOperatorSet.Connection(ho_RegionDifferencey, out ho_ConnectedRegionsy1);
                ho_SelectedRegionsyy.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegionsy1, out ho_SelectedRegionsyy, (new HTuple("outer_radius")).TupleConcat(
                    "area"), "and", (new HTuple(6.5).TupleConcat(75)), (new HTuple(16)).TupleConcat(
                    400));
                HOperatorSet.AreaCenter(ho_SelectedRegionsyy, out hv_Areayy, out hv_Rowyy, out hv_Columnyy);
                if ((int)(new HTuple((new HTuple(hv_Areayy.TupleLength())).TupleEqual(1))) != 0)
                {
                    HOperatorSet.SmallestCircle(ho_SelectedRegionsyy, out hv_Rowyy1, out hv_Columnyy1,
                        out hv_Radiusyy1);
                    ho_Circleyy1.Dispose();
                    HOperatorSet.GenCircle(out ho_Circleyy1, hv_Rowyy1, hv_Columnyy1, hv_Radiusyy1);
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.Union2(ho_Regionzx, ho_Circleyy1, out ExpTmpOutVar_0);
                        ho_Regionzx.Dispose();
                        ho_Regionzx = ExpTmpOutVar_0;
                    }
                }
                //HDevelopStop();
                ////zuo
                //HOperatorSet.DrawRectangle2(hv_ExpDefaultWinHandle, out hv_Rowz, out hv_Columnz,
                //    out hv_Phiz, out hv_Length1z, out hv_Length2z);
                ho_Rectanglez.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectanglez, DRowz, DColumnz, DPhiz, DLength1z, DLength2z);
                ho_ImageReducedz.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Rectanglez, out ho_ImageReducedz);
                ho_Regionz.Dispose();
                HOperatorSet.Threshold(ho_ImageReducedz, out ho_Regionz, 128, 255);
                ho_ConnectedRegionsz.Dispose();
                HOperatorSet.Connection(ho_Regionz, out ho_ConnectedRegionsz);
                ho_SelectedRegionsz.Dispose();
                HOperatorSet.SelectShapeStd(ho_ConnectedRegionsz, out ho_SelectedRegionsz, "max_area",
                    70);
                ho_RegionFillUpz.Dispose();
                HOperatorSet.FillUp(ho_SelectedRegionsz, out ho_RegionFillUpz);
                ho_RegionDifferencez.Dispose();
                HOperatorSet.Difference(ho_RegionFillUpz, ho_SelectedRegionsz, out ho_RegionDifferencez
                    );
                ho_ConnectedRegionsz1.Dispose();
                HOperatorSet.Connection(ho_RegionDifferencez, out ho_ConnectedRegionsz1);
                ho_SelectedRegionszz.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegionsz1, out ho_SelectedRegionszz, (new HTuple("outer_radius")).TupleConcat(
                    "area"), "and", (new HTuple(6.5)).TupleConcat(75), (new HTuple(16)).TupleConcat(
                    400));
                HOperatorSet.AreaCenter(ho_SelectedRegionszz, out hv_Areazz, out hv_Rowzz, out hv_Columnzz);
                if ((int)(new HTuple((new HTuple(hv_Areazz.TupleLength())).TupleEqual(1))) != 0)
                {
                    HOperatorSet.SmallestCircle(ho_SelectedRegionszz, out hv_Rowzz1, out hv_Columnzz1,
                        out hv_Radiuszz1);
                    ho_Circlezz1.Dispose();
                    HOperatorSet.GenCircle(out ho_Circlezz1, hv_Rowzz1, hv_Columnzz1, hv_Radiuszz1);
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.Union2(ho_Regionzx, ho_Circlezz1, out ExpTmpOutVar_0);
                        ho_Regionzx.Dispose();
                        ho_Regionzx = ExpTmpOutVar_0;
                    }
                }
                //HDevelopStop();
                ////shang
                //HOperatorSet.DrawRectangle2(hv_ExpDefaultWinHandle, out hv_Rows, out hv_Columns,
                //    out hv_Phis, out hv_Length1s, out hv_Length2s);
                ho_Rectangles.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangles, DRows, DColumns, DPhis, DLength1s, DLength2s);
                ho_ImageReduceds.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Rectangles, out ho_ImageReduceds);
                ho_Regions.Dispose();
                HOperatorSet.Threshold(ho_ImageReduceds, out ho_Regions,128, 255);
                ho_ConnectedRegionss.Dispose();
                HOperatorSet.Connection(ho_Regions, out ho_ConnectedRegionss);
                ho_SelectedRegionss.Dispose();
                HOperatorSet.SelectShapeStd(ho_ConnectedRegionss, out ho_SelectedRegionss, "max_area",
                    70);
                ho_RegionFillUps.Dispose();
                HOperatorSet.FillUp(ho_SelectedRegionss, out ho_RegionFillUps);
                ho_RegionDifferences.Dispose();
                HOperatorSet.Difference(ho_RegionFillUps, ho_SelectedRegionss, out ho_RegionDifferences
                    );
                ho_ConnectedRegionss1.Dispose();
                HOperatorSet.Connection(ho_RegionDifferences, out ho_ConnectedRegionss1);
                ho_SelectedRegionsss.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegionss1, out ho_SelectedRegionsss, (new HTuple("outer_radius")).TupleConcat(
                    "area"), "and", (new HTuple(6.5)).TupleConcat(75), (new HTuple(16)).TupleConcat(
                    400));
                HOperatorSet.AreaCenter(ho_SelectedRegionsss, out hv_Areass, out hv_Rowss, out hv_Columnss);
                if ((int)(new HTuple((new HTuple(hv_Areass.TupleLength())).TupleEqual(1))) != 0)
                {
                    HOperatorSet.SmallestCircle(ho_SelectedRegionsss, out hv_Rowss1, out hv_Columnss1,
                        out hv_Radiusss1);
                    ho_Circless1.Dispose();
                    HOperatorSet.GenCircle(out ho_Circless1, hv_Rowss1, hv_Columnss1, hv_Radiusss1);
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.Union2(ho_Regionzx, ho_Circless1, out ExpTmpOutVar_0);
                        ho_Regionzx.Dispose();
                        ho_Regionzx = ExpTmpOutVar_0;
                    }
                }
                //HDevelopStop();
                ////xia
                //HOperatorSet.DrawRectangle2(hv_ExpDefaultWinHandle, out hv_Rowx, out hv_Columnx,
                //    out hv_Phix, out hv_Length1x, out hv_Length2x);
                //ho_Rectanglex.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectanglex, DRowx, DColumnx, DPhix, DLength1x, DLength2x);
                ho_ImageReducedx.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Rectanglex, out ho_ImageReducedx);
                ho_Regionx.Dispose();
                HOperatorSet.Threshold(ho_ImageReducedx, out ho_Regionx, 128, 255);
                ho_ConnectedRegionsx.Dispose();
                HOperatorSet.Connection(ho_Regionx, out ho_ConnectedRegionsx);
                ho_SelectedRegionsx.Dispose();
                HOperatorSet.SelectShapeStd(ho_ConnectedRegionsx, out ho_SelectedRegionsx, "max_area",
                    70);
                ho_RegionFillUpx.Dispose();
                HOperatorSet.FillUp(ho_SelectedRegionsx, out ho_RegionFillUpx);
                ho_RegionDifferencex.Dispose();
                HOperatorSet.Difference(ho_RegionFillUpx, ho_SelectedRegionsx, out ho_RegionDifferencex
                    );
                ho_ConnectedRegionsx1.Dispose();
                HOperatorSet.Connection(ho_RegionDifferencex, out ho_ConnectedRegionsx1);
                ho_SelectedRegionsxx.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegionsx1, out ho_SelectedRegionsxx, (new HTuple("outer_radius")).TupleConcat(
                    "area"), "and", (new HTuple(6.5)).TupleConcat(75), (new HTuple(16)).TupleConcat(
                    400));
                HOperatorSet.AreaCenter(ho_SelectedRegionsxx, out hv_Areaxx, out hv_Rowxx, out hv_Columnxx);
                if ((int)(new HTuple((new HTuple(hv_Areaxx.TupleLength())).TupleEqual(1))) != 0)
                {
                    HOperatorSet.SmallestCircle(ho_SelectedRegionsxx, out hv_Rowxx1, out hv_Columnxx1,
                        out hv_Radiusxx1);
                    ho_Circlexx1.Dispose();
                    HOperatorSet.GenCircle(out ho_Circlexx1, hv_Rowxx1, hv_Columnxx1, hv_Radiusxx1);
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.Union2(ho_Regionzx, ho_Circlexx1, out ExpTmpOutVar_0);
                        ho_Regionzx.Dispose();
                        ho_Regionzx = ExpTmpOutVar_0;
                    }
                }
                //HDevelopStop();

                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Regionzx, out ho_ConnectedRegions);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                    "and", 5, 99999999);
                hv_n = 0;
                HOperatorSet.CountObj(ho_SelectedRegions, out hv_Number);
                HOperatorSet.AreaCenter(ho_SelectedRegions, out hv_Area, out hv_Row, out hv_Column);

                if ((int)(new HTuple(hv_Number.TupleEqual(2))) != 0)
                {
                    HOperatorSet.DistancePp(hv_Row.TupleSelect(0), hv_Column.TupleSelect(0), hv_Row.TupleSelect(
                        1), hv_Column.TupleSelect(1), out hv_Distance);
                    if ((int)(new HTuple(hv_Distance.TupleLess(55))) != 0)
                    {
                        hv_n = 1;
                    }
                    else
                    {
                        hv_n = 2;
                    }
                }
                else
                {
                    hv_n = hv_Number.Clone();
                }

                HOperatorSet.Union1(ho_SelectedRegions, out RegionToDisp);

                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("数量");
                hv_result = hv_result.TupleConcat(hv_n.D);
                result = hv_result.Clone();
                ho_Regionzx.Dispose();
                ho_Rectangley.Dispose();
                ho_ImageReducedy.Dispose();
                ho_Regiony.Dispose();
                ho_ConnectedRegionsy.Dispose();
                ho_SelectedRegionsy.Dispose();
                ho_RegionFillUpy.Dispose();
                ho_RegionDifferencey.Dispose();
                ho_ConnectedRegionsy1.Dispose();
                ho_SelectedRegionsyy.Dispose();
                ho_Circleyy1.Dispose();
                ho_Rectanglez.Dispose();
                ho_ImageReducedz.Dispose();
                ho_Regionz.Dispose();
                ho_ConnectedRegionsz.Dispose();
                ho_SelectedRegionsz.Dispose();
                ho_RegionFillUpz.Dispose();
                ho_RegionDifferencez.Dispose();
                ho_ConnectedRegionsz1.Dispose();
                ho_SelectedRegionszz.Dispose();
                ho_Circlezz1.Dispose();
                ho_Rectangles.Dispose();
                ho_ImageReduceds.Dispose();
                ho_Regions.Dispose();
                ho_ConnectedRegionss.Dispose();
                ho_SelectedRegionss.Dispose();
                ho_RegionFillUps.Dispose();
                ho_RegionDifferences.Dispose();
                ho_ConnectedRegionss1.Dispose();
                ho_SelectedRegionsss.Dispose();
                ho_Circless1.Dispose();
                ho_Rectanglex.Dispose();
                ho_ImageReducedx.Dispose();
                ho_Regionx.Dispose();
                ho_ConnectedRegionsx.Dispose();
                ho_SelectedRegionsx.Dispose();
                ho_RegionFillUpx.Dispose();
                ho_RegionDifferencex.Dispose();
                ho_ConnectedRegionsx1.Dispose();
                ho_SelectedRegionsxx.Dispose();
                ho_Circlexx1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                algorithm.Region.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("数量");
                hv_result = hv_result.TupleConcat(99);
                result = hv_result.Clone();

                ho_Regionzx.Dispose();
                ho_Rectangley.Dispose();
                ho_ImageReducedy.Dispose();
                ho_Regiony.Dispose();
                ho_ConnectedRegionsy.Dispose();
                ho_SelectedRegionsy.Dispose();
                ho_RegionFillUpy.Dispose();
                ho_RegionDifferencey.Dispose();
                ho_ConnectedRegionsy1.Dispose();
                ho_SelectedRegionsyy.Dispose();
                ho_Circleyy1.Dispose();
                ho_Rectanglez.Dispose();
                ho_ImageReducedz.Dispose();
                ho_Regionz.Dispose();
                ho_ConnectedRegionsz.Dispose();
                ho_SelectedRegionsz.Dispose();
                ho_RegionFillUpz.Dispose();
                ho_RegionDifferencez.Dispose();
                ho_ConnectedRegionsz1.Dispose();
                ho_SelectedRegionszz.Dispose();
                ho_Circlezz1.Dispose();
                ho_Rectangles.Dispose();
                ho_ImageReduceds.Dispose();
                ho_Regions.Dispose();
                ho_ConnectedRegionss.Dispose();
                ho_SelectedRegionss.Dispose();
                ho_RegionFillUps.Dispose();
                ho_RegionDifferences.Dispose();
                ho_ConnectedRegionss1.Dispose();
                ho_SelectedRegionsss.Dispose();
                ho_Circless1.Dispose();
                ho_Rectanglex.Dispose();
                ho_ImageReducedx.Dispose();
                ho_Regionx.Dispose();
                ho_ConnectedRegionsx.Dispose();
                ho_SelectedRegionsx.Dispose();
                ho_RegionFillUpx.Dispose();
                ho_RegionDifferencex.Dispose();
                ho_ConnectedRegionsx1.Dispose();
                ho_SelectedRegionsxx.Dispose();
                ho_Circlexx1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                algorithm.Region.Dispose();

            }
            finally
            {
                ho_Regionzx.Dispose();
                ho_Rectangley.Dispose();
                ho_ImageReducedy.Dispose();
                ho_Regiony.Dispose();
                ho_ConnectedRegionsy.Dispose();
                ho_SelectedRegionsy.Dispose();
                ho_RegionFillUpy.Dispose();
                ho_RegionDifferencey.Dispose();
                ho_ConnectedRegionsy1.Dispose();
                ho_SelectedRegionsyy.Dispose();
                ho_Circleyy1.Dispose();
                ho_Rectanglez.Dispose();
                ho_ImageReducedz.Dispose();
                ho_Regionz.Dispose();
                ho_ConnectedRegionsz.Dispose();
                ho_SelectedRegionsz.Dispose();
                ho_RegionFillUpz.Dispose();
                ho_RegionDifferencez.Dispose();
                ho_ConnectedRegionsz1.Dispose();
                ho_SelectedRegionszz.Dispose();
                ho_Circlezz1.Dispose();
                ho_Rectangles.Dispose();
                ho_ImageReduceds.Dispose();
                ho_Regions.Dispose();
                ho_ConnectedRegionss.Dispose();
                ho_SelectedRegionss.Dispose();
                ho_RegionFillUps.Dispose();
                ho_RegionDifferences.Dispose();
                ho_ConnectedRegionss1.Dispose();
                ho_SelectedRegionsss.Dispose();
                ho_Circless1.Dispose();
                ho_Rectanglex.Dispose();
                ho_ImageReducedx.Dispose();
                ho_Regionx.Dispose();
                ho_ConnectedRegionsx.Dispose();
                ho_SelectedRegionsx.Dispose();
                ho_RegionFillUpx.Dispose();
                ho_RegionDifferencex.Dispose();
                ho_ConnectedRegionsx1.Dispose();
                ho_SelectedRegionsxx.Dispose();
                ho_Circlexx1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
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


