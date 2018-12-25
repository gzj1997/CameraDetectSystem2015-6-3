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
    class CCD2 : ImageTools
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

        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public CCD2()
        {
            RegionToDisp = Image;
        }
        public CCD2(HObject Image, Algorithm al)
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
             dRow=413.824;dColumn=1102.5;
             dPhi = -0.952709; dLength1 =9.38345; dLength2 =10.7386;
            this.hv_Row = dRow.D;
            this.hv_Column = dColumn.D;
            this.hv_Phi = dPhi.D;
            this.hv_Length1 = dLength1.D;
            this.hv_Length2 = dLength2.D;
            this.hv1_Row = ddRow.D;
            this.hv1_Column = ddColumn.D;
            this.hv1_Phi = ddPhi.D;
            this.hv1_Length1 = ddLength1.D;
            this.hv1_Length2 = ddLength2.D;
            HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            thv = thresholdValue.D;
            ho_Rectangle.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangle, hv_Row, hv_Column, hv_Phi, hv_Length1,
                hv_Length2);
            ho_ImageReduced.Dispose();
            HOperatorSet.ReduceDomain(Image, ho_Rectangle, out ho_ImageReduced);
            ho_Border.Dispose();
            HOperatorSet.ThresholdSubPix(ho_ImageReduced, out ho_Border, thv);
            HOperatorSet.CreateShapeModelXld(ho_Border, "auto", -3.14, 6.29, "auto", "auto", "ignore_local_polarity", 5, out hv_ModelID);
            HOperatorSet.WriteShapeModel(hv_ModelID, PathHelper.currentProductPath + @"\lwspmd.shm");
            ho_Rectangle.Dispose();
            ho_ImageReduced.Dispose();
            ho_Border.Dispose();
        }
        //DateTime t1, t2, t3, t4,t5,t6,t7;
        private void action()
        {
            HObject ho_Rectangle, ho_ImageReduced, imagexz, retxz;
            HObject ho_Border, ho_Circle, ho_ImageReduced2, ho_Region, ho_RegionLines;
            HObject ho_Rectangle1 = null, ho_ImageReduced1 = null, ho_Border1 = null;
            HObject ho_SelectedContours = null;

            // Local control variables 

            HTuple hv_Row1 = null, hv_Column1 = null, hv_Angle = null, hv_l1 = null, hv_l2 = null;
            HTuple hv_Score = null, hv_zj = null, hv_Area = null, hv_Row3 = null;
            HTuple hv_Column2 = null, hv_Phi1 = null, hv_zpj = null;
            HTuple hv_zcj = null, hv_zpr = null, hv_zpc = null, hv_zcr = null;
            HTuple hv_zcc = null, hv_Tp = null, hv_Tc = null, hv_Length = null;
            HTuple hv_dx = null, hv_dn = null, hv_dxr = null, hv_dxc = null;
            HTuple hv_dnr = null, hv_dnc = null, hv_i = null;
            //HTuple hv_Col = new HTuple(), hv_dis = new HTuple(), hv_dxi = new HTuple(), hv_Row2 = new HTuple();
            //HTuple hv_dni = new HTuple(), hv_Ix = new HTuple(), hv_In = new HTuple();
            //HTuple hv_Exception = new HTuple(), hv_j = new HTuple(), hv_jh = new HTuple();
            HTuple hv_disdxpx = null;
            HTuple hv_disdnpx = null;
            HTuple hv_djd = null, hv_djdr = null, hv_djdc = null, hv_djx = null;
            HTuple hv_djxr = null, hv_djxc = null, hv_xjd = null, hv_xjdr = null;
            HTuple hv_xjdc = null, hv_xjx = null, hv_xjxr = null, hv_xjxc = null;
            HTuple hv_ljd = null, hv_ljx = null, hv_dj = null, hv_xj = null;
            HTuple hv_djn = null, hv_djm = null, hv_xjn = null, hv_xjm = null;
            HTuple hv_djzd = null, hv_djzx = null, hv_xjzd = null;
            HTuple hv_xjzx = null, hv_ljtd = null, hv_ljtx = null;
            HTuple hv_Length3 = null, hv_Length4 = null, hv_ljdx = null;
            HTuple hv_ljdn = null, hv_ljxx = null, hv_ljxn = null;
            HTuple hv_ljdm = null, hv_ljxm = null, hv_djiaodu = null;
            HTuple hv_xjiaodu = null, hv_Length5 = null, hv_Length6 = null;
            HTuple hv_djiaodu1 = null, hv_xjiaodu1 = null, hv_jjdd = null;
            HTuple hv_jjdx = null, hv_jjxd = null, hv_jjxx = null;
            HTuple hv_jjdm = null, hv_jjxm = null;
            // Initialize local and output iconic variables 

            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Border);
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced2);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
            HOperatorSet.GenEmptyObj(out ho_Border1);
            HOperatorSet.GenEmptyObj(out ho_SelectedContours);
            HOperatorSet.GenEmptyObj(out ho_RegionLines);
            HOperatorSet.GenEmptyObj(out imagexz);
            HOperatorSet.GenEmptyObj(out retxz);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            //t3 = DateTime.Now;
            try
            {
                if (hv_ModelID == null)
                {
                    HOperatorSet.ReadShapeModel(PathHelper.currentProductPath + @"\lwspmd.shm", out hv_ModelID);
                }
                retxz.Dispose();
                HOperatorSet.GenRectangle2(out retxz, hv1_Row, hv1_Column, hv1_Phi, hv1_Length1, hv1_Length2);
                imagexz.Dispose();
                HOperatorSet.ReduceDomain(Image, retxz, out imagexz);
                HOperatorSet.FindShapeModel(imagexz, hv_ModelID, -3.14, 6.29,0.7, 0, 0.1,
                    "least_squares", 0, 0.8, out hv_Row1, out hv_Column1, out hv_Angle, out hv_Score);
                hv_zj = (((((hv_Row1.TupleMax()) - (hv_Row1.TupleMin())) * ((hv_Row1.TupleMax()
                    ) - (hv_Row1.TupleMin()))) + (((hv_Column1.TupleMax()) - (hv_Column1.TupleMin()
                    )) * ((hv_Column1.TupleMax()) - (hv_Column1.TupleMin()))))).TupleSqrt();
                ho_Circle.Dispose();
                HOperatorSet.GenCircle(out ho_Circle, hv_Row1.TupleMean(), hv_Column1.TupleMean()
                    , hv_zj / 2);
                ho_ImageReduced2.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Circle, out ho_ImageReduced2);
                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced2, out ho_Region, 0, thv);
                HOperatorSet.AreaCenter(ho_Region, out hv_Area, out hv_Row3, out hv_Column2);
                HOperatorSet.OrientationRegion(ho_Region, out hv_Phi1);
                hv_zpj = hv_Phi1.Clone();
                hv_zcj = hv_Phi1 + ((new HTuple(90)).TupleRad());
                hv_zpr = hv_Row3 + ((hv_zcj.TupleSin()) * hv_zj);
                hv_zpc = hv_Column2 - ((hv_zcj.TupleCos()) * hv_zj);
                hv_zcr = hv_Row3 + ((hv_zpj.TupleSin()) * hv_zj);
                hv_zcc = hv_Row3 - ((hv_zpj.TupleCos()) * hv_zj);
                HOperatorSet.TupleTan(hv_zpj, out hv_Tp);
                HOperatorSet.TupleTan(hv_zcj, out hv_Tc);
                HOperatorSet.TupleLength(hv_Row1, out hv_Length);
                hv_dx = new HTuple();
                hv_dn = new HTuple();
                hv_dxr = new HTuple();
                hv_dxc = new HTuple();
                hv_dnr = new HTuple();
                hv_dnc = new HTuple();
                HTuple end_val29 = hv_Length - 1;
                HTuple step_val29 = 1;
                for (hv_i = 0; hv_i.Continue(end_val29, step_val29); hv_i = hv_i.TupleAdd(step_val29))
                {
                    //try


                    ho_Rectangle1.Dispose();
                    HOperatorSet.GenRectangle2(out ho_Rectangle1, hv_Row1.TupleSelect(hv_i),
                        hv_Column1.TupleSelect(hv_i), hv_Phi + (hv_Angle.TupleSelect(hv_i)),
                        hv_Length1, hv_Length2);
                    ho_ImageReduced1.Dispose();
                    HOperatorSet.ReduceDomain(Image, ho_Rectangle1, out ho_ImageReduced1
                        );
                    ho_Border1.Dispose();
                    HOperatorSet.ThresholdSubPix(ho_ImageReduced1, out ho_Border1, thv);
                    ho_SelectedContours.Dispose();
                    HOperatorSet.SelectContoursXld(ho_Border1, out ho_SelectedContours, "contour_length",
                        30, 2000000, -0.5, 0.5);
                    HOperatorSet.UnionAdjacentContoursXld(ho_SelectedContours, out ho_SelectedContours, 10, 1, "attr_keep");
                    HOperatorSet.GetContourXld(ho_SelectedContours, out hv_Row2, out hv_Col);
                    hv_dis = ((((((hv_Tp * hv_Col) + hv_Row2) - (hv_Tp * hv_zpc)) - hv_zpr) / (((1 + (hv_Tp * hv_Tp))).TupleSqrt()
                        ))).TupleAbs();
                    hv_dxi = hv_dis.TupleMax();
                    hv_dni = hv_dis.TupleMin();
                    HOperatorSet.TupleFind(hv_dis, hv_dxi, out hv_Ix);
                    HOperatorSet.TupleFind(hv_dis, hv_dni, out hv_In);
                    hv_dx = hv_dx.TupleConcat(hv_dxi);
                    hv_dn = hv_dn.TupleConcat(hv_dni);
                    hv_dxr = hv_dxr.TupleConcat(hv_Row2.TupleSelect(hv_Ix));
                    hv_dxc = hv_dxc.TupleConcat(hv_Col.TupleSelect(hv_Ix));
                    hv_dnr = hv_dnr.TupleConcat(hv_Row2.TupleSelect(hv_In));
                    hv_dnc = hv_dnc.TupleConcat(hv_Col.TupleSelect(hv_In));
                    //catch (Exception)
                    //endtry
                    //stop ()
                }
                hv_disdxpx = ((((((hv_Tc * hv_dxc) + hv_dxr) - (hv_Tc * hv_zcc)) - hv_zcr) / (((1 + (hv_Tc * hv_Tc))).TupleSqrt()
                    ))).TupleAbs();
                hv_disdnpx = ((((((hv_Tc * hv_dnc) + hv_dnr) - (hv_Tc * hv_zcc)) - hv_zcr) / (((1 + (hv_Tc * hv_Tc))).TupleSqrt()
                    ))).TupleAbs();
                HTuple end_val55 = hv_Length - 2;
                HTuple step_val55 = 1;
                //t1 = DateTime.Now;
                for (hv_i = 0; hv_i.Continue(end_val55, step_val55); hv_i = hv_i.TupleAdd(step_val55))
                {
                    //try


                    HTuple end_val59 = hv_Length - 1;
                    HTuple step_val59 = 1;
                    for (hv_j = hv_i + 1; hv_j.Continue(end_val59, step_val59); hv_j = hv_j.TupleAdd(step_val59))
                    {
                        if ((int)(new HTuple(((hv_disdxpx.TupleSelect(hv_i))).TupleGreater(hv_disdxpx.TupleSelect(
                            hv_j)))) != 0)
                        {
                            hv_jh = hv_disdxpx.TupleSelect(hv_i);
                            if (hv_disdxpx == null)
                                hv_disdxpx = new HTuple();
                            hv_disdxpx[hv_i] = hv_disdxpx.TupleSelect(hv_j);
                            if (hv_disdxpx == null)
                                hv_disdxpx = new HTuple();
                            hv_disdxpx[hv_j] = hv_jh;
                            hv_jh = hv_dx.TupleSelect(hv_i);
                            if (hv_dx == null)
                                hv_dx = new HTuple();
                            hv_dx[hv_i] = hv_dx.TupleSelect(hv_j);
                            if (hv_dx == null)
                                hv_dx = new HTuple();
                            hv_dx[hv_j] = hv_jh;
                            hv_jh = hv_dxr.TupleSelect(hv_i);
                            if (hv_dxr == null)
                                hv_dxr = new HTuple();
                            hv_dxr[hv_i] = hv_dxr.TupleSelect(hv_j);
                            if (hv_dxr == null)
                                hv_dxr = new HTuple();
                            hv_dxr[hv_j] = hv_jh;
                            hv_jh = hv_dxc.TupleSelect(hv_i);
                            if (hv_dxc == null)
                                hv_dxc = new HTuple();
                            hv_dxc[hv_i] = hv_dxc.TupleSelect(hv_j);
                            if (hv_dxc == null)
                                hv_dxc = new HTuple();
                            hv_dxc[hv_j] = hv_jh;
                        }
                        if ((int)(new HTuple(((hv_disdnpx.TupleSelect(hv_i))).TupleGreater(hv_disdnpx.TupleSelect(
                            hv_j)))) != 0)
                        {
                            hv_jh = hv_disdnpx.TupleSelect(hv_i);
                            if (hv_disdnpx == null)
                                hv_disdnpx = new HTuple();
                            hv_disdnpx[hv_i] = hv_disdnpx.TupleSelect(hv_j);
                            if (hv_disdnpx == null)
                                hv_disdnpx = new HTuple();
                            hv_disdnpx[hv_j] = hv_jh;
                            hv_jh = hv_dn.TupleSelect(hv_i);
                            if (hv_dn == null)
                                hv_dn = new HTuple();
                            hv_dn[hv_i] = hv_dn.TupleSelect(hv_j);
                            if (hv_dn == null)
                                hv_dn = new HTuple();
                            hv_dn[hv_j] = hv_jh;
                            hv_jh = hv_dnr.TupleSelect(hv_i);
                            if (hv_dnr == null)
                                hv_dnr = new HTuple();
                            hv_dnr[hv_i] = hv_dnr.TupleSelect(hv_j);
                            if (hv_dnr == null)
                                hv_dnr = new HTuple();
                            hv_dnr[hv_j] = hv_jh;
                            hv_jh = hv_dnc.TupleSelect(hv_i);
                            if (hv_dnc == null)
                                hv_dnc = new HTuple();
                            hv_dnc[hv_i] = hv_dnc.TupleSelect(hv_j);
                            if (hv_dnc == null)
                                hv_dnc = new HTuple();
                            hv_dnc[hv_j] = hv_jh;
                        }

                    }
                    //catch (Exception)
                    //endtry
                }
                //t2 = DateTime.Now;
                hv_djd = new HTuple();
                hv_djdr = new HTuple();
                hv_djdc = new HTuple();
                hv_djx = new HTuple();
                hv_djxr = new HTuple();
                hv_djxc = new HTuple();
                hv_xjd = new HTuple();
                hv_xjdr = new HTuple();
                hv_xjdc = new HTuple();
                hv_xjx = new HTuple();
                hv_xjxr = new HTuple();
                hv_xjxc = new HTuple();
                hv_ljd = new HTuple();
                hv_ljx = new HTuple();
                HTuple end_val107 = hv_Length - 1;
                HTuple step_val107 = 1;
                for (hv_i = 0; hv_i.Continue(end_val107, step_val107); hv_i = hv_i.TupleAdd(step_val107))
                {

                    if ((int)(new HTuple(((hv_dx.TupleSelect(hv_i))).TupleGreater(hv_zj))) != 0)
                    {
                        for (hv_j = -3; (int)hv_j <= 3; hv_j = (int)hv_j + 1)
                        {
                            if (((hv_i + hv_j) >= 0) && ((hv_i + hv_j) <= end_val107))
                            {
                                if ((int)((new HTuple(((hv_disdnpx.TupleSelect(hv_i + hv_j))).TupleGreater(
                                    hv_disdxpx.TupleSelect(hv_i)))).TupleAnd(new HTuple(((hv_dn.TupleSelect(
                                    hv_i + hv_j))).TupleLess(hv_zj)))) != 0)
                                {
                                    hv_ljd = hv_ljd.TupleConcat(hv_disdxpx.TupleSelect(hv_i));
                                    hv_djd = hv_djd.TupleConcat(hv_dx.TupleSelect(hv_i));
                                    hv_djdr = hv_djdr.TupleConcat(hv_dxr.TupleSelect(hv_i));
                                    hv_djdc = hv_djdc.TupleConcat(hv_dxc.TupleSelect(hv_i));
                                    hv_djx = hv_djx.TupleConcat(hv_dn.TupleSelect(hv_i + hv_j));
                                    hv_djxr = hv_djxr.TupleConcat(hv_dnr.TupleSelect(hv_i + hv_j));
                                    hv_djxc = hv_djxc.TupleConcat(hv_dnc.TupleSelect(hv_i + hv_j));
                                    break;
                                }
                            }
                            // catch (Exception) 
                            //catch (HalconException)
                            //{
                            //    //HDevExpDefaultException1.ToHTuple(out hv_Exception);
                            //}
                        }
                    }

                    if ((int)(new HTuple(((hv_dn.TupleSelect(hv_i))).TupleGreater(hv_zj))) != 0)
                    {
                        for (hv_j = -3; (int)hv_j <= 3; hv_j = (int)hv_j + 1)
                        {
                            //try
                            if (((hv_i + hv_j) >= 0) && ((hv_i + hv_j) <= end_val107))
                            {
                                if ((int)((new HTuple(((hv_disdxpx.TupleSelect(hv_i + hv_j))).TupleGreater(
                                    hv_disdnpx.TupleSelect(hv_i)))).TupleAnd(new HTuple(((hv_dx.TupleSelect(
                                    hv_i + hv_j))).TupleLess(hv_zj)))) != 0)
                                {
                                    hv_ljx = hv_ljx.TupleConcat(hv_disdnpx.TupleSelect(hv_i));
                                    hv_xjd = hv_xjd.TupleConcat(hv_dn.TupleSelect(hv_i));
                                    hv_xjdr = hv_xjdr.TupleConcat(hv_dnr.TupleSelect(hv_i));
                                    hv_xjdc = hv_xjdc.TupleConcat(hv_dnc.TupleSelect(hv_i));
                                    hv_xjx = hv_xjx.TupleConcat(hv_dx.TupleSelect(hv_i + hv_j));
                                    hv_xjxr = hv_xjxr.TupleConcat(hv_dxr.TupleSelect(hv_i + hv_j));
                                    hv_xjxc = hv_xjxc.TupleConcat(hv_dxc.TupleSelect(hv_i + hv_j));
                                    break;
                                }

                            }
                            // catch (Exception) 
                            //catch (HalconException)
                            //{
                            //    //HDevExpDefaultException1.ToHTuple(out hv_Exception);
                            //}
                        }
                    }
                }
                HOperatorSet.TupleLength(hv_ljd, out hv_l1);
                HTuple end_val153 = 1;
                HTuple step_val153 = -1;
                for (hv_i = hv_l1 - 1; hv_i.Continue(end_val153, step_val153); hv_i = hv_i.TupleAdd(step_val153))
                {
                    if ((int)(new HTuple(((hv_ljd.TupleSelect(hv_i))).TupleEqual(hv_ljd.TupleSelect(
                        hv_i - 1)))) != 0)
                    {
                        HOperatorSet.TupleRemove(hv_ljd, hv_i, out hv_ljd);
                        HOperatorSet.TupleRemove(hv_djd, hv_i, out hv_djd);
                        HOperatorSet.TupleRemove(hv_djdr, hv_i, out hv_djdr);
                        HOperatorSet.TupleRemove(hv_djdc, hv_i, out hv_djdc);
                        HOperatorSet.TupleRemove(hv_djx, hv_i, out hv_djx);
                        HOperatorSet.TupleRemove(hv_djxr, hv_i, out hv_djxr);
                        HOperatorSet.TupleRemove(hv_djxc, hv_i, out hv_djxc);
                    }
                }
                HOperatorSet.TupleLength(hv_ljx, out hv_l2);
                HTuple end_val165 = 1;
                HTuple step_val165 = -1;
                for (hv_i = hv_l2 - 1; hv_i.Continue(end_val165, step_val165); hv_i = hv_i.TupleAdd(step_val165))
                {
                    if ((int)(new HTuple(((hv_ljd.TupleSelect(hv_i))).TupleEqual(hv_ljd.TupleSelect(
                        hv_i - 1)))) != 0)
                    {
                        HOperatorSet.TupleRemove(hv_ljx, hv_i, out hv_ljx);
                        HOperatorSet.TupleRemove(hv_xjd, hv_i, out hv_xjd);
                        HOperatorSet.TupleRemove(hv_xjdr, hv_i, out hv_xjdr);
                        HOperatorSet.TupleRemove(hv_xjdc, hv_i, out hv_xjdc);
                        HOperatorSet.TupleRemove(hv_xjx, hv_i, out hv_xjx);
                        HOperatorSet.TupleRemove(hv_xjxr, hv_i, out hv_xjxr);
                        HOperatorSet.TupleRemove(hv_xjxc, hv_i, out hv_xjxc);
                    }
                }
                HOperatorSet.TupleLength(hv_ljx, out hv_l2);
                HOperatorSet.TupleLength(hv_ljd, out hv_l1);
                //t5 = DateTime.Now;
                hv_dj = hv_djd - hv_djx;
                hv_xj = hv_xjd - hv_xjx;
                hv_djx = hv_dj.TupleMax();
                hv_djn = hv_dj.TupleMin();
                hv_djm = hv_dj.TupleMean();
                hv_xjx = hv_xj.TupleMax();
                hv_xjn = hv_xj.TupleMin();
                hv_xjm = hv_xj.TupleMean();
                hv_djzd = (hv_djd.TupleMax()) - (hv_djx.TupleMin());
                hv_djzx = (hv_djd.TupleMin()) - (hv_djx.TupleMax());
                hv_xjzd = (hv_xjd.TupleMax()) - (hv_xjx.TupleMin());
                hv_xjzx = (hv_xjd.TupleMin()) - (hv_xjx.TupleMax());
                hv_ljtd = (hv_ljd.TupleConcat(0)) - ((new HTuple(0)).TupleConcat(hv_ljd));
                hv_ljtx = (hv_ljx.TupleConcat(0)) - ((new HTuple(0)).TupleConcat(hv_ljx));
                HOperatorSet.TupleLength(hv_ljtd, out hv_Length3);
                HOperatorSet.TupleLength(hv_ljtx, out hv_Length4);
                hv_ljtd = hv_ljtd.TupleSelectRange(1, hv_Length3 - 2);
                hv_ljtx = hv_ljtx.TupleSelectRange(1, hv_Length4 - 2);
                hv_ljdx = hv_ljtd.TupleMax();
                hv_ljdn = hv_ljtd.TupleMin();
                hv_ljxx = hv_ljtx.TupleMax();
                hv_ljxn = hv_ljtx.TupleMin();
                hv_ljdm = hv_ljtd.TupleMean();
                hv_ljxm = hv_ljtx.TupleMean();
                HOperatorSet.TupleAtan((hv_djxr - hv_djdr) / (hv_djdc - hv_djxc), out hv_djiaodu);
                HOperatorSet.TupleAtan((hv_xjxr - hv_xjdr) / (hv_xjdc - hv_xjxc), out hv_xjiaodu);
                HOperatorSet.TupleLength(hv_djiaodu, out hv_Length5);
                HOperatorSet.TupleLength(hv_xjiaodu, out hv_Length6);
                hv_djiaodu1 = ((hv_djiaodu - hv_zcj)).TupleAbs();
                hv_xjiaodu1 = ((hv_xjiaodu - hv_zcj)).TupleAbs();
                HTuple end_val177 = hv_Length5 - 1;
                HTuple step_val177 = 1;
                //t6 = DateTime.Now;
                for (hv_i = 0; hv_i.Continue(end_val177, step_val177); hv_i = hv_i.TupleAdd(step_val177))
                {
                    while ((int)(new HTuple(hv_djiaodu1.TupleGreater((new HTuple(90)).TupleRad()
                        ))) != 0)
                    {
                        hv_djiaodu1 = hv_djiaodu1 - ((new HTuple(90)).TupleRad());
                    }
                    if ((int)(new HTuple(hv_djiaodu1.TupleGreater((new HTuple(45)).TupleRad()
                        ))) != 0)
                    {
                        hv_djiaodu1 = (-hv_djiaodu1) + ((new HTuple(90)).TupleRad());
                    }
                }
                HTuple end_val185 = hv_Length6 - 1;
                HTuple step_val185 = 1;
                for (hv_i = 0; hv_i.Continue(end_val185, step_val185); hv_i = hv_i.TupleAdd(step_val185))
                {
                    while ((int)(new HTuple(hv_xjiaodu1.TupleGreater((new HTuple(90)).TupleRad()
                        ))) != 0)
                    {
                        hv_xjiaodu1 = hv_xjiaodu1 - ((new HTuple(90)).TupleRad());
                    }
                    if ((int)(new HTuple(hv_xjiaodu1.TupleGreater((new HTuple(45)).TupleRad()
                        ))) != 0)
                    {
                        hv_xjiaodu1 = (-hv_xjiaodu1) + ((new HTuple(90)).TupleRad());
                    }
                }
                hv_jjdd = hv_djiaodu1.TupleMax().TupleDeg();
                hv_jjdx = hv_djiaodu1.TupleMin().TupleDeg();
                hv_jjxd = hv_xjiaodu1.TupleMax().TupleDeg();
                hv_jjxx = hv_xjiaodu1.TupleMin().TupleDeg();
                hv_jjdm = hv_djiaodu1.TupleMean().TupleDeg();
                hv_jjxm = hv_xjiaodu1.TupleMean().TupleDeg();
                ho_RegionLines.Dispose();
                HOperatorSet.GenRegionLine(out ho_RegionLines, hv_djdr, hv_djdc, hv_djxr, hv_djxc);
                HOperatorSet.Union2(ho_RegionLines, ho_RegionLines, out RegionToDisp);
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("螺纹条数1");
                hv_result = hv_result.TupleConcat(hv_l1);
                hv_result = hv_result.TupleConcat("螺纹条数2");
                hv_result = hv_result.TupleConcat(hv_l2);
                hv_result = hv_result.TupleConcat("大径最大值");
                hv_result = hv_result.TupleConcat(hv_djx.D * pixeldist);
                hv_result = hv_result.TupleConcat("大径最小值");
                hv_result = hv_result.TupleConcat(hv_djn.D * pixeldist);
                hv_result = hv_result.TupleConcat("大径平均值");
                hv_result = hv_result.TupleConcat(hv_djm.D * pixeldist);
                hv_result = hv_result.TupleConcat("小径最大值");
                hv_result = hv_result.TupleConcat(hv_xjx.D * pixeldist);
                hv_result = hv_result.TupleConcat("小径最小值");
                hv_result = hv_result.TupleConcat(hv_xjn.D * pixeldist);
                hv_result = hv_result.TupleConcat("小径平均值");
                hv_result = hv_result.TupleConcat(hv_xjm.D * pixeldist);
                hv_result = hv_result.TupleConcat("整体大径最大值");
                hv_result = hv_result.TupleConcat(hv_djzd.D * pixeldist);
                hv_result = hv_result.TupleConcat("整体大径最小值");
                hv_result = hv_result.TupleConcat(hv_djzx.D * pixeldist);
                hv_result = hv_result.TupleConcat("整体小径最大值");
                hv_result = hv_result.TupleConcat(hv_xjzd.D * pixeldist);
                hv_result = hv_result.TupleConcat("整体小径最小值");
                hv_result = hv_result.TupleConcat(hv_xjzx.D * pixeldist);
                hv_result = hv_result.TupleConcat("螺距最大值");
                hv_result = hv_result.TupleConcat(hv_ljdx.D * pixeldist);
                hv_result = hv_result.TupleConcat("螺距最小值");
                hv_result = hv_result.TupleConcat(hv_ljdn.D * pixeldist);
                hv_result = hv_result.TupleConcat("螺距平均值");
                hv_result = hv_result.TupleConcat(hv_ljdm.D * pixeldist);
                hv_result = hv_result.TupleConcat("角度最大值");
                hv_result = hv_result.TupleConcat(hv_jjdd.D);
                hv_result = hv_result.TupleConcat("角度最大值");
                hv_result = hv_result.TupleConcat(hv_jjdx.D);
                hv_result = hv_result.TupleConcat("角度平均值");
                hv_result = hv_result.TupleConcat(hv_jjdm.D);
                result = hv_result.Clone();

                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Border.Dispose();
                ho_Circle.Dispose();
                ho_ImageReduced2.Dispose();
                ho_Region.Dispose();
                ho_Rectangle1.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Border1.Dispose();
                ho_SelectedContours.Dispose();
                imagexz.Dispose();
                retxz.Dispose();
                ho_RegionLines.Dispose();
                algorithm.Region.Dispose();
                //t4 = DateTime.Now;
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("螺纹条数1");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("螺纹条数2");
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
                hv_result = hv_result.TupleConcat("整体大径最大值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("整体大径最小值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("整体小径最大值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("整体小径最小值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("螺距最大值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("螺距最小值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("螺距平均值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("角度最大值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("角度最大值");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("角度平均值");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();
                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Border.Dispose();
                ho_Circle.Dispose();
                ho_ImageReduced2.Dispose();
                ho_Region.Dispose();
                ho_Rectangle1.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Border1.Dispose();
                ho_SelectedContours.Dispose();
                imagexz.Dispose();
                retxz.Dispose();
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