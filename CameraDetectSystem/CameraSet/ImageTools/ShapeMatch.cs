using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Diagnostics;
namespace CameraDetectSystem
{
    [Serializable]
    public class ShapeMatch : ImageTools
    {
        public ShapeMatch()
        {
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
        }
        public ShapeMatch(HObject Image, Algorithm al)
        {
            HOperatorSet.GenEmptyObj(out RegionToDisp);
            RegionToDisp.Dispose();
            this.Image = Image; this.algorithm = al; vOrh = "h";
            pixeldist = 1;
        }

        public string vOrh { set; get; }

        public HTuple Length
        {
            get { return _length; }
            set { _length = value; }
        }
        [NonSerialized]
        HTuple _length;
        [NonSerialized]
        private HTuple Row1, Row2, Column1, Column2, hv_ModelID;
        public double DRow1, DRow2, DColumn1, DColumn2;

       
        public override void draw()
        {
            HOperatorSet.SetColor(this.LWindowHandle, "cyan");
            HOperatorSet.SetDraw(this.LWindowHandle, "margin");
            HOperatorSet.DrawRectangle1(this.LWindowHandle, out Row1, out Column1, out Row2, out Column2);
            DRow1 = Row1.D;
            DRow2 = Row2.D;
            DColumn1 = Column1.D;
            DColumn2 = Column2.D;
            HObject ho_Rectangle, hImage, ho_ModelImages, ho_ModelRegions;

            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out hImage);
            HOperatorSet.GenEmptyObj(out ho_ModelImages);
            HOperatorSet.GenEmptyObj(out ho_ModelRegions);
            ho_Rectangle.Dispose();
            hImage.Dispose();
            HOperatorSet.GenRectangle1(out ho_Rectangle, this.DRow1, this.DColumn1, this.DRow2, this.DColumn2);
            HOperatorSet.ReduceDomain(this.Image, ho_Rectangle, out hImage);

            HOperatorSet.CreateShapeModel(hImage, "auto", 0, (new HTuple(360)).TupleRad()
    , (new HTuple(1)).TupleRad(), "auto", "use_polarity", "auto", "auto", out hv_ModelID);
            ho_ModelImages.Dispose();
            ho_ModelRegions.Dispose();
            HOperatorSet.InspectShapeModel(hImage, out ho_ModelImages, out ho_ModelRegions,
                4, 30);
            HOperatorSet.DispObj(ho_ModelRegions, this.LWindowHandle);
            HOperatorSet.WriteShapeModel(hv_ModelID, PathHelper.currentProductPath + @"\Pictures.shm");


            ho_Rectangle.Dispose();
            hImage.Dispose();
            ho_ModelImages.Dispose();
            ho_ModelRegions.Dispose();
        }
        
        private void action()
        {
            _length = 0;
            // Local iconic variables 

            HObject ho_ImageReduced;
            HObject region, ho_regionOpening;
            // Local control variables 
            HTuple hv_Number;
            HTuple hv_Row, hv_Column, hv_Angle, hv_Score, hv_Newtuple;
            HOperatorSet.GenEmptyObj(out region);
        
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            
            try
            {
                if (hv_ModelID.Length!=1)
                HOperatorSet.ReadShapeModel(PathHelper.currentProductPath + @"\Pictures.shm", out hv_ModelID);

                HOperatorSet.FindShapeModel(this.Image, hv_ModelID, 0, (new HTuple(360)).TupleRad()
        , 0.5, 0, 0.5, "least_squares", 0, 0.9, out hv_Row, out hv_Column, out hv_Angle,
        out hv_Score);
                if ((int)(new HTuple((new HTuple(hv_Score.TupleLength())).TupleGreaterEqual(1))) != 0)
                {
                    HOperatorSet.TupleGenConst(new HTuple(hv_Score.TupleLength()), 100, out hv_Newtuple);

                    HOperatorSet.GenCircle(out region, hv_Row, hv_Column, hv_Newtuple);
                }
                this.result = new HTuple();
                if (region.IsInitialized())
                {

                    HOperatorSet.CountObj(region, out hv_Number);
                    if ((int)(new HTuple(hv_Number.TupleGreaterEqual(1))) != 0)
                    {
                        this.Result = this.Result.TupleConcat("数量");
                        this.Result = this.Result.TupleConcat(hv_Number);
                    }
                   
                    
                        RegionToDisp.Dispose();
                        if (RegionToDisp.IsInitialized())
                            HOperatorSet.ConcatObj(region, RegionToDisp, out RegionToDisp);
                        else
                        {
                            //HOperatorSet.GenRegionContourXld(region, out RegionToDisp, "filled");
                            HOperatorSet.Union1(region, out RegionToDisp);
                        }
                    
                }

            }
            catch (Exception e)
            {
                this.Result = new HTuple();
                this.Result = this.Result.TupleConcat("数量");
                this.Result = this.Result.TupleConcat(0);
            }
            finally
            {
                //algorithm.Region.Dispose();
                region.Dispose();
                ho_ImageReduced.Dispose();
            }
        }
        public override bool method()
        {
            try
            {
                
                    action();
                    return true;
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                return false;
            }
        }

    }
}
