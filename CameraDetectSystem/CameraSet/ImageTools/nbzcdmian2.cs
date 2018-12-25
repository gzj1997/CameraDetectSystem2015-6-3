using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
    [Serializable]
    class nbzcdmian2 : ImageTools
    {
        #region ROI
        [NonSerialized]
        private HTuple hv_Rowy1 = new HTuple();
        [NonSerialized]
        private HTuple hv_Columny1 = new HTuple();
        [NonSerialized]
        private HTuple hv_Phiy1 = new HTuple();



        public double DRowy1 { set; get; }
        public double DColumny1 { set; get; }
        public double DPhiy1 { set; get; }

        [NonSerialized]
        private HTuple hv_Rowy2 = new HTuple();
        [NonSerialized]
        private HTuple hv_Columny2 = new HTuple();
        [NonSerialized]
        private HTuple hv_Phiy2 = new HTuple();



        public double DRowy2 { set; get; }
        public double DColumny2 { set; get; }
        public double DPhiy2 { set; get; }


        [NonSerialized]
        private HTuple hv_Rowz = new HTuple();
        [NonSerialized]
        private HTuple hv_Columnz = new HTuple();
        [NonSerialized]
        private HTuple hv_Phiz = new HTuple();



        public double DRowz { set; get; }
        public double DColumnz { set; get; }
        public double DPhiz { set; get; }


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
        public double mj { set; get; }
        public double cd { set; get; }

        public double yd { set; get; }

        public double bjz { set; get; }
        public double kdz { set; get; }
        public double pzz { set; get; }





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
        public nbzcdmian2()
        {
            //RegionToDisp = Image;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public nbzcdmian2(HObject Image, Algorithm al)
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

            HObject ho_Circley1, ho_Circley2;
            HObject ho_RegionDifferencem;
            HObject ho_ImageReducedm, ho_Borderm, ho_UnionContoursm;
            HObject ho_SelectedContoursm, ho_Regionm = null, ho_Circlem = null;
            // Local control variables 

            HTuple hv_Numberm = null, hv_Rowym = new HTuple(), hv_Columnym = new HTuple();
            HTuple hv_Radiusym = new HTuple(), hv_ModelID = new HTuple();
            HTuple hv_Area1s = null, hv_Row1s = null, hv_Column1s = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Circley1);
            HOperatorSet.GenEmptyObj(out ho_Circley2);
             HOperatorSet.GenEmptyObj(out ho_RegionDifferencem);
            HOperatorSet.GenEmptyObj(out ho_ImageReducedm);
            HOperatorSet.GenEmptyObj(out ho_Borderm);
            HOperatorSet.GenEmptyObj(out ho_UnionContoursm);
            HOperatorSet.GenEmptyObj(out ho_SelectedContoursm);
            HOperatorSet.GenEmptyObj(out ho_Regionm);
            HOperatorSet.GenEmptyObj(out ho_Circlem);

            HOperatorSet.DrawCircle(this.LWindowHandle, out hv_Rowy1, out hv_Columny1,
        out hv_Phiy1);
            this.DRowy1 = hv_Rowy1.D;
            this.DColumny1 = hv_Columny1.D;
            this.DPhiy1 = hv_Phiy1.D;
            HOperatorSet.DrawCircle(this.LWindowHandle, out hv_Rowy2, out hv_Columny2,
        out hv_Phiy2);
            this.DRowy2 = hv_Rowy2.D;
            this.DColumny2 = hv_Columny2.D;
            this.DPhiy2 = hv_Phiy2.D;




            HOperatorSet.DrawCircle(this.LWindowHandle, out hv_Rowz, out hv_Columnz,
                out hv_Phiz);

            this.DRowz = hv_Rowz.D;
            this.DColumnz = hv_Columnz.D;
            this.DPhiz = hv_Phiz.D;


            HOperatorSet.DrawRectangle2(this.LWindowHandle, out hv_Rows, out hv_Columns,
                out hv_Phis, out hv_Length1s, out hv_Length2s);


            this.DRows = hv_Rows.D;
            this.DColumns = hv_Columns.D;
            this.DPhis = hv_Phis.D;
            this.DLength1s = hv_Length1s.D;
            this.DLength2s = hv_Length2s.D;


            ho_Circley1.Dispose();
            HOperatorSet.GenCircle(out ho_Circley1, DRowy1, DColumny1, DPhiy1);
             ho_Circley2.Dispose();
             HOperatorSet.GenCircle(out ho_Circley2, DRowy2, DColumny2, DPhiy2);
             ho_RegionDifferencem.Dispose();
             HOperatorSet.Difference(ho_Circley1, ho_Circley2, out ho_RegionDifferencem);
             ho_ImageReducedm.Dispose();
             HOperatorSet.ReduceDomain(this.Image, ho_RegionDifferencem, out ho_ImageReducedm
                 );
             ho_Borderm.Dispose();
             HOperatorSet.ThresholdSubPix(ho_ImageReducedm, out ho_Borderm, thv);
             ho_UnionContoursm.Dispose();
             HOperatorSet.UnionAdjacentContoursXld(ho_Borderm, out ho_UnionContoursm, 10,
                 1, "attr_keep");
             ho_SelectedContoursm.Dispose();
             HOperatorSet.SelectContoursXld(ho_UnionContoursm, out ho_SelectedContoursm, "contour_length",
                 200, 2000000, -0.5, 0.5);

             HOperatorSet.CountObj(ho_SelectedContoursm, out hv_Numberm);
             if ((int)(new HTuple(hv_Numberm.TupleEqual(1))) != 0)
             {
                 ho_Regionm.Dispose();
                 HOperatorSet.GenRegionContourXld(ho_SelectedContoursm, out ho_Regionm, "filled");
                 HOperatorSet.SmallestCircle(ho_Regionm, out hv_Rowym, out hv_Columnym, out hv_Radiusym);
                 ho_Circlem.Dispose();
                 HOperatorSet.GenCircle(out ho_Circlem, hv_Rowym, hv_Columnym, hv_Radiusym);
                 HOperatorSet.AreaCenter(ho_Circlem, out hv_Area1s, out hv_Row1s, out hv_Column1s);
                 this.DArea1s = hv_Area1s.D;
                 this.DRow1s = hv_Row1s.D;
                 this.DColumn1s = hv_Column1s.D;
                 HOperatorSet.CreateShapeModelXld(ho_SelectedContoursm, "auto", -6.29, 6.29,
                     "auto", "auto", "ignore_local_polarity", 30, out hv_ModelID);
                 HOperatorSet.WriteShapeModel(hv_ModelID, PathHelper.currentProductPath + @"\翻边外壁.shm");
             }
            
            HOperatorSet.WriteShapeModel(hv_ModelID, PathHelper.currentProductPath + @"\quekou.shm");
            ho_Circley1.Dispose();
            ho_Circley2.Dispose();
            ho_RegionDifferencem.Dispose();
            ho_ImageReducedm.Dispose();
            ho_Borderm.Dispose();
            ho_UnionContoursm.Dispose();
            ho_SelectedContoursm.Dispose();
            ho_Regionm.Dispose();
            ho_Circlem.Dispose();


            HTuple hv_qxmj = new HTuple(), hv_qxcd = new HTuple(), hv_ydz = new HTuple(), hv_bydz = new HTuple(), hv_kddz = new HTuple(), hv_pzzz = new HTuple();


            //
            int xx = 0;
            HTuple nu1 = new HTuple();
            while (xx == 0)
            {
                disp_message(this.LWindowHandle, "请输入最小面积（参考值10）,以回车键结束", "window", 10, 10, "black", "true");
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
            //int xy = 0;
            //HTuple nu = new HTuple();
            //while (xy == 0)
            //{
            //    disp_message(this.LWindowHandle, "请输入亮色灰度值（参考值245）,以回车键结束", "window", 45, 10, "black", "true");
            //    HOperatorSet.SetTposition(this.LWindowHandle, 160, 150);
            //    HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_qxcd);

            //    try
            //    {
            //        HOperatorSet.TupleNumber(hv_qxcd, out nu);
            //        xy = 1;
            //        this.cd = nu.D;
            //    }
            //    catch
            //    {
            //    }
            //}
            int xz = 0;
            HTuple nu2 = new HTuple();
            while (xz == 0)
            {
                disp_message(this.LWindowHandle, "请输入模糊值（参考值15）,以回车键结束", "window", 80, 10, "black", "true");
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
                disp_message(this.LWindowHandle, "请输入边界值（参考值35）,以回车键结束", "window", 115, 10, "black", "true");
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
            //int xz2 = 0;
            //HTuple nu4 = new HTuple();
            //while (xz2 == 0)
            //{
            //    disp_message(this.LWindowHandle, "请输入边界腐蚀（参考值5.5）,以回车键结束", "window", 150, 10, "black", "true");
            //    HOperatorSet.SetTposition(this.LWindowHandle, 400, 150);
            //    HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_kddz);

            //    try
            //    {
            //        HOperatorSet.TupleNumber(hv_kddz, out nu4);
            //        xz2 = 1;
            //        this.kdz = nu4.D;
            //    }
            //    catch
            //    {
            //    }
            //}
            //int xz3= 0;
            //HTuple nu5= new HTuple();
            //while (xz3 == 0)
            //{
            //    disp_message(this.LWindowHandle, "请输入膨胀值（参考值10.5）,以回车键结束", "window", 185, 10, "black", "true");
            //    HOperatorSet.SetTposition(this.LWindowHandle, 480, 150);
            //    HOperatorSet.ReadString(this.LWindowHandle, "", 32, out hv_pzzz);

            //    try
            //    {
            //        HOperatorSet.TupleNumber(hv_pzzz, out nu5);
            //        xz3 = 1;
            //        this.pzz = nu5.D;
            //    }
            //    catch
            //    {
            //    }
            //}
        }

        private void action()
        {
            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_Circley1;
            HObject ho_Circley3, ho_Rectanglet;
            HObject ho_Region, ho_Region1, ho_Region2, ho_RegionDifference;
            HObject ho_RegionDifference1, ho_ImageAffinTrans = null, ho_ImageReduced = null;
            HObject ho_ImageMean = null, ho_RegionDynThresh = null, ho_RegionDynThresh1 = null;
            HObject ho_ConnectedRegions = null, ho_SelectedRegions = null;
            HObject ho_RegionAffineTrans = null;

            // Local control variables 

            HTuple hv_ModelID = new HTuple();
            HTuple hv_m = null, hv_n = null;
            HTuple hv_Row = null, hv_Column = null, hv_Angle = null;
            HTuple hv_Score = null, hv_HomMat2D = new HTuple(), hv_Area = new HTuple();
            HTuple hv_Row1 = new HTuple(), hv_Column1 = new HTuple();
            HTuple hv_HomMat2D1 = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Circley1);
            HOperatorSet.GenEmptyObj(out ho_Circley3);
            HOperatorSet.GenEmptyObj(out ho_Rectanglet);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_Region2);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference1);
            HOperatorSet.GenEmptyObj(out ho_ImageAffinTrans);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_ImageMean);
            HOperatorSet.GenEmptyObj(out ho_RegionDynThresh);
            HOperatorSet.GenEmptyObj(out ho_RegionDynThresh1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionAffineTrans);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {
                hv_m = 0;
                hv_n = 0;
                ho_Region.Dispose();
                HOperatorSet.GenRegionPoints(out ho_Region, hv_Rowy1, hv_Columny1);
                ho_Region1.Dispose();
                HOperatorSet.GenRegionPoints(out ho_Region1, hv_Rowy1, hv_Columny1);
                ho_Region2.Dispose();
                HOperatorSet.GenRegionPoints(out ho_Region2, hv_Rowy1, hv_Columny1);

                ho_Circley1.Dispose();
                HOperatorSet.GenCircle(out ho_Circley1, DRowy1, DColumny1, DPhiy1);
                ho_Circley3.Dispose();
                HOperatorSet.GenCircle(out ho_Circley3, DRowz, DColumnz, DPhiz);
                ho_Rectanglet.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectanglet, DRows, DColumns, DPhis, DLength1s, DLength2s);

                ho_RegionDifference.Dispose();
                HOperatorSet.Difference(ho_Circley3, ho_Circley1, out ho_RegionDifference);
                ho_RegionDifference1.Dispose();
                HOperatorSet.Difference(ho_RegionDifference, ho_Rectanglet, out ho_RegionDifference1
                    );

                HOperatorSet.ReadShapeModel(PathHelper.currentProductPath + @"\quekou.shm", out hv_ModelID);
                HOperatorSet.FindShapeModel(Image, hv_ModelID, 0, 6.29, 0.3, 1, 0.5, "least_squares",
        0, 1, out hv_Row, out hv_Column, out hv_Angle, out hv_Score);
                if ((int)(new HTuple((new HTuple(hv_Score.TupleLength())).TupleEqual(1))) != 0)
                {
                    HOperatorSet.VectorAngleToRigid(hv_Row, hv_Column, hv_Angle, DRow1s, DColumn1s,
                        0, out hv_HomMat2D);
                    ho_ImageAffinTrans.Dispose();
                    HOperatorSet.AffineTransImage(Image, out ho_ImageAffinTrans, hv_HomMat2D,
                        "constant", "false");
                    ho_ImageReduced.Dispose();
                    HOperatorSet.ReduceDomain(ho_ImageAffinTrans, ho_RegionDifference1, out ho_ImageReduced
                        );
                    ho_ImageMean.Dispose();
                    HOperatorSet.MeanImage(ho_ImageReduced, out ho_ImageMean, this.yd, this.yd);
                    ho_RegionDynThresh.Dispose();
                    HOperatorSet.DynThreshold(ho_ImageReduced, ho_ImageMean, out ho_RegionDynThresh,
                        this.bjz, "light");
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.Union2(ho_Region1, ho_RegionDynThresh, out ExpTmpOutVar_0);
                        ho_Region1.Dispose();
                        ho_Region1 = ExpTmpOutVar_0;
                    }
                    ho_RegionDynThresh1.Dispose();
                    HOperatorSet.DynThreshold(ho_ImageReduced, ho_ImageMean, out ho_RegionDynThresh1,
                        this.bjz, "dark");
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.Union2(ho_Region1, ho_RegionDynThresh1, out ExpTmpOutVar_0);
                        ho_Region1.Dispose();
                        ho_Region1 = ExpTmpOutVar_0;
                    }
                    ho_ConnectedRegions.Dispose();
                    HOperatorSet.Connection(ho_Region1, out ho_ConnectedRegions);
                    ho_SelectedRegions.Dispose();
                    HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                        "and", this.mj, 9999999999);
                    HOperatorSet.AreaCenter(ho_SelectedRegions, out hv_Area, out hv_Row1, out hv_Column1);
                    if ((int)(new HTuple((new HTuple(hv_Area.TupleLength())).TupleGreater(0))) != 0)
                    {
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.Union2(ho_Region2, ho_SelectedRegions, out ExpTmpOutVar_0);
                            ho_Region2.Dispose();
                            ho_Region2 = ExpTmpOutVar_0;
                        }
                        hv_m = hv_Area.TupleSum();
                        hv_n = hv_Area.TupleMax();
                    }
                    else
                    {
                        hv_m = 0;
                        hv_n = 0;
                    }
                    HOperatorSet.VectorAngleToRigid(DRow1s, DColumn1s, 0, hv_Row, hv_Column,
                        hv_Angle, out hv_HomMat2D1);
                    ho_RegionAffineTrans.Dispose();
                    HOperatorSet.AffineTransRegion(ho_Region2, out ho_RegionAffineTrans, hv_HomMat2D1,
                        "nearest_neighbor");
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.Union2(ho_Region, ho_RegionAffineTrans, out ExpTmpOutVar_0);
                        ho_Region.Dispose();
                        ho_Region = ExpTmpOutVar_0;
                    }
                }
                else
                {
                    hv_m = 9999999;
                    hv_n = 9999999;
                }


                HOperatorSet.ClearShapeModel(hv_ModelID);
                HOperatorSet.Union1(ho_Region, out RegionToDisp);

                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("缺陷总面积");
                hv_result = hv_result.TupleConcat(hv_m.D);
                hv_result = hv_result.TupleConcat("最大面积");
                hv_result = hv_result.TupleConcat(hv_n.D);
                result = hv_result.Clone();
                ho_Circley1.Dispose();
                ho_Circley3.Dispose();
                ho_Rectanglet.Dispose();
                ho_Region.Dispose();
                ho_Region1.Dispose();
                ho_Region2.Dispose();
                ho_RegionDifference.Dispose();
                ho_RegionDifference1.Dispose();
                ho_ImageAffinTrans.Dispose();
                ho_ImageReduced.Dispose();
                ho_ImageMean.Dispose();
                ho_RegionDynThresh.Dispose();
                ho_RegionDynThresh1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionAffineTrans.Dispose();
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

                ho_Circley1.Dispose();
                ho_Circley3.Dispose();
                ho_Rectanglet.Dispose();
                ho_Region.Dispose();
                ho_Region1.Dispose();
                ho_Region2.Dispose();
                ho_RegionDifference.Dispose();
                ho_RegionDifference1.Dispose();
                ho_ImageAffinTrans.Dispose();
                ho_ImageReduced.Dispose();
                ho_ImageMean.Dispose();
                ho_RegionDynThresh.Dispose();
                ho_RegionDynThresh1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionAffineTrans.Dispose();
                algorithm.Region.Dispose();

            }
            finally
            {
                ho_Circley1.Dispose();
                ho_Circley3.Dispose();
                ho_Rectanglet.Dispose();
                ho_Region.Dispose();
                ho_Region1.Dispose();
                ho_Region2.Dispose();
                ho_RegionDifference.Dispose();
                ho_RegionDifference1.Dispose();
                ho_ImageAffinTrans.Dispose();
                ho_ImageReduced.Dispose();
                ho_ImageMean.Dispose();
                ho_RegionDynThresh.Dispose();
                ho_RegionDynThresh1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
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




