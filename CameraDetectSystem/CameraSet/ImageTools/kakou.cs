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
    class kakou : ImageTools
    {
        #region ROI
        [NonSerialized]
        private HTuple Phi = new HTuple();
        [NonSerialized]
        private HTuple Length1 = new HTuple();
        [NonSerialized]
        private HTuple Length2 = new HTuple();
        [NonSerialized]
        private HTuple thresholdValue = new HTuple();
        [NonSerialized]

        private HTuple centerRow = new HTuple();
        [NonSerialized]
        private HTuple centerColumn = new HTuple();

        //public double Dthv { set; get; }
        public double DLength1 { set; get; }
        public double DLength2 { set; get; }
        public double DPhi { set; get; }
        public double DcenterRow { set; get; }
        public double DcenterColumn { set; get; }
        #endregion
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public kakou()
        {
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public kakou(HObject Image, Algorithm al)
        {
            //gexxs = 1;
            //gex = 0;
            Initial();
            this.Image = Image;
            this.algorithm.Image = Image;
            this.algorithm = al;
            pixeldist = 1;
        }
        private void Initial()
        {
            //Dthv = null;
            Length1 = null;
            Length2 = null;
            Phi = null;
            centerRow = null;
            centerColumn = null;
            //centerRow = new HTuple();
            //Dthv = new HTuple();
            Length1 = new HTuple();
            Length2 = new HTuple();
            Phi = new HTuple();
            centerRow = new HTuple();
            centerColumn = new HTuple();

            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public override void draw()
        {
            //HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            //thv = thresholdValue.D;
            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            HOperatorSet.DrawRectangle2(this.LWindowHandle, out centerRow, out centerColumn,
    out Phi, out Length1, out Length2);
            this.DLength1 = Length1.D;
            this.DLength2 = Length2.D;
            this.DPhi = Phi.D;
            this.DcenterRow = centerRow.D;
            this.DcenterColumn = centerColumn.D;
        }
        private void action()
        {

            // Local iconic variables 

            HObject ho_Rectangle, ho_ImageReduced;
            HObject ho_ImageMean, ho_RegionDynThresh, ho_ConnectedRegions;
            HObject ho_SelectedRegions, ho_Rectangle1 = null, ho_RegionFillUp = null;

            // Local control variables 

            HTuple hv_Row0 = null, hv_Column0 = null, hv_Phi0 = null;
            HTuple hv_Length10 = null, hv_Length20 = null, hv_Number = null;
            HTuple hv_Rowa = new HTuple(), hv_Columna = new HTuple();
            HTuple hv_Phia = new HTuple(), hv_Length1a = new HTuple();
            HTuple hv_Length2a = new HTuple(), hv_Area = new HTuple();
            HTuple hv_Row1a = new HTuple(), hv_Column1a = new HTuple();
            // Initialize local and output iconic variables 
           HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_ImageMean);
            HOperatorSet.GenEmptyObj(out ho_RegionDynThresh);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            try
            {
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle, DcenterRow, DcenterColumn, DPhi, DLength1,
                    DLength2);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Rectangle, out ho_ImageReduced);
                ho_ImageMean.Dispose();
                HOperatorSet.MeanImage(ho_ImageReduced, out ho_ImageMean, 5, 5);
                ho_RegionDynThresh.Dispose();
                HOperatorSet.DynThreshold(ho_ImageReduced, ho_ImageMean, out ho_RegionDynThresh, 5,
                    "dark");
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_RegionDynThresh, out ho_ConnectedRegions);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                    "and", 1000, 100000);
                HOperatorSet.CountObj(ho_SelectedRegions, out hv_Number);
                if ((int)(new HTuple(hv_Number.TupleEqual(1))) != 0)
                {
                    HOperatorSet.SmallestRectangle2(ho_SelectedRegions, out hv_Rowa, out hv_Columna,
                        out hv_Phia, out hv_Length1a, out hv_Length2a);
                    ho_Rectangle1.Dispose();
                    HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle1, hv_Rowa, hv_Columna,
                        hv_Phia, hv_Length1a, hv_Length2a);
                    ho_RegionFillUp.Dispose();
                    HOperatorSet.FillUp(ho_SelectedRegions, out ho_RegionFillUp);
                    HOperatorSet.AreaCenter(ho_RegionFillUp, out hv_Area, out hv_Row1a, out hv_Column1a);

                }


                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("宽度");
                hv_result = hv_result.TupleConcat(hv_Length1a.D * pixeldist*2);
                hv_result = hv_result.TupleConcat("面积");
                hv_result = hv_result.TupleConcat(hv_Area);
                result = hv_result.Clone();

            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("宽度");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("面积");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();

            }
            finally
            {
                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_ImageMean.Dispose();
                ho_RegionDynThresh.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_Rectangle1.Dispose();
                ho_RegionFillUp.Dispose();
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