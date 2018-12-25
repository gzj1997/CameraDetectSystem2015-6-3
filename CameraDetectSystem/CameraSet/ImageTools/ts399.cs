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
    class ts399 : ImageTools
    {

        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public ts399()
        {
            RegionToDisp = Image;
        }
        public ts399(HObject Image, Algorithm al)
        {
            gex = 0;
            this.algorithm = al;

            this.Image = Image;
            RegionToDisp = Image;
            pixeldist = 1;
        }
        public override void draw()
        {

        }
        private void action()
        {
            // Local iconic variables 

            HObject ho_Image, ho_ImageMean, ho_RegionDynThresh;
            HObject ho_RegionOpening, ho_ConnectedRegions, ho_SelectedRegions1;
            HObject ho_Rectangle, ho_Region1, ho_ConnectedRegions2;
            HObject ho_SelectedRegions3, ho_Circle;

            // Local control variables 

            HTuple hv_Row1 = null, hv_Column1 = null, hv_Phi = null;
            HTuple hv_Length1 = null, hv_Length2 = null, hv_Row = null;
            HTuple hv_Column = null, hv_Radius = null, hv_DistanceMin = null;
            HTuple hv_DistanceMax = null, hv_Distance1 = null, hv_RowArr = null;
            HTuple hv_ColArr = null, hv_RadiusArr = null, hv_Distance = null;
            HTuple hv_Sorted1 = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_ImageMean);
            HOperatorSet.GenEmptyObj(out ho_RegionDynThresh);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions3);
            HOperatorSet.GenEmptyObj(out ho_Circle);
            try
            {

                ho_ImageMean.Dispose();
                HOperatorSet.MeanImage(Image, out ho_ImageMean, 70, 70);
                ho_RegionDynThresh.Dispose();
                HOperatorSet.DynThreshold(Image, ho_ImageMean, out ho_RegionDynThresh, 3.5,
                    "dark");
                ho_RegionOpening.Dispose();
                HOperatorSet.OpeningCircle(ho_RegionDynThresh, out ho_RegionOpening, 4);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_RegionOpening, out ho_ConnectedRegions);
                ho_SelectedRegions1.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions1, "area",
                    "and", 150000, 9999900);
                HOperatorSet.SmallestRectangle2(ho_SelectedRegions1, out hv_Row1, out hv_Column1,
                    out hv_Phi, out hv_Length1, out hv_Length2);
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle, hv_Row1, hv_Column1,
                    hv_Phi, hv_Length1, hv_Length2);
                ho_Region1.Dispose();
                HOperatorSet.Threshold(Image, out ho_Region1, 240, 255);
                ho_ConnectedRegions2.Dispose();
                HOperatorSet.Connection(ho_Region1, out ho_ConnectedRegions2);
                ho_SelectedRegions3.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions2, out ho_SelectedRegions3, "area",
                    "and", 15000, 99999);
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetDraw(HDevWindowStack.GetActive(), "margin");
                }
                HOperatorSet.SmallestCircle(ho_SelectedRegions3, out hv_Row, out hv_Column,
                    out hv_Radius);
                ho_Circle.Dispose();
                HOperatorSet.GenCircle(out ho_Circle, hv_Row, hv_Column, hv_Radius);
                HOperatorSet.DistancePc(ho_Rectangle, hv_Row, hv_Column, out hv_DistanceMin,
                    out hv_DistanceMax);
                HOperatorSet.DistancePl(hv_Row1, hv_Column1, hv_Row, hv_Column, hv_Row, hv_Column,
                    out hv_Distance1);
                hv_RowArr = new HTuple();
                hv_ColArr = new HTuple();
                hv_RadiusArr = new HTuple();
                hv_RowArr = hv_RowArr.TupleConcat(hv_Row);
                hv_ColArr = hv_ColArr.TupleConcat(hv_Column);
                hv_RadiusArr = hv_RadiusArr.TupleConcat(hv_Radius);
                HOperatorSet.DistancePp(hv_RowArr.TupleSelect(0), hv_ColArr.TupleSelect(0),
                    hv_RowArr.TupleSelect(1), hv_ColArr.TupleSelect(1), out hv_Distance);
                HOperatorSet.TupleSort(hv_RadiusArr, out hv_Sorted1);
                

                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("孔距L1");
                hv_result = hv_result.TupleConcat(hv_Distance.D * pixeldist);
                hv_result = hv_result.TupleConcat("直径1");
                hv_result = hv_result.TupleConcat(((hv_Sorted1.TupleSelect(0)).D * pixeldist) * 2);
                hv_result = hv_result.TupleConcat("直径2");
                hv_result = hv_result.TupleConcat(((hv_Sorted1.TupleSelect(1)).D * pixeldist) * 2);
                hv_result = hv_result.TupleConcat("长度L");
                hv_result = hv_result.TupleConcat((hv_Length1.D * pixeldist) * 2);
                hv_result = hv_result.TupleConcat("宽度W");
                hv_result = hv_result.TupleConcat((hv_Length2.D * pixeldist) * 2);
                hv_result = hv_result.TupleConcat("圆心1到边缘1");
                hv_result = hv_result.TupleConcat((hv_DistanceMin.TupleSelect(0)).D * pixeldist);
                hv_result = hv_result.TupleConcat("圆心2到边缘2");
                hv_result = hv_result.TupleConcat((hv_DistanceMin.TupleSelect(1)).D * pixeldist);
                result = hv_result.Clone();

            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("孔距L1");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("直径1");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("直径2");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("长度L");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("宽度W");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("圆心1到边缘1");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("圆心2到边缘2");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();

            }
            finally
            {
                //ho_Image.Dispose();
                ho_ImageMean.Dispose();
                ho_RegionDynThresh.Dispose();
                ho_RegionOpening.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_Rectangle.Dispose();
                ho_Region1.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_SelectedRegions3.Dispose();
                ho_Circle.Dispose();
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