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
    class mianjiceliang : ImageTools
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
        public mianjiceliang()
        {
            RegionToDisp = Image;
        }
        public mianjiceliang(HObject Image, Algorithm al)
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
            HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            thv = thresholdValue.D;
            this.hv_Radius = dPhi;
            this.hv_Row = dRow;
            this.hv_Column = dColumn;
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
            HObject ho_Circle = null, ho_ImageReduced = null;
            HObject ho_Region = null, ho_RegionErosion = null, ho_ConnectedRegions = null;

            // Local control variables 
            HTuple hv_Area = new HTuple();
            HTuple hv_Rows = new HTuple(), hv_Columns = new HTuple();
            HTuple hv_zmj = new HTuple(), hv_zdmj = new HTuple(), hv_gs = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_RegionErosion);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            //t3 = DateTime.Now;
            try
            {
                ho_Circle.Dispose();
                HOperatorSet.GenCircle(out ho_Circle, hv_Row, hv_Column, hv_Radius);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Circle, out ho_ImageReduced);
                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, thv, 255);
                ho_RegionErosion.Dispose();
                HOperatorSet.ErosionCircle(ho_Region, out ho_RegionErosion, 1.5);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_RegionErosion, out ho_ConnectedRegions);
                HOperatorSet.AreaCenter(ho_ConnectedRegions, out hv_Area, out hv_Rows, out hv_Columns);
                hv_zmj = hv_Area.TupleSum();
                hv_zdmj = hv_Area.TupleMax();
                hv_gs = new HTuple(hv_Area.TupleLength());
                HOperatorSet.Union1(ho_ConnectedRegions, out RegionToDisp
                    );
                if (hv_zmj.TupleLength()==0)
                {
                    HOperatorSet.Union1(ho_Circle, out RegionToDisp
                      );
                }
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("总面积");
                hv_result = hv_result.TupleConcat(hv_zmj.D);
                hv_result = hv_result.TupleConcat("最大面积");
                hv_result = hv_result.TupleConcat(hv_zdmj.D);
                hv_result = hv_result.TupleConcat("面积个数");
                hv_result = hv_result.TupleConcat(hv_gs.D);
                result = hv_result.Clone();

                //t4 = DateTime.Now;
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("总面积");
                hv_result = hv_result.TupleConcat(-1);
                hv_result = hv_result.TupleConcat("最大面积");
                hv_result = hv_result.TupleConcat(-1);
                hv_result = hv_result.TupleConcat("面积个数");
                hv_result = hv_result.TupleConcat(-1);
                result = hv_result.Clone();
                HOperatorSet.Union1(ho_Circle, out RegionToDisp
                      );


            }
            finally
            {
                ho_Circle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_RegionErosion.Dispose();
                ho_ConnectedRegions.Dispose();
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