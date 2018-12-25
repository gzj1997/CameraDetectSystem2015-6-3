using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
namespace CameraDetectSystem
{
    [Serializable]
    public class ScrewPitchTool : ImageTools
    {
        [NonSerialized]
        HTuple toothRow1, toothRow2, toothColumn1, toothColumn2;
        [NonSerialized]
        HTuple RoiRow1, RoiRow2, RoiColumn1, RoiColumn2;

        public ScrewPitchTool(HObject Image, Algorithm al)
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
            HalconHelp.disp_message(this.LWindowHandle, "选中一个牙", "window", 12, 12, "green",
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

        public void TeethDist(HObject ho_Region, out HObject ho_Cross, HTuple hv_vOrh,
      out HTuple hv_maxDist, out HTuple hv_minDist, out HTuple hv_meanDist)
        {
            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];
            long SP_O = 0;

            // Local iconic variables 

            HObject ho_RegionErosion, ho_ConnectedRegions;
            HObject ho_SortedRegions = null, ho_EmptyRegion = null, ho_ObjectSelected = null;
            HObject ho_RegionTrans = null, ho_SortedRegions1 = null, ho_SelectedRegions1 = null;
            HObject ho_ObjectSelected1 = null, ho_SelectedRegions2 = null;
            HObject ho_ObjectSelected2 = null;


            // Local control variables 

            HTuple hv_Number = new HTuple(), hv_Index = new HTuple();
            HTuple hv_Area = new HTuple(), hv_Row = new HTuple(), hv_Column = new HTuple();
            HTuple hv_Number1 = new HTuple(), hv_Number2 = new HTuple();
            HTuple hv_dist = new HTuple(), hv_Index1 = new HTuple(), hv_Area1 = new HTuple();
            HTuple hv_Row4 = new HTuple(), hv_Column3 = new HTuple(), hv_Area2 = new HTuple();
            HTuple hv_Row5 = new HTuple(), hv_Column4 = new HTuple();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Cross);
            HOperatorSet.GenEmptyObj(out ho_RegionErosion);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SortedRegions);
            HOperatorSet.GenEmptyObj(out ho_EmptyRegion);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans);
            HOperatorSet.GenEmptyObj(out ho_SortedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected2);

            try
            {
                ho_RegionErosion.Dispose();
                HOperatorSet.ErosionCircle(ho_Region, out ho_RegionErosion, 3.5);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_RegionErosion, out ho_ConnectedRegions);
                if ((int)(new HTuple(hv_vOrh.TupleEqual("h"))) != 0)
                {
                    ho_SortedRegions.Dispose();
                    HOperatorSet.SortRegion(ho_ConnectedRegions, out ho_SortedRegions, "character",
                        "true", "column");
                    HOperatorSet.CountObj(ho_SortedRegions, out hv_Number);
                    ho_EmptyRegion.Dispose();
                    HOperatorSet.GenEmptyRegion(out ho_EmptyRegion);

                    for (hv_Index = 3; hv_Index.Continue(hv_Number - 2, 1); hv_Index = hv_Index.TupleAdd(1))
                    {
                        ho_ObjectSelected.Dispose();
                        HOperatorSet.SelectObj(ho_SortedRegions, out ho_ObjectSelected, hv_Index);
                        OTemp[SP_O] = ho_EmptyRegion.CopyObj(1, -1);
                        SP_O++;
                        ho_EmptyRegion.Dispose();
                        HOperatorSet.ConcatObj(ho_ObjectSelected, OTemp[SP_O - 1], out ho_EmptyRegion
                            );
                        OTemp[SP_O - 1].Dispose();
                        SP_O = 0;
                    }
                    ho_RegionTrans.Dispose();
                    HOperatorSet.ShapeTrans(ho_EmptyRegion, out ho_RegionTrans, "outer_circle");
                    ho_SortedRegions1.Dispose();
                    HOperatorSet.SortRegion(ho_RegionTrans, out ho_SortedRegions1, "character",
                        "true", "row");
                    HOperatorSet.AreaCenter(ho_SortedRegions1, out hv_Area, out hv_Row, out hv_Column);
                    ho_Cross.Dispose();
                    HOperatorSet.GenCrossContourXld(out ho_Cross, hv_Row, hv_Column, 6, 0.785398);


                    HOperatorSet.CountObj(ho_SortedRegions1, out hv_Number1);
                    ho_SelectedRegions1.Dispose();
                    HOperatorSet.SelectShape(ho_SortedRegions1, out ho_SelectedRegions1, "row",
                        "and", (hv_Row.TupleSelect(0)) - 10, (hv_Row.TupleSelect(0)) + 10);
                    HOperatorSet.CountObj(ho_SelectedRegions1, out hv_Number2);
                    hv_dist = new HTuple();
                    for (hv_Index1 = 1; hv_Index1.Continue(hv_Number2 - 1, 1); hv_Index1 = hv_Index1.TupleAdd(1))
                    {
                        ho_ObjectSelected1.Dispose();
                        HOperatorSet.SelectObj(ho_SelectedRegions1, out ho_ObjectSelected1, hv_Index1);
                        HOperatorSet.AreaCenter(ho_ObjectSelected1, out hv_Area1, out hv_Row4,
                            out hv_Column3);

                        ho_ObjectSelected1.Dispose();
                        HOperatorSet.SelectObj(ho_SelectedRegions1, out ho_ObjectSelected1, hv_Index1 + 1);
                        HOperatorSet.AreaCenter(ho_ObjectSelected1, out hv_Area2, out hv_Row5,
                            out hv_Column4);

                        hv_dist = hv_dist.TupleConcat(hv_Column4 - hv_Column3);
                    }


                    ho_SelectedRegions2.Dispose();
                    HOperatorSet.SelectShape(ho_SortedRegions1, out ho_SelectedRegions2, "row",
                        "and", (hv_Row.TupleSelect(hv_Number1 - 1)) - 10, (hv_Row.TupleSelect(hv_Number1 - 1)) + 10);

                    HOperatorSet.CountObj(ho_SelectedRegions2, out hv_Number2);
                    for (hv_Index1 = 1; hv_Index1.Continue(hv_Number2 - 1, 1); hv_Index1 = hv_Index1.TupleAdd(1))
                    {
                        ho_ObjectSelected2.Dispose();
                        HOperatorSet.SelectObj(ho_SelectedRegions2, out ho_ObjectSelected2, hv_Index1);
                        HOperatorSet.AreaCenter(ho_ObjectSelected2, out hv_Area1, out hv_Row4,
                            out hv_Column3);

                        ho_ObjectSelected2.Dispose();
                        HOperatorSet.SelectObj(ho_SelectedRegions2, out ho_ObjectSelected2, hv_Index1 + 1);
                        HOperatorSet.AreaCenter(ho_ObjectSelected2, out hv_Area2, out hv_Row5,
                            out hv_Column4);

                        hv_dist = hv_dist.TupleConcat(hv_Column4 - hv_Column3);
                    }
                }
                else
                {
                    ho_SortedRegions.Dispose();
                    HOperatorSet.SortRegion(ho_ConnectedRegions, out ho_SortedRegions, "character",
                        "true", "row");
                    HOperatorSet.CountObj(ho_SortedRegions, out hv_Number);
                    ho_EmptyRegion.Dispose();
                    HOperatorSet.GenEmptyRegion(out ho_EmptyRegion);

                    for (hv_Index = 3; hv_Index.Continue(hv_Number - 2, 1); hv_Index = hv_Index.TupleAdd(1))
                    {
                        ho_ObjectSelected.Dispose();
                        HOperatorSet.SelectObj(ho_SortedRegions, out ho_ObjectSelected, hv_Index);
                        OTemp[SP_O] = ho_EmptyRegion.CopyObj(1, -1);
                        SP_O++;
                        ho_EmptyRegion.Dispose();
                        HOperatorSet.ConcatObj(ho_ObjectSelected, OTemp[SP_O - 1], out ho_EmptyRegion
                            );
                        OTemp[SP_O - 1].Dispose();
                        SP_O = 0;
                    }
                    ho_RegionTrans.Dispose();
                    HOperatorSet.ShapeTrans(ho_EmptyRegion, out ho_RegionTrans, "outer_circle");
                    ho_SortedRegions1.Dispose();
                    HOperatorSet.SortRegion(ho_RegionTrans, out ho_SortedRegions1, "character",
                        "true", "column");
                    HOperatorSet.AreaCenter(ho_SortedRegions1, out hv_Area, out hv_Row, out hv_Column);
                    ho_Cross.Dispose();
                    HOperatorSet.GenCrossContourXld(out ho_Cross, hv_Row, hv_Column, 6, 0.785398);


                    HOperatorSet.CountObj(ho_SortedRegions1, out hv_Number1);
                    ho_SelectedRegions1.Dispose();
                    HOperatorSet.SelectShape(ho_SortedRegions1, out ho_SelectedRegions1, "column",
                        "and", (hv_Column.TupleSelect(0)) - 10, (hv_Column.TupleSelect(0)) + 10);
                    HOperatorSet.CountObj(ho_SelectedRegions1, out hv_Number2);
                    hv_dist = new HTuple();
                    for (hv_Index1 = 1; hv_Index1.Continue(hv_Number2 - 1, 1); hv_Index1 = hv_Index1.TupleAdd(1))
                    {
                        ho_ObjectSelected1.Dispose();
                        HOperatorSet.SelectObj(ho_SelectedRegions1, out ho_ObjectSelected1, hv_Index1);
                        HOperatorSet.AreaCenter(ho_ObjectSelected1, out hv_Area1, out hv_Row4,
                            out hv_Column3);

                        ho_ObjectSelected1.Dispose();
                        HOperatorSet.SelectObj(ho_SelectedRegions1, out ho_ObjectSelected1, hv_Index1 + 1);
                        HOperatorSet.AreaCenter(ho_ObjectSelected1, out hv_Area2, out hv_Row5,
                            out hv_Column4);

                        hv_dist = hv_dist.TupleConcat(hv_Row5 - hv_Row4);
                    }


                    ho_SelectedRegions2.Dispose();
                    HOperatorSet.SelectShape(ho_SortedRegions1, out ho_SelectedRegions2, "column",
                        "and", (hv_Column.TupleSelect(hv_Number1 - 1)) - 10, (hv_Column.TupleSelect(
                        hv_Number1 - 1)) + 10);

                    HOperatorSet.CountObj(ho_SelectedRegions2, out hv_Number2);
                    for (hv_Index1 = 1; hv_Index1.Continue(hv_Number2 - 1, 1); hv_Index1 = hv_Index1.TupleAdd(1))
                    {
                        ho_ObjectSelected2.Dispose();
                        HOperatorSet.SelectObj(ho_SelectedRegions2, out ho_ObjectSelected2, hv_Index1);
                        HOperatorSet.AreaCenter(ho_ObjectSelected2, out hv_Area1, out hv_Row4,
                            out hv_Column3);

                        ho_ObjectSelected2.Dispose();
                        HOperatorSet.SelectObj(ho_SelectedRegions2, out ho_ObjectSelected2, hv_Index1 + 1);
                        HOperatorSet.AreaCenter(ho_ObjectSelected2, out hv_Area2, out hv_Row5,
                            out hv_Column4);

                        hv_dist = hv_dist.TupleConcat(hv_Row5 - hv_Row4);
                    }
                }
                hv_maxDist = hv_dist.TupleMax();
                hv_minDist = hv_dist.TupleMin();
                hv_meanDist = hv_dist.TupleMean();
                ho_RegionErosion.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SortedRegions.Dispose();
                ho_EmptyRegion.Dispose();
                ho_ObjectSelected.Dispose();
                ho_RegionTrans.Dispose();
                ho_SortedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_ObjectSelected1.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_ObjectSelected2.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_RegionErosion.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SortedRegions.Dispose();
                ho_EmptyRegion.Dispose();
                ho_ObjectSelected.Dispose();
                ho_RegionTrans.Dispose();
                ho_SortedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_ObjectSelected1.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_ObjectSelected2.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Main procedure 
        private void action()
        {
            // Local iconic variables 

            HObject ho_Rectangle, ho_ImageReduced;
            HObject ho_RegionToDetect, ho_RegionOpening, ho_RegionDifference;
            HObject ho_Cross = null;
            // Local control variables 
            HObject ho_Region;
            HTuple  hv_vOrh;
            HTuple hv_maxDist = new HTuple(), hv_minDist = new HTuple();
            HTuple hv_meanDist = new HTuple(), hv_Exception;

            // Initialize local and output iconic variables 

            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_RegionToDetect);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_Cross);

            try
            {
                //HOperatorSet.SetDraw(this.LWindowHandle, "margin");
                //disp_message(this.LWindowHandle, "选中一个牙", "window", 12, 12, "black",
                //    "true");
                //HOperatorSet.DrawRectangle1(this.LWindowHandle, out hv_Row1, out hv_Column1,
                //    out hv_Row2, out hv_Column2);
                //ho_Rectangle.Dispose();
                //HOperatorSet.GenRectangle1(out ho_Rectangle, hv_Row1, hv_Column1, hv_Row2,
                //    hv_Column2);
                //disp_message(this.LWindowHandle, "选中检测区域", "window", 12, 12, "black",
                //    "true");
                //HOperatorSet.DrawRectangle1(this.LWindowHandle, out hv_Row11, out hv_Column11,
                //    out hv_Row21, out hv_Column21);
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle, this.rr1, this.rc1, this.rr2,
                    this.rc2);
                ho_Region.Dispose();
                HOperatorSet.Intersection(ho_Rectangle, this.algorithm.Region, out ho_Region);
                //ho_ImageReduced.Dispose();
                //HOperatorSet.ReduceDomain(ho_Image, ho_Rectangle, out ho_ImageReduced);
                //ho_RegionToDetect.Dispose();
                //HOperatorSet.Threshold(ho_ImageReduced, out ho_RegionToDetect, 0, 128);
                ho_RegionOpening.Dispose();
                HOperatorSet.OpeningRectangle1(ho_Region, out ho_RegionOpening, tc2 - tc1,
                    tr2 - tr1);
                ho_RegionDifference.Dispose();
                HOperatorSet.Difference(ho_Region, ho_RegionOpening, out ho_RegionDifference
                    );
                hv_vOrh = "h";
                try
                {
                    ho_Cross.Dispose();
                    TeethDist(ho_RegionDifference, out ho_Cross, hv_vOrh, out hv_maxDist, out hv_minDist,
                        out hv_meanDist);
                    if (ho_Cross.IsInitialized())
                    {

                        if (RegionToDisp.IsInitialized())
                        {
                            HOperatorSet.ConcatObj(ho_Cross, RegionToDisp, out RegionToDisp);
                            HOperatorSet.GenRegionContourXld(ho_Cross, out RegionToDisp, "filled");
                            HOperatorSet.Union1(RegionToDisp, out RegionToDisp);
                        }
                        else
                        {
                            HOperatorSet.GenRegionContourXld(ho_Cross, out RegionToDisp, "filled");
                            HOperatorSet.Union1(RegionToDisp, out RegionToDisp);
                        }
                    }
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    hv_maxDist = 0;
                    hv_minDist = 0;
                    hv_meanDist = 0;
                }
                finally
                {
                    HTuple hv_result = new HTuple();
                        hv_result = hv_result.TupleConcat("最大间距");
                        hv_result = hv_result.TupleConcat(hv_maxDist * pixeldist);

                        hv_result = hv_result.TupleConcat("最小间距");
                        hv_result = hv_result.TupleConcat(hv_minDist * pixeldist);

                        hv_result = hv_result.TupleConcat("平均间距");
                        hv_result = hv_result.TupleConcat(hv_meanDist * pixeldist);

                        result = hv_result.Clone();
                }

            }
            catch (HalconException HDevExpDefaultException)
            {
                
                ho_Rectangle.Dispose();
                ho_ImageReduced.Dispose();
                ho_RegionToDetect.Dispose();
                ho_RegionOpening.Dispose();
                ho_RegionDifference.Dispose();
                ho_Cross.Dispose();

                throw HDevExpDefaultException;
            }
       
            ho_Rectangle.Dispose();
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
