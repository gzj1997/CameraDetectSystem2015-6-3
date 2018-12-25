using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
namespace CameraDetectSystem
{
    [Serializable]
    class heipiwaiquan : ImageTools
    {
        #region ROI
        [NonSerialized]
        private HTuple Row1m = new HTuple();
        [NonSerialized]
        private HTuple Col1m = new HTuple();
        [NonSerialized]
        private HTuple Radius = new HTuple();
        //[NonSerialized]
        //private HTuple thresholdValue = new HTuple();

        //public double Dthv { set; get; }
        public double DRow1m { set; get; }
        public double DCol1m { set; get; }
        public double DRadius { set; get; }

        #endregion
        private static HTuple GetHv_result()
        {
            HTuple hv_result = new HTuple();
            return hv_result;
        }
        public heipiwaiquan()
        {
            //RegionToDisp = Image;
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public heipiwaiquan(HObject Image, Algorithm al)
        {
            gexxs = 1;
            gex = 0;
            //Initial();
            this.Image = Image;
            this.algorithm.Image = Image;
            this.algorithm = al;
            //HOperatorSet.GenEmptyObj(out RegionToDisp);
            //RegionToDisp.Dispose();
            pixeldist = 1;
        }

        // double rr1, rr2, rc1, rc2;
        public override void draw()
        {
            HTuple Row1m = null, Col1m = null, Radius = null;
            HObject ho_Circle;
            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.DrawCircle(this.LWindowHandle, out Row1m, out Col1m, out Radius);
            this.DRow1m = Row1m.D;
            this.DCol1m = Col1m.D;
            this.DRadius = Radius.D;
            //  HOperatorSet.ReadTuple(PathHelper.currentProductPath + @"\thresholdValue", out thresholdValue);
            //  Dthv = thresholdValue.D;
            HOperatorSet.GenCircle(out ho_Circle, DRow1m, DCol1m, DRadius);
            ho_Circle.Dispose();//

        }
        // Main procedure 
        private void action()
        {

            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_Circle, ho_ImageReduced;
            HObject ho_Region, ho_ConnectedRegions, ho_SelectedRegions;
            HObject ho_Circle3, ho_RegionUnion1, ho_Circle1, ho_RegionErosion;
            HObject ho_RegionErosion1, ho_RegionFillUp, ho_Circle4;
            HObject ho_RegionDifference6, ho_ImageReduced1, ho_Region1 = null;
            HObject ho_ConnectedRegions1 = null, ho_RegionFillUp1, ho_RegionDifference2;
            HObject ho_ImageReduced2, ho_Region2, ho_ConnectedRegions2;
            HObject ho_SelectedRegions1, ho_Circle2, ho_RegionErosion2;
            HObject ho_Circle5, ho_RegionDifference7, ho_ImageReduced3;
            HObject ho_Region3 = null, ho_ConnectedRegions3 = null, ho_RegionFillUp3;
            HObject ho_RegionDifference5, ho_ImageReduced4, ho_Region4;
            HObject ho_ConnectedRegions4, ho_SelectedRegions2;

            // Local control variables 

            HTuple hv_Row1m = null, hv_Col1m = null, hv_Radius = null;
            HTuple hv_Row1 = null, hv_Column1 = null, hv_Radius1 = null;
            HTuple hv_Row9 = null, hv_Column9 = null, hv_Radius3 = null;
            HTuple hv_Area = null, hv_Row2 = null, hv_Column2 = null;
            HTuple hv_Mean = null, hv_Deviation = null, hv_a1 = null;
            HTuple hv_Area1 = new HTuple(), hv_Row3 = new HTuple();
            HTuple hv_Column3 = new HTuple(), hv_Sum = new HTuple();
            HTuple hv_Row4 = null, hv_Column4 = null, hv_Radius2 = null;
            HTuple hv_Row8 = null, hv_Column8 = null, hv_Radius4 = null;
            HTuple hv_Area2 = null, hv_Row5 = null, hv_Column5 = null;
            HTuple hv_Mean1 = null, hv_Deviation1 = null, hv_a2 = null;
            HTuple hv_Area3 = new HTuple(), hv_Row6 = new HTuple();
            HTuple hv_Column6 = new HTuple(), hv_Sum1 = new HTuple();
            HTuple hv_Area4 = null, hv_Row7 = null, hv_Column7 = null;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_Circle3);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion1);
            HOperatorSet.GenEmptyObj(out ho_Circle1);
            HOperatorSet.GenEmptyObj(out ho_RegionErosion);
            HOperatorSet.GenEmptyObj(out ho_RegionErosion1);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_Circle4);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference6);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
            HOperatorSet.GenEmptyObj(out ho_Region1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp1);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference2);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced2);
            HOperatorSet.GenEmptyObj(out ho_Region2);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_Circle2);
            HOperatorSet.GenEmptyObj(out ho_RegionErosion2);
            HOperatorSet.GenEmptyObj(out ho_Circle5);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference7);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced3);
            HOperatorSet.GenEmptyObj(out ho_Region3);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions3);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp3);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference5);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced4);
            HOperatorSet.GenEmptyObj(out ho_Region4);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions4);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions2);
            HOperatorSet.Union1(algorithm.Region, out RegionToDisp);
            try
            {

                ho_Circle.Dispose();
                HOperatorSet.GenCircle(out ho_Circle, DRow1m, DCol1m, DRadius);

                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(Image, ho_Circle, out ho_ImageReduced);

                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_Region, 128, 255);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);

                //外圈
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShapeStd(ho_ConnectedRegions, out ho_SelectedRegions, "max_area",
                    70);
                HOperatorSet.SmallestCircle(ho_SelectedRegions, out hv_Row1, out hv_Column1,
                    out hv_Radius1);
                ho_Circle3.Dispose();
                HOperatorSet.GenCircle(out ho_Circle3, hv_Row1, hv_Column1, 0.5);
                ho_RegionUnion1.Dispose();
                HOperatorSet.Union1(ho_Circle3, out ho_RegionUnion1);
                ho_Circle1.Dispose();
                HOperatorSet.GenCircle(out ho_Circle1, hv_Row1, hv_Column1, hv_Radius1);
                ho_RegionErosion.Dispose();
                HOperatorSet.ErosionCircle(ho_Circle1, out ho_RegionErosion, 3.5);
                ho_RegionErosion1.Dispose();
                HOperatorSet.ErosionCircle(ho_SelectedRegions, out ho_RegionErosion1, 5.5);
                ho_RegionFillUp.Dispose();
                HOperatorSet.FillUp(ho_RegionErosion1, out ho_RegionFillUp);
                HOperatorSet.SmallestCircle(ho_RegionFillUp, out hv_Row9, out hv_Column9, out hv_Radius3);
                ho_Circle4.Dispose();
                HOperatorSet.GenCircle(out ho_Circle4, hv_Row9, hv_Column9, hv_Radius3 * 0.88);
                ho_RegionDifference6.Dispose();
                HOperatorSet.Difference(ho_RegionFillUp, ho_Circle4, out ho_RegionDifference6
                    );

                //difference (RegionFillUp, RegionErosion1, RegionDifference)
                //difference (RegionErosion, RegionDifference, RegionDifference1)
                //外圈面积
                HOperatorSet.AreaCenter(ho_RegionDifference6, out hv_Area, out hv_Row2, out hv_Column2);
                ho_ImageReduced1.Dispose();
                HOperatorSet.ReduceDomain(ho_ImageReduced, ho_RegionDifference6, out ho_ImageReduced1
                    );
                HOperatorSet.Intensity(ho_RegionDifference6, ho_ImageReduced1, out hv_Mean, out hv_Deviation);
                hv_a1 = 0;
                if ((int)(new HTuple(hv_Mean.TupleLess(255))) != 0)
                {
                    ho_Region1.Dispose();
                    HOperatorSet.Threshold(ho_ImageReduced1, out ho_Region1, 128, hv_Mean - (hv_Deviation / 2));
                    //外圈缺陷
                    ho_ConnectedRegions1.Dispose();
                    HOperatorSet.Connection(ho_Region1, out ho_ConnectedRegions1);
                    HOperatorSet.AreaCenter(ho_ConnectedRegions1, out hv_Area1, out hv_Row3, out hv_Column3);
                    HOperatorSet.TupleSum(hv_Area1, out hv_Sum);
                    hv_a1 = hv_Sum.Clone();
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.Union2(ho_RegionUnion1, ho_ConnectedRegions1, out ExpTmpOutVar_0
                            );
                        ho_RegionUnion1.Dispose();
                        ho_RegionUnion1 = ExpTmpOutVar_0;
                    }
                }
                else
                {
                    hv_a1 = 0;
                }
                //中圈
                ho_RegionFillUp1.Dispose();
                HOperatorSet.FillUp(ho_SelectedRegions, out ho_RegionFillUp1);
                ho_RegionDifference2.Dispose();
                HOperatorSet.Difference(ho_RegionFillUp1, ho_SelectedRegions, out ho_RegionDifference2
                    );
                ho_ImageReduced2.Dispose();
                HOperatorSet.ReduceDomain(ho_ImageReduced, ho_RegionDifference2, out ho_ImageReduced2
                    );
                ho_Region2.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced2, out ho_Region2, 128, 255);
                ho_ConnectedRegions2.Dispose();
                HOperatorSet.Connection(ho_Region2, out ho_ConnectedRegions2);
                ho_SelectedRegions1.Dispose();
                HOperatorSet.SelectShapeStd(ho_ConnectedRegions2, out ho_SelectedRegions1, "max_area",
                    70);
                HOperatorSet.SmallestCircle(ho_SelectedRegions1, out hv_Row4, out hv_Column4,
                    out hv_Radius2);
                ho_Circle2.Dispose();
                HOperatorSet.GenCircle(out ho_Circle2, hv_Row4, hv_Column4, hv_Radius2);
                //erosion_circle (Circle2, RegionErosion2, 3.5)
                ho_RegionErosion2.Dispose();
                HOperatorSet.ErosionCircle(ho_Circle2, out ho_RegionErosion2, 5.5);
                HOperatorSet.SmallestCircle(ho_RegionErosion2, out hv_Row8, out hv_Column8, out hv_Radius4);
                ho_Circle5.Dispose();
                HOperatorSet.GenCircle(out ho_Circle5, hv_Row8, hv_Column8, hv_Radius4 * 0.85);
                ho_RegionDifference7.Dispose();
                HOperatorSet.Difference(ho_RegionErosion2, ho_Circle5, out ho_RegionDifference7
                    );



                //erosion_circle (SelectedRegions1, RegionErosion3, 6.5)
                //fill_up (RegionErosion3, RegionFillUp2)
                //difference (RegionFillUp2, RegionErosion3, RegionDifference3)
                //difference (RegionErosion2, RegionDifference3, RegionDifference4)
                //中圈面积
                HOperatorSet.AreaCenter(ho_RegionDifference7, out hv_Area2, out hv_Row5, out hv_Column5);
                ho_ImageReduced3.Dispose();
                HOperatorSet.ReduceDomain(ho_ImageReduced, ho_RegionDifference7, out ho_ImageReduced3
                    );
                HOperatorSet.Intensity(ho_RegionDifference7, ho_ImageReduced3, out hv_Mean1,
                    out hv_Deviation1);
                hv_a2 = 0;
                if ((int)(new HTuple(hv_Mean1.TupleLess(255))) != 0)
                {
                    ho_Region3.Dispose();
                    HOperatorSet.Threshold(ho_ImageReduced3, out ho_Region3, 128, hv_Mean1 - (hv_Deviation1 / 2));
                    //中圈缺陷
                    ho_ConnectedRegions3.Dispose();
                    HOperatorSet.Connection(ho_Region3, out ho_ConnectedRegions3);
                    HOperatorSet.AreaCenter(ho_ConnectedRegions3, out hv_Area3, out hv_Row6, out hv_Column6);
                    HOperatorSet.TupleSum(hv_Area3, out hv_Sum1);
                    hv_a2 = hv_Sum1.Clone();
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.Union2(ho_RegionUnion1, ho_ConnectedRegions3, out ExpTmpOutVar_0
                            );
                        ho_RegionUnion1.Dispose();
                        ho_RegionUnion1 = ExpTmpOutVar_0;
                    }
                }
                else
                {
                    hv_a2 = 0;
                }
                //内圈
                ho_RegionFillUp3.Dispose();
                HOperatorSet.FillUp(ho_SelectedRegions1, out ho_RegionFillUp3);
                ho_RegionDifference5.Dispose();
                HOperatorSet.Difference(ho_RegionFillUp3, ho_SelectedRegions1, out ho_RegionDifference5
                    );
                ho_ImageReduced4.Dispose();
                HOperatorSet.ReduceDomain(ho_ImageReduced, ho_RegionDifference5, out ho_ImageReduced4
                    );
                ho_Region4.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced4, out ho_Region4, 150, 255);
                ho_ConnectedRegions4.Dispose();
                HOperatorSet.Connection(ho_Region4, out ho_ConnectedRegions4);
                ho_SelectedRegions2.Dispose();
                HOperatorSet.SelectShapeStd(ho_ConnectedRegions4, out ho_SelectedRegions2, "max_area",
                    70);
                HOperatorSet.AreaCenter(ho_SelectedRegions2, out hv_Area4, out hv_Row7, out hv_Column7);
                HOperatorSet.Union1(ho_RegionUnion1, out RegionToDisp);
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("外圈面积");
                hv_result = hv_result.TupleConcat(hv_Area.D);
                hv_result = hv_result.TupleConcat("外圈缺陷面积");
                hv_result = hv_result.TupleConcat(hv_a1.D);
                hv_result = hv_result.TupleConcat("中圈面积");
                hv_result = hv_result.TupleConcat(hv_Area2.D);
                hv_result = hv_result.TupleConcat("中圈缺陷面积");
                hv_result = hv_result.TupleConcat(hv_a2.D);
                hv_result = hv_result.TupleConcat("内圈面积");
                hv_result = hv_result.TupleConcat(hv_Area4.D);

                result = hv_result.Clone();
                ho_Circle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_Circle3.Dispose();
                ho_RegionUnion1.Dispose();
                ho_Circle1.Dispose();
                ho_RegionErosion.Dispose();
                ho_RegionErosion1.Dispose();
                ho_RegionFillUp.Dispose();
                ho_Circle4.Dispose();
                ho_RegionDifference6.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_RegionFillUp1.Dispose();
                ho_RegionDifference2.Dispose();
                ho_ImageReduced2.Dispose();
                ho_Region2.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_Circle2.Dispose();
                ho_RegionErosion2.Dispose();
                ho_Circle5.Dispose();
                ho_RegionDifference7.Dispose();
                ho_ImageReduced3.Dispose();
                ho_Region3.Dispose();
                ho_ConnectedRegions3.Dispose();
                ho_RegionFillUp3.Dispose();
                ho_RegionDifference5.Dispose();
                ho_ImageReduced4.Dispose();
                ho_Region4.Dispose();
                ho_ConnectedRegions4.Dispose();
                ho_SelectedRegions2.Dispose();
                algorithm.Region.Dispose();
            }
            catch
            {
                HTuple hv_result = GetHv_result();
                hv_result = hv_result.TupleConcat("外圈面积");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("外圈缺陷面积");
                hv_result = hv_result.TupleConcat(99999);
                hv_result = hv_result.TupleConcat("中圈面积");
                hv_result = hv_result.TupleConcat(0);
                hv_result = hv_result.TupleConcat("中圈缺陷面积");
                hv_result = hv_result.TupleConcat(99999);
                hv_result = hv_result.TupleConcat("内圈面积");
                hv_result = hv_result.TupleConcat(0);

                result = hv_result.Clone();
                ho_Circle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_Circle3.Dispose();
                ho_RegionUnion1.Dispose();
                ho_Circle1.Dispose();
                ho_RegionErosion.Dispose();
                ho_RegionErosion1.Dispose();
                ho_RegionFillUp.Dispose();
                ho_Circle4.Dispose();
                ho_RegionDifference6.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_RegionFillUp1.Dispose();
                ho_RegionDifference2.Dispose();
                ho_ImageReduced2.Dispose();
                ho_Region2.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_Circle2.Dispose();
                ho_RegionErosion2.Dispose();
                ho_Circle5.Dispose();
                ho_RegionDifference7.Dispose();
                ho_ImageReduced3.Dispose();
                ho_Region3.Dispose();
                ho_ConnectedRegions3.Dispose();
                ho_RegionFillUp3.Dispose();
                ho_RegionDifference5.Dispose();
                ho_ImageReduced4.Dispose();
                ho_Region4.Dispose();
                ho_ConnectedRegions4.Dispose();
                ho_SelectedRegions2.Dispose();
                algorithm.Region.Dispose();

            }
            finally
            {
                ho_Circle.Dispose();
                ho_ImageReduced.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_Circle3.Dispose();
                ho_RegionUnion1.Dispose();
                ho_Circle1.Dispose();
                ho_RegionErosion.Dispose();
                ho_RegionErosion1.Dispose();
                ho_RegionFillUp.Dispose();
                ho_Circle4.Dispose();
                ho_RegionDifference6.Dispose();
                ho_ImageReduced1.Dispose();
                ho_Region1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_RegionFillUp1.Dispose();
                ho_RegionDifference2.Dispose();
                ho_ImageReduced2.Dispose();
                ho_Region2.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_Circle2.Dispose();
                ho_RegionErosion2.Dispose();
                ho_Circle5.Dispose();
                ho_RegionDifference7.Dispose();
                ho_ImageReduced3.Dispose();
                ho_Region3.Dispose();
                ho_ConnectedRegions3.Dispose();
                ho_RegionFillUp3.Dispose();
                ho_RegionDifference5.Dispose();
                ho_ImageReduced4.Dispose();
                ho_Region4.Dispose();
                ho_ConnectedRegions4.Dispose();
                ho_SelectedRegions2.Dispose();
                algorithm.Region.Dispose();
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




