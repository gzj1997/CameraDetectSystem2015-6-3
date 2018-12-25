using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
    [Serializable]
    class xiaoziceliang : ImageTools
    {
        #region ROI
        [NonSerialized]
        private HTuple dcenterRow = new HTuple();
        [NonSerialized]
        private HTuple dcenterColumn = new HTuple();
        [NonSerialized]
        private HTuple dra = new HTuple();
        [NonSerialized]
        private HTuple mianjisx = new HTuple();
        [NonSerialized]
        private HTuple mianjixx = new HTuple();
        [NonSerialized]
        private HTuple thresholdValue = new HTuple();
        #endregion

        //public double zxr { set; get; }
        //public double zxc { set; get; }
        //public double ra { set; get; }
        //public double thv { set; get; }
        //public double mjsx { set; get; }
        //public double mjxx { set; get; }
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public xiaoziceliang()
        {
            RegionToDisp = Image;
        }
        public xiaoziceliang(HObject Image, Algorithm al)
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
            //HTuple dcenterRow = null, dcenterColumn = null, dra = null;
            //HObject ho_circle;
            //HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            //HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            //HOperatorSet.GenEmptyObj(out ho_circle);
            //HOperatorSet.DrawCircle(this.LWindowHandle, out dcenterRow, out dcenterColumn, out dra);
            //HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            //thv = thresholdValue.D;
            //HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\mianjixx", out mianjixx);
            //mjxx = mianjixx.D;
            //HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\mianjisx", out mianjisx);
            //mjsx = mianjisx.D;
            //this.zxr = dcenterRow.D;
            //this.zxc = dcenterColumn.D;
            //this.ra = dra.D;
        }
        private void action()
        {
            HObject ho_Edges, ho_UnionContours;
            HObject ho_Region1, ho_SelectedRegions3, ho_SelectedRegions1;
            HObject ho_SelectedRegions, ho_Rectangle, ho_Rectangle1;
            HObject ho_RegionIntersection, ho_RegionIntersection1, ho_Rectangle2;
            HObject ho_Rectangle3, ho_RegionDifference, ho_RegionDifference1;

            // Local control variables 

            HTuple hv_Row = null, hv_Column = null, hv_Phi = null;
            HTuple hv_Length1 = null, hv_Length2 = null, hv_Row1 = null;
            HTuple hv_Column1 = null, hv_Phi1 = null, hv_Length11 = null;
            HTuple hv_Length21 = null, hv_Row2 = null, hv_Column2 = null;
            HTuple hv_Phi2 = null, hv_Length12 = null, hv_Length22 = null;
            HTuple hv_Area = null, hv_Row3 = null, hv_Column3 = null;
            HTuple hv_Area1 = null, hv_Row4 = null, hv_Column4 = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Edges);
            HOperatorSet.GenEmptyObj(out ho_UnionContours);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions3);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection1);
            HOperatorSet.GenEmptyObj(out ho_Rectangle2);
            HOperatorSet.GenEmptyObj(out ho_Rectangle3);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference1);
            try
            {
                ho_Edges.Dispose();
                HOperatorSet.EdgesSubPix(Image, out ho_Edges, "canny", 3, 1, 100);
                ho_UnionContours.Dispose();
                HOperatorSet.UnionAdjacentContoursXld(ho_Edges, out ho_UnionContours, 50, 1,
                    "attr_forget");
                ho_Region1.Dispose();
                HOperatorSet.GenRegionContourXld(ho_UnionContours, out ho_Region1, "filled");
                ho_SelectedRegions3.Dispose();
                HOperatorSet.SelectShape(ho_Region1, out ho_SelectedRegions3, "area", "and",
                    15000, 9999900);
                ho_SelectedRegions1.Dispose();
                HOperatorSet.Union1(ho_SelectedRegions3, out ho_SelectedRegions1);
                ho_SelectedRegions.Dispose();
                HOperatorSet.FillUp(ho_SelectedRegions1, out ho_SelectedRegions);
                HOperatorSet.SmallestRectangle2(ho_SelectedRegions, out hv_Row, out hv_Column,
                    out hv_Phi, out hv_Length1, out hv_Length2);
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle, hv_Row - ((hv_Length1 * 0.5) * (hv_Phi.TupleSin()
                    )), hv_Column + ((hv_Length1 * 0.5) * (hv_Phi.TupleCos())), hv_Phi, 20, hv_Length2);
                ho_Rectangle1.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle1, hv_Row + ((hv_Length1 * 0.5) * (hv_Phi.TupleSin()
                    )), hv_Column - ((hv_Length1 * 0.5) * (hv_Phi.TupleCos())), hv_Phi, 20, hv_Length2);
                ho_RegionIntersection.Dispose();
                HOperatorSet.Intersection(ho_Rectangle, ho_SelectedRegions, out ho_RegionIntersection
                    );
                ho_RegionIntersection1.Dispose();
                HOperatorSet.Intersection(ho_Rectangle1, ho_SelectedRegions, out ho_RegionIntersection1
                    );
                HOperatorSet.SmallestRectangle2(ho_RegionIntersection, out hv_Row1, out hv_Column1,
                    out hv_Phi1, out hv_Length11, out hv_Length21);
                HOperatorSet.SmallestRectangle2(ho_RegionIntersection1, out hv_Row2, out hv_Column2,
                    out hv_Phi2, out hv_Length12, out hv_Length22);
                ho_Rectangle2.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle2, hv_Row - ((hv_Length1 * 0.8) * (hv_Phi.TupleSin()
                    )), hv_Column + ((hv_Length1 * 0.8) * (hv_Phi.TupleCos())), hv_Phi, hv_Length1 * 0.2,
                    hv_Length11);
                ho_Rectangle3.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle3, hv_Row + ((hv_Length1 * 0.8) * (hv_Phi.TupleSin()
                    )), hv_Column - ((hv_Length1 * 0.8) * (hv_Phi.TupleCos())), hv_Phi, hv_Length1 * 0.2,
                    hv_Length12);
                ho_RegionDifference.Dispose();
                HOperatorSet.Difference(ho_Rectangle2, ho_SelectedRegions, out ho_RegionDifference
                    );
                ho_RegionDifference1.Dispose();
                HOperatorSet.Difference(ho_Rectangle3, ho_SelectedRegions, out ho_RegionDifference1
                    );
                HOperatorSet.AreaCenter(ho_RegionDifference, out hv_Area, out hv_Row3, out hv_Column3);
                HOperatorSet.AreaCenter(ho_RegionDifference1, out hv_Area1, out hv_Row4, out hv_Column4);
                HOperatorSet.Union1(ho_SelectedRegions, out RegionToDisp);

                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("长度");
                hv_result = hv_result.TupleConcat(hv_Length1.D * pixeldist*2);
                hv_result = hv_result.TupleConcat("直径1");
                hv_result = hv_result.TupleConcat(hv_Length2.D * pixeldist*2);
                hv_result = hv_result.TupleConcat("直径2");
                hv_result = hv_result.TupleConcat(hv_Length11.D * pixeldist*2);
                hv_result = hv_result.TupleConcat("直径3");
                hv_result = hv_result.TupleConcat(hv_Length12.D * pixeldist*2);
                hv_result = hv_result.TupleConcat("倒角1");
                hv_result = hv_result.TupleConcat(hv_Area.D);
                hv_result = hv_result.TupleConcat("倒角2");
                hv_result = hv_result.TupleConcat(hv_Area1.D);
                result = hv_result.Clone();

            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("长度");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("直径1");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("直径2");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("直径3");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("倒角1");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("倒角2");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();
            }
            ho_Edges.Dispose();
            ho_UnionContours.Dispose();
            ho_Region1.Dispose();
            ho_SelectedRegions3.Dispose();
            ho_SelectedRegions1.Dispose();
            ho_SelectedRegions.Dispose();
            ho_Rectangle.Dispose();
            ho_Rectangle1.Dispose();
            ho_RegionIntersection.Dispose();
            ho_RegionIntersection1.Dispose();
            ho_Rectangle2.Dispose();
            ho_Rectangle3.Dispose();
            ho_RegionDifference.Dispose();
            ho_RegionDifference1.Dispose();
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