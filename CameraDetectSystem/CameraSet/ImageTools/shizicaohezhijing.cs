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
    class shizicaohezhijing : ImageTools
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
        public shizicaohezhijing()
        {
            RegionToDisp = Image;
        }
        public shizicaohezhijing(HObject Image, Algorithm al)
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
            //t1 = DateTime.Now;
            HObject ho_RegionAffineTrans = null;
            HObject ho_ImageReduced = null, ho_ImaAmp = null, ho_ImaDir = null;
            HObject ho_Region = null, ho_Circle = null, ho_ImageReduced1 = null;
            HObject ho_Region1 = null, ho_ConnectedRegions = null, ho_SelectedRegions = null;
            HObject ho_RegionUnion = null, ho_RegionClosing = null, ho_RegionFillUp = null;
            HObject ho_Contours = null, ho_ContCircle = null, ho_Region2 = null;

            HTuple hv_HomMat2DIdentity = new HTuple();
            HTuple hv_Row2 = new HTuple(), hv_Column2 = new HTuple();
            HTuple hv_Angle1 = new HTuple(), hv_Scale = new HTuple();
            HTuple hv_Score = new HTuple(), hv_HomMat2DRotate = new HTuple();
            HTuple hv_HomMat2DTranslate = new HTuple(), hv_Mean = new HTuple();
            HTuple hv_Deviation = new HTuple(), hv_Area1 = new HTuple();
            HTuple hv_Row = new HTuple(), hv_Column = new HTuple();
            HTuple hv_Row1 = new HTuple(), hv_Column1 = new HTuple();
            HTuple hv_Radius = new HTuple(), hv_Mean1 = new HTuple();
            HTuple hv_Deviation1 = new HTuple(), hv_Row4 = new HTuple();
            HTuple hv_Column4 = new HTuple(), hv_Radius1 = new HTuple();
            HTuple hv_StartPhi = new HTuple(), hv_EndPhi = new HTuple();
            HTuple hv_PointOrder = new HTuple(), hv_Row5 = new HTuple();
            HTuple hv_Col = new HTuple();

            HOperatorSet.GenEmptyObj(out ho_RegionAffineTrans);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_ImaAmp);
            HOperatorSet.GenEmptyObj(out ho_ImaDir);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_Contours);
            HOperatorSet.GenEmptyObj(out ho_ContCircle);
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
                if (hv_ModelID == null)
                {
                    HOperatorSet.ReadShapeModel(PathHelper.currentProductPath + @"\shizicao.shm", out hv_ModelID);
                    HOperatorSet.ReadRegion(out ho_RegionFillUp1, PathHelper.currentProductPath + @"\shiziquyu.hobj");
                }
                HOperatorSet.HomMat2dIdentity(out hv_HomMat2DIdentity);
                HOperatorSet.FindScaledShapeModel(Image, hv_ModelID, -3.14, 3.2, 0.9, 1.1,
                    0.1, 1, 0.5, "least_squares", 0, 0.9, out hv_Row2, out hv_Column2, out hv_Angle1,
                    out hv_Scale, out hv_Score);
                //t4 = DateTime.Now;
                //xx = t4 - t1;
                //if (xx.Seconds * 1000 + xx.Milliseconds > 100)
                //{
                //    t2 = DateTime.Now;
                //}
                HOperatorSet.HomMat2dRotate(hv_HomMat2DIdentity, hv_Angle1, hv_Row3, hv_Column3,
                    out hv_HomMat2DRotate);
                HOperatorSet.HomMat2dTranslate(hv_HomMat2DRotate, hv_Row2 - hv_Row3, hv_Column2 - hv_Column3,
                    out hv_HomMat2DTranslate);
                ho_RegionAffineTrans.Dispose();
                HOperatorSet.AffineTransRegion(ho_RegionFillUp1, out ho_RegionAffineTrans,
                    hv_HomMat2DTranslate, "nearest_neighbor");
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_RegionAffineTrans, out ho_ImageReduced
                    );
                ho_ImaAmp.Dispose(); ho_ImaDir.Dispose();
                HOperatorSet.EdgesImage(ho_ImageReduced, out ho_ImaAmp, out ho_ImaDir, "canny",
                    1, "nms", 30, 50);
                HOperatorSet.Intensity(ho_RegionAffineTrans, ho_ImaAmp, out hv_Mean, out hv_Deviation);
                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_ImaAmp, out ho_Region, ((((hv_Mean - hv_Deviation)).TupleConcat(
                    1))).TupleMax(), 255);
                HOperatorSet.AreaCenter(ho_Region, out hv_Area1, out hv_Row, out hv_Column);

                HOperatorSet.Union1(ho_Region, out RegionToDisp);
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("十字槽堵槽");
                hv_result = hv_result.TupleConcat(hv_Area1.D);
                result = hv_result.Clone();

                ho_RegionAffineTrans.Dispose();
                ho_ImageReduced.Dispose();
                ho_ImaAmp.Dispose();
                ho_ImaDir.Dispose();
                ho_Region.Dispose();
                ho_Circle.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionUnion.Dispose();
                ho_RegionClosing.Dispose();
                ho_RegionFillUp.Dispose();
                ho_Contours.Dispose();
                ho_ContCircle.Dispose();
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
                ho_RegionAffineTrans.Dispose();
                ho_ImageReduced.Dispose();
                ho_ImaAmp.Dispose();
                ho_ImaDir.Dispose();
                ho_Region.Dispose();
                ho_Circle.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionUnion.Dispose();
                ho_RegionClosing.Dispose();
                ho_RegionFillUp.Dispose();
                ho_Contours.Dispose();
                ho_ContCircle.Dispose();
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