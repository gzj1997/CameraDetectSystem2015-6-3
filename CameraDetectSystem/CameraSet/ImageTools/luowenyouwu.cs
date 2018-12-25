using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
    [Serializable]
    class luowenyouwu : ImageTools
    {
        #region ROI
        [NonSerialized]
        private HTuple dPhi = new HTuple();
        [NonSerialized]
        private HTuple dLength1 = new HTuple();
        [NonSerialized]
        private HTuple dLength2 = new HTuple();
        [NonSerialized]

        private HTuple dcenterRow = new HTuple();
        [NonSerialized]
        private HTuple dcenterColumn = new HTuple();
        #endregion

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
        public luowenyouwu()
        {
            RegionToDisp = Image;
        }
        public luowenyouwu(HObject Image, Algorithm al)
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

            HTuple hv_Number = null;

            HObject ho_ImageCED = null, ho_Edges = null;
            HObject ho_ContoursSplit1 = null, ho_EmptyObject = null, ho_ObjectSelected = null;
            HObject ho_Region = null;
            HTuple hv_i = new HTuple();
            HTuple hv_Attrib = new HTuple(), hv_Row1 = new HTuple(), hv_Number1 = new HTuple();
            HTuple hv_Column = new HTuple(), hv_Radius = new HTuple();
            HTuple hv_StartPhi = new HTuple(), hv_EndPhi = new HTuple();
            HTuple hv_PointOrder = new HTuple(), hv_Row = new HTuple();
            HTuple hv_Col = new HTuple(),RR=new HTuple(),CC=new HTuple();

            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_ImageCED);
            HOperatorSet.GenEmptyObj(out ho_Edges);
            HOperatorSet.GenEmptyObj(out ho_ContoursSplit1);
            HOperatorSet.GenEmptyObj(out ho_EmptyObject);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle, hv_centerRowm, hv_centerColumnm, hv_Phim, hv_Length1m,
                    hv_Length2m);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Rectangle, out ho_ImageReduced);
                ho_ImageCED.Dispose();
                HOperatorSet.CoherenceEnhancingDiff(ho_ImageReduced, out ho_ImageCED, 0.1,
                    3, 0.5, 2);

                ho_Edges.Dispose();
                HOperatorSet.EdgesSubPix(ho_ImageCED, out ho_Edges, "canny", 1, 5, 20);
                ho_ContoursSplit1.Dispose();
                HOperatorSet.SegmentContoursXld(ho_Edges, out ho_ContoursSplit1, "lines_circles",
                    7, 7, 2);
                HOperatorSet.CountObj(ho_ContoursSplit1, out hv_Number1);
                hv_Number = 0;
                ho_EmptyObject.Dispose();
                HOperatorSet.GenEmptyObj(out ho_EmptyObject);
                HTuple end_val13 = hv_Number1;
                HTuple step_val13 = 1;
                for (hv_i = 1; hv_i.Continue(end_val13, step_val13); hv_i = hv_i.TupleAdd(step_val13))
                {
                    ho_ObjectSelected.Dispose();
                    HOperatorSet.SelectObj(ho_ContoursSplit1, out ho_ObjectSelected, hv_i);
                    HOperatorSet.GetContourGlobalAttribXld(ho_ObjectSelected, "cont_approx",
                        out hv_Attrib);
                    if ((int)(new HTuple(hv_Attrib.TupleGreater(-1))) != 0)
                    {
                        HOperatorSet.FitCircleContourXld(ho_ObjectSelected, "algebraic", -1, 0,
                            0, 3, 2, out hv_Row1, out hv_Column, out hv_Radius, out hv_StartPhi,
                            out hv_EndPhi, out hv_PointOrder);
                        if ((int)((new HTuple(hv_Column.TupleLess(hv_centerRowm - hv_Length1m / 10))).TupleAnd(new HTuple(hv_Radius.TupleGreater(
                            hv_Length1m*0.3)))) != 0)
                        {
                            HOperatorSet.GetContourXld(ho_ObjectSelected,out RR,out CC);
                            hv_Number = hv_Number + RR.TupleLength();
                            HOperatorSet.GetContourXld(ho_ObjectSelected, out hv_Row, out hv_Col);
                            ho_Region.Dispose();
                            HOperatorSet.GenRegionPoints(out ho_Region, hv_Row, hv_Col);
                            {
                                HObject ExpTmpOutVar_0;
                                HOperatorSet.ConcatObj(ho_EmptyObject, ho_Region, out ExpTmpOutVar_0);
                                ho_EmptyObject.Dispose();
                                ho_EmptyObject = ExpTmpOutVar_0;
                            }
                        }
                    }
                }
                HOperatorSet.Union1(ho_EmptyObject, out RegionToDisp);
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("螺纹数量");
                hv_result = hv_result.TupleConcat(hv_Number.I);
                result = hv_result.Clone();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("螺纹数量");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();
            }
            finally
            {
                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_ImageCED.Dispose();
                ho_Edges.Dispose();
                ho_ContoursSplit1.Dispose();
                ho_EmptyObject.Dispose();
                ho_ObjectSelected.Dispose();
                ho_Region.Dispose();
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