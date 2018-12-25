using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Windows.Forms;
using System.Xml.Serialization;
namespace CameraDetectSystem
{
     [Serializable]
   public class OutCircletool : ImageTools
   {
        #region ROI
         [NonSerialized]
        private HTuple centerRow = new HTuple();
         [NonSerialized]
        private HTuple centerColumn = new HTuple();
         [NonSerialized]
        private HTuple radius = new HTuple();

        public double DCenterRow { set; get; }
        public double DCenterColumn { set; get; }
        public double DRadius { set; get; }
        #endregion
        public OutCircletool()
        {
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public OutCircletool(HObject Image, Algorithm al)
        { 
            Initial(); 
            this.Image = Image;
            this.algorithm.Image = Image;
            this.algorithm = al;
            pixeldist = 1;
        }
        
        private void Initial()
        {
            centerRow = null;
            centerColumn = null;
            radius = null;
            centerRow = new HTuple();
            centerColumn = new HTuple();
            radius = new HTuple();

            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }

        public override void draw()
        {
            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            HOperatorSet.DrawCircle(this.LWindowHandle, out centerRow, out centerColumn, out radius);
            this.DCenterRow = centerRow.D;
            this.DCenterColumn = centerColumn.D;
            this.DRadius = radius.D;
        }
        public void region_outer_circle(HObject ho_SelectedRegions, out HObject ho_outCircle,
            out HTuple hv_outer_Row, out HTuple hv_outer_Column, out HTuple hv_outer_Radius,
            out HTuple hv_Circularity)
        {
            // Local iconic variables 

            HObject ho_RegionFillUp;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_outCircle);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            try
            {
                ho_RegionFillUp.Dispose();
                HTuple Num=new HTuple();
                HOperatorSet.CountObj(ho_SelectedRegions, out Num);
                if (Num.I == 0)
                {
                    //MessageBox.Show("请先二值化图像");
                    hv_outer_Row = new HTuple();
                    hv_outer_Column = new HTuple();
                    hv_outer_Radius = new HTuple();
                    hv_Circularity =  new  HTuple();

                    return;
                }
                HOperatorSet.FillUp(ho_SelectedRegions, out ho_RegionFillUp);
                ho_outCircle.Dispose();
                HOperatorSet.ShapeTrans(ho_RegionFillUp, out ho_outCircle, "outer_circle");
                HOperatorSet.SmallestCircle(ho_outCircle, out hv_outer_Row, out hv_outer_Column,
                    out hv_outer_Radius);
                HOperatorSet.Circularity(ho_RegionFillUp, out hv_Circularity);
                HTuple dist=new HTuple(),sigma=new HTuple(),roundness=new HTuple(),sides=new HTuple();

                HOperatorSet.Roundness(ho_RegionFillUp, out dist, out sigma, out roundness, out sides);
                if (hv_outer_Row.Length > 1)
                {
                    HOperatorSet.SelectShape(ho_outCircle, out ho_outCircle, "inner_radius", "and", hv_outer_Radius.TupleMax() - 1, hv_outer_Radius.TupleMax() + 1);
                    hv_outer_Radius = hv_outer_Radius.TupleMax();
                    HTuple index = new HTuple();
                    index=hv_outer_Radius.TupleFind(hv_outer_Radius.TupleMax());
                    hv_outer_Row = hv_outer_Row[index];
                    hv_outer_Column = hv_outer_Column[index];
                    hv_Circularity = hv_Circularity[index];
                }
                ho_RegionFillUp.Dispose();
                
                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_RegionFillUp.Dispose();

                throw HDevExpDefaultException;
            }
        }

        public void region_locate(HObject ho_Image, out HObject ho_SelectedRegions, HTuple hv_threshold_value,
            HTuple hv_blackOrwhite, HTuple hv_select_method, HTuple hv_select_andOror, HTuple hv_select_method_min_value,
            HTuple hv_select_method_max_value)
        {
            // Local iconic variables 

            HObject ho_Region = null, ho_ConnectedRegions;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);

