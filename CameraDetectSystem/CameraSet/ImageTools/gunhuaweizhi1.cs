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
    class gunhuaweizhi1 : ImageTools
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
        public double c_Row1 { set; get; }
        public double c_Column1 { set; get; }
        public double c_Phi1 { set; get; }
        public double thv { set; get; }
        public double c_Length11 { set; get; }
        public double c_Length21 { set; get; }
        public double c1_Row { set; get; }
        public double c1_Column { set; get; }
        public double c1_Phi { set; get; }
        public double c1_Length1 { set; get; }
        public double c1_Length2 { set; get; }
        public double c_Radius { set; get; }
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public gunhuaweizhi1()
        {
            RegionToDisp = Image;
        }
        public gunhuaweizhi1(HObject Image, Algorithm al)
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
            this.c_Row1 = dRow;
            this.c_Column1 = dColumn;
            this.c_Phi1 = dPhi;
            this.c_Length11 = dLength1;
            this.c_Length21 = dLength2;
        }
        //DateTime t1, t2, t3, t4,t5,t6,t7;
        private void action()
        {
            HObject ho_Region1 = null, ho_RegionClosing = null;
            HObject ho_RegionErosion = null, ho_ImageReduced = null, ho_ImageMedian = null;
            HObject ho_ImageDeviation = null, ho_Region2 = null, ho_RegionErosion1 = null;
            HObject ho_ConnectedRegions = null, ho_SelectedRegions = null;
            HObject ho_RegionClosing1 = null, ho_Rectangle = null, ho_Rectangle1 = null;
            HObject ho_ImageReduced1 = null, ho_Edges = null, ho_ContoursSplit = null;
            HObject ho_UnionContours = null, ho_SelectedContours = null;

            // Local control variables 

            HTuple hv_UsedThreshold = new HTuple(), hv_UsedThreshold1 = new HTuple(), hv_i=new HTuple();
            HTuple hv_Row1 = new HTuple(), hv_Column1 = new HTuple(), hv_Sorted=new HTuple();
            HTuple hv_Phi = new HTuple(), hv_Length1 = new HTuple();
            HTuple hv_Length2 = new HTuple(), hv_Mean = new HTuple();
            HTuple hv_Deviation = new HTuple(), hv_RowBegin = new HTuple();
            HTuple hv_ColBegin = new HTuple(), hv_RowEnd = new HTuple();
            HTuple hv_ColEnd = new HTuple(), hv_Nr = new HTuple();
            HTuple hv_Nc = new HTuple(), hv_Dist = new HTuple(), hv_RowBeginOut = new HTuple();
            HTuple hv_ColBeginOut = new HTuple(), hv_RowEndOut = new HTuple();
            HTuple hv_ColEndOut = new HTuple(), hv_Distance = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing);
            HOperatorSet.GenEmptyObj(out ho_RegionErosion);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_ImageMedian);
            HOperatorSet.GenEmptyObj(out ho_ImageDeviation);
            HOperatorSet.GenEmptyObj(out ho_Region2);
            HOperatorSet.GenEmptyObj(out ho_RegionErosion1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing1);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
            HOperatorSet.GenEmptyObj(out ho_Edges);
            HOperatorSet.GenEmptyObj(out ho_ContoursSplit);
            HOperatorSet.GenEmptyObj(out ho_UnionContours);
            HOperatorSet.GenEmptyObj(out ho_SelectedContours);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            //t3 = DateTime.Now;
            try
            {
                ho_Region1.Dispose();
                HOperatorSet.BinaryThreshold(Image, out ho_Region1, "max_separability",
                    "dark", out hv_UsedThreshold);
                ho_RegionClosing.Dispose();
                HOperatorSet.ClosingCircle(ho_Region1, out ho_RegionClosing, 35.5);
                ho_RegionErosion.Dispose();
                HOperatorSet.ErosionCircle(ho_RegionClosing, out ho_RegionErosion, 15.5);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_RegionErosion, out ho_ImageReduced);
                ho_ImageMedian.Dispose();
                HOperatorSet.MedianImage(ho_ImageReduced, out ho_ImageMedian, "circle", 5,
                    "mirrored");
                ho_ImageDeviation.Dispose();
                HOperatorSet.DeviationImage(ho_ImageMedian, out ho_ImageDeviation, 25, 25);
                ho_Region2.Dispose();
                HOperatorSet.BinaryThreshold(ho_ImageDeviation, out ho_Region2, "max_separability",
                    "light", out hv_UsedThreshold1);
                ho_RegionErosion1.Dispose();
                HOperatorSet.ErosionCircle(ho_Region2, out ho_RegionErosion1, 3.5);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_RegionErosion1, out ho_ConnectedRegions);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                    "and", 15000, 99999);
                ho_RegionClosing1.Dispose();
                HOperatorSet.ClosingCircle(ho_SelectedRegions, out ho_RegionClosing1, 35.5);
                HOperatorSet.SmallestRectangle2(ho_RegionClosing1, out hv_Row1, out hv_Column1,
                    out hv_Phi, out hv_Length1, out hv_Length2);
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle, hv_Row1, hv_Column1, hv_Phi, hv_Length1,
                    hv_Length2);
                ho_Rectangle1.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle1, c_Row1, c_Column1, c_Phi1, c_Length11,
                    c_Length21);
                ho_ImageReduced1.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Rectangle1, out ho_ImageReduced1);
                HOperatorSet.Intensity(ho_Rectangle1, ho_ImageReduced1, out hv_Mean, out hv_Deviation);
                if ((int)(new HTuple(hv_Deviation.TupleGreater(7))) != 0)
                {
                    ho_Edges.Dispose();
                    HOperatorSet.EdgesSubPix(ho_ImageReduced1, out ho_Edges, "canny", 1, 20,
                        40);
                    ho_ContoursSplit.Dispose();
                    HOperatorSet.SegmentContoursXld(ho_Edges, out ho_ContoursSplit, "lines_circles",
                        5, 4, 2);
                    ho_UnionContours.Dispose();
                    HOperatorSet.UnionCollinearContoursExtXld(ho_ContoursSplit, out ho_UnionContours,
                        2000, 20, 20, 0.2, 0, -1, 1, 1, 1, 1, 1, 0, "attr_keep");
                    ho_SelectedContours.Dispose();
                    HOperatorSet.SelectContoursXld(ho_UnionContours, out ho_SelectedContours,
                        "contour_length", 50, 20000, -0.5, 0.5);
                    HOperatorSet.FitLineContourXld(ho_SelectedContours, "tukey", -1, 0, 5, 2,
                        out hv_RowBegin, out hv_ColBegin, out hv_RowEnd, out hv_ColEnd, out hv_Nr,
                        out hv_Nc, out hv_Dist);
                    HOperatorSet.SelectLinesLongest(hv_RowBegin, hv_ColBegin, hv_RowEnd, hv_ColEnd,
                        1, out hv_RowBeginOut, out hv_ColBeginOut, out hv_RowEndOut, out hv_ColEndOut);
                    HOperatorSet.DistancePl(hv_Row1, hv_Column1, hv_RowBeginOut, hv_ColBeginOut,
                        hv_RowEndOut, hv_ColEndOut, out hv_Distance);
                }
                HOperatorSet.Union1(ho_Rectangle,out RegionToDisp);
                if ((int)(new HTuple((new HTuple(hv_Distance.TupleLength())).TupleGreater(0))) != 0)
                {
                    HOperatorSet.TupleSort(hv_Distance, out hv_Sorted);
                    HTuple hv_result = GetHv_result();
                    for (hv_i = (new HTuple(hv_Sorted.TupleLength())) - 1; (int)hv_i >= 0; hv_i = (int)hv_i - 1)
                    {
                        hv_result = hv_result.TupleConcat("位置" + hv_i.I.ToString());
                        hv_result = hv_result.TupleConcat(hv_Sorted.TupleSelect(hv_i) * pixeldist);
                    }
                    result = hv_result.Clone();

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

            }
            finally
            {
                ho_Region1.Dispose();
                ho_RegionClosing.Dispose();
                ho_RegionErosion.Dispose();
                ho_ImageReduced.Dispose();
                ho_ImageMedian.Dispose();
                ho_ImageDeviation.Dispose();
                ho_Region2.Dispose();
                ho_RegionErosion1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionClosing1.Dispose();
                ho_Rectangle.Dispose();
                ho_Rectangle1.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Edges.Dispose();
                ho_ContoursSplit.Dispose();
                ho_UnionContours.Dispose();
                ho_SelectedContours.Dispose();
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