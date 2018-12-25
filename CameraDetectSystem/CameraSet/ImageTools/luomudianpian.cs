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
    class luomudianpian : ImageTools
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
        public double hv_Radius { set; get; }
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public luomudianpian()
        {
            RegionToDisp = Image;
        }
        public luomudianpian(HObject Image, Algorithm al)
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
            this.hv_Radius = dPhi;
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
            HObject ho_Image1 = null, ho_Image2 = null;
            HObject ho_Image3 = null, ho_ImageResult = null, ho_ImageResult1 = null;
            HObject ho_Region = null, ho_ConnectedRegions = null, ho_ObjectSelected = null;
            HObject ho_RegionClosing = null, ho_RegionDifference = null;
            HObject ho_ConnectedRegions1 = null, ho_ObjectSelected1 = null;
            HObject ho_Contours = null, ho_RegionErosion1 = null, ho_RegionDilation1 = null;
            HObject ho_RegionErosion = null, ho_RegionIntersection = null;
            HObject ho_RegionDilation2 = null, ho_RegionDifference1 = null;
            HObject ho_ConnectedRegions2 = null, ho_SelectedRegions = null;
            HObject ho_RegionUnion = null, ho_RegionDilation = null, ho_ImageReduced = null;
            HObject ho_Region1 = null, ho_ConnectedRegions3 = null, ho_ObjectSelected2 = null;
            HObject ho_RegionFillUp = null, ho_Circle = null, ho_Circle1 = null;
            HObject ho_RegionDifference2 = null, ho_RegionUnion1 = null;
            HObject ho_RegionUnion2 = null;

            HTuple hv_Area = new HTuple(), hv_Row = new HTuple(), hv_Column = new HTuple();
            HTuple hv_Indices = new HTuple(), hv_Area1 = new HTuple();
            HTuple hv_Row2 = new HTuple(), hv_Column2 = new HTuple();
            HTuple hv_Indices1 = new HTuple(), hv_Row3 = new HTuple();
            HTuple hv_Column3 = new HTuple(), hv_Radius1 = new HTuple();
            HTuple hv_StartPhi = new HTuple(), hv_EndPhi = new HTuple();
            HTuple hv_PointOrder = new HTuple(), hv_Circularity = new HTuple();
            HTuple hv_Area2 = new HTuple(), hv_Row5 = new HTuple();
            HTuple hv_Column5 = new HTuple(), hv_Area3 = new HTuple();
            HTuple hv_Row6 = new HTuple(), hv_Column6 = new HTuple();
            HTuple hv_Indices2 = new HTuple(), hv_Row7 = new HTuple();
            HTuple hv_Column7 = new HTuple(), hv_Radius2 = new HTuple();
            HTuple hv_Row8 = new HTuple(), hv_Column8 = new HTuple();
            HTuple hv_Radius3 = new HTuple();

            HOperatorSet.GenEmptyObj(out ho_Image1);
            HOperatorSet.GenEmptyObj(out ho_Image2);
            HOperatorSet.GenEmptyObj(out ho_Image3);
            HOperatorSet.GenEmptyObj(out ho_ImageResult);
            HOperatorSet.GenEmptyObj(out ho_ImageResult1);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected1);
            HOperatorSet.GenEmptyObj(out ho_Contours);
            HOperatorSet.GenEmptyObj(out ho_RegionErosion1);
            HOperatorSet.GenEmptyObj(out ho_RegionDilation1);
            HOperatorSet.GenEmptyObj(out ho_RegionErosion);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection);
            HOperatorSet.GenEmptyObj(out ho_RegionDilation2);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion);
            HOperatorSet.GenEmptyObj(out ho_RegionDilation);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions3);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected2);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_Circle1);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference2);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion1);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion2);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            //t3 = DateTime.Now;
            try
            {
                ho_Image1.Dispose(); ho_Image2.Dispose(); ho_Image3.Dispose();
                HOperatorSet.Decompose3(Image, out ho_Image1, out ho_Image2, out ho_Image3
                    );
                ho_ImageResult.Dispose();
                HOperatorSet.DivImage(ho_Image1, ho_Image2, out ho_ImageResult, 100, 0);
                ho_ImageResult1.Dispose();
                HOperatorSet.DivImage(ho_Image1, ho_Image3, out ho_ImageResult1, 100, 0);
                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_ImageResult, out ho_Region, 101, 255);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
                HOperatorSet.AreaCenter(ho_ConnectedRegions, out hv_Area, out hv_Row, out hv_Column);
                HOperatorSet.TupleFind(hv_Area, hv_Area.TupleMax(), out hv_Indices);
                ho_ObjectSelected.Dispose();
                HOperatorSet.SelectObj(ho_ConnectedRegions, out ho_ObjectSelected, hv_Indices + 1);
                ho_RegionClosing.Dispose();
                HOperatorSet.ClosingCircle(ho_ObjectSelected, out ho_RegionClosing, 700);
                ho_RegionDifference.Dispose();
                HOperatorSet.Difference(ho_RegionClosing, ho_ObjectSelected, out ho_RegionDifference
                    );
                ho_ConnectedRegions1.Dispose();
                HOperatorSet.Connection(ho_RegionDifference, out ho_ConnectedRegions1);
                HOperatorSet.AreaCenter(ho_ConnectedRegions1, out hv_Area1, out hv_Row2, out hv_Column2);
                HOperatorSet.TupleFind(hv_Area1, hv_Area1.TupleMax(), out hv_Indices1);
                ho_ObjectSelected1.Dispose();
                HOperatorSet.SelectObj(ho_ConnectedRegions1, out ho_ObjectSelected1, hv_Indices1 + 1);
                ho_Contours.Dispose();
                HOperatorSet.GenContourRegionXld(ho_ObjectSelected1, out ho_Contours, "border");
                HOperatorSet.FitCircleContourXld(ho_Contours, "algebraic", -1, 0, 0, 3, 2,
                    out hv_Row3, out hv_Column3, out hv_Radius1, out hv_StartPhi, out hv_EndPhi,
                    out hv_PointOrder);
                ho_RegionErosion1.Dispose();
                HOperatorSet.ErosionCircle(ho_ObjectSelected1, out ho_RegionErosion1, 10.5);
                ho_RegionDilation1.Dispose();
                HOperatorSet.DilationCircle(ho_RegionErosion1, out ho_RegionDilation1, 10.5);
                HOperatorSet.Circularity(ho_RegionDilation1, out hv_Circularity);
                ho_RegionErosion.Dispose();
                HOperatorSet.ErosionCircle(ho_RegionClosing, out ho_RegionErosion, 7.5);
                ho_RegionIntersection.Dispose();
                HOperatorSet.Intersection(ho_RegionDifference, ho_RegionErosion, out ho_RegionIntersection
                    );
                ho_RegionDilation2.Dispose();
                HOperatorSet.DilationCircle(ho_RegionDilation1, out ho_RegionDilation2, 5.5);
                ho_RegionDifference1.Dispose();
                HOperatorSet.Difference(ho_RegionIntersection, ho_RegionDilation2, out ho_RegionDifference1
                    );
                ho_ConnectedRegions2.Dispose();
                HOperatorSet.Connection(ho_RegionDifference1, out ho_ConnectedRegions2);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions2, out ho_SelectedRegions, "area",
                    "and", 125, 99999000);
                ho_RegionUnion.Dispose();
                HOperatorSet.Union1(ho_SelectedRegions, out ho_RegionUnion);
                HOperatorSet.AreaCenter(ho_RegionUnion, out hv_Area2, out hv_Row5, out hv_Column5);
                ho_RegionDilation.Dispose();
                HOperatorSet.DilationCircle(ho_RegionClosing, out ho_RegionDilation, 170);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(ho_Image2, ho_RegionDilation, out ho_ImageReduced
                    );
                ho_Region1.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region1, 170, 255);
                ho_ConnectedRegions3.Dispose();
                HOperatorSet.Connection(ho_Region1, out ho_ConnectedRegions3);
                HOperatorSet.AreaCenter(ho_ConnectedRegions3, out hv_Area3, out hv_Row6, out hv_Column6);
                HOperatorSet.TupleFind(hv_Area3, hv_Area3.TupleMax(), out hv_Indices2);
                ho_ObjectSelected2.Dispose();
                HOperatorSet.SelectObj(ho_ConnectedRegions3, out ho_ObjectSelected2, hv_Indices2 + 1);
                ho_RegionFillUp.Dispose();
                HOperatorSet.FillUp(ho_ObjectSelected2, out ho_RegionFillUp);
                HOperatorSet.SmallestCircle(ho_RegionFillUp, out hv_Row7, out hv_Column7, out hv_Radius2);
                ho_Circle.Dispose();
                HOperatorSet.GenCircle(out ho_Circle, hv_Row7, hv_Column7, hv_Radius2);
                HOperatorSet.InnerCircle(ho_RegionFillUp, out hv_Row8, out hv_Column8, out hv_Radius3);
                ho_Circle1.Dispose();
                HOperatorSet.GenCircle(out ho_Circle1, hv_Row8, hv_Column8, hv_Radius3);
                ho_RegionDifference2.Dispose();
                HOperatorSet.Difference(ho_Circle, ho_Circle1, out ho_RegionDifference2);
                ho_RegionUnion1.Dispose();
                HOperatorSet.Union2(ho_RegionDifference2, ho_RegionUnion, out ho_RegionUnion1
                    );
                ho_RegionUnion2.Dispose();
                HOperatorSet.Union2(ho_RegionUnion1, ho_RegionDilation1, out RegionToDisp
                    );
                if (hv_Area2.TupleLength() == 0)
                {
                    hv_Area2 = 0;
                }
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("垫片内孔圆度");
                hv_result = hv_result.TupleConcat(hv_Circularity.D);
                hv_result = hv_result.TupleConcat("垫片内孔直径");
                hv_result = hv_result.TupleConcat(hv_Radius1.D*2*pixeldist);
                hv_result = hv_result.TupleConcat("垫片灰尘");
                hv_result = hv_result.TupleConcat(hv_Area2.D);
                hv_result = hv_result.TupleConcat("不圆");
                hv_result = hv_result.TupleConcat((hv_Radius2.D - hv_Radius3.D)*pixeldist);
                result = hv_result.Clone();

                //t4 = DateTime.Now;
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("垫片内孔圆度");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("垫片内孔直径");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("垫片灰尘");
                hv_result = hv_result.TupleConcat(999999);
                hv_result = hv_result.TupleConcat("不圆");
                hv_result = hv_result.TupleConcat(1000* pixeldist);
                result = hv_result.Clone();

                

            }
            finally
            {
                ho_Image1.Dispose();
                ho_Image2.Dispose();
                ho_Image3.Dispose();
                ho_ImageResult.Dispose();
                ho_ImageResult1.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_ObjectSelected.Dispose();
                ho_RegionClosing.Dispose();
                ho_RegionDifference.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_ObjectSelected1.Dispose();
                ho_Contours.Dispose();
                ho_RegionErosion1.Dispose();
                ho_RegionDilation1.Dispose();
                ho_RegionErosion.Dispose();
                ho_RegionIntersection.Dispose();
                ho_RegionDilation2.Dispose();
                ho_RegionDifference1.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionUnion.Dispose();
                ho_RegionDilation.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region1.Dispose();
                ho_ConnectedRegions3.Dispose();
                ho_ObjectSelected2.Dispose();
                ho_RegionFillUp.Dispose();
                ho_Circle.Dispose();
                ho_Circle1.Dispose();
                ho_RegionDifference2.Dispose();
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