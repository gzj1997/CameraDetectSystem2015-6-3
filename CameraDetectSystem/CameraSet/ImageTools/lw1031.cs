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
    class lw1031 : ImageTools
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
        #endregion
        public double hv_Rowm { set; get; }
        public double hv_Columnm { set; get; }
        public double hv_Phim { set; get; }
        public double thv { set; get; }
        public double hv_Length1m { set; get; }
        public double hv_Length2m { set; get; }
        public double hv1_Rowx { set; get; }
        public double hv1_Columnx { set; get; }
        public double hv1_Phix { set; get; }
        public double hv1_Length1x { set; get; }
        public double hv1_Length2x { set; get; }

        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public lw1031()
        {
            RegionToDisp = Image;
        }
        public lw1031(HObject Image, Algorithm al)
        {
            gex = 0;
            this.algorithm = al;

            this.Image = Image;
            RegionToDisp = Image;
            pixeldist = 1;
        }
        public override void draw()
        {
            //HTuple dRow = null, dColumn = null, dPhi = null, thresholdValue = null, dLength1 = null, dLength2 = null;
            HObject ho_Rectangle, ho_ImageReduced, ho_Border;
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Border);
            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            HOperatorSet.DrawRectangle2(this.LWindowHandle, out dRow, out dColumn,
                out dPhi, out dLength1, out dLength2);
            HOperatorSet.DrawRectangle2(this.LWindowHandle, out ddRow, out ddColumn,
                out ddPhi, out ddLength1, out ddLength2);
            dRow = 490.009; dColumn =1082.65;
            dPhi = 1.50586; dLength1 =90.8456; dLength2 = 8.34029;
            this.hv_Rowm = dRow.D;
            this.hv_Columnm = dColumn.D;
            this.hv_Phim = dPhi.D;
            this.hv_Length1m = dLength1.D;
            this.hv_Length2m = dLength2.D;
            this.hv1_Rowx = ddRow.D;
            this.hv1_Columnx = ddColumn.D;
            this.hv1_Phix = ddPhi.D;
            this.hv1_Length1x = ddLength1.D;
            this.hv1_Length2x = ddLength2.D;
            HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            thv = thresholdValue.D;
            ho_Rectangle.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangle, hv_Rowm, hv_Columnm, hv_Phim, hv_Length1m,
                hv_Length2m);
            ho_ImageReduced.Dispose();
            HOperatorSet.ReduceDomain(Image, ho_Rectangle, out ho_ImageReduced);
            ho_Border.Dispose();
            HOperatorSet.ThresholdSubPix(ho_ImageReduced, out ho_Border, thv);
            HOperatorSet.CreateNccModel(ho_ImageReduced, 0, -3.14, 6.29, 0.0175, "use_polarity",
        out hv_ModelID);
            HOperatorSet.WriteNccModel(hv_ModelID, PathHelper.currentProductPath + @"\lwspmd.ncm");
            ho_Rectangle.Dispose();
            ho_ImageReduced.Dispose();
            ho_Border.Dispose();
        }
        //DateTime t1, t2, t3, t4,t5,t6,t7;
        private void action()
        {
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_Rectangle;
            HObject ho_ImageReduced, ho_Rectangle1, ho_ImageReduced1;
            HObject ho_Region, ho_ConnectedRegions, ho_ObjectSelected1;
            HObject ho_Contours, ho_ContoursSplit, ho_SelectedContours;
            HObject ho_UnionContours, ho_ObjectSelected = null, ho_Region1 = null;
            HObject ho_EmptyObject, ho_Rectangle3 = null, ho_ImageReduced2 = null;
            HObject ho_Edges = null, ho_UnionContours1 = null, ho_ObjectSelected2 = null;
            HObject ho_RegionLines = null;

            // Local control variables 

            
            HTuple hv_ModelID = null;
            
            HTuple hv_Area = null;
            HTuple hv_Row5 = null, hv_Column4 = null, hv_Indices = null;
            HTuple hv_Length = null, hv_Mean = null, hv_Deviation = null;
            HTuple hv_Number = null, hv_Row3 = new HTuple(), hv_Col = new HTuple();
            HTuple hv_Row4 = new HTuple(), hv_Column3 = new HTuple();
            HTuple hv_Phi2 = new HTuple(), hv_Length12 = new HTuple();
            HTuple hv_Length22 = new HTuple(), hv_sdr1 = null, hv_sdr2 = null;
            HTuple hv_sdc1 = null, hv_sdc2 = null, hv_cdr1 = null;
            HTuple hv_cdr2 = null, hv_cdc1 = null, hv_cdc2 = null;
            HTuple hv_Row2 = null, hv_Column2 = null, hv_Angle = null;
            HTuple hv_Score = null, hv_djj = null, hv_djy = null, hv_xjj = null;
            HTuple hv_xjy = null, hv_Index = null, hv_Number1 = new HTuple();
            HTuple hv_min1 = new HTuple(), hv_max1 = new HTuple();
            HTuple hv_d = new HTuple(), hv_r6 = new HTuple(), hv_c6 = new HTuple();
            HTuple hv_Index1 = new HTuple(), hv_Row6 = new HTuple();
            HTuple hv_Col1 = new HTuple(), hv_Distance = new HTuple();
            HTuple hv_Mins = new HTuple(), hv_Maxs = new HTuple();
            HTuple hv_Indices1 = new HTuple(), hv_Indices2 = new HTuple();
            HTuple hv_djpj = null, hv_djzd = null, hv_djzx = null;
            HTuple hv_xjpj = null, hv_xjzd = null, hv_xjzx = null;
            HTuple hv_Distance1 = null, hv_lwts = null, hv_lwcd = null;
            HTuple hv_luoju = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected1);
            HOperatorSet.GenEmptyObj(out ho_Contours);
            HOperatorSet.GenEmptyObj(out ho_ContoursSplit);
            HOperatorSet.GenEmptyObj(out ho_SelectedContours);
            HOperatorSet.GenEmptyObj(out ho_UnionContours);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_EmptyObject);
            HOperatorSet.GenEmptyObj(out ho_Rectangle3);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced2);
            HOperatorSet.GenEmptyObj(out ho_Edges);
            HOperatorSet.GenEmptyObj(out ho_UnionContours1);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected2);
            HOperatorSet.GenEmptyObj(out ho_RegionLines);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            //t3 = DateTime.Now;
            try
            {
                if (hv_ModelID == null)
                {
                    HOperatorSet.ReadNccModel(PathHelper.currentProductPath + @"\lwspmd.ncm", out hv_ModelID);
                }
                HOperatorSet.GenRectangle2(out ho_Rectangle1, hv1_Rowx, hv1_Columnx, hv1_Phix, hv1_Length1x,
        hv1_Length2x);
                ho_ImageReduced1.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Rectangle1, out ho_ImageReduced1
                    );
                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced1, out ho_Region, 0, 128);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
                HOperatorSet.AreaCenter(ho_ConnectedRegions, out hv_Area, out hv_Row5, out hv_Column4);
                HOperatorSet.TupleFind(hv_Area, hv_Area.TupleMax(), out hv_Indices);
                ho_ObjectSelected1.Dispose();
                HOperatorSet.SelectObj(ho_ConnectedRegions, out ho_ObjectSelected1, hv_Indices + 1);
                ho_Contours.Dispose();
                HOperatorSet.GenContourRegionXld(ho_ObjectSelected1, out ho_Contours, "border");
                ho_ContoursSplit.Dispose();
                HOperatorSet.SegmentContoursXld(ho_Contours, out ho_ContoursSplit, "lines_circles",
                    5, 4, 2);
                HOperatorSet.LengthXld(ho_ContoursSplit, out hv_Length);
                HOperatorSet.TupleMean(hv_Length, out hv_Mean);
                HOperatorSet.TupleDeviation(hv_Length, out hv_Deviation);
                ho_SelectedContours.Dispose();
                HOperatorSet.SelectContoursXld(ho_ContoursSplit, out ho_SelectedContours, "contour_length",
                    0, hv_Mean + (hv_Deviation * 0.4), -0.5, 0.5);
                ho_UnionContours.Dispose();
                HOperatorSet.UnionAdjacentContoursXld(ho_SelectedContours, out ho_UnionContours,
                    10, 1, "attr_keep");
                HOperatorSet.CountObj(ho_UnionContours, out hv_Number);
                if ((int)(new HTuple(hv_Number.TupleEqual(2))) != 0)
                {
                    ho_ObjectSelected.Dispose();
                    HOperatorSet.SelectObj(ho_UnionContours, out ho_ObjectSelected, 1);
                    HOperatorSet.GetContourXld(ho_ObjectSelected, out hv_Row3, out hv_Col);
                    ho_Region1.Dispose();
                    HOperatorSet.GenRegionPoints(out ho_Region1, hv_Row3, hv_Col);
                    HOperatorSet.SmallestRectangle2(ho_Region1, out hv_Row4, out hv_Column3, out hv_Phi2,
                        out hv_Length12, out hv_Length22);
                }
                hv_sdr1 = hv_Row4 + (10000.000 * (hv_Phi2.TupleSin()));
                hv_sdr2 = hv_Row4 - (10000.000 * (hv_Phi2.TupleSin()));
                hv_sdc1 = hv_Column3 - (10000.000 * (hv_Phi2.TupleCos()));
                hv_sdc2 = hv_Column3 + (10000.000 * (hv_Phi2.TupleCos()));
                hv_cdr1 = hv_Row4 - (10000.000 * (hv_Phi2.TupleCos()));
                hv_cdr2 = hv_Row4 + (10000.000 * (hv_Phi2.TupleCos()));
                hv_cdc1 = hv_Column3 - (10000.000 * (hv_Phi2.TupleSin()));
                hv_cdc2 = hv_Column3 + (10000.000 * (hv_Phi2.TupleSin()));
                HOperatorSet.FindNccModel(ho_ImageReduced1, hv_ModelID, -((new HTuple(5)).TupleRad()
                    ), (new HTuple(5)).TupleRad(), 0.5, 0, 0.5, "true", 0, out hv_Row2, out hv_Column2,
                    out hv_Angle, out hv_Score);
                hv_djj = new HTuple();
                hv_djy = new HTuple();
                hv_xjj = new HTuple();
                hv_xjy = new HTuple();
                ho_EmptyObject.Dispose();
                HOperatorSet.GenEmptyObj(out ho_EmptyObject);
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Row2.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
                {
                    ho_Rectangle3.Dispose();
                    HOperatorSet.GenRectangle2(out ho_Rectangle3, hv_Row2.TupleSelect(hv_Index),
                        hv_Column2.TupleSelect(hv_Index), hv_Phim + (hv_Angle.TupleSelect(hv_Index)),
                        hv_Length1m, hv_Length2m);
                    ho_ImageReduced2.Dispose();
                    HOperatorSet.ReduceDomain(Image, ho_Rectangle3, out ho_ImageReduced2
                        );
                    ho_Edges.Dispose();
                    HOperatorSet.EdgesSubPix(ho_ImageReduced2, out ho_Edges, "canny", 1, 20, 40);
                    ho_UnionContours1.Dispose();
                    HOperatorSet.UnionAdjacentContoursXld(ho_Edges, out ho_UnionContours1, 10,
                        1, "attr_keep");
                    HOperatorSet.CountObj(ho_UnionContours1, out hv_Number1);
                    if ((int)(new HTuple(hv_Number1.TupleEqual(2))) != 0)
                    {
                        hv_min1 = new HTuple();
                        hv_max1 = new HTuple();
                        hv_d = new HTuple();
                        hv_r6 = new HTuple();
                        hv_c6 = new HTuple();
                        for (hv_Index1 = 0; (int)hv_Index1 <= 1; hv_Index1 = (int)hv_Index1 + 1)
                        {
                            ho_ObjectSelected2.Dispose();
                            HOperatorSet.SelectObj(ho_UnionContours1, out ho_ObjectSelected2, hv_Index1 + 1);
                            HOperatorSet.GetContourXld(ho_ObjectSelected2, out hv_Row6, out hv_Col1);
                            HOperatorSet.DistancePl(hv_Row6, hv_Col1, hv_sdr1, hv_sdc1, hv_sdr2, hv_sdc2,
                                out hv_Distance);
                            HOperatorSet.TupleMin(hv_Distance, out hv_Mins);
                            HOperatorSet.TupleMax(hv_Distance, out hv_Maxs);
                            hv_min1 = hv_min1.TupleConcat(hv_Mins);
                            hv_max1 = hv_max1.TupleConcat(hv_Maxs);
                            hv_d = hv_d.TupleConcat(hv_Distance);
                            hv_r6 = hv_r6.TupleConcat(hv_Row6);
                            hv_c6 = hv_c6.TupleConcat(hv_Col1);
                        }
                    }
                    hv_djj = hv_djj.TupleConcat(hv_min1.TupleMin());
                    hv_djy = hv_djy.TupleConcat(hv_max1.TupleMax());
                    hv_xjj = hv_xjj.TupleConcat(hv_min1.TupleMax());
                    hv_xjy = hv_xjy.TupleConcat(hv_max1.TupleMin());
                    HOperatorSet.TupleFind(hv_d, hv_min1.TupleMin(), out hv_Indices1);
                    HOperatorSet.TupleFind(hv_d, hv_max1.TupleMax(), out hv_Indices2);
                    ho_RegionLines.Dispose();
                    HOperatorSet.GenRegionLine(out ho_RegionLines, hv_r6.TupleSelect(hv_Indices1),
                        hv_c6.TupleSelect(hv_Indices1), hv_r6.TupleSelect(hv_Indices2), hv_c6.TupleSelect(
                        hv_Indices2));
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.ConcatObj(ho_EmptyObject, ho_RegionLines, out ExpTmpOutVar_0);
                        ho_EmptyObject.Dispose();
                        ho_EmptyObject = ExpTmpOutVar_0;
                    }
                }
                hv_djpj = (hv_djy.TupleMean()) - (hv_djj.TupleMean());
                hv_djzd = (hv_djy.TupleMax()) - (hv_djj.TupleMin());
                hv_djzx = (hv_djy.TupleMin()) - (hv_djj.TupleMax());
                hv_xjpj = (hv_xjy.TupleMean()) - (hv_xjj.TupleMean());
                hv_xjzd = (hv_xjy.TupleMax()) - (hv_xjj.TupleMin());
                hv_xjzx = (hv_xjy.TupleMin()) - (hv_xjj.TupleMax());
                HOperatorSet.DistancePl(hv_Row2, hv_Column2, hv_cdr1, hv_cdc1, hv_cdr2, hv_cdc2,
                    out hv_Distance1);
                hv_lwts = hv_Index.Clone();
                hv_lwcd = (hv_Distance1.TupleMax()) - (hv_Distance1.TupleMin());
                hv_luoju = hv_lwcd / (hv_lwts - 1);
                HOperatorSet.Union1(ho_EmptyObject,out RegionToDisp);
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("螺纹条数1");
                hv_result = hv_result.TupleConcat(hv_lwts.D);
                hv_result = hv_result.TupleConcat("大径最大值");
                hv_result = hv_result.TupleConcat(hv_djzd.D * pixeldist);
                hv_result = hv_result.TupleConcat("大径最小值");
                hv_result = hv_result.TupleConcat(hv_djzx.D * pixeldist);
                hv_result = hv_result.TupleConcat("大径平均值");
                hv_result = hv_result.TupleConcat(hv_djpj.D * pixeldist);
                hv_result = hv_result.TupleConcat("小径最大值");
                hv_result = hv_result.TupleConcat(hv_xjzd.D * pixeldist);
                hv_result = hv_result.TupleConcat("小径最小值");
                hv_result = hv_result.TupleConcat(hv_xjzx.D * pixeldist);
                hv_result = hv_result.TupleConcat("小径平均值");
                hv_result = hv_result.TupleConcat(hv_xjpj.D * pixeldist);
                hv_result = hv_result.TupleConcat("螺距");
                hv_result = hv_result.TupleConcat(hv_luoju.D * pixeldist);
                hv_result = hv_result.TupleConcat("螺纹长度");
                hv_result = hv_result.TupleConcat(hv_lwcd.D * pixeldist);
                result = hv_result.Clone();

                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Rectangle1.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_ObjectSelected1.Dispose();
                ho_Contours.Dispose();
                ho_ContoursSplit.Dispose();
                ho_SelectedContours.Dispose();
                ho_UnionContours.Dispose();
                ho_ObjectSelected.Dispose();
                ho_Region1.Dispose();
                ho_EmptyObject.Dispose();
                ho_Rectangle3.Dispose();
                ho_ImageReduced2.Dispose();
                ho_Edges.Dispose();
                ho_UnionContours1.Dispose();
                ho_ObjectSelected2.Dispose();
                ho_RegionLines.Dispose();
                algorithm.Region.Dispose();
                //t4 = DateTime.Now;
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("螺纹条数1");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("大径最大值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("大径最小值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("大径平均值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("小径最大值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("小径最小值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("小径平均值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("螺距");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("螺纹长度");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();
                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Rectangle1.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_ObjectSelected1.Dispose();
                ho_Contours.Dispose();
                ho_ContoursSplit.Dispose();
                ho_SelectedContours.Dispose();
                ho_UnionContours.Dispose();
                ho_ObjectSelected.Dispose();
                ho_Region1.Dispose();
                ho_EmptyObject.Dispose();
                ho_Rectangle3.Dispose();
                ho_ImageReduced2.Dispose();
                ho_Edges.Dispose();
                ho_UnionContours1.Dispose();
                ho_ObjectSelected2.Dispose();
                ho_RegionLines.Dispose();
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