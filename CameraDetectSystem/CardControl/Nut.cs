using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
        class CameraCheckOut
        {
            public int leadToTheHole;
            public string nextCameraName;
            public int initialPos;
        }
    class jiancejieguo
    {
        public string jiancexiangmu;
        public double celiangjieguo;
    }
        class Nut
        {
            public List<CameraCheckOut> checkedResult;
            public List<jiancejieguo> jiance;
            public int initialPos;
            public int cas;
            public int posNo;
            public int getTheHole()
            {
                int i = (int)(Holes.firstHole);
                List<int> checklist = new List<int>();
                foreach (CameraCheckOut cco in checkedResult)
                {
                    checklist.Add(cco.leadToTheHole);
                }
                if (checklist.Count>0)
                checklist.RemoveAt(0);
                
               

               if (checklist.Contains((int)Holes.firstHole))
                {
                    i = 1;

                }
                //未识别
                else if (checklist.Contains((int)Holes.thirdHole))
                {
                    i =3;
                }
                //良品
                else if (checklist.Contains((int)Holes.secondHole))
                {
                    i =2;
                }
                else
                {
                    i =3;
                }
                if (checklist.Count != cas)
                {
                    i = 3;
                }
                return i;
            }
            public Nut()
            {
                CameraCheckOut inic = new CameraCheckOut();
                inic.leadToTheHole = 1;
                inic.nextCameraName = "CCD1";
                this.checkedResult = new List<CameraCheckOut>();
                jiance = new List<jiancejieguo>();
                this.checkedResult.Add(inic);
                posNo = 0;
            }
        }
}
