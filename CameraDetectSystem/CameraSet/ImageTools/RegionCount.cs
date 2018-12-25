using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
namespace CameraDetectSystem
{
    [Serializable]
    public class RegionCounts : ImageTools
    {
        [NonSerialized]
        HTuple toothRow1, toothRow2, toothColumn1, toothColumn2;
        [NonSerialized]
        HTuple RoiRow1, RoiRow2, RoiColumn1, RoiColumn2;

        public RegionCounts(HObject Image, Algorithm al)
        {
            this.Image = Image; this.algorithm.Image = Image; this.algorithm = al;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
            pixeldist = 1;
        }

        double tr1, tr2, tc1, tc2;
        double rr1, rr2, rc1, rc2;
        public override void draw()
        {
            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            HalconHelp.disp_message(this.LWindowHandle, "选中最小区域", "window", 12, 12, "green",
                    "true");
            HOperatorSet.DrawRectangle1(this.LWindowHandle, out toothRow1, out toothColumn1, out toothRow2, out toothColumn2);

            tr1 = toothRow1.D;
            tr2 = toothRow2.D;
            tc1 = toothColumn1.D;
            tc2 = toothColumn2.D;
            HObject rect1;
            HOperatorSet.GenEmptyObj(out rect1);
            HOperatorSet.GenRectangle1(out rect1, toothRow1, toothColumn1, toothRow2, toothColumn2);
            HOperatorSet.DispObj(rect1, this.LWindowHandle);
            HalconHelp.disp_message(this.LWindowHandle, "选中检测区域", "window", 12, 12, "green",
                    "true");

            HOperatorSet.DrawRectangle1(this.LWindowHandle, out RoiRow1, out RoiColumn1, out RoiRow2, out RoiColumn2);

            rr1 = RoiRow1.D;
            rr2 = RoiRow2.D;
            rc1 = RoiColumn1.D;
            rc2 = RoiColumn2.D;

            HObject rect2;
            HOperatorSet.GenEmptyObj(out rect2);
            HOperatorSet.GenRectangle1(out rect2, RoiRow1, RoiColumn1, RoiRow2, RoiColumn2);
            HOperatorSet.DispObj(rect2, this.LWindowHandle);

        }
        // Main procedure 
        private void action()
        {
            // Local iconic variables 

            HObject ho_Rectangle1,ho_Rectangle2, ho_ImageReduced;
            HObject ho_RegionToDetect, ho_RegionOpening, ho_RegionDifference;
            HObject ho_Cross = null;
            HObject selectObject;
            // Local control variables 
            HObject ho_Region;
            HTuple hv_vOrh;
            HTuple hv_maxDist = new HTuple(), hv_minDist = new HTuple();
            HTuple hv_meanDist = new HTuple(), hv_Exception;
            HTuple Num;
            // Initialize local and output iconic variables 

            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_Rectangle2);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_RegionToDetect);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_Cross);
            HOperatorSet.GenEmptyObj(out selectObject);
            try
            {
               
                ho_Rectangle1.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle1, this.tr1, this.tc1, this.tr2,
                    this.tc2);
                ho_Region.Dispose();
                HOperatorSet.Intersection(ho_Rectangle1, this.algorithm.Region, out ho_Region);
                //ho_ImageReduced.Dispose();
                //HOperatorSet.ReduceDomain(ho_Image, ho_Rectangle, out ho_ImageReduced);
                //ho_RegionToDetect.Dispose();
                //HOperatorSet.Threshold(ho_ImageReduced, out ho_RegionToDetect, 0, 128);
                HTuple area,row,col;
                HOperatorSet.Connection(ho_Region, out ho_Region);
                HOperatorSet.AreaCenter(ho_Region, out area, out row,out col);
                ho_Rectangle2.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle2, this.rr1, this.rc1, rr2, rc2);
                ho_Region.Dispose();
                HOperatorSet.Intersection(ho_Rectangle2, this.algorithm.Region, out ho_Region);

                HOperatorSet.Connection(ho_Region, out ho_Region);
                HOperatorSet.CountObj(ho_Region, out Num);
                HOperatorSet.SelectShape(ho_Region, out selectObject, "area", "and", area.TupleMax(), area.TupleMax() * 3);
                HOperatorSet.ShapeTrans(selectObject, out RegionToDisp, "rectangle1");
                HOperatorSet.CountObj(selectObject, out Num);
                try
                {
                    if (selectObject.IsInitialized())
                        {

                            HOperatorSet.Union1(selectObject, out RegionToDisp);
                        }
                        else
                        {

                            HOperatorSet.Union1(selectObject, out RegionToDisp);
                        }
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    Num = 0;
                    
                }
                finally
                {
                    HTuple hv_result = new HTuple();
                    hv_result = hv_result.TupleConcat("数量");
                    hv_result = hv_result.TupleConcat(Num);
                    result = hv_result.Clone();

                }

            }
            catch (HalconException HDevExpDefaultException)
            {

                ho_Rectangle1.Dispose();
                ho_Rectangle2.Dispose();
                ho_ImageReduced.Dispose();
                ho_RegionToDetect.Dispose();
                ho_RegionOpening.Dispose();
                ho_RegionDifference.Dispose();
                ho_Cross.Dispose();

                throw HDevExpDefaultException;
            }

            ho_Rectangle1.Dispose();
            ho_Rectangle2.Dispose();
            ho_ImageReduced.Dispose();
            ho_RegionToDetect.Dispose();
            ho_RegionOpening.Dispose();
            ho_RegionDifference.Dispose();
            ho_Cross.Dispose();

        }

        public override bool method()
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
