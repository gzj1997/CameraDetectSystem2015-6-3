using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
namespace CameraDetectSystem
{
    [Serializable]
    class PartHeight : ImageTools
    {
        #region ROI
        [NonSerialized]
        HTuple Row1 = new HTuple();
        [NonSerialized]
        HTuple Column1 = new HTuple();
        [NonSerialized]
        HTuple Row2 = new HTuple();
        [NonSerialized]
        HTuple Column2 = new HTuple();
        public double R1 { set; get; }
        public double R2 { set; get; }
        public double C1 { set; get; }
        public double C2 { set; get; }
        #endregion
        public PartHeight()
        {
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public PartHeight(HObject Image, Algorithm al)
        {
            this.Image = Image; this.algorithm.Image = Image; this.algorithm = al;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
            pixeldist = 1;
        }

        public override void draw()
        {
            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            //HOperatorSet.SetDraw(LWindowHandle, "margin");
            //HOperatorSet.SetColor(LWindowHandle, "green");
            HalconHelp.disp_message(LWindowHandle, "选取需要检测部分的最宽处", "window",
                   12, 12, "black", "true");
            HOperatorSet.DrawRectangle1(this.LWindowHandle, out Row1, out Column1, out Row2, out Column2);
            this.R1 = Row1.D;
            this.R2 = Row2.D;
            this.C1 = Column1.D;
            this.C2 = Column2.D;
        }

        public void gen_arrow_contour_xld(out HObject ho_Arrow, HTuple hv_Row1, HTuple hv_Column1,
    HTuple hv_Row2, HTuple hv_Column2, HTuple hv_HeadLength, HTuple hv_HeadWidth)
        {


            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];
            long SP_O = 0;

            // Local iconic variables 

            HObject ho_TempArrow = null;


            // Local control variables 

            HTuple hv_Length, hv_ZeroLengthIndices, hv_DR;
            HTuple hv_DC, hv_HalfHeadWidth, hv_RowP1, hv_ColP1, hv_RowP2;
            HTuple hv_ColP2, hv_Index;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Arrow);
            HOperatorSet.GenEmptyObj(out ho_TempArrow);

