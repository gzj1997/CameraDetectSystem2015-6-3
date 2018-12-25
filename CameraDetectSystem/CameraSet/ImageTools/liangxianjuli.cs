using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
    [Serializable]
    class liangxianjuli : ImageTools
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
        [NonSerialized]
        private HTuple dcenterRow1 = new HTuple();
        [NonSerialized]
        private HTuple dcenterColumn1 = new HTuple();
        [NonSerialized]
        private HTuple dPhi1 = new HTuple();
        [NonSerialized]
        private HTuple dLength11 = new HTuple();
        [NonSerialized]
        private HTuple dLength21 = new HTuple();
        #endregion
        public double thv { set; get; }
        public double hv_Length1m { set; get; }
        public double hv_Length2m { set; get; }
        public double hv_Phim { set; get; }
        public double hv_centerRowm { set; get; }
        public double hv_centerColumnm { set; get; }
        public double hv_Length1m1 { set; get; }
        public double hv_Length2m1 { set; get; }
        public double hv_Phim1 { set; get; }
        public double hv_centerRowm1 { set; get; }
        public double hv_centerColumnm1 { set; get; }
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public liangxianjuli()
        {
            RegionToDisp = Image;
        }
        public liangxianjuli(HObject Image, Algorithm al)
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
            HOperatorSet.DrawRectangle2(this.LWindowHandle, out dcenterRow1, out dcenterColumn1,
out dPhi1, out dLength11, out dLength21);
            this.hv_Length1m1 = dLength11.D;
            this.hv_Length2m1 = dLength21.D;
            this.hv_Phim1 = dPhi1.D;
            this.hv_centerRowm1 = dcenterRow1.D;
            this.hv_centerColumnm1 = dcenterColumn1.D;
        }
        private void action()
        {

            HObject ho_Rectangle = null;
            HObject ho_Rectangle1 = null, ho_ImageReduced = null, ho_ImageReduced1 = null;
            HObject ho_Edges = null, ho_Edges1 = null, ho_ContoursSplit = null;
            HObject ho_ContoursSplit1 = null, ho_UnionContours = null, ho_UnionContours1 = null;
            HObject ho_ObjectSelected = null, ho_ObjectSelected1 = null;
            HObject ho_RegionLines = null, ho_RegionLines1 = null;

            // Local control variables 

            HTuple hv_Row1 = new HTuple(), hv_Column1 = new HTuple(),M1=new HTuple(),D1=new HTuple(),M2=new HTuple(),D2=new HTuple();
            HTuple hv_Phi1 = new HTuple(), hv_Length11 = new HTuple();
            HTuple hv_Length21 = new HTuple(), hv_Row = new HTuple();
            HTuple hv_Column = new HTuple(), hv_Phi = new HTuple();
            HTuple hv_Length1 = new HTuple(), hv_Length2 = new HTuple();
            HTuple hv_Length = new HTuple(), hv_Indices = new HTuple();
            HTuple hv_Indices1 = new HTuple(), hv_RowBegin = new HTuple();
            HTuple hv_ColBegin = new HTuple(), hv_RowEnd = new HTuple();
            HTuple hv_ColEnd = new HTuple(), hv_Nr = new HTuple();
            HTuple hv_Nc = new HTuple(), hv_Dist = new HTuple(), hv_RowBegin1 = new HTuple();
            HTuple hv_ColBegin1 = new HTuple(), hv_RowEnd1 = new HTuple();
            HTuple hv_ColEnd1 = new HTuple(), hv_Nr1 = new HTuple();
            HTuple hv_Nc1 = new HTuple(), hv_Dist1 = new HTuple();
            HTuple hv_ATan = new HTuple(), hv_a = new HTuple(), hv_b = new HTuple();
            HTuple hv_c = new HTuple(), hv_x = new HTuple(), hv_y = new HTuple();
            HTuple hv_juli = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
            HOperatorSet.GenEmptyObj(out ho_Edges);
            HOperatorSet.GenEmptyObj(out ho_Edges1);
            HOperatorSet.GenEmptyObj(out ho_ContoursSplit);
            HOperatorSet.GenEmptyObj(out ho_ContoursSplit1);
            HOperatorSet.GenEmptyObj(out ho_UnionContours);
            HOperatorSet.GenEmptyObj(out ho_UnionContours1);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected1);
            HOperatorSet.GenEmptyObj(out ho_RegionLines);
            HOperatorSet.GenEmptyObj(out ho_RegionLines1);
            try
            {
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle, hv_centerRowm, hv_centerColumnm, hv_Phim, hv_Length1m,
                    hv_Length2m);
                ho_Rectangle1.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle1, hv_centerRowm1, hv_centerColumnm1, hv_Phim1, hv_Length1m1,
                    hv_Length2m1);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Rectangle1, out ho_ImageReduced);
                ho_ImageReduced1.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Rectangle, out ho_ImageReduced1);
                HOperatorSet.Intensity(ho_Rectangle1,ho_ImageReduced,out M1,out D1);
                HOperatorSet.Intensity(ho_Rectangle, ho_ImageReduced1, out M2, out D2);
                if (D1.D > 5 && D2.D > 5)
                {
                    ho_Edges.Dispose();
                    HOperatorSet.EdgesSubPix(ho_ImageReduced1, out ho_Edges, "canny", 1, 20, 40);
                    ho_Edges1.Dispose();
                    HOperatorSet.EdgesSubPix(ho_ImageReduced, out ho_Edges1, "canny", 1, 20, 40);
                    ho_ContoursSplit.Dispose();
                    HOperatorSet.SegmentContoursXld(ho_Edges1, out ho_ContoursSplit, "lines_circles",
                        5, 4, 2);
                    ho_ContoursSplit1.Dispose();
                    HOperatorSet.SegmentContoursXld(ho_Edges, out ho_ContoursSplit1, "lines_circles",
                        5, 4, 2);
                    ho_UnionContours.Dispose();
                    HOperatorSet.UnionCollinearContoursXld(ho_ContoursSplit1, out ho_UnionContours,
                        1000, 20, 20, 0.1, "attr_keep");
                    ho_UnionContours1.Dispose();
                    HOperatorSet.UnionCollinearContoursXld(ho_ContoursSplit, out ho_UnionContours1,
                        1000, 20, 20, 0.1, "attr_keep");
                    HOperatorSet.LengthXld(ho_UnionContours1, out hv_Length);
                    HOperatorSet.TupleFind(hv_Length, hv_Length.TupleMax(), out hv_Indices);
                    ho_ObjectSelected.Dispose();
                    HOperatorSet.SelectObj(ho_UnionContours1, out ho_ObjectSelected, hv_Indices + 1);
                    HOperatorSet.LengthXld(ho_UnionContours, out hv_Length1);
                    HOperatorSet.TupleFind(hv_Length1, hv_Length1.TupleMax(), out hv_Indices1);
                    ho_ObjectSelected1.Dispose();
                    HOperatorSet.SelectObj(ho_UnionContours, out ho_ObjectSelected1, hv_Indices1 + 1);
                    HOperatorSet.FitLineContourXld(ho_ObjectSelected1, "tukey", -1, 0, 5, 2, out hv_RowBegin,
                        out hv_ColBegin, out hv_RowEnd, out hv_ColEnd, out hv_Nr, out hv_Nc, out hv_Dist);
                    HOperatorSet.FitLineContourXld(ho_ObjectSelected, "tukey", -1, 0, 5, 2, out hv_RowBegin1,
                        out hv_ColBegin1, out hv_RowEnd1, out hv_ColEnd1, out hv_Nr1, out hv_Nc1,
                        out hv_Dist1);

                    HOperatorSet.TupleAtan2(hv_RowBegin - hv_RowEnd, hv_ColEnd - hv_ColBegin, out hv_ATan);
                    HOperatorSet.TupleTan(hv_ATan, out hv_a);
                    hv_b = -1.000;
                    hv_c = ((-hv_a) * hv_ColEnd) - hv_RowEnd;
                    hv_x = (hv_ColBegin1 + hv_ColEnd1) / 2.000;
                    hv_y = (-(hv_RowBegin1 + hv_RowEnd1)) / 2.000;
                    hv_juli = (((((hv_a * hv_x) + (hv_b * hv_y)) + hv_c)).TupleAbs()) / ((((hv_b * hv_b) + (hv_a * hv_a))).TupleSqrt()
                        );
                    ho_RegionLines.Dispose();
                    HOperatorSet.GenRegionLine(out ho_RegionLines, hv_RowBegin1, hv_ColBegin1,
                        hv_RowEnd1, hv_ColEnd1);
                    ho_RegionLines1.Dispose();
                    HOperatorSet.GenRegionLine(out ho_RegionLines1, hv_RowBegin, hv_ColBegin, hv_RowEnd,
                        hv_ColEnd);
                    HOperatorSet.Union2(ho_RegionLines, ho_RegionLines1, out RegionToDisp);
                }

                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("Distance");
                hv_result = hv_result.TupleConcat(hv_juli.D * pixeldist);
                result = hv_result.Clone();

                ho_Rectangle.Dispose();
                ho_Rectangle1.Dispose();
                ho_ImageReduced.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Edges.Dispose();
                ho_Edges1.Dispose();
                ho_ContoursSplit.Dispose();
                ho_ContoursSplit1.Dispose();
                ho_UnionContours.Dispose();
                ho_UnionContours1.Dispose();
                ho_ObjectSelected.Dispose();
                ho_ObjectSelected1.Dispose();
                ho_RegionLines.Dispose();
                ho_RegionLines1.Dispose();
                algorithm.Region.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("Distance");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();
                HOperatorSet.Union1(ho_Rectangle, out RegionToDisp
                      );
                ho_Rectangle.Dispose();
                ho_Rectangle1.Dispose();
                ho_ImageReduced.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Edges.Dispose();
                ho_Edges1.Dispose();
                ho_ContoursSplit.Dispose();
                ho_ContoursSplit1.Dispose();
                ho_UnionContours.Dispose();
                ho_UnionContours1.Dispose();
                ho_ObjectSelected.Dispose();
                ho_ObjectSelected1.Dispose();
                ho_RegionLines.Dispose();
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