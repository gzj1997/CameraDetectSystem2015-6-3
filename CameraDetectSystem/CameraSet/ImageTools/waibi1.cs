using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
    [Serializable]
    class waibi1 : ImageTools
    {
        #region ROI
        [NonSerialized]
        private HTuple hv_Rowy = new HTuple();
        [NonSerialized]
        private HTuple hv_Columny = new HTuple();
        [NonSerialized]
        private HTuple hv_Phiy = new HTuple();
        //[NonSerialized]
        //private HTuple hv_Length1y = new HTuple();
        //[NonSerialized]
        //private HTuple hv_Length2y = new HTuple();


        public double DRowy { set; get; }
        public double DColumny { set; get; }
        public double DPhiy { set; get; }
        //public double DLength1y { set; get; }
        //public double DLength2y { set; get; }

        [NonSerialized]
        private HTuple hv_Rowz = new HTuple();
        [NonSerialized]
        private HTuple hv_Columnz = new HTuple();
        [NonSerialized]
        private HTuple hv_Phiz = new HTuple();
        //[NonSerialized]
        //private HTuple hv_Length1z = new HTuple();
        //[NonSerialized]
        //private HTuple hv_Length2z = new HTuple();


        public double DRowz { set; get; }
        public double DColumnz { set; get; }
        public double DPhiz { set; get; }
        //public double DLength1z { set; get; }
        //public double DLength2z { set; get; }

        [NonSerialized]
        private HTuple thresholdValue = new HTuple();

        public double thv { set; get; }
       


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

        public double yd { set; get; }

        public double bjz { set; get; }
        //[NonSerialized]
        //private HTuple hv_ModelIDy = new HTuple();
        //[NonSerialized]
        //private HTuple hv_Area1y = new HTuple();
        //[NonSerialized]
        //private HTuple hv_Row1y = new HTuple();
        //[NonSerialized]
        //private HTuple hv_Column1y = new HTuple();
        //public double DArea1y { set; get; }
        //public double DRow1y { set; get; }
        //public double DColumn1y { set; get; }

        //[NonSerialized]
        //private HTuple hv_ModelIDz = new HTuple();
        //[NonSerialized]
        //private HTuple hv_Area1z = new HTuple();
        //[NonSerialized]
        //private HTuple hv_Row1z = new HTuple();
        //[NonSerialized]
        //private HTuple hv_Column1z = new HTuple();
        //public double DArea1z { set; get; }
        //public double DRow1z { set; get; }
        //public double DColumn1z { set; get; }

        //[NonSerialized]
        //private HTuple hv_ModelIDs = new HTuple();
        //[NonSerialized]
        //private HTuple hv_Area1s = new HTuple();
        //[NonSerialized]
        //private HTuple hv_Row1s = new HTuple();
        //[NonSerialized]
        //private HTuple hv_Column1s = new HTuple();
        //public double DArea1s { set; get; }
        //public double DRow1s { set; get; }
        //public double DColumn1s { set; get; }

        //[NonSerialized]
        //private HTuple hv_ModelIDx = new HTuple();
        //[NonSerialized]
        //private HTuple hv_Area1x = new HTuple();
        //[NonSerialized]
        //private HTuple hv_Row1x = new HTuple();
        //[NonSerialized]
        //private HTuple hv_Column1x = new HTuple();
        //public double DArea1x { set; get; }
        //public double DRow1x { set; get; }
        //public double DColumn1x { set; get; }

        #endregion
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public waibi1()
        {
            //RegionToDisp = Image;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public waibi1(HObject Image, Algorithm al)
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
            HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            thv = thresholdValue.D;
            //disp_message(this.LWindowHandle, "请依次在右，左，上，下镜子中绘制检测区域", "window", 12, 12, "black", "true");

            //右模板
            //// Local iconic variables 

            //HObject ho_Rectangley, ho_ImageReducedy;
            //HObject ho_ModelImagesy, ho_ModelRegionsy;

            //// Local control variables 

            //HTuple hv_Rowy = null, hv_Columny = null, hv_Phiy = null;
            //HTuple hv_Length1y = null, hv_Length2y = null, hv_ModelIDy = null;
            //HTuple hv_Area1y = null, hv_Row1y = null, hv_Column1y = null;
            //// Initialize local and output iconic variables 
            //HOperatorSet.GenEmptyObj(out ho_Rectangley);
            //HOperatorSet.GenEmptyObj(out ho_ImageReducedy);
            //HOperatorSet.GenEmptyObj(out ho_ModelImagesy);
            //HOperatorSet.GenEmptyObj(out ho_ModelRegionsy);

            HOperatorSet.DrawCircle(this.LWindowHandle, out hv_Rowy, out hv_Columny,
        out hv_Phiy);
            this.DRowy = hv_Rowy.D;
            this.DColumny = hv_Columny.D;
            this.DPhiy = hv_Phiy.D;
            //ho_Rectangley.Dispose();
            //HOperatorSet.GenRectangle2(out ho_Rectangley, DRowy, DColumny, DPhiy, DLength1y,
            //    DLength2y);
            //ho_ImageReducedy.Dispose();
            //HOperatorSet.ReduceDomain(this.Image, ho_Rectangley, out ho_ImageReducedy);
            //HOperatorSet.CreateShapeModel(ho_ImageReducedy, "auto", -1.57, 1.57, "auto",
            //    "auto", "use_polarity", "auto", "auto", out hv_ModelIDy);
            //ho_ModelImagesy.Dispose(); ho_ModelRegionsy.Dispose();
            //HOperatorSet.InspectShapeModel(ho_ImageReducedy, out ho_ModelImagesy, out ho_ModelRegionsy,
            //    4, 30);
            //HOperatorSet.AreaCenter(ho_ImageReducedy, out hv_Area1y, out hv_Row1y, out hv_Column1y);
            //this.DArea1y = hv_Area1y.D;
            //this.DRow1y = hv_Row1y.D;
            //this.DColumn1y = hv_Column1y.D;
            //HOperatorSet.DispObj(ho_ModelImagesy, this.LWindowHandle);
            //HOperatorSet.WriteShapeModel(hv_ModelIDy, PathHelper.currentProductPath + @"\jingziy.shm");
            //ho_Rectangley.Dispose();
            //ho_ImageReducedy.Dispose();
            //ho_ModelImagesy.Dispose();
            //ho_ModelRegionsy.Dispose();



            //左模板

            // Local iconic variables 

            //HObject ho_Rectanglez, ho_ImageReducedz;
            //HObject ho_ModelImagesz, ho_ModelRegionsz;

            //// Local control variables 

            //HTuple hv_Rowz = null, hv_Columnz = null, hv_Phiz = null;
            //HTuple hv_Length1z = null, hv_Length2z = null, hv_ModelIDz = null;
            //HTuple hv_Area1z = null, hv_Row1z = null, hv_Column1z = null;
            //// Initialize local and output iconic variables 
            //HOperatorSet.GenEmptyObj(out ho_Rectanglez);
            //HOperatorSet.GenEmptyObj(out ho_ImageReducedz);
            //HOperatorSet.GenEmptyObj(out ho_ModelImagesz);
            //HOperatorSet.GenEmptyObj(out ho_ModelRegionsz);
            //模板
            HOperatorSet.DrawCircle(this.LWindowHandle, out hv_Rowz, out hv_Columnz,
                out hv_Phiz);
            //ho_Rectanglez.Dispose();
            //HOperatorSet.GenRectangle2(out ho_Rectanglez, hv_Rowz, hv_Columnz, hv_Phiz, hv_Length1z,
            //    hv_Length2z);
            this.DRowz = hv_Rowz.D;
            this.DColumnz = hv_Columnz.D;
            this.DPhiz = hv_Phiz.D;
            //this.DLength1z = hv_Length1z.D;
            //this.DLength2z = hv_Length2z.D;
            //ho_ImageReducedz.Dispose();
            //HOperatorSet.ReduceDomain(this.Image, ho_Rectanglez, out ho_ImageReducedz);
            //HOperatorSet.CreateShapeModel(ho_ImageReducedz, "auto", -1.57, 1.57, "auto",
            //    "auto", "use_polarity", "auto", "auto", out hv_ModelIDz);
            //ho_ModelImagesz.Dispose(); ho_ModelRegionsz.Dispose();
            //HOperatorSet.InspectShapeModel(ho_ImageReducedz, out ho_ModelImagesz, out ho_ModelRegionsz,
            //    1, 30);
            //HOperatorSet.AreaCenter(ho_ImageReducedz, out hv_Area1z, out hv_Row1z, out hv_Column1z);
            //this.DArea1z = hv_Area1z.D;
            //this.DRow1z = hv_Row1z.D;
            //this.DColumn1z = hv_Column1z.D;
            //HOperatorSet.DispObj(ho_ModelImagesz, this.LWindowHandle);
            //HOperatorSet.WriteShapeModel(hv_ModelIDz, PathHelper.currentProductPath + @"\jingziz.shm");
            //ho_Rectanglez.Dispose();
            //ho_ImageReducedz.Dispose();
            //ho_ModelImagesz.Dispose();
            //ho_ModelRegionsz.Dispose();







            HTuple hv_qxmj = new HTuple(), hv_qxcd = new HTuple(), hv_ydz = new HTuple(), hv_bydz=new HTuple();


            //
            int xx = 0;
            HTuple nu1 = new HTuple();
            while (xx == 0)
            {
                disp_message(this.LWindowHandle, "请输入最小面积（参考值50）,以回车键结束", "window", 10, 10, "black", "true");
                HOperatorSet.SetTposition(this.LWindowHandle, 80, 150);
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
                disp_message(this.LWindowHandle, "请输入腐蚀值（参考值6.5）,以回车键结束", "window", 45, 10, "black", "true");
                HOperatorSet.SetTposition(this.LWindowHandle, 160, 150);
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
                disp_message(this.LWindowHandle, "请输入模糊值（参考值11）,以回车键结束", "window", 80, 10, "black", "true");
                HOperatorSet.SetTposition(this.LWindowHandle, 240, 150);
                HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_ydz);

                try
                {
                    HOperatorSet.TupleNumber(hv_ydz, out nu2);
                    xz = 1;
                    this.yd = nu2.D;
                }
                catch
                {
                }
            }
            int xz1 = 0;
            HTuple nu3 = new HTuple();
            while (xz1 == 0)
            {
                disp_message(this.LWindowHandle, "请输入边界值（参考值25）,以回车键结束", "window", 115, 10, "black", "true");
                HOperatorSet.SetTposition(this.LWindowHandle, 320, 150);
                HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_bydz);

                try
                {
                    HOperatorSet.TupleNumber(hv_bydz, out nu3);
                    xz1 = 1;
                    this.bjz = nu3.D;
                }
                catch
                {
                }
            }

        }

        private void action()
        {
            // Local iconic variables 

            HObject ho_Circley, ho_Circley1;
            HObject ho_RegionDifference, ho_ImageReduced, ho_Region;
            HObject ho_ConnectedRegions, ho_SelectedRegions, ho_RegionDilation;
            HObject ho_RegionFillUp, ho_RegionDifference1, ho_ConnectedRegions1;
            HObject ho_SelectedRegions1, ho_RegionErosion, ho_RegionDifference2;
            HObject ho_ImageReduced1, ho_Region1, ho_RegionDilation1;
            HObject ho_RegionDifference3, ho_ImageReduced2, ho_ImageMean;
            HObject ho_RegionDynThresh, ho_ConnectedRegions2, ho_SelectedRegions3;

            // Local control variables 

            HTuple hv_a = null;
            HTuple hv_b = null, hv_Area = null, hv_Row = null, hv_Column = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Circley);
            HOperatorSet.GenEmptyObj(out ho_Circley1);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionDilation);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_RegionErosion);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference2);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_RegionDilation1);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference3);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced2);
            HOperatorSet.GenEmptyObj(out ho_ImageMean);
            HOperatorSet.GenEmptyObj(out ho_RegionDynThresh);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions3);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {

                hv_a = 0;
                hv_b = 0;

                ho_Circley.Dispose();
                HOperatorSet.GenCircle(out ho_Circley, DRowy, DColumny, DPhiy);
                ho_Circley1.Dispose();
                HOperatorSet.GenCircle(out ho_Circley1, DRowz, DColumnz, DPhiz);
                ho_RegionDifference.Dispose();
                HOperatorSet.Difference(ho_Circley, ho_Circley1, out ho_RegionDifference);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_RegionDifference, out ho_ImageReduced
                    );
                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, 0, thv);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShapeStd(ho_ConnectedRegions, out ho_SelectedRegions, "max_area",
                    70);
                ho_RegionDilation.Dispose();
                HOperatorSet.DilationCircle(ho_SelectedRegions, out ho_RegionDilation, this.cd);
                ho_RegionFillUp.Dispose();
                HOperatorSet.FillUp(ho_RegionDilation, out ho_RegionFillUp);
                ho_RegionDifference1.Dispose();
                HOperatorSet.Difference(ho_RegionFillUp, ho_RegionDilation, out ho_RegionDifference1
                    );
                ho_ConnectedRegions1.Dispose();
                HOperatorSet.Connection(ho_RegionDifference1, out ho_ConnectedRegions1);
                ho_SelectedRegions1.Dispose();
                HOperatorSet.SelectShapeStd(ho_ConnectedRegions1, out ho_SelectedRegions1,
                    "max_area", 70);
                ho_RegionErosion.Dispose();
                HOperatorSet.ErosionCircle(ho_RegionFillUp, out ho_RegionErosion, this.cd + this.cd);
                ho_RegionDifference2.Dispose();
                HOperatorSet.Difference(ho_RegionErosion, ho_SelectedRegions1, out ho_RegionDifference2
                    );



                ho_ImageReduced1.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_RegionDifference2, out ho_ImageReduced1
                    );
                ho_Region1.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced1, out ho_Region1, thv, 255);
                ho_RegionDilation1.Dispose();
                HOperatorSet.DilationCircle(ho_Region1, out ho_RegionDilation1, this.cd);
                ho_RegionDifference3.Dispose();
                HOperatorSet.Difference(ho_RegionDifference2, ho_RegionDilation1, out ho_RegionDifference3
                    );

                ho_ImageReduced2.Dispose();
                HOperatorSet.ReduceDomain(ho_ImageReduced, ho_RegionDifference3, out ho_ImageReduced2
                    );


                ho_ImageMean.Dispose();
                HOperatorSet.MeanImage(ho_ImageReduced2, out ho_ImageMean, this.yd, this.yd);
                ho_RegionDynThresh.Dispose();
                HOperatorSet.DynThreshold(ho_ImageReduced2, ho_ImageMean, out ho_RegionDynThresh,
                    15, "dark");
                ho_ConnectedRegions2.Dispose();
                HOperatorSet.Connection(ho_RegionDynThresh, out ho_ConnectedRegions2);
                ho_SelectedRegions3.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions2, out ho_SelectedRegions3, "area",
                    "and", this.mj, 9999999999);
                HOperatorSet.AreaCenter(ho_SelectedRegions3, out hv_Area, out hv_Row, out hv_Column);
                hv_a = hv_Area.TupleSum();
                hv_b = hv_Area.TupleMax();




                HOperatorSet.Union1(ho_SelectedRegions3, out RegionToDisp);

                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("缺陷总面积");
                hv_result = hv_result.TupleConcat(hv_a.D);
                hv_result = hv_result.TupleConcat("最大面积");
                hv_result = hv_result.TupleConcat(hv_b.D);
                result = hv_result.Clone();
                ho_Circley.Dispose();
                ho_Circley1.Dispose();
                ho_RegionDifference.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionDilation.Dispose();
                ho_RegionFillUp.Dispose();
                ho_RegionDifference1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionErosion.Dispose();
                ho_RegionDifference2.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region1.Dispose();
                ho_RegionDilation1.Dispose();
                ho_RegionDifference3.Dispose();
                ho_ImageReduced2.Dispose();
                ho_ImageMean.Dispose();
                ho_RegionDynThresh.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_SelectedRegions3.Dispose();
                algorithm.Region.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("缺陷总面积");
                hv_result = hv_result.TupleConcat(999999);
                hv_result = hv_result.TupleConcat("最大面积");
                hv_result = hv_result.TupleConcat(999999);
                result = hv_result.Clone();
                ho_Circley.Dispose();
                ho_Circley1.Dispose();
                ho_RegionDifference.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionDilation.Dispose();
                ho_RegionFillUp.Dispose();
                ho_RegionDifference1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionErosion.Dispose();
                ho_RegionDifference2.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region1.Dispose();
                ho_RegionDilation1.Dispose();
                ho_RegionDifference3.Dispose();
                ho_ImageReduced2.Dispose();
                ho_ImageMean.Dispose();
                ho_RegionDynThresh.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_SelectedRegions3.Dispose();
                algorithm.Region.Dispose();

            }
            finally
            {
                ho_Circley.Dispose();
                ho_Circley1.Dispose();
                ho_RegionDifference.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionDilation.Dispose();
                ho_RegionFillUp.Dispose();
                ho_RegionDifference1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionErosion.Dispose();
                ho_RegionDifference2.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region1.Dispose();
                ho_RegionDilation1.Dispose();
                ho_RegionDifference3.Dispose();
                ho_ImageReduced2.Dispose();
                ho_ImageMean.Dispose();
                ho_RegionDynThresh.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_SelectedRegions3.Dispose();
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



