using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
    [Serializable]
    class liujiaoceliang : ImageTools
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

        public double zxr { set; get; }
        public double zxc { set; get; }
        public double ra { set; get; }
        public double thv { set; get; }
        public double mjsx { set; get; }
        public double mjxx { set; get; }
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public liujiaoceliang()
        {
            RegionToDisp = Image;
        }
        public liujiaoceliang(HObject Image, Algorithm al)
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
            HTuple dcenterRow = null, dcenterColumn = null, dra=null;
            HObject ho_circle;
            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            HOperatorSet.GenEmptyObj(out ho_circle);
            HOperatorSet.DrawCircle(this.LWindowHandle, out dcenterRow, out dcenterColumn, out dra);
            HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            thv = thresholdValue.D;
            HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\mianjixx", out mianjixx);
            mjxx = mianjixx.D;
            HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\mianjisx", out mianjisx);
            mjsx = mianjisx.D;
            this.zxr = dcenterRow.D;
            this.zxc = dcenterColumn.D;
            this.ra = dra.D;
        }
        private void action()
        {
            // Local iconic variables 

            HObject ho_Circle, ho_ImageReduced;
            HObject ho_Border, ho_UnionContours, ho_SelectedXLD, ho_Region;

            // Local control variables 

            HTuple hv_bianc = null, hv_duibianc = null;
            HObject ho_Regions, ho_ObjectSelected;
            HObject ho_ConnectedRegions, ho_SelectedRegions1, ho_RegionClosing;
            HObject ho_RegionFillUp;


            // Local control variables 

            HTuple hv_Area, hv_Row, hv_Column;


            HOperatorSet.GenEmptyObj(out ho_Regions);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            try
            {
                ho_Circle.Dispose();
                HOperatorSet.GenCircle(out ho_Circle, zxr, zxc, ra);

                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Circle, out ho_ImageReduced);

                ho_Regions.Dispose();
                HOperatorSet.AutoThreshold(ho_ImageReduced, out ho_Regions, 0.3);
                ho_ObjectSelected.Dispose();
                HOperatorSet.SelectObj(ho_Regions, out ho_ObjectSelected, 1);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_ObjectSelected, out ho_ConnectedRegions);
                ho_SelectedRegions1.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions1, "outer_radius",
                    "and", 200, 300);
                ho_RegionClosing.Dispose();
                HOperatorSet.ClosingCircle(ho_SelectedRegions1, out ho_RegionClosing, 9.5);
                ho_RegionFillUp.Dispose();
                HOperatorSet.FillUp(ho_RegionClosing, out ho_RegionFillUp);
                HOperatorSet.AreaCenter(ho_RegionFillUp, out hv_Area, out hv_Row, out hv_Column);
                hv_bianc = ((((hv_Area * 2) / 3) / ((new HTuple(3)).TupleSqrt()))).TupleSqrt();
                hv_duibianc = hv_bianc * ((new HTuple(3)).TupleSqrt());
                HOperatorSet.Union1(ho_RegionFillUp, out RegionToDisp);


                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("六角边长");
                hv_result = hv_result.TupleConcat(hv_bianc.D * pixeldist);
                hv_result = hv_result.TupleConcat("六角对边长");
                hv_result = hv_result.TupleConcat(hv_duibianc.D * pixeldist);
                result = hv_result.Clone();


                ho_Circle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Regions.Dispose();
                ho_ObjectSelected.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionClosing.Dispose();
                ho_RegionFillUp.Dispose();

            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("六角边长");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("六角对边长");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();


                ho_Circle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Regions.Dispose();
                ho_ObjectSelected.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionClosing.Dispose();
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