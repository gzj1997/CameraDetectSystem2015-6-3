using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace CameraDetectSystem
{
    class Card
    {
        public static int sjc(DateTime tq, DateTime th)
        {
            TimeSpan ts = th - tq;
            int hmc = ts.Days * 24 * 3600000 + ts.Hours * ts.Minutes * 60000 + ts.Seconds * 1000 + ts.Milliseconds;
            return hmc;
        }
        public static ushort sensorIO = 1;
        public static ushort cardNo = 0;
        public static ushort shakePan = 31;
        public static ushort On = 0;
        public static ushort zhouhao = 0;
        public static ushort hd = 28;
        public static ushort jsqm = 36;
        public static ushort jsqfw = 31;
        public static ushort ld = 27;
        public static ushort jsqqg = 30;
        public static ushort Off = 1;
        public static Mutex mu = new Mutex();
        public static object lockobj = new object();
        public static ushort Solenoid1 = 5;
        public static ushort Solenoid2 = 6;
        //public static ushort Solenoid3 = 0;
        public static ushort Out1 = 1;
        public static ushort jiting = 24;
        public static int minspeed = 1000;
        public static int maxspeed = 20000;
        public static double acc = 10000;
        public static int shakePanIO = 33;
        public static int niulimax = 120;
        public static int niulimax2 = 110;
        public static ushort zhizhenSensor = 2;
        public static ushort servoAlarm = 3;
        public static ushort servoRes = 4;
        public static ushort chuiqizongkaiguan = 10;
        public static MathineTyple mathineTyple = MathineTyple.boli;
    }
    public enum MathineTyple { fendu, boli };
}