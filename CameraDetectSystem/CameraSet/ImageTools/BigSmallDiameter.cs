using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
namespace CameraDetectSystem
{
    [Serializable]
    class BigSmallDiameter : ImageTools
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
        public BigSmallDiameter()
        {
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public BigSmallDiameter(HObject Image, Algorithm al)
        {
            this.Image = Image; 
            this.algorithm.Image = Image; 
            this.algorithm = al;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
            pixeldist = 1;
        }

        public override void draw()
        {
            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
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

        public void big_small_screwteeth(HObject ho_RegionToDetect, out HObject ho_Arrow,
            HTuple hv_blackOrwhite, HTuple hv_vOrh, out HTuple hv_big_diameter, out HTuple hv_small_diameter)
        {



            // Local iconic variables 

            HObject ho_RegionTrans, ho_RegionBorder, ho_Rectangle = null;
            HObject ho_RegionIntersection = null, ho_ConnectedRegions = null;
            HObject ho_SortedRegions = null, ho_ObjectSelected1 = null;
            HObject ho_ObjectSelected2 = null, ho_Arrow1 = null, ho_Arrow2 = null;
            HObject ho_Arrow3 = null, ho_Arrow4 = null, ho_Arrow5, ho_Arrow6;


            // Local control variables 

            HTuple hv_Row1, hv_Column1, hv_Row2, hv_Column2;
            HTuple hv_Rows = new HTuple(), hv_Columns = new HTuple(), hv_maxr1 = new HTuple();
            HTuple hv_minr1 = new HTuple(), hv_Indices1 = new HTuple();
            HTuple hv_Indices2 = new HTuple(), hv_maxr2 = new HTuple();
            HTuple hv_minr2 = new HTuple(), hv_maxc1 = new HTuple(), hv_minc1 = new HTuple();
            HTuple hv_maxc2 = new HTuple(), hv_minc2 = new HTuple();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Arrow);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans);
            HOperatorSet.GenEmptyObj(out ho_RegionBorder);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SortedRegions);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected1);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected2);
            HOperatorSet.GenEmptyObj(out ho_Arrow1);
            HOperatorSet.GenEmptyObj(out ho_Arrow2);
            HOperatorSet.GenEmptyObj(out ho_Arrow3);
            HOperatorSet.GenEmptyObj(out ho_Arrow4);
            HOperatorSet.GenEmptyObj(out ho_Arrow5);
            HOperatorSet.GenEmptyObj(out ho_Arrow6);

            hv_big_diameter = new HTuple();
            hv_small_diameter = new HTuple();

            ho_RegionTrans.Dispose();
            HOperatorSet.ShapeTrans(ho_RegionToDetect, out ho_RegionTrans, "rectangle1");

            HOperatorSet.SmallestRectangle1(ho_RegionTrans, out hv_Row1, out hv_Column1,
                out hv_Row2, out hv_Column2);

            ho_RegionBorder.Dispose();
            HOperatorSet.Boundary(ho_RegionToDetect, out ho_RegionBorder, "inner_filled");

            if ((int)(new HTuple(hv_vOrh.TupleEqual("h"))) != 0)
            {

                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle, hv_Row1, hv_Column1 + 20, hv_Row2,
                    hv_Column2 - 20);
                ho_RegionIntersection.Dispose();
                HOperatorSet.Intersection(ho_Rectangle, ho_RegionBorder, out ho_RegionIntersection
                    );
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_RegionIntersection, out ho_ConnectedRegions);

                ho_SortedRegions.Dispose();
                HOperatorSet.SortRegion(ho_ConnectedRegions, out ho_SortedRegions, "first_point",
                    "true", "row");

                ho_ObjectSelected1.Dispose();
                HOperatorSet.SelectObj(ho_SortedRegions, out ho_ObjectSelected1, 1);
                ho_ObjectSelected2.Dispose();
                HOperatorSet.SelectObj(ho_SortedRegions, out ho_ObjectSelected2, 2);
                HOperatorSet.GetRegionPoints(ho_ObjectSelected1, out hv_Rows, out hv_Columns);


                hv_maxr1 = hv_Rows.TupleMax();
                hv_minr1 = hv_Rows.TupleMin();
                HOperatorSet.TupleFind(hv_Rows, hv_maxr1, out hv_Indices1);
                HOperatorSet.TupleFind(hv_Rows, hv_minr1, out hv_Indices2);
                ho_Arrow1.Dispose();
                gen_arrow_contour_xld(out ho_Arrow1, (hv_Rows.TupleSelect(hv_Indices1.TupleSelect(
                    0))) - 50, hv_Columns.TupleSelect(hv_Indices1.TupleSelect(0)), hv_Rows.TupleSelect(
                    hv_Indices1.TupleSelect(0)), hv_Columns.TupleSelect(hv_Indices1.TupleSelect(
                    0)), 15, 15);
                ho_Arrow2.Dispose();
                gen_arrow_contour_xld(out ho_Arrow2, (hv_Rows.TupleSelect(hv_Indices2.TupleSelect(
                    0))) - 50, hv_Columns.TupleSelect(hv_Indices2.TupleSelect(0)), hv_Rows.TupleSelect(
                    hv_Indices2.TupleSelect(0)), hv_Columns.TupleSelect(hv_Indices2.TupleSelect(
                    0)), 15, 15);

                HOperatorSet.GetRegionPoints(ho_ObjectSelected2, out hv_Rows, out hv_Columns);
                hv_maxr2 = hv_Rows.TupleMax();
                hv_minr2 = hv_Rows.TupleMin();
                //maxc := max(Columns)
                //minc := min(Columns)
                //big_diameter := maxr-minr
                HOperatorSet.TupleFind(hv_Rows, hv_maxr2, out hv_Indices1);
                HOperatorSet.TupleFind(hv_Rows, hv_minr2, out hv_Indices2);
                ho_Arrow3.Dispose();
                gen_arrow_contour_xld(out ho_Arrow3, (hv_Rows.TupleSelect(hv_Indices1.TupleSelect(
                    0))) + 50, hv_Columns.TupleSelect(hv_Indices1.TupleSelect(0)), hv_Rows.TupleSelect(
                    hv_Indices1.TupleSelect(0)), hv_Columns.TupleSelect(hv_Indices1.TupleSelect(
                    0)), 15, 15);
                ho_Arrow4.Dispose();
                gen_arrow_contour_xld(out ho_Arrow4, (hv_Rows.TupleSelect(hv_Indices2.TupleSelect(
                    0))) + 50, hv_Columns.TupleSelect(hv_Indices2.TupleSelect(0)), hv_Rows.TupleSelect(
                    hv_Indices2.TupleSelect(0)), hv_Columns.TupleSelect(hv_Indices2.TupleSelect(
                    0)), 15, 15);

                hv_big_diameter = hv_maxr2 - hv_minr1;
                hv_small_diameter = hv_minr2 - hv_maxr1;
            }
            else if ((int)(new HTuple(hv_vOrh.TupleEqual("v"))) != 0)
            {
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle, hv_Row1 + 20, hv_Column1, hv_Row2 - 20,
                    hv_Column2);
                ho_RegionIntersection.Dispose();
                HOperatorSet.Intersection(ho_Rectangle, ho_RegionBorder, out ho_RegionIntersection
                    );
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_RegionIntersection, out ho_ConnectedRegions);

                ho_SortedRegions.Dispose();
                HOperatorSet.SortRegion(ho_ConnectedRegions, out ho_SortedRegions, "first_point",
                    "true", "row");

                ho_ObjectSelected1.Dispose();
                HOperatorSet.SelectObj(ho_SortedRegions, out ho_ObjectSelected1, 1);
                ho_ObjectSelected2.Dispose();
                HOperatorSet.SelectObj(ho_SortedRegions, out ho_ObjectSelected2, 2);
                HOperatorSet.GetRegionPoints(ho_ObjectSelected1, out hv_Rows, out hv_Columns);

                hv_maxc1 = hv_Columns.TupleMax();
                hv_minc1 = hv_Columns.TupleMin();

                HOperatorSet.TupleFind(hv_Columns, hv_maxc1, out hv_Indices1);
                HOperatorSet.TupleFind(hv_Columns, hv_minc1, out hv_Indices2);
                ho_Arrow1.Dispose();
                gen_arrow_contour_xld(out ho_Arrow1, hv_Rows.TupleSelect(hv_Indices1.TupleSelect(
                    0)), (hv_Columns.TupleSelect(hv_Indices1.TupleSelect(0))) - 50, hv_Rows.TupleSelect(
                    hv_Indices1.TupleSelect(0)), hv_Columns.TupleSelect(hv_Indices1.TupleSelect(
                    0)), 15, 15);
                ho_Arrow2.Dispose();
                gen_arrow_contour_xld(out ho_Arrow2, hv_Rows.TupleSelect(hv_Indices2.TupleSelect(
                    0)), (hv_Columns.TupleSelect(hv_Indices2.TupleSelect(0))) - 50, hv_Rows.TupleSelect(
                    hv_Indices2.TupleSelect(0)), hv_Columns.TupleSelect(hv_Indices2.TupleSelect(
                    0)), 15, 15);

                HOperatorSet.GetRegionPoints(ho_ObjectSelected2, out hv_Rows, out hv_Columns);

                hv_maxc2 = hv_Columns.TupleMax();
                hv_minc2 = hv_Columns.TupleMin();
                HOperatorSet.TupleFind(hv_Columns, hv_maxc2, out hv_Indices1);
                HOperatorSet.TupleFind(hv_Columns, hv_minc2, out hv_Indices2);
                ho_Arrow3.Dispose();
                gen_arrow_contour_xld(out ho_Arrow3, hv_Rows.TupleSelect(hv_Indices1.TupleSelect(
                    0)), (hv_Columns.TupleSelect(hv_Indices1.TupleSelect(0))) + 50, hv_Rows.TupleSelect(
                    hv_Indices1.TupleSelect(0)), hv_Columns.TupleSelect(hv_Indices1.TupleSelect(
                    0)), 15, 15);
                ho_Arrow4.Dispose();
                gen_arrow_contour_xld(out ho_Arrow4, hv_Rows.TupleSelect(hv_Indices2.TupleSelect(
                    0)), (hv_Columns.TupleSelect(hv_Indices2.TupleSelect(0))) + 50, hv_Rows.TupleSelect(
                    hv_Indices2.TupleSelect(0)), hv_Columns.TupleSelect(hv_Indices2.TupleSelect(
                    0)), 15, 15);

                hv_big_diameter = hv_maxc2 - hv_minc1;
                hv_small_diameter = hv_minc2 - hv_maxc1;

            }
            ho_Arrow5.Dispose();
            HOperatorSet.ConcatObj(ho_Arrow1, ho_Arrow2, out ho_Arrow5);
            ho_Arrow6.Dispose();
            HOperatorSet.ConcatObj(ho_Arrow3, ho_Arrow4, out ho_Arrow6);
            ho_Arrow.Dispose();
            HOperatorSet.ConcatObj(ho_Arrow5, ho_Arrow6, out ho_Arrow);
            ho_RegionTrans.Dispose();
            ho_RegionBorder.Dispose();
            ho_Rectangle.Dispose();
            ho_RegionIntersection.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_SortedRegions.Dispose();
            ho_ObjectSelected1.Dispose();
            ho_ObjectSelected2.Dispose();
            ho_Arrow1.Dispose();
            ho_Arrow2.Dispose();
            ho_Arrow3.Dispose();
            ho_Arrow4.Dispose();
            ho_Arrow5.Dispose();
            ho_Arrow6.Dispose();
            return;
        }

        private void action()
        {
            HObject ho_Rectangle;
            HObject ho_Region, ho_Arrow;
            HTuple hv_result = new HTuple();
            HTuple hv_big_diameter, hv_small_diameter;

            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_Arrow);
            try
            {
                ho_Rectangle.Dispose();
            HOperatorSet.GenRectangle1(out ho_Rectangle, this.R1, this.C1, this.R2, this.C2);
            if (algorithm.Region.IsInitialized())
                HOperatorSet.Intersection(ho_Rectangle, algorithm.Region, out ho_Region);
            else
            {
                //MyDebug.ShowMessage("BigSmallDiameter RegionToDisp=NULL");
            }
            ho_Arrow.Dispose();
            hv_big_diameter = 0;
            hv_small_diameter = 0;
            big_small_screwteeth(ho_Region, out ho_Arrow, "black", "h", out hv_big_diameter,
                out hv_small_diameter);

            

            if ((int)(new HTuple(hv_big_diameter.Length)) ==1)
            {
                hv_result = hv_result.TupleConcat("big_diameter");
                hv_result = hv_result.TupleConcat(hv_big_diameter * pixeldist);
                hv_result = hv_result.TupleConcat("small_diameter");
                hv_result = hv_result.TupleConcat(hv_small_diameter * pixeldist);
                
                result = hv_result.Clone();
            }
            else
            {
                hv_result = hv_result.TupleConcat("big_diameter");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("small_diameter");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();
            }
            if (ho_Arrow.IsInitialized())
            {

                if (RegionToDisp.IsInitialized())
                {
                    HOperatorSet.ConcatObj(ho_Arrow, RegionToDisp, out RegionToDisp);
                    HOperatorSet.GenRegionContourXld(ho_Arrow, out RegionToDisp, "filled");
                    HOperatorSet.Union1(RegionToDisp, out RegionToDisp);
                }
                else
                {
                    HOperatorSet.GenRegionContourXld(ho_Arrow, out RegionToDisp, "filled");
                    HOperatorSet.Union1(RegionToDisp, out RegionToDisp);
                }
            }
            }
            catch(Exception e)
            {
                this.Result = new HTuple();
                hv_result = hv_result.TupleConcat("big_diameter");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("small_diameter");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();
            }
            finally
            {
                ho_Rectangle.Dispose();
                ho_Region.Dispose();
                ho_Arrow.Dispose();
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
