using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using HalconDotNet;
using System.IO.Ports;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;

namespace CameraDetectSystem
{
    class Turntable
    {
        public List<Nut> nutqueue;
        public jcqdl[] jcqdls;
        Thread thread;
        public bool isStart;
        bool zhuanPanIsRun;
        bool zhuanpanStateRuning;
        int[] xjzsr = new int[5];
        List<int> cameraPos;
        List<int> valvePos;
        MyTimer paiz;
        MyTimer[] timers;
        xjfw[] xjfws;
        public List<int>[] dpdl;
        MyTimer zhizhenLowSensor;
        public List<int> PosArray;
        public List<ushort> IOs;
        public ProductNum pn=new ProductNum();
        int speedTemp = 0;
        System.Windows.Forms.Timer SpeedTime=new System.Windows.Forms.Timer();
        static private Turntable instance=null;
        SerialPort MySerialPort;
        volatile bool isbussy;
        public delegate void UpdateProductInfo();
        public delegate void StrongPress();
        public event StrongPress StrongPressEventHandler;
        public delegate void wuliaohandler();
        public event wuliaohandler wuliao;
        public void Onwuliao()
        {
            wuliao(); 
        }
        MyTimer StartDelay=new MyTimer();
        static public Turntable Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Turntable();
                }
                return instance;
            }
        }
        public event UpdateProductInfo OnUpdateProductInfoEvent;      
        public void startShakePan()
        {
           int j =PCI408.PCI408_check_done(Card.cardNo);
           if (j == 0)
           {

               PCI408.PCI408_write_outbit(Card.cardNo, Card.shakePan, Card.On);
           }
        }
        public void stopShakePan()
        {            
            //Turntable.Instance.cardInitial();
            PCI408.PCI408_write_outbit(Card.cardNo, Card.shakePan, Card.Off);
            //Thread.Sleep(4500);
            //isStart = false;
        }
        public Turntable()
        {
            wkb = new HSSFWorkbook();
            sheet = wkb.CreateSheet("检测结果(Measure Data)");
            tjcjg = DateTime.Now;
            sheetrr = sheet.CreateRow(sheetr);
            xjzsr[0] = 2;
            xjzsr[1] = 34;
            xjzsr[2] = 30;//31
            xjzsr[3] = 3;//21
            xjzsr[4] = 4;//32
            sheetcc = sheetrr.CreateCell(sheetc);
            watchdog.OnTimerEvent+=new MyTimer.OnTimerEventHandler(watchdog_OnTimerEvent);
            zhuanPanIsRun = false;
            zhizhenLowSensor = new MyTimer();
            StartDelay.OnTimerEvent+=new MyTimer.OnTimerEventHandler(StartDelay_OnTimerEvent);
            zhizhenLowSensor.OnTimerEvent+=new MyTimer.OnTimerEventHandler(zhizhenLowSensor_OnTimerEvent);
            MySerialPort = new SerialPort();
            MySerialPort.DataReceived += new SerialDataReceivedEventHandler(MySerialPort_DataReceived);
            isbussy = false;
            nutqueue = new List<Nut>();
            isStart=false;
            jcqdls = new jcqdl[5];
            bjgd = new long[5];
            for (int jcqdlsl = 0; jcqdlsl < 5; jcqdlsl++)
            {jcqdls[jcqdlsl]=new jcqdl();
                jcqdls[jcqdlsl].xjjcqdl = new Queue<long>();
                bjgd[jcqdlsl] = 0;
            }
                xjfws = new xjfw[7];
            for (int xjfwi = 0; xjfwi < 5; xjfwi++)
            {
                xjfws[xjfwi] = new xjfw();
                xjfws[xjfwi].ID = xjfwi;
                xjfws[xjfwi].t1 = DateTime.Now;
                xjfws[xjfwi].t2 = DateTime.Now;
            }
            copos = new coppos[7];
            for (int k = 0; k < 5; k++)
            {
                copos[k] = new coppos();
                copos[k].ID = k;
              //  copos[k].onwrite = false;
            }
            timers = new MyTimer[7];
            for (int i = 0; i < timers.Length;i++ )
            {
                timers[i] = new MyTimer();
                timers[i].ID = i;
                //timers[i].OnTimerEvent += new MyTimer.OnTimerEventHandler(OnTimer);
            }
            dpdl = new List<int>[5];
            for (int i = 0; i < 5; i++)
            {
                dpdl[i] = new List<int>();
            }
                for (ushort i = 0; i < 9; i++)
                {
                    PCI408.PCI408_write_outbit(Card.cardNo, (ushort)(Card.Out1 + i), Card.Off);
                }
            PCI408.PCI408_set_position(Card.cardNo, 0);
            PCI408.PCI408_set_pulse_outmode(Card.cardNo, 0);
            //SpeedTime.Tick += new EventHandler(SpeedTime_OnTime);
            SpeedTime.Enabled = true;
            SpeedTime.Interval = 5000;
            zhuanpanStateRuning = false;
            //if (!MySerialPort.IsOpen)
            //{
            //    MySerialPort.BaudRate = 9600;
            //    MySerialPort.StopBits = StopBits.One;
            //    MySerialPort.Parity = Parity.None;
            //    MySerialPort.PortName = "COM1";
            //    MySerialPort.DataBits = 8;
            //    MySerialPort.ReceivedBytesThreshold = 21;
            //    MySerialPort.ReadBufferSize = 512;
            //    MySerialPort.Open();
            //}
        }
        ~Turntable()
        {
            cardInitial();
        }
        public void cardInitial()
        {
            for (ushort i = 0; i < 32; i++)
            {
                PCI408.PCI408_write_outbit(Card.cardNo, (ushort)(Card.Out1 + i), Card.Off);
            }
        }
        public HSSFWorkbook wkb;
        ISheet sheet;
        DateTime tjcjg,tclsj;
        public int sheetr = 0, sheetc = 0;
        public void Start()
        {
            if (!isStart)
            {
                for (int i = 0; i < IOs.Count; i++)
                {
                    PCI408.PCI408_write_outbit(Card.cardNo, IOs[i], Card.Off);
                }
                thread = new Thread(Run);
                thread.IsBackground = true;
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
                isStart = true;
            }
            SpeedTime.Start();
            //PCI408.PCI408_write_SEVON_PIN(Card.cardNo, Card.On);
            PCI408.PCI408_set_profile(Card.cardNo, Card.minspeed, Card.maxspeed, Card.acc, Card.acc);
            PCI408.PCI408_vmove(Card.cardNo, 0,Card.maxspeed);
            zhuanPanIsRun = true;
        }
        public void Stop()
        {
            SpeedTime.Enabled = false;
            //isStart=false;
            PCI408.PCI408_decel_stop(Card.cardNo, Card.acc);
            //PCI408.PCI408_write_outbit(Card.cardNo, Card.chuiqizongkaiguan, Card.Off);
            zhuanPanIsRun = false;
           // PCI408.PCI408_write_SEVON_PIN(Card.cardNo, Card.Off);
        }
        void lackStop()
        {
            SpeedTime.Stop();
            SpeedTime.Enabled = false;
           
            PCI408.PCI408_decel_stop(Card.cardNo, Card.acc);
            //PCI408.PCI408_write_outbit(Card.cardNo, Card.chuiqizongkaiguan,Card.Off);
            niulimax = Card.niulimax;
        }
        void lackStart()
        {
            zhizhenLowSensor.timer.Interval = 2000;
            zhizhenLowSensor.timer.Start();

            StartDelay.timer.Interval = 3000;
            StartDelay.timer.Start();

        }
        DateTime t11 = new DateTime(), t21 = new DateTime(); TimeSpan ts;
        int ss;
        Thread baojing;
        void bjzx()
        {
            Thread.Sleep(7000);
            //PCI408.PCI408_write_outbit(Card.cardNo,5, Card.On); 

        }
        public int currentPos = 0,sdkz=0,sdkz1=100,sdkz3;
        double spd;
        public DateTime sdkzt1 = new DateTime(), sdkzt2 = new DateTime(), sdkzt3 = new DateTime(), sdkzt4 = new DateTime();
        public long[] bjgd; public int qc = 0;
        DateTime tlt11 = new DateTime(), tlt21 = new DateTime(), liaota1 = new DateTime(), liaota2 = new DateTime(); TimeSpan tslt, liaota;
        void Run()
        {
            //将当前线程绑定到指定的cpu核心上
            GetCpu.SetThreadAffinityMask(GetCpu.GetCurrentThread(), new UIntPtr(0x4));

            kzwbmlt12 = DateTime.Now;
            string path = PathHelper.currentProductPath + @"\" +"spd"+ ".txt";
            DateTime t1;
            int xjfwi;
            spd = Card.maxspeed;
            int preSignal = 0, signal = 0; 
            PCI408.PCI408_counter_config(0, 3);
            PCI408.PCI408_set_encoder(0, 0);
            int fwjs1 = 0, fwjs2 = 0; sdkz3 = 100000;
            long cuowu=PCI408.PCI408_compare_set_filter_Extern(Card.cardNo,100000);
            PCI408.PCI408_config_latch_mode(Card.cardNo, Card.zhouhao);
            sdkzt1 = DateTime.Now; sdkzt2 = DateTime.Now; sdkzt4 = DateTime.Now; sdkzt3 = DateTime.Now;
            long jcqbz;
            for (int xjh = 0; xjh < PosArray.Count(); xjh++)
            {
                PCI408.PCI408_compare_clear_points_Extern(0,(ushort)(xjh+1));
                PCI408.PCI408_compare_config_Extern(Card.cardNo, (ushort)(xjh + 1), 1, Card.zhouhao, IOs[xjh]);
                PCI408.PCI408_compare_set_pulsetimes_Extern(0, (ushort)(xjh + 1), 20);
            }
            PCI408.PCI408_reset_latch_flag(Card.cardNo);
            long s5;
            currentPos = (int)PCI408.PCI408_get_encoder(Card.cardNo);
            int[] sigci = new int[5], psigci = new int[5];
            for (int sigfi = 0; sigfi < 5; sigfi++)
            {
                sigci[sigfi] = 0; 
                psigci[sigfi] = 0;
            }

            //？
            int zdwy = 40000;
                while (isStart)
                {
                    try
                    {
                    //    for (xjfwi = 1; xjfwi < cameraCount + 1; xjfwi++)
                    //{
                    //    xjfws[xjfwi].t2 = DateTime.Now;
                    //    //if (xjfws[xjfwi].tsc() > 200000 && copos[xjfwi].pos.Count() > 0)
                    //   //if (xjfws[xjfwi].tsc() > 200000)
                    //   // {
                    //   //     //copos[0].pos.Clear();
                    //   //     //copos[1].pos.Clear();
                    //   //     //copos[2].pos.Clear();
                    //   //     //copos[3].pos.Clear();
                    //   //     //copos[4].pos.Clear();
                    //   //     //copos[5].pos.Clear();
                    //   //     //copos[6].pos.Clear();
                    //   //     nutqueue.Clear();
                    //   //     xjfws.Initialize();
                    //   //     copos.Initialize();
                    //   //     timers.Initialize();
                    //   //     xjfws = new xjfw[7];
                    //   //     //for (xjfwi = 0; xjfwi < 5; xjfwi++)
                    //   //     //{
                    //   //     //    xjfws[xjfwi] = new xjfw();
                    //   //     //    xjfws[xjfwi].ID = xjfwi;
                    //   //     //    xjfws[xjfwi].t1 = DateTime.Now;
                    //   //     //    xjfws[xjfwi].t2 = DateTime.Now;
                    //   //     //}
                    //   //     copos = new coppos[7];
                    //   //     //for (int k = 0; k < 5; k++)
                    //   //     //{
                    //   //     //    copos[k] = new coppos();
                    //   //     //    copos[k].ID = k;
                    //   //     //    //copos[k].onwrite = false;
                    //   //     //}
                    //   //     timers = new MyTimer[7];
                    //   //     //for (int i = 0; i < timers.Length; i++)
                    //   //     //{
                    //   //     //    timers[i] = new MyTimer();
                    //   //     //    timers[i].ID = i;
                    //   //     //    //timers[i].OnTimerEvent += new MyTimer.OnTimerEventHandler(OnTimer);
                    //   //     //}
                    //   //     //Thread.Sleep(1000);
                    //   // }
                    //}
                    //    t1 = DateTime.Now;
                        currentPos = (int)PCI408.PCI408_get_encoder(Card.zhouhao);
                        for (int sr = 0; sr < cameraCount; sr++)
                        {
                            sigci[sr] = PCI408.PCI408_read_inbit(Card.cardNo, (ushort)xjzsr[sr]);
                            if (sigci[sr] == 0 && psigci[sr] == 1)
                            {
                                int length1 = dpdl[sr].Count(), jr = 200, jr1 = 5000;
                                for (int listi = 0; listi < length1; listi++)
                                {
                                    int ssg = Math.Abs(dpdl[sr][listi] + PosArray[sr] - currentPos);
                                    if (ssg < jr1)
                                    {
                                        jr1 = ssg;
                                        jr = listi;
                                        
                                    }
                                }
                                if (jr != 200 && jr1 < 5000)
                                {
                                    copos[sr].pos.Add(dpdl[sr][jr]);
                                 //   Console.WriteLine("trun " + DateTime.Now.ToString("mm-ss-fff"));
                                    dpdl[sr].RemoveAt(jr);
                                }
                            }
                            psigci[sr] = sigci[sr];
                        }
                        for (int sr1 = 0; sr1 < cameraCount; sr1++)
                        {
                            int length2 = dpdl[sr1].Count();
                            for (int listi1 = 0; listi1 < length2; listi1++)
                            {
                                if (Math.Abs(currentPos - dpdl[sr1][listi1]) > zdwy)
                                {
                                    dpdl[sr1].RemoveAt(listi1);
                                }
                            }
                        }
                        jcqbz = PCI408.PCI408_get_latch_flag(Card.cardNo);//读取锁存器状态
                        if ((jcqbz & 0xf00) > 0)
                        {
                            PCI408.PCI408_reset_latch_flag(Card.cardNo);
                            signal = PCI408.PCI408_get_latch_value(Card.zhouhao);
                        }
                        if (fwjs1 - fwjs2 > 4000)
                        {
                            fwjs1 = fwjs2;
                            for (int sr = 0; sr < cameraCount; sr++)
                            {
                                sigci[sr] = 0;
                                psigci[sr] = 0;
                                dpdl[sr].Clear();
                                copos[sr].pos.Clear();
                            }
                                for (int xjh = 0; xjh < PosArray.Count(); xjh++)
                                {
                                    PCI408.PCI408_set_encoder(0, 0);
                                    PCI408.PCI408_compare_clear_points_Extern(0, (ushort)(xjh + 1));
                                    PCI408.PCI408_compare_config_Extern(Card.cardNo, (ushort)(xjh + 1), 1, Card.zhouhao, IOs[xjh]);
                                    PCI408.PCI408_compare_set_pulsetimes_Extern(0, (ushort)(xjh + 1), 5);

                                }
                            for (int xjs = 0; xjs < cameraCount; xjs++)
                            {
                                jcqdls[xjs].clear();
                                //File.AppendAllText(path,signal.ToString()+"  ", Encoding.Default);
                            }
                            PCI408.PCI408_reset_latch_flag(Card.cardNo);
                            preSignal = signal;
                            if (isStart)
                            {
                                nutqueue.Clear();
                            }
                        }
                        if (signal != preSignal)
                        {
                            kzwbmlt12 = DateTime.Now;
                            s5 = PCI408.PCI408_compare_get_points_remained_Extern(0, 1);
                            if (s5 > cameraCount + 3)
                            {
                                Nut nut = new Nut();
                                fwjs1 += 1;
                                nut.initialPos = signal;
                                nutqueue.Add(nut);
                                nut.cas = cameraCount;
                                pn.totalCount++;
                                speedTemp++;
                                ss = pn.totalCount;
                                for (int xjs = 0; xjs < cameraCount; xjs++)
                                {
                                    PCI408.PCI408_compare_config_Extern(Card.cardNo, (ushort)(xjs + 1), 1, Card.zhouhao, 1);
                                    PCI408.PCI408_compare_add_point_Extern(Card.cardNo, (ushort)(xjs + 1), PosArray[xjs] + (int)signal, 1, 9, IOs[xjs]);

                                   // copos[nut.posNo + 1].pos.Add (nut.initialPos);
                                   // copos[nut.posNo + 1].onwrite = true;
                                    dpdl[xjs].Add(signal);
                                   // Console.WriteLine("ss" + signal);
                                    //File.AppendAllText(path,signal.ToString()+"  ", Encoding.Default);
                                }
                            }
                        }
                        niuli();

                        for (int i = nutqueue.Count - 1; i > -1; i--)
                        {

                            Nut nutc = new Nut();
                            nutc = nutqueue[i];
                            //if (currentPos < nutc.initialPos)
                            //{
                            //    nutqueue.Remove(nutc);
                            //    break;
                            //}
                            if (currentPos - nutc.initialPos > PosArray[cameraCount] - 1000)
                            {
                              int a= nutc.getTheHole() ;
                                switch (a.ToString())
                                {
                                    case "1":
                                    //pn.mnum++;
                                    //Console.WriteLine("cipin" + pn.mnum);
                                    PCI408.PCI408_compare_config_Extern(Card.cardNo, (ushort)(cameraCount + 1), 1, Card.zhouhao, 1);
                                    PCI408.PCI408_compare_set_pulsetimes_Extern(0, (ushort)(cameraCount + 1), 10);
                                    PCI408.PCI408_compare_add_point_Extern(Card.cardNo, (ushort)(cameraCount + 1), PosArray[cameraCount] + (int)nutc.initialPos, 1, 9, IOs[cameraCount]);
                                    break;
                                    //case "3":
                                    //pn.nnum++;
                                    //Console.WriteLine("未识别" + pn.nnum);
                                    //PCI408.PCI408_compare_config_Extern(Card.cardNo, (ushort)(cameraCount + 1), 1, Card.zhouhao, 1);
                                    //PCI408.PCI408_compare_set_pulsetimes_Extern(0, (ushort)(cameraCount + 1), 20);
                                    //PCI408.PCI408_compare_add_point_Extern(Card.cardNo, (ushort)(cameraCount + 1), PosArray[cameraCount] + (int)nutc.initialPos, 1, 9, IOs[cameraCount]);
                                    //break;
                                    case "2":
                                    pn.goodNum++;
                                    fwjs2 = fwjs1;
                                    PCI408.PCI408_compare_config_Extern(Card.cardNo, (ushort)(cameraCount + 2), 1, Card.zhouhao, 1);
                                    PCI408.PCI408_compare_set_pulsetimes_Extern(0, (ushort)(cameraCount + 2), 10);
                                    PCI408.PCI408_compare_add_point_Extern(Card.cardNo, (ushort)(cameraCount + 2), PosArray[cameraCount + 1] + (int)nutc.initialPos, 1, 9, IOs[cameraCount + 1]);
                                    break;


                                }
                                jcjg2 = nutc.jiance;
                                Thread ccjcjg = new Thread(cunchujiancejieguo);
                                ccjcjg.Start();
                                nutqueue.Remove(nutc);
                                pn.badNum = pn.totalCount - pn.goodNum;
                                
                            }

                        }
                        preSignal = signal;
                        kongzhiwaibu();
                        sdkzt1 = DateTime.Now;
                        sdkzt4 = DateTime.Now;
                        if (Card.sjc(sdkzt3, sdkzt4) < sdkz3)
                        {
                            sdkz3 = Card.sjc(sdkzt3, sdkzt4);
                        }
                        sdkzt3 = DateTime.Now;
                        //if (Card.sjc(sdkzt2, sdkzt1) > 60000)
                        //{
                        //    sdkzt2 = DateTime.Now;
                        //    if (sdkz3 < 10)
                        //    {
                        //        spd = spd + 100;
                        //        PCI408.PCI408_change_speed(Card.cardNo, spd);
                        //        File.AppendAllText(path, spd.ToString() + "  ", Encoding.Default);
                        //    }
                        //}
                    }
                    catch (Exception e)
                    {
                        MyDebug.ShowMessage(e, "运动控制出错");
                    }
                    finally
                    {
                        //MyDebug.ShowMessage(nutqueue.Count.ToString());
                    }
                }
        }


        DateTime kzwbmlt11, kzwbmlt12; TimeSpan kzwbmlts;
        DateTime lmxh1, lmxh2; TimeSpan lmxhts;
        int kzwbhdzt=0,djzt=0,kzwbldzt=0,s4 = 0, s5 = 0, sc = 0;
        private void kongzhiwaibu()
        {
            kzwbmlt11 = DateTime.Now;
            kzwbmlts = kzwbmlt11 - kzwbmlt12;
            int kzwbml;
            kzwbml = kzwbmlts.Days * 86400 + kzwbmlts.Hours * 3600 + kzwbmlts.Minutes * 60 + kzwbmlts.Seconds;
            if (kzwbml > 240)
            {
                Onwuliao();
                baojing = new Thread(bjzx);
                baojing.Start();
                isStart = false;
            }
            djzt=PCI408.PCI408_check_done(Card.cardNo);
            if (djzt ==1)
            {
                if (kzwbhdzt == 1)
                {
                    PCI408.PCI408_write_outbit(Card.cardNo, Card.hd, Card.On);
                    PCI408.PCI408_write_outbit(Card.cardNo, Card.shakePan, Card.Off);
                    kzwbhdzt = 0;
                }
                if (kzwbldzt == 1)
                {
                    PCI408.PCI408_write_outbit(Card.cardNo, Card.ld, Card.Off);
                    kzwbldzt = 1;
                }
            }
            else
            {
                if (kzwbldzt == 1)
                {
                    PCI408.PCI408_write_outbit(Card.cardNo, Card.ld, Card.On);
                    kzwbldzt = 0;
                }
                if (kzwbhdzt == 0)
                {
                    PCI408.PCI408_write_outbit(Card.cardNo, Card.hd, Card.Off);
                    kzwbhdzt = 1;
                }
            }
            if (sc == 1)
            {
                s4 = PCI408.PCI408_read_inbit(Card.cardNo, Card.jsqm);
                if (s4 == 0)
                {
                    PCI408.PCI408_write_outbit(Card.cardNo, Card.jsqqg, Card.Off);
                    sc = 0;
                }
            }
            if (sc == 0)
            {
                s5 = PCI408.PCI408_read_inbit(Card.cardNo, Card.jsqfw);
                if (s5 == 0)
                {
                    PCI408.PCI408_write_outbit(Card.cardNo, Card.jsqqg, Card.On);
                    sc = 1;
                    lmxh2 = DateTime.Now;
                }
            }
            lmxhts = lmxh1 - lmxh2;//20170622
            lmxh1 = DateTime.Now;
            int lmxh;
            lmxh = lmxhts.Days * 86400 + lmxhts.Hours * 3600 + lmxhts.Minutes * 60 + lmxhts.Seconds;
            if (lmxh > 60 && sc == 1)
            {
                //baojing = new Thread(bjzx);
                //baojing.Start();
                //isStart = false;
                //lackStop();
                ////zhuanpanStateRuning = false;
                //SpeedTime.Stop();
                //Turntable.Instance.Stop();
                //zhuanpanStateRuning = false;
                ////zhizhenLowSensor.timer.Start();
                //SpeedTime.Enabled = false;
                PCI408.PCI408_decel_stop(Card.cardNo, Card.acc);
                PCI408.PCI408_write_outbit(Card.cardNo, Card.chuiqizongkaiguan, Card.Off);
                //zhuanPanIsRun = false;
                //zhizhenLowSensor.timer.Stop();


            } 
        }
        
        //void duankou()
        //{
        //    PCI408.PCI408_write_outbit(Card.cardNo, bitnopz, Card.On);
        //    paiz.timer.Interval = 30;  //相机触发时间，单位为ms
        //    paiz.timer.Start();
        //}
        public coppos[] copos;
        public int zdc12 = 0, zdc13 = 0, zdc14 = 0;
        public int chuc = 0;
        //void ArriveCamera(int CurrentPos, int Pos, MyTimer timer, ushort bitno, int checkno)
        //{
        //    for (int i = 0; i < nutqueue.Count; i++)
        //    {
        //        Nut nut = nutqueue[i];
        //        if (((CurrentPos - nut.initialPos) >= Pos) && (nut.posNo == checkno))
        //        {
        //            if (nut.posNo < cameraCount && timer.timer.IsRunning == false && copos[nut.posNo + 1].pos.Count == 0)
        //            {
        //                xjfws[nut.posNo + 1].t1 = DateTime.Now;
        //                copos[nut.posNo + 1].pos.Add(nut.initialPos);
        //                t1 = DateTime.Now;
        //                PCI408.PCI408_write_outbit(Card.cardNo, bitno, Card.On);
        //                timer.timer.Interval = 15;  //相机触发时间，单位为ms
        //                timer.timer.Start();
        //                //10.28 ch 添加
        //                ////////如果是ccd1 ，则改nextcamera为ccd1
        //                //nut.checkedResult[0].nextCameraName = "CCD1";
        //                //
        //                t2 = DateTime.Now;
        //            }
        //            else if (cameraCount <= nut.posNo)
        //            {
        //                if ((nut.posNo + 1 - cameraCount) == nut.getTheHole())
        //                {
        //                    if (nut.getTheHole() == (int)Holes.secondHole && timer.timer.IsRunning == false)
        //                    {
        //                        pn.goodNum++;
        //                        //pn.badNum = pn.totalCount - pn.goodNum;
        //                    }
        //                    //chui = new Thread(duankou);
        //                    //chui.Start();
        //                    PCI408.PCI408_write_outbit(Card.cardNo, bitno, Card.On);
        //                    timer.timer.Interval =15; //吹气触发时间，单位为ms
        //                    timer.timer.Start();
        //                }
        //            }
        //            nut.posNo++;
        //            if (nut.posNo >= cameraCount + valveCount)
        //            {
        //                //jcjg2 = nut.jiance;
        //                //Thread ccjcjg = new Thread(cunchujiancejieguo);
        //                //ccjcjg.Start();
        //                nutqueue.Remove(nut);
        //                //pn.badNum = pn.totalCount - pn.goodNum;
        //                //updateProductInfo();
        //            }
        //            pn.badNum = pn.totalCount - pn.goodNum;
        //            upd=new Thread(updateProductInfo);
        //            upd.Start();
        //            break;
        //        }
        //    }
        //}
        List<jiancejieguo> jcjg2 = new List<jiancejieguo>();
        public IRow sheetrr; public ICell sheetcc;
        void cunchujiancejieguo()
        {

            try
            {
                tclsj = DateTime.Now;
                sheetr += 1;
                sheetc = 0;
                sheetrr = sheet.CreateRow(sheetr);
                sheetcc = sheetrr.CreateCell(sheetc);
                sheetcc.SetCellValue(sheetr); 
                sheetc += 1;
                sheetcc = sheetrr.CreateCell(sheetc);
                sheetcc.SetCellValue(tclsj.Minute.ToString() + "/" + tclsj.Second.ToString() + "/" + tclsj.Millisecond.ToString());
                foreach (jiancejieguo jcjg3 in jcjg2)
                {
                    sheetc += 1;
                    sheetcc = sheetrr.CreateCell(sheetc);
                    sheetcc.SetCellValue(jcjg3.celiangjieguo);
                }
                //if ((tclsj.Hour - tjcjg.Hour) >1)
                if ((tclsj.Hour - tjcjg.Hour) * 60 + (tclsj.Minute - tjcjg.Minute) > 30)
                {
                    chuchushuju();
                }
            }
            finally
            { }
        }
        public void chuchushuju()
        {
            try
            {
                string path = PathHelper.dataPath + @"\Measure\" + tjcjg.Year.ToString() + tjcjg.Month.ToString() + tjcjg.Day.ToString() + tjcjg.Hour.ToString() + tjcjg.Minute.ToString() + tjcjg.Second.ToString() + ".xls";
                using (FileStream fs = File.OpenWrite(path))
                {
                    sheetc = 0;
                    sheetrr = sheet.CreateRow(0);
                    sheetcc = sheetrr.CreateCell(sheetc);
                    sheetcc.SetCellValue("序号（NUMBER）");
                    sheetc += 1; 
                    sheetcc = sheetrr.CreateCell(sheetc);
                    sheetcc.SetCellValue("测量时间（MESURE TIME）");
                    foreach (jiancejieguo jcjg3 in jcjg2)
                    {
                        sheetc += 1;
                        sheetcc = sheetrr.CreateCell(sheetc);
                        sheetcc.SetCellValue(jcjg3.jiancexiangmu);
                    }
                    wkb.Write(fs);//向打开的这个xls文件中写入并保存。  
                    wkb.Clear();
                    sheetr = 0; sheetc = 0;
                    sheet = wkb.CreateSheet("检测结果(Measure Data)");
                    tjcjg = DateTime.Now;
                    sheetrr = sheet.CreateRow(sheetr);
                    sheetcc = sheetrr.CreateCell(sheetc);
                }
            }
            catch 
            {
 
            }
            finally { }
        }
        //Thread upd,chui;
        //void updateProductInfo()
        //{
        //    if (OnUpdateProductInfoEvent != null)
        //    {
        //        OnUpdateProductInfoEvent();
        //    }
        //}
        
        //void OnTimer(object sender,EventArgs e)
        //{
        //    MyTimer t = sender as MyTimer;
        //    PCI408.PCI408_write_outbit(Card.cardNo, IOs[t.ID], Card.Off);
        //    t.timer.Stop();
        //    if(t.ID==cameraCount)
        //    {
              
        //    }
            
        //}

        private int cameraCount=0, valveCount;

        public void ReadPos(string fileName)
        {
            try
            {
                PosArray = new List<int>();
                IOs = new List<ushort>();
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(fileName);
                XmlNodeList xnl;
                XmlNode xn = null;
                xn = xmldoc.GetElementsByTagName("位置")[0];
                xnl = xn.ChildNodes;
                foreach (XmlNode x in xnl)
                {
                    XmlElement xe = x as XmlElement;
                    int r = 0;
                    int.TryParse(xe.Attributes["Pos"].Value, out r);
                    PosArray.Add(r);
                }
                xn = xmldoc.GetElementsByTagName("相机电磁阀点")[0];
                xnl = xn.ChildNodes;
                foreach (XmlNode x in xnl)
                {
                    XmlElement xe = x as XmlElement;
                    ushort r = 0;
                    ushort.TryParse(xe.Attributes["IO"].Value, out r);
                    IOs.Add(r);
                }
                xn = xmldoc.GetElementsByTagName("相机数量")[0];
                xnl = xn.ChildNodes;
                foreach (XmlNode x in xnl)
                {
                    XmlElement xe = x as XmlElement;
                    ushort r = 0;
                    ushort.TryParse(xe.Attributes["Count"].Value, out r);
                    cameraCount = r;
                }

                xn = xmldoc.GetElementsByTagName("电磁阀数量")[0];
                xnl = xn.ChildNodes;
                foreach (XmlNode x in xnl)
                {
                    XmlElement xe = x as XmlElement;
                    ushort r = 0;
                    ushort.TryParse(xe.Attributes["Count"].Value, out r);
                    valveCount = r;
                }
            }
            catch(Exception e)
            {
                MyDebug.ShowMessage(e, "相机位置初始化错误");
            }
            XmlDocument xd = new XmlDocument();
            string path = PathHelper.dataPath + @"\Position.xml";
            xd.Load(fileName);

            XmlNode xnd;
            xnd = xd.GetElementsByTagName("速度")[0];

            if (xnd != null)
            {
                XmlNodeList xnl;
                xnl = xnd.ChildNodes;
                foreach (XmlNode x in xnl)
                {
                    Card.maxspeed=int.Parse(x.Attributes["Value"].Value);
                    Card.maxspeed = Card.maxspeed < Card.minspeed ? 1000 : Card.maxspeed;
                }

            }
            
        }

        //void SpeedTime_OnTime(object sender, EventArgs e)
        //{
        //    if (speedTemp == 0)
        //    {
        //        pn.countPerMinitor = ((speedTemp) * 1000.000) / (SpeedTime.Interval * 1.000);
        //        //speedTemp = 0;
        //    }
        //    else
        //    {
        //        pn.countPerMinitor = ((speedTemp + 0.5) * 1000.000) / (SpeedTime.Interval * 1.000);

        //    }
        //    speedTemp = 0;

        //}
        MyTimer watchdog = new MyTimer();
        void watchdog_OnTimerEvent(object sender, EventArgs e)
        {
            isbussy = false;
            
        }
        void niuli()
        {
            if (Card.mathineTyple == MathineTyple.fendu)
            {
               int st1 = PCI408.PCI408_read_inbit(Card.cardNo, Card.servoAlarm);
               if (st1 == Card.Off)
               {
                   if (StrongPressEventHandler != null)
                   {
                       zhuanpanStateRuning = false;

                       zhuanPanIsRun = false;
                       StrongPressEventHandler();
                   }
               }
               int state= PCI408.PCI408_read_inbit(Card.cardNo, Card.zhizhenSensor);
               if (state != Card.Off&& zhuanPanIsRun&&zhuanpanStateRuning)
               {
                   lackStop();
                   zhuanpanStateRuning = false;
               }
               else if (state == Card.Off && zhuanPanIsRun && !zhizhenLowSensor.timer.IsRunning && !zhuanpanStateRuning)
               {
                   lackStart();
                   
               }
            }
        }
        int niulimax = 0;
        void MySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
            byte[] data = new byte[21];
            if (MySerialPort.BytesToRead==21)
            MySerialPort.Read(data, 0, 21);
            if (data[0] == 1)
            {
                MySerialPort.DiscardInBuffer();
                watchdog.timer.Stop();
                if (data[18] > niulimax)
                {
                    if (StrongPressEventHandler != null)
                    {
                        zhuanpanStateRuning = false;

                        zhuanPanIsRun = false;
                        StrongPressEventHandler();
                    }
                }

                isbussy = false;
            }
        }
        void zhizhenLowSensor_OnTimerEvent(object sender, EventArgs e)
        {
            int state = PCI408.PCI408_read_inbit(Card.cardNo, Card.zhizhenSensor);
            if (state != Card.On)
            {
                SpeedTime.Start();
                PCI408.PCI408_write_SEVON_PIN(Card.cardNo, Card.On);
                PCI408.PCI408_set_profile(Card.cardNo, Card.minspeed, Card.maxspeed, Card.acc, Card.acc);
                PCI408.PCI408_vmove(Card.cardNo, 0, Card.maxspeed);
                PCI408.PCI408_write_outbit(Card.cardNo,Card.chuiqizongkaiguan,Card.On);
                zhuanpanStateRuning = true;
                zhizhenLowSensor.timer.Stop();
            }
        }
        void StartDelay_OnTimerEvent(object sender, EventArgs e)
        {
            niulimax = Card.niulimax2;
        }
        public void servoAlarmClear()
        {
            PCI408.PCI408_write_outbit(Card.cardNo, Card.servoRes, Card.On);
            Thread.Sleep(100);
            PCI408.PCI408_write_outbit(Card.cardNo, Card.servoRes, Card.Off);
        }
    }
}