            try
            {
                if ((int)(new HTuple(hv_blackOrwhite.TupleEqual("black"))) != 0)
                {
                    ho_Region.Dispose();
                    HOperatorSet.Threshold(ho_Image, out ho_Region, 0, hv_threshold_value);
                }
                else if ((int)(new HTuple(hv_blackOrwhite.TupleEqual("white"))) != 0)
                {
                    ho_Region.Dispose();
                    HOperatorSet.Threshold(ho_Image, out ho_Region, 128, hv_threshold_value);
                }
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, hv_select_method,
                    hv_select_andOror, hv_select_method_min_value, hv_select_method_max_value);
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                throw HDevExpDefaultException;
            }
        }

        private void action()
        {
            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];
            long SP_O = 0;
            // Local iconic variables 
            HObject ho_Circle,region;
            HObject ho_SelectedRegions, ho_outCircle;
            // Local control variables 

            //HTuple hv_blackOrwhite, hv_threshold_value;
            //HTuple hv_method, hv_andOror, hv_method_min_value, hv_method_max_value;
            HTuple hv_outer_Row, hv_outer_Column;
            HTuple hv_outer_Radius, hv_Circularity;
            HTuple hv_Number = new HTuple(), hv_result = new HTuple();
            // Initialize local and output iconic variables 
            //HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_outCircle);
            HOperatorSet.GenEmptyObj(out region);
            try
            {
                //HOperatorSet.SetColor(this.LWindowHandle, "green");
                //    out hv_Radius);
                ho_Circle.Dispose();
                HOperatorSet.GenCircle(out ho_Circle, DCenterRow, DCenterColumn, DRadius);
                HOperatorSet.Intersection(ho_Circle, algorithm.Region, out region);
                region_outer_circle(region, out ho_outCircle, out hv_outer_Row,
                    out hv_outer_Column, out hv_outer_Radius, out hv_Circularity);

                //HOperatorSet.DispObj(ho_outCircle, this.LWindowHandle);
                RegionToDisp.Dispose();
                if (!RegionToDisp.IsInitialized())
                {
                    HOperatorSet.CopyObj(ho_outCircle, out RegionToDisp, 1, 1);
                }
                else
                {
                    HOperatorSet.ConcatObj(ho_outCircle, RegionToDisp, out RegionToDisp);
                }
                //RegionToDisp
                HOperatorSet.CountObj(ho_outCircle, out hv_Number);
                if ((int)(new HTuple(hv_Number.TupleEqual(1))) != 0)
                {
                    hv_result = hv_result.TupleConcat("center_row");
                    hv_result = hv_result.TupleConcat(hv_outer_Row*pixeldist);
                    hv_result = hv_result.TupleConcat("center_column");
                    hv_result = hv_result.TupleConcat(hv_outer_Column * pixeldist);
                    hv_result = hv_result.TupleConcat("diameter");
                    hv_result = hv_result.TupleConcat(2*hv_outer_Radius * pixeldist);
                    hv_result = hv_result.TupleConcat("circularity");
                    hv_result = hv_result.TupleConcat(hv_Circularity);
                    result = hv_result.Clone();
                }
                else
                {
                    hv_result = hv_result.TupleConcat("center_row");
                    hv_result = hv_result.TupleConcat(0);
                    hv_result = hv_result.TupleConcat("center_column");
                    hv_result = hv_result.TupleConcat(0);
                    hv_result = hv_result.TupleConcat("diameter");
                    hv_result = hv_result.TupleConcat(0);
                    hv_result = hv_result.TupleConcat("circularity");
                    hv_result = hv_result.TupleConcat(0);
                    result = hv_result.Clone();
                }
            }
            catch (HalconException HDevExpDefaultException)
            {
                this.Result = new HTuple();
                MyDebug.ShowMessage("CircleToolsError:=" + HDevExpDefaultException);
                hv_result = hv_result.TupleConcat("center_row");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("center_column");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("diameter");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("circularity");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();
            }
            finally
            {
                ho_Circle.Dispose();
                region.Dispose();
                ho_SelectedRegions.Dispose();
                ho_outCircle.Dispose();
                algorithm.Region.Dispose();
            }

        }
        
        public override  bool method()
        {
            if (base.method())
            {
                action();
                return true;
            }
            else
            {
            return true;
            }
        }
    }
}
