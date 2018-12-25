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
    class ts122 : ImageTools
    {
        
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public ts122()
        {
            RegionToDisp = Image;
        }
        public ts122(HObject Image, Algorithm al)
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

            HObject ho_ImageMean, ho_RegionDynThresh;
            HObject ho_RegionOpening, ho_ConnectedRegions, ho_SelectedRegions1;
            HObject ho_Rectangle, ho_Region1, ho_ConnectedRegions2;
            HObject ho_SelectedRegions3, ho_Circle, ho_SelectedRegions2;
            HObject ho_Circle2;

            // Local control variables 

            HTuple hv_Row1 = null, hv_Column1 = null, hv_Phi = null;
            HTuple hv_Length1 = null, hv_Length2 = null, hv_Row = null;
            HTuple hv_Column = null, hv_Radius = null, hv_Row2 = null;
            HTuple hv_Column2 = null, hv_Radius2 = null, hv_DistanceMin = null;
            HTuple hv_DistanceMax = null, hv_RowArr = null, hv_ColArr = null;
            HTuple hv_RadiusArr = null, hv_Distance1 = null, hv_Distance2 = null;
            HTuple hv_Distance3 = null, hv_Distance4 = null, hv_Distance5 = null;
            HTuple hv_Distance6 = null, hv_DistanceArr = null, hv_Sorted = null;
            HTuple hv_Sorted1 = null;
            // Initialize local and output iconic variables 
            //HOperatorSet.GenEmptyObj(out ho_Image);
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
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_Circle2);
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
          "and", 150, 99999);
      if (HDevWindowStack.IsOpen())
      {
        HOperatorSet.SetDraw(HDevWindowStack.GetActive(), "margin");
      }
      HOperatorSet.SmallestCircle(ho_SelectedRegions3, out hv_Row, out hv_Column, 
          out hv_Radius);
      ho_Circle.Dispose();
      HOperatorSet.GenCircle(out ho_Circle, hv_Row, hv_Column, hv_Radius);
      ho_SelectedRegions2.Dispose();
      HOperatorSet.SelectShape(ho_ConnectedRegions2, out ho_SelectedRegions2, "area", 
          "and", 70000, 99999);
      HOperatorSet.SmallestCircle(ho_SelectedRegions2, out hv_Row2, out hv_Column2, 
          out hv_Radius2);
      ho_Circle2.Dispose();
      HOperatorSet.GenCircle(out ho_Circle2, hv_Row2, hv_Column2, hv_Radius2);
      HOperatorSet.DistancePc(ho_Rectangle, hv_Row2, hv_Column2, out hv_DistanceMin, 
          out hv_DistanceMax);
      hv_RowArr = new HTuple();
      hv_ColArr = new HTuple();
      hv_RadiusArr = new HTuple();
      hv_RowArr = hv_RowArr.TupleConcat(hv_Row);
      hv_ColArr = hv_ColArr.TupleConcat(hv_Column);
      hv_RadiusArr = hv_RadiusArr.TupleConcat(hv_Radius);
      //*disp_line(3600,RowArr[0], ColArr[0], RowArr[1], ColArr[1])
      HOperatorSet.DistancePp(hv_RowArr.TupleSelect(0), hv_ColArr.TupleSelect(0), 
          hv_RowArr.TupleSelect(1), hv_ColArr.TupleSelect(1), out hv_Distance1);
      //*disp_line(3600,RowArr[0], ColArr[0], RowArr[2], ColArr[2])
      HOperatorSet.DistancePp(hv_RowArr.TupleSelect(0), hv_ColArr.TupleSelect(0), 
          hv_RowArr.TupleSelect(2), hv_ColArr.TupleSelect(2), out hv_Distance2);
      //*disp_line(3600,RowArr[1], ColArr[1], RowArr[3], ColArr[3])
      HOperatorSet.DistancePp(hv_RowArr.TupleSelect(1), hv_ColArr.TupleSelect(1), 
          hv_RowArr.TupleSelect(3), hv_ColArr.TupleSelect(3), out hv_Distance3);
      //*disp_line(3600,RowArr[2], ColArr[2], RowArr[3], ColArr[3])
      HOperatorSet.DistancePp(hv_RowArr.TupleSelect(2), hv_ColArr.TupleSelect(2), 
          hv_RowArr.TupleSelect(3), hv_ColArr.TupleSelect(3), out hv_Distance4);
      HOperatorSet.DistancePp(hv_RowArr.TupleSelect(1), hv_ColArr.TupleSelect(1), 
          hv_RowArr.TupleSelect(2), hv_ColArr.TupleSelect(2), out hv_Distance5);
      HOperatorSet.DistancePp(hv_RowArr.TupleSelect(0), hv_ColArr.TupleSelect(0), 
          hv_RowArr.TupleSelect(3), hv_ColArr.TupleSelect(3), out hv_Distance6);
      hv_DistanceArr = new HTuple();
      hv_DistanceArr = hv_DistanceArr.TupleConcat(hv_Distance1);
      hv_DistanceArr = hv_DistanceArr.TupleConcat(hv_Distance2);
      hv_DistanceArr = hv_DistanceArr.TupleConcat(hv_Distance3);
      hv_DistanceArr = hv_DistanceArr.TupleConcat(hv_Distance4);
      hv_DistanceArr = hv_DistanceArr.TupleConcat(hv_Distance5);
      hv_DistanceArr = hv_DistanceArr.TupleConcat(hv_Distance6);
      HOperatorSet.TupleSort(hv_DistanceArr, out hv_Sorted);
      //disp_message(3600, "L1="+((hv_Sorted.TupleSelect(0))*pixeldist), "window", 10, 
      //    10, "green", "false");
      //disp_message(3600, "L2="+((hv_Sorted.TupleSelect(1))*pixeldist), "window", 30, 
      //    10, "green", "false");
      //disp_message(3600, "L3="+((hv_Sorted.TupleSelect(2))*pixeldist), "window", 50, 
      //    10, "green", "false");
      //disp_message(3600, "L4="+((hv_Sorted.TupleSelect(3))*pixeldist), "window", 70, 
      //    10, "green", "false");
      HOperatorSet.TupleSort(hv_RadiusArr, out hv_Sorted1);
      //disp_message(3600, "D1="+(((hv_Sorted1.TupleSelect(0))*pixeldist)*2), "window", 
      //    90, 10, "green", "false");
      //disp_message(3600, "D2="+(((hv_Sorted1.TupleSelect(1))*pixeldist)*2), "window", 
      //    110, 10, "green", "false");
      //disp_message(3600, "D3="+(((hv_Sorted1.TupleSelect(2))*pixeldist)*2), "window", 
      //    130, 10, "green", "false");
      //disp_message(3600, "D4="+(((hv_Sorted1.TupleSelect(3))*pixeldist)*2), "window", 
      //    150, 10, "green", "false");
      //disp_message(3600, "L="+((hv_Length1*pixeldist)*2), "window", 170, 10, "green", 
      //    "false");
      //disp_message(3600, "W="+((hv_Length2*pixeldist)*2), "window", 190, 10, "green", 
      //    "false");
      //disp_message(3600, "Ls1="+((hv_DistanceMin.TupleSelect(0))*pixeldist), "window", 
      //    210, 10, "green", "false");
      //disp_message(3600, "Ls2="+((hv_DistanceMin.TupleSelect(1))*pixeldist), "window", 
      //    230, 10, "green", "false");
    //}

               
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("孔距L1");
                hv_result = hv_result.TupleConcat((hv_Sorted.TupleSelect(0)).D*pixeldist);
                hv_result = hv_result.TupleConcat("孔距L2");
                hv_result = hv_result.TupleConcat((hv_Sorted.TupleSelect(1)).D*pixeldist);
                hv_result = hv_result.TupleConcat("孔距L3");
                hv_result = hv_result.TupleConcat((hv_Sorted.TupleSelect(2)).D*pixeldist);
                hv_result = hv_result.TupleConcat("孔距L4");
                hv_result = hv_result.TupleConcat((hv_Sorted.TupleSelect(3)).D*pixeldist);
                hv_result = hv_result.TupleConcat("直径1");
                hv_result = hv_result.TupleConcat(((hv_Sorted1.TupleSelect(0)).D*pixeldist)*2);
                hv_result = hv_result.TupleConcat("直径2");
                hv_result = hv_result.TupleConcat(((hv_Sorted1.TupleSelect(1)).D*pixeldist)*2);
                hv_result = hv_result.TupleConcat("直径3");
                hv_result = hv_result.TupleConcat(((hv_Sorted1.TupleSelect(2)).D*pixeldist)*2);
                hv_result = hv_result.TupleConcat("直径4");
                hv_result = hv_result.TupleConcat(((hv_Sorted1.TupleSelect(3)).D*pixeldist)*2);
                hv_result = hv_result.TupleConcat("长度L");
                hv_result = hv_result.TupleConcat((hv_Length1.D*pixeldist)*2);
                hv_result = hv_result.TupleConcat("宽度W");
                hv_result = hv_result.TupleConcat((hv_Length2.D*pixeldist)*2);
                hv_result = hv_result.TupleConcat("圆心1到边缘1");
                hv_result = hv_result.TupleConcat((hv_DistanceMin.TupleSelect(0)).D*pixeldist);
                hv_result = hv_result.TupleConcat("圆心2到边缘2");
                hv_result = hv_result.TupleConcat((hv_DistanceMin.TupleSelect(1)).D*pixeldist);
                result = hv_result.Clone();

            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("孔距L1");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("孔距L2");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("孔距L3");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("孔距L4");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("直径1");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("直径2");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("直径3");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("直径4");
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
                 ho_RegionDynThresh.Dispose();
                 ho_RegionOpening.Dispose();
                 ho_ConnectedRegions.Dispose();
                 ho_SelectedRegions1.Dispose();
                 ho_Rectangle.Dispose();
                 ho_Region1.Dispose();
                 ho_ConnectedRegions2.Dispose();
                 ho_SelectedRegions3.Dispose();
                 ho_Circle.Dispose();
                 ho_SelectedRegions2.Dispose();
                 ho_Circle2.Dispose();
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