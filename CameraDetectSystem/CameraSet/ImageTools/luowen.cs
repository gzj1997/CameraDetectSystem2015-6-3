using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
    [Serializable]
    class luowen : ImageTools
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
        private HTuple thresholdValue = new HTuple();

        public double Dthv { set; get; }
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
        public luowen()
        {
            //RegionToDisp = Image;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public luowen(HObject Image, Algorithm al)
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

            HTuple Row1m = null, Col1m = null, Row2m = null, Col2m = null, thresholdValue = null;
            HObject ho_Rectangle;
            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.DrawRectangle1(this.LWindowHandle, out Row1m, out Col1m, out Row2m, out Col2m);
            this.DRow1m = Row1m.D;
            this.DCol1m = Col1m.D;
            this.DRow2m = Row2m.D;
            this.DCol2m = Col2m.D;
            HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            Dthv = thresholdValue.D;
            HOperatorSet.GenRectangle1(out ho_Rectangle, DRow1m, DCol1m, DRow2m, DCol2m);
            ho_Rectangle.Dispose();
        }

        private void action()
        {
            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_Rectangle, ho_ImageReduced;
            HObject ho_Region1, ho_Region, ho_Contours, ho_SelectedContours;
            HObject ho_SmoothedContours1, ho_ClippedContoursu, ho_ClippedContoursd;
            HObject ho_RegionLinesa, ho_RegionLinesb, ho_RegionLinesc;
            HObject ho_RegionLinesd, ho_RegionUniona, ho_RegionUnionb;
            HObject ho_RegionUnionc, ho_RegionLines1 = null;

            // Local control variables 

            HTuple hv_Width = null, hv_Height = null, hv_Row1m = null;
            HTuple hv_Col1m = null, hv_Row2m = null, hv_Col2m = null;
            HTuple hv_Row = null, hv_Column = null, hv_Phi = null;
            HTuple hv_Length1 = null, hv_Length2 = null, hv_Rown = null;
            HTuple hv_Rowu = null, hv_Columnu = null, hv_Phiu = null;
            HTuple hv_Length1u = null, hv_Length2u = null, hv_MeasureHandleu = null;
            HTuple hv_GrayValuesu = null, hv_Functionu = null, hv_SmoothedFunctionu = null;
            HTuple hv_FirstDerivativeu = null, hv_ZeroCrossingsu = null;
            HTuple hv_RowStartu = null, hv_ColStartu = null, hv_RowEndu = null;
            HTuple hv_ColEndu = null, hv_iu = null, hv_Rowd = null;
            HTuple hv_Columnd = null, hv_Phid = null, hv_Length1d = null;
            HTuple hv_Length2d = null, hv_MeasureHandled = null, hv_GrayValuesd = null;
            HTuple hv_Functiond = null, hv_SmoothedFunctiond = null;
            HTuple hv_FirstDerivatived = null, hv_ZeroCrossingsd = null;
            HTuple hv_RowStartd = null, hv_ColStartd = null, hv_RowEndd = null;
            HTuple hv_ColEndd = null, hv_id = null, hv_p = null, hv_Length3 = null;
            HTuple hv_Cos = null, hv_Sin = null, hv_RT_X = null, hv_RT_Y = null;
            HTuple hv_AX = null, hv_AY = null, hv_RB_X = null, hv_RB_Y = null;
            HTuple hv_BX = null, hv_BY = null, hv_LB_X = null, hv_LB_Y = null;
            HTuple hv_CX = null, hv_CY = null, hv_LT_X = null, hv_LT_Y = null;
            HTuple hv_DX = null, hv_DY = null, hv_RowIntus = null;
            HTuple hv_ColumnIntus = null, hv_RowIntuAll = null, hv_ColumnIntuAll = null;
            HTuple hv_RowIntud = null, hv_ColumnIntud = null, hv_i = null;
            HTuple hv_RowIntu = new HTuple(), hv_ColumnIntu = new HTuple();
            HTuple hv_IsOverlappingu = new HTuple(), hv_Distance1 = new HTuple();
            HTuple hv_RowIntdAll = null, hv_ColumnIntdAll = null, hv_RowIntds = null;
            HTuple hv_ColumnIntds = null, hv_RowIntdd = null, hv_ColumnIntdd = null;
            HTuple hv_j = null, hv_RowIntd = new HTuple(), hv_ColumnIntd = new HTuple();
            HTuple hv_IsOverlappingd = new HTuple(), hv_Distance2 = new HTuple();
            HTuple hv_i1 = null, hv_j1 = null, hv_Deg1All = null, hv_Distance3All = null;
            HTuple hv_Min2 = null, hv_k = null, hv_Angle = new HTuple();
            HTuple hv_Deg1 = new HTuple(), hv_Distance3 = new HTuple();
            HTuple hv_Sorted1 = null, hv_Mean1 = null, hv_dajingmin = null;
            HTuple hv_dajingmax = null, hv_Distance5 = null, hv_Distance4 = null;
            HTuple hv_Distance6 = null, hv_yaju = null, hv_Sorted2 = null;
            HTuple hv_Mean2 = null, hv_Deg1min = null, hv_Deg1max = null;
            HTuple hv_i2 = null, hv_j2 = null, hv_Distance7All = null;
            HTuple hv_Min3 = null, hv_ki = null, hv_Distance7 = new HTuple();
            HTuple hv_Sorted3 = null, hv_Mean3 = null, hv_xiaojingmin = null;
            HTuple hv_xiaojingmax = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_Contours);
            HOperatorSet.GenEmptyObj(out ho_SelectedContours);
            HOperatorSet.GenEmptyObj(out ho_SmoothedContours1);
            HOperatorSet.GenEmptyObj(out ho_ClippedContoursu);
            HOperatorSet.GenEmptyObj(out ho_ClippedContoursd);
            HOperatorSet.GenEmptyObj(out ho_RegionLinesa);
            HOperatorSet.GenEmptyObj(out ho_RegionLinesb);
            HOperatorSet.GenEmptyObj(out ho_RegionLinesc);
            HOperatorSet.GenEmptyObj(out ho_RegionLinesd);
            HOperatorSet.GenEmptyObj(out ho_RegionUniona);
            HOperatorSet.GenEmptyObj(out ho_RegionUnionb);
            HOperatorSet.GenEmptyObj(out ho_RegionUnionc);
            HOperatorSet.GenEmptyObj(out ho_RegionLines1);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {
                HOperatorSet.GetImageSize(Image, out hv_Width, out hv_Height);
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle, DRow1m, DCol1m, DRow2m, DCol2m);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Rectangle, out ho_ImageReduced);
                ho_Region1.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region1, 0, Dthv);
                HOperatorSet.SmallestRectangle2(ho_Region1, out hv_Row, out hv_Column, out hv_Phi,
                    out hv_Length1, out hv_Length2);
                //HOperatorSet.TupleDeg(hv_Phi, out hv_Deg);
                //*rotate_image (Image, Image1, -Deg, 'constant')
                //**
                ho_Region.Dispose();
                HOperatorSet.Threshold(Image, out ho_Region, 0, Dthv);
                ho_Contours.Dispose();
                HOperatorSet.GenContourRegionXld(ho_Region, out ho_Contours, "border");
                ho_SelectedContours.Dispose();
                HOperatorSet.SelectContoursXld(ho_Contours, out ho_SelectedContours, "contour_length",
                    20, 200000, -1, 1);
                ho_SmoothedContours1.Dispose();
                HOperatorSet.SmoothContoursXld(ho_SelectedContours, out ho_SmoothedContours1,
                    11);


                hv_Rown = (DRow1m + DRow2m) / 2;
                //*gen_rectangle1(Rectangle2, Row1m, Col1m, Rown, Col2m)
                //*gen_rectangle1(Rectangle3, Rown, Col1m, Row2m, Col2m)
                ho_ClippedContoursu.Dispose();
                HOperatorSet.ClipContoursXld(ho_SmoothedContours1, out ho_ClippedContoursu,
                    DRow1m, DCol1m, hv_Rown, DCol2m);
                ho_ClippedContoursd.Dispose();
                HOperatorSet.ClipContoursXld(ho_SmoothedContours1, out ho_ClippedContoursd,
                    hv_Rown, DCol1m, DRow2m, DCol2m);
                hv_Rowu = (DRow1m + hv_Rown) / 2;
                hv_Columnu = (DCol1m + DCol2m) / 2;
                hv_Phiu = 0;
                hv_Length1u = (DCol2m - DCol1m) / 2;
                hv_Length2u = (hv_Rown - DRow1m) / 2;

                HOperatorSet.GenMeasureRectangle2(hv_Rowu, hv_Columnu, hv_Phiu, hv_Length1u,
                    hv_Length2u, hv_Width, hv_Height, "bilinear", out hv_MeasureHandleu);
                HOperatorSet.MeasureProjection(Image, hv_MeasureHandleu, out hv_GrayValuesu);
                //hv_Sigmau = 5;
                HOperatorSet.CreateFunct1dArray(hv_GrayValuesu, out hv_Functionu);
                HOperatorSet.SmoothFunct1dGauss(hv_Functionu, 5, out hv_SmoothedFunctionu);
                HOperatorSet.DerivateFunct1d(hv_SmoothedFunctionu, "first", out hv_FirstDerivativeu);
                HOperatorSet.ZeroCrossingsFunct1d(hv_FirstDerivativeu, out hv_ZeroCrossingsu);
                //*disp_cross(3600, Rowu, Columnu, 6, Phiu)//在一个窗口中显示交叉

                hv_RowStartu = hv_Rowu - (hv_Length2u / 2);
                hv_ColStartu = (hv_Columnu - hv_Length1u) + hv_ZeroCrossingsu;
                hv_RowEndu = hv_Rowu + (hv_Length2u / 2);
                hv_ColEndu = (hv_Columnu - hv_Length1u) + hv_ZeroCrossingsu;
                hv_iu = new HTuple(hv_ZeroCrossingsu.TupleLength());
                hv_Rowd = (hv_Rown + DRow2m) / 2;
                hv_Columnd = (DCol1m + DCol2m) / 2;
                hv_Phid = 0;
                hv_Length1d = (DCol2m - DCol1m) / 2;
                hv_Length2d = (DRow2m - hv_Rown) / 2;

                HOperatorSet.GenMeasureRectangle2(hv_Rowd, hv_Columnd, hv_Phid, hv_Length1d,
                    hv_Length2d, hv_Width, hv_Height, "bilinear", out hv_MeasureHandled);
                HOperatorSet.MeasureProjection(Image, hv_MeasureHandled, out hv_GrayValuesd);
                //hv_Sigmad = 5;
                HOperatorSet.CreateFunct1dArray(hv_GrayValuesd, out hv_Functiond);
                HOperatorSet.SmoothFunct1dGauss(hv_Functiond, 5, out hv_SmoothedFunctiond);
                HOperatorSet.DerivateFunct1d(hv_SmoothedFunctiond, "first", out hv_FirstDerivatived);
                HOperatorSet.ZeroCrossingsFunct1d(hv_FirstDerivatived, out hv_ZeroCrossingsd);
                //*stop()

                //
                //*disp_cross(3600, Rowd, Columnd, 6, Phid)//在一个窗口中显示交叉

                hv_RowStartd = hv_Rowd - (hv_Length2d / 2);
                hv_ColStartd = (hv_Columnd - hv_Length1d) + hv_ZeroCrossingsd;
                hv_RowEndd = hv_Rowd + (hv_Length2d / 2);
                hv_ColEndd = (hv_Columnd - hv_Length1d) + hv_ZeroCrossingsd;

                //*dev_clear_window()
                hv_id = new HTuple(hv_ZeroCrossingsd.TupleLength());
                //**螺距
                hv_p = hv_Length1 / hv_id;
                hv_Length3 = hv_Length2 - (hv_p * 1.5);
                //*gen_rectangle2(Rectanglezj,Row, Column, Phi, Length1, Length3)

                HOperatorSet.TupleCos(hv_Phi, out hv_Cos);
                HOperatorSet.TupleSin(hv_Phi, out hv_Sin);
                //*dev_set_color('green')
                hv_RT_X = ((-hv_Length1) * hv_Cos) - (hv_Length3 * hv_Sin);
                hv_RT_Y = ((-hv_Length1) * hv_Sin) + (hv_Length3 * hv_Cos);
                //*gen_circle (Circle, Row-RT_Y, Column+RT_X, 10)
                //*最小矩形的顶点A
                hv_AX = ((-hv_Length1) * hv_Cos) - (hv_Length2 * hv_Sin);
                hv_AY = ((-hv_Length1) * hv_Sin) + (hv_Length2 * hv_Cos);
                //*dev_set_color('red')
                hv_RB_X = (hv_Length1 * hv_Cos) - (hv_Length3 * hv_Sin);
                hv_RB_Y = (hv_Length1 * hv_Sin) + (hv_Length3 * hv_Cos);
                //*gen_circle (Circle, Row-RB_Y, Column+RB_X, 10)
                //*最小矩形的顶点B
                hv_BX = (hv_Length1 * hv_Cos) - (hv_Length2 * hv_Sin);
                hv_BY = (hv_Length1 * hv_Sin) + (hv_Length2 * hv_Cos);

                //*dev_set_color('yellow')
                hv_LB_X = (hv_Length1 * hv_Cos) + (hv_Length3 * hv_Sin);
                hv_LB_Y = (hv_Length1 * hv_Sin) - (hv_Length3 * hv_Cos);
                //*gen_circle (Circle, Row-LB_Y, Column+LB_X, 10)
                //*最小矩形的顶点C
                hv_CX = (hv_Length1 * hv_Cos) + (hv_Length2 * hv_Sin);
                hv_CY = (hv_Length1 * hv_Sin) - (hv_Length2 * hv_Cos);

                //*dev_set_color('pink')
                hv_LT_X = ((-hv_Length1) * hv_Cos) + (hv_Length3 * hv_Sin);
                hv_LT_Y = ((-hv_Length1) * hv_Sin) - (hv_Length3 * hv_Cos);
                //*gen_circle (Circle, Row-LT_Y, Column+LT_X, 10)
                //*最小矩形的顶点D
                hv_DX = ((-hv_Length1) * hv_Cos) + (hv_Length2 * hv_Sin);
                hv_DY = ((-hv_Length1) * hv_Sin) - (hv_Length2 * hv_Cos);

                //*disp_line(3600, (Row-RT_Y+Row-LT_Y)/2, (Column+RT_X+Column+LT_X)/2, Row, Column)
                ho_RegionLinesa.Dispose();
                HOperatorSet.GenRegionLine(out ho_RegionLinesa, hv_Row - hv_AY, hv_Column + hv_AX,
                    hv_Row - hv_BY, hv_Column + hv_BX);
                ho_RegionLinesb.Dispose();
                HOperatorSet.GenRegionLine(out ho_RegionLinesb, hv_Row - hv_AY, hv_Column + hv_AX,
                    hv_Row - hv_DY, hv_Column + hv_DX);
                ho_RegionLinesc.Dispose();
                HOperatorSet.GenRegionLine(out ho_RegionLinesc, hv_Row - hv_BY, hv_Column + hv_BX,
                    hv_Row - hv_CY, hv_Column + hv_CX);
                ho_RegionLinesd.Dispose();
                HOperatorSet.GenRegionLine(out ho_RegionLinesd, hv_Row - hv_CY, hv_Column + hv_CX,
                    hv_Row - hv_DY, hv_Column + hv_DX);
                ho_RegionUniona.Dispose();
                HOperatorSet.Union2(ho_RegionLinesa, ho_RegionLinesb, out ho_RegionUniona);
                ho_RegionUnionb.Dispose();
                HOperatorSet.Union2(ho_RegionLinesc, ho_RegionUniona, out ho_RegionUnionb);
                ho_RegionUnionc.Dispose();
                HOperatorSet.Union2(ho_RegionLinesd, ho_RegionUnionb, out ho_RegionUnionc);

                //*dev_set_line_width (2)
                //*dev_set_color ('green')
                hv_RowIntus = new HTuple();
                hv_ColumnIntus = new HTuple();
                hv_RowIntuAll = new HTuple();
                hv_ColumnIntuAll = new HTuple();
                hv_RowIntud = new HTuple();
                hv_ColumnIntud = new HTuple();
                //hv_u1 = 0;
                HTuple end_val128 = hv_iu - 1;
                HTuple step_val128 = 1;
                for (hv_i = 0; hv_i.Continue(end_val128, step_val128); hv_i = hv_i.TupleAdd(step_val128))
                {
                    HOperatorSet.IntersectionLineContourXld(ho_ClippedContoursu, hv_RowStartu,
                        hv_ColStartu.TupleSelect(hv_i), hv_RowEndu, hv_ColEndu.TupleSelect(hv_i),
                        out hv_RowIntu, out hv_ColumnIntu, out hv_IsOverlappingu);
                    //ho_Cross1u.Dispose();
                    //HOperatorSet.GenCrossContourXld(out ho_Cross1u, hv_RowIntu, hv_ColumnIntu,
                    //    12, 0);
                    hv_RowIntuAll = hv_RowIntuAll.TupleConcat(hv_RowIntu);
                    hv_ColumnIntuAll = hv_ColumnIntuAll.TupleConcat(hv_ColumnIntu);
                    HOperatorSet.DistancePl(hv_RowIntu, hv_ColumnIntu, (((hv_Row - hv_RT_Y) + hv_Row) - hv_LT_Y) / 2,
                        (((hv_Column + hv_RT_X) + hv_Column) + hv_LT_X) / 2, hv_Row, hv_Column, out hv_Distance1);
                    if ((int)(new HTuple(hv_Distance1.TupleGreater(hv_Length3))) != 0)
                    {
                        hv_RowIntus = hv_RowIntus.TupleConcat(hv_RowIntu);
                        hv_ColumnIntus = hv_ColumnIntus.TupleConcat(hv_ColumnIntu);
                    }
                    if ((int)(new HTuple(hv_Distance1.TupleLessEqual(hv_Length3))) != 0)
                    {
                        hv_RowIntud = hv_RowIntud.TupleConcat(hv_RowIntu);
                        hv_ColumnIntud = hv_ColumnIntud.TupleConcat(hv_ColumnIntu);
                    }
                    //hv_u1 = hv_u1 + 1;
                }

                //*dev_display (Cross1u)
                hv_RowIntdAll = new HTuple();
                hv_ColumnIntdAll = new HTuple();
                hv_RowIntds = new HTuple();
                hv_ColumnIntds = new HTuple();
                hv_RowIntdd = new HTuple();
                hv_ColumnIntdd = new HTuple();
                //hv_d1 = 0;
                HTuple end_val153 = hv_id - 1;
                HTuple step_val153 = 1;
                for (hv_j = 0; hv_j.Continue(end_val153, step_val153); hv_j = hv_j.TupleAdd(step_val153))
                {
                    HOperatorSet.IntersectionLineContourXld(ho_ClippedContoursd, hv_RowStartd,
                        hv_ColStartd.TupleSelect(hv_j), hv_RowEndd, hv_ColEndd.TupleSelect(hv_j),
                        out hv_RowIntd, out hv_ColumnIntd, out hv_IsOverlappingd);
                    //ho_Cross1d.Dispose();
                    //HOperatorSet.GenCrossContourXld(out ho_Cross1d, hv_RowIntd, hv_ColumnIntd,
                    //    12, 0);
                    hv_RowIntdAll = hv_RowIntdAll.TupleConcat(hv_RowIntd);
                    hv_ColumnIntdAll = hv_ColumnIntdAll.TupleConcat(hv_ColumnIntd);
                    HOperatorSet.DistancePl(hv_RowIntd, hv_ColumnIntd, (((hv_Row - hv_RT_Y) + hv_Row) - hv_LT_Y) / 2,
                        (((hv_Column + hv_RT_X) + hv_Column) + hv_LT_X) / 2, hv_Row, hv_Column, out hv_Distance2);
                    if ((int)(new HTuple(hv_Distance2.TupleGreater(hv_Length3))) != 0)
                    {
                        hv_RowIntds = hv_RowIntds.TupleConcat(hv_RowIntd);
                        hv_ColumnIntds = hv_ColumnIntds.TupleConcat(hv_ColumnIntd);
                    }
                    if ((int)(new HTuple(hv_Distance2.TupleLessEqual(hv_Length3))) != 0)
                    {
                        hv_RowIntdd = hv_RowIntdd.TupleConcat(hv_RowIntd);
                        hv_ColumnIntdd = hv_ColumnIntdd.TupleConcat(hv_ColumnIntd);
                    }
                    //hv_d1 = hv_d1 + 1;
                }
                hv_i1 = new HTuple(hv_ColumnIntus.TupleLength());
                hv_j1 = new HTuple(hv_ColumnIntds.TupleLength());
                //hv_k1 = 0;
                hv_Deg1All = new HTuple();
                hv_Distance3All = new HTuple();
                HOperatorSet.TupleMin2(hv_i1, hv_j1, out hv_Min2);
                HTuple end_val175 = hv_Min2 - 1;
                HTuple step_val175 = 1;
                for (hv_k = 0; hv_k.Continue(end_val175, step_val175); hv_k = hv_k.TupleAdd(step_val175))
                {
                    ho_RegionLines1.Dispose();
                    HOperatorSet.GenRegionLine(out ho_RegionLines1, hv_RowIntus.TupleSelect(hv_k),
                        hv_ColumnIntus.TupleSelect(hv_k), hv_RowIntds.TupleSelect(hv_k), hv_ColumnIntds.TupleSelect(
                        hv_k));
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.Union2(ho_RegionUnionc, ho_RegionLines1, out ExpTmpOutVar_0);
                        ho_RegionUnionc.Dispose();
                        ho_RegionUnionc = ExpTmpOutVar_0;
                    }
                    HOperatorSet.AngleLl(hv_RowIntus.TupleSelect(hv_k), hv_ColumnIntus.TupleSelect(
                        hv_k), hv_RowIntds.TupleSelect(hv_k), hv_ColumnIntds.TupleSelect(hv_k),
                        (((hv_Row - hv_RT_Y) + hv_Row) - hv_LT_Y) / 2, (((hv_Column + hv_RT_X) + hv_Column) + hv_LT_X) / 2,
                        hv_Row, hv_Column, out hv_Angle);
                    HOperatorSet.TupleDeg(hv_Angle, out hv_Deg1);
                    hv_Deg1All = hv_Deg1All.TupleConcat(hv_Deg1);
                    if ((int)(new HTuple(hv_k.TupleGreater(0))) != 0)
                    {
                        HOperatorSet.DistancePl(hv_RowIntus.TupleSelect(hv_k), hv_ColumnIntus.TupleSelect(
                            hv_k), hv_RowIntds.TupleSelect(hv_k), hv_ColumnIntds.TupleSelect(hv_k),
                            hv_RowIntds.TupleSelect(hv_k - 1), hv_ColumnIntds.TupleSelect(hv_k - 1),
                            out hv_Distance3);
                        hv_Distance3All = hv_Distance3All.TupleConcat(hv_Distance3);
                    }
                    //hv_k1 = hv_k1 + 1;
                }
                HOperatorSet.TupleSort(hv_Distance3All, out hv_Sorted1);
                HOperatorSet.TupleMean(hv_Distance3All, out hv_Mean1);
                hv_dajingmin = hv_Sorted1[0];
                hv_dajingmax = hv_Sorted1[(new HTuple(hv_Sorted1.TupleLength())) - 1];
                HOperatorSet.DistancePp(hv_RowIntus.TupleSelect(0), hv_ColumnIntus.TupleSelect(
                    0), hv_RowIntus.TupleSelect(hv_Min2 - 1), hv_ColumnIntus.TupleSelect(hv_Min2 - 1),
                    out hv_Distance5);
                HOperatorSet.DistancePp(hv_RowIntds.TupleSelect(0), hv_ColumnIntds.TupleSelect(
                    0), hv_RowIntds.TupleSelect(hv_Min2 - 1), hv_ColumnIntds.TupleSelect(hv_Min2 - 1),
                    out hv_Distance4);
                hv_Distance6 = (hv_Distance5 + hv_Distance4) / 2;
                hv_yaju = hv_Distance6 / hv_Min2;
                HOperatorSet.TupleSort(hv_Deg1All, out hv_Sorted2);
                HOperatorSet.TupleMean(hv_Deg1All, out hv_Mean2);
                hv_Deg1min = hv_Sorted2[0];
                hv_Deg1max = hv_Sorted2[(new HTuple(hv_Sorted2.TupleLength())) - 1];
                //***小径
                hv_i2 = new HTuple(hv_ColumnIntud.TupleLength());
                hv_j2 = new HTuple(hv_ColumnIntdd.TupleLength());
                //hv_k2 = 0;
                hv_Distance7All = new HTuple();
                HOperatorSet.TupleMin2(hv_i2, hv_j2, out hv_Min3);
                HTuple end_val205 = hv_Min3 - 1;
                HTuple step_val205 = 1;
                for (hv_ki = 0; hv_ki.Continue(end_val205, step_val205); hv_ki = hv_ki.TupleAdd(step_val205))
                {
                    if ((int)(new HTuple(hv_ki.TupleGreater(0))) != 0)
                    {
                        HOperatorSet.DistancePl(hv_RowIntud.TupleSelect(hv_ki), hv_ColumnIntud.TupleSelect(
                            hv_ki), hv_RowIntdd.TupleSelect(hv_ki), hv_ColumnIntdd.TupleSelect(
                            hv_ki), hv_RowIntdd.TupleSelect(hv_ki - 1), hv_ColumnIntdd.TupleSelect(
                            hv_ki - 1), out hv_Distance7);
                        hv_Distance7All = hv_Distance7All.TupleConcat(hv_Distance7);
                    }
                    //hv_k2 = hv_k2 + 1;
                }
                HOperatorSet.TupleSort(hv_Distance7All, out hv_Sorted3);
                HOperatorSet.TupleMean(hv_Distance7All, out hv_Mean3);
                hv_xiaojingmin = hv_Sorted3[0];
                hv_xiaojingmax = hv_Sorted3[(new HTuple(hv_Sorted3.TupleLength())) - 1];
                HOperatorSet.Union1(ho_RegionUnionc, out RegionToDisp);

                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("齿数");
                hv_result = hv_result.TupleConcat(hv_Min2.D);
                hv_result = hv_result.TupleConcat("螺纹长度");
                hv_result = hv_result.TupleConcat(hv_Distance6.D * pixeldist);
                hv_result = hv_result.TupleConcat("螺纹大径平均值");
                hv_result = hv_result.TupleConcat(hv_Mean1.D * pixeldist);
                hv_result = hv_result.TupleConcat("螺纹大径最小值");
                hv_result = hv_result.TupleConcat(hv_dajingmin.D * pixeldist);
                hv_result = hv_result.TupleConcat("螺纹大径最大值");
                hv_result = hv_result.TupleConcat(hv_dajingmax.D * pixeldist);
                hv_result = hv_result.TupleConcat("螺纹小径平均值");
                hv_result = hv_result.TupleConcat(hv_Mean3.D * pixeldist);
                hv_result = hv_result.TupleConcat("螺纹小径最小值");
                hv_result = hv_result.TupleConcat(hv_xiaojingmin.D * pixeldist);
                hv_result = hv_result.TupleConcat("螺纹小径最大值");
                hv_result = hv_result.TupleConcat(hv_xiaojingmax.D * pixeldist);
                hv_result = hv_result.TupleConcat("牙距");
                hv_result = hv_result.TupleConcat(hv_yaju.D * pixeldist);
                hv_result = hv_result.TupleConcat("平均牙倾角");
                hv_result = hv_result.TupleConcat(hv_Mean2.D);
                hv_result = hv_result.TupleConcat("最小牙倾角");
                hv_result = hv_result.TupleConcat(hv_Deg1min.D);
                hv_result = hv_result.TupleConcat("最大牙倾角");
                hv_result = hv_result.TupleConcat(hv_Deg1max.D);
                result = hv_result.Clone();
                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region1.Dispose();
                ho_Region.Dispose();
                ho_Contours.Dispose();
                ho_SelectedContours.Dispose();
                ho_SmoothedContours1.Dispose();
                ho_ClippedContoursu.Dispose();
                ho_ClippedContoursd.Dispose();
                ho_RegionLinesa.Dispose();
                ho_RegionLinesb.Dispose();
                ho_RegionLinesc.Dispose();
                ho_RegionLinesd.Dispose();
                ho_RegionUniona.Dispose();
                ho_RegionUnionb.Dispose();
                ho_RegionUnionc.Dispose();
                ho_RegionLines1.Dispose();
                algorithm.Region.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("齿数");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("螺纹长度");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("螺纹大径平均值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("螺纹大径最小值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("螺纹大径最大值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("螺纹小径平均值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("螺纹小径最小值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("螺纹小径最大值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("牙距");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("平均牙倾角");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("最小牙倾角");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("最大牙倾角");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();
                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region1.Dispose();
                ho_Region.Dispose();
                ho_Contours.Dispose();
                ho_SelectedContours.Dispose();
                ho_SmoothedContours1.Dispose();
                ho_ClippedContoursu.Dispose();
                ho_ClippedContoursd.Dispose();
                ho_RegionLinesa.Dispose();
                ho_RegionLinesb.Dispose();
                ho_RegionLinesc.Dispose();
                ho_RegionLinesd.Dispose();
                ho_RegionUniona.Dispose();
                ho_RegionUnionb.Dispose();
                ho_RegionUnionc.Dispose();
                ho_RegionLines1.Dispose();
                algorithm.Region.Dispose();

            }
            finally
            {
                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region1.Dispose();
                ho_Region.Dispose();
                ho_Contours.Dispose();
                ho_SelectedContours.Dispose();
                ho_SmoothedContours1.Dispose();
                ho_ClippedContoursu.Dispose();
                ho_ClippedContoursd.Dispose();
                ho_RegionLinesa.Dispose();
                ho_RegionLinesb.Dispose();
                ho_RegionLinesc.Dispose();
                ho_RegionLinesd.Dispose();
                ho_RegionUniona.Dispose();
                ho_RegionUnionb.Dispose();
                ho_RegionUnionc.Dispose();
                ho_RegionLines1.Dispose();
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


