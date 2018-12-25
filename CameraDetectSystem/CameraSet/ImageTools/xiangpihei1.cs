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
    class xiangpihei1 : ImageTools
    {
        #region ROI
        [NonSerialized]
        private HTuple dPhi = new HTuple();
        [NonSerialized]
        private HTuple mianjisx = new HTuple();
        [NonSerialized]
        private HTuple mianjixx = new HTuple();
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
        public double hv_Row { set; get; }
        public double hv_Column { set; get; }
        public double hv_Phi { set; get; }
        public double thv { set; get; }
        public double hv_Length1 { set; get; }
        public double hv_Length2 { set; get; }
        public double hv1_Row { set; get; }
        public double hv1_Column { set; get; }
        public double hv1_Phi { set; get; }
        public double hv1_Length1 { set; get; }
        public double hv1_Length2 { set; get; }
        public double hv_Radiust { set; get; }
        public double mjsx { set; get; }
        public double mjxx { set; get; }
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public xiangpihei1()
        {
            RegionToDisp = Image;
        }
        public xiangpihei1(HObject Image, Algorithm al)
        {
            gex = 0;
            this.algorithm = al;

            this.Image = Image;
            RegionToDisp = Image;
            pixeldist = 1;
        }
        public override void draw()
        {
            HObject ho_Circle, ho_ImageReduced;
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            HOperatorSet.DrawCircle(this.LWindowHandle, out dRow, out dColumn, out dPhi);
            this.hv_Radiust = dPhi;
            HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            thv = thresholdValue.D;
            HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\mianjixx", out mianjixx);
            mjxx = mianjixx.D;
            HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\mianjisx", out mianjisx);
            mjsx = mianjisx.D;
            //ho_Circle.Dispose();
            //HOperatorSet.GenCircle(out ho_Circle, dRow, dColumn, dPhi);
            //ho_ImageReduced.Dispose();
            //HOperatorSet.ReduceDomain(Image, ho_Circle, out ho_ImageReduced);
            //HOperatorSet.CreateNccModel(ho_ImageReduced, 0, -3.14, 6.29, 0.0175, "use_polarity",
            //    out hv_ModelID);
            //HOperatorSet.WriteNccModel(hv_ModelID, PathHelper.currentProductPath + @"\zifu.ncm");
            ho_Circle.Dispose();
            ho_ImageReduced.Dispose();
        }
        //DateTime t1, t2, t3, t4,t5,t6,t7;
        private void action()
        {
            HObject ho_Region = null, ho_ConnectedRegions = null;
            HObject ho_SelectedRegions = null, ho_SelectedRegions1 = null;
            HObject ho_RegionFillUp = null, ho_RegionDifference = null;
            HObject ho_ConnectedRegions1 = null, ho_SelectedRegions2 = null;
            HObject ho_RegionDilation = null, ho_ImageReduced = null, ho_Region1 = null;
            HObject ho_RegionFillUp1 = null, ho_ConnectedRegions2 = null;
            HObject ho_SelectedRegions3 = null, ho_RegionDifference1 = null,ho_Circle2=null,ho_RegionIntersection=null;
            HObject ho_Circle = null, ho_Circle1 = null, ho_RegionDifference2 = null;
            HObject ho_ConnectedRegions3 = null, ho_SelectedRegions4 = null;
            HObject ho_RegionUnion = null, ho_RegionUnion1 = null, ho_RegionUnion2 = null;

            // Local control variables 

            HTuple hv_Area = new HTuple(), hv_Row = new HTuple(), hv_Column = new HTuple();
            HTuple hv_Row1 = new HTuple(), hv_Column1 = new HTuple();
            HTuple hv_Radius = new HTuple(), hv_Area1 = new HTuple();
            HTuple hv_Row2 = new HTuple(), hv_Column2 = new HTuple();

            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_RegionDilation);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions3);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference1);
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_Circle1);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference2);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions3);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions4);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion1);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion2);
            HOperatorSet.GenEmptyObj(out ho_Circle2);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            //t3 = DateTime.Now;
            try
            {
                ho_Region.Dispose();
                HOperatorSet.Threshold(Image, out ho_Region, 0, thv);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area_holes",
                    "and", 30000, 200000);
                ho_SelectedRegions1.Dispose();
                HOperatorSet.SelectShapeStd(ho_SelectedRegions, out ho_SelectedRegions1, "max_area",
                    70);
                ho_RegionFillUp.Dispose();
                HOperatorSet.FillUp(ho_SelectedRegions1, out ho_RegionFillUp);
                ho_RegionDifference.Dispose();
                HOperatorSet.Difference(ho_RegionFillUp, ho_SelectedRegions1, out ho_RegionDifference
                    );
                ho_ConnectedRegions1.Dispose();
                HOperatorSet.Connection(ho_RegionDifference, out ho_ConnectedRegions1);
                ho_SelectedRegions2.Dispose();
                HOperatorSet.SelectShapeStd(ho_ConnectedRegions1, out ho_SelectedRegions2,
                    "max_area", 70);
                HOperatorSet.AreaCenter(ho_SelectedRegions2, out hv_Area, out hv_Row, out hv_Column);
                if ((int)(new HTuple((new HTuple(hv_Area.TupleLength())).TupleEqual(0))) != 0)
                {
                    hv_Area = 0;
                }
                ho_RegionDilation.Dispose();
                HOperatorSet.DilationCircle(ho_RegionFillUp, out ho_RegionDilation, 200);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_RegionDilation, out ho_ImageReduced);
                ho_Region1.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region1, thv+30, 255);
                ho_RegionFillUp1.Dispose();
                HOperatorSet.FillUp(ho_Region1, out ho_RegionFillUp1);
                ho_ConnectedRegions2.Dispose();
                HOperatorSet.Connection(ho_RegionFillUp1, out ho_ConnectedRegions2);
                ho_SelectedRegions3.Dispose();
                HOperatorSet.SelectShapeStd(ho_ConnectedRegions2, out ho_SelectedRegions3,
                    "max_area", 70);
                HOperatorSet.InnerCircle(ho_SelectedRegions3, out hv_Row1, out hv_Column1,
                    out hv_Radius);
                ho_RegionDifference1.Dispose();
                HOperatorSet.Difference(ho_RegionDifference, ho_SelectedRegions2, out ho_RegionDifference1
                    );
                if ((int)(new HTuple((new HTuple(hv_Row1.TupleLength())).TupleEqual(1))) != 0)
                {
                    ho_Circle.Dispose();
                    HOperatorSet.GenCircle(out ho_Circle, hv_Row1, hv_Column1, hv_Radius);
                    ho_Circle1.Dispose();
                    HOperatorSet.GenCircle(out ho_Circle1, hv_Row1, hv_Column1, hv_Radius - 2);
                    ho_RegionDifference2.Dispose();
                    HOperatorSet.Difference(ho_Circle, ho_Circle1, out ho_RegionDifference2);
                }
                ho_ConnectedRegions3.Dispose();
                HOperatorSet.Connection(ho_RegionDifference1, out ho_ConnectedRegions3);
                ho_SelectedRegions4.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions3, out ho_SelectedRegions4, "area",
                    "and", 1500, 9999900);
                ho_RegionUnion.Dispose();
                HOperatorSet.Union1(ho_SelectedRegions4, out ho_RegionUnion);
                ho_Circle2.Dispose();
                HOperatorSet.GenCircle(out ho_Circle2, hv_Row, hv_Column, hv_Radiust);
                 ho_RegionIntersection.Dispose();
      HOperatorSet.Intersection(ho_Circle2, ho_RegionUnion, out ho_RegionIntersection
          );
                HOperatorSet.AreaCenter(ho_RegionIntersection, out hv_Area1, out hv_Row2, out hv_Column2);
                if ((int)(new HTuple((new HTuple(hv_Area1.TupleLength())).TupleEqual(0))) != 0)
                {
                    hv_Area1 = 0;
                }
                ho_RegionUnion1.Dispose();
                HOperatorSet.Union2(ho_RegionIntersection, ho_SelectedRegions2, out ho_RegionUnion1
                    );
                ho_RegionUnion2.Dispose();
                HOperatorSet.Union2(ho_RegionUnion1, ho_RegionDifference2, out RegionToDisp
                    );


                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("垫片内孔面积");
                hv_result = hv_result.TupleConcat(hv_Area.D);
                hv_result = hv_result.TupleConcat("内圆直径");
                hv_result = hv_result.TupleConcat(hv_Radius.D * 2 * pixeldist);
                hv_result = hv_result.TupleConcat("垫片灰尘");
                hv_result = hv_result.TupleConcat(hv_Area1.D);
                result = hv_result.Clone();

                //t4 = DateTime.Now;
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("垫片内孔面积");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("内圆直径");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("垫片灰尘");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();



            }
            finally
            {
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionFillUp.Dispose();
                ho_RegionDifference.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_RegionDilation.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region1.Dispose();
                ho_RegionFillUp1.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_SelectedRegions3.Dispose();
                ho_RegionDifference1.Dispose();
                ho_Circle.Dispose();
                ho_Circle1.Dispose();
                ho_RegionDifference2.Dispose();
                ho_ConnectedRegions3.Dispose();
                ho_SelectedRegions4.Dispose();
                ho_RegionUnion.Dispose();
                ho_RegionUnion1.Dispose();
                ho_RegionUnion2.Dispose();
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