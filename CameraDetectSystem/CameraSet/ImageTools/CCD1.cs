using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace CameraDetectSystem
{
    [Serializable]
    class CCD1:ImageTools
    {
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public CCD1()
        {
            RegionToDisp = Image;
        }
        public CCD1(HObject Image, Algorithm al)
        {
            gex = 1;
            al = null;
            this.Image = Image;
            RegionToDisp = Image;
            pixeldist = 1;
        }
        private void action()
        {
            HTuple hv_Area1 = null, hv_Row = null, hv_Column = null;
            HTuple hv_Area2 = null, hv_Area3 = null, hv_Area4 = null, hv_Area5 = null, hv_Area6=null;
            HTuple hv_l1 = null, hv_l2 = null;
            HObject ho_Region, ho_Rectangle1, ho_Rectangle2, ho_Rectangle5, ho_Rectangle6, ho_RegionIntersection5, ho_RegionIntersection6;
            HObject ho_Rectangle3, ho_Rectangle4, ho_RegionIntersection1;
            HObject ho_RegionIntersection2, ho_RegionIntersection3;
            HObject ho_RegionIntersection4;
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_Rectangle2);
            HOperatorSet.GenEmptyObj(out ho_Rectangle3);
            HOperatorSet.GenEmptyObj(out ho_Rectangle4);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection1);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection2);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection3);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection4);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection5);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection6);
            HOperatorSet.GenEmptyObj(out ho_Rectangle5);
            HOperatorSet.GenEmptyObj(out ho_Rectangle6);

            ho_Region.Dispose();
            HOperatorSet.Threshold(Image, out ho_Region, 0, 64);
            ho_Rectangle1.Dispose();
            HOperatorSet.GenRectangle1(out ho_Rectangle1, 5, 5, 75, 1900);
            ho_Rectangle2.Dispose();
            HOperatorSet.GenRectangle1(out ho_Rectangle2, 550, 5, 650, 1900);
            ho_Rectangle3.Dispose();
            HOperatorSet.GenRectangle1(out ho_Rectangle3, 1160, 5, 1230, 1900);
            ho_Rectangle4.Dispose();
            HOperatorSet.GenRectangle1(out ho_Rectangle4, 0, 1000, 1236, 1400);
            ho_Rectangle5.Dispose();
            HOperatorSet.GenRectangle1(out ho_Rectangle5, 5, 1000, 75, 1050);
            ho_Rectangle6.Dispose();
            HOperatorSet.GenRectangle1(out ho_Rectangle6, 1160, 1000, 1230, 1050);
            ho_RegionIntersection5.Dispose();
            HOperatorSet.Intersection(ho_Region, ho_Rectangle5, out ho_RegionIntersection5
                );
            ho_RegionIntersection6.Dispose();
            HOperatorSet.Intersection(ho_Region, ho_Rectangle6, out ho_RegionIntersection6
                );
            ho_RegionIntersection1.Dispose();
            HOperatorSet.Intersection(ho_Region, ho_Rectangle1, out ho_RegionIntersection1
                );
            ho_RegionIntersection2.Dispose();
            HOperatorSet.Intersection(ho_Region, ho_Rectangle2, out ho_RegionIntersection2
                );
            ho_RegionIntersection3.Dispose();
            HOperatorSet.Intersection(ho_Region, ho_Rectangle3, out ho_RegionIntersection3
                );
            ho_RegionIntersection4.Dispose();
            HOperatorSet.Intersection(ho_Region, ho_Rectangle4, out ho_RegionIntersection4
                );
            HOperatorSet.AreaCenter(ho_RegionIntersection1, out hv_Area1, out hv_Row, out hv_Column);
            HOperatorSet.AreaCenter(ho_RegionIntersection2, out hv_Area2, out hv_Row, out hv_Column);
            HOperatorSet.AreaCenter(ho_RegionIntersection3, out hv_Area3, out hv_Row, out hv_Column);
            HOperatorSet.AreaCenter(ho_RegionIntersection4, out hv_Area4, out hv_Row, out hv_Column);
            HOperatorSet.AreaCenter(ho_RegionIntersection5, out hv_Area5, out hv_Row, out hv_Column);
            HOperatorSet.AreaCenter(ho_RegionIntersection6, out hv_Area6, out hv_Row, out hv_Column);
            if ((int)((new HTuple(hv_Area5.TupleEqual(0))).TupleAnd(new HTuple(hv_Area6.TupleEqual(
                0)))) != 0)
            {
                hv_l1 = (hv_Area2 / 100) - ((hv_Area1 + hv_Area3) / 140);
            }
            else if ((int)((new HTuple(hv_Area5.TupleEqual(0))).TupleAnd(new HTuple(hv_Area6.TupleGreater(
                0)))) != 0)
            {
                hv_l1 = (hv_Area2 / 100) - (hv_Area1 / 70);
            }
            else if ((int)((new HTuple(hv_Area5.TupleGreater(0))).TupleAnd(new HTuple(hv_Area6.TupleEqual(
                0)))) != 0)
            {
                hv_l1 = (hv_Area2 / 100) - (hv_Area3 / 70);
            }
            hv_l2 = (double)hv_Area4 / 400;
            HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("length1");
                hv_result = hv_result.TupleConcat(hv_l1 * pixeldist);
                hv_result = hv_result.TupleConcat("length2");
                hv_result = hv_result.TupleConcat(hv_l2 * pixeldist);
                result = hv_result.Clone();

                ho_Region.Dispose();
                ho_Rectangle1.Dispose();
                ho_Rectangle2.Dispose();
                ho_Rectangle3.Dispose();
                ho_Rectangle4.Dispose();
                ho_Rectangle5.Dispose();
                ho_Rectangle6.Dispose();
                ho_RegionIntersection1.Dispose();
                ho_RegionIntersection2.Dispose();
                ho_RegionIntersection3.Dispose();
                ho_RegionIntersection4.Dispose();
                ho_RegionIntersection5.Dispose();
                ho_RegionIntersection6.Dispose();
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
