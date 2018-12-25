using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
    [Serializable]
    class luowencc : ImageTools
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
        public luowencc()
        {
            //RegionToDisp = Image;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public luowencc(HObject Image, Algorithm al)
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
            HObject ho_Region, ho_ConnectedRegions, ho_SelectedRegions;
            HObject ho_Rectangle1, ho_ImageRotate, ho_Region1, ho_RegionAffineTrans;
            HObject ho_Region2, ho_RegionUnionu, ho_RegionUniond, ho_Contours;
            HObject ho_SelectedContours, ho_SmoothedContours1, ho_ClippedContoursu;
            HObject ho_ClippedContoursd, ho_Rectangle2, ho_ClippedContoursuObjectSelected = null;
            HObject ho_ClippedContoursdObjectSelected = null, ho_Circle = null;
            HObject ho_Circle1 = null, ho_ConnectedRegions1, ho_SelectedRegions1;
            HObject ho_SortedRegionssdj, ho_Region3, ho_ConnectedRegions2;
            HObject ho_SelectedRegions2, ho_SortedRegionsxdj, ho_RegionLines = null;
            HObject ho_RegionAffineTrans1;

            // Local control variables 

            HTuple hv_Width = null, hv_Height = null;
            HTuple hv_jd = null, hv_Row = null, hv_Column = null, hv_Phi = null;
            HTuple hv_Length1 = null, hv_Length2 = null, hv_HomMat2DIdentity = null;
            HTuple hv_HomMat2DRotate = null, hv_Row1 = null, hv_Column1 = null;
            HTuple hv_Row2 = null, hv_Column2 = null, hv_Rown = null;
            HTuple hv_Rowu = null, hv_Columnu = null, hv_Phiu = null;
            HTuple hv_Length1u = null, hv_Length2u = null, hv_MeasureHandleu = null;
            HTuple hv_GrayValuesu = null, hv_Sigmau = null, hv_Functionu = null;
            HTuple hv_SmoothedFunctionu = null, hv_FirstDerivativeu = null;
            HTuple hv_ZeroCrossingsu = null, hv_RowStartu = null, hv_ColStartu = null;
            HTuple hv_RowEndu = null, hv_ColEndu = null, hv_iu = null;
            HTuple hv_Rowd = null, hv_Columnd = null, hv_Phid = null;
            HTuple hv_Length1d = null, hv_Length2d = null, hv_MeasureHandled = null;
            HTuple hv_GrayValuesd = null, hv_Sigmad = null, hv_Functiond = null;
            HTuple hv_SmoothedFunctiond = null, hv_FirstDerivatived = null;
            HTuple hv_ZeroCrossingsd = null, hv_RowStartd = null, hv_ColStartd = null;
            HTuple hv_RowEndd = null, hv_ColEndd = null, hv_id = null;
            HTuple hv_RowIntuAll = null, hv_ColumnIntuAll = null, hv_u1 = null;
            HTuple hv_i = null, hv_Length = new HTuple(), hv_Indices1 = new HTuple();
            HTuple hv_RowIntu = new HTuple(), hv_ColumnIntu = new HTuple();
            HTuple hv_IsOverlappingu = new HTuple(), hv_RowIntdAll = null;
            HTuple hv_ColumnIntdAll = null, hv_j = null, hv_RowIntd = new HTuple();
            HTuple hv_ColumnIntd = new HTuple(), hv_IsOverlappingd = new HTuple();
            HTuple hv_Sortedu = null, hv_Sortedd = null, hv_RowIntuu = null;
            HTuple hv_ColumnIntuu = null, hv_RowIntud = null, hv_ColumnIntud = null;
            HTuple hv_uu = null, hv_ud = null, hv_ub1 = null, hv_RowIntdu = null;
            HTuple hv_ColumnIntdu = null, hv_RowIntdd = null, hv_ColumnIntdd = null;
            HTuple hv_du = null, hv_dd = null, hv_db1 = null, hv_Areasdj = null;
            HTuple hv_Rowsdj = null, hv_Columnsdj = null, hv_sdj = null;
            HTuple hv_Areaxdj = null, hv_Rowxdj = null, hv_Columnxdj = null;
            HTuple hv_xdj = null, hv_djsl1 = null, hv_djsl = null;
            HTuple hv_Mean1 = null, hv_Mean2 = null, hv_Mean3 = null;
            HTuple hv_Mean4 = null, hv_Columnuu = null, hv_Columndd = null;
            HTuple hv_AngleAll = null, hv_l = null, hv_Angle = new HTuple();
            HTuple hv_DegAll = null, hv_Mean5 = null, hv_Mean8 = null;
            HTuple hv_Columnuum = null, hv_uum1 = null, hv_uus = null;
            HTuple hv_Columnddm = null, hv_ddm = null, hv_dds = null;
            HTuple hv_ColumnmArr = null, hv_Mean9 = null, hv_Mean10 = null;
            HTuple hv_Sorteda = null, hv_Ai = null, hv_Ax = null, hv_pixeldist = null;
            HTuple hv_L = null, hv_D = null, hv_Dmax = null, hv_Dmin = null;
            HTuple hv_d = null, hv_dmax = null, hv_dmin = null, hv_LS = null;
            HTuple hv_HomMat2DIdentity1 = null, hv_HomMat2DRotate1 = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_ImageRotate);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_RegionAffineTrans);
            HOperatorSet.GenEmptyObj(out ho_Region2);
            HOperatorSet.GenEmptyObj(out ho_RegionUnionu);
            HOperatorSet.GenEmptyObj(out ho_RegionUniond);
            HOperatorSet.GenEmptyObj(out ho_Contours);
            HOperatorSet.GenEmptyObj(out ho_SelectedContours);
            HOperatorSet.GenEmptyObj(out ho_SmoothedContours1);
            HOperatorSet.GenEmptyObj(out ho_ClippedContoursu);
            HOperatorSet.GenEmptyObj(out ho_ClippedContoursd);
            HOperatorSet.GenEmptyObj(out ho_Rectangle2);
            HOperatorSet.GenEmptyObj(out ho_ClippedContoursuObjectSelected);
            HOperatorSet.GenEmptyObj(out ho_ClippedContoursdObjectSelected);
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_Circle1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SortedRegionssdj);
            HOperatorSet.GenEmptyObj(out ho_Region3);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_SortedRegionsxdj);
            HOperatorSet.GenEmptyObj(out ho_RegionLines);
            HOperatorSet.GenEmptyObj(out ho_RegionAffineTrans1);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {
                HOperatorSet.GetImageSize(Image, out hv_Width, out hv_Height);
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle, DRow1m, DCol1m, DRow2m, DCol2m);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Rectangle, out ho_ImageReduced);

                //*
                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, 0, 128);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShapeStd(ho_ConnectedRegions, out ho_SelectedRegions, "max_area",
                    70);
                //hv_jd = 0;
                HOperatorSet.SmallestRectangle2(ho_SelectedRegions, out hv_Row, out hv_Column,
                    out hv_Phi, out hv_Length1, out hv_Length2);
                ho_Rectangle1.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle1, hv_Row, hv_Column, hv_Phi, hv_Length1,
                    hv_Length2);
                ho_ImageRotate.Dispose();
                HOperatorSet.RotateImage(Image, out ho_ImageRotate, -(hv_Phi.TupleDeg()),
                    "constant");
                //*
                ho_Region1.Dispose();
                HOperatorSet.Threshold(ho_ImageRotate, out ho_Region1, 0, 128);
                //*
                HOperatorSet.HomMat2dIdentity(out hv_HomMat2DIdentity);
                HOperatorSet.HomMat2dRotate(hv_HomMat2DIdentity, -hv_Phi, hv_Height / 2, hv_Width / 2,
                    out hv_HomMat2DRotate);
                ho_RegionAffineTrans.Dispose();
                HOperatorSet.AffineTransRegion(ho_Rectangle1, out ho_RegionAffineTrans, hv_HomMat2DRotate,
                    "nearest_neighbor");



                HOperatorSet.SmallestRectangle1(ho_RegionAffineTrans, out hv_Row1, out hv_Column1,
        out hv_Row2, out hv_Column2);

                //***
                ho_Region2.Dispose();
                HOperatorSet.GenRegionPoints(out ho_Region2, hv_Row1, hv_Column1);
                ho_RegionUnionu.Dispose();
                HOperatorSet.Union1(ho_Region2, out ho_RegionUnionu);
                ho_RegionUniond.Dispose();
                HOperatorSet.Union1(ho_Region2, out ho_RegionUniond);



                ho_Contours.Dispose();
                HOperatorSet.GenContourRegionXld(ho_Region1, out ho_Contours, "border");
                ho_SelectedContours.Dispose();
                HOperatorSet.SelectContoursXld(ho_Contours, out ho_SelectedContours, "contour_length",
                    20, 2000000, -1, 1);
                ho_SmoothedContours1.Dispose();
                HOperatorSet.SmoothContoursXld(ho_SelectedContours, out ho_SmoothedContours1,
                    13);
                //*
                hv_Rown = (hv_Row1 + hv_Row2) / 2;
                ho_ClippedContoursu.Dispose();
                HOperatorSet.ClipContoursXld(ho_SmoothedContours1, out ho_ClippedContoursu, hv_Row1,
                    hv_Column1, hv_Rown, hv_Column2);
                ho_ClippedContoursd.Dispose();
                HOperatorSet.ClipContoursXld(ho_SmoothedContours1, out ho_ClippedContoursd, hv_Rown,
                    hv_Column1, hv_Row2, hv_Column2);
                hv_Rowu = (hv_Row1 + hv_Rown) / 2;
                hv_Columnu = (hv_Column1 + hv_Column2) / 2;
                hv_Phiu = 0;
                hv_Length1u = (hv_Column2 - hv_Column1) / 2;
                hv_Length2u = (hv_Rown - hv_Row1) / 2;
                //dev_set_draw ('margin')
                HOperatorSet.GenMeasureRectangle2(hv_Rowu, hv_Columnu, hv_Phiu, hv_Length1u,
                    hv_Length2u, hv_Width, hv_Height, "bilinear", out hv_MeasureHandleu);
                ho_Rectangle2.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle2, hv_Rowu, hv_Columnu, hv_Phiu, hv_Length1u,
                    hv_Length2u);

                HOperatorSet.MeasureProjection(ho_ImageRotate, hv_MeasureHandleu, out hv_GrayValuesu);
                hv_Sigmau = 5;
                HOperatorSet.CreateFunct1dArray(hv_GrayValuesu, out hv_Functionu);
                HOperatorSet.SmoothFunct1dGauss(hv_Functionu, hv_Sigmau, out hv_SmoothedFunctionu);
                HOperatorSet.DerivateFunct1d(hv_SmoothedFunctionu, "first", out hv_FirstDerivativeu);
                HOperatorSet.ZeroCrossingsFunct1d(hv_FirstDerivativeu, out hv_ZeroCrossingsu);

                hv_RowStartu = hv_Rowu - (hv_Length2u / 2);
                hv_ColStartu = (hv_Columnu - hv_Length1u) + hv_ZeroCrossingsu;
                hv_RowEndu = hv_Rowu + (hv_Length2u / 2);
                hv_ColEndu = (hv_Columnu - hv_Length1u) + hv_ZeroCrossingsu;
                hv_iu = new HTuple(hv_ZeroCrossingsu.TupleLength());


                hv_Rowd = (hv_Rown + hv_Row2) / 2;
                hv_Columnd = (hv_Column1 + hv_Column2) / 2;
                hv_Phid = 0;
                hv_Length1d = (hv_Column2 - hv_Column1) / 2;
                hv_Length2d = (hv_Row2 - hv_Rown) / 2;
                HOperatorSet.GenMeasureRectangle2(hv_Rowd, hv_Columnd, hv_Phid, hv_Length1d,
                    hv_Length2d, hv_Width, hv_Height, "bilinear", out hv_MeasureHandled);
                HOperatorSet.MeasureProjection(ho_ImageRotate, hv_MeasureHandled, out hv_GrayValuesd);
                //
                hv_Sigmad = 5;
                HOperatorSet.CreateFunct1dArray(hv_GrayValuesd, out hv_Functiond);
                HOperatorSet.SmoothFunct1dGauss(hv_Functiond, hv_Sigmad, out hv_SmoothedFunctiond);
                HOperatorSet.DerivateFunct1d(hv_SmoothedFunctiond, "first", out hv_FirstDerivatived);
                HOperatorSet.ZeroCrossingsFunct1d(hv_FirstDerivatived, out hv_ZeroCrossingsd);

                HOperatorSet.CloseMeasure(hv_MeasureHandleu);
                HOperatorSet.CloseMeasure(hv_MeasureHandled);

                hv_RowStartd = hv_Rowd - (hv_Length2d / 2);
                hv_ColStartd = (hv_Columnd - hv_Length1d) + hv_ZeroCrossingsd;
                hv_RowEndd = hv_Rowd + (hv_Length2d / 2);
                hv_ColEndd = (hv_Columnd - hv_Length1d) + hv_ZeroCrossingsd;

                hv_id = new HTuple(hv_ZeroCrossingsd.TupleLength());


                hv_RowIntuAll = new HTuple();
                hv_ColumnIntuAll = new HTuple();
                hv_u1 = 0;
                HTuple end_val96 = hv_iu - 1;
                HTuple step_val96 = 1;
                for (hv_i = 0; hv_i.Continue(end_val96, step_val96); hv_i = hv_i.TupleAdd(step_val96))
                {
                    HOperatorSet.LengthXld(ho_ClippedContoursu, out hv_Length);
                    HOperatorSet.TupleFind(hv_Length, hv_Length.TupleMax(), out hv_Indices1);
                    ho_ClippedContoursuObjectSelected.Dispose();
                    HOperatorSet.SelectObj(ho_ClippedContoursu, out ho_ClippedContoursuObjectSelected,
                        hv_Indices1 + 1);
                    HOperatorSet.IntersectionLineContourXld(ho_ClippedContoursuObjectSelected,
                        hv_RowStartu, hv_ColStartu.TupleSelect(hv_i), hv_RowEndu, hv_ColEndu.TupleSelect(
                        hv_i), out hv_RowIntu, out hv_ColumnIntu, out hv_IsOverlappingu);
                    //*gen_cross_contour_xld (Cross1u, RowIntu, ColumnIntu, 12, 0)//根据每个输入点交叉的形状创键一个XLD轮廓(contour)
                    hv_RowIntuAll = hv_RowIntuAll.TupleConcat(hv_RowIntu);
                    hv_ColumnIntuAll = hv_ColumnIntuAll.TupleConcat(hv_ColumnIntu);
                    hv_u1 = hv_u1 + 1;
                }

                hv_RowIntdAll = new HTuple();
                hv_ColumnIntdAll = new HTuple();
                HTuple end_val109 = hv_id - 1;
                HTuple step_val109 = 1;
                for (hv_j = 0; hv_j.Continue(end_val109, step_val109); hv_j = hv_j.TupleAdd(step_val109))
                {
                    HOperatorSet.LengthXld(ho_ClippedContoursd, out hv_Length);
                    HOperatorSet.TupleFind(hv_Length, hv_Length.TupleMax(), out hv_Indices1);
                    ho_ClippedContoursdObjectSelected.Dispose();
                    HOperatorSet.SelectObj(ho_ClippedContoursd, out ho_ClippedContoursdObjectSelected,
                        hv_Indices1 + 1);
                    HOperatorSet.IntersectionLineContourXld(ho_ClippedContoursdObjectSelected,
                        hv_RowStartd, hv_ColStartd.TupleSelect(hv_j), hv_RowEndd, hv_ColEndd.TupleSelect(
                        hv_j), out hv_RowIntd, out hv_ColumnIntd, out hv_IsOverlappingd);
                    //*gen_cross_contour_xld (Cross1d, RowIntd, ColumnIntd, 12, 0)//根据每个输入点交叉的形状创键一个XLD轮廓(contour)
                    hv_RowIntdAll = hv_RowIntdAll.TupleConcat(hv_RowIntd);
                    hv_ColumnIntdAll = hv_ColumnIntdAll.TupleConcat(hv_ColumnIntd);
                }

                //stop ()

                HOperatorSet.TupleSort(hv_RowIntuAll, out hv_Sortedu);
                HOperatorSet.TupleSort(hv_RowIntdAll, out hv_Sortedd);
                hv_RowIntuu = new HTuple();
                hv_ColumnIntuu = new HTuple();
                hv_RowIntud = new HTuple();
                hv_ColumnIntud = new HTuple();
                hv_uu = 0;
                hv_ud = 0;
                for (hv_ub1 = 0; (int)hv_ub1 <= (int)((new HTuple(hv_Sortedu.TupleLength())) - 1); hv_ub1 = (int)hv_ub1 + 1)
                {
                    if ((int)(new HTuple(((hv_RowIntuAll.TupleSelect(hv_ub1))).TupleLess(((((hv_Sortedu.TupleSelect(
                        0)) * 2) + (hv_Sortedu.TupleSelect((new HTuple(hv_Sortedu.TupleLength())) - 1))) / 3) + 3))) != 0)
                    {
                        hv_RowIntuu = hv_RowIntuu.TupleConcat(hv_RowIntuAll.TupleSelect(hv_ub1));
                        hv_ColumnIntuu = hv_ColumnIntuu.TupleConcat(hv_ColumnIntuAll.TupleSelect(
                            hv_ub1));

                        ho_Circle.Dispose();
                        HOperatorSet.GenCircle(out ho_Circle, hv_RowIntuAll.TupleSelect(hv_ub1),
                            hv_ColumnIntuAll.TupleSelect(hv_ub1), 1);
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.Union2(ho_RegionUnionu, ho_Circle, out ExpTmpOutVar_0);
                            ho_RegionUnionu.Dispose();
                            ho_RegionUnionu = ExpTmpOutVar_0;
                        }
                        hv_uu = hv_uu + 1;
                    }

                    if ((int)(new HTuple(((hv_RowIntuAll.TupleSelect(hv_ub1))).TupleGreater((((hv_Sortedu.TupleSelect(
                        0)) + ((hv_Sortedu.TupleSelect((new HTuple(hv_Sortedu.TupleLength())) - 1)) * 2)) / 3) - 3))) != 0)
                    {
                        hv_RowIntud = hv_RowIntud.TupleConcat(hv_RowIntuAll.TupleSelect(hv_ub1));
                        hv_ColumnIntud = hv_ColumnIntud.TupleConcat(hv_ColumnIntuAll.TupleSelect(
                            hv_ub1));
                        hv_ud = hv_ud + 1;
                    }
                }

                //stop ()

                hv_RowIntdu = new HTuple();
                hv_ColumnIntdu = new HTuple();
                hv_RowIntdd = new HTuple();
                hv_ColumnIntdd = new HTuple();
                hv_du = 0;
                hv_dd = 0;
                for (hv_db1 = 0; (int)hv_db1 <= (int)((new HTuple(hv_Sortedd.TupleLength())) - 1); hv_db1 = (int)hv_db1 + 1)
                {
                    if ((int)(new HTuple(((hv_RowIntdAll.TupleSelect(hv_db1))).TupleLess(((((hv_Sortedd.TupleSelect(
                        0)) * 2) + (hv_Sortedd.TupleSelect((new HTuple(hv_Sortedd.TupleLength())) - 1))) / 3) + 3))) != 0)
                    {
                        hv_RowIntdu = hv_RowIntdu.TupleConcat(hv_RowIntdAll.TupleSelect(hv_db1));
                        hv_ColumnIntdu = hv_ColumnIntdu.TupleConcat(hv_ColumnIntdAll.TupleSelect(
                            hv_db1));
                        hv_du = hv_du + 1;
                    }
                    if ((int)(new HTuple(((hv_RowIntdAll.TupleSelect(hv_db1))).TupleGreater((((hv_Sortedd.TupleSelect(
                        0)) + ((hv_Sortedd.TupleSelect((new HTuple(hv_Sortedd.TupleLength())) - 1)) * 2)) / 3) - 3))) != 0)
                    {
                        hv_RowIntdd = hv_RowIntdd.TupleConcat(hv_RowIntdAll.TupleSelect(hv_db1));
                        hv_ColumnIntdd = hv_ColumnIntdd.TupleConcat(hv_ColumnIntdAll.TupleSelect(
                            hv_db1));
                        ho_Circle1.Dispose();
                        HOperatorSet.GenCircle(out ho_Circle1, hv_RowIntdAll.TupleSelect(hv_db1),
                            hv_ColumnIntdAll.TupleSelect(hv_db1), 1);
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.Union2(ho_RegionUniond, ho_Circle1, out ExpTmpOutVar_0);
                            ho_RegionUniond.Dispose();
                            ho_RegionUniond = ExpTmpOutVar_0;
                        }
                        hv_dd = hv_dd + 1;
                    }
                }

                ho_ConnectedRegions1.Dispose();
                HOperatorSet.Connection(ho_RegionUnionu, out ho_ConnectedRegions1);
                ho_SelectedRegions1.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions1, out ho_SelectedRegions1, "area",
                    "and", 3, 99999);
                ho_SortedRegionssdj.Dispose();
                HOperatorSet.SortRegion(ho_SelectedRegions1, out ho_SortedRegionssdj, "upper_left",
                    "true", "column");
                HOperatorSet.AreaCenter(ho_SortedRegionssdj, out hv_Areasdj, out hv_Rowsdj, out hv_Columnsdj);
                ho_Region3.Dispose();
                HOperatorSet.GenRegionPoints(out ho_Region3, hv_Rowsdj.TupleSelect(0), hv_Columnsdj.TupleSelect(
                    0));
                hv_sdj = new HTuple(hv_Columnsdj.TupleLength());

                ho_ConnectedRegions2.Dispose();
                HOperatorSet.Connection(ho_RegionUniond, out ho_ConnectedRegions2);
                ho_SelectedRegions2.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions2, out ho_SelectedRegions2, "area",
                    "and", 3, 99999);
                ho_SortedRegionsxdj.Dispose();
                HOperatorSet.SortRegion(ho_SelectedRegions2, out ho_SortedRegionsxdj, "upper_left",
                    "true", "column");
                HOperatorSet.AreaCenter(ho_SortedRegionsxdj, out hv_Areaxdj, out hv_Rowxdj, out hv_Columnxdj);
                hv_xdj = new HTuple(hv_Columnxdj.TupleLength());
                hv_djsl1 = new HTuple();
                hv_djsl1 = hv_djsl1.TupleConcat(hv_sdj);
                hv_djsl1 = hv_djsl1.TupleConcat(hv_xdj);
                HOperatorSet.TupleMin(hv_djsl1, out hv_djsl);
                HOperatorSet.TupleMean(hv_RowIntuu, out hv_Mean1);
                HOperatorSet.TupleMean(hv_RowIntud, out hv_Mean2);
                HOperatorSet.TupleMean(hv_RowIntdu, out hv_Mean3);
                HOperatorSet.TupleMean(hv_RowIntdd, out hv_Mean4);

                hv_Columnuu = new HTuple();
                hv_Columndd = new HTuple();
                hv_AngleAll = new HTuple();
                HTuple end_val191 = hv_djsl - 1;
                HTuple step_val191 = 1;
                for (hv_l = 0; hv_l.Continue(end_val191, step_val191); hv_l = hv_l.TupleAdd(step_val191))
                {

                    ho_RegionLines.Dispose();
                    HOperatorSet.GenRegionLine(out ho_RegionLines, hv_Rowsdj.TupleSelect(hv_l),
                        hv_Columnsdj.TupleSelect(hv_l), hv_Rowxdj.TupleSelect(hv_l), hv_Columnxdj.TupleSelect(
                        hv_l));
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.Union2(ho_Region3, ho_RegionLines, out ExpTmpOutVar_0);
                        ho_Region3.Dispose();
                        ho_Region3 = ExpTmpOutVar_0;
                    }
                    HOperatorSet.AngleLx(hv_Rowsdj.TupleSelect(hv_l), hv_Columnsdj.TupleSelect(
                        hv_l), hv_Rowxdj.TupleSelect(hv_l), hv_Columnxdj.TupleSelect(hv_l), out hv_Angle);
                    hv_AngleAll = hv_AngleAll.TupleConcat(0 - hv_Angle);
                    if ((int)(new HTuple(hv_l.TupleGreater(0))) != 0)
                    {
                        hv_Columnuu = hv_Columnuu.TupleConcat((hv_ColumnIntuu.TupleSelect(hv_l)) - (hv_ColumnIntuu.TupleSelect(
                            hv_l - 1)));
                        hv_Columndd = hv_Columndd.TupleConcat((hv_ColumnIntdd.TupleSelect(hv_l)) - (hv_ColumnIntdd.TupleSelect(
                            hv_l - 1)));
                    }

                    //stop ()

                }


                HOperatorSet.TupleDeg(hv_AngleAll, out hv_DegAll);
                HOperatorSet.TupleMean(hv_Columnuu, out hv_Mean5);
                HOperatorSet.TupleMean(hv_Columndd, out hv_Mean8);
                //**过滤
                hv_Columnuum = new HTuple();
                hv_uum1 = 0;
                for (hv_uus = 0; (int)hv_uus <= (int)((new HTuple(hv_Columnuu.TupleLength())) - 1); hv_uus = (int)hv_uus + 1)
                {
                    if ((int)((new HTuple(hv_Columnuu.TupleLessEqual(hv_Mean5 * 1.1))).TupleAnd(new HTuple(hv_Columnuu.TupleGreaterEqual(
                        hv_Mean5 * 0.9)))) != 0)
                    {
                        hv_Columnuum = hv_Columnuum.TupleConcat(hv_Columnuu.TupleSelect(hv_uus));
                        hv_uum1 = hv_uum1 + 1;
                    }
                }
                hv_Columnddm = new HTuple();
                hv_ddm = 0;
                for (hv_dds = 0; (int)hv_dds <= (int)((new HTuple(hv_Columndd.TupleLength())) - 1); hv_dds = (int)hv_dds + 1)
                {
                    if ((int)((new HTuple(hv_Columndd.TupleLessEqual(hv_Mean8 * 1.1))).TupleAnd(new HTuple(hv_Columndd.TupleGreaterEqual(
                        hv_Mean8 * 0.9)))) != 0)
                    {
                        hv_Columnddm = hv_Columnddm.TupleConcat(hv_Columndd.TupleSelect(hv_dds));
                        hv_ddm = hv_ddm + 1;
                    }
                }
                hv_ColumnmArr = new HTuple();
                hv_ColumnmArr = hv_ColumnmArr.TupleConcat(hv_Columnuum);
                hv_ColumnmArr = hv_ColumnmArr.TupleConcat(hv_Columnddm);
                HOperatorSet.TupleMean(hv_ColumnmArr, out hv_Mean9);
                HOperatorSet.TupleMean(hv_DegAll, out hv_Mean10);
                HOperatorSet.TupleSort(hv_DegAll, out hv_Sorteda);
                hv_Ai = hv_Sorteda[0];
                hv_Ax = hv_Sorteda[(new HTuple(hv_Sorteda.TupleLength())) - 1];
                hv_pixeldist = 1;
                hv_L = ((hv_Columnsdj.TupleSelect(hv_djsl - 1)) - (hv_Columnsdj.TupleSelect(0))) * hv_pixeldist;
                hv_D = (hv_Mean4 - hv_Mean1) * hv_pixeldist;
                hv_Dmax = ((hv_RowIntdd.TupleMax()) - (hv_RowIntuu.TupleMin())) * hv_pixeldist;
                hv_Dmin = ((hv_RowIntdd.TupleMin()) - (hv_RowIntuu.TupleMax())) * hv_pixeldist;
                hv_d = (hv_Mean3 - hv_Mean2) * hv_pixeldist;
                hv_dmax = ((hv_RowIntdu.TupleMax()) - (hv_RowIntud.TupleMin())) * hv_pixeldist;
                hv_dmin = ((hv_RowIntdu.TupleMin()) - (hv_RowIntud.TupleMax())) * hv_pixeldist;
                hv_LS = hv_Mean9 * hv_pixeldist;
                //*区域旋转
                HOperatorSet.HomMat2dIdentity(out hv_HomMat2DIdentity1);
                HOperatorSet.HomMat2dRotate(hv_HomMat2DIdentity1, hv_Phi, hv_Height / 2, hv_Width / 2,
                    out hv_HomMat2DRotate1);
                ho_RegionAffineTrans1.Dispose();
                HOperatorSet.AffineTransRegion(ho_Region3, out ho_RegionAffineTrans1, hv_HomMat2DRotate1,
                    "nearest_neighbor");
                HOperatorSet.Union1(ho_RegionAffineTrans1, out RegionToDisp);

                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("齿数");
                hv_result = hv_result.TupleConcat(hv_djsl.D);
                hv_result = hv_result.TupleConcat("螺纹长度");
                hv_result = hv_result.TupleConcat(hv_L.D * pixeldist);
                hv_result = hv_result.TupleConcat("螺纹大径平均值");
                hv_result = hv_result.TupleConcat(hv_D.D * pixeldist);
                hv_result = hv_result.TupleConcat("螺纹大径最小值");
                hv_result = hv_result.TupleConcat(hv_Dmin.D * pixeldist);
                hv_result = hv_result.TupleConcat("螺纹大径最大值");
                hv_result = hv_result.TupleConcat(hv_Dmax.D * pixeldist);
                hv_result = hv_result.TupleConcat("螺纹小径平均值");
                hv_result = hv_result.TupleConcat(hv_d.D * pixeldist);
                hv_result = hv_result.TupleConcat("螺纹小径最小值");
                hv_result = hv_result.TupleConcat(hv_dmin.D * pixeldist);
                hv_result = hv_result.TupleConcat("螺纹小径最大值");
                hv_result = hv_result.TupleConcat(hv_dmax.D * pixeldist);
                hv_result = hv_result.TupleConcat("牙距");
                hv_result = hv_result.TupleConcat(hv_LS.D * pixeldist);
                hv_result = hv_result.TupleConcat("平均牙倾角");
                hv_result = hv_result.TupleConcat(hv_Mean10.D);
                hv_result = hv_result.TupleConcat("最小牙倾角");
                hv_result = hv_result.TupleConcat(hv_Ai.D);
                hv_result = hv_result.TupleConcat("最大牙倾角");
                hv_result = hv_result.TupleConcat(hv_Ax.D);
                result = hv_result.Clone();
                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_Rectangle1.Dispose();
                ho_ImageRotate.Dispose();
                ho_Region1.Dispose();
                ho_RegionAffineTrans.Dispose();
                ho_Region2.Dispose();
                ho_RegionUnionu.Dispose();
                ho_RegionUniond.Dispose();
                ho_Contours.Dispose();
                ho_SelectedContours.Dispose();
                ho_SmoothedContours1.Dispose();
                ho_ClippedContoursu.Dispose();
                ho_ClippedContoursd.Dispose();
                ho_Rectangle2.Dispose();
                ho_ClippedContoursuObjectSelected.Dispose();
                ho_ClippedContoursdObjectSelected.Dispose();
                ho_Circle.Dispose();
                ho_Circle1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_SortedRegionssdj.Dispose();
                ho_Region3.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_SortedRegionsxdj.Dispose();
                ho_RegionLines.Dispose();
                ho_RegionAffineTrans1.Dispose();
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
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_Rectangle1.Dispose();
                ho_ImageRotate.Dispose();
                ho_Region1.Dispose();
                ho_RegionAffineTrans.Dispose();
                ho_Region2.Dispose();
                ho_RegionUnionu.Dispose();
                ho_RegionUniond.Dispose();
                ho_Contours.Dispose();
                ho_SelectedContours.Dispose();
                ho_SmoothedContours1.Dispose();
                ho_ClippedContoursu.Dispose();
                ho_ClippedContoursd.Dispose();
                ho_Rectangle2.Dispose();
                ho_ClippedContoursuObjectSelected.Dispose();
                ho_ClippedContoursdObjectSelected.Dispose();
                ho_Circle.Dispose();
                ho_Circle1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_SortedRegionssdj.Dispose();
                ho_Region3.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_SortedRegionsxdj.Dispose();
                ho_RegionLines.Dispose();
                ho_RegionAffineTrans1.Dispose();
                algorithm.Region.Dispose();

            }
            finally
            {
                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_Rectangle1.Dispose();
                ho_ImageRotate.Dispose();
                ho_Region1.Dispose();
                ho_RegionAffineTrans.Dispose();
                ho_Region2.Dispose();
                ho_RegionUnionu.Dispose();
                ho_RegionUniond.Dispose();
                ho_Contours.Dispose();
                ho_SelectedContours.Dispose();
                ho_SmoothedContours1.Dispose();
                ho_ClippedContoursu.Dispose();
                ho_ClippedContoursd.Dispose();
                ho_Rectangle2.Dispose();
                ho_ClippedContoursuObjectSelected.Dispose();
                ho_ClippedContoursdObjectSelected.Dispose();
                ho_Circle.Dispose();
                ho_Circle1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_SortedRegionssdj.Dispose();
                ho_Region3.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_SortedRegionsxdj.Dispose();
                ho_RegionLines.Dispose();
                ho_RegionAffineTrans1.Dispose();
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