            //This procedure generates arrow shaped XLD contours,
            //pointing from (Row1, Column1) to (Row2, Column2).
            //If starting and end point are identical, a contour consisting
            //of a single point is returned.
            //
            //input parameteres:
            //Row1, Column1: Coordinates of the arrows' starting points
            //Row2, Column2: Coordinates of the arrows' end points
            //HeadLength, HeadWidth: Size of the arrow heads in pixels
            //
            //output parameter:
            //Arrow: The resulting XLD contour
            //
            //The input tuples Row1, Column1, Row2, and Column2 have to be of
            //the same length.
            //HeadLength and HeadWidth either have to be of the same length as
            //Row1, Column1, Row2, and Column2 or have to be a single element.
            //If one of the above restrictions is violated, an error will occur.
            //
            //
            //Init
            ho_Arrow.Dispose();
            HOperatorSet.GenEmptyObj(out ho_Arrow);
            //
            //Calculate the arrow length
            HOperatorSet.DistancePp(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_Length);
            //
            //Mark arrows with identical start and end point
            //(set Length to -1 to avoid division-by-zero exception)
            hv_ZeroLengthIndices = hv_Length.TupleFind(0);
            if ((int)(new HTuple(hv_ZeroLengthIndices.TupleNotEqual(-1))) != 0)
            {
                hv_Length[hv_ZeroLengthIndices] = -1;
            }
            //
            //Calculate auxiliary variables.
            hv_DR = (1.0 * (hv_Row2 - hv_Row1)) / hv_Length;
            hv_DC = (1.0 * (hv_Column2 - hv_Column1)) / hv_Length;
            hv_HalfHeadWidth = hv_HeadWidth / 2.0;
            //
            //Calculate end points of the arrow head.
            hv_RowP1 = (hv_Row1 + ((hv_Length - hv_HeadLength) * hv_DR)) + (hv_HalfHeadWidth * hv_DC);
            hv_ColP1 = (hv_Column1 + ((hv_Length - hv_HeadLength) * hv_DC)) - (hv_HalfHeadWidth * hv_DR);
            hv_RowP2 = (hv_Row1 + ((hv_Length - hv_HeadLength) * hv_DR)) - (hv_HalfHeadWidth * hv_DC);
            hv_ColP2 = (hv_Column1 + ((hv_Length - hv_HeadLength) * hv_DC)) + (hv_HalfHeadWidth * hv_DR);
            //
            //Finally create output XLD contour for each input point pair
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Length.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
            {
                if ((int)(new HTuple(((hv_Length.TupleSelect(hv_Index))).TupleEqual(-1))) != 0)
                {
                    //Create_ single points for arrows with identical start and end point
                    ho_TempArrow.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_TempArrow, hv_Row1.TupleSelect(hv_Index),
                        hv_Column1.TupleSelect(hv_Index));
                }
                else
                {
                    //Create arrow contour
                    ho_TempArrow.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_TempArrow, ((((((((((hv_Row1.TupleSelect(
                        hv_Index))).TupleConcat(hv_Row2.TupleSelect(hv_Index)))).TupleConcat(
                        hv_RowP1.TupleSelect(hv_Index)))).TupleConcat(hv_Row2.TupleSelect(hv_Index)))).TupleConcat(
                        hv_RowP2.TupleSelect(hv_Index)))).TupleConcat(hv_Row2.TupleSelect(hv_Index)),
                        ((((((((((hv_Column1.TupleSelect(hv_Index))).TupleConcat(hv_Column2.TupleSelect(
                        hv_Index)))).TupleConcat(hv_ColP1.TupleSelect(hv_Index)))).TupleConcat(
                        hv_Column2.TupleSelect(hv_Index)))).TupleConcat(hv_ColP2.TupleSelect(
                        hv_Index)))).TupleConcat(hv_Column2.TupleSelect(hv_Index)));
                }
                OTemp[SP_O] = ho_Arrow.CopyObj(1, -1);
                SP_O++;
                ho_Arrow.Dispose();
                HOperatorSet.ConcatObj(OTemp[SP_O - 1], ho_TempArrow, out ho_Arrow);
                OTemp[SP_O - 1].Dispose();
                SP_O = 0;
            }
            ho_TempArrow.Dispose();

            return;
        }

        public void length1(HObject ho_RegionIntersection, HObject ho_RegionDifference,
        out HObject ho_ObjectsConcat, out HTuple hv_length)
        {


            // Local iconic variables 

            HObject ho_RegionOpening = null, ho_Arrow1 = null;
            HObject ho_Arrow2 = null, ho_Region1 = null, ho_Region2 = null;


            // Local control variables 

            HTuple hv_Rows = new HTuple(), hv_Columns = new HTuple();
            HTuple hv_Rows1 = new HTuple(), hv_Columns1 = new HTuple();
            HTuple hv_Exception;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ObjectsConcat);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening);
            HOperatorSet.GenEmptyObj(out ho_Arrow1);
            HOperatorSet.GenEmptyObj(out ho_Arrow2);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_Region2);

            hv_length = new HTuple();
            try
            {
                try
                {
                    HOperatorSet.GetRegionPoints(ho_RegionIntersection, out hv_Rows, out hv_Columns);

                    //水平
                    ho_RegionOpening.Dispose();
                    HOperatorSet.OpeningRectangle1(ho_RegionDifference, out ho_RegionOpening,
                        (hv_Columns.TupleMax()) - (hv_Columns.TupleMin()+30), (hv_Rows.TupleMax()) - (hv_Rows.TupleMin()+30
                        ));
                    HOperatorSet.GetRegionPoints(ho_RegionOpening, out hv_Rows1, out hv_Columns1);
                    if ((int)(new HTuple((new HTuple(hv_Rows1.TupleLength())).TupleGreater(0))) == 0)
                    {
                    
                        hv_length = 0;
                        return;
                    }
                    if ((int)(new HTuple((new HTuple(hv_Columns1.TupleLength())).TupleGreater(0))) == 0)
                    {

                        hv_length = 0;
                        return;
                    }
                    ho_Arrow1.Dispose();
                    gen_arrow_contour_xld(out ho_Arrow1, hv_Rows1.TupleMax(), (hv_Columns1.TupleMax()
                        ) + 20, hv_Rows1.TupleMax(), hv_Columns1.TupleMax(), 5, 5);
                    ho_Arrow2.Dispose();
                    gen_arrow_contour_xld(out ho_Arrow2, hv_Rows1.TupleMin(), (hv_Columns1.TupleMin()
                        ) - 20, hv_Rows1.TupleMin(), hv_Columns1.TupleMin(), 5, 5);
                    ho_Region1.Dispose();
                    HOperatorSet.GenRegionContourXld(ho_Arrow1, out ho_Region1, "filled");
                    ho_Region2.Dispose();
                    HOperatorSet.GenRegionContourXld(ho_Arrow2, out ho_Region2, "filled");
                    ho_ObjectsConcat.Dispose();
                    HOperatorSet.ConcatObj(ho_Region1, ho_Region2, out ho_ObjectsConcat);

                    if ((int)(new HTuple((new HTuple(hv_Columns1.TupleLength())).TupleGreater(0))) != 0)
                    {
                        hv_length = (hv_Columns1.TupleMax()) - (hv_Columns1.TupleMin());
                    }

                    else
                    {

                        hv_length = 0;
                    
                    }
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    hv_length = 0;
                }

                ho_RegionOpening.Dispose();
                ho_Arrow1.Dispose();
                ho_Arrow2.Dispose();
                ho_Region1.Dispose();
                ho_Region2.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_RegionOpening.Dispose();
                ho_Arrow1.Dispose();
                ho_Arrow2.Dispose();
                ho_Region1.Dispose();
                ho_Region2.Dispose();

                throw HDevExpDefaultException;
            }
        }
        private void action()
        {
            HObject ho_RegionOpening1;
            HObject ho_RegionDifference, ho_Rectangle, ho_RegionIntersection;
            HObject ho_ObjectsConcat;


            // Local control variables 

            HTuple hv_Width, hv_Height, hv_Row1, hv_Column1;
            HTuple hv_Row2, hv_Column2, hv_length;

            // Initialize local and output iconic variables 
          
            
            HOperatorSet.GenEmptyObj(out ho_RegionOpening1);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection);
            HOperatorSet.GenEmptyObj(out ho_ObjectsConcat);

            try
            {
                HOperatorSet.GetImageSize(this.algorithm.Image, out hv_Width, out hv_Height);
                ho_RegionOpening1.Dispose();
                HOperatorSet.OpeningRectangle1(algorithm.Region, out ho_RegionOpening1, 2, hv_Height);
                ho_RegionDifference.Dispose();
                HOperatorSet.Difference(algorithm.Region, ho_RegionOpening1, out ho_RegionDifference
                    );
                
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle, R1, C1, R2,
                    C2);

                ho_RegionIntersection.Dispose();
                HOperatorSet.Intersection(ho_RegionDifference, ho_Rectangle, out ho_RegionIntersection
                    );
                ho_ObjectsConcat.Dispose();
                length1(ho_RegionIntersection, ho_RegionDifference, out ho_ObjectsConcat, out hv_length);
                hv_length *= pixeldist;
                HTuple hv_result = new HTuple();
                this.Result = new HTuple();
                hv_result = hv_result.TupleConcat("PartLenght");
                hv_result = hv_result.TupleConcat(hv_length);
                result = hv_result.Clone();
                if (ho_ObjectsConcat.IsInitialized())
                {

                    if (RegionToDisp.IsInitialized())
                    {
                        HOperatorSet.ConcatObj(ho_ObjectsConcat, RegionToDisp, out RegionToDisp);
                        //HOperatorSet.GenRegionContourXld(ho_ObjectsConcat, out RegionToDisp, "filled");
                        HOperatorSet.Union1(RegionToDisp, out RegionToDisp);
                    }
                    else
                    {
                        HOperatorSet.GenRegionContourXld( ho_ObjectsConcat, out RegionToDisp, "filled");
                        HOperatorSet.Union1(RegionToDisp, out RegionToDisp);
                    }
                }
            }
            catch (HalconException HDevExpDefaultException)
            {
                hv_length = 0;
                HTuple hv_result = new HTuple();
                this.Result = new HTuple();
                hv_result = hv_result.TupleConcat("PartLenght");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();

             
                ho_RegionOpening1.Dispose();
                ho_RegionDifference.Dispose();
                ho_Rectangle.Dispose();
                ho_RegionIntersection.Dispose();
                ho_ObjectsConcat.Dispose();

                throw HDevExpDefaultException;
            }
           
         
            ho_RegionOpening1.Dispose();
            ho_RegionDifference.Dispose();
            ho_Rectangle.Dispose();
            ho_RegionIntersection.Dispose();
            ho_ObjectsConcat.Dispose();
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
