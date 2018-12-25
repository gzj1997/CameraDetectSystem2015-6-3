using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
    [Serializable]
    class xiaomaoding1 : ImageTools
    {
        #region ROI





        [NonSerialized]
        private HTuple hv_Rows = new HTuple();
        [NonSerialized]
        private HTuple hv_Columns = new HTuple();
        [NonSerialized]
        private HTuple hv_Length1s = new HTuple();
        [NonSerialized]
        private HTuple hv_Length2s = new HTuple();


        public double DRows { set; get; }
        public double DColumns { set; get; }
        public double DLength1s { set; get; }
        public double DLength2s { set; get; }


        [NonSerialized]
        private HTuple thresholdValue = new HTuple();

        public double thv { set; get; }



        







        #endregion
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public xiaomaoding1()
        {
            //RegionToDisp = Image;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public xiaomaoding1(HObject Image, Algorithm al)
        {
            gexxs = 1;
            gex = 0;
            this.Image = Image;
            this.algorithm.Image = Image;
            this.algorithm = al;
            pixeldist = 1;
        }
        //}
     
        public override void draw()
        {

            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            thv = thresholdValue.D;
            //HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\mianjixx", out mianjixx);
            //mjxx = mianjixx.D;
            //HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\mianjisx", out mianjisx);
            //mjsx = mianjisx.D;
            //disp_message(this.LWindowHandle, "请依次在右，左，上，下镜子中绘制检测区域", "window", 12, 12, "black", "true");

           

            HOperatorSet.DrawRectangle1(this.LWindowHandle, out hv_Rows, out hv_Columns, out hv_Length1s, out hv_Length2s);


            this.DRows = hv_Rows.D;
            this.DColumns = hv_Columns.D;
            this.DLength1s = hv_Length1s.D;
            this.DLength2s = hv_Length2s.D;




        }

        private void action()
        {

            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_Rectangle, ho_Region;
            HObject ho_ImageReduced, ho_Region1, ho_ConnectedRegions;
            HObject ho_SelectedRegions, ho_RegionDifference, ho_ConnectedRegions1;
            HObject ho_SelectedRegions1, ho_RegionLines;

            // Local control variables 

            HTuple hv_MinDistance = null, hv_Row11 = null;
            HTuple hv_Column11 = null, hv_Row21 = null, hv_Column21 = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_RegionLines);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {
                ho_Rectangle.Dispose();
                HOperatorSet.GenRectangle1(out ho_Rectangle, DRows, DColumns, DLength1s, DLength2s);

                ho_Region.Dispose();
                HOperatorSet.GenRegionPoints(out ho_Region, DRows, DColumns);

                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Rectangle, out ho_ImageReduced);
                ho_Region1.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region1, thv, 255);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Region1, out ho_ConnectedRegions);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShapeStd(ho_ConnectedRegions, out ho_SelectedRegions, "max_area",
                    70);
                ho_RegionDifference.Dispose();
                HOperatorSet.Difference(ho_Region1, ho_SelectedRegions, out ho_RegionDifference
                    );
                ho_ConnectedRegions1.Dispose();
                HOperatorSet.Connection(ho_RegionDifference, out ho_ConnectedRegions1);
                ho_SelectedRegions1.Dispose();
                HOperatorSet.SelectShapeStd(ho_ConnectedRegions1, out ho_SelectedRegions1, "max_area",
                    70);
                HOperatorSet.DistanceRrMin(ho_SelectedRegions, ho_SelectedRegions1, out hv_MinDistance,
                    out hv_Row11, out hv_Column11, out hv_Row21, out hv_Column21);
                ho_RegionLines.Dispose();
                HOperatorSet.GenRegionLine(out ho_RegionLines, hv_Row11, hv_Column11, hv_Row21,
                    hv_Column21);
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.Union2(ho_Region, ho_RegionLines, out ExpTmpOutVar_0);
                    ho_Region.Dispose();
                    ho_Region = ExpTmpOutVar_0;
                }

                //HOperatorSet.ClearShapeModel(hv_ModelID);
                HOperatorSet.Union1(ho_Region, out RegionToDisp);

                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("距离");
                hv_result = hv_result.TupleConcat(hv_MinDistance.D*pixeldist);
                result = hv_result.Clone();
                ho_Rectangle.Dispose();
                ho_Region.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionDifference.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionLines.Dispose();
                algorithm.Region.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("距离");
                hv_result = hv_result.TupleConcat(0);
                result = hv_result.Clone();
                ho_Rectangle.Dispose();
                ho_Region.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionDifference.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionLines.Dispose();
                algorithm.Region.Dispose();

            }
            finally
            {
                ho_Rectangle.Dispose();
                ho_Region.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region1.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionDifference.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionLines.Dispose();
                algorithm.Region.Dispose();
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




