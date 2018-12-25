using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
    [Serializable]
    class dajingpingjun : ImageTools
    {
        #region ROI
        [NonSerialized]
        private HTuple dPhi = new HTuple();
        [NonSerialized]
        private HTuple dLength1 = new HTuple();
        [NonSerialized]
        private HTuple dLength2 = new HTuple();
        [NonSerialized]
        private HTuple thresholdValue = new HTuple();
        [NonSerialized]

        private HTuple dcenterRow = new HTuple();
        [NonSerialized]
        private HTuple dcenterColumn = new HTuple();
        #endregion
        public double thv { set; get; }
        public double hv_Length1m { set; get; }
        public double hv_Length2m { set; get; }
        public double hv_Phim { set; get; }
        public double hv_centerRowm { set; get; }
        public double hv_centerColumnm { set; get; }
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public dajingpingjun()
        {
            RegionToDisp = Image;
        }
        public dajingpingjun(HObject Image, Algorithm al)
        {
            gexxs = 1;
            gex = 0;
            this.Image = Image;
            this.algorithm.Image = Image;
            this.algorithm = al;
            pixeldist = 1;
        }
        public override void draw()
        {
            HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            thv = thresholdValue.D;
            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            HOperatorSet.DrawRectangle2(this.LWindowHandle, out dcenterRow, out dcenterColumn,
    out dPhi, out dLength1, out dLength2);
            this.hv_Length1m = dLength1.D;
            this.hv_Length2m = dLength2.D;
            this.hv_Phim = dPhi.D;
            this.hv_centerRowm = dcenterRow.D;
            this.hv_centerColumnm = dcenterColumn.D;
        }
        private void action()
        {

            HObject ho_Rectangle, ho_ImageReduced, ho_ConnectedRegions2, ho_ObjectSelected3, ho_cl;
            HObject ho_Regions, ho_ObjectSelected, ho_RegionFillUp;
            HObject ho_Contours, ho_Rectangle1, ho_Rectangle3, ho_RegionDifference;
            HObject ho_ImageReduced1, ho_Border, ho_SelectedContours = null;
            HObject ho_ObjectSelected1 = null, ho_ObjectSelected2 = null;
            HObject ho_ContoursSplit1 = null, ho_RegionLines = null, ho_Region = null;
            HObject ho_RegionIntersection = null, ho_ConnectedRegions = null;
            HObject ho_Circle = null, ho_RegionLines1 = null, ho_Region1 = null;
            HObject ho_RegionIntersection1 = null, ho_ConnectedRegions1 = null;
            HObject ho_Circle1 = null;

            // Local control variables 


            HTuple hv_Row4 = null;
            HTuple hv_Column4 = null, hv_Phi2 = null, hv_Length12 = null;
            HTuple hv_Length22 = null, hv_Area2 = null, hv_Row5 = null;
            HTuple hv_Column5 = null, hv_p = null, hv_fx = new HTuple();
            HTuple hv_cz = new HTuple(), hv_dz = new HTuple(), hv_lll = null;
            HTuple hv_Mean = null, hv_Deviation1 = null, hv_Length8 = null;
            HTuple hv_Number = new HTuple(), hv_Rowo1 = new HTuple();
            HTuple hv_Colo1 = new HTuple(), hv_Rowo2 = new HTuple();
            HTuple hv_Colo2 = new HTuple(), hv_as = new HTuple(), hv_ac = new HTuple();
            HTuple hv_b = new HTuple(), hv_cs = new HTuple(), hv_cc = new HTuple();
            HTuple hv_ds1 = new HTuple(), hv_ds2 = new HTuple(), hv_dc1 = new HTuple();
            HTuple hv_dc2 = new HTuple(), hv_Length3 = new HTuple();
            HTuple hv_dd = new HTuple(), hv_Floor = new HTuple(), hv_Length4 = new HTuple();
            HTuple hv_Sequence1 = new HTuple(), hv_Length5 = new HTuple();
            HTuple hv_Sequence = new HTuple(), hv_Sequence2 = new HTuple();
            HTuple hv_Reduced1 = new HTuple(), hv_Reduced2 = new HTuple();
            HTuple hv_Reduced3 = new HTuple(), hv_Reduced4 = new HTuple();
            HTuple hv_d1 = new HTuple(), hv_Length6 = new HTuple();
            HTuple hv_Sequence3 = new HTuple(), hv_Area = new HTuple();
            HTuple hv_Row2 = new HTuple(), hv_Column2 = new HTuple();
            HTuple hv_Selectedc = new HTuple(), hv_Selectedr = new HTuple();
            HTuple hv_Newtuple = new HTuple(), hv_d2 = new HTuple();
            HTuple hv_Length7 = new HTuple(), hv_Sequence4 = new HTuple();
            HTuple hv_Area1 = new HTuple(), hv_Row3 = new HTuple();
            HTuple hv_Column3 = new HTuple(), hv_Selectedc1 = new HTuple();
            HTuple hv_Selectedr1 = new HTuple(), hv_Newtuple5 = new HTuple();
            HTuple hv_Selectedds1 = new HTuple(), hv_Selecteddc1 = new HTuple();
            HTuple hv_Selectedds2 = new HTuple(), hv_Selecteddc2 = new HTuple();
            HTuple hv_hs1 = new HTuple(), hv_hs1r = new HTuple(), hv_hs1c = new HTuple();
            HTuple hv_hs2 = new HTuple(), hv_hs2r = new HTuple(), hv_hs2c = new HTuple();
            HTuple hv_hc1 = new HTuple(), hv_hc2 = new HTuple(), hv_jdr = new HTuple();
            HTuple hv_jdc = new HTuple(), hv_jds = new HTuple(), hv_jdC = new HTuple();
            HTuple hv_jxr = new HTuple(), hv_jxc = new HTuple(), hv_jxs = new HTuple();
            HTuple hv_jxC = new HTuple(), hv_ydr = new HTuple(), hv_ydc = new HTuple();
            HTuple hv_yds = new HTuple(), hv_ydC = new HTuple(), hv_yxr = new HTuple();
            HTuple hv_yxc = new HTuple(), hv_yxs = new HTuple(), hv_yxC = new HTuple();
            HTuple hv_jin = new HTuple(), hv_yu = new HTuple(), hv_jp = new HTuple(), hv_Indices = new HTuple();
            HTuple hv_yp = new HTuple(), hv_j = new HTuple(), hv_luoju1 = new HTuple(), hv_Area3 = new HTuple(), hv_Row1 = new HTuple(), hv_Column1 = new HTuple();
            HTuple hv_luoju2 = new HTuple(), hv_dajing = new HTuple();
            HTuple hv_xiaojing = new HTuple(), hv_changdu = new HTuple();
            HTuple hv_tiaoshu = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Regions);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_Contours);
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_Rectangle3);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
            HOperatorSet.GenEmptyObj(out ho_Border);
            HOperatorSet.GenEmptyObj(out ho_SelectedContours);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected1);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected2);
            HOperatorSet.GenEmptyObj(out ho_ContoursSplit1);
            HOperatorSet.GenEmptyObj(out ho_RegionLines);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_RegionLines1);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_Circle1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected3);
            HOperatorSet.GenEmptyObj(out ho_cl);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle, hv_centerRowm, hv_centerColumnm, hv_Phim, hv_Length1m,
                    hv_Length2m);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Rectangle, out ho_ImageReduced);
                ho_Regions.Dispose();
                HOperatorSet.AutoThreshold(ho_ImageReduced, out ho_Regions, 3);

                ho_ObjectSelected.Dispose();
                HOperatorSet.SelectObj(ho_Regions, out ho_ObjectSelected, 1);
                ho_ObjectSelected.Dispose();
                HOperatorSet.SelectObj(ho_Regions, out ho_ObjectSelected, 1);
                ho_ConnectedRegions2.Dispose();
                HOperatorSet.Connection(ho_ObjectSelected, out ho_ConnectedRegions2);
                HOperatorSet.AreaCenter(ho_ConnectedRegions2, out hv_Area3, out hv_Row1, out hv_Column1);
                HOperatorSet.TupleFind(hv_Area3, hv_Area3.TupleMax(), out hv_Indices);
                ho_ObjectSelected3.Dispose();
                HOperatorSet.SelectObj(ho_ConnectedRegions2, out ho_ObjectSelected3, hv_Indices + 1);
                ho_cl.Dispose();
                HOperatorSet.ClosingCircle(ho_ObjectSelected3, out ho_cl, 3.5);
                ho_RegionFillUp.Dispose();
                HOperatorSet.FillUp(ho_ObjectSelected3, out ho_RegionFillUp);
                ho_Contours.Dispose();
                HOperatorSet.GenContourRegionXld(ho_RegionFillUp, out ho_Contours, "border");
                HOperatorSet.SmallestRectangle2Xld(ho_Contours, out hv_Row4, out hv_Column4,
                    out hv_Phi2, out hv_Length12, out hv_Length22);
                HOperatorSet.AreaCenter(ho_RegionFillUp, out hv_Area2, out hv_Row5, out hv_Column5);
                hv_p = ((hv_Phi2 - hv_Phim)).TupleAbs();
                while ((int)(new HTuple(hv_p.TupleGreater((new HTuple(90)).TupleRad()))) != 0)
                {
                    hv_p = ((hv_p - ((new HTuple(180)).TupleRad()))).TupleAbs();
                }
                if ((int)(new HTuple(hv_p.TupleGreater((new HTuple(45)).TupleRad()))) != 0)
                {
                    hv_fx = hv_Phi2.Clone();
                    hv_cz = hv_Length12.Clone();
                    hv_dz = hv_Length22.Clone();
                }
                else
                {
                    hv_fx = hv_Phi2 + ((new HTuple(90)).TupleRad());
                    hv_cz = hv_Length22.Clone();
                    hv_dz = hv_Length12.Clone();
                }
                hv_lll = ((((hv_cz * hv_dz) * 4) - hv_Area2) / 2) / hv_cz;
                ho_Rectangle1.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle1, hv_Row4, hv_Column4, hv_fx, hv_cz,
                    hv_dz + 5);
                ho_Rectangle3.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle3, hv_Row4, hv_Column4, hv_Phi2, hv_Length12 + 200,
                    hv_lll);
                ho_RegionDifference.Dispose();
                HOperatorSet.Difference(ho_Rectangle1, ho_Rectangle3, out ho_RegionDifference
                    );
                ho_ImageReduced1.Dispose();
                HOperatorSet.ReduceDomain(ho_ImageReduced, ho_RegionDifference, out ho_ImageReduced1
                    );
                HOperatorSet.Intensity(ho_RegionDifference, ho_ImageReduced1, out hv_Mean, out hv_Deviation1);
                ho_Border.Dispose();
                HOperatorSet.ThresholdSubPix(ho_ImageReduced1, out ho_Border, hv_Mean);
                HOperatorSet.LengthXld(ho_Border, out hv_Length8);
                if ((int)(new HTuple((new HTuple(hv_Length8.TupleLength())).TupleGreater(1))) != 0)
                {
                    ho_SelectedContours.Dispose();
                    HOperatorSet.SelectContoursXld(ho_Border, out ho_SelectedContours, "contour_length",
                        0.5 * (hv_Length8.TupleMax()), 1.5 * (hv_Length8.TupleMax()), -0.5, 0.5);
                    HOperatorSet.CountObj(ho_SelectedContours, out hv_Number);
                    if ((int)(new HTuple(hv_Number.TupleEqual(2))) != 0)
                    {
                        ho_ObjectSelected1.Dispose();
                        HOperatorSet.SelectObj(ho_SelectedContours, out ho_ObjectSelected1, 1);
                        ho_ObjectSelected2.Dispose();
                        HOperatorSet.SelectObj(ho_SelectedContours, out ho_ObjectSelected2, 2);
                        HOperatorSet.GetContourXld(ho_ObjectSelected1, out hv_Rowo1, out hv_Colo1);
                        HOperatorSet.GetContourXld(ho_ObjectSelected2, out hv_Rowo2, out hv_Colo2);
                        HOperatorSet.TupleTan(hv_fx + ((new HTuple(0.000001)).TupleRad()), out hv_as);
                        HOperatorSet.TupleTan(hv_fx + ((new HTuple(90.000001)).TupleRad()), out hv_ac);
                        hv_b = -1;
                        hv_cs = (-(hv_Row4 + (5000 * (((hv_fx + ((new HTuple(90)).TupleRad()))).TupleSin()
                            )))) - (hv_as * (hv_Column4 - (5000 * (((hv_fx + ((new HTuple(90)).TupleRad()))).TupleCos()
                            ))));
                        hv_cc = (-(hv_Row4 + (5000 * (hv_fx.TupleSin())))) - (hv_ac * (hv_Column4 - (5000 * (hv_fx.TupleCos()
                            ))));
                        hv_ds1 = (((((hv_as * hv_Colo1) + (hv_b * (-hv_Rowo1))) + hv_cs)).TupleAbs()) / (((1 + (hv_as * hv_as))).TupleSqrt()
                            );
                        hv_ds2 = (((((hv_as * hv_Colo2) + (hv_b * (-hv_Rowo2))) + hv_cs)).TupleAbs()) / (((1 + (hv_as * hv_as))).TupleSqrt()
                            );
                        hv_dc1 = (((((hv_ac * hv_Colo1) + (hv_b * (-hv_Rowo1))) + hv_cc)).TupleAbs()) / (((1 + (hv_ac * hv_ac))).TupleSqrt()
                            );
                        hv_dc2 = (((((hv_ac * hv_Colo2) + (hv_b * (-hv_Rowo2))) + hv_cc)).TupleAbs()) / (((1 + (hv_ac * hv_ac))).TupleSqrt()
                            );
                        ho_ContoursSplit1.Dispose();
                        HOperatorSet.SegmentContoursXld(ho_ObjectSelected2, out ho_ContoursSplit1,
                            "lines_circles", 5, 4, 2);
                        HOperatorSet.LengthXld(ho_ContoursSplit1, out hv_Length3);
                        hv_dd = hv_Length3.TupleMean();
                        HOperatorSet.TupleFloor(hv_dd, out hv_Floor);
                        HOperatorSet.TupleLength(hv_Colo1, out hv_Length4);
                        HOperatorSet.TupleGenSequence(hv_Length4 - hv_Floor, hv_Length4 - 1, 1, out hv_Sequence1);
                        HOperatorSet.TupleLength(hv_Colo2, out hv_Length5);
                        HOperatorSet.TupleGenSequence(0, hv_Floor - 1, 1, out hv_Sequence);
                        HOperatorSet.TupleGenSequence(hv_Length5 - hv_Floor, hv_Length5 - 1, 1, out hv_Sequence2);
                        HOperatorSet.TupleRemove(hv_ds1, hv_Sequence, out hv_Reduced1);
                        HOperatorSet.TupleRemove(hv_ds1, hv_Sequence1, out hv_Reduced2);
                        HOperatorSet.TupleRemove(hv_ds2, hv_Sequence, out hv_Reduced3);
                        HOperatorSet.TupleRemove(hv_ds2, hv_Sequence2, out hv_Reduced4);
                        hv_d1 = hv_Reduced1 - hv_Reduced2;
                        HOperatorSet.TupleLength(hv_d1, out hv_Length6);
                        HOperatorSet.TupleGenSequence(1, hv_Length6, 1, out hv_Sequence3);
                        ho_RegionLines.Dispose();
                        HOperatorSet.GenRegionLine(out ho_RegionLines, 0, 0, 0, hv_Length6);
                        ho_Region.Dispose();
                        HOperatorSet.GenRegionPoints(out ho_Region, hv_d1, hv_Sequence3);
                        ho_RegionIntersection.Dispose();
                        HOperatorSet.Intersection(ho_RegionLines, ho_Region, out ho_RegionIntersection
                            );
                        ho_ConnectedRegions.Dispose();
                        HOperatorSet.Connection(ho_RegionIntersection, out ho_ConnectedRegions);
                        HOperatorSet.AreaCenter(ho_ConnectedRegions, out hv_Area, out hv_Row2, out hv_Column2);
                        HOperatorSet.TupleSelect(hv_Colo1, ((hv_Column2 + (hv_dd / 2))).TupleFloor(),
                            out hv_Selectedc);
                        HOperatorSet.TupleSelect(hv_Rowo1, ((hv_Column2 + (hv_dd / 2))).TupleFloor(),
                            out hv_Selectedr);
                        HOperatorSet.TupleGenConst(new HTuple(hv_Selectedr.TupleLength()), 1, out hv_Newtuple);
                        ho_Circle.Dispose();
                        HOperatorSet.GenCircle(out ho_Circle, hv_Selectedr, hv_Selectedc, hv_Newtuple);
                        hv_d2 = hv_Reduced3 - hv_Reduced4;
                        HOperatorSet.TupleLength(hv_d2, out hv_Length7);
                        HOperatorSet.TupleGenSequence(1, hv_Length7, 1, out hv_Sequence4);
                        ho_RegionLines1.Dispose();
                        HOperatorSet.GenRegionLine(out ho_RegionLines1, 0, 0, 0, hv_Length7);
                        ho_Region1.Dispose();
                        HOperatorSet.GenRegionPoints(out ho_Region1, hv_d2, hv_Sequence4);
                        ho_RegionIntersection1.Dispose();
                        HOperatorSet.Intersection(ho_Region1, ho_RegionLines1, out ho_RegionIntersection1
                            );
                        ho_ConnectedRegions1.Dispose();
                        HOperatorSet.Connection(ho_RegionIntersection1, out ho_ConnectedRegions1);
                        HOperatorSet.AreaCenter(ho_ConnectedRegions1, out hv_Area1, out hv_Row3,
                            out hv_Column3);
                        HOperatorSet.TupleSelect(hv_Colo2, ((hv_Column3 + (hv_dd / 2))).TupleFloor(),
                            out hv_Selectedc1);
                        HOperatorSet.TupleSelect(hv_Rowo2, ((hv_Column3 + (hv_dd / 2))).TupleFloor(),
                            out hv_Selectedr1);
                        HOperatorSet.TupleGenConst(new HTuple(hv_Selectedr1.TupleLength()), 1, out hv_Newtuple5);
                        ho_Circle1.Dispose();
                        HOperatorSet.GenCircle(out ho_Circle1, hv_Selectedr1, hv_Selectedc1, hv_Newtuple5);
                        HOperatorSet.TupleSelect(hv_ds1, ((hv_Column2 + (hv_dd / 2))).TupleFloor(), out hv_Selectedds1);
                        HOperatorSet.TupleSelect(hv_dc1, ((hv_Column2 + (hv_dd / 2))).TupleFloor(), out hv_Selecteddc1);
                        HOperatorSet.TupleSelect(hv_ds2, ((hv_Column3 + (hv_dd / 2))).TupleFloor(), out hv_Selectedds2);
                        HOperatorSet.TupleSelect(hv_dc2, ((hv_Column3 + (hv_dd / 2))).TupleFloor(), out hv_Selecteddc2);
                        if ((int)(new HTuple((new HTuple(hv_Selectedds1.TupleMean())).TupleLess(hv_Selectedds2.TupleMean()
                            ))) != 0)
                        {
                            hv_hs1 = hv_Selectedds1.Clone();
                            hv_hs1r = hv_Selectedr.Clone();
                            hv_hs1c = hv_Selectedc.Clone();
                            hv_hs2 = hv_Selectedds2.Clone();
                            hv_hs2r = hv_Selectedr1.Clone();
                            hv_hs2c = hv_Selectedc1.Clone();
                            hv_hc1 = hv_Selecteddc1.Clone();
                            hv_hc2 = hv_Selecteddc2.Clone();
                        }
                        else
                        {
                            hv_hs1 = hv_Selectedds2.Clone();
                            hv_hs2r = hv_Selectedr.Clone();
                            hv_hs2c = hv_Selectedc.Clone();
                            hv_hs2 = hv_Selectedds1.Clone();
                            hv_hs1r = hv_Selectedr1.Clone();
                            hv_hs1c = hv_Selectedc1.Clone();
                            hv_hc2 = hv_Selecteddc1.Clone();
                            hv_hc1 = hv_Selecteddc2.Clone();
                        }
                        hv_jdr = new HTuple();
                        hv_jdc = new HTuple();
                        hv_jds = new HTuple();
                        hv_jdC = new HTuple();
                        hv_jxr = new HTuple();
                        hv_jxc = new HTuple();
                        hv_jxs = new HTuple();
                        hv_jxC = new HTuple();
                        hv_ydr = new HTuple();
                        hv_ydc = new HTuple();
                        hv_yds = new HTuple();
                        hv_ydC = new HTuple();
                        hv_yxr = new HTuple();
                        hv_yxc = new HTuple();
                        hv_yxs = new HTuple();
                        hv_yxC = new HTuple();
                        hv_jin = new HTuple(hv_hs1.TupleLength());
                        hv_yu = new HTuple(hv_hs2.TupleLength());
                        hv_jp = hv_hs1.TupleMean();
                        hv_yp = hv_hs2.TupleMean();
                        HTuple end_val128 = hv_jin - 1;
                        HTuple step_val128 = 1;
                        for (hv_j = 0; hv_j.Continue(end_val128, step_val128); hv_j = hv_j.TupleAdd(step_val128))
                        {
                            if ((int)(new HTuple(((hv_hs1.TupleSelect(hv_j))).TupleLess(hv_jp))) != 0)
                            {
                                hv_jdr = hv_jdr.TupleConcat(hv_hs1r.TupleSelect(hv_j));
                                hv_jdc = hv_jdc.TupleConcat(hv_hs1c.TupleSelect(hv_j));
                                hv_jds = hv_jds.TupleConcat(hv_hs1.TupleSelect(hv_j));
                                hv_jdC = hv_jdC.TupleConcat(hv_hc1.TupleSelect(hv_j));
                            }
                            else
                            {
                                hv_jxr = hv_jxr.TupleConcat(hv_hs1r.TupleSelect(hv_j));
                                hv_jxc = hv_jxc.TupleConcat(hv_hs1c.TupleSelect(hv_j));
                                hv_jxs = hv_jxs.TupleConcat(hv_hs1.TupleSelect(hv_j));
                                hv_jxC = hv_jxC.TupleConcat(hv_hc1.TupleSelect(hv_j));
                            }
                        }
                        HTuple end_val141 = hv_yu - 1;
                        HTuple step_val141 = 1;
                        for (hv_j = 0; hv_j.Continue(end_val141, step_val141); hv_j = hv_j.TupleAdd(step_val141))
                        {
                            if ((int)(new HTuple(((hv_hs2.TupleSelect(hv_j))).TupleGreater(hv_yp))) != 0)
                            {
                                hv_ydr = hv_ydr.TupleConcat(hv_hs2r.TupleSelect(hv_j));
                                hv_ydc = hv_ydc.TupleConcat(hv_hs2c.TupleSelect(hv_j));
                                hv_yds = hv_yds.TupleConcat(hv_hs2.TupleSelect(hv_j));
                                hv_ydC = hv_ydC.TupleConcat(hv_hc2.TupleSelect(hv_j));
                            }
                            else
                            {
                                hv_yxr = hv_yxr.TupleConcat(hv_hs2r.TupleSelect(hv_j));
                                hv_yxc = hv_yxc.TupleConcat(hv_hs2c.TupleSelect(hv_j));
                                hv_yxs = hv_yxs.TupleConcat(hv_hs2.TupleSelect(hv_j));
                                hv_yxC = hv_yxC.TupleConcat(hv_hc2.TupleSelect(hv_j));
                            }
                        }
                        hv_luoju1 = (((hv_hc1.TupleMax()) - (hv_hc1.TupleMin())) / ((new HTuple(hv_hc1.TupleLength()
                            )) - 1)) * 2;
                        hv_luoju2 = (((hv_hc2.TupleMax()) - (hv_hc2.TupleMin())) / ((new HTuple(hv_hc2.TupleLength()
                            )) - 1)) * 2;
                        hv_dajing = (hv_yds.TupleMean()) - (hv_jds.TupleMean());
                        hv_xiaojing = (hv_yxs.TupleMean()) - (hv_jxs.TupleMean());
                        hv_changdu = (((hv_hc1.TupleConcat(hv_hc2))).TupleMax()) - (((hv_hc1.TupleConcat(
                            hv_hc2))).TupleMin());
                        hv_tiaoshu = (((((((new HTuple(hv_jds.TupleLength())).TupleConcat(new HTuple(hv_jxs.TupleLength()
                            )))).TupleConcat(new HTuple(hv_yds.TupleLength())))).TupleConcat(new HTuple(hv_yxs.TupleLength()
                            )))).TupleMax();
                    }
                }
                HOperatorSet.Union2(ho_Circle, ho_Circle1, out RegionToDisp);



                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("大径平均值");
                hv_result = hv_result.TupleConcat(hv_dajing.D * pixeldist);
                hv_result = hv_result.TupleConcat("螺纹长度");
                hv_result = hv_result.TupleConcat(hv_changdu.D * pixeldist);
                hv_result = hv_result.TupleConcat("螺纹条数");
                hv_result = hv_result.TupleConcat(hv_tiaoshu.D);
                hv_result = hv_result.TupleConcat("螺距1");
                hv_result = hv_result.TupleConcat(hv_luoju1.D * pixeldist);
                hv_result = hv_result.TupleConcat("螺距2");
                hv_result = hv_result.TupleConcat(hv_luoju2.D * pixeldist);
                result = hv_result.Clone();

                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Regions.Dispose();
                ho_ObjectSelected.Dispose();
                ho_RegionFillUp.Dispose();
                ho_Contours.Dispose();
                ho_Rectangle1.Dispose();
                ho_Rectangle3.Dispose();
                ho_RegionDifference.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Border.Dispose();
                ho_SelectedContours.Dispose();
                ho_ObjectSelected1.Dispose();
                ho_ObjectSelected2.Dispose();
                ho_ContoursSplit1.Dispose();
                ho_RegionLines.Dispose();
                ho_Region.Dispose();
                ho_RegionIntersection.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_Circle.Dispose();
                ho_RegionLines1.Dispose();
                ho_Region1.Dispose();
                ho_RegionIntersection1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_Circle1.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_ObjectSelected3.Dispose();
                ho_cl.Dispose();
                algorithm.Region.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("大径平均值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("螺纹长度");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("螺纹条数");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("螺距1");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("螺距2");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();

                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Regions.Dispose();
                ho_ObjectSelected.Dispose();
                ho_RegionFillUp.Dispose();
                ho_Contours.Dispose();
                ho_Rectangle1.Dispose();
                ho_Rectangle3.Dispose();
                ho_RegionDifference.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Border.Dispose();
                ho_SelectedContours.Dispose();
                ho_ObjectSelected1.Dispose(); ho_cl.Dispose();
                ho_ObjectSelected2.Dispose();
                ho_ContoursSplit1.Dispose();
                ho_RegionLines.Dispose();
                ho_Region.Dispose();
                ho_RegionIntersection.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_Circle.Dispose();
                ho_RegionLines1.Dispose();
                ho_Region1.Dispose();
                ho_RegionIntersection1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_Circle1.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_ObjectSelected3.Dispose();
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