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
    class zigongluosishizicao : ImageTools
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
        public zigongluosishizicao()
        {
            RegionToDisp = Image;
        }
        public zigongluosishizicao(HObject Image, Algorithm al)
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
            HObject ho_Rectangle, ho_Region;
            HObject ho_ConnectedRegions, ho_ObjectSelected, ho_RegionClosing;
            HObject ho_RegionFillUp, ho_RegionDifference, ho_RegionDilation1;
            HObject ho_RegionErosion, ho_ImageReduced, ho_Region1, ho_RegionClosing2;
            HObject ho_ConnectedRegions1, ho_ObjectSelected1, ho_RegionFillUp1;
            HObject ho_RegionDilation, ho_ImageReduced1, ho_Region2;

            // Local control variables 

            HTuple hv_Width = null, hv_Height = null, hv_Mean1 = null;
            HTuple hv_Deviation1 = null, hv_Area = null, hv_Row = null;
            HTuple hv_Column = null, hv_Indices = null, hv_Area1 = null;
            HTuple hv_Row1 = null, hv_Column1 = null, hv_Indices1 = null;
            HTuple hv_Mean = null, hv_Deviation = null, hv_Area2 = null;
            HTuple hv_Row2 = null, hv_Column2 = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_RegionDilation1);
            HOperatorSet.GenEmptyObj(out ho_RegionErosion);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing2);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected1);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp1);
            HOperatorSet.GenEmptyObj(out ho_RegionDilation);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
            HOperatorSet.GenEmptyObj(out ho_Region2);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            //t3 = DateTime.Now;                
            //t4 = DateTime.Now;
            //xx = t4 - t1;
            //if (xx.Seconds * 1000 + xx.Milliseconds > 100)
            //{
            //    t2 = DateTime.Now;
            //}
            try
            {
                HOperatorSet.GetImageSize(Image, out hv_Width, out hv_Height);
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle, 0, 0, hv_Height, hv_Width);
                HOperatorSet.Intensity(ho_Rectangle, Image, out hv_Mean1, out hv_Deviation1);
                ho_Region.Dispose();
                HOperatorSet.Threshold(Image, out ho_Region, 0, hv_Mean1);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
                HOperatorSet.AreaCenter(ho_ConnectedRegions, out hv_Area, out hv_Row, out hv_Column);
                HOperatorSet.TupleFind(hv_Area, hv_Area.TupleMax(), out hv_Indices);
                ho_ObjectSelected.Dispose();
                HOperatorSet.SelectObj(ho_ConnectedRegions, out ho_ObjectSelected, hv_Indices + 1);
                ho_RegionClosing.Dispose();
                HOperatorSet.ClosingCircle(ho_ObjectSelected, out ho_RegionClosing, 3.5);
                ho_RegionFillUp.Dispose();
                HOperatorSet.FillUp(ho_RegionClosing, out ho_RegionFillUp);
                ho_RegionDifference.Dispose();
                HOperatorSet.Difference(ho_RegionFillUp, ho_RegionClosing, out ho_RegionDifference
                    );
                ho_RegionDilation1.Dispose();
                HOperatorSet.DilationCircle(ho_RegionDifference, out ho_RegionDilation1, 53.5);
                ho_RegionErosion.Dispose();
                HOperatorSet.ErosionCircle(ho_RegionDilation1, out ho_RegionErosion, 70.5);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_RegionErosion, out ho_ImageReduced
                    );
                ho_Region1.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region1, 0, 240);
                ho_ConnectedRegions1.Dispose();
                HOperatorSet.Connection(ho_Region1, out ho_ConnectedRegions1);
                HOperatorSet.AreaCenter(ho_ConnectedRegions1, out hv_Area1, out hv_Row1, out hv_Column1);
                HOperatorSet.TupleFind(hv_Area1, hv_Area1.TupleMax(), out hv_Indices1);
                ho_ObjectSelected1.Dispose();
                HOperatorSet.SelectObj(ho_ConnectedRegions1, out ho_ObjectSelected1, hv_Indices1 + 1);
                ho_RegionFillUp1.Dispose();
                HOperatorSet.FillUp(ho_ObjectSelected1, out ho_RegionFillUp1);
                ho_RegionDilation.Dispose();
                HOperatorSet.ErosionCircle(ho_RegionFillUp1, out ho_RegionDilation, 3.5);
                ho_ImageReduced1.Dispose();
                HOperatorSet.ReduceDomain(ho_ImageReduced, ho_RegionDilation, out ho_ImageReduced1
                    );
                HOperatorSet.Intensity(ho_RegionDilation, ho_ImageReduced1, out hv_Mean, out hv_Deviation);
                ho_Region2.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced1, out ho_Region2, hv_Mean + 70, 255);
                HOperatorSet.AreaCenter(ho_Region2, out hv_Area2, out hv_Row2, out hv_Column2);
                HOperatorSet.Union1(ho_Region2, out RegionToDisp);
                //HOperatorSet.Union1(ho_Region, out RegionToDisp);
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("十字槽堵槽");
                hv_result = hv_result.TupleConcat(hv_Area2.D);
                result = hv_result.Clone();

                ho_Rectangle.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_ObjectSelected.Dispose();
                ho_RegionClosing.Dispose();
                ho_RegionFillUp.Dispose();
                ho_RegionDifference.Dispose();
                ho_RegionDilation1.Dispose();
                ho_RegionErosion.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region1.Dispose();
                ho_RegionClosing2.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_ObjectSelected1.Dispose();
                ho_RegionFillUp1.Dispose();
                ho_RegionDilation.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region2.Dispose();
                algorithm.Region.Dispose();
                //t4 = DateTime.Now;
                //xx = t4 - t1;
                //if (xx.Seconds * 1000 + xx.Milliseconds > 100)
                //{
                //    t2 = DateTime.Now;
                //}
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("十字槽堵槽");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();
                ho_Rectangle.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_ObjectSelected.Dispose();
                ho_RegionClosing.Dispose();
                ho_RegionFillUp.Dispose();
                ho_RegionDifference.Dispose();
                ho_RegionDilation1.Dispose();
                ho_RegionErosion.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region1.Dispose();
                ho_RegionClosing2.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_ObjectSelected1.Dispose();
                ho_RegionFillUp1.Dispose();
                ho_RegionDilation.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region2.Dispose();
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