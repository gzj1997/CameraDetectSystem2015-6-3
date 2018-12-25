using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Windows.Forms;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Xml.Serialization;
namespace CameraDetectSystem
{
     [Serializable]
    public class ThresholdSelectRegion:Algorithm
    {
        public ThresholdSelectRegion()
        {
            selectMethod=new string[10];
            selectMethodMinValue = new double[10];
            selectMethodMaxValue = new double[10];
            this.thresholdValue = 0;
            this.whiteOrBlack = "black";
            this.selectAndOrOr = "and";
            this.selectMethod[0] = "area";
            this.selectMethodMinValue[0] = 150;
            this.selectMethodMaxValue[0] = 99999;
        }
       public ThresholdSelectRegion(HObject Image)
        {
            selectMethod = new string[10];
            selectMethodMinValue = new double[10];
            selectMethodMaxValue = new double[10];
            this.thresholdValue = 0;
            this.whiteOrBlack = "black";
            this.selectAndOrOr = "and";
            this.selectMethod[0] = "area";
            this.selectMethodMinValue[0] = 150;
            this.selectMethodMaxValue[0] = 99999;
            
        }
       ~ThresholdSelectRegion()
       {
           if (RegionConnection != null)
           {
               RegionConnection.Dispose();
           }
           if (RegionSelected != null)
           {
               RegionSelected.Dispose();
           }
       }
       bool valueJudgement()
       {
           List<HTuple> test = new List<HTuple>();
           test.Add(thresholdValue);
           test.Add(whiteOrBlack);
           test.Add(selectMethod);
           test.Add(selectAndOrOr);
           test.Add(selectMethodMinValue);
           test.Add(selectMethodMaxValue);
           foreach (HTuple t in test)
           {
               if (t.Type == HTupleType.EMPTY)
               {
                   MyDebug.ShowMessage("请先初始化ThresholdSelectRegion算法参数");
                   return false;
               }
           }
           return true;
       }
       //HTuple _thresholdValue=null;
       //HTuple _whiteOrBlack = null;
       //HTuple _selectMethod = null;
       //HTuple _selectAndOrOr = null;
       //HTuple _selectMethodMinValue = null;
       //HTuple _selectMethodMaxValue = null;
       public int thresholdValue { set; get; }
         //public HTuple thresholdValue { get { return _thresholdValue;} set { _thresholdValue = value; } }
       public string whiteOrBlack { set; get; }
         //public HTuple whiteOrBlack { get { return _whiteOrBlack; } set { _whiteOrBlack = value; } }
       public string[] selectMethod { set; get; }
         //public HTuple selectMethod { get { return _selectMethod; } set { _selectMethod = value; } }
       public string selectAndOrOr { set; get; }
         //public HTuple selectAndOrOr { get { return _selectAndOrOr; } set { _selectAndOrOr = value; } }
       public double[] selectMethodMinValue { set; get; } 
         //public HTuple selectMethodMinValue { get { return _selectMethodMinValue; } set { _selectMethodMinValue = value; } }
       public double[] selectMethodMaxValue { set; get; }
        //public HTuple selectMethodMaxValue { get { return _selectMethodMaxValue; } set { _selectMethodMaxValue = value; } }
         [NonSerialized]
        HObject RegionConnection=null;
         [NonSerialized]
        HObject RegionSelected = null;
        public override void ThresholdMethod()
        {
            try
            {
                if (!valueJudgement())
                {
                    return;
                }
                HOperatorSet.GenEmptyObj(out RegionConnection);
                RegionConnection.Dispose();
                HOperatorSet.GenEmptyObj(out RegionSelected);
                RegionSelected.Dispose();
                HOperatorSet.Threshold(this.Image, out _region, whiteOrBlack == "black" ? new HTuple(0) : new HTuple(this.thresholdValue), whiteOrBlack == "black" ? new HTuple(this.thresholdValue) : new HTuple(255));
            }
            catch(Exception e)
            {
                MyDebug.ShowMessage(e,"ThresholdMethodError");
            }
        }
        public override void SelectMethod()
        {
            try
            {
                if (valueJudgement())
                {
                    
                    HOperatorSet.Connection(_region, out RegionConnection);
                    HOperatorSet.SelectShape(RegionConnection, out this._region, this.selectMethod, this.selectAndOrOr, this.selectMethodMinValue, this.selectMethodMaxValue);
                    RegionConnection.Dispose();
                }
            }
            catch (Exception e)
            {
                MyDebug.ShowMessage("ThresholdMethodError:=" + e.Message);
            }
        
        }
        public bool CheckData()
        {
            int i1 = this.selectMethod.Length;
            int i2 = this.selectMethodMaxValue.Length;
            int i3 = this.selectMethodMinValue.Length;
            if (i1 == i2 && i1 == i3)
                return true;
            else
            {
                MyDebug.ShowMessage("ThresholdSelectRegion参数长度不一样");
                return false;
            }
        }
    }
}
