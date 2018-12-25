using System.Text;
using HalconDotNet;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System;
namespace CameraDetectSystem
{
    [Serializable]
    class gunhuaweizhi : ImageTools
    {
        #region ROI
        [NonSerialized]
        private HTuple dPhi = new HTuple();
        [NonSerialized]
        private HTuple dLength1 = new HTuple();
        [NonSerialized]
        private HTuple dLength2 = new HTuple();
        [NonSerialized]
        private HTuple dRow = new HTuple();
        [NonSerialized]
        private HTuple dColumn = new HTuple();
        [NonSerialized]
        private HTuple ddPhi = new HTuple();
        [NonSerialized]
        private HTuple ddLength1 = new HTuple();
        [NonSerialized]
        private HTuple ddLength2 = new HTuple();
        [NonSerialized]
        private HTuple ddRow = new HTuple();
        [NonSerialized]
        private HTuple ddColumn = new HTuple();
        [NonSerialized]
        private HTuple hv_Radiusd = new HTuple();

        [NonSerialized]

        HTuple hv_Col = new HTuple(), hv_dis = new HTuple(), hv_dxi = new HTuple(), hv_Exception = new HTuple(), hv_dn = new HTuple(),
        hv_dxr = new HTuple(), hv_dxc = new HTuple(), hv_Row2 = new HTuple(), hv_dnr = new HTuple(),
        hv_dnc = new HTuple(), hv_dni = new HTuple(), hv_Ix = new HTuple(), hv_In, hv_dx = new HTuple();
        [NonSerialized]
        HTuple hv_djd = new HTuple(), hv_djdr = new HTuple(), hv_djdc = new HTuple(), hv_djx = new HTuple(),
        hv_djxr = new HTuple(), hv_jh = new HTuple(), hv_j = new HTuple(), hv_djxc = new HTuple(), hv_xjd = new HTuple(),
        hv_xjdr = new HTuple(), hv_xjdc = new HTuple(), hv_xjx = new HTuple(), hv_xjxr = new HTuple(),
        hv_xjxc = new HTuple(), hv_ljd = new HTuple(), hv_ljx = new HTuple();

