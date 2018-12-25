using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Diagnostics;
namespace CameraDetectSystem
{
     [Serializable]
    public class myrake:ImageTools
    {
         public myrake()
         {
             HOperatorSet.GenEmptyObj(out RegionToDisp);
             RegionToDisp.Dispose();
         }
         public myrake(HObject Image, Algorithm al)
         {
             HOperatorSet.GenEmptyObj(out RegionToDisp);
             RegionToDisp.Dispose(); 
             this.Image = Image; this.algorithm = al; vOrh = "h";
             pixeldist = 1;
         }

         public string vOrh { set; get; }
         public string taijie { set; get;}
        public HTuple Length
        {
            get { return _length; }
            set { _length = value; }
        }
        [NonSerialized]
        HTuple _length;
        [NonSerialized]
        private HTuple Row1, Row2, Column1, Column2; 
        public double DRow1, DRow2, DColumn1, DColumn2;
        public override  void draw()
        {
            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            HOperatorSet.DrawRectangle1(this.LWindowHandle, out Row1, out Column1, out Row2, out Column2);
            DRow1 = Row1.D;
            DRow2 = Row2.D;
            DColumn1 = Column1.D;
            DColumn2=Column2.D;
        }
        public void my_clamp(HObject ho_Region, out HObject ho_Arrow, HTuple hv_vOrh,
     out HTuple hv_length)
        {
            // Local iconic variables 
            try
            {
                HObject ho_Arrow1 = null, ho_Arrow2 = null;
                // Local control variables 
                HTuple hv_Rows = new HTuple(), hv_Columns = new HTuple(), hv_maxc = new HTuple();
                HTuple hv_minc = new HTuple(), hv_index = new HTuple();

                // Initialize local and output iconic variables 
                HOperatorSet.GenEmptyObj(out ho_Arrow);
                HOperatorSet.GenEmptyObj(out ho_Arrow1);
                HOperatorSet.GenEmptyObj(out ho_Arrow2);

                hv_length = new HTuple();
                HTuple Num = new HTuple();
                HOperatorSet.CountObj(ho_Region, out Num);
                if (Num.I != 1)
                {
                    //MessageBox.Show("将图像分割为一种颜色");
                    Debug.Print("将图像分割为一种颜色");
                    return;
                }
                HOperatorSet.GetRegionPoints(ho_Region, out hv_Rows, out hv_Columns);
                if ((int)(new HTuple(hv_vOrh.TupleEqual("h"))) != 0&&hv_Rows.Length>0)
                {
                    hv_maxc = hv_Columns.TupleMax();
                    hv_minc = hv_Columns.TupleMin();
                    hv_length = hv_maxc - hv_minc;
                    HOperatorSet.TupleFind(hv_Columns, hv_maxc, out hv_index);

                    ho_Arrow1.Dispose();
                    HalconHelp.gen_arrow_contour_xld(out ho_Arrow1, hv_Rows.TupleSelect(hv_index.TupleSelect(
                        0)), (hv_Columns.TupleSelect(hv_index.TupleSelect(0))) + 50, hv_Rows.TupleSelect(
                        hv_index.TupleSelect(0)), hv_Columns.TupleSelect(hv_index.TupleSelect(0)),
                        15, 15);
                    HOperatorSet.TupleFind(hv_Columns, hv_minc, out hv_index);
                    ho_Arrow2.Dispose();
                    HalconHelp.gen_arrow_contour_xld(out ho_Arrow2, hv_Rows.TupleSelect(hv_index.TupleSelect(
                        0)), (hv_Columns.TupleSelect(hv_index.TupleSelect(0))) - 50, hv_Rows.TupleSelect(
                        hv_index.TupleSelect(0)), hv_Columns.TupleSelect(hv_index.TupleSelect(0)),
                        15, 15);
                    ho_Arrow.Dispose();
                    HOperatorSet.ConcatObj(ho_Arrow1, ho_Arrow2, out ho_Arrow);
                }
                else if ((int)(new HTuple(hv_vOrh.TupleEqual("v"))) != 0 && hv_Rows.Length > 0)
                {
                    hv_maxc = hv_Rows.TupleMax();
                    hv_minc = hv_Rows.TupleMin();
                    hv_length = hv_maxc - hv_minc;
                    HOperatorSet.TupleFind(hv_Rows, hv_maxc, out hv_index);

                    ho_Arrow1.Dispose();
                    HalconHelp.gen_arrow_contour_xld(out ho_Arrow1, (hv_Rows.TupleSelect(hv_index.TupleSelect(
                        0))) + 50, hv_Columns.TupleSelect(hv_index.TupleSelect(0)), hv_Rows.TupleSelect(
                        hv_index.TupleSelect(0)), hv_Columns.TupleSelect(hv_index.TupleSelect(0)),
                        15, 15);
                    HOperatorSet.TupleFind(hv_Rows, hv_minc, out hv_index);
                    ho_Arrow2.Dispose();
                    HalconHelp.gen_arrow_contour_xld(out ho_Arrow2, (hv_Rows.TupleSelect(hv_index.TupleSelect(
                        0))) - 50, hv_Columns.TupleSelect(hv_index.TupleSelect(0)), hv_Rows.TupleSelect(
                        hv_index.TupleSelect(0)), hv_Columns.TupleSelect(hv_index.TupleSelect(0)),
                        15, 15);
                    ho_Arrow.Dispose();
                    HOperatorSet.ConcatObj(ho_Arrow1, ho_Arrow2, out ho_Arrow);

                    hv_length = (hv_Rows.TupleMax()) - (hv_Rows.TupleMin());
                }
                ho_Arrow1.Dispose();
                ho_Arrow2.Dispose();
                return;
            }

            catch (Exception e)
            {
                ho_Arrow = null;
                hv_length = 0;
                return;
            }  
        }
        public void RegionOpen(HObject ho_Region, HObject ho_Rectangle, out HObject ho_RegionOpening1)
        {


            // Local iconic variables 

            HObject ho_RegionIntersection, ho_RegionOpening = null;
            HObject ho_RegionDifference;


            // Local control variables 

            HTuple hv_h, hv_Row2 = new HTuple(), hv_Row1 = new HTuple();
            HTuple hv_w, hv_Column2 = new HTuple(), hv_Column1 = new HTuple();
            HTuple hv_Rows, hv_Columns, hv_h1, hv_w1;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_RegionOpening1);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);

