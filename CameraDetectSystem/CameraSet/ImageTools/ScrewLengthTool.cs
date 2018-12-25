using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
namespace CameraDetectSystem
{
    [Serializable]
    public class ScrewLength : ImageTools
    {
        [NonSerialized]
        HTuple toothRow1, toothRow2, toothColumn1, toothColumn2;
        [NonSerialized]
        HTuple RoiRow1, RoiRow2, RoiColumn1, RoiColumn2;

        public ScrewLength(HObject Image, Algorithm al)
        {
            this.Image = Image; this.algorithm.Image = Image; this.algorithm = al;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
            pixeldist = 1;
        }

        double tr1, tr2, tc1, tc2;
        double rr1, rr2, rc1, rc2;
        public string hv_vOrh;
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


        public void screwLength(HObject ho_RegionFillUp, out HObject ho_RegionOut, HTuple hv_Row1,
            HTuple hv_Column1, HTuple hv_Row2, HTuple hv_Column2, HTuple hv_vOrh, out HTuple hv_length)
        {



            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];
            long SP_O = 0;

            // Local iconic variables 

            HObject ho_RegionClosing, ho_RegionDifference;
            HObject ho_ConnectedRegions, ho_SelectedRegions, ho_SelectedRegions1;
            HObject ho_RegionTrans, ho_SortedRegions = null, ho_First = null;
            HObject ho_Last = null, ho_ObjectSelected1 = null, ho_ObjectSelected2 = null;
            HObject ho_Rectangle1 = null, ho_Rectangle2 = null;


            // Local control variables 