        [NonSerialized]
        private HTuple thresholdValue = new HTuple();
        [field: NonSerializedAttribute()]
        HTuple hv_ModelID = null;
        #endregion
        public double hv_Row1 { set; get; }
        public double hv_Column1 { set; get; }
        public double hv_Phi1 { set; get; }
        public double thv { set; get; }
        public double hv_Length11 { set; get; }
        public double hv_Length21 { set; get; }
        public double hv1_Row { set; get; }
        public double hv1_Column { set; get; }
        public double hv1_Phi { set; get; }
        public double hv1_Length1 { set; get; }
        public double hv1_Length2 { set; get; }
        public double hv_Radius { set; get; }
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public gunhuaweizhi()
        {
            RegionToDisp = Image;
        }
        public gunhuaweizhi(HObject Image, Algorithm al)
        {
            gex = 0;
            this.algorithm = al;

            this.Image = Image;
            RegionToDisp = Image;
            pixeldist = 1;
        }
        public override void draw()
        {
            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            HOperatorSet.DrawRectangle2(this.LWindowHandle, out dRow, out dColumn, out dPhi, out dLength1, out dLength2);
            this.hv_Row1 = dRow;
            this.hv_Column1 = dColumn;
            this.hv_Phi1 = dPhi;
            this.hv_Length11 = dLength1;
            this.hv_Length21 = dLength2;
        }
        //DateTime t1, t2, t3, t4,t5,t6,t7;
        private void action()
        {
            HObject ho_Region = null, ho_RegionClosing = null;
            HObject ho_RegionErosion = null, ho_ImageReduced = null, ho_Edges = null;
            HObject ho_SelectedContours = null, ho_SelectedContours1 = null;
            HObject ho_SelectedContours2 = null, ho_ObjectsConcat = null;
            HObject ho_Region1 = null, ho_RegionUnion = null, ho_RegionClosing1 = null;
            HObject ho_ConnectedRegions = null, ho_SelectedRegions = null;
            HObject ho_Rectangle = null, ho_Rectangle1 = null, ho_ImageReduced1 = null;
            HObject ho_Edges2 = null, ho_ContoursSplit = null, ho_UnionContours = null;
            HObject ho_RegionLines = null;

            // Local control variables 


            HTuple hv_Row = new HTuple(), hv_i = new HTuple(), hv_Sorted=new HTuple();
            HTuple hv_Column = new HTuple(), hv_Phi = new HTuple();
            HTuple hv_Length1 = new HTuple(), hv_Length2 = new HTuple();
            HTuple hv_RowBegin = new HTuple(), hv_ColBegin = new HTuple();
            HTuple hv_RowEnd = new HTuple(), hv_ColEnd = new HTuple();
            HTuple hv_Nr = new HTuple(), hv_Nc = new HTuple(), hv_Dist = new HTuple();
            HTuple hv_RowBeginOut = new HTuple(), hv_ColBeginOut = new HTuple();
            HTuple hv_RowEndOut = new HTuple(), hv_ColEndOut = new HTuple();
            HTuple hv_Distance = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing);
            HOperatorSet.GenEmptyObj(out ho_RegionErosion);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Edges);
            HOperatorSet.GenEmptyObj(out ho_SelectedContours);
            HOperatorSet.GenEmptyObj(out ho_SelectedContours1);
            HOperatorSet.GenEmptyObj(out ho_SelectedContours2);
            HOperatorSet.GenEmptyObj(out ho_ObjectsConcat);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
            HOperatorSet.GenEmptyObj(out ho_Edges2);
            HOperatorSet.GenEmptyObj(out ho_ContoursSplit);
            HOperatorSet.GenEmptyObj(out ho_UnionContours);
            HOperatorSet.GenEmptyObj(out ho_RegionLines);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            //t3 = DateTime.Now;
            try
            {
                ho_Region.Dispose();
                HOperatorSet.Threshold(Image, out ho_Region, 0, 128);
                ho_RegionClosing.Dispose();
                HOperatorSet.ClosingCircle(ho_Region, out ho_RegionClosing, 33.5);
                ho_RegionErosion.Dispose();
                HOperatorSet.ErosionCircle(ho_RegionClosing, out ho_RegionErosion, 13.5);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_RegionErosion, out ho_ImageReduced);
                ho_Edges.Dispose();
                HOperatorSet.EdgesSubPix(ho_ImageReduced, out ho_Edges, "canny", 1, 20, 20);
                ho_SelectedContours.Dispose();
                HOperatorSet.SelectContoursXld(ho_Edges, out ho_SelectedContours, "contour_length",
                    10, 400, -0.5, 0.5);
                ho_SelectedContours1.Dispose();
                HOperatorSet.SelectContoursXld(ho_SelectedContours, out ho_SelectedContours1,
                    "direction", (new HTuple(20)).TupleRad(), (new HTuple(70)).TupleRad(),
                    -0.5, 0.5);
                ho_SelectedContours2.Dispose();
                HOperatorSet.SelectContoursXld(ho_SelectedContours, out ho_SelectedContours2,
                    "direction", (new HTuple(110)).TupleRad(), (new HTuple(160)).TupleRad()
                    , -0.5, 0.5);
                ho_ObjectsConcat.Dispose();
                HOperatorSet.ConcatObj(ho_SelectedContours1, ho_SelectedContours2, out ho_ObjectsConcat
                    );
                ho_Region1.Dispose();
                HOperatorSet.GenRegionContourXld(ho_ObjectsConcat, out ho_Region1, "filled");
                ho_RegionUnion.Dispose();
                HOperatorSet.Union1(ho_Region1, out ho_RegionUnion);
                ho_RegionClosing1.Dispose();
                HOperatorSet.ClosingCircle(ho_RegionUnion, out ho_RegionClosing1, 15);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_RegionClosing1, out ho_ConnectedRegions);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                    "and", 1500, 999990);
                HOperatorSet.SmallestRectangle2(ho_SelectedRegions, out hv_Row, out hv_Column,
                    out hv_Phi, out hv_Length1, out hv_Length2);
                if ((int)(new HTuple((new HTuple(hv_Row.TupleLength())).TupleGreater(0))) != 0)
                {
                    ho_Rectangle.Dispose();
                    HOperatorSet.GenRectangle2(out ho_Rectangle, hv_Row, hv_Column, hv_Phi, hv_Length1,
                        hv_Length2);
                    ho_Rectangle1.Dispose();
                    HOperatorSet.GenRectangle2(out ho_Rectangle1, hv_Row1, hv_Column1, hv_Phi1,
                        hv_Length11, hv_Length21);
                    ho_ImageReduced1.Dispose();
                    HOperatorSet.ReduceDomain(Image, ho_Rectangle1, out ho_ImageReduced1);
                    ho_Edges2.Dispose();
                    HOperatorSet.EdgesSubPix(ho_ImageReduced1, out ho_Edges2, "canny", 1, 20,
                        20);
                    ho_ContoursSplit.Dispose();
                    HOperatorSet.SegmentContoursXld(ho_Edges2, out ho_ContoursSplit, "lines_circles",
                        5, 4, 2);
                    ho_UnionContours.Dispose();
                    HOperatorSet.UnionCollinearContoursXld(ho_ContoursSplit, out ho_UnionContours,
                        1000, 2, 11, 0.1, "attr_keep");
                    HOperatorSet.FitLineContourXld(ho_UnionContours, "tukey", -1, 0, 5, 2, out hv_RowBegin,
                        out hv_ColBegin, out hv_RowEnd, out hv_ColEnd, out hv_Nr, out hv_Nc,
                        out hv_Dist);
                    if (hv_ColEnd.TupleLength() > 0)
                    {
                        HOperatorSet.SelectLinesLongest(hv_RowBegin, hv_ColBegin, hv_RowEnd, hv_ColEnd,
                            1, out hv_RowBeginOut, out hv_ColBeginOut, out hv_RowEndOut, out hv_ColEndOut);
                        HOperatorSet.DistancePl(hv_Row, hv_Column, hv_RowBeginOut, hv_ColBeginOut,
                            hv_RowEndOut, hv_ColEndOut, out hv_Distance);
                        ho_RegionLines.Dispose();
                        HOperatorSet.GenRegionLine(out ho_RegionLines, hv_RowBeginOut, hv_ColBeginOut,
                            hv_RowEndOut, hv_ColEndOut);
                        HOperatorSet.Union2(ho_RegionLines, ho_Rectangle, out RegionToDisp);
                        HOperatorSet.TupleSort(hv_Distance, out hv_Sorted);
                        HTuple hv_result = GetHv_result();
                        for (hv_i = 0; (int)hv_i <= (int)((new HTuple(hv_Length1.TupleLength())) - 1); hv_i = (int)hv_i + 1)
                        {
                            hv_result = hv_result.TupleConcat("位置" + hv_i.I.ToString());
                            hv_result = hv_result.TupleConcat(hv_Sorted.TupleSelect(hv_i) * pixeldist);
                        }
                        result = hv_result.Clone();
                    }
                    ho_Region.Dispose();
                    ho_RegionClosing.Dispose();
                    ho_RegionErosion.Dispose();
                    ho_ImageReduced.Dispose();
                    ho_Edges.Dispose();
                    ho_SelectedContours.Dispose();
                    ho_SelectedContours1.Dispose();
                    ho_SelectedContours2.Dispose();
                    ho_ObjectsConcat.Dispose();
                    ho_Region1.Dispose();
                    ho_RegionUnion.Dispose();
                    ho_RegionClosing1.Dispose();
                    ho_ConnectedRegions.Dispose();
                    ho_SelectedRegions.Dispose();
                    ho_Rectangle.Dispose();
                    ho_Rectangle1.Dispose();
                    ho_ImageReduced1.Dispose();
                    ho_Edges2.Dispose();
                    ho_ContoursSplit.Dispose();
                    ho_UnionContours.Dispose();
                    ho_RegionLines.Dispose();
                    algorithm.Region.Dispose();
                    //t4 = DateTime.Now;
                }
                else
                {
                    HTuple hv_result = GetHv_result();
                    for (hv_i = 0; (int)hv_i <= 10; hv_i = (int)hv_i + 1)
                    {
                        hv_result = hv_result.TupleConcat("位置" + hv_i.I.ToString());
                        hv_result = hv_result.TupleConcat(0);
                    }
                    result = hv_result.Clone();
                    ho_Region.Dispose();
                    ho_RegionClosing.Dispose();
                    ho_RegionErosion.Dispose();
                    ho_ImageReduced.Dispose();
                    ho_Edges.Dispose();
                    ho_SelectedContours.Dispose();
                    ho_SelectedContours1.Dispose();
                    ho_SelectedContours2.Dispose();
                    ho_ObjectsConcat.Dispose();
                    ho_Region1.Dispose();
                    ho_RegionUnion.Dispose();
                    ho_RegionClosing1.Dispose();
                    ho_ConnectedRegions.Dispose();
                    ho_SelectedRegions.Dispose();
                    ho_Rectangle.Dispose();
                    ho_Rectangle1.Dispose();
                    ho_ImageReduced1.Dispose();
                    ho_Edges2.Dispose();
                    ho_ContoursSplit.Dispose();
                    ho_UnionContours.Dispose();
                    ho_RegionLines.Dispose();
                }
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                for (hv_i = 0; (int)hv_i <= 10; hv_i = (int)hv_i + 1)
                {
                    hv_result = hv_result.TupleConcat("位置" + hv_i.I.ToString());
                    hv_result = hv_result.TupleConcat(0);
                }
                result = hv_result.Clone();
                ho_Region.Dispose();
                ho_RegionClosing.Dispose();
                ho_RegionErosion.Dispose();
                ho_ImageReduced.Dispose();
                ho_Edges.Dispose();
                ho_SelectedContours.Dispose();
                ho_SelectedContours1.Dispose();
                ho_SelectedContours2.Dispose();
                ho_ObjectsConcat.Dispose();
                ho_Region1.Dispose();
                ho_RegionUnion.Dispose();
                ho_RegionClosing1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_Rectangle.Dispose();
                ho_Rectangle1.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Edges2.Dispose();
                ho_ContoursSplit.Dispose();
                ho_UnionContours.Dispose();
                ho_RegionLines.Dispose();

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