            ho_RegionIntersection.Dispose();

            HOperatorSet.Intersection(ho_Region, ho_Rectangle, out ho_RegionIntersection);
            
            HOperatorSet.SmallestRectangle1(ho_Rectangle, out hv_Row1, out hv_Column1, out hv_Row2, out hv_Column2);

            hv_h = hv_Row2 - hv_Row1;
            hv_w = hv_Column2 - hv_Column1;
            HOperatorSet.GetRegionPoints(ho_RegionIntersection, out hv_Rows, out hv_Columns);
            hv_h1 = (hv_Rows.TupleMax()) - (hv_Rows.TupleMin());
            hv_w1 = (hv_Columns.TupleMax()) - (hv_Columns.TupleMin());
            //if ((int)(new HTuple(((hv_w.TupleRound())).TupleEqual(hv_w1))) != 0)
            //{

            if (vOrh == "h")
            {
                hv_h = hv_h.TupleConcat(hv_h1);
                ho_RegionOpening.Dispose();
                HOperatorSet.OpeningRectangle1(ho_RegionIntersection, out ho_RegionOpening,
                    10,hv_h.TupleMin());
            }
            //}
            else
            {
                hv_w = hv_w.TupleConcat(hv_w1);
                ho_RegionOpening.Dispose();
                HOperatorSet.OpeningRectangle1(ho_RegionIntersection, out ho_RegionOpening,
                    hv_w.TupleMin(), 10);
            }
            ho_RegionDifference.Dispose();
            HOperatorSet.Difference(ho_RegionIntersection, ho_RegionOpening, out ho_RegionDifference
                );
            ho_RegionOpening1.Dispose();
            HOperatorSet.OpeningCircle(ho_RegionDifference, out ho_RegionOpening1, 3.5);
            ho_RegionIntersection.Dispose();
            ho_RegionOpening.Dispose();
            ho_RegionDifference.Dispose();

            return;
        }
        private void action()
        {
            _length = 0;
            // Local iconic variables 

            HObject ho_Rectangle, ho_ImageReduced;
            HObject ho_Arrow, region,ho_regionOpening;
            // Local control variables 
            HTuple hv_Number;
            HOperatorSet.GenEmptyObj(out region);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Arrow);
            try
            {

                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle, this.DRow1, this.DColumn1, this.DRow2, this.DColumn2);

                //HOperatorSet.Intersection(algorithm.Region, ho_Rectangle, out region);
                if (taijie == "true")
                {
                    RegionOpen(algorithm.Region, ho_Rectangle, out region);
                }
                else
                {
                    HOperatorSet.Intersection(algorithm.Region, ho_Rectangle, out region);
                }
                this.result = new HTuple();
                ho_Arrow.Dispose();
                if (algorithm.Region.IsInitialized())
                {
                    my_clamp(region, out ho_Arrow, vOrh, out this._length);

                    HOperatorSet.CountObj(region, out hv_Number);
                    if ((int)(new HTuple(hv_Number.TupleEqual(1))) != 0)
                    {
                        this.Result = this.Result.TupleConcat("length");
                        this.Result = this.Result.TupleConcat(this._length * pixeldist);
                    }
                    else
                    {
                        this.Result = this.Result.TupleConcat("length");
                        this.Result = this.Result.TupleConcat(0);
                    }
                    //HOperatorSet.SetColor(this.LWindowHandle, "green");
                    //HOperatorSet.DispObj(ho_Arrow, this.LWindowHandle);
                    if (ho_Arrow.IsInitialized())
                    {
                        RegionToDisp.Dispose();
                        if (RegionToDisp.IsInitialized())
                            HOperatorSet.ConcatObj(ho_Arrow, RegionToDisp, out RegionToDisp);
                        else
                        {
                            HOperatorSet.GenRegionContourXld(ho_Arrow, out RegionToDisp, "filled");
                            HOperatorSet.Union1(RegionToDisp, out RegionToDisp);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                this.Result = new HTuple();
                this.Result = this.Result.TupleConcat("length");
                this.Result = this.Result.TupleConcat(0);
            }
            finally
            {
                algorithm.Region.Dispose();
                region.Dispose();
                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Arrow.Dispose();
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
            catch(Exception e)
            {
                Debug.Print(e.Message);
                return false;
            }
        }

    }
}
