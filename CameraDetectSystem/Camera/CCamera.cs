using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using HalconDotNet;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;
using Basler.Pylon;

namespace CameraDetectSystem
{
    /// <summary>
    /// 相机,一个camera代表一个相机拍照
    /// </summary>
    public class CCamera
    {
        public List<int> goodcountlist;
        public List<int> badcountlist;

        public string name;
        public string logicName;
        public string physicsname;
        public int sortnum;
        public List<HObject> listimage;
        public Queue<HObject> listimage1;
    //    public ImageProvider m_imageProvider;
        public Camera camera;

        public delegate void OnImageProcessedEventHandler(CCamera instance, HObject ho_Image);
        //private delegate void DeviceRemovedEventHandler();
        public List<ImageTools> imageTools;
        public event OnImageProcessedEventHandler OnImageProcessedEvent;

        public HObject Image;
        private IntPtr latestFrameAddress = IntPtr.Zero;
        private PixelDataConverter converter = new PixelDataConverter();
        volatile private HObject hPylonImage = null;
        private HObject TImage = null;
        
        /// <summary>
        /// 图像处理自定义委托
        /// </summary>
        /// <param name="hImage">halcon图像变量</param>
        //public delegate void delegateProcessHImage(HObject hImage);
        /// <summary>
        /// 图像处理委托事件
        /// </summary>
        //public event delegateProcessHImage eventProcessImage;


 //       public delegate void OnImageProcessedEventHandler(Camera instance, HObject ho_Image);
        //private delegate void DeviceRemovedEventHandler();

      //  public List<ImageTools> imageTools;
   //     public List<DeviceEnumerator.Device> list;
   //     public event OnImageProcessedEventHandler OnImageProcessedEvent;

        /// <summary>
        /// if >= Sfnc2_0_0,说明是ＵＳＢ３的相机
        /// </summary>
        static Version Sfnc2_0_0 = new Version(2, 0, 0);

        public HTuple resultHTuple;
        public HObject RegionToDisp;
        public Result result;
        protected Object m_lockObject;
        public HObject tempImage;
        public List<DataSelected> dataSelectedShowed;
        public bool isopen;
        private int exposureTime;
        Thread threadp;
        public int ExposureTime
        {
            get { return exposureTime; }
            set { exposureTime = value; }
        }
        private int gain;

        public int Gain
        {
            get { return gain; }
            set { gain = value;  }
        }
        private double pixelDist;

        public double PixelDist
        {
            get { return pixelDist; }
            set { pixelDist = value; }
        }

