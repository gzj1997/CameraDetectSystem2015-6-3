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
    class xiangpihong1 : ImageTools
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
        public double hv_Radius { set; get; }
        public double mjsx { set; get; }
        public double mjxx { set; get; }
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public xiangpihong1()
        {
            RegionToDisp = Image;
        }
        public xiangpihong1(HObject Image, Algorithm al)
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
            HObject ho_Image1 = null, ho_Image2 = null, ho_ImageMean=null,ho_d=null;
            HObject ho_Image3 = null, ho_ImageResult = null, ho_ImageResult1 = null;
            HObject ho_Region = null, ho_ConnectedRegions = null, ho_SelectedRegions = null;
            HObject ho_RegionFillUp = null, ho_RegionDifference = null;
            HObject ho_ConnectedRegions1 = null, ho_SelectedRegions1 = null;
            HObject ho_RegionDifference1 = null, ho_ConnectedRegions2 = null;
            HObject ho_SelectedRegions2 = null, ho_RegionDilation = null;
            HObject ho_ImageReduced = null, ho_Region1 = null, ho_ConnectedRegions3 = null;
            HObject ho_SelectedRegions3 = null, ho_RegionFillUp1 = null;
            HObject ho_Circle = null, ho_Circle1 = null, ho_RegionDifference2 = null;
            HObject ho_RegionUnion = null;

            HTuple hv_Area = new HTuple(), hv_Row = new HTuple(), hv_Column = new HTuple(),rx=new HTuple(),rc=new HTuple(),re=new HTuple();
            HTuple hv_Area1 = new HTuple(), hv_Row1 = new HTuple(),hmp=new HTuple(),dmp=new HTuple();
            HTuple hv_Column1 = new HTuple(), hv_Row2 = new HTuple();
            HTuple hv_Column2 = new HTuple(), hv_Radius = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image1);
            HOperatorSet.GenEmptyObj(out ho_Image2);
            HOperatorSet.GenEmptyObj(out ho_Image3);
            HOperatorSet.GenEmptyObj(out ho_ImageResult);
            HOperatorSet.GenEmptyObj(out ho_ImageResult1);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_RegionDilation);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions3);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions3);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp1);
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_Circle1);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference2);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion);
            HOperatorSet.GenEmptyObj(out ho_ImageMean);
            HOperatorSet.GenEmptyObj(out ho_d);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            //t3 = DateTime.Now;
            try
            {
                ho_Image1.Dispose(); ho_Image2.Dispose(); ho_Image3.Dispose();
                HOperatorSet.Decompose3(Image, out ho_Image1, out ho_Image2, out ho_Image3
                    );
                ho_ImageResult.Dispose();
                HOperatorSet.AddImage(ho_Image2, ho_Image3, out ho_ImageResult, 0.5, 0);
                ho_ImageResult1.Dispose();
                HOperatorSet.DivImage(ho_Image1, ho_ImageResult, out ho_ImageResult1, 128,
                    0);
                ho_ImageMean.Dispose();
                HOperatorSet.MeanImage(ho_ImageResult1, out ho_ImageMean, 500, 500);
                //ho_RegionDynThresh.Dispose();
                ho_Region.Dispose();
                HOperatorSet.DynThreshold(ho_ImageResult1, ho_ImageMean, out ho_Region,
                    3, "light");
                //ho_Region.Dispose();
                //HOperatorSet.Threshold(ho_ImageResult1, out ho_Region, 130, 255);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShapeStd(ho_ConnectedRegions, out ho_SelectedRegions, "max_area",
                    70);
                ho_RegionFillUp.Dispose();
                HOperatorSet.FillUp(ho_SelectedRegions, out ho_RegionFillUp);
                ho_RegionDifference.Dispose();
                HOperatorSet.Difference(ho_RegionFillUp, ho_SelectedRegions, out ho_RegionDifference
                    );
                ho_ConnectedRegions1.Dispose();
                HOperatorSet.Connection(ho_RegionDifference, out ho_ConnectedRegions1);
                ho_SelectedRegions1.Dispose();
                HOperatorSet.SelectShapeStd(ho_ConnectedRegions1, out ho_SelectedRegions1,
                    "max_area", 70);
                HOperatorSet.AreaCenter(ho_SelectedRegions1, out hv_Area, out hv_Row, out hv_Column);
                HOperatorSet.SmallestCircle(ho_SelectedRegions1,out rx,out rc,out re);
                HOperatorSet.Intensity(ho_SelectedRegions1,Image,out hmp,out dmp);
                ho_RegionDifference1.Dispose();
                HOperatorSet.Difference(ho_RegionDifference, ho_SelectedRegions1, out ho_RegionDifference1
                    );
                ho_ConnectedRegions2.Dispose();
                HOperatorSet.Connection(ho_RegionDifference1, out ho_ConnectedRegions2);
                ho_SelectedRegions2.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions2, out ho_SelectedRegions2, "area",
                    "and", 1500, 9999900);
                HOperatorSet.AreaCenter(ho_SelectedRegions2, out hv_Area1, out hv_Row1, out hv_Column1);
                ho_RegionDilation.Dispose();
                HOperatorSet.DilationCircle(ho_RegionFillUp, out ho_RegionDilation, 200);
                ho_d.Dispose();
                HOperatorSet.Difference(ho_RegionDilation, ho_RegionFillUp,out ho_d);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_d, out ho_ImageReduced);
                
                
                ho_Region1.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region1, thv, 255);
                ho_ConnectedRegions3.Dispose();
                HOperatorSet.Connection(ho_Region1, out ho_ConnectedRegions3);
                ho_SelectedRegions3.Dispose();
                HOperatorSet.SelectShapeStd(ho_ConnectedRegions3, out ho_SelectedRegions3,
                    "max_area", 70);
                ho_RegionFillUp1.Dispose();
                HOperatorSet.FillUp(ho_SelectedRegions3, out ho_RegionFillUp1);
                HOperatorSet.InnerCircle(ho_RegionFillUp1, out hv_Row2, out hv_Column2, out hv_Radius);
                if ((int)(new HTuple((new HTuple(hv_Row2.TupleLength())).TupleEqual(1))) != 0)
                {
                    ho_Circle.Dispose();
                    HOperatorSet.GenCircle(out ho_Circle, hv_Row2, hv_Column2, hv_Radius);
                    ho_Circle1.Dispose();
                    HOperatorSet.GenCircle(out ho_Circle1, hv_Row2, hv_Column2, hv_Radius - 2);
                    ho_RegionDifference2.Dispose();
                    HOperatorSet.Difference(ho_Circle, ho_Circle1, out ho_RegionDifference2);
                }
                ho_RegionUnion.Dispose();
                HOperatorSet.Union2(ho_RegionDifference2, ho_SelectedRegions2, out ho_RegionUnion
                    );
                HOperatorSet.Union2(ho_RegionUnion, ho_SelectedRegions1, out RegionToDisp
                    );
                if (hv_Area1.TupleLength() == 0)
                {
                    hv_Area1 = 0;
                }

                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("垫片内孔面积");
                hv_result = hv_result.TupleConcat(hv_Area.D);
                hv_result = hv_result.TupleConcat("内圆直径");
                hv_result = hv_result.TupleConcat(hv_Radius.D * 2 * pixeldist);
                hv_result = hv_result.TupleConcat("垫片灰尘");
                hv_result = hv_result.TupleConcat(hv_Area1.D);
                hv_result = hv_result.TupleConcat("内孔灰度");
                hv_result = hv_result.TupleConcat(hmp.D);
                hv_result = hv_result.TupleConcat("内孔直径");
                hv_result = hv_result.TupleConcat(re.D*pixeldist*2);
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
                hv_result = hv_result.TupleConcat("内孔灰度");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("内孔直径");
                hv_result = hv_result.TupleConcat(0);
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
                ho_SelectedRegions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_RegionDifference.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionDifference1.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_RegionDilation.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region1.Dispose();
                ho_ConnectedRegions3.Dispose();
                ho_SelectedRegions3.Dispose();
                ho_RegionFillUp1.Dispose();
                ho_Circle.Dispose();
                ho_Circle1.Dispose();
                ho_RegionDifference2.Dispose();
                ho_RegionUnion.Dispose();
                ho_ImageMean.Dispose();
                ho_d.Dispose();
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