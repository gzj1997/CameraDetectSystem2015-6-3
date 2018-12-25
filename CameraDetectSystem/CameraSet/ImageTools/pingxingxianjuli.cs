using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
    [Serializable]
    class pingxingxianjuli : ImageTools
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
        public pingxingxianjuli()
        {
            RegionToDisp = Image;
        }
        public pingxingxianjuli(HObject Image, Algorithm al)
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

            HObject ho_Rectangle, ho_ImageReduced;
            HObject ho_Border, ho_Region, ho_RegionUnion;
            HTuple hv_RowBegin = null;
            HTuple hv_ColBegin = null, hv_RowEnd = null, hv_ColEnd = null;
            HTuple hv_Nr = null, hv_Nc = null, hv_Dist = null, hv_dis = null;
            HTuple hv_Length = null, hv_i = null, hv_j = new HTuple();
            HTuple hv_temp = new HTuple(), hv_dianr = null, hv_dianc = null;
            HTuple hv_t = null, hv_jl = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Border);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion);
            try
            {
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle, hv_centerRowm, hv_centerColumnm, hv_Phim, hv_Length1m,
                    hv_Length2m);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Rectangle, out ho_ImageReduced);
                ho_Border.Dispose();
                HOperatorSet.ThresholdSubPix(ho_ImageReduced, out ho_Border, thv);
                HOperatorSet.FitLineContourXld(ho_Border, "tukey", -1, 0, 5, 2, out hv_RowBegin,
                    out hv_ColBegin, out hv_RowEnd, out hv_ColEnd, out hv_Nr, out hv_Nc, out hv_Dist);
                hv_dis = ((((hv_RowBegin - hv_RowEnd) * (hv_RowBegin - hv_RowEnd)) + ((hv_ColBegin - hv_ColEnd) * (hv_ColBegin - hv_ColEnd)))).TupleSqrt()
                    ;
                HOperatorSet.TupleLength(hv_dis, out hv_Length);
                if (hv_Length.I >= 2)
                {
                    HTuple end_val8 = hv_Length - 2;
                    HTuple step_val8 = 1;
                    for (hv_i = 0; hv_i.Continue(end_val8, step_val8); hv_i = hv_i.TupleAdd(step_val8))
                    {
                        HTuple end_val9 = hv_Length - 1;
                        HTuple step_val9 = 1;
                        for (hv_j = hv_i + 1; hv_j.Continue(end_val9, step_val9); hv_j = hv_j.TupleAdd(step_val9))
                        {
                            if ((int)(new HTuple(((hv_dis.TupleSelect(hv_i))).TupleLess(hv_dis.TupleSelect(
                                hv_j)))) != 0)
                            {
                                hv_temp = hv_dis.TupleSelect(hv_i);
                                if (hv_dis == null)
                                    hv_dis = new HTuple();
                                hv_dis[hv_i] = hv_dis.TupleSelect(hv_j);
                                if (hv_dis == null)
                                    hv_dis = new HTuple();
                                hv_dis[hv_j] = hv_temp;
                                hv_temp = hv_RowBegin.TupleSelect(hv_i);
                                if (hv_RowBegin == null)
                                    hv_RowBegin = new HTuple();
                                hv_RowBegin[hv_i] = hv_RowBegin.TupleSelect(hv_j);
                                if (hv_RowBegin == null)
                                    hv_RowBegin = new HTuple();
                                hv_RowBegin[hv_j] = hv_temp;
                                hv_temp = hv_ColBegin.TupleSelect(hv_i);
                                if (hv_ColBegin == null)
                                    hv_ColBegin = new HTuple();
                                hv_ColBegin[hv_i] = hv_ColBegin.TupleSelect(hv_j);
                                if (hv_ColBegin == null)
                                    hv_ColBegin = new HTuple();
                                hv_ColBegin[hv_j] = hv_temp;
                                hv_temp = hv_RowEnd.TupleSelect(hv_i);
                                if (hv_RowEnd == null)
                                    hv_RowEnd = new HTuple();
                                hv_RowEnd[hv_i] = hv_RowEnd.TupleSelect(hv_j);
                                if (hv_RowEnd == null)
                                    hv_RowEnd = new HTuple();
                                hv_RowEnd[hv_j] = hv_temp;
                                hv_temp = hv_ColEnd.TupleSelect(hv_i);
                                if (hv_ColEnd == null)
                                    hv_ColEnd = new HTuple();
                                hv_ColEnd[hv_i] = hv_ColEnd.TupleSelect(hv_j);
                                if (hv_ColEnd == null)
                                    hv_ColEnd = new HTuple();
                                hv_ColEnd[hv_j] = hv_temp;
                            }
                        }
                    }
                    hv_dianr = ((hv_RowBegin.TupleSelect(0)) + (hv_RowEnd.TupleSelect(0))) / 2;
                    hv_dianc = ((hv_ColBegin.TupleSelect(0)) + (hv_ColEnd.TupleSelect(0))) / 2;
                    hv_t = ((-(hv_RowBegin.TupleSelect(1))) + (hv_RowEnd.TupleSelect(1))) / ((hv_ColBegin.TupleSelect(
                        1)) - (hv_ColEnd.TupleSelect(1)));
                    hv_jl = ((((((hv_t * hv_dianc) + hv_dianr) - (hv_RowBegin.TupleSelect(1))) - ((hv_ColBegin.TupleSelect(
                        1)) * hv_t)) / ((((hv_t * hv_t) + 1)).TupleSqrt()))).TupleAbs();
                    ho_Region.Dispose();
                    HOperatorSet.GenRegionContourXld(ho_Border, out ho_Region, "filled");
                    ho_RegionUnion.Dispose();
                    HOperatorSet.Union2(ho_Region, ho_Region, out RegionToDisp);
                }


                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("ParallelDistance");
                hv_result = hv_result.TupleConcat(hv_jl.D * pixeldist);
                result = hv_result.Clone();

                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Border.Dispose();
                ho_Region.Dispose();
                ho_RegionUnion.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("ParallelDistance");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();

                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Border.Dispose();
                ho_Region.Dispose();
                ho_RegionUnion.Dispose();
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