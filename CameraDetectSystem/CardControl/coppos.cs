using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CameraDetectSystem
{
    public class jcqdl
    {
        public Queue<long> xjjcqdl=new Queue<long>();
        int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public void enquene1(long ss)
        {
            xjjcqdl.Enqueue(ss);
        }
        public long[] toarray()
        {
            long[] ar = new long[xjjcqdl.Count()];
            ar = xjjcqdl.ToArray();
            return ar;
        }
        public long dequene1()
        {
            return xjjcqdl.Dequeue();
        }
        public void clear()
        {
            xjjcqdl.Clear();
        }
    }
    public class coppos
    {
        int id;
        //public int pos;
        public bool onwrite;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public List<int> pos = new List<int>();
    }
    public class xjfw
    {
        public DateTime t1, t2;
        int id;
        public TimeSpan ts;
        public int ID
        {
            get { return id; }
            set { id = value; }

        }
        public int tsc()
        {
            ts = t2 - t1;
            return ts.Days * 24 * 3600000 + ts.Hours * 3600000 + ts.Minutes * 60000 + ts.Seconds * 1000 + ts.Milliseconds;
        }
    }
}
