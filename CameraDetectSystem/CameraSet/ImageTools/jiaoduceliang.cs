using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
    [Serializable]
    class jiaoduceliang : ImageTools
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
        [field: NonSerializedAttribute()]
        HTuple hv_ModelID = null;
        #endregion

        public double hv_Length1 { set; get; }
        public double hv_Length2 { set; get; }
        public double hv_Phi { set; get; }
        public double thv { set; get; }
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public jiaoduceliang()
        {
            RegionToDisp = Image;
        }
        public jiaoduceliang(HObject Image, Algorithm al)
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
            HTuple dcenterRow = null, dcenterColumn = null;
            HObject ho_Rectangle, ho_ImageReduced;
            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.DrawRectangle2(this.LWindowHandle, out dcenterRow, out dcenterColumn,
    out dPhi, out dLength1, out dLength2);
            HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            thv = thresholdValue.D;
            ho_Rectangle.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangle, dcenterRow, dcenterColumn, dPhi, dLength1,
                dLength2);
            ho_ImageReduced.Dispose();
            HOperatorSet.ReduceDomain(Image, ho_Rectangle, out ho_ImageReduced);
            HOperatorSet.CreateNccModel(ho_ImageReduced, "auto", -0.2, 0.39, 0.0175, "use_polarity",
                out hv_ModelID);
            HOperatorSet.WriteNccModel(hv_ModelID, PathHelper.currentProductPath + @"\jdmx.ncm");

            this.hv_Length1 = dLength1.D;
            this.hv_Length2 = dLength2.D;
            this.hv_Phi = dPhi.D;
            ho_Rectangle.Dispose();
            ho_ImageReduced.Dispose();
        }
        private void action()
        {
            HObject ho_Rectangle1, ho_ImageReduced1, ho_Border, ho_ContoursSplit, ho_RegionLines;
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
            HOperatorSet.GenEmptyObj(out ho_Border);
            HOperatorSet.GenEmptyObj(out ho_ContoursSplit);
            HOperatorSet.GenEmptyObj(out ho_RegionLines);
            HTuple hv_RowBeginOut = null, hv_ColBeginOut = null, hv_RowEndOut = null, hv_ColEndOut = null;
            if (hv_ModelID == null)
            {
                HOperatorSet.ReadNccModel(PathHelper.currentProductPath + @"\jdmx.ncm", out hv_ModelID);
            }
            HTuple hv_ColBegin = null, hv_RowEnd = null, hv_ColEnd = null, hv_Angle1 = null, hv_jiaodu = null;
            HTuple hv_Nr = null, hv_Nc = null, hv_Dist = null, hv_RowBegin = null;
            HTuple hv_Angle = null, hv_Score = null, hv_Row1 = null, hv_Column1 = null;
            HOperatorSet.FindNccModel(Image, hv_ModelID, -0.2, 0.39, 0.8, 1, 0.5, "true",
                0, out hv_Row1, out hv_Column1, out hv_Angle, out hv_Score);
            ho_Rectangle1.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangle1, hv_Row1, hv_Column1, hv_Phi + hv_Angle,
                hv_Length1, hv_Length2);
            ho_ImageReduced1.Dispose();
            HOperatorSet.ReduceDomain(Image, ho_Rectangle1, out ho_ImageReduced1);
            ho_Border.Dispose();
            HOperatorSet.ThresholdSubPix(ho_ImageReduced1, out ho_Border, thv);
            ho_ContoursSplit.Dispose();
            HOperatorSet.SegmentContoursXld(ho_Border, out ho_ContoursSplit, "lines_circles",
                5, 10, 10);
            HOperatorSet.FitLineContourXld(ho_ContoursSplit, "tukey", -1, 0, 5, 2, out hv_RowBegin,
                out hv_ColBegin, out hv_RowEnd, out hv_ColEnd, out hv_Nr, out hv_Nc, out hv_Dist);
            HOperatorSet.SelectLinesLongest(hv_RowBegin, hv_ColBegin, hv_RowEnd, hv_ColEnd,
    2, out hv_RowBeginOut, out hv_ColBeginOut, out hv_RowEndOut, out hv_ColEndOut);
            HOperatorSet.AngleLl(hv_RowBeginOut.TupleSelect(0), hv_ColBeginOut.TupleSelect(0),
                 hv_RowEndOut.TupleSelect(0), hv_ColEndOut.TupleSelect(0), hv_RowBeginOut.TupleSelect(
                 1), hv_ColBeginOut.TupleSelect(1), hv_RowEndOut.TupleSelect(1), hv_ColEndOut.TupleSelect(
                 1), out hv_Angle1);
            ho_RegionLines.Dispose();
            HOperatorSet.GenRegionLine(out ho_RegionLines, hv_RowBeginOut, hv_ColBeginOut,
                hv_RowEndOut, hv_ColEndOut);
            HOperatorSet.Union2(ho_RegionLines, ho_RegionLines, out RegionToDisp);
            hv_jiaodu = ((hv_Angle1.TupleDeg())).TupleAbs();
            HTuple hv_result = GetHv_result();
            hv_result = hv_result.TupleConcat("夹角");
            hv_result = hv_result.TupleConcat(hv_jiaodu.D);
            result = hv_result.Clone();
            ho_Rectangle1.Dispose();
            ho_ImageReduced1.Dispose();
            ho_Border.Dispose();
            ho_ContoursSplit.Dispose();
            ho_RegionLines.Dispose();
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