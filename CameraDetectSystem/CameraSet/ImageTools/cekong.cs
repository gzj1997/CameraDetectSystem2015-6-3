using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
    [Serializable]
    class cekong : ImageTools
    {
        #region ROI
        [NonSerialized]
        private HTuple hv_Rowy = new HTuple();
        [NonSerialized]
        private HTuple hv_Columny = new HTuple();
        [NonSerialized]
        private HTuple hv_Phiy = new HTuple();
        [NonSerialized]
        private HTuple hv_Length1y = new HTuple();
        [NonSerialized]
        private HTuple hv_Length2y = new HTuple();


        public double DRowy { set; get; }
        public double DColumny { set; get; }
        public double DPhiy { set; get; }
        public double DLength1y { set; get; }
        public double DLength2y { set; get; }

        [NonSerialized]
        private HTuple hv_Rowz = new HTuple();
        [NonSerialized]
        private HTuple hv_Columnz = new HTuple();
        [NonSerialized]
        private HTuple hv_Phiz = new HTuple();
        [NonSerialized]
        private HTuple hv_Length1z = new HTuple();
        [NonSerialized]
        private HTuple hv_Length2z = new HTuple();


        public double DRowz { set; get; }
        public double DColumnz { set; get; }
        public double DPhiz { set; get; }
        public double DLength1z { set; get; }
        public double DLength2z { set; get; }


        [NonSerialized]
        private HTuple hv_Rows = new HTuple();
        [NonSerialized]
        private HTuple hv_Columns = new HTuple();
        [NonSerialized]
        private HTuple hv_Phis = new HTuple();
        [NonSerialized]
        private HTuple hv_Length1s = new HTuple();
        [NonSerialized]
        private HTuple hv_Length2s = new HTuple();


        public double DRows { set; get; }
        public double DColumns { set; get; }
        public double DPhis { set; get; }
        public double DLength1s { set; get; }
        public double DLength2s { set; get; }


        [NonSerialized]
        private HTuple hv_Rowx = new HTuple();
        [NonSerialized]
        private HTuple hv_Columnx = new HTuple();
        [NonSerialized]
        private HTuple hv_Phix = new HTuple();
        [NonSerialized]
        private HTuple hv_Length1x = new HTuple();
        [NonSerialized]
        private HTuple hv_Length2x = new HTuple();


        public double DRowx { set; get; }
        public double DColumnx { set; get; }
        public double DPhix{ set; get; }
        public double DLength1x { set; get; }
        public double DLength2x { set; get; }


        [NonSerialized]
        private HTuple nu = new HTuple();
        [NonSerialized]
        private HTuple nu1 = new HTuple();
        [NonSerialized]
        private HTuple nu2 = new HTuple();
                

        public double mj { set; get; }
        public double cd { set; get; }
        
        public double yd { set; get; }

        [NonSerialized]
        private HTuple hv_ModelIDy = new HTuple();
        [NonSerialized]
        private HTuple hv_Area1y = new HTuple();
        [NonSerialized]
        private HTuple hv_Row1y = new HTuple();
        [NonSerialized]
        private HTuple hv_Column1y = new HTuple();
        public double DArea1y { set; get; }
        public double DRow1y { set; get; }
        public double DColumn1y { set; get; }

        [NonSerialized]
        private HTuple hv_ModelIDz = new HTuple();
        [NonSerialized]
        private HTuple hv_Area1z = new HTuple();
        [NonSerialized]
        private HTuple hv_Row1z = new HTuple();
        [NonSerialized]
        private HTuple hv_Column1z = new HTuple();
        public double DArea1z { set; get; }
        public double DRow1z { set; get; }
        public double DColumn1z { set; get; }

        [NonSerialized]
        private HTuple hv_ModelIDs = new HTuple();
        [NonSerialized]
        private HTuple hv_Area1s = new HTuple();
        [NonSerialized]
        private HTuple hv_Row1s = new HTuple();
        [NonSerialized]
        private HTuple hv_Column1s = new HTuple();
        public double DArea1s { set; get; }
        public double DRow1s { set; get; }
        public double DColumn1s { set; get; }

        [NonSerialized]
        private HTuple hv_ModelIDx = new HTuple();
        [NonSerialized]
        private HTuple hv_Area1x = new HTuple();
        [NonSerialized]
        private HTuple hv_Row1x = new HTuple();
        [NonSerialized]
        private HTuple hv_Column1x = new HTuple();
        public double DArea1x { set; get; }
        public double DRow1x { set; get; }
        public double DColumn1x { set; get; }

        #endregion
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public cekong()
        {
            //RegionToDisp = Image;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public cekong(HObject Image, Algorithm al)
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
            disp_message(this.LWindowHandle, "请依次在右，左，上，下镜子中绘制检测区域", "window", 12, 12, "black", "true");

            //右模板
            // Local iconic variables 

            HObject ho_Rectangley, ho_ImageReducedy;
            HObject ho_ModelImagesy, ho_ModelRegionsy;

            // Local control variables 

            HTuple hv_Rowy = null, hv_Columny = null, hv_Phiy = null;
            HTuple hv_Length1y = null, hv_Length2y = null, hv_ModelIDy = null;
            HTuple hv_Area1y = null, hv_Row1y = null, hv_Column1y = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangley);
            HOperatorSet.GenEmptyObj(out ho_ImageReducedy);
            HOperatorSet.GenEmptyObj(out ho_ModelImagesy);
            HOperatorSet.GenEmptyObj(out ho_ModelRegionsy);

            HOperatorSet.DrawRectangle2(this.LWindowHandle, out hv_Rowy, out hv_Columny,
        out hv_Phiy, out hv_Length1y, out hv_Length2y);
            this.DRowy = hv_Rowy.D;
            this.DColumny = hv_Columny.D;
            this.DPhiy = hv_Phiy.D;
            this.DLength1y = hv_Length1y.D;
            this.DLength2y = hv_Length2y.D;
            ho_Rectangley.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangley, DRowy, DColumny, DPhiy, DLength1y,
                DLength2y);
            ho_ImageReducedy.Dispose();
            HOperatorSet.ReduceDomain(this.Image, ho_Rectangley, out ho_ImageReducedy);
            HOperatorSet.CreateShapeModel(ho_ImageReducedy, "auto", -1.57, 1.57, "auto",
                "auto", "use_polarity", "auto", "auto", out hv_ModelIDy);
            ho_ModelImagesy.Dispose(); ho_ModelRegionsy.Dispose();
            HOperatorSet.InspectShapeModel(ho_ImageReducedy, out ho_ModelImagesy, out ho_ModelRegionsy,
                4, 30);
            HOperatorSet.AreaCenter(ho_ImageReducedy, out hv_Area1y, out hv_Row1y, out hv_Column1y);
            this.DArea1y = hv_Area1y.D;
            this.DRow1y = hv_Row1y.D;
            this.DColumn1y = hv_Column1y.D;
            HOperatorSet.DispObj(ho_ModelImagesy, this.LWindowHandle);
            HOperatorSet.WriteShapeModel(hv_ModelIDy, PathHelper.currentProductPath + @"\jingziy.shm");
            ho_Rectangley.Dispose();
            ho_ImageReducedy.Dispose();
            ho_ModelImagesy.Dispose();
            ho_ModelRegionsy.Dispose();



            //左模板

            // Local iconic variables 

            HObject ho_Rectanglez, ho_ImageReducedz;
            HObject ho_ModelImagesz, ho_ModelRegionsz;

            // Local control variables 

            HTuple hv_Rowz = null, hv_Columnz = null, hv_Phiz = null;
            HTuple hv_Length1z = null, hv_Length2z = null, hv_ModelIDz = null;
            HTuple hv_Area1z = null, hv_Row1z = null, hv_Column1z = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectanglez);
            HOperatorSet.GenEmptyObj(out ho_ImageReducedz);
            HOperatorSet.GenEmptyObj(out ho_ModelImagesz);
            HOperatorSet.GenEmptyObj(out ho_ModelRegionsz);
            //模板
            HOperatorSet.DrawRectangle2(this.LWindowHandle, out hv_Rowz, out hv_Columnz,
                out hv_Phiz, out hv_Length1z, out hv_Length2z);
            ho_Rectanglez.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectanglez, hv_Rowz, hv_Columnz, hv_Phiz, hv_Length1z,
                hv_Length2z);
            this.DRowz = hv_Rowz.D;
            this.DColumnz = hv_Columnz.D;
            this.DPhiz = hv_Phiz.D;
            this.DLength1z = hv_Length1z.D;
            this.DLength2z = hv_Length2z.D;
            ho_ImageReducedz.Dispose();
            HOperatorSet.ReduceDomain(this.Image, ho_Rectanglez, out ho_ImageReducedz);
            HOperatorSet.CreateShapeModel(ho_ImageReducedz, "auto", -1.57, 1.57, "auto",
                "auto", "use_polarity", "auto", "auto", out hv_ModelIDz);
            ho_ModelImagesz.Dispose(); ho_ModelRegionsz.Dispose();
            HOperatorSet.InspectShapeModel(ho_ImageReducedz, out ho_ModelImagesz, out ho_ModelRegionsz,
                1, 30);
            HOperatorSet.AreaCenter(ho_ImageReducedz, out hv_Area1z, out hv_Row1z, out hv_Column1z);
            this.DArea1z = hv_Area1z.D;
            this.DRow1z = hv_Row1z.D;
            this.DColumn1z = hv_Column1z.D;
            HOperatorSet.DispObj(ho_ModelImagesz, this.LWindowHandle);
            HOperatorSet.WriteShapeModel(hv_ModelIDz, PathHelper.currentProductPath + @"\jingziz.shm");
            ho_Rectanglez.Dispose();
            ho_ImageReducedz.Dispose();
            ho_ModelImagesz.Dispose();
            ho_ModelRegionsz.Dispose();

            //上模板

            // Local iconic variables 

            HObject ho_Rectangles, ho_ImageReduceds;
            HObject ho_ModelImagess, ho_ModelRegionss;

            // Local control variables 

            HTuple hv_Rows = null, hv_Columns = null, hv_Phis = null;
            HTuple hv_Length1s = null, hv_Length2s = null, hv_ModelIDs = null;
            HTuple hv_Area1s = null, hv_Row1s = null, hv_Column1s = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangles);
            HOperatorSet.GenEmptyObj(out ho_ImageReduceds);
            HOperatorSet.GenEmptyObj(out ho_ModelImagess);
            HOperatorSet.GenEmptyObj(out ho_ModelRegionss);
            //模板
            HOperatorSet.DrawRectangle2(this.LWindowHandle, out hv_Rows, out hv_Columns,
                out hv_Phis, out hv_Length1s, out hv_Length2s);

            ho_Rectangles.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangles, hv_Rows, hv_Columns, hv_Phis, hv_Length1s,
                hv_Length2s);
            this.DRows = hv_Rows.D;
            this.DColumns = hv_Columns.D;
            this.DPhis = hv_Phis.D;
            this.DLength1s = hv_Length1s.D;
            this.DLength2s = hv_Length2s.D;
            ho_ImageReduceds.Dispose();
            HOperatorSet.ReduceDomain(this.Image, ho_Rectangles, out ho_ImageReduceds);
            HOperatorSet.CreateShapeModel(ho_ImageReduceds, "auto", -1.57, 1.57, "auto",
                "auto", "use_polarity", "auto", "auto", out hv_ModelIDs);
            ho_ModelImagess.Dispose(); ho_ModelRegionss.Dispose();
            HOperatorSet.InspectShapeModel(ho_ImageReduceds, out ho_ModelImagess, out ho_ModelRegionss,
                1, 30);
            HOperatorSet.AreaCenter(ho_ImageReduceds, out hv_Area1s, out hv_Row1s, out hv_Column1s);
            this.DArea1s = hv_Area1s.D;
            this.DRow1s = hv_Row1s.D;
            this.DColumn1s = hv_Column1s.D;
            HOperatorSet.DispObj(ho_ModelImagess, this.LWindowHandle);
            HOperatorSet.WriteShapeModel(hv_ModelIDs, PathHelper.currentProductPath + @"\jingzis.shm");
            ho_Rectangles.Dispose();
            ho_ImageReduceds.Dispose();
            ho_ModelImagess.Dispose();
            ho_ModelRegionss.Dispose();

            // Local iconic variables 
            //下模板

            HObject ho_Rectanglex, ho_ImageReducedx;
            HObject ho_ModelImagesx, ho_ModelRegionsx;

            // Local control variables 

            HTuple hv_Rowx = null, hv_Columnx = null, hv_Phix = null;
            HTuple hv_Length1x = null, hv_Length2x = null, hv_ModelIDx = null;
            HTuple hv_Area1x = null, hv_Row1x = null, hv_Column1x = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectanglex);
            HOperatorSet.GenEmptyObj(out ho_ImageReducedx);
            HOperatorSet.GenEmptyObj(out ho_ModelImagesx);
            HOperatorSet.GenEmptyObj(out ho_ModelRegionsx);
            //模板
            HOperatorSet.DrawRectangle2(this.LWindowHandle, out hv_Rowx, out hv_Columnx,
                out hv_Phix, out hv_Length1x, out hv_Length2x);
            ho_Rectanglex.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectanglex, hv_Rowx, hv_Columnx, hv_Phix, hv_Length1x,
                hv_Length2x);
            this.DRowx = hv_Rowx.D;
            this.DColumnx = hv_Columnx.D;
            this.DPhix = hv_Phix.D;
            this.DLength1x = hv_Length1x.D;
            this.DLength2x = hv_Length2x.D;
            ho_ImageReducedx.Dispose();
            HOperatorSet.ReduceDomain(this.Image, ho_Rectanglex, out ho_ImageReducedx);
            HOperatorSet.CreateShapeModel(ho_ImageReducedx, "auto", -1.57, 1.57, "auto",
                "auto", "use_polarity", "auto", "auto", out hv_ModelIDx);
            ho_ModelImagesx.Dispose(); ho_ModelRegionsx.Dispose();
            HOperatorSet.InspectShapeModel(ho_ImageReducedx, out ho_ModelImagesx, out ho_ModelRegionsx,
                1, 30);
            HOperatorSet.AreaCenter(ho_ImageReducedx, out hv_Area1x, out hv_Row1x, out hv_Column1x);
            this.DArea1x = hv_Area1x.D;
            this.DRow1x = hv_Row1x.D;
            this.DColumn1x = hv_Column1x.D;
            HOperatorSet.DispObj(ho_ModelImagesx, this.LWindowHandle);
            HOperatorSet.WriteShapeModel(hv_ModelIDx, PathHelper.currentProductPath + @"\jingzix.shm");
            ho_Rectanglex.Dispose();
            ho_ImageReducedx.Dispose();
            ho_ModelImagesx.Dispose();
            ho_ModelRegionsx.Dispose();






    //        //右模板
    //        HTuple Rowy = null, Columny = null, Phiy = null, Length1y = null, Length2y = null;
    //        HTuple Area1y = null;
    //        HTuple Row1y = null;
    //        HTuple Column1y = null;
    //        HObject ho_Rectangley, ho_ImageReducedy, ho_ModelImagesy, ho_ModelRegionsy;
    //        HOperatorSet.GenEmptyObj(out ho_Rectangley);
    //        HOperatorSet.GenEmptyObj(out ho_ImageReducedy);
    //        HOperatorSet.GenEmptyObj(out ho_ModelImagesy);
    //        HOperatorSet.GenEmptyObj(out ho_ModelRegionsy);
    //        HOperatorSet.DrawRectangle2(this.LWindowHandle, out Rowy, out Columny, out Phiy, out Length1y, out Length2y);
    //        this.DRowy = Rowy.D;
    //        this.DColumny = Columny.D;
    //        this.DPhiy = Phiy.D;
    //        this.DLength1y = Length1y.D;
    //        this.DLength2y = Length2y.D;
    //        ho_Rectangley.Dispose();
    //        HOperatorSet.GenRectangle2(out ho_Rectangley, DRowy, DColumny, DPhiy, DLength1y, DLength2y);

    //        ho_ImageReducedy.Dispose();
    //        HOperatorSet.ReduceDomain(this.Image, ho_Rectangley, out ho_ImageReducedy);
    //        HOperatorSet.CreateShapeModel(ho_ImageReducedy, "auto", -1.57, 1.57, "auto",
    //            "auto", "use_polarity", "auto", "auto", out hv_ModelIDy);
    //        ho_ModelImagesy.Dispose();
    //        ho_ModelRegionsy.Dispose();
    //        HOperatorSet.InspectShapeModel(ho_ImageReducedy, out ho_ModelImagesy, out ho_ModelRegionsy,
    //            4, 30);
    //        HOperatorSet.AreaCenter(ho_ImageReducedy, out Area1y, out Row1y, out Column1y);
    //        this.DArea1y = Area1y.D;
    //        this.DRow1y = Row1y.D;
    //        this.DColumn1y = Column1y.D;
    //        HOperatorSet.DispObj(ho_ModelRegionsy, this.LWindowHandle);
    //        HOperatorSet.WriteShapeModel(hv_ModelIDy, PathHelper.currentProductPath + @"\jingziy.shm");
    //        ho_Rectangley.Dispose();
    //        ho_ImageReducedy.Dispose();
    //        ho_ModelImagesy.Dispose();
    //        ho_ModelRegionsy.Dispose();



    

            HTuple hv_qxmj = new HTuple(), hv_qxcd = new HTuple(), hv_ydz = new HTuple();
            
            
            //
            int xx = 0;
            HTuple nu1 = new HTuple();
            while (xx == 0)
            {
                disp_message(this.LWindowHandle, "请输入最小面积（参考值75）,以回车键结束", "window", 10, 10, "black", "true");
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
                    disp_message(this.LWindowHandle, "请输入最小半径（参考值6）,以回车键结束", "window", 45, 10, "black", "true");
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
                    disp_message(this.LWindowHandle, "请输入分割值（参考值128）,以回车键结束", "window", 80, 10, "black", "true");
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
                
              
        }

        private void action()
        {
            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_Regionzx, ho_Rectangley;
            HObject ho_Rectanglez, ho_Rectangles, ho_Rectanglex;
            HObject ho_Regionzzx, ho_Regionzy, ho_Regionzz;
            HObject ho_Regionzs, ho_Regionzy1, ho_Regionzz1, ho_Regionzs1;
            HObject ho_Regionzx1, ho_ImageAffinTransy = null, ho_ImageReducedyy = null;
            HObject ho_Regiony = null, ho_ConnectedRegionsy = null, ho_SelectedRegionsy = null;
            HObject ho_RegionFillUpy = null, ho_RegionDifferencey = null;
            HObject ho_ConnectedRegionsy1 = null, ho_SelectedRegionsyy = null;
            HObject ho_Circleyy1 = null, ho_RegionAffineTransy1 = null;
            HObject ho_ImageAffinTransz = null, ho_ImageReducedzz = null;
            HObject ho_Regionz = null, ho_ConnectedRegionsz = null, ho_SelectedRegionsz = null;
            HObject ho_RegionFillUpz = null, ho_RegionDifferencez = null;
            HObject ho_ConnectedRegionsz1 = null, ho_SelectedRegionszz = null;
            HObject ho_Circlezz1 = null, ho_RegionAffineTransz1 = null;
            HObject ho_ImageAffinTranss = null, ho_ImageReducedss = null;
            HObject ho_Regions = null, ho_ConnectedRegionss = null, ho_SelectedRegionss = null;
            HObject ho_RegionFillUps = null, ho_RegionDifferences = null;
            HObject ho_ConnectedRegionss1 = null, ho_SelectedRegionsss = null;
            HObject ho_Circless1 = null, ho_RegionAffineTranss1 = null;
            HObject ho_ImageAffinTranxx = null, ho_ImageReducedxx = null;
            HObject ho_Regionx = null, ho_ConnectedRegionsx = null, ho_SelectedRegionsx = null;
            HObject ho_RegionFillUpx = null, ho_RegionDifferencex = null;
            HObject ho_ConnectedRegionsx1 = null, ho_SelectedRegionsxx = null;
            HObject ho_Circlexx1 = null, ho_RegionAffineTranxx1 = null;

            // Local control variables 

            HTuple hv_m = null, hv_y = null, hv_Row1yy = null, hv_Column1yy = null;
            HTuple hv_Angleyy = null, hv_Scoreyy = null, hv_HomMat2Dy = new HTuple();
            HTuple hv_Areayy = new HTuple(), hv_Rowyy = new HTuple();
            HTuple hv_Columnyy = new HTuple(), hv_Rowyy1 = new HTuple();
            HTuple hv_Columnyy1 = new HTuple(), hv_Radiusyy1 = new HTuple();
            HTuple hv_HomMat2Dy1 = new HTuple(), hv_z = null, hv_Row1zz = null;
            HTuple hv_Column1zz = null, hv_Anglezz = null, hv_Scorezz = null;
            HTuple hv_HomMat2Dz = new HTuple(), hv_Areazz = new HTuple();
            HTuple hv_Rowzz = new HTuple(), hv_Columnzz = new HTuple();
            HTuple hv_Rowzz1 = new HTuple(), hv_Columnzz1 = new HTuple();
            HTuple hv_Radiuszz1 = new HTuple(), hv_HomMat2Dz1 = new HTuple();
            HTuple hv_s = null, hv_Row1ss = null, hv_Column1ss = null;
            HTuple hv_Angless = null, hv_Scoress = null, hv_HomMat2Ds = new HTuple();
            HTuple hv_Areass = new HTuple(), hv_Rowss = new HTuple();
            HTuple hv_Columnss = new HTuple(), hv_Rowss1 = new HTuple();
            HTuple hv_Columnss1 = new HTuple(), hv_Radiusss1 = new HTuple();
            HTuple hv_HomMat2Ds1 = new HTuple(), hv_x = null, hv_Row1xx = null;
            HTuple hv_Column1xx = null, hv_Anglexx = null, hv_Scorexx = null;
            HTuple hv_HomMat2Dx = new HTuple(), hv_Areaxx = new HTuple();
            HTuple hv_Rowxx = new HTuple(), hv_Columnxx = new HTuple();
            HTuple hv_Rowxx1 = new HTuple(), hv_Columnxx1 = new HTuple();
            HTuple hv_Radiusxx1 = new HTuple(), hv_HomMat2Dx1 = new HTuple();
            HTuple hv_mm = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Regionzzx);
            HOperatorSet.GenEmptyObj(out ho_Regionzy);
            HOperatorSet.GenEmptyObj(out ho_Regionzz);
            HOperatorSet.GenEmptyObj(out ho_Regionzs);
            HOperatorSet.GenEmptyObj(out ho_Regionzx);
            HOperatorSet.GenEmptyObj(out ho_Regionzy1);
            HOperatorSet.GenEmptyObj(out ho_Regionzz1);
            HOperatorSet.GenEmptyObj(out ho_Regionzs1);
            HOperatorSet.GenEmptyObj(out ho_Regionzx1);
            HOperatorSet.GenEmptyObj(out ho_Rectangley);
            HOperatorSet.GenEmptyObj(out ho_Rectanglez);
            HOperatorSet.GenEmptyObj(out ho_Rectangles);
            HOperatorSet.GenEmptyObj(out ho_Rectanglex);
            HOperatorSet.GenEmptyObj(out ho_ImageAffinTransy);
            HOperatorSet.GenEmptyObj(out ho_ImageReducedyy);
            HOperatorSet.GenEmptyObj(out ho_Regiony);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegionsy);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsy);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUpy);
            HOperatorSet.GenEmptyObj(out ho_RegionDifferencey);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegionsy1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsyy);
            HOperatorSet.GenEmptyObj(out ho_Circleyy1);
            HOperatorSet.GenEmptyObj(out ho_RegionAffineTransy1);
            HOperatorSet.GenEmptyObj(out ho_ImageAffinTransz);
            HOperatorSet.GenEmptyObj(out ho_ImageReducedzz);
            HOperatorSet.GenEmptyObj(out ho_Regionz);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegionsz);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsz);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUpz);
            HOperatorSet.GenEmptyObj(out ho_RegionDifferencez);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegionsz1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionszz);
            HOperatorSet.GenEmptyObj(out ho_Circlezz1);
            HOperatorSet.GenEmptyObj(out ho_RegionAffineTransz1);
            HOperatorSet.GenEmptyObj(out ho_ImageAffinTranss);
            HOperatorSet.GenEmptyObj(out ho_ImageReducedss);
            HOperatorSet.GenEmptyObj(out ho_Regions);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegionss);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionss);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUps);
            HOperatorSet.GenEmptyObj(out ho_RegionDifferences);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegionss1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsss);
            HOperatorSet.GenEmptyObj(out ho_Circless1);
            HOperatorSet.GenEmptyObj(out ho_RegionAffineTranss1);
            HOperatorSet.GenEmptyObj(out ho_ImageAffinTranxx);
            HOperatorSet.GenEmptyObj(out ho_ImageReducedxx);
            HOperatorSet.GenEmptyObj(out ho_Regionx);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegionsx);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsx);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUpx);
            HOperatorSet.GenEmptyObj(out ho_RegionDifferencex);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegionsx1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsxx);
            HOperatorSet.GenEmptyObj(out ho_Circlexx1);
            HOperatorSet.GenEmptyObj(out ho_RegionAffineTranxx1);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {
                ho_Regionzzx.Dispose();
                HOperatorSet.GenRegionPoints(out ho_Regionzzx, DRowy, DColumny);
                ho_Regionzy.Dispose();
                HOperatorSet.GenRegionPoints(out ho_Regionzy, DRowy, DColumny);
                ho_Regionzz.Dispose();
                HOperatorSet.GenRegionPoints(out ho_Regionzz, DRowz, DColumnz);
                ho_Regionzs.Dispose();
                HOperatorSet.GenRegionPoints(out ho_Regionzs, DRows, DColumns);
                ho_Regionzx.Dispose();
                HOperatorSet.GenRegionPoints(out ho_Regionzx, DRowx, DColumnx);
                ho_Regionzy1.Dispose();
                HOperatorSet.GenRegionPoints(out ho_Regionzy1,DRowy, DColumny);
                ho_Regionzz1.Dispose();
                HOperatorSet.GenRegionPoints(out ho_Regionzz1, DRowz, DColumnz);
                ho_Regionzs1.Dispose();
                HOperatorSet.GenRegionPoints(out ho_Regionzs1, DRows, DColumns);
                ho_Regionzx1.Dispose();
                HOperatorSet.GenRegionPoints(out ho_Regionzx1, DRowx, DColumnx);
                hv_m = new HTuple();
                //you

                hv_y = 0;
                ho_Rectangley.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangley, DRowy, DColumny, DPhiy, DLength1y, DLength2y);
                HOperatorSet.ReadShapeModel(PathHelper.currentProductPath + @"\jingziy.shm", out hv_ModelIDy);

                HOperatorSet.FindShapeModel(Image, hv_ModelIDy, -1.57, 1.57, 0.3, 1, 0.5,
       "least_squares", 0, 0.9, out hv_Row1yy, out hv_Column1yy, out hv_Angleyy,
       out hv_Scoreyy);
                if ((int)(new HTuple((new HTuple(hv_Scoreyy.TupleLength())).TupleEqual(1))) != 0)
                {
                    HOperatorSet.VectorAngleToRigid(hv_Row1yy, hv_Column1yy, hv_Angleyy, DRow1y,
                        DColumn1y, 0, out hv_HomMat2Dy);
                    ho_ImageAffinTransy.Dispose();
                    HOperatorSet.AffineTransImage(Image, out ho_ImageAffinTransy, hv_HomMat2Dy,
                        "constant", "false");
                    ho_ImageReducedyy.Dispose();
                    HOperatorSet.ReduceDomain(ho_ImageAffinTransy, ho_Rectangley, out ho_ImageReducedyy
                        );
                    ho_Regiony.Dispose();
                    HOperatorSet.Threshold(ho_ImageReducedyy, out ho_Regiony, this.yd, 255);
                    ho_ConnectedRegionsy.Dispose();
                    HOperatorSet.Connection(ho_Regiony, out ho_ConnectedRegionsy);
                    ho_SelectedRegionsy.Dispose();
                    HOperatorSet.SelectShapeStd(ho_ConnectedRegionsy, out ho_SelectedRegionsy,
                        "max_area", 70);
                    ho_RegionFillUpy.Dispose();
                    HOperatorSet.FillUp(ho_SelectedRegionsy, out ho_RegionFillUpy);
                    ho_RegionDifferencey.Dispose();
                    HOperatorSet.Difference(ho_RegionFillUpy, ho_SelectedRegionsy, out ho_RegionDifferencey
                        );
                    ho_ConnectedRegionsy1.Dispose();
                    HOperatorSet.Connection(ho_RegionDifferencey, out ho_ConnectedRegionsy1);
                    ho_SelectedRegionsyy.Dispose();
                    HOperatorSet.SelectShape(ho_ConnectedRegionsy1, out ho_SelectedRegionsyy, (new HTuple("outer_radius")).TupleConcat(
                        "area"), "and", (new HTuple(this.cd)).TupleConcat(this.mj), (new HTuple(16)).TupleConcat(
                        400));
                    HOperatorSet.AreaCenter(ho_SelectedRegionsyy, out hv_Areayy, out hv_Rowyy,
                        out hv_Columnyy);
                    if ((int)(new HTuple((new HTuple(hv_Areayy.TupleLength())).TupleGreaterEqual(
                        1))) != 0)
                    {
                        hv_y = 1;
                        HOperatorSet.SmallestCircle(ho_SelectedRegionsyy, out hv_Rowyy1, out hv_Columnyy1,
                            out hv_Radiusyy1);
                        ho_Circleyy1.Dispose();
                        HOperatorSet.GenCircle(out ho_Circleyy1, hv_Rowyy1, hv_Columnyy1, hv_Radiusyy1);
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.Union2(ho_Regionzy1, ho_Circleyy1, out ExpTmpOutVar_0);
                            ho_Regionzy1.Dispose();
                            ho_Regionzy1 = ExpTmpOutVar_0;
                        }
                    }
                    HOperatorSet.VectorAngleToRigid(DRow1y, DColumn1y, 0, hv_Row1yy, hv_Column1yy,
                        hv_Angleyy, out hv_HomMat2Dy1);
                    ho_RegionAffineTransy1.Dispose();
                    HOperatorSet.AffineTransRegion(ho_Regionzy1, out ho_RegionAffineTransy1, hv_HomMat2Dy1,
                        "nearest_neighbor");
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.Union2(ho_Regionzy, ho_RegionAffineTransy1, out ExpTmpOutVar_0
                            );
                        ho_Regionzy.Dispose();
                        ho_Regionzy = ExpTmpOutVar_0;
                    }
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.Union2(ho_Regionzzx, ho_Regionzy, out ExpTmpOutVar_0);
                    ho_Regionzzx.Dispose();
                    ho_Regionzzx = ExpTmpOutVar_0;
                }
                HOperatorSet.ClearShapeModel(hv_ModelIDy);

                //zuo

                hv_z = 0;
                ho_Rectanglez.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectanglez, DRowz, DColumnz, DPhiz, DLength1z, DLength2z);
                HOperatorSet.ReadShapeModel(PathHelper.currentProductPath + @"\jingziz.shm", out hv_ModelIDz);

                HOperatorSet.FindShapeModel(Image, hv_ModelIDz, -1.57, 1.57, 0.3, 1, 0.5,
        "least_squares", 0, 0.9, out hv_Row1zz, out hv_Column1zz, out hv_Anglezz,
        out hv_Scorezz);
                if ((int)(new HTuple((new HTuple(hv_Scorezz.TupleLength())).TupleEqual(1))) != 0)
                {
                    HOperatorSet.VectorAngleToRigid(hv_Row1zz, hv_Column1zz, hv_Anglezz, DRow1z,
                        DColumn1z, 0, out hv_HomMat2Dz);
                    ho_ImageAffinTransz.Dispose();
                    HOperatorSet.AffineTransImage(Image, out ho_ImageAffinTransz, hv_HomMat2Dz,
                        "constant", "false");
                    ho_ImageReducedzz.Dispose();
                    HOperatorSet.ReduceDomain(ho_ImageAffinTransz, ho_Rectanglez, out ho_ImageReducedzz
                        );
                    ho_Regionz.Dispose();
                    HOperatorSet.Threshold(ho_ImageReducedzz, out ho_Regionz, this.yd, 255);
                    ho_ConnectedRegionsz.Dispose();
                    HOperatorSet.Connection(ho_Regionz, out ho_ConnectedRegionsz);
                    ho_SelectedRegionsz.Dispose();
                    HOperatorSet.SelectShapeStd(ho_ConnectedRegionsz, out ho_SelectedRegionsz,
                        "max_area", 70);
                    ho_RegionFillUpz.Dispose();
                    HOperatorSet.FillUp(ho_SelectedRegionsz, out ho_RegionFillUpz);
                    ho_RegionDifferencez.Dispose();
                    HOperatorSet.Difference(ho_RegionFillUpz, ho_SelectedRegionsz, out ho_RegionDifferencez
                        );
                    ho_ConnectedRegionsz1.Dispose();
                    HOperatorSet.Connection(ho_RegionDifferencez, out ho_ConnectedRegionsz1);
                    ho_SelectedRegionszz.Dispose();
                    HOperatorSet.SelectShape(ho_ConnectedRegionsz1, out ho_SelectedRegionszz, (new HTuple("outer_radius")).TupleConcat(
                        "area"), "and", (new HTuple(this.cd)).TupleConcat(this.mj), (new HTuple(16)).TupleConcat(
                        400));
                    HOperatorSet.AreaCenter(ho_SelectedRegionszz, out hv_Areazz, out hv_Rowzz,
                        out hv_Columnzz);
                    if ((int)(new HTuple((new HTuple(hv_Areazz.TupleLength())).TupleGreaterEqual(
                        1))) != 0)
                    {
                        hv_z = 1;
                        HOperatorSet.SmallestCircle(ho_SelectedRegionszz, out hv_Rowzz1, out hv_Columnzz1,
                            out hv_Radiuszz1);
                        ho_Circlezz1.Dispose();
                        HOperatorSet.GenCircle(out ho_Circlezz1, hv_Rowzz1, hv_Columnzz1, hv_Radiuszz1);
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.Union2(ho_Regionzz1, ho_Circlezz1, out ExpTmpOutVar_0);
                            ho_Regionzz1.Dispose();
                            ho_Regionzz1 = ExpTmpOutVar_0;
                        }
                    }
                    HOperatorSet.VectorAngleToRigid(DRow1z, DColumn1z, 0, hv_Row1zz, hv_Column1zz,
                        hv_Anglezz, out hv_HomMat2Dz1);
                    ho_RegionAffineTransz1.Dispose();
                    HOperatorSet.AffineTransRegion(ho_Regionzz1, out ho_RegionAffineTransz1, hv_HomMat2Dy1,
                        "nearest_neighbor");
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.Union2(ho_Regionzz, ho_RegionAffineTransz1, out ExpTmpOutVar_0
                            );
                        ho_Regionzz.Dispose();
                        ho_Regionzz = ExpTmpOutVar_0;
                    }
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.Union2(ho_Regionzzx, ho_Regionzz, out ExpTmpOutVar_0);
                    ho_Regionzzx.Dispose();
                    ho_Regionzzx = ExpTmpOutVar_0;
                }
                HOperatorSet.ClearShapeModel(hv_ModelIDz);
                //HDevelopStop();
                ////shang
                //HOperatorSet.DrawRectangle2(hv_ExpDefaultWinHandle, out hv_Rows, out hv_Columns,
                //    out hv_Phis, out hv_Length1s, out hv_Length2s);
                hv_s = 0;
                ho_Rectangles.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangles, DRows, DColumns, DPhis, DLength1s, DLength2s);
                HOperatorSet.ReadShapeModel(PathHelper.currentProductPath + @"\jingzis.shm", out hv_ModelIDs);
                HOperatorSet.FindShapeModel(Image, hv_ModelIDs, -1.57, 1.57, 0.3, 1, 0.5,
        "least_squares", 0, 0.9, out hv_Row1ss, out hv_Column1ss, out hv_Angless,
        out hv_Scoress);
                if ((int)(new HTuple((new HTuple(hv_Scoress.TupleLength())).TupleEqual(1))) != 0)
                {
                    HOperatorSet.VectorAngleToRigid(hv_Row1ss, hv_Column1ss, hv_Angless, DRow1s,
                        DColumn1s, 0, out hv_HomMat2Ds);
                    ho_ImageAffinTranss.Dispose();
                    HOperatorSet.AffineTransImage(Image, out ho_ImageAffinTranss, hv_HomMat2Ds,
                        "constant", "false");
                    ho_ImageReducedss.Dispose();
                    HOperatorSet.ReduceDomain(ho_ImageAffinTranss, ho_Rectangles, out ho_ImageReducedss
                        );
                    ho_Regions.Dispose();
                    HOperatorSet.Threshold(ho_ImageReducedss, out ho_Regions, this.yd, 255);
                    ho_ConnectedRegionss.Dispose();
                    HOperatorSet.Connection(ho_Regions, out ho_ConnectedRegionss);
                    ho_SelectedRegionss.Dispose();
                    HOperatorSet.SelectShapeStd(ho_ConnectedRegionss, out ho_SelectedRegionss,
                        "max_area", 70);
                    ho_RegionFillUps.Dispose();
                    HOperatorSet.FillUp(ho_SelectedRegionss, out ho_RegionFillUps);
                    ho_RegionDifferences.Dispose();
                    HOperatorSet.Difference(ho_RegionFillUps, ho_SelectedRegionss, out ho_RegionDifferences
                        );
                    ho_ConnectedRegionss1.Dispose();
                    HOperatorSet.Connection(ho_RegionDifferences, out ho_ConnectedRegionss1);
                    ho_SelectedRegionsss.Dispose();
                    HOperatorSet.SelectShape(ho_ConnectedRegionss1, out ho_SelectedRegionsss, (new HTuple("outer_radius")).TupleConcat(
                        "area"), "and", (new HTuple(this.cd)).TupleConcat(this.mj), (new HTuple(16)).TupleConcat(
                        400));
                    HOperatorSet.AreaCenter(ho_SelectedRegionsss, out hv_Areass, out hv_Rowss,
                        out hv_Columnss);
                    if ((int)(new HTuple((new HTuple(hv_Areass.TupleLength())).TupleGreaterEqual(
                        1))) != 0)
                    {
                        hv_s = 1;
                        HOperatorSet.SmallestCircle(ho_SelectedRegionsss, out hv_Rowss1, out hv_Columnss1,
                            out hv_Radiusss1);
                        ho_Circless1.Dispose();
                        HOperatorSet.GenCircle(out ho_Circless1, hv_Rowss1, hv_Columnss1, hv_Radiusss1);
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.Union2(ho_Regionzs1, ho_Circless1, out ExpTmpOutVar_0);
                            ho_Regionzs1.Dispose();
                            ho_Regionzs1 = ExpTmpOutVar_0;
                        }
                    }
                    HOperatorSet.VectorAngleToRigid(DRow1s, DColumn1s, 0, hv_Row1ss, hv_Column1ss,
                        hv_Angless, out hv_HomMat2Ds1);
                    ho_RegionAffineTranss1.Dispose();
                    HOperatorSet.AffineTransRegion(ho_Regionzs1, out ho_RegionAffineTranss1, hv_HomMat2Ds1,
                        "nearest_neighbor");
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.Union2(ho_Regionzs, ho_RegionAffineTranss1, out ExpTmpOutVar_0
                            );
                        ho_Regionzs.Dispose();
                        ho_Regionzs = ExpTmpOutVar_0;
                    }
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.Union2(ho_Regionzzx, ho_Regionzs, out ExpTmpOutVar_0);
                    ho_Regionzzx.Dispose();
                    ho_Regionzzx = ExpTmpOutVar_0;
                }
                HOperatorSet.ClearShapeModel(hv_ModelIDs);
                //HDevelopStop();
                ////xia
                //HOperatorSet.DrawRectangle2(hv_ExpDefaultWinHandle, out hv_Rowx, out hv_Columnx,
                //    out hv_Phix, out hv_Length1x, out hv_Length2x);

                hv_x = 0;
                ho_Rectanglex.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectanglex, DRowx, DColumnx, DPhix, DLength1x, DLength2x);
                HOperatorSet.ReadShapeModel(PathHelper.currentProductPath + @"\jingzix.shm", out hv_ModelIDx);
                HOperatorSet.FindShapeModel(Image, hv_ModelIDx, -1.57, 1.57, 0.3, 1, 0.5,
        "least_squares", 0, 0.9, out hv_Row1xx, out hv_Column1xx, out hv_Anglexx,
        out hv_Scorexx);
                if ((int)(new HTuple((new HTuple(hv_Scorexx.TupleLength())).TupleEqual(1))) != 0)
                {
                    HOperatorSet.VectorAngleToRigid(hv_Row1xx, hv_Column1xx, hv_Anglexx, DRow1x,
                       DColumn1x, 0, out hv_HomMat2Dx);
                    ho_ImageAffinTranxx.Dispose();
                    HOperatorSet.AffineTransImage(Image, out ho_ImageAffinTranxx, hv_HomMat2Dx,
                        "constant", "false");
                    ho_ImageReducedxx.Dispose();
                    HOperatorSet.ReduceDomain(ho_ImageAffinTranxx, ho_Rectanglex, out ho_ImageReducedxx
                        );
                    ho_Regionx.Dispose();
                    HOperatorSet.Threshold(ho_ImageReducedxx, out ho_Regionx, this.yd, 255);
                    ho_ConnectedRegionsx.Dispose();
                    HOperatorSet.Connection(ho_Regionx, out ho_ConnectedRegionsx);
                    ho_SelectedRegionsx.Dispose();
                    HOperatorSet.SelectShapeStd(ho_ConnectedRegionsx, out ho_SelectedRegionsx,
                        "max_area", 70);
                    ho_RegionFillUpx.Dispose();
                    HOperatorSet.FillUp(ho_SelectedRegionsx, out ho_RegionFillUpx);
                    ho_RegionDifferencex.Dispose();
                    HOperatorSet.Difference(ho_RegionFillUpx, ho_SelectedRegionsx, out ho_RegionDifferencex
                        );
                    ho_ConnectedRegionsx1.Dispose();
                    HOperatorSet.Connection(ho_RegionDifferencex, out ho_ConnectedRegionsx1);
                    ho_SelectedRegionsxx.Dispose();
                    HOperatorSet.SelectShape(ho_ConnectedRegionsx1, out ho_SelectedRegionsxx, (new HTuple("outer_radius")).TupleConcat(
                        "area"), "and", (new HTuple(this.cd)).TupleConcat(this.mj), (new HTuple(16)).TupleConcat(
                        400));
                    HOperatorSet.AreaCenter(ho_SelectedRegionsxx, out hv_Areaxx, out hv_Rowxx,
                        out hv_Columnxx);
                    if ((int)(new HTuple((new HTuple(hv_Areaxx.TupleLength())).TupleGreaterEqual(
                        1))) != 0)
                    {
                        hv_x = 1;
                        HOperatorSet.SmallestCircle(ho_SelectedRegionsxx, out hv_Rowxx1, out hv_Columnxx1,
                            out hv_Radiusxx1);
                        ho_Circlexx1.Dispose();
                        HOperatorSet.GenCircle(out ho_Circlexx1, hv_Rowxx1, hv_Columnxx1, hv_Radiusxx1);
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.Union2(ho_Regionzx1, ho_Circlexx1, out ExpTmpOutVar_0);
                            ho_Regionzx1.Dispose();
                            ho_Regionzx1 = ExpTmpOutVar_0;
                        }
                    }
                    HOperatorSet.VectorAngleToRigid(DRow1x, DColumn1x, 0, hv_Row1xx, hv_Column1xx,
                        hv_Anglexx, out hv_HomMat2Dx1);
                    ho_RegionAffineTranxx1.Dispose();
                    HOperatorSet.AffineTransRegion(ho_Regionzx1, out ho_RegionAffineTranxx1, hv_HomMat2Dx1,
                        "nearest_neighbor");
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.Union2(ho_Regionzx, ho_RegionAffineTranxx1, out ExpTmpOutVar_0
                            );
                        ho_Regionzx.Dispose();
                        ho_Regionzx = ExpTmpOutVar_0;
                    }
                }
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.Union2(ho_Regionzzx, ho_Regionzx, out ExpTmpOutVar_0);
                    ho_Regionzzx.Dispose();
                    ho_Regionzzx = ExpTmpOutVar_0;
                }
                HOperatorSet.ClearShapeModel(hv_ModelIDx);

                hv_m = new HTuple();
                hv_m = hv_m.TupleConcat(hv_y + hv_z);
                hv_m = hv_m.TupleConcat(hv_s + hv_x);
                hv_mm = hv_m.TupleMax();

                HOperatorSet.Union1(ho_Regionzzx, out RegionToDisp);

                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("数量");
                hv_result = hv_result.TupleConcat(hv_mm.D);
                result = hv_result.Clone();
                ho_Regionzx.Dispose();
                ho_Rectangley.Dispose();
                ho_Rectanglez.Dispose();
                ho_Rectangles.Dispose();
                ho_Rectanglex.Dispose();
                ho_Regionzzx.Dispose();
                ho_Regionzy.Dispose();
                ho_Regionzz.Dispose();
                ho_Regionzs.Dispose();
                ho_Regionzy1.Dispose();
                ho_Regionzz1.Dispose();
                ho_Regionzs1.Dispose();
                ho_Regionzx1.Dispose();
                ho_ImageAffinTransy.Dispose();
                ho_ImageReducedyy.Dispose();
                ho_Regiony.Dispose();
                ho_ConnectedRegionsy.Dispose();
                ho_SelectedRegionsy.Dispose();
                ho_RegionFillUpy.Dispose();
                ho_RegionDifferencey.Dispose();
                ho_ConnectedRegionsy1.Dispose();
                ho_SelectedRegionsyy.Dispose();
                ho_Circleyy1.Dispose();
                ho_RegionAffineTransy1.Dispose();
                ho_ImageAffinTransz.Dispose();
                ho_ImageReducedzz.Dispose();
                ho_Regionz.Dispose();
                ho_ConnectedRegionsz.Dispose();
                ho_SelectedRegionsz.Dispose();
                ho_RegionFillUpz.Dispose();
                ho_RegionDifferencez.Dispose();
                ho_ConnectedRegionsz1.Dispose();
                ho_SelectedRegionszz.Dispose();
                ho_Circlezz1.Dispose();
                ho_RegionAffineTransz1.Dispose();
                ho_ImageAffinTranss.Dispose();
                ho_ImageReducedss.Dispose();
                ho_Regions.Dispose();
                ho_ConnectedRegionss.Dispose();
                ho_SelectedRegionss.Dispose();
                ho_RegionFillUps.Dispose();
                ho_RegionDifferences.Dispose();
                ho_ConnectedRegionss1.Dispose();
                ho_SelectedRegionsss.Dispose();
                ho_Circless1.Dispose();
                ho_RegionAffineTranss1.Dispose();
                ho_ImageAffinTranxx.Dispose();
                ho_ImageReducedxx.Dispose();
                ho_Regionx.Dispose();
                ho_ConnectedRegionsx.Dispose();
                ho_SelectedRegionsx.Dispose();
                ho_RegionFillUpx.Dispose();
                ho_RegionDifferencex.Dispose();
                ho_ConnectedRegionsx1.Dispose();
                ho_SelectedRegionsxx.Dispose();
                ho_Circlexx1.Dispose();
                ho_RegionAffineTranxx1.Dispose();
                algorithm.Region.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("数量");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();

                ho_Regionzx.Dispose();
                ho_Rectangley.Dispose();
                ho_Rectanglez.Dispose();
                ho_Rectangles.Dispose();
                ho_Rectanglex.Dispose();
                ho_Regionzzx.Dispose();
                ho_Regionzy.Dispose();
                ho_Regionzz.Dispose();
                ho_Regionzs.Dispose();
                ho_Regionzy1.Dispose();
                ho_Regionzz1.Dispose();
                ho_Regionzs1.Dispose();
                ho_Regionzx1.Dispose();
                ho_ImageAffinTransy.Dispose();
                ho_ImageReducedyy.Dispose();
                ho_Regiony.Dispose();
                ho_ConnectedRegionsy.Dispose();
                ho_SelectedRegionsy.Dispose();
                ho_RegionFillUpy.Dispose();
                ho_RegionDifferencey.Dispose();
                ho_ConnectedRegionsy1.Dispose();
                ho_SelectedRegionsyy.Dispose();
                ho_Circleyy1.Dispose();
                ho_RegionAffineTransy1.Dispose();
                ho_ImageAffinTransz.Dispose();
                ho_ImageReducedzz.Dispose();
                ho_Regionz.Dispose();
                ho_ConnectedRegionsz.Dispose();
                ho_SelectedRegionsz.Dispose();
                ho_RegionFillUpz.Dispose();
                ho_RegionDifferencez.Dispose();
                ho_ConnectedRegionsz1.Dispose();
                ho_SelectedRegionszz.Dispose();
                ho_Circlezz1.Dispose();
                ho_RegionAffineTransz1.Dispose();
                ho_ImageAffinTranss.Dispose();
                ho_ImageReducedss.Dispose();
                ho_Regions.Dispose();
                ho_ConnectedRegionss.Dispose();
                ho_SelectedRegionss.Dispose();
                ho_RegionFillUps.Dispose();
                ho_RegionDifferences.Dispose();
                ho_ConnectedRegionss1.Dispose();
                ho_SelectedRegionsss.Dispose();
                ho_Circless1.Dispose();
                ho_RegionAffineTranss1.Dispose();
                ho_ImageAffinTranxx.Dispose();
                ho_ImageReducedxx.Dispose();
                ho_Regionx.Dispose();
                ho_ConnectedRegionsx.Dispose();
                ho_SelectedRegionsx.Dispose();
                ho_RegionFillUpx.Dispose();
                ho_RegionDifferencex.Dispose();
                ho_ConnectedRegionsx1.Dispose();
                ho_SelectedRegionsxx.Dispose();
                ho_Circlexx1.Dispose();
                ho_RegionAffineTranxx1.Dispose();
                algorithm.Region.Dispose();

            }
            finally
            {
                ho_Regionzx.Dispose();
                ho_Rectangley.Dispose();
                ho_Rectanglez.Dispose();
                ho_Rectangles.Dispose();
                ho_Rectanglex.Dispose();
                ho_Regionzzx.Dispose();
                ho_Regionzy.Dispose();
                ho_Regionzz.Dispose();
                ho_Regionzs.Dispose();
                ho_Regionzy1.Dispose();
                ho_Regionzz1.Dispose();
                ho_Regionzs1.Dispose();
                ho_Regionzx1.Dispose();
                ho_ImageAffinTransy.Dispose();
                ho_ImageReducedyy.Dispose();
                ho_Regiony.Dispose();
                ho_ConnectedRegionsy.Dispose();
                ho_SelectedRegionsy.Dispose();
                ho_RegionFillUpy.Dispose();
                ho_RegionDifferencey.Dispose();
                ho_ConnectedRegionsy1.Dispose();
                ho_SelectedRegionsyy.Dispose();
                ho_Circleyy1.Dispose();
                ho_RegionAffineTransy1.Dispose();
                ho_ImageAffinTransz.Dispose();
                ho_ImageReducedzz.Dispose();
                ho_Regionz.Dispose();
                ho_ConnectedRegionsz.Dispose();
                ho_SelectedRegionsz.Dispose();
                ho_RegionFillUpz.Dispose();
                ho_RegionDifferencez.Dispose();
                ho_ConnectedRegionsz1.Dispose();
                ho_SelectedRegionszz.Dispose();
                ho_Circlezz1.Dispose();
                ho_RegionAffineTransz1.Dispose();
                ho_ImageAffinTranss.Dispose();
                ho_ImageReducedss.Dispose();
                ho_Regions.Dispose();
                ho_ConnectedRegionss.Dispose();
                ho_SelectedRegionss.Dispose();
                ho_RegionFillUps.Dispose();
                ho_RegionDifferences.Dispose();
                ho_ConnectedRegionss1.Dispose();
                ho_SelectedRegionsss.Dispose();
                ho_Circless1.Dispose();
                ho_RegionAffineTranss1.Dispose();
                ho_ImageAffinTranxx.Dispose();
                ho_ImageReducedxx.Dispose();
                ho_Regionx.Dispose();
                ho_ConnectedRegionsx.Dispose();
                ho_SelectedRegionsx.Dispose();
                ho_RegionFillUpx.Dispose();
                ho_RegionDifferencex.Dispose();
                ho_ConnectedRegionsx1.Dispose();
                ho_SelectedRegionsxx.Dispose();
                ho_Circlexx1.Dispose();
                ho_RegionAffineTranxx1.Dispose();
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


