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
    class hongsexiangpi : ImageTools
    {
        //#region ROI
        //[NonSerialized]
        //private HTuple dPhi = new HTuple();
        //[NonSerialized]
        //private HTuple mianjisx = new HTuple();
        //[NonSerialized]
        //private HTuple mianjixx = new HTuple();
        //[NonSerialized]
        //private HTuple dLength1 = new HTuple();
        //[NonSerialized]
        //private HTuple dLength2 = new HTuple();
        //[NonSerialized]
        //private HTuple dRow = new HTuple();
        //[NonSerialized]
        //private HTuple dColumn = new HTuple();
        //[NonSerialized]
        //private HTuple ddPhi = new HTuple();
        //[NonSerialized]
        //private HTuple ddLength1 = new HTuple();
        //[NonSerialized]
        //private HTuple ddLength2 = new HTuple();
        //[NonSerialized]
        //private HTuple ddRow = new HTuple();
        //[NonSerialized]
        //private HTuple ddColumn = new HTuple();
        //[NonSerialized]
        //private HTuple hv_Radiusd = new HTuple();

        //[NonSerialized]

        //HTuple hv_Col = new HTuple(), hv_dis = new HTuple(), hv_dxi = new HTuple(), hv_Exception = new HTuple(), hv_dn = new HTuple(),
        //hv_dxr = new HTuple(), hv_dxc = new HTuple(), hv_Row2 = new HTuple(), hv_dnr = new HTuple(),
        //hv_dnc = new HTuple(), hv_dni = new HTuple(), hv_Ix = new HTuple(), hv_In, hv_dx = new HTuple();
        //[NonSerialized]
        //HTuple hv_djd = new HTuple(), hv_djdr = new HTuple(), hv_djdc = new HTuple(), hv_djx = new HTuple(),
        //hv_djxr = new HTuple(), hv_jh = new HTuple(), hv_j = new HTuple(), hv_djxc = new HTuple(), hv_xjd = new HTuple(),
        //hv_xjdr = new HTuple(), hv_xjdc = new HTuple(), hv_xjx = new HTuple(), hv_xjxr = new HTuple(),
        //hv_xjxc = new HTuple(), hv_ljd = new HTuple(), hv_ljx = new HTuple();

        //[NonSerialized]
        //private HTuple thresholdValue = new HTuple();
        //[field: NonSerializedAttribute()]
        //HTuple hv_ModelID = null;
        //#endregion
        //public double hv_Row { set; get; }
        //public double hv_Column { set; get; }
        //public double hv_Phi { set; get; }
        //public double thv { set; get; }
        //public double hv_Length1 { set; get; }
        //public double hv_Length2 { set; get; }
        //public double hv1_Row { set; get; }
        //public double hv1_Column { set; get; }
        //public double hv1_Phi { set; get; }
        //public double hv1_Length1 { set; get; }
        //public double hv1_Length2 { set; get; }
        //public double hv_Radius { set; get; }
        //public double mjsx { set; get; }
        //public double mjxx { set; get; }
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public hongsexiangpi()
        {
            RegionToDisp = Image;
        }
        public hongsexiangpi(HObject Image, Algorithm al)
        {
            gex = 0;
            this.algorithm = al;

            this.Image = Image;
            RegionToDisp = Image;
            pixeldist = 1;
        }
        public override void draw()
        {
            //HObject ho_Circle, ho_ImageReduced;
            //HOperatorSet.GenEmptyObj(out ho_Circle);
            //HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            //HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            //HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            //HOperatorSet.DrawCircle(this.LWindowHandle, out dRow, out dColumn, out dPhi);
            //this.hv_Radius = dPhi;
            //HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            //thv = thresholdValue.D;
            //HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\mianjixx", out mianjixx);
            //mjxx = mianjixx.D;
            //HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\mianjisx", out mianjisx);
            //mjsx = mianjisx.D;
            ////ho_Circle.Dispose();
            ////HOperatorSet.GenCircle(out ho_Circle, dRow, dColumn, dPhi);
            ////ho_ImageReduced.Dispose();
            ////HOperatorSet.ReduceDomain(Image, ho_Circle, out ho_ImageReduced);
            ////HOperatorSet.CreateNccModel(ho_ImageReduced, 0, -3.14, 6.29, 0.0175, "use_polarity",
            ////    out hv_ModelID);
            ////HOperatorSet.WriteNccModel(hv_ModelID, PathHelper.currentProductPath + @"\zifu.ncm");
            //ho_Circle.Dispose();
            //ho_ImageReduced.Dispose();
        }
        //DateTime t1, t2, t3, t4,t5,t6,t7;
        private void action()
        {
            // Local iconic variables 

            HObject ho_GrayImage, ho_Image1;
            HObject ho_Image2, ho_Image3, ho_ImageResult1, ho_ImageResult2;
            HObject ho_ImageResult3, ho_im, ho_Region, ho_RegionConnection;
            HObject ho_RegionSelected, ho_RegionDilation, ho_RegionFillUp;
            HObject ho_RegionDifference, ho_ConnectedRegions1, ho_SelectedRegions1;
            HObject ho_Contour_JiaoQuanOuter, ho_Circle, ho_Contour_JiaoQuanInner;
            HObject ho_Region_WaiKuang, ho_RegionFillUp_WaiKuang, ho_ConnectedRegions_WaiKuang;
            HObject ho_SelectedRegions_WaiKuang, ho_Contour_WaiKuang;
            HObject ho_Image33;

            // Local control variables 

            HTuple hv_Area2 = null, hv_Row = null, hv_Column = null;
            HTuple hv_AreaRegionSelected = null, hv_RowRegionSelected = null;
            HTuple hv_ColumnRegionSelected = null, hv_NumRegion = null;
            HTuple hv_ContourCount_JiaoQuanOuter = null, hv_RowFit_JiaoQuanOuter = null;
            HTuple hv_ColumnFit_JiaoQuanOuter = null, hv_RadiusFit_JiaoQuanOuter = null;
            HTuple hv_StartPhi = null, hv_EndPhi = null, hv_PointOrder = null;
            HTuple hv_RowSmallest_JiaoQuanOuter = null, hv_ColumnSmallest_JiaoQuanOuter = null;
            HTuple hv_RadiusSmallest_JiaoQuanOuter = null, hv_RowInner_JiaoQuanOuter = null;
            HTuple hv_ColumnInner_JiaoQuanOuter = null, hv_RadiusInner_JiaoQuanOuter = null;
            HTuple hv_RadiusRatio_JiaoQuanOuter = null, hv_Row_JiaoQuanOuter = null;
            HTuple hv_Col_JiaoQuanOuter = null, hv_GapAbsAll_JiaoQuanOuter = null;
            HTuple hv_j = null, hv_i = null, hv_k = null, hv_Distance1 = new HTuple(), hv_GapAbs = new HTuple();
            HTuple hv_Sorted = new HTuple(), hv_Sorted_JiaoQuanOuter = new HTuple(), hv_Sorted_JiaoQuanInner = new HTuple(), hv_Sorted_WaiKuang = new HTuple();
            HTuple hv_MaxGap_JiaoQuanOuter = null;
            HTuple hv_ContourCount_JiaoQuanInner = null, hv_RowFit_JiaoQuanInner = null;
            HTuple hv_ColumnFit_JiaoQuanInner = null, hv_RadiusFit_JiaoQuanInner = null;
            HTuple hv_RowSmallest_JiaoQuanInner = null, hv_ColumnSmallest_JiaoQuanInner = null;
            HTuple hv_RadiusSmallest_JiaoQuanInner = null, hv_RowInner_JiaoQuanInner = null;
            HTuple hv_ColumnInner_JiaoQuanInner = null, hv_RadiusInner_JiaoQuanInner = null;
            HTuple hv_RadiusRatio_JiaoQuanInner = null, hv_Row_JiaoQuanInner = null;
            HTuple hv_Col_JiaoQuanInner = null, hv_GapAbsAll_JiaoQuanInner = null;
            HTuple hv_MaxGap_JiaoQuanInner = null, hv_ContourCount_WaiKuang = null;
            HTuple hv_RowFit_WaiKuang = null, hv_ColumnFit_WaiKuang = null;
            HTuple hv_RadiusFit_WaiKuang = null, hv_RowSmallest_WaiKuang = null;
            HTuple hv_ColumnSmallest_WaiKuang = null, hv_RadiusSmallest_WaiKuang = null;
            HTuple hv_RowInner_WaiKuang = null, hv_ColumnInner_WaiKuang = null;
            HTuple hv_RadiusInner_WaiKuang = null, hv_RadiusRatio_WaiKuang = null;
            HTuple hv_Row_WaiKuang = null, hv_Col_WaiKuang = null;
            HTuple hv_GapAbsAll_WaiKuang = null, hv_MaxGap_WaiKuang = null;
            HTuple hv_NumConnected = null, hv_NumHoles = null, hv_Area_JiaoQuan = null;
            HTuple hv_Row_JiaoQuan = null, hv_Column_JiaoQuan = null;
            HTuple hv_Area_JiaoQuanOuter = null, hv_Column_JiaoQuanOuter = null;
            HTuple hv_Area_JiaoQuanInner = null, hv_Column_JiaoQuanInner = null;
            HTuple hv_a = null, hv_hd = null, hv_Deviation = null;
            //HTuple hv_result = new HTuple();
            // Initialize local and output iconic variables 
            //HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_GrayImage);
            HOperatorSet.GenEmptyObj(out ho_Image1);
            HOperatorSet.GenEmptyObj(out ho_Image2);
            HOperatorSet.GenEmptyObj(out ho_Image3);
            HOperatorSet.GenEmptyObj(out ho_ImageResult1);
            HOperatorSet.GenEmptyObj(out ho_ImageResult2);
            HOperatorSet.GenEmptyObj(out ho_ImageResult3);
            HOperatorSet.GenEmptyObj(out ho_im);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_RegionConnection);
            HOperatorSet.GenEmptyObj(out ho_RegionSelected);
            HOperatorSet.GenEmptyObj(out ho_RegionDilation);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_Contour_JiaoQuanOuter);
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_Contour_JiaoQuanInner);
            HOperatorSet.GenEmptyObj(out ho_Region_WaiKuang);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp_WaiKuang);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions_WaiKuang);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions_WaiKuang);
            HOperatorSet.GenEmptyObj(out ho_Contour_WaiKuang);
            HOperatorSet.GenEmptyObj(out ho_Image33);

            //t3 = DateTime.Now;
            try
            {

                ho_Image1.Dispose(); ho_Image2.Dispose(); ho_Image3.Dispose();
                HOperatorSet.Decompose3(Image, out ho_Image1, out ho_Image2, out ho_Image3);
                ho_ImageResult1.Dispose(); ho_ImageResult2.Dispose(); ho_ImageResult3.Dispose();
                HOperatorSet.TransFromRgb(ho_Image1, ho_Image2, ho_Image3, out ho_ImageResult1, out ho_ImageResult2, out ho_ImageResult3, "hls");
                ho_ImageResult1.Dispose();
                HOperatorSet.SubImage(ho_Image1, ho_Image2, out ho_ImageResult1, 1, 0);
                ho_im.Dispose();
                HOperatorSet.MeanImage(ho_ImageResult1, out ho_im, 9, 9);
                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_ImageResult1, out ho_Region, 40, 255);
                //closing_circle (Region, Region, 3.5)
                ho_RegionConnection.Dispose();
                HOperatorSet.Connection(ho_Region, out ho_RegionConnection);
                //select_shape (RegionConnection, RegionSelected, 'area', 'and', 10000, 9999999)
                ho_RegionSelected.Dispose();
                HOperatorSet.SelectShapeStd(ho_RegionConnection, out ho_RegionSelected, "max_area", 70);
                ho_RegionDilation.Dispose();
                HOperatorSet.DilationCircle(ho_RegionSelected, out ho_RegionDilation, 1.5);
                HOperatorSet.AreaCenter(ho_RegionDilation, out hv_Area2, out hv_Row, out hv_Column);
                ho_RegionFillUp.Dispose();
                HOperatorSet.FillUp(ho_RegionDilation, out ho_RegionFillUp);
                ho_RegionDifference.Dispose();
                HOperatorSet.Difference(ho_RegionFillUp, ho_RegionDilation, out ho_RegionDifference);
                ho_ConnectedRegions1.Dispose();
                HOperatorSet.Connection(ho_RegionDifference, out ho_ConnectedRegions1);
                ho_SelectedRegions1.Dispose();
                HOperatorSet.SelectShapeStd(ho_ConnectedRegions1, out ho_SelectedRegions1, "max_area", 70);

                //JiaoQuanOuter胶圈外圆：圆度、直径
                HOperatorSet.AreaCenter(ho_RegionFillUp, out hv_AreaRegionSelected, out hv_RowRegionSelected, out hv_ColumnRegionSelected);
                hv_NumRegion = new HTuple(hv_AreaRegionSelected.TupleLength());

                ho_Contour_JiaoQuanOuter.Dispose();
                HOperatorSet.GenContourRegionXld(ho_RegionFillUp, out ho_Contour_JiaoQuanOuter, "border");
                HOperatorSet.ContourPointNumXld(ho_Contour_JiaoQuanOuter, out hv_ContourCount_JiaoQuanOuter);

                HOperatorSet.FitCircleContourXld(ho_Contour_JiaoQuanOuter, "atukey", -1, 2, 0,
                    10, 1, out hv_RowFit_JiaoQuanOuter, out hv_ColumnFit_JiaoQuanOuter, out hv_RadiusFit_JiaoQuanOuter,
                    out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);

                HOperatorSet.SmallestCircleXld(ho_Contour_JiaoQuanOuter, out hv_RowSmallest_JiaoQuanOuter,
                    out hv_ColumnSmallest_JiaoQuanOuter, out hv_RadiusSmallest_JiaoQuanOuter);

                HOperatorSet.InnerCircle(ho_RegionFillUp, out hv_RowInner_JiaoQuanOuter, out hv_ColumnInner_JiaoQuanOuter,
                    out hv_RadiusInner_JiaoQuanOuter);
                //*半径比值 of 外接圆和内切圆
                hv_RadiusRatio_JiaoQuanOuter = hv_RadiusInner_JiaoQuanOuter / hv_RadiusSmallest_JiaoQuanOuter;

                //**Contour Point
                HOperatorSet.GetContourXld(ho_Contour_JiaoQuanOuter, out hv_Row_JiaoQuanOuter, out hv_Col_JiaoQuanOuter);
                hv_GapAbsAll_JiaoQuanOuter = new HTuple();
                hv_j = 0;
                HTuple end_val51 = hv_ContourCount_JiaoQuanOuter - 1;
                HTuple step_val51 = 10;
                for (hv_i = 0; hv_i.Continue(end_val51, step_val51); hv_i = hv_i.TupleAdd(step_val51))
                {
                    HOperatorSet.DistancePp(hv_Row_JiaoQuanOuter.TupleSelect(hv_i), hv_Col_JiaoQuanOuter.TupleSelect(
                        hv_i), hv_RowFit_JiaoQuanOuter, hv_ColumnFit_JiaoQuanOuter, out hv_Distance1);
                    HOperatorSet.TupleAbs(hv_Distance1 - hv_RadiusFit_JiaoQuanOuter, out hv_GapAbs);
                    hv_GapAbsAll_JiaoQuanOuter = hv_GapAbsAll_JiaoQuanOuter.TupleConcat(hv_GapAbs);
                    hv_j = hv_j + 1;
                }
                HOperatorSet.TupleSort(hv_GapAbsAll_JiaoQuanOuter, out hv_Sorted_JiaoQuanOuter);
                hv_MaxGap_JiaoQuanOuter = 0;
                for (hv_k = 0; (int)hv_k <= 2; hv_k = (int)hv_k + 1)
                {
                    hv_MaxGap_JiaoQuanOuter = hv_MaxGap_JiaoQuanOuter + (hv_Sorted_JiaoQuanOuter.TupleSelect((hv_j - hv_k) - 1));
                }
                hv_MaxGap_JiaoQuanOuter = hv_MaxGap_JiaoQuanOuter / 3;
                //hv_MaxGap_JiaoQuanOuter = hv_Sorted_JiaoQuanOuter.TupleSelect(hv_ContourCount_JiaoQuanOuter - 1);

                //JiaoQuanInner胶圈内圆：圆度、直径
                HOperatorSet.AreaCenter(ho_SelectedRegions1, out hv_AreaRegionSelected, out hv_RowRegionSelected, out hv_ColumnRegionSelected);
                hv_NumRegion = new HTuple(hv_AreaRegionSelected.TupleLength());
                ho_Contour_JiaoQuanInner.Dispose();
                HOperatorSet.GenContourRegionXld(ho_SelectedRegions1, out ho_Contour_JiaoQuanInner, "border");
                HOperatorSet.ContourPointNumXld(ho_Contour_JiaoQuanInner, out hv_ContourCount_JiaoQuanInner);

                HOperatorSet.FitCircleContourXld(ho_Contour_JiaoQuanInner, "atukey", -1, 2, 0,
                    10, 1, out hv_RowFit_JiaoQuanInner, out hv_ColumnFit_JiaoQuanInner, out hv_RadiusFit_JiaoQuanInner,
                    out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);

                ho_Circle.Dispose();
                HOperatorSet.GenCircle(out ho_Circle, hv_RowFit_JiaoQuanInner, hv_ColumnFit_JiaoQuanInner,
                    hv_RadiusFit_JiaoQuanInner);

                HOperatorSet.SmallestCircleXld(ho_Contour_JiaoQuanInner, out hv_RowSmallest_JiaoQuanInner, out hv_ColumnSmallest_JiaoQuanInner, out hv_RadiusSmallest_JiaoQuanInner);
                //gen_circle (Circle1, RowSmallest_JiaoQuanInner, ColumnSmallest_JiaoQuanInner, RadiusSmallest_JiaoQuanInner)

                HOperatorSet.InnerCircle(ho_SelectedRegions1, out hv_RowInner_JiaoQuanInner, out hv_ColumnInner_JiaoQuanInner, out hv_RadiusInner_JiaoQuanInner);
                //gen_circle (Circle2, RowInner_JiaoQuanInner, ColumnInner_JiaoQuanInner, RadiusInner_JiaoQuanInner)
                //*半径比值 of 外接圆和内切圆
                hv_RadiusRatio_JiaoQuanInner = hv_RadiusInner_JiaoQuanInner / hv_RadiusSmallest_JiaoQuanInner;

                //**Contour Point
                HOperatorSet.GetContourXld(ho_Contour_JiaoQuanInner, out hv_Row_JiaoQuanInner, out hv_Col_JiaoQuanInner);
                hv_GapAbsAll_JiaoQuanInner = new HTuple();
                hv_j = 0;
                HTuple end_val89 = hv_ContourCount_JiaoQuanInner - 1;
                HTuple step_val89 = 10;
                for (hv_i = 0; hv_i.Continue(end_val89, step_val89); hv_i = hv_i.TupleAdd(step_val89))
                {
                    HOperatorSet.DistancePp(hv_Row_JiaoQuanInner.TupleSelect(hv_i), hv_Col_JiaoQuanInner.TupleSelect(
                        hv_i), hv_RowFit_JiaoQuanInner, hv_ColumnFit_JiaoQuanInner, out hv_Distance1);
                    HOperatorSet.TupleAbs(hv_Distance1 - hv_RadiusFit_JiaoQuanInner, out hv_GapAbs);
                    hv_GapAbsAll_JiaoQuanInner = hv_GapAbsAll_JiaoQuanInner.TupleConcat(hv_GapAbs);
                    hv_j = hv_j + 1;
                }
                HOperatorSet.TupleSort(hv_GapAbsAll_JiaoQuanInner, out hv_Sorted_JiaoQuanInner);
                hv_MaxGap_JiaoQuanInner = 0;
                for (hv_k = 0; (int)hv_k <= 2; hv_k = (int)hv_k + 1)
                {
                    hv_MaxGap_JiaoQuanInner = hv_MaxGap_JiaoQuanInner + (hv_Sorted_JiaoQuanInner.TupleSelect((hv_j - hv_k) - 1));
                }
                hv_MaxGap_JiaoQuanInner = hv_MaxGap_JiaoQuanInner / 3;
                //hv_MaxGap_JiaoQuanInner = hv_Sorted_JiaoQuanInner.TupleSelect(hv_ContourCount_JiaoQuanInner - 1);

                //外框外圆：圆度、直径
                ho_GrayImage.Dispose();
                HOperatorSet.Rgb1ToGray(Image, out ho_GrayImage);
                ho_Region_WaiKuang.Dispose();
                HOperatorSet.Threshold(ho_GrayImage, out ho_Region_WaiKuang, 100, 255);
                ho_RegionFillUp_WaiKuang.Dispose();
                HOperatorSet.FillUp(ho_Region_WaiKuang, out ho_RegionFillUp_WaiKuang);
                ho_ConnectedRegions_WaiKuang.Dispose();
                HOperatorSet.Connection(ho_RegionFillUp_WaiKuang, out ho_ConnectedRegions_WaiKuang);
                ho_SelectedRegions_WaiKuang.Dispose();
                HOperatorSet.SelectShapeStd(ho_ConnectedRegions_WaiKuang, out ho_SelectedRegions_WaiKuang, "max_area", 70);
                //smallest_circle (RegionFillUp_WaiKuang, Row2, Column2, Radius)
                //inner_circle (RegionFillUp_WaiKuang, Row3, Column3, Radius1)

                HOperatorSet.AreaCenter(ho_SelectedRegions_WaiKuang, out hv_AreaRegionSelected, out hv_RowRegionSelected, out hv_ColumnRegionSelected);
                hv_NumRegion = new HTuple(hv_AreaRegionSelected.TupleLength());

                ho_Contour_WaiKuang.Dispose();
                HOperatorSet.GenContourRegionXld(ho_SelectedRegions_WaiKuang, out ho_Contour_WaiKuang, "border");
                HOperatorSet.ContourPointNumXld(ho_Contour_WaiKuang, out hv_ContourCount_WaiKuang);

                HOperatorSet.FitCircleContourXld(ho_Contour_WaiKuang, "atukey", -1, 2, 0, 10,
                    1, out hv_RowFit_WaiKuang, out hv_ColumnFit_WaiKuang, out hv_RadiusFit_WaiKuang,
                    out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);

                ho_Circle.Dispose();
                HOperatorSet.GenCircle(out ho_Circle, hv_RowFit_WaiKuang, hv_ColumnFit_WaiKuang,
                    hv_RadiusFit_WaiKuang);

                HOperatorSet.SmallestCircleXld(ho_Contour_WaiKuang, out hv_RowSmallest_WaiKuang, out hv_ColumnSmallest_WaiKuang, out hv_RadiusSmallest_WaiKuang);
                //gen_circle (Circle1, RowSmallest_WaiKuang, ColumnSmallest_WaiKuang, RadiusSmallest_WaiKuang)

                HOperatorSet.InnerCircle(ho_SelectedRegions_WaiKuang, out hv_RowInner_WaiKuang, out hv_ColumnInner_WaiKuang, out hv_RadiusInner_WaiKuang);
                //gen_circle (Circle2, RowInner_WaiKuang, ColumnInner_WaiKuang, RadiusInner_WaiKuang)
                //*半径比值 of 外接圆和内切圆
                hv_RadiusRatio_WaiKuang = hv_RadiusInner_WaiKuang / hv_RadiusSmallest_WaiKuang;

                //**Contour Point
                HOperatorSet.GetContourXld(ho_Contour_WaiKuang, out hv_Row_WaiKuang, out hv_Col_WaiKuang);
                hv_GapAbsAll_WaiKuang = new HTuple();
                hv_j = 0;
                HTuple end_val135 = hv_ContourCount_WaiKuang - 1;
                HTuple step_val135 = 10;
                for (hv_i = 0; hv_i.Continue(end_val135, step_val135); hv_i = hv_i.TupleAdd(step_val135))
                {
                    HOperatorSet.DistancePp(hv_Row_WaiKuang.TupleSelect(hv_i), hv_Col_WaiKuang.TupleSelect(
                        hv_i), hv_RowFit_WaiKuang, hv_ColumnFit_WaiKuang, out hv_Distance1);
                    HOperatorSet.TupleAbs(hv_Distance1 - hv_RadiusFit_WaiKuang, out hv_GapAbs);
                    hv_GapAbsAll_WaiKuang = hv_GapAbsAll_WaiKuang.TupleConcat(hv_GapAbs);
                    hv_j = hv_j + 1;
                }
                HOperatorSet.TupleSort(hv_GapAbsAll_WaiKuang, out hv_Sorted_WaiKuang);
                hv_MaxGap_WaiKuang = 0;
                for (hv_k = 0; (int)hv_k <= 2; hv_k = (int)hv_k + 1)
                {
                    hv_MaxGap_WaiKuang = hv_MaxGap_WaiKuang + (hv_Sorted_WaiKuang.TupleSelect((hv_j - hv_k) - 1));
                }
                hv_MaxGap_WaiKuang = hv_MaxGap_WaiKuang / 3;
                //hv_MaxGap_WaiKuang = hv_Sorted_WaiKuang.TupleSelect(hv_ContourCount_WaiKuang - 1);

                //脏污
                HOperatorSet.ConnectAndHoles(ho_RegionSelected, out hv_NumConnected, out hv_NumHoles);

                HOperatorSet.AreaCenter(ho_RegionFillUp, out hv_Area_JiaoQuan, out hv_Row_JiaoQuan, out hv_Column_JiaoQuan);
                HOperatorSet.AreaCenter(ho_RegionSelected, out hv_Area_JiaoQuanOuter, out hv_Row_JiaoQuanOuter, out hv_Column_JiaoQuanOuter);
                HOperatorSet.AreaCenter(ho_SelectedRegions1, out hv_Area_JiaoQuanInner, out hv_Row_JiaoQuanInner, out hv_Column_JiaoQuanInner);
                hv_a = (hv_Area_JiaoQuan - hv_Area_JiaoQuanOuter) - hv_Area_JiaoQuanInner;

                ho_Image33.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_SelectedRegions1, out ho_Image33);
                HOperatorSet.Intensity(ho_SelectedRegions1, ho_Image33, out hv_hd, out hv_Deviation);

                HOperatorSet.Union1(ho_RegionDilation, out RegionToDisp);

                //Result
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("橡皮圆度-半径比");
                hv_result = hv_result.TupleConcat(hv_RadiusRatio_JiaoQuanOuter.D);
                hv_result = hv_result.TupleConcat("橡皮圆度-最大值");
                hv_result = hv_result.TupleConcat(hv_MaxGap_JiaoQuanOuter.D);

                hv_result = hv_result.TupleConcat("橡皮内孔圆度-半径比");
                hv_result = hv_result.TupleConcat(hv_RadiusRatio_JiaoQuanInner.D);
                hv_result = hv_result.TupleConcat("橡皮内孔圆度-最大值");
                hv_result = hv_result.TupleConcat(hv_MaxGap_JiaoQuanInner.D);
                hv_result = hv_result.TupleConcat("橡皮内孔面积");
                hv_result = hv_result.TupleConcat(hv_Area_JiaoQuanInner.D);

                hv_result = hv_result.TupleConcat("橡皮面积");
                hv_result = hv_result.TupleConcat(hv_Area_JiaoQuanOuter.D);

                hv_result = hv_result.TupleConcat("橡皮灰尘");
                hv_result = hv_result.TupleConcat(hv_a.D);

                hv_result = hv_result.TupleConcat("外圆圆度-半径比");
                hv_result = hv_result.TupleConcat(hv_RadiusRatio_WaiKuang.D);
                hv_result = hv_result.TupleConcat("外圆圆度-最大值");
                hv_result = hv_result.TupleConcat(hv_MaxGap_WaiKuang.D);
                hv_result = hv_result.TupleConcat("外圆内圆直径");
                hv_result = hv_result.TupleConcat(hv_RadiusInner_WaiKuang.D * pixeldist * 2);
                hv_result = hv_result.TupleConcat("外圆外圆直径");
                hv_result = hv_result.TupleConcat(hv_RadiusSmallest_WaiKuang.D * pixeldist * 2);

                hv_result = hv_result.TupleConcat("内孔灰度");
                hv_result = hv_result.TupleConcat(hv_hd.D);
                result = hv_result.Clone();

                //t4 = DateTime.Now;
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("橡皮圆度-半径比");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("橡皮圆度-最大值");
                hv_result = hv_result.TupleConcat(0);

                hv_result = hv_result.TupleConcat("橡皮内孔圆度-半径比");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("橡皮内孔圆度-最大值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("橡皮内孔面积");
                hv_result = hv_result.TupleConcat(0);

                hv_result = hv_result.TupleConcat("橡皮面积");
                hv_result = hv_result.TupleConcat(0);

                hv_result = hv_result.TupleConcat("橡皮灰尘");
                hv_result = hv_result.TupleConcat(0);

                hv_result = hv_result.TupleConcat("外圆圆度-半径比");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("外圆圆度-最大值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("外圆内圆直径");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("外圆外圆直径");
                hv_result = hv_result.TupleConcat(0);

                hv_result = hv_result.TupleConcat("内孔灰度");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();

            }
            finally
            {
                //ho_Image.Dispose();
                ho_GrayImage.Dispose();
                ho_Image1.Dispose();
                ho_Image2.Dispose();
                ho_Image3.Dispose();
                ho_ImageResult1.Dispose();
                ho_ImageResult2.Dispose();
                ho_ImageResult3.Dispose();
                ho_im.Dispose();
                ho_Region.Dispose();
                ho_RegionConnection.Dispose();
                ho_RegionSelected.Dispose();
                ho_RegionDilation.Dispose();
                ho_RegionFillUp.Dispose();
                ho_RegionDifference.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_Contour_JiaoQuanOuter.Dispose();
                ho_Circle.Dispose();
                ho_Contour_JiaoQuanInner.Dispose();
                ho_Region_WaiKuang.Dispose();
                ho_RegionFillUp_WaiKuang.Dispose();
                ho_ConnectedRegions_WaiKuang.Dispose();
                ho_SelectedRegions_WaiKuang.Dispose();
                ho_Contour_WaiKuang.Dispose();
                ho_Image33.Dispose();
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