using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
    [Serializable]
    class waiguan1 : ImageTools
    {
        #region ROI
        [NonSerialized]
        private HTuple hv_Rows = new HTuple();
        [NonSerialized]
        private HTuple hv_Columns = new HTuple();
        [NonSerialized]
        private HTuple hv_Length1s = new HTuple();
        [NonSerialized]
        private HTuple hv_Length2s = new HTuple();


        public double DRows { set; get; }
        public double DColumns { set; get; }
        public double DLength1s { set; get; }
        public double DLength2s { set; get; }






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
        [NonSerialized]
        private HTuple nu4 = new HTuple();
        [NonSerialized]
        private HTuple nu5 = new HTuple();
        [NonSerialized]
        private HTuple nu6 = new HTuple();
        [NonSerialized]
        private HTuple nu7 = new HTuple();
        public double mj { set; get; }
        public double cd { set; get; }

        public double yd { set; get; }

        public double bjz { set; get; }
        public double kdz { set; get; }
        public double pzz { set; get; }
        public double afs { set; get; }
        public double apz { set; get; }




        [NonSerialized]
        private HTuple hv_ModelID = new HTuple();
        [NonSerialized]
        private HTuple hv_Area1s = new HTuple();
        [NonSerialized]
        private HTuple hv_Row1s = new HTuple();
        [NonSerialized]
        private HTuple hv_Column1s = new HTuple();
        public double DArea1s { set; get; }
        public double DRow1s { set; get; }
        public double DColumn1s { set; get; }



        #endregion
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public waiguan1()
        {
            //RegionToDisp = Image;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public waiguan1(HObject Image, Algorithm al)
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

        public override void draw()
        {

            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            thv = thresholdValue.D;
            //disp_message(this.LWindowHandle, "请依次在右，左，上，下镜子中绘制检测区域", "window", 12, 12, "black", "true");

            HObject ho_Rectanglet, ho_ImageReduced, ho_ModelImages, ho_ModelRegions;

            // Local control variables 

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectanglet);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_ModelImages);
            HOperatorSet.GenEmptyObj(out ho_ModelRegions);


            HOperatorSet.DrawRectangle1(this.LWindowHandle, out hv_Rows, out hv_Columns, out hv_Length1s, out hv_Length2s);


            this.DRows = hv_Rows.D;
            this.DColumns = hv_Columns.D;
            this.DLength1s = hv_Length1s.D;
            this.DLength2s = hv_Length2s.D;

            ho_Rectanglet.Dispose();
            HOperatorSet.GenRectangle1(out ho_Rectanglet, DRows, DColumns, DLength1s, DLength2s);

            ho_ImageReduced.Dispose();
            HOperatorSet.ReduceDomain(this.Image, ho_Rectanglet, out ho_ImageReduced);
            HOperatorSet.CreateShapeModel(ho_ImageReduced, "auto", -6.29, 6.29, "auto", "auto",
                "use_polarity", "auto", "auto", out hv_ModelID);
            ho_ModelImages.Dispose(); ho_ModelRegions.Dispose();
            HOperatorSet.InspectShapeModel(ho_ImageReduced, out ho_ModelImages, out ho_ModelRegions,
                10, 30);
            HOperatorSet.AreaCenter(ho_Rectanglet, out hv_Area1s, out hv_Row1s, out hv_Column1s);
            this.DArea1s = hv_Area1s.D;
            this.DRow1s = hv_Row1s.D;
            this.DColumn1s = hv_Column1s.D;


            HOperatorSet.WriteShapeModel(hv_ModelID, PathHelper.currentProductPath + @"\waiguan1.shm");
            ho_Rectanglet.Dispose();
            ho_ImageReduced.Dispose();
            ho_ModelImages.Dispose();
            ho_ModelRegions.Dispose();


            HTuple hv_qxmj = new HTuple(), hv_qxcd = new HTuple(), hv_ydz = new HTuple(), hv_bydz = new HTuple(), hv_kddz = new HTuple(), hv_pzzz = new HTuple();
            HTuple hv_afsz = new HTuple(), hv_apzz = new HTuple();
            

            //

            disp_message(this.LWindowHandle, "请输入亮色区最小缺陷面积（参考值10）,以回车键结束", "window", 10, 10, "black", "true");
            HOperatorSet.SetTposition(this.LWindowHandle, 80, 150);
            HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_qxmj);

            try
            {
                HOperatorSet.TupleNumber(hv_qxmj, out nu1);
                this.mj = nu1.D;
            }
            catch
            {
                this.mj = 10;
            }

            disp_message(this.LWindowHandle, "请输入腐蚀膨胀值宽（参考值11）,以回车键结束", "window", 45, 10, "black", "true");
            HOperatorSet.SetTposition(this.LWindowHandle, 160, 150);
            HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_qxcd);

            try
            {
                HOperatorSet.TupleNumber(hv_qxcd, out nu);
                this.cd = nu.D;
            }
            catch
            {
                this.cd = 11;
            }


            disp_message(this.LWindowHandle, "请输入亮处高（参考值100）,以回车键结束", "window", 80, 10, "black", "true");
            HOperatorSet.SetTposition(this.LWindowHandle, 240, 150);
            HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_ydz);

            try
            {
                HOperatorSet.TupleNumber(hv_ydz, out nu2);
                this.yd = nu2.D;
            }
            catch
            {
                this.yd = 100;
            }

            disp_message(this.LWindowHandle, "请输入亮处宽（参考值30）,以回车键结束", "window", 115, 10, "black", "true");
            HOperatorSet.SetTposition(this.LWindowHandle, 320, 150);
            HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_bydz);

            try
            {
                HOperatorSet.TupleNumber(hv_bydz, out nu3);
                this.bjz = nu3.D;
            }
            catch
            {
                this.bjz = 30;
            }

            disp_message(this.LWindowHandle, "请输入暗色区最小缺陷面积（参考值10）,以回车键结束", "window", 150, 10, "black", "true");
            HOperatorSet.SetTposition(this.LWindowHandle, 400, 150);
            HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_kddz);

            try
            {
                HOperatorSet.TupleNumber(hv_kddz, out nu4);
                this.kdz = nu4.D;
            }
            catch
            {
                this.kdz = 10;
            }

            disp_message(this.LWindowHandle, "请输入腐蚀膨胀值宽（参考值21）,以回车键结束", "window", 185, 10, "black", "true");
            HOperatorSet.SetTposition(this.LWindowHandle, 480, 150);
            HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_pzzz);

            try
            {
                HOperatorSet.TupleNumber(hv_pzzz, out nu5);
                this.pzz = nu5.D;
            }
            catch
            {
                this.pzz = 21;
            }
            disp_message(this.LWindowHandle, "请输入暗腐蚀（参考值7.5）,以回车键结束", "window", 220, 10, "black", "true");
            HOperatorSet.SetTposition(this.LWindowHandle, 560, 150);
            HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_afsz);

            try
            {
                HOperatorSet.TupleNumber(hv_afsz, out nu6);
                this.afs = nu6.D;
            }
            catch
            {
                this.afs = 7.5;
            }
            disp_message(this.LWindowHandle, "请输入暗膨胀值（参考值10.5）,以回车键结束", "window", 255, 10, "black", "true");
            HOperatorSet.SetTposition(this.LWindowHandle, 640, 150);
            HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_apzz);

            try
            {
                HOperatorSet.TupleNumber(hv_apzz, out nu7);
                this.apz = nu7.D;
            }
            catch
            {
                this.apz = 10.5;
            }
        }

        private void action()
        {
            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_Rectanglem;
            HObject ho_Regionm;
            HObject ho_Regionn, ho_Regiont, ho_ImageAffinTrans = null;
            HObject ho_ImageReduced = null, ho_Region = null, ho_RegionDilation1 = null;
            HObject ho_ConnectedRegions2 = null, ho_SelectedRegions2 = null;
            HObject ho_RegionTrans2 = null, ho_RegionDifference2 = null;
            HObject ho_RegionErosion1 = null, ho_ConnectedRegions3 = null;
            HObject ho_SelectedRegions3 = null, ho_RegionTrans3 = null;
            HObject ho_ImageReduced3 = null, ho_Region3 = null, ho_ConnectedRegions4 = null;
            HObject ho_SelectedRegions4 = null, ho_RegionErosion = null;
            HObject ho_RegionDilation = null, ho_RegionClosing = null, ho_ConnectedRegions = null;
            HObject ho_SelectedRegions = null, ho_ObjectSelected = null;
            HObject ho_RegionTrans = null, ho_ImageReduced1 = null, ho_Region1 = null;
            HObject ho_RegionDifference = null, ho_ObjectSelected1 = null;
            HObject ho_RegionTrans1 = null, ho_ImageReduced2 = null, ho_Region2 = null;
            HObject ho_RegionDifference1 = null, ho_ConnectedRegions1 = null;
            HObject ho_SelectedRegions1 = null, ho_RegionAffineTrans = null;

            // Local control variables 

            HTuple hv_m = null, hv_n = null, hv_m1 = null, hv_n1 = null;
            HTuple hv_h1 = null, hv_w1 = null, hv_h2 = null, hv_w2 = null;
            HTuple hv_Row = null, hv_Column = null, hv_Angle = null;
            HTuple hv_Score = null, hv_HomMat2D = new HTuple(), hv_Area1 = new HTuple();
            HTuple hv_Row4 = new HTuple(), hv_Column4 = new HTuple();
            HTuple hv_Number = new HTuple(), hv_Row1 = new HTuple();
            HTuple hv_Column1 = new HTuple(), hv_Row2 = new HTuple();
            HTuple hv_Column2 = new HTuple(), hv_Row11 = new HTuple();
            HTuple hv_Column11 = new HTuple(), hv_Row21 = new HTuple();
            HTuple hv_Column21 = new HTuple(), hv_Area = new HTuple();
            HTuple hv_Row3 = new HTuple(), hv_Column3 = new HTuple();
            HTuple hv_HomMat2D1 = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectanglem);
            HOperatorSet.GenEmptyObj(out ho_Regionm);
            HOperatorSet.GenEmptyObj(out ho_Regionn);
            HOperatorSet.GenEmptyObj(out ho_Regiont);
            HOperatorSet.GenEmptyObj(out ho_ImageAffinTrans);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_RegionDilation1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans2);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference2);
            HOperatorSet.GenEmptyObj(out ho_RegionErosion1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions3);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions3);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans3);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced3);
            HOperatorSet.GenEmptyObj(out ho_Region3);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions4);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions4);
            HOperatorSet.GenEmptyObj(out ho_RegionErosion);
            HOperatorSet.GenEmptyObj(out ho_RegionDilation);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected1);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans1);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced2);
            HOperatorSet.GenEmptyObj(out ho_Region2);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_RegionAffineTrans);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {
                hv_m = 0;
                hv_n = 0;
                hv_m1 = 0;
                hv_n1 = 0;
                hv_h1 = 0;
                hv_w1 = 0;
                hv_h2 = 0;
                hv_w2 = 0;
                ho_Regionm.Dispose();
                HOperatorSet.GenRegionPoints(out ho_Regionm, DRows, DColumns);
                ho_Regionn.Dispose();
                HOperatorSet.GenRegionPoints(out ho_Regionn, DRows, DColumns);
                ho_Regiont.Dispose();
                HOperatorSet.GenRegionPoints(out ho_Regiont, DRows, DColumns);
                ho_Rectanglem.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectanglem, DRows, DColumns, DLength1s, DLength2s);
                HOperatorSet.ReadShapeModel(PathHelper.currentProductPath + @"\waiguan1.shm", out hv_ModelID);
                HOperatorSet.FindShapeModel(Image, hv_ModelID, -6.29, 6.29, 0, 1, 0.5, "least_squares",
        0, 0.9, out hv_Row, out hv_Column, out hv_Angle, out hv_Score);
                if ((int)(new HTuple((new HTuple(hv_Score.TupleLength())).TupleEqual(1))) != 0)
                {
                    HOperatorSet.VectorAngleToRigid(hv_Row, hv_Column, hv_Angle, DRow1s, DColumn1s,
                        0, out hv_HomMat2D);
                    ho_ImageAffinTrans.Dispose();
                    HOperatorSet.AffineTransImage(Image, out ho_ImageAffinTrans, hv_HomMat2D,
                        "constant", "false");
                    ho_ImageReduced.Dispose();
                    HOperatorSet.ReduceDomain(ho_ImageAffinTrans, ho_Rectanglem, out ho_ImageReduced
                        );
                    ho_Region.Dispose();
                    HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, thv, 255);
                    //
                    ho_RegionDilation1.Dispose();
                    HOperatorSet.DilationCircle(ho_Region, out ho_RegionDilation1, this.afs);
                    ho_ConnectedRegions2.Dispose();
                    HOperatorSet.Connection(ho_RegionDilation1, out ho_ConnectedRegions2);
                    ho_SelectedRegions2.Dispose();
                    HOperatorSet.SelectShapeStd(ho_ConnectedRegions2, out ho_SelectedRegions2,
                        "max_area", 70);
                    ho_RegionTrans2.Dispose();
                    HOperatorSet.ShapeTrans(ho_SelectedRegions2, out ho_RegionTrans2, "convex");
                    ho_RegionDifference2.Dispose();
                    HOperatorSet.Difference(ho_RegionTrans2, ho_SelectedRegions2, out ho_RegionDifference2
                        );
                    ho_RegionErosion1.Dispose();
                    HOperatorSet.ErosionCircle(ho_RegionDifference2, out ho_RegionErosion1, this.apz);
                    ho_ConnectedRegions3.Dispose();
                    HOperatorSet.Connection(ho_RegionErosion1, out ho_ConnectedRegions3);
                    ho_SelectedRegions3.Dispose();
                    HOperatorSet.SelectShapeStd(ho_ConnectedRegions3, out ho_SelectedRegions3,
                        "max_area", 70);
                    ho_RegionTrans3.Dispose();
                    HOperatorSet.ShapeTrans(ho_SelectedRegions3, out ho_RegionTrans3, "convex");
                    ho_ImageReduced3.Dispose();
                    HOperatorSet.ReduceDomain(ho_ImageReduced, ho_RegionTrans3, out ho_ImageReduced3
                        );
                    ho_Region3.Dispose();
                    HOperatorSet.Threshold(ho_ImageReduced3, out ho_Region3, thv, 255);
                    ho_ConnectedRegions4.Dispose();
                    HOperatorSet.Connection(ho_Region3, out ho_ConnectedRegions4);
                    ho_SelectedRegions4.Dispose();
                    HOperatorSet.SelectShape(ho_ConnectedRegions4, out ho_SelectedRegions4, "area",
                        "and", this.kdz, 9900999);
                    HOperatorSet.AreaCenter(ho_SelectedRegions4, out hv_Area1, out hv_Row4, out hv_Column4);
                    if ((int)(new HTuple((new HTuple(hv_Area1.TupleLength())).TupleGreater(0))) != 0)
                    {
                        hv_m1 = hv_Area1.TupleMax();
                        hv_n1 = hv_Area1.TupleSum();
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.Union2(ho_Regionn, ho_SelectedRegions4, out ExpTmpOutVar_0);
                            ho_Regionn.Dispose();
                            ho_Regionn = ExpTmpOutVar_0;
                        }
                    }
                    else
                    {
                        hv_m1 = 0;
                        hv_n1 = 0;
                    }


