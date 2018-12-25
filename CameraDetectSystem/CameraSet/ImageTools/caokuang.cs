using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
    [Serializable]
    class caokuang : ImageTools
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
        public caokuang()
        {
            RegionToDisp = Image;
        }
        public caokuang(HObject Image, Algorithm al)
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
            HObject ho_ImageMedian, ho_Regions, ho_ObjectSelected, ho_Rectangle1;
            HObject ho_Rectangle2, ho_RegionDifference, ho_RegionDifference1;
            HObject ho_ConnectedRegions;
            HTuple hv_Row1 = null;
            HTuple hv_Column1 = null, hv_Phi1 = null, hv_Length11 = null;
            HTuple hv_Length21 = null, hv_Area = null, hv_Row2 = null;
            HTuple hv_Column2 = null, hv_a1 = null, hv_Indices = null;
            HTuple hv_Reduced = null, hv_a2 = null, hv_Row3 = null;
            HTuple hv_Column3 = null, hv_Phi2 = null, hv_Length12 = null;
            HTuple hv_Length22 = null, hv_caokuang = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_ImageMedian);
            HOperatorSet.GenEmptyObj(out ho_Regions);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected);
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_Rectangle2);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            try
            {
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle, hv_centerRowm, hv_centerColumnm, hv_Phim, hv_Length1m,
                    hv_Length2m);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Rectangle, out ho_ImageReduced);
                ho_ImageMedian.Dispose();
                HOperatorSet.MedianImage(ho_ImageReduced, out ho_ImageMedian, "circle", 3, "mirrored");
                ho_Regions.Dispose();
                HOperatorSet.AutoThreshold(ho_ImageMedian, out ho_Regions, 7);
                ho_ObjectSelected.Dispose();
                HOperatorSet.SelectObj(ho_Regions, out ho_ObjectSelected, 1);
                HOperatorSet.SmallestRectangle2(ho_ObjectSelected, out hv_Row1, out hv_Column1,
                    out hv_Phi1, out hv_Length11, out hv_Length21);
                ho_Rectangle1.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle1, hv_Row1, hv_Column1, hv_Phi1, hv_Length11 - 3,
                    hv_Length21 - 3);
                ho_Rectangle2.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle2, hv_Row1, hv_Column1, hv_Phi1, hv_Length11 - 5,
                    hv_Length21 - 5);
                ho_RegionDifference.Dispose();
                HOperatorSet.Difference(ho_Rectangle1, ho_Rectangle2, out ho_RegionDifference
                    );
                ho_RegionDifference1.Dispose();
                HOperatorSet.Difference(ho_RegionDifference, ho_ObjectSelected, out ho_RegionDifference1
                    );
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_RegionDifference1, out ho_ConnectedRegions);
                HOperatorSet.AreaCenter(ho_ConnectedRegions, out hv_Area, out hv_Row2, out hv_Column2);
                hv_a1 = hv_Area.TupleMax();
                HOperatorSet.TupleFind(hv_Area, hv_a1, out hv_Indices);
                HOperatorSet.TupleRemove(hv_Area, hv_Indices, out hv_Reduced);
                hv_a2 = hv_Reduced.TupleMax();
                HOperatorSet.SmallestRectangle2(ho_ConnectedRegions, out hv_Row3, out hv_Column3,
                    out hv_Phi2, out hv_Length12, out hv_Length22);
                hv_caokuang = (hv_a1 + hv_a2) / 4.0;
                HOperatorSet.Union1(ho_RegionDifference1,out RegionToDisp);

                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("caokuang");
                hv_result = hv_result.TupleConcat(hv_caokuang.D * pixeldist);
                result = hv_result.Clone();

                ho_Rectangle.Dispose();
                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_ImageMedian.Dispose();
                ho_Regions.Dispose();
                ho_ObjectSelected.Dispose();
                ho_Rectangle1.Dispose();
                ho_Rectangle2.Dispose();
                ho_RegionDifference.Dispose();
                ho_RegionDifference1.Dispose();
                ho_ConnectedRegions.Dispose();
                algorithm.Region.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("caokuang");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();

                ho_Rectangle.Dispose();
                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_ImageMedian.Dispose();
                ho_Regions.Dispose();
                ho_ObjectSelected.Dispose();
                ho_Rectangle1.Dispose();
                ho_Rectangle2.Dispose();
                ho_RegionDifference.Dispose();
                ho_RegionDifference1.Dispose();
                ho_ConnectedRegions.Dispose();
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