        public int poscmin;
        public int disposnum =0;
        public int imagenum =0;
        bool getcpu = true;
        public bool isfirst  = true;
        /// <summary>
        /// 创建一个相机，相机初始化
        /// </summary>
        public CCamera( string name)
        {
            try
            {
                isopen = false;
                imageTools = new List<ImageTools>();
                isfirst = true;
                List<ICameraInfo> allCameraInfos = CameraFinder.Enumerate();
                bool isconnect = false;
                foreach (ICameraInfo cameraInfo in allCameraInfos)
                {
                    Console.WriteLine(cameraInfo[CameraInfoKey.FriendlyName] + "--" + name);
                    if (cameraInfo[CameraInfoKey.FriendlyName] == name)
                    {
                        physicsname = cameraInfo[CameraInfoKey.FriendlyName];
                        camera = new Camera(cameraInfo);
                        isconnect = true;
                    }
                }

                if (isconnect == false)
                {
                    MessageBox.Show(name + "没有连接");
                }
                resultHTuple = new HTuple();
                result = new Result();
                HOperatorSet.GenEmptyObj(out RegionToDisp);
                HOperatorSet.GenEmptyObj(out Image);
                HOperatorSet.GenEmptyObj(out TImage);
                HOperatorSet.GenEmptyObj(out tempImage);
                m_lockObject = new Object();
                dataSelectedShowed = new List<DataSelected>();
                exposureTime = 100;
                gain = 300;
                pixelDist = 1;
                getcpu = true;
                listimage = new List<HObject>();
                listimage1 = new Queue<HObject>();
                goodcountlist = new List<int>();
                badcountlist = new List<int>();
            }
            catch (Exception)
            {
                MessageBox.Show("相机初始化失败");
            }
        }
        /// <summary>
        /// 打开所有的相机设备
        /// </summary>
        public void Open()
        {
            //相机个数
            try
            {
                camera.Open();
                //camera.Parameters[PLCamera.AcquisitionFrameRateEnable].SetValue(true);  // 限制相机帧率
                //camera.Parameters[PLCamera.AcquisitionFrameRateAbs].SetValue(90);
                //camera.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(10);          // 设置内存中接收图像缓冲区大小

                //     imageWidth = camera.Parameters[PLCamera.Width].GetValue();               // 获取图像宽 
                //     imageHeight = camera.Parameters[PLCamera.Height].GetValue();              // 获取图像高
                //     GetMinMaxExposureTime();
                //      GetMinMaxGain();
                camera.StreamGrabber.ImageGrabbed += OnImageGrabbed;                      // 注册采集回调函数
                //    camera.ConnectionLost += OnConnectionLost;
                isopen = true;
            }
            catch (Exception e)
            {
           //     ShowException(e);
            }
        }
        /// <summary>
        /// 关闭相机,释放相关资源
        /// </summary>
        public void CloseCam()
        {
            try
            {
                camera.Close();
                camera.Dispose();

                if (hPylonImage != null)
                {
                    hPylonImage.Dispose();
                }

                if (latestFrameAddress != null)
                {
                    Marshal.FreeHGlobal(latestFrameAddress);
                    latestFrameAddress = IntPtr.Zero;
                }
            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }
        /// <summary>
        /// 开始拍照
        /// </summary>
        public void StartGrab()
        {
            XmlDocument xmlDocCamera = new XmlDocument();
            xmlDocCamera.Load("./Data/" + "CameraSettings.xml");
            XmlNodeList xnl;
            XmlNode xn;
            xn = xmlDocCamera.GetElementsByTagName("Camera")[0];
            xnl = xn.ChildNodes;

            foreach (XmlNode x in xnl)
            {
                if (this.logicName == x.Name)
                {
                    ExposureTime = int.Parse(x.Attributes["ExposureTime"].Value);
                    Gain = int.Parse(x.Attributes["Gain"].Value);
                    PixelDist = double.Parse(x.Attributes["PixelDist"].Value);
                }
            }
            SetExternTrigger();
            SetExposureTime(ExposureTime);
            SetGain(Gain);
          
           // threadp.Start();

            StartGrabbing();
            foreach (ImageTools it in imageTools)
            {
                it.pixeldist = this.PixelDist;
            }
            
        }

        /// <summary>
        /// 开始连续采集
        /// </summary>
        public bool StartGrabbing()
        {
            try
            {
                if (camera.StreamGrabber.IsGrabbing)
                {
                    MessageBox.Show("相机当前正处于采集状态！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);

                    camera.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                    //            stopWatch.Restart();    // ****  重启采集时间计时器   ****
                    threadp = new Thread(processimage);
                    threadp.IsBackground = true;
                    threadp.Start();
                    return true;
                }
            }
            catch (Exception e)
            {
                ShowException(e);
                return false;
            }
        }

        /// <summary>
        /// 停止连续采集
        /// </summary>
        public void StopGrabbing()
        {
            try
            {
                if (camera.StreamGrabber.IsGrabbing)
                {
                    camera.StreamGrabber.Stop();
                    threadp.Abort();
                    listimage.Clear();
                }
            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }
        public int kkk = 0;
            /// <summary>
              ///  相机取像回调函数.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
        private void OnImageGrabbed(Object sender, ImageGrabbedEventArgs e)
        {
            try
            {

                // Acquire the image from the camera. Only show the latest image. The camera may acquire images faster than the images can be displayed.
                // Get the grab result.
                if ( getcpu )
                {
                    if (sortnum == 0)
                    {
                    
                        GetCpu.SetThreadAffinityMask(GetCpu.GetCurrentThread(), new UIntPtr(0x1));
                    }
                    else
                    {
                        GetCpu.SetThreadAffinityMask(GetCpu.GetCurrentThread(), new UIntPtr(0x2));
                    }

                    Thread.CurrentThread.Priority = ThreadPriority.Highest;
                    getcpu = false;
                }
          
                IGrabResult grabResult =e.GrabResult;
                // Check if the image can be displayed.
                if (grabResult.GrabSucceeded)
                {
                        //stopWatch.Restart();
                        // 判断是否是黑白图片格式
                        if (grabResult.PixelTypeValue == PixelType.Mono8)
                        {
                            //allocate the m_stream_size amount of bytes in non-managed environment 
                            if (latestFrameAddress == IntPtr.Zero)
                            {
                                latestFrameAddress = Marshal.AllocHGlobal((Int32)grabResult.PayloadSize);
                            }
                            converter.OutputPixelFormat = PixelType.Mono8;
                            converter.Convert(latestFrameAddress, grabResult.PayloadSize, grabResult);

                            // 转换为Halcon图像显示
                            HOperatorSet.GenImage1(out hPylonImage, "byte", (HTuple)grabResult.Width, (HTuple)grabResult.Height, (HTuple)latestFrameAddress);

                        }
                        else if (grabResult.PixelTypeValue == PixelType.BayerBG8 || grabResult.PixelTypeValue == PixelType.BayerGB8
                                    || grabResult.PixelTypeValue == PixelType.BayerRG8 || grabResult.PixelTypeValue == PixelType.BayerGR8)
                        {
                            int imageWidth = grabResult.Width - 1;
                            int imageHeight = grabResult.Height - 1;
                            int payloadSize = imageWidth * imageHeight;

                            //allocate the m_stream_size amount of bytes in non-managed environment 
                            if (latestFrameAddress == IntPtr.Zero)
                            {
                                latestFrameAddress = Marshal.AllocHGlobal((Int32)(3 * payloadSize));
                            }
                            converter.OutputPixelFormat = PixelType.BGR8packed;     // 根据bayer格式不同切换以下代码
                         
                            converter.Parameters[PLPixelDataConverter.InconvertibleEdgeHandling].SetValue("Clip");
                            converter.Convert(latestFrameAddress, 3 * payloadSize, grabResult);

                            HOperatorSet.GenImageInterleaved(out hPylonImage, latestFrameAddress, "bgr",
                                     (HTuple)imageWidth, (HTuple)imageHeight, -1, "byte", (HTuple)imageWidth, (HTuple)imageHeight, 0, 0, -1, 0);

                        }

                        // 抛出图像处理事件
                        if (hPylonImage == null)
                        {
                            MessageBox.Show("hPylonImage null");
                        }
                        //Console.WriteLine("CAMmm" + sortnum + "when--1--" + DateTime.Now.ToString("mm-ss-fff"));
                        poscmin = Turntable.Instance.copos[sortnum].pos.FirstOrDefault();
                      //  poscmin = Turntable.Instance.copos[sortnum].pos.First();
                        //Console.WriteLine("w" + sortnum + "ss" + poscmin);
                       Turntable.Instance.copos[sortnum].pos.Clear();
                        imagenum++;
                        //if (kkk > 1 && sortnum == 0)
                        //{
                        //    HOperatorSet.WriteImage(hPylonImage, "bmp", 0, @"C:\Users\mxw\Desktop\a2.bmp");
                        //    Console.WriteLine(sortnum+1);

                        ////}
                        HObject mimage = new HObject();
                        HOperatorSet.GenEmptyObj(out mimage);
                        mimage.Dispose();
                       // TImage.Dispose();
                        HOperatorSet.CopyImage(hPylonImage, out mimage);
                       // listimage.Add(mimage);
                        listimage1.Enqueue(mimage);
                        hPylonImage.Dispose();
                       
       
                }
                else
                {
                    MessageBox.Show("Grab faild!\n" + grabResult.ErrorDescription, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine("  ShowException(exception);");
             //   ShowException(exception);
              //  Turntable.Instance.copos[sortnum + 1].onwrite = false;
                poscmin = Turntable.Instance.copos[sortnum].pos.FirstOrDefault();
                Turntable.Instance.copos[sortnum].pos.Clear();
            }
            finally
            {
              
                // Dispose the grab result if needed for returning it to the grab loop.
                e.DisposeGrabResultIfClone();
          //    Console.WriteLine("CAM" + sortnum + "when--2--" + DateTime.Now.ToString("mm-ss-fff"));
            }
        }

        /// <summary>
        /// 设置相机外触发模式
        /// </summary>
        public void SetExternTrigger()
        {
            try
            {
                if (camera.GetSfncVersion() < Sfnc2_0_0)
                {
                    if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart))
                    {
                        if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart))
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);

                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                            camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Line1);
                        }
                        else
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.AcquisitionStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                            camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Line1);
                        }
                    }

