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
    class shizicaoheibai : ImageTools
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
        [field: NonSerializedAttribute()]
        HObject ho_RegionFillUp1 = null;
        #endregion
        public double hv_Row3 { set; get; }
        public double hv_Column3 { set; get; }
        public double hv_Phi { set; get; }
        public double thv { set; get; }
        public double hv_Length1 { set; get; }
        public double hv_Length2 { set; get; }
        public double hv1_Row { set; get; }
        public double hv1_Column { set; get; }
        public double hv1_Phi { set; get; }
        public double hv1_Length1 { set; get; }
        public double hv1_Length2 { set; get; }

        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public shizicaoheibai()
        {
            RegionToDisp = Image;
        }
        public shizicaoheibai(HObject Image, Algorithm al)
        {
            gex = 0;
            this.algorithm = al;

            this.Image = Image;
            RegionToDisp = Image;
            pixeldist = 1;
        }
        public override void draw()
        {
            HObject ho_PolygonRegion, ho_ImageReduced2;
            HTuple hv_Area = null, hv_Rowd = null, hv_Columnd = null;
            HOperatorSet.GenEmptyObj(out ho_PolygonRegion);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp1);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced2);
            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            ho_PolygonRegion.Dispose();
            HOperatorSet.DrawPolygon(out ho_PolygonRegion, this.LWindowHandle);
            ho_RegionFillUp1.Dispose();
            HOperatorSet.FillUp(ho_PolygonRegion, out ho_RegionFillUp1);
            ho_ImageReduced2.Dispose();
            HOperatorSet.ReduceDomain(Image, ho_RegionFillUp1, out ho_ImageReduced2);
            HOperatorSet.AreaCenter(ho_RegionFillUp1, out hv_Area, out hv_Rowd, out hv_Columnd);
            HOperatorSet.CreateScaledShapeModel(ho_ImageReduced2, "auto", -0.39, 1.57, "auto",
                0.9, 1.1, "auto", "auto", "use_polarity", "auto", "auto", out hv_ModelID);
            HOperatorSet.WriteShapeModel(hv_ModelID, PathHelper.currentProductPath + @"\shizicao.shm");
            HOperatorSet.WriteRegion(ho_RegionFillUp1, PathHelper.currentProductPath + @"\shiziquyu.hobj");
            //HTuple dRow = null, dColumn = null, dPhi = null, thresholdValue = null, dLength1 = null, dLength2 = null;
            //HObject ho_Rectangle, ho_ImageReduced, ho_Border;
            //HOperatorSet.GenEmptyObj(out ho_Rectangle);
            //HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            //HOperatorSet.GenEmptyObj(out ho_Border);
            //HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            //HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            //HOperatorSet.DrawRectangle2(this.LWindowHandle, out dRow, out dColumn,
            //    out dPhi, out dLength1, out dLength2);
            //HOperatorSet.DrawRectangle2(this.LWindowHandle, out ddRow, out ddColumn,
            //    out ddPhi, out ddLength1, out ddLength2);
            this.hv_Row3 = hv_Rowd.D;
            this.hv_Column3 = hv_Columnd.D;
            ho_PolygonRegion.Dispose();

            ho_ImageReduced2.Dispose();
            //this.hv_Phi = dPhi.D;
            //this.hv_Length1 = dLength1.D;
            //this.hv_Length2 = dLength2.D;
            //this.hv1_Row = ddRow.D;
            //this.hv1_Column = ddColumn.D;
            //this.hv1_Phi = ddPhi.D;
            //this.hv1_Length1 = ddLength1.D;
            //this.hv1_Length2 = ddLength2.D;
            //HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            //thv = thresholdValue.D;
            //ho_Rectangle.Dispose();
            //HOperatorSet.GenRectangle2(out ho_Rectangle, hv_Row, hv_Column, hv_Phi, hv_Length1,
            //    hv_Length2);
            //ho_ImageReduced.Dispose();
            //HOperatorSet.ReduceDomain(Image, ho_Rectangle, out ho_ImageReduced);
            //ho_Border.Dispose();
            //HOperatorSet.ThresholdSubPix(ho_ImageReduced, out ho_Border, thv);
            //HOperatorSet.CreateShapeModelXld(ho_Border, "auto", -3.14, 6.29, "auto", "auto", "ignore_local_polarity", 5, out hv_ModelID);
            //HOperatorSet.WriteShapeModel(hv_ModelID, PathHelper.currentProductPath + @"\lwspmd.shm");

            //ho_Rectangle.Dispose();
            //ho_ImageReduced.Dispose();
            //ho_Border.Dispose();
        }
        //DateTime t1, t2, t3, t4, t5, t6, t7; TimeSpan xx;
        private void action()
        {
            HObject ho_Region = null, ho_RegionErosion = null;
            HObject ho_RegionDilation = null, ho_ConnectedRegions = null;
            HObject ho_ObjectSelected = null, ho_RegionClosing = null, ho_RegionFillUp = null;
            HObject ho_RegionDifference = null, ho_ConnectedRegions1 = null;
            HObject ho_ObjectSelected1 = null, ho_ImageReduced = null, ho_Region1 = null;

            // Local control variables 
            HTuple hv_Area = new HTuple(), hv_Row = new HTuple(), hv_Column = new HTuple();
            HTuple hv_Indices = new HTuple(), hv_Row1 = new HTuple();
            HTuple hv_Column1 = new HTuple(), hv_Radius = new HTuple();
            HTuple hv_Area1 = new HTuple(), hv_Row2 = new HTuple();
            HTuple hv_Column2 = new HTuple(), hv_Indices1 = new HTuple();
            HTuple hv_Mean = new HTuple(), hv_Deviation = new HTuple();
            HTuple hv_Area2 = new HTuple(), hv_Row3 = new HTuple();
            HTuple hv_Column3 = new HTuple(), hv_Area3 = new HTuple();
            HTuple hv_Row4 = new HTuple(), hv_Column4 = new HTuple();
            HTuple hv_heise = new HTuple(), hv_bili = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_RegionErosion);
            HOperatorSet.GenEmptyObj(out ho_RegionDilation);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected1);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {
                ho_Region.Dispose();
                HOperatorSet.Threshold(Image, out ho_Region, 70, 250);
                ho_RegionErosion.Dispose();
                HOperatorSet.ErosionCircle(ho_Region, out ho_RegionErosion, 3.5);
                ho_RegionDilation.Dispose();
                HOperatorSet.DilationCircle(ho_RegionErosion, out ho_RegionDilation, 8.5);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_RegionDilation, out ho_ConnectedRegions);
                HOperatorSet.AreaCenter(ho_ConnectedRegions, out hv_Area, out hv_Row, out hv_Column);
                HOperatorSet.TupleFind(hv_Area, hv_Area.TupleMax(), out hv_Indices);
                ho_ObjectSelected.Dispose();
                HOperatorSet.SelectObj(ho_ConnectedRegions, out ho_ObjectSelected, hv_Indices + 1);
                ho_RegionClosing.Dispose();
                HOperatorSet.ClosingCircle(ho_ObjectSelected, out ho_RegionClosing, 13.5);
                ho_RegionFillUp.Dispose();
                HOperatorSet.FillUp(ho_RegionClosing, out ho_RegionFillUp);
                HOperatorSet.SmallestCircle(ho_RegionFillUp, out hv_Row1, out hv_Column1, out hv_Radius);
                ho_RegionDifference.Dispose();
                HOperatorSet.Difference(ho_RegionFillUp, ho_ObjectSelected, out ho_RegionDifference
                    );
                ho_ConnectedRegions1.Dispose();
                HOperatorSet.Connection(ho_RegionDifference, out ho_ConnectedRegions1);
                HOperatorSet.AreaCenter(ho_ConnectedRegions1, out hv_Area1, out hv_Row2, out hv_Column2);
                HOperatorSet.TupleFind(hv_Area1, hv_Area1.TupleMax(), out hv_Indices1);
                ho_ObjectSelected1.Dispose();
                HOperatorSet.SelectObj(ho_ConnectedRegions1, out ho_ObjectSelected1, hv_Indices1 + 1);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_ObjectSelected1, out ho_ImageReduced
                    );
                HOperatorSet.Intensity(ho_ObjectSelected1, ho_ImageReduced, out hv_Mean, out hv_Deviation);
                ho_Region1.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region1, hv_Mean, 255);
                HOperatorSet.AreaCenter(ho_Region1, out hv_Area2, out hv_Row3, out hv_Column3);
                HOperatorSet.AreaCenter(ho_RegionFillUp, out hv_Area3, out hv_Row4, out hv_Column4);
                hv_heise = hv_Area3 - hv_Area2;
                hv_bili = (hv_heise * 1.000) / (hv_Area2 * 1.000);
                HOperatorSet.Union1(ho_Region1, out RegionToDisp);
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("十字槽黑色");
                hv_result = hv_result.TupleConcat(hv_heise.D);
                hv_result = hv_result.TupleConcat("十字槽白色");
                hv_result = hv_result.TupleConcat(hv_Area2.D);
                hv_result = hv_result.TupleConcat("十字槽黑白比例");
                hv_result = hv_result.TupleConcat(hv_bili.D);
                hv_result = hv_result.TupleConcat("十字槽直径");
                hv_result = hv_result.TupleConcat(hv_Radius.D * pixeldist);
                result = hv_result.Clone();

                ho_Region.Dispose();
                ho_RegionErosion.Dispose();
                ho_RegionDilation.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_ObjectSelected.Dispose();
                ho_RegionClosing.Dispose();
                ho_RegionFillUp.Dispose();
                ho_RegionDifference.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_ObjectSelected1.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region1.Dispose();
                algorithm.Region.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("十字槽黑色");
                hv_result = hv_result.TupleConcat(-2);
                hv_result = hv_result.TupleConcat("十字槽白色");
                hv_result = hv_result.TupleConcat(-2);
                hv_result = hv_result.TupleConcat("十字槽黑白比例");
                hv_result = hv_result.TupleConcat(-2);
                hv_result = hv_result.TupleConcat("十字槽直径");
                hv_result = hv_result.TupleConcat(-2);
                result = hv_result.Clone();
                ho_Region.Dispose();
                ho_RegionErosion.Dispose();
                ho_RegionDilation.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_ObjectSelected.Dispose();
                ho_RegionClosing.Dispose();
                ho_RegionFillUp.Dispose();
                ho_RegionDifference.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_ObjectSelected1.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region1.Dispose();
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