            HTuple hv_Area = new HTuple(), hv_Row = new HTuple();
            HTuple hv_Column = new HTuple(), hv_Number = new HTuple();
            HTuple hv_Area1 = new HTuple(), hv_Row3 = new HTuple(), hv_Column3 = new HTuple();
            HTuple hv_Area2 = new HTuple(), hv_Row4 = new HTuple(), hv_Column4 = new HTuple();
            HTuple hv_Area3 = new HTuple(), hv_Row5 = new HTuple(), hv_Column5 = new HTuple();
            HTuple hv_Area4 = new HTuple(), hv_Row6 = new HTuple(), hv_Column6 = new HTuple();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_RegionOut);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans);
            HOperatorSet.GenEmptyObj(out ho_SortedRegions);
            HOperatorSet.GenEmptyObj(out ho_First);
            HOperatorSet.GenEmptyObj(out ho_Last);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected1);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected2);
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_Rectangle2);

            hv_length = new HTuple();
            try
            {
                ho_RegionClosing.Dispose();
                HOperatorSet.OpeningRectangle1(ho_RegionFillUp, out ho_RegionClosing, hv_Row2 - hv_Row1,
                    hv_Column2 - hv_Column1);
                ho_RegionDifference.Dispose();
                HOperatorSet.Difference(ho_RegionFillUp, ho_RegionClosing, out ho_RegionDifference
                    );
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_RegionDifference, out ho_ConnectedRegions);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                    "and", ((hv_Row2 - hv_Row1) * (hv_Column2 - hv_Column1)) / 10, (hv_Row2 - hv_Row1) * (hv_Column2 - hv_Column1));
                ho_SelectedRegions1.Dispose();
                HOperatorSet.SelectShape(ho_SelectedRegions, out ho_SelectedRegions1, "compactness",
                    "and", 1, 2);
                ho_RegionTrans.Dispose();
                HOperatorSet.ShapeTrans(ho_SelectedRegions1, out ho_RegionTrans, "inner_circle");

                if ((int)(new HTuple(hv_vOrh.TupleEqual("h"))) != 0)
                {
                    ho_SortedRegions.Dispose();
                    HOperatorSet.SortRegion(ho_RegionTrans, out ho_SortedRegions, "character",
                        "true", "column");
                    HOperatorSet.AreaCenter(ho_SortedRegions, out hv_Area, out hv_Row, out hv_Column);
                    HOperatorSet.CountObj(ho_SortedRegions, out hv_Number);

                    ho_First.Dispose();
                    HOperatorSet.GenEmptyObj(out ho_First);
                    ho_Last.Dispose();
                    HOperatorSet.GenEmptyObj(out ho_Last);
                    ho_ObjectSelected1.Dispose();
                    HOperatorSet.SelectObj(ho_SortedRegions, out ho_ObjectSelected1, 1);
                    ho_ObjectSelected2.Dispose();
                    HOperatorSet.SelectObj(ho_SortedRegions, out ho_ObjectSelected2, 2);
                    HOperatorSet.AreaCenter(ho_ObjectSelected1, out hv_Area1, out hv_Row3, out hv_Column3);
                    HOperatorSet.AreaCenter(ho_ObjectSelected2, out hv_Area2, out hv_Row4, out hv_Column4);
                    if ((int)(new HTuple(hv_Column3.TupleLess(hv_Column4))) != 0)
                    {
                        OTemp[SP_O] = ho_First.CopyObj(1, -1);
                        SP_O++;
                        ho_First.Dispose();
                        HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_ObjectSelected1, out ho_First);
                        OTemp[SP_O - 1].Dispose();
                        SP_O = 0;
                    }
                    else
                    {
                        OTemp[SP_O] = ho_First.CopyObj(1, -1);
                        SP_O++;
                        ho_First.Dispose();
                        HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_ObjectSelected2, out ho_First);
                        OTemp[SP_O - 1].Dispose();
                        SP_O = 0;
                    }

                    ho_ObjectSelected1.Dispose();
                    HOperatorSet.SelectObj(ho_SortedRegions, out ho_ObjectSelected1, hv_Number - 1);
                    ho_ObjectSelected2.Dispose();
                    HOperatorSet.SelectObj(ho_SortedRegions, out ho_ObjectSelected2, hv_Number);
                    HOperatorSet.AreaCenter(ho_ObjectSelected1, out hv_Area1, out hv_Row3, out hv_Column3);
                    HOperatorSet.AreaCenter(ho_ObjectSelected2, out hv_Area2, out hv_Row4, out hv_Column4);
                    if ((int)(new HTuple(hv_Column3.TupleGreater(hv_Column4))) != 0)
                    {
                        OTemp[SP_O] = ho_Last.CopyObj(1, -1);
                        SP_O++;
                        ho_Last.Dispose();
                        HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_ObjectSelected1, out ho_Last);
                        OTemp[SP_O - 1].Dispose();
                        SP_O = 0;
                    }
                    else
                    {
                        OTemp[SP_O] = ho_Last.CopyObj(1, -1);
                        SP_O++;
                        ho_Last.Dispose();
                        HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_ObjectSelected2, out ho_Last);
                        OTemp[SP_O - 1].Dispose();
                        SP_O = 0;
                    }

                    HOperatorSet.AreaCenter(ho_First, out hv_Area3, out hv_Row5, out hv_Column5);
                    HOperatorSet.AreaCenter(ho_Last, out hv_Area4, out hv_Row6, out hv_Column6);

                    ho_Rectangle1.Dispose();

                    if (hv_Row5 < hv_Row6)
                    {
                        ho_Rectangle1.Dispose();
                        HOperatorSet.GenRectangle1(out ho_Rectangle1, hv_Row5, hv_Column5, hv_Row6,
                            hv_Column5);
                        ho_Rectangle2.Dispose();
                        HOperatorSet.GenRectangle1(out ho_Rectangle2, hv_Row6, hv_Column5, hv_Row6,
                            hv_Column6);
                    }
                    else
                    {
                        ho_Rectangle1.Dispose();
                        HOperatorSet.GenRectangle1(out ho_Rectangle1, hv_Row6, hv_Column6, hv_Row5,
                            hv_Column6);
                        ho_Rectangle2.Dispose();
                        HOperatorSet.GenRectangle1(out ho_Rectangle2, hv_Row5, hv_Column5, hv_Row5,
                            hv_Column6);
                    }

                    hv_length = (hv_Column.TupleMax()) - (hv_Column.TupleMin());
                }
                else
                {
                    ho_SortedRegions.Dispose();
                    HOperatorSet.SortRegion(ho_RegionTrans, out ho_SortedRegions, "character",
                        "true", "row");
                    HOperatorSet.AreaCenter(ho_SortedRegions, out hv_Area, out hv_Row, out hv_Column);
                    HOperatorSet.CountObj(ho_SortedRegions, out hv_Number);
                    ho_First.Dispose();
                    HOperatorSet.GenEmptyObj(out ho_First);
                    ho_Last.Dispose();
                    HOperatorSet.GenEmptyObj(out ho_Last);
                    ho_ObjectSelected1.Dispose();
                    HOperatorSet.SelectObj(ho_SortedRegions, out ho_ObjectSelected1, 1);
                    ho_ObjectSelected2.Dispose();
                    HOperatorSet.SelectObj(ho_SortedRegions, out ho_ObjectSelected2, 2);
                    HOperatorSet.AreaCenter(ho_ObjectSelected1, out hv_Area1, out hv_Row3, out hv_Column3);
                    HOperatorSet.AreaCenter(ho_ObjectSelected2, out hv_Area2, out hv_Row4, out hv_Column4);
                    if ((int)(new HTuple(hv_Row3.TupleLess(hv_Row4))) != 0)
                    {
                        OTemp[SP_O] = ho_First.CopyObj(1, -1);
                        SP_O++;
                        ho_First.Dispose();
                        HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_ObjectSelected1, out ho_First);
                        OTemp[SP_O - 1].Dispose();
                        SP_O = 0;
                    }
                    else
                    {
                        OTemp[SP_O] = ho_First.CopyObj(1, -1);
                        SP_O++;
                        ho_First.Dispose();
                        HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_ObjectSelected2, out ho_First);
                        OTemp[SP_O - 1].Dispose();
                        SP_O = 0;
                    }

                    ho_ObjectSelected1.Dispose();
                    HOperatorSet.SelectObj(ho_SortedRegions, out ho_ObjectSelected1, hv_Number - 1);
                    ho_ObjectSelected2.Dispose();
                    HOperatorSet.SelectObj(ho_SortedRegions, out ho_ObjectSelected2, hv_Number);
                    HOperatorSet.AreaCenter(ho_ObjectSelected1, out hv_Area1, out hv_Row3, out hv_Column3);
                    HOperatorSet.AreaCenter(ho_ObjectSelected2, out hv_Area2, out hv_Row4, out hv_Column4);
                    if ((int)(new HTuple(hv_Row3.TupleGreater(hv_Row4))) != 0)
                    {
                        OTemp[SP_O] = ho_Last.CopyObj(1, -1);
                        SP_O++;
                        ho_Last.Dispose();
                        HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_ObjectSelected1, out ho_Last);
                        OTemp[SP_O - 1].Dispose();
                        SP_O = 0;
                    }
                    else
                    {
                        OTemp[SP_O] = ho_Last.CopyObj(1, -1);
                        SP_O++;
                        ho_Last.Dispose();
                        HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_ObjectSelected2, out ho_Last);
                        OTemp[SP_O - 1].Dispose();
                        SP_O = 0;
                    }

                    HOperatorSet.AreaCenter(ho_First, out hv_Area3, out hv_Row5, out hv_Column5);
                    HOperatorSet.AreaCenter(ho_Last, out hv_Area4, out hv_Row6, out hv_Column6);
                    if (hv_Row5 < hv_Row6)
                    {
                        ho_Rectangle1.Dispose();
                        HOperatorSet.GenRectangle1(out ho_Rectangle1, hv_Row5, hv_Column5, hv_Row6,
                            hv_Column5);
                        ho_Rectangle2.Dispose();
                        HOperatorSet.GenRectangle1(out ho_Rectangle2, hv_Row6, hv_Column5, hv_Row6,
                            hv_Column6);
                    }
                    else
                    {
                        ho_Rectangle1.Dispose();
                        HOperatorSet.GenRectangle1(out ho_Rectangle1, hv_Row6, hv_Column6, hv_Row5,
                            hv_Column6);
                        ho_Rectangle2.Dispose();
                        HOperatorSet.GenRectangle1(out ho_Rectangle2, hv_Row5, hv_Column5, hv_Row5,
                            hv_Column6);
                    }
                    hv_length = (hv_Row.TupleMax()) - (hv_Row.TupleMin());
                }
                ho_RegionOut.Dispose();
                HOperatorSet.GenEmptyObj(out ho_RegionOut);
                OTemp[SP_O] = ho_RegionOut.CopyObj(1, -1);
                SP_O++;
                ho_RegionOut.Dispose();
                HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Rectangle1, out ho_RegionOut);
                OTemp[SP_O - 1].Dispose();
                SP_O = 0;
                OTemp[SP_O] = ho_RegionOut.CopyObj(1, -1);
                SP_O++;
                ho_RegionOut.Dispose();
                HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Rectangle2, out ho_RegionOut);
                OTemp[SP_O - 1].Dispose();
                SP_O = 0;
                OTemp[SP_O] = ho_RegionOut.CopyObj(1, -1);
                SP_O++;
                ho_RegionOut.Dispose();
                HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_First, out ho_RegionOut);
                OTemp[SP_O - 1].Dispose();
                SP_O = 0;
                OTemp[SP_O] = ho_RegionOut.CopyObj(1, -1);
                SP_O++;
                ho_RegionOut.Dispose();
                HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_Last, out ho_RegionOut);
                OTemp[SP_O - 1].Dispose();
                SP_O = 0;
                ho_RegionClosing.Dispose();
                ho_RegionDifference.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionTrans.Dispose();
                ho_SortedRegions.Dispose();
                ho_First.Dispose();
                ho_Last.Dispose();
                ho_ObjectSelected1.Dispose();
                ho_ObjectSelected2.Dispose();
                ho_Rectangle1.Dispose();
                ho_Rectangle2.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_RegionClosing.Dispose();
                ho_RegionDifference.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionTrans.Dispose();
                ho_SortedRegions.Dispose();
                ho_First.Dispose();
                ho_Last.Dispose();
                ho_ObjectSelected1.Dispose();
                ho_ObjectSelected2.Dispose();
                ho_Rectangle1.Dispose();
                ho_Rectangle2.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Main procedure 
        private void action()
        {
            // Local iconic variables 
            HObject ho_Rectangle;
            HObject ho_RegionFillUp, ho_RegionOut, Region;
            // Local control variables 
            HTuple hv_length = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_RegionOut);
            HOperatorSet.GenEmptyObj(out Region);
            try
            {
                try
                {
                    ho_Rectangle.Dispose();
                    HOperatorSet.GenRectangle1(out ho_Rectangle, this.rr1, rc1, rr2,
                        rc2);
                    Region.Dispose();
                    HOperatorSet.Intersection(ho_Rectangle, this.algorithm.Region, out Region);

                    ho_RegionFillUp.Dispose();
                    HOperatorSet.FillUp(Region, out ho_RegionFillUp);
                    ho_RegionOut.Dispose();
                    screwLength(ho_RegionFillUp, out ho_RegionOut, tr1, tc1, tr2,
                        tc2, hv_vOrh, out hv_length);
                    if (ho_RegionOut.IsInitialized())
                    {
                        RegionToDisp.Dispose();
                        HOperatorSet.Union1(ho_RegionOut, out RegionToDisp);
                        //HOperatorSet.ConcatObj(ho_RegionOut, RegionToDisp, out RegionToDisp);
                    }
                }
                catch (Exception e)
                {
                    hv_length = 0;
                }
                finally
                {
                    HTuple hv_result = new HTuple();
                    hv_result = hv_result.TupleConcat("螺牙长度");
                    hv_result = hv_result.TupleConcat(hv_length * pixeldist);
                    result = hv_result.Clone();
                }
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_Rectangle.Dispose();
                ho_RegionFillUp.Dispose();
                ho_RegionOut.Dispose();
                throw HDevExpDefaultException;
            }
            finally
            {
                ho_Rectangle.Dispose();
                ho_RegionFillUp.Dispose();
                ho_RegionOut.Dispose();
            }
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