//
                    ho_RegionErosion.Dispose();
                    HOperatorSet.ErosionRectangle1(ho_Region, out ho_RegionErosion, this.cd, this.pzz);
                    ho_RegionDilation.Dispose();
                    HOperatorSet.DilationRectangle1(ho_RegionErosion, out ho_RegionDilation, this.cd,
                        this.pzz);
                    ho_RegionClosing.Dispose();
                    HOperatorSet.ClosingRectangle1(ho_RegionDilation, out ho_RegionClosing, this.yd,
                        this.bjz);
                    ho_ConnectedRegions.Dispose();
                    HOperatorSet.Connection(ho_RegionClosing, out ho_ConnectedRegions);
                    ho_SelectedRegions.Dispose();
                    HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, (new HTuple("height")).TupleConcat(
                        "width"), "and", (new HTuple(this.yd)).TupleConcat(this.bjz), (new HTuple(9999)).TupleConcat(
                        5000));
                    HOperatorSet.CountObj(ho_SelectedRegions, out hv_Number);
                    if ((int)(new HTuple(hv_Number.TupleEqual(2))) != 0)
                    {
                        ho_ObjectSelected.Dispose();
                        HOperatorSet.SelectObj(ho_SelectedRegions, out ho_ObjectSelected, 1);
                        ho_RegionTrans.Dispose();
                        HOperatorSet.ShapeTrans(ho_ObjectSelected, out ho_RegionTrans, "convex");
                        HOperatorSet.SmallestRectangle1(ho_RegionTrans, out hv_Row1, out hv_Column1,
                            out hv_Row2, out hv_Column2);
                        hv_h1 = hv_Row2 - hv_Row1;
                        hv_w1 = hv_Column2 - hv_Column1;
                        ho_ImageReduced1.Dispose();
                        HOperatorSet.ReduceDomain(ho_ImageReduced, ho_RegionTrans, out ho_ImageReduced1
                            );
                        ho_Region1.Dispose();
                        HOperatorSet.Threshold(ho_ImageReduced1, out ho_Region1, thv, 255);
                        ho_RegionDifference.Dispose();
                        HOperatorSet.Difference(ho_RegionTrans, ho_Region1, out ho_RegionDifference
                            );
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.Union2(ho_Regionm, ho_RegionDifference, out ExpTmpOutVar_0);
                            ho_Regionm.Dispose();
                            ho_Regionm = ExpTmpOutVar_0;
                        }
                        ho_ObjectSelected1.Dispose();
                        HOperatorSet.SelectObj(ho_SelectedRegions, out ho_ObjectSelected1, 2);
                        ho_RegionTrans1.Dispose();
                        HOperatorSet.ShapeTrans(ho_ObjectSelected1, out ho_RegionTrans1, "convex");
                        HOperatorSet.SmallestRectangle1(ho_RegionTrans1, out hv_Row11, out hv_Column11,
                            out hv_Row21, out hv_Column21);
                        hv_h2 = hv_Row21 - hv_Row11;
                        hv_w2 = hv_Column21 - hv_Column11;
                        ho_ImageReduced2.Dispose();
                        HOperatorSet.ReduceDomain(ho_ImageReduced, ho_RegionTrans1, out ho_ImageReduced2
                            );
                        ho_Region2.Dispose();
                        HOperatorSet.Threshold(ho_ImageReduced2, out ho_Region2, thv, 255);
                        ho_RegionDifference1.Dispose();
                        HOperatorSet.Difference(ho_RegionTrans1, ho_Region2, out ho_RegionDifference1
                            );
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.Union2(ho_Regionm, ho_RegionDifference1, out ExpTmpOutVar_0);
                            ho_Regionm.Dispose();
                            ho_Regionm = ExpTmpOutVar_0;
                        }
                        ho_ConnectedRegions1.Dispose();
                        HOperatorSet.Connection(ho_Regionm, out ho_ConnectedRegions1);
                        ho_SelectedRegions1.Dispose();
                        HOperatorSet.SelectShape(ho_ConnectedRegions1, out ho_SelectedRegions1, "area",
                            "and", this.mj, 99999);
                        HOperatorSet.AreaCenter(ho_SelectedRegions1, out hv_Area, out hv_Row3, out hv_Column3);
                        if ((int)(new HTuple((new HTuple(hv_Area.TupleLength())).TupleGreater(0))) != 0)
                        {
                            hv_m = hv_Area.TupleMax();
                            hv_n = hv_Area.TupleSum();
                            {
                                HObject ExpTmpOutVar_0;
                                HOperatorSet.Union2(ho_Regionn, ho_SelectedRegions1, out ExpTmpOutVar_0
                                    );
                                ho_Regionn.Dispose();
                                ho_Regionn = ExpTmpOutVar_0;
                            }
                        }
                        else
                        {
                            hv_m = 0;
                            hv_n = 0;
                        }
                    }

                    HOperatorSet.VectorAngleToRigid(DRow1s, DColumn1s, 0, hv_Row, hv_Column,
                        hv_Angle, out hv_HomMat2D1);
                    ho_RegionAffineTrans.Dispose();
                    HOperatorSet.AffineTransRegion(ho_Regionn, out ho_RegionAffineTrans, hv_HomMat2D1,
                        "nearest_neighbor");
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.Union2(ho_Regiont, ho_RegionAffineTrans, out ExpTmpOutVar_0);
                        ho_Regiont.Dispose();
                        ho_Regiont = ExpTmpOutVar_0;
                    }

                }
                else
                {
                    hv_h1 = 0;
                    hv_w1 = 0;
                    hv_h2 = 0;
                    hv_w2 = 0;
                    hv_m = 99999;
                    hv_n = 99999;
                    hv_m1 = 99999;
                    hv_n1 = 99999;



                }


                HOperatorSet.ClearShapeModel(hv_ModelID);
                HOperatorSet.Union1(ho_Regiont, out RegionToDisp);

                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("亮色区缺陷总面积");
                hv_result = hv_result.TupleConcat(hv_n.D);
                hv_result = hv_result.TupleConcat("亮色区最大缺陷面积");
                hv_result = hv_result.TupleConcat(hv_m.D);
                hv_result = hv_result.TupleConcat("暗色区缺陷总面积");
                hv_result = hv_result.TupleConcat(hv_n1.D);
                hv_result = hv_result.TupleConcat("暗色区最大缺陷面积");
                hv_result = hv_result.TupleConcat(hv_m1.D);
                hv_result = hv_result.TupleConcat("高1");
                hv_result = hv_result.TupleConcat(hv_h1.D);
                hv_result = hv_result.TupleConcat("宽1");
                hv_result = hv_result.TupleConcat(hv_w1.D);
                hv_result = hv_result.TupleConcat("高2");
                hv_result = hv_result.TupleConcat(hv_h2.D);
                hv_result = hv_result.TupleConcat("宽2");
                hv_result = hv_result.TupleConcat(hv_w2.D);

                result = hv_result.Clone();
                ho_Rectanglem.Dispose();
                ho_Regionm.Dispose();
                ho_Regionn.Dispose();
                ho_Regiont.Dispose();
                ho_ImageAffinTrans.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_RegionDilation1.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_RegionTrans2.Dispose();
                ho_RegionDifference2.Dispose();
                ho_RegionErosion1.Dispose();
                ho_ConnectedRegions3.Dispose();
                ho_SelectedRegions3.Dispose();
                ho_RegionTrans3.Dispose();
                ho_ImageReduced3.Dispose();
                ho_Region3.Dispose();
                ho_ConnectedRegions4.Dispose();
                ho_SelectedRegions4.Dispose();
                ho_RegionErosion.Dispose();
                ho_RegionDilation.Dispose();
                ho_RegionClosing.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_ObjectSelected.Dispose();
                ho_RegionTrans.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region1.Dispose();
                ho_RegionDifference.Dispose();
                ho_ObjectSelected1.Dispose();
                ho_RegionTrans1.Dispose();
                ho_ImageReduced2.Dispose();
                ho_Region2.Dispose();
                ho_RegionDifference1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionAffineTrans.Dispose();
                algorithm.Region.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("亮色区缺陷总面积");
                hv_result = hv_result.TupleConcat(9999999);
                hv_result = hv_result.TupleConcat("亮色区最大缺陷面积");
                hv_result = hv_result.TupleConcat(9999999);
                hv_result = hv_result.TupleConcat("暗色区缺陷总面积");
                hv_result = hv_result.TupleConcat(9999999);
                hv_result = hv_result.TupleConcat("暗色区最大缺陷面积");
                hv_result = hv_result.TupleConcat(9999999);
                hv_result = hv_result.TupleConcat("高1");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("宽1");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("高2");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("宽2");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();
                
                ho_Rectanglem.Dispose();
                ho_Regionm.Dispose();
                ho_Regionn.Dispose();
                ho_Regiont.Dispose();
                ho_ImageAffinTrans.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_RegionDilation1.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_RegionTrans2.Dispose();
                ho_RegionDifference2.Dispose();
                ho_RegionErosion1.Dispose();
                ho_ConnectedRegions3.Dispose();
                ho_SelectedRegions3.Dispose();
                ho_RegionTrans3.Dispose();
                ho_ImageReduced3.Dispose();
                ho_Region3.Dispose();
                ho_ConnectedRegions4.Dispose();
                ho_SelectedRegions4.Dispose();
                ho_RegionErosion.Dispose();
                ho_RegionDilation.Dispose();
                ho_RegionClosing.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_ObjectSelected.Dispose();
                ho_RegionTrans.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region1.Dispose();
                ho_RegionDifference.Dispose();
                ho_ObjectSelected1.Dispose();
                ho_RegionTrans1.Dispose();
                ho_ImageReduced2.Dispose();
                ho_Region2.Dispose();
                ho_RegionDifference1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionAffineTrans.Dispose();
                algorithm.Region.Dispose();

            }
            finally
            {
                ho_Rectanglem.Dispose();
                ho_Regionm.Dispose();
                ho_Regionn.Dispose();
                ho_Regiont.Dispose();
                ho_ImageAffinTrans.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_RegionDilation1.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_RegionTrans2.Dispose();
                ho_RegionDifference2.Dispose();
                ho_RegionErosion1.Dispose();
                ho_ConnectedRegions3.Dispose();
                ho_SelectedRegions3.Dispose();
                ho_RegionTrans3.Dispose();
                ho_ImageReduced3.Dispose();
                ho_Region3.Dispose();
                ho_ConnectedRegions4.Dispose();
                ho_SelectedRegions4.Dispose();
                ho_RegionErosion.Dispose();
                ho_RegionDilation.Dispose();
                ho_RegionClosing.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_ObjectSelected.Dispose();
                ho_RegionTrans.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region1.Dispose();
                ho_RegionDifference.Dispose();
                ho_ObjectSelected1.Dispose();
                ho_RegionTrans1.Dispose();
                ho_ImageReduced2.Dispose();
                ho_Region2.Dispose();
                ho_RegionDifference1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionAffineTrans.Dispose();
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





