using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
    [Serializable]
    class chilungaodu : ImageTools
    {
        #region ROI
        //[NonSerialized]
        //private HTuple dPhi = new HTuple();
        //[NonSerialized]
        //private HTuple dLength1 = new HTuple();
        //[NonSerialized]
        //private HTuple dLength2 = new HTuple();
        //[NonSerialized]
        //private HTuple thresholdValue = new HTuple();
        [NonSerialized]
        private HTuple dcenterRow1 = new HTuple();
        [NonSerialized]
        private HTuple dcenterColumn1 = new HTuple();


        [NonSerialized]
        private HTuple dcenterRow2 = new HTuple();
        [NonSerialized]
        private HTuple dcenterColumn2 = new HTuple();
        //[NonSerialized]
        //private HTuple dcenterRow1 = new HTuple();
        //[NonSerialized]
        //private HTuple dcenterColumn1 = new HTuple();
        //[NonSerialized]
        //private HTuple dPhi1 = new HTuple();
        //[NonSerialized]
        //private HTuple dLength11 = new HTuple();
        //[NonSerialized]
        //private HTuple dLength21 = new HTuple();

        //[NonSerialized]
        //private HTuple dcenterRow2 = new HTuple();
        //[NonSerialized]
        //private HTuple dcenterColumn2 = new HTuple();
        //[NonSerialized]
        //private HTuple dPhi2 = new HTuple();
        //[NonSerialized]
        //private HTuple dLength12 = new HTuple();
        //[NonSerialized]
        //private HTuple dLength22 = new HTuple();
        #endregion
        //public double thv { set; get; }
        //public double hv_Length1m { set; get; }
        //public double hv_Length2m { set; get; }
        //public double hv_Phim { set; get; }
        public double hv_centerRowm1 { set; get; }
        public double hv_centerColumnm1 { set; get; }

        public double hv_centerRowm2 { set; get; }
        public double hv_centerColumnm2 { set; get; }
        //public double hv_Length1m1 { set; get; }
        //public double hv_Length2m1 { set; get; }
        //public double hv_Phim1 { set; get; }
        //public double hv_centerRowm1 { set; get; }
        //public double hv_centerColumnm1 { set; get; }

        //public double hv_Length1m2 { set; get; }
        //public double hv_Length2m2 { set; get; }
        //public double hv_Phim2 { set; get; }
        //public double hv_centerRowm2 { set; get; }
        //public double hv_centerColumnm2 { set; get; }


        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public chilungaodu()
        {
            RegionToDisp = Image;
        }
        public chilungaodu(HObject Image, Algorithm al)
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
            //HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            //thv = thresholdValue.D;
            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            HOperatorSet.DrawRectangle1(this.LWindowHandle, out dcenterRow1, out dcenterColumn1,
    out dcenterRow2, out dcenterColumn2);
            //this.hv_Length1m = dLength1.D;
            //this.hv_Length2m = dLength2.D;
            //this.hv_Phim = dPhi.D;
            this.hv_centerRowm1 = dcenterRow1.D;
            this.hv_centerColumnm1 = dcenterColumn1.D;
            this.hv_centerRowm2 = dcenterRow2.D;
            this.hv_centerColumnm2 = dcenterColumn2.D;
            //            HOperatorSet.DrawRectangle2(this.LWindowHandle, out dcenterRow1, out dcenterColumn1,
            //out dPhi1, out dLength11, out dLength21);
            //            this.hv_Length1m1 = dLength11.D;
            //            this.hv_Length2m1 = dLength21.D;
            //            this.hv_Phim1 = dPhi1.D;
            //            this.hv_centerRowm1 = dcenterRow1.D;
            //            this.hv_centerColumnm1 = dcenterColumn1.D;

            //            HOperatorSet.DrawRectangle2(this.LWindowHandle, out dcenterRow2, out dcenterColumn2,
            //out dPhi2, out dLength12, out dLength22);
            //            this.hv_Length1m2 = dLength12.D;
            //            this.hv_Length2m2 = dLength22.D;
            //            this.hv_Phim2 = dPhi2.D;
            //            this.hv_centerRowm2 = dcenterRow2.D;
            //            this.hv_centerColumnm2 = dcenterColumn2.D;



        }
        private void action()
        {


            // Local iconic variables 

            HObject ho_Rectangle, ho_ImageReduced;
            HObject ho_Region, ho_Rectangle2q, ho_ImageReduced2, ho_Region1;
            HObject ho_ConnectedRegions, ho_SelectedRegions, ho_Rectangle2w;

            // Local control variables 

            HTuple hv_Ro1 = null, hv_Col1 = null, hv_Ro2 = null;
            HTuple hv_Col2 = null, hv_Rowq1 = null, hv_Columnq1 = null;
            HTuple hv_Phiq2 = null, hv_Length1q2 = null, hv_Length2q2 = null;
            HTuple hv_Roww1 = null, hv_Columnw1 = null, hv_Phiw2 = null;
            HTuple hv_Length1w2 = null, hv_Length2w2 = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_Rectangle2q);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced2);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_Rectangle2w);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle, hv_centerRowm1, hv_centerColumnm1, hv_centerRowm2, hv_centerColumnm2);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Rectangle, out ho_ImageReduced);
                //*reduce_domain(Image, Rectangle1, ImageReduced1)
                //*reduce_domain(Image, Rectangle2, ImageReduced2)
                //*intensity(Rectangle, ImageReduced, M1, D1)
                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, 200, 255);
                HOperatorSet.SmallestRectangle2(ho_Region, out hv_Rowq1, out hv_Columnq1, out hv_Phiq2,
                    out hv_Length1q2, out hv_Length2q2);
                ho_Rectangle2q.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle2q, hv_Rowq1, hv_Columnq1, hv_Phiq2,
                    hv_Length1q2, hv_Length2q2);
                ho_ImageReduced2.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Rectangle2q, out ho_ImageReduced2);
                ho_Region1.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced2, out ho_Region1, 0, 60);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Region1, out ho_ConnectedRegions);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                    "and", 1500, 999990000);

                //*threshold(ImageReduced2, Region1, 0, 128)
                HOperatorSet.SmallestRectangle2(ho_SelectedRegions, out hv_Roww1, out hv_Columnw1,
                    out hv_Phiw2, out hv_Length1w2, out hv_Length2w2);
                ho_Rectangle2w.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle2w, hv_Roww1, hv_Columnw1, hv_Phiw2,
                    hv_Length1w2, hv_Length2w2);
                HOperatorSet.Union1(ho_Rectangle2w, out RegionToDisp);
                //}

                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("高度");
                hv_result = hv_result.TupleConcat(hv_Length2w2.D * pixeldist * 2);
                hv_result = hv_result.TupleConcat("高度");
                hv_result = hv_result.TupleConcat(hv_Length1w2.D * pixeldist * 2);
                result = hv_result.Clone();

                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_Rectangle2q.Dispose();
                ho_ImageReduced2.Dispose();
                ho_Region1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_Rectangle2w.Dispose();
                algorithm.Region.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("高度");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("高度");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();

                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_Rectangle2q.Dispose();
                ho_ImageReduced2.Dispose();
                ho_Region1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_Rectangle2w.Dispose();
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