                    //Sets the trigger delay time in microseconds.
                    camera.Parameters[PLCamera.TriggerDelayAbs].SetValue(5);        // 设置触发延时

                    //Sets the absolute value of the selected line debouncer time in microseconds
                    camera.Parameters[PLCamera.LineSelector].TrySetValue(PLCamera.LineSelector.Line1);
                    camera.Parameters[PLCamera.LineMode].TrySetValue(PLCamera.LineMode.Input);
                    camera.Parameters[PLCamera.LineDebouncerTimeAbs].SetValue(5);       // 设置去抖延时，过滤触发信号干扰
                }
                else // For SFNC 2.0 cameras, e.g. USB3 Vision cameras
                {
                    if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart))
                    {
                        if (camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart))
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);

                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                            camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Line1);
                        }
                        else
                        {
                            camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameBurstStart);
                            camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                            camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Line1);
                        }
                    }

                    //Sets the trigger delay time in microseconds.//float
                    camera.Parameters[PLCamera.TriggerDelay].SetValue(5);       // 设置触发延时

                    //Sets the absolute value of the selected line debouncer time in microseconds
                    camera.Parameters[PLCamera.LineSelector].TrySetValue(PLCamera.LineSelector.Line1);
                    camera.Parameters[PLCamera.LineMode].TrySetValue(PLCamera.LineMode.Input);
                    camera.Parameters[PLCamera.LineDebouncerTime].SetValue(5);       // 设置去抖延时，过滤触发信号干扰

                }
           //     stopWatch.Reset();    // ****  重置采集时间计时器   ****
            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }

        /// <summary>
        /// 设置相机曝光时间
        /// </summary>
        /// <param name="value"></param>
        public void SetExposureTime(long value)
        {
            try
            {
                // Some camera models may have auto functions enabled. To set the ExposureTime value to a specific value,
                // the ExposureAuto function must be disabled first (if ExposureAuto is available).
                camera.Parameters[PLCamera.ExposureAuto].TrySetValue(PLCamera.ExposureAuto.Off); // Set ExposureAuto to Off if it is writable.
                camera.Parameters[PLCamera.ExposureMode].TrySetValue(PLCamera.ExposureMode.Timed); // Set ExposureMode to Timed if it is writable.

                if (camera.GetSfncVersion() < Sfnc2_0_0)
                {
                    // In previous SFNC versions, ExposureTimeRaw is an integer parameter,单位us
                    // integer parameter的数据，设置之前，需要进行有效值整合，否则可能会报错
                    long min = camera.Parameters[PLCamera.ExposureTimeRaw].GetMinimum();
                    long max = camera.Parameters[PLCamera.ExposureTimeRaw].GetMaximum();
                    long incr = camera.Parameters[PLCamera.ExposureTimeRaw].GetIncrement();
                    if (value < min)
                    {
                        value = min;
                    }
                    else if (value > max)
                    {
                        value = max;
                    }
                    else
                    {
                        value = min + (((value - min) / incr) * incr);
                    }
                    camera.Parameters[PLCamera.ExposureTimeRaw].SetValue(value);

                    // Or,here, we let pylon correct the value if needed.
                    //camera.Parameters[PLCamera.ExposureTimeRaw].SetValue(value, IntegerValueCorrection.Nearest);
                }
                else // For SFNC 2.0 cameras, e.g. USB3 Vision cameras
                {
                    // In SFNC 2.0, ExposureTimeRaw is renamed as ExposureTime,is a float parameter, 单位us.
                    camera.Parameters[PLUsbCamera.ExposureTime].SetValue((double)value);
                }
            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }

        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="value"></param>
        public void SetGain(long value)
        {
            try
            {
                // Some camera models may have auto functions enabled. To set the gain value to a specific value,
                // the Gain Auto function must be disabled first (if gain auto is available).
                camera.Parameters[PLCamera.GainAuto].TrySetValue(PLCamera.GainAuto.Off); // Set GainAuto to Off if it is writable.

                if (camera.GetSfncVersion() < Sfnc2_0_0)
                {
                    // Some parameters have restrictions. You can use GetIncrement/GetMinimum/GetMaximum to make sure you set a valid value.                              
                    // In previous SFNC versions, GainRaw is an integer parameter.
                    // integer parameter的数据，设置之前，需要进行有效值整合，否则可能会报错
                    long min = camera.Parameters[PLCamera.GainRaw].GetMinimum();
                    long max = camera.Parameters[PLCamera.GainRaw].GetMaximum();
                    long incr = camera.Parameters[PLCamera.GainRaw].GetIncrement();
                    if (value < min)
                    {
                        value = min;
                    }
                    else if (value > max)
                    {
                        value = max;
                    }
                    else
                    {
                        value = min + (((value - min) / incr) * incr);
                    }
                    camera.Parameters[PLCamera.GainRaw].SetValue(value);

                    //// Or,here, we let pylon correct the value if needed.
                    //camera.Parameters[PLCamera.GainRaw].SetValue(value, IntegerValueCorrection.Nearest);
                }
                else // For SFNC 2.0 cameras, e.g. USB3 Vision cameras
                {
                    // In SFNC 2.0, Gain is a float parameter.
                    camera.Parameters[PLUsbCamera.Gain].SetValue(value);
                }
            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }


        public void processimage()
        {

            while (true)
            {
                while (listimage1.Count != 0)
                {
                    try
                    {
           

                        Image.Dispose(); 
                        TImage = listimage1.Dequeue();
                        HOperatorSet.CopyImage(TImage, out Image);
                        TImage.Dispose();
                      
                        if (Image == null)
                        {
                            Console.WriteLine("Image == null");
                            return;
                        }
                        result = new Result();
                        HTuple r = new HTuple();
                        this.resultHTuple = new HTuple();
                        foreach (ImageTools it in imageTools)
                        {
                            it.Image = Image;
                            it.method();
                            if (it.RegionToDisp.IsInitialized() && it.RegionToDisp.CountObj() > 0)
                            {
                                if (!RegionToDisp.IsInitialized())
                                    HOperatorSet.CopyObj(it.RegionToDisp, out RegionToDisp, 1, 1);// RegionToDisp = it.RegionToDisp;
                                else
                                {
                                    HOperatorSet.ConcatObj(it.RegionToDisp, RegionToDisp, out RegionToDisp);
                                }
                            }
                            it.RegionToDisp.Dispose();
                            r = r.TupleConcat(it.result);
                            this.resultHTuple = this.resultHTuple.TupleConcat(it.result.Clone());

                        }
                        if (r.Length > 0)
                        {
                            result.GetResult(r.Clone());
                            result.GetResultToShow(dataSelectedShowed);
                        }
                    }
                    catch (Exception e)
                    {
                        //  MyDebug.ShowMessage(e, "Dispose Image and Region");

                        foreach (ImageTools it in imageTools)
                        {
                            it.RegionToDisp.Dispose();
                        }
                        RegionToDisp.Dispose();
                    }
                    finally
                    {
                        disposnum++;
                        var handler = OnImageProcessedEvent;
                        if (handler != null && Image != null)
                            handler(this, Image);
                    }
                }
                Thread.Sleep(1);
            }

        }

        /// <summary>
        /// 处理图像
        /// </summary>
        /// <param name="himage"></param>
      
      
        private void ShowException(Exception exception)
        {
            MessageBox.Show("Exception caught:\n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
     
    }
}
