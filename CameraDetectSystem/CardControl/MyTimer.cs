using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dongzr.MidiLite;
namespace CameraDetectSystem
{
    class MyTimer
    {
        public MmTimer timer;
        int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public delegate void OnTimerEventHandler(object sender, EventArgs e);
        public event OnTimerEventHandler OnTimerEvent;

        public MyTimer()
        {
            timer = new MmTimer();
            timer.Mode = MmTimerMode.OneShot;
            timer.Tick+=new EventHandler(timer_Tick);
            id = 0;
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (OnTimerEvent != null)
            {
                OnTimerEvent((object)this, e);
            }
        }
    }
}
