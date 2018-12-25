using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;


namespace CameraDetectSystem
{
    public class PCI408
    {
        //---------------------   板卡初始和配置函数  ----------------------
        /********************************************************************************
        ** 函数名称: PCI408_card_init
        ** 功能描述: 控制板初始化，设置初始化和速度等设置
        ** 输　  入: 无
        ** 返 回 值: 0：无卡； 1-8：成功(实际卡数) 
        **     
        *********************************************************************************/
        [DllImport("PCI408.dll", EntryPoint = "PCI408_card_init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 PCI408_card_init();

        /********************************************************************************
        ** 函数名称: PCI408_card_close
        ** 功能描述: 关闭所有卡
        ** 输　  入: 无
        ** 返 回 值: 无
        ** 日    期: 2014.02.1
        *********************************************************************************/
        [DllImport("PCI408.dll", EntryPoint = "PCI408_card_close", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void PCI408_board_close();

        /********************************************************************************
        ** 函数名称: 控制卡复位
        ** 功能描述: 复位所有卡，只能在初始化完成之后调用．
        ** 输　  入: 卡号
        ** 返 回 值: 错误码
        ** 日    期:
        *********************************************************************************/
        [DllImport("PCI408.dll", EntryPoint = "PCI408_card_reset", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_card_reset(UInt16 card);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_card_version", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_card_version(UInt16 card);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_card_soft_version", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_card_soft_version(UInt16 card, ref UInt16 firm_id, ref UInt32 sub_firm_id);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_client_ID", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_client_ID(UInt16 card);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_lib_version", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_lib_version();

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_card_ID", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_card_ID(UInt16 card);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_total_axes", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_total_axes(UInt16 card);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_test_software", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_test_software(UInt16 card, UInt16 testid, UInt16 para1, UInt16 para2, UInt16 para3);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_test_hardware", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_test_hardware(UInt16 card);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_download_firmware", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_download_firmware(UInt16 card, ref char pfilename);


        //脉冲输入输出配置

        [DllImport("PCI408.dll", EntryPoint = "PCI408_set_pulse_outmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_set_pulse_outmode(UInt16 axis, UInt16 outmode);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_counter_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_counter_config(UInt16 axis, UInt16 mode);


        //添加配置读

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_pulse_outmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_pulse_outmode(UInt16 axis, ref UInt16 outmode);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_counter_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_counter_config(UInt16 axis, ref UInt16 mode);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_config_SD_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_config_SD_PIN(UInt16 axis, ref UInt16 enable, ref UInt16 sd_logic, ref UInt16 sd_mode);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_config_INP_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_config_INP_PIN(UInt16 axis, ref UInt16 enable, ref UInt16 inp_logic);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_config_ERC_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_config_ERC_PIN(UInt16 axis, ref UInt16 enable, ref UInt16 erc_logic,
                        ref UInt16 erc_width, ref UInt16 erc_off_time);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_config_ALM_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_config_ALM_PIN(UInt16 axis, ref UInt16 enable, ref UInt16 alm_logic, ref UInt16 alm_action);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_config_EL_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_config_EL_PIN(UInt16 axis, ref UInt16 el_logic, ref UInt16 el_mode);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_config_HOME_PIN_logic", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_config_HOME_PIN_logic(UInt16 axis, ref UInt16 org_logic, ref UInt16 filter);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_config_home_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_config_home_mode(UInt16 axis, ref UInt16 home_dir, ref double vel, ref UInt16 home_mode, ref UInt16 EZ_count);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_handwheel_inmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_handwheel_inmode(UInt16 axis, ref UInt16 inmode, ref double multi);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_config_LTC_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_config_LTC_PIN(UInt16 axis, ref UInt16 ltc_logic, ref UInt16 ltc_mode);

        //[DllImport("PCI408.dll", EntryPoint = "PCI408_get_config_latch_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        //public static extern UInt32  PCI408_get_config_latch_mode(UInt16 cardno, ref UInt16 all_enable);


        //专用信号设置函数

        [DllImport("PCI408.dll", EntryPoint = "PCI408_config_SD_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_config_SD_PIN(UInt16 axis, UInt16 enable, UInt16 sd_logic, UInt16 sd_mode);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_config_INP_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_config_INP_PIN(UInt16 axis, UInt16 enable, UInt16 inp_logic);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_config_ERC_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_config_ERC_PIN(UInt16 axis, UInt16 enable, UInt16 erc_logic,
                        UInt16 erc_width, UInt16 erc_off_time);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_config_EMG_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_config_EMG_PIN(UInt16 cardno, UInt16 option, UInt16 emg_logic);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_config_EMG_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_config_EMG_PIN(UInt16 cardno, ref UInt16 enbale, ref UInt16 emg_logic);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_config_ALM_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_config_ALM_PIN(UInt16 axis, UInt16 enable, UInt16 alm_logic, UInt16 alm_action);
        //new

        [DllImport("PCI408.dll", EntryPoint = "PCI408_config_EL_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_config_EL_PIN(UInt16 axis, UInt16 el_logic, UInt16 el_mode);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_config_HOME_PIN_logic", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_config_HOME_PIN_logic(UInt16 axis, UInt16 org_logic, UInt16 filter);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_write_SEVON_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_write_SEVON_PIN(UInt16 axis, UInt16 on_off);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_read_SEVON_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 PCI408_read_SEVON_PIN(UInt16 axis);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_write_ERC_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_write_ERC_PIN(UInt16 axis, UInt16 sel);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_read_RDY_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 PCI408_read_RDY_PIN(UInt16 axis);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_set_backlash", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_set_backlash(UInt16 axis, Int32 backlash);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_backlash", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_backlash(UInt16 axis, ref Int32 backlash);

        //通用输入/输出控制函数

        [DllImport("PCI408.dll", EntryPoint = "PCI408_read_inbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 PCI408_read_inbit(UInt16 cardno, UInt16 bitno);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_write_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_write_outbit(UInt16 cardno, UInt16 bitno, UInt16 on_off);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_read_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 PCI408_read_outbit(UInt16 cardno, UInt16 bitno);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_read_inport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 PCI408_read_inport(UInt16 cardno);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_read_outport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 PCI408_read_outport(UInt16 cardno);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_write_outport", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_write_outport(UInt16 cardno, UInt32 port_value);

        //制动函数

        [DllImport("PCI408.dll", EntryPoint = "PCI408_decel_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_decel_stop(UInt16 axis, double dec);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_imd_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_imd_stop(UInt16 axis);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_emg_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_emg_stop();

        [DllImport("PCI408.dll", EntryPoint = "PCI408_simultaneous_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_simultaneous_stop(UInt16 axis);

        //位置设置和读取函数

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_position", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 PCI408_get_position(UInt16 axis);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_set_position", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_set_position(UInt16 axis, Int32 current_position);


        //状态检测函数

        [DllImport("PCI408.dll", EntryPoint = "PCI408_check_done", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 PCI408_check_done(UInt16 axis);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_axis_io_status", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_axis_io_status(UInt16 axis);


        //速度设置

        [DllImport("PCI408.dll", EntryPoint = "PCI408_read_current_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern double PCI408_read_current_speed(UInt16 axis);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_read_vector_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern double PCI408_read_vector_speed(UInt16 card);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_change_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_change_speed(UInt16 axis, double Curr_Vel);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_set_vector_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_set_vector_profile(UInt16 cardno, double s_para, double Max_Vel, double acc, double dec);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_vector_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_vector_profile(UInt16 cardno, ref double s_para, ref double Max_Vel, ref double acc, ref double dec);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_set_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_set_profile(UInt16 axis, double option, double Max_Vel, double acc, double dec);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_set_s_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_set_s_profile(UInt16 axis, double s_para);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_profile(UInt16 axis, ref double option, ref double Max_Vel, ref double acc, ref double dec);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_s_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_s_profile(UInt16 axis, ref double s_para);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_reset_target_position", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_reset_target_position(UInt16 axis, Int32 dist);

        //单轴定长运动

        [DllImport("PCI408.dll", EntryPoint = "PCI408_pmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_pmove(UInt16 axis, Int32 Dist, UInt16 posi_mode);

        //单轴连续运动

        [DllImport("PCI408.dll", EntryPoint = "PCI408_vmove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_vmove(UInt16 axis, UInt16 dir, double vel);

        //线性插补

        [DllImport("PCI408.dll", EntryPoint = "PCI408_line2", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_line2(UInt16 axis1, Int32 Dist1, UInt16 axis2, Int32 Dist2, UInt16 posi_mode);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_line3", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_line3(ref UInt16 axis, Int32 Dist1, Int32 Dist2, Int32 Dist3, UInt16 posi_mode);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_line4", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_line4(UInt16 cardno, Int32 Dist1, Int32 Dist2, Int32 Dist3, Int32 Dist4, UInt16 posi_mode);

        //手轮运动

        [DllImport("PCI408.dll", EntryPoint = "PCI408_set_handwheel_inmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_set_handwheel_inmode(UInt16 axis, UInt16 inmode, double multi);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_handwheel_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_handwheel_move(UInt16 axis);

        //找原点

        [DllImport("PCI408.dll", EntryPoint = "PCI408_config_home_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_config_home_mode(UInt16 axis, UInt16 home_dir, double vel, UInt16 mode, UInt16 EZ_count);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_home_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_home_move(UInt16 axis);

        //圆弧插补

        [DllImport("PCI408.dll", EntryPoint = "PCI408_arc_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_arc_move(ref UInt16 axis, ref Int32 target_pos, ref Int32 cen_pos, UInt16 arc_dir);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_rel_arc_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_rel_arc_move(ref UInt16 axis, ref Int32 rel_pos, ref Int32 rel_cen, UInt16 arc_dir);

        //设置和读取位置比较信号

        [DllImport("PCI408.dll", EntryPoint = "PCI408_compare_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_compare_config(UInt16 card, UInt16 enable, UInt16 axis, UInt16 cmp_source);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_compare_get_config", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_compare_get_config(UInt16 card, ref UInt16 enable, ref UInt16 axis, ref UInt16 cmp_source);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_compare_clear_points", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_compare_clear_points(UInt16 card);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_compare_add_point", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_compare_add_point(UInt16 card, Int32 pos, UInt16 dir, UInt16 action, Int32 actpara);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_compare_get_current_point", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 PCI408_compare_get_current_point(UInt16 card);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_compare_get_points_runned", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 PCI408_compare_get_points_runned(UInt16 card);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_compare_get_points_remained", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 PCI408_compare_get_points_remained(UInt16 card);

        //---------------------   分选 ----------------------//

        //中断回调函数指针定义
        //中断回调函数指针定义
        //callback
        public delegate uint COMPARE_OPERATE_FUN(UInt16 card, UInt16 queue, IntPtr operate_data);
        [DllImport("PCI408.dll", EntryPoint = "PCI408_compare_regist_callback", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_compare_regist_callback(UInt16 cardno, COMPARE_OPERATE_FUN funcIntHandler, IntPtr operate_data);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_compare_set_pulsetimes_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_compare_set_pulsetimes_Extern(UInt16 card, UInt16 queue, UInt16 itimes);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_compare_set_filter_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_compare_set_filter_Extern(UInt16 card, UInt32 frequency);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_compare_get_filter_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_compare_get_filter_Extern(UInt16 card, ref UInt32 frequency);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_compare_config_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_compare_config_Extern(UInt16 card, UInt16 queue, UInt16 enable, UInt16 axis, UInt16 cmp_source);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_compare_get_config_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_compare_get_config_Extern(UInt16 card, UInt16 queue, ref UInt16 enable, ref UInt16 axis, ref UInt16 cmp_source);


        [DllImport("PCI408.dll", EntryPoint = "PCI408_compare_clear_points_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_compare_clear_points_Extern(UInt16 card, UInt16 queue);


        [DllImport("PCI408.dll", EntryPoint = "PCI408_compare_add_point_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_compare_add_point_Extern(UInt16 card, UInt16 queue, Int32 pos, UInt16 dir, UInt16 action, Int32 actpara);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_compare_get_current_point_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 PCI408_compare_get_current_point_Extern(UInt16 card, UInt16 queue);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_compare_get_points_runned_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 PCI408_compare_get_points_runned_Extern(UInt16 card, UInt16 queue);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_compare_get_points_remained_Extern", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 PCI408_compare_get_points_remained_Extern(UInt16 card, UInt16 queue);
        //---------------------   编码器计数功能  ----------------------//

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_encoder", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 PCI408_get_encoder(UInt16 axis);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_set_encoder", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_set_encoder(UInt16 axis, Int32 encoder_value);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_config_EZ_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_config_EZ_PIN(UInt16 axis, UInt16 ez_logic, UInt16 ez_mode);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_config_EZ_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_config_EZ_PIN(UInt16 axis, ref UInt16 ez_logic, ref UInt16 ez_mode);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_config_LTC_PIN", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_config_LTC_PIN(UInt16 axis, UInt16 ltc_logic, UInt16 ltc_mode);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_config_latch_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_config_latch_mode(UInt16 cardno, UInt16 all_enable);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_latch_value", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 PCI408_get_latch_value(UInt16 axis);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_latch_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 PCI408_get_latch_flag(UInt16 cardno);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_reset_latch_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_reset_latch_flag(UInt16 cardno);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_counter_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 PCI408_get_counter_flag(UInt16 cardno);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_reset_counter_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_reset_counter_flag(UInt16 cardno);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_reset_clear_flag", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_reset_clear_flag(UInt16 cardno);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_triger_chunnel", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_triger_chunnel(UInt16 cardno, UInt16 num);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_set_speaker_logic", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_set_speaker_logic(UInt16 cardno, UInt16 logic);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_speaker_logic", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_speaker_logic(UInt16 cardno, ref UInt16 logic);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_config_latch_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_config_latch_mode(UInt16 cardno, ref UInt16 all_enable);

        //软件限位功能

        [DllImport("PCI408.dll", EntryPoint = "PCI408_config_softlimit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_config_softlimit(UInt16 axis, UInt16 ON_OFF, UInt16 source_sel, UInt16 SL_action, Int32 N_limit, Int32 P_limit);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_get_config_softlimit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_get_config_softlimit(UInt16 axis, ref UInt16 ON_OFF, ref UInt16 source_sel, ref UInt16 SL_action, ref Int32 N_limit, ref Int32 P_limit);


        //连续插补函数

        [DllImport("PCI408.dll", EntryPoint = "PCI408_conti_lines", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_conti_lines(UInt16 axisNum, ref UInt16 piaxisList,
            ref Int32 pPosList, UInt16 posi_mode);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_conti_arc", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_conti_arc(ref UInt16 axis, ref Int32 rel_pos, ref Int32 rel_cen, UInt16 arc_dir, UInt16 posi_mode);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_conti_restrain_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_conti_restrain_speed(UInt16 card, double v);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_conti_change_speed_ratio", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_conti_change_speed_ratio(UInt16 card, double percent);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_conti_get_current_speed_ratio", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern double PCI408_conti_get_current_speed_ratio(UInt16 card);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_conti_set_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_conti_set_mode(UInt16 card, UInt16 conti_mode, double conti_vl, double conti_para, double filter);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_conti_get_mode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_conti_get_mode(UInt16 card, ref UInt16 conti_mode, ref double conti_vl, ref double conti_para, ref double filter);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_conti_open_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_conti_open_list(UInt16 axisNum, ref UInt16 piaxisList);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_conti_close_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_conti_close_list(UInt16 card);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_conti_check_remain_space", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_conti_check_remain_space(UInt16 card);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_conti_decel_stop_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_conti_decel_stop_list(UInt16 card);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_conti_sudden_stop_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_conti_sudden_stop_list(UInt16 card);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_conti_pause_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_conti_pause_list(UInt16 card);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_conti_start_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_conti_start_list(UInt16 card);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_conti_read_current_mark", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 PCI408_conti_read_current_mark(UInt16 card);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_conti_extern_lines", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_conti_extern_lines(UInt16 axisNum, ref UInt16 piaxisListw,
                                                       ref Int32 pPosList, UInt16 posi_mode, Int32 imark);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_conti_extern_arc", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_conti_extern_arc(ref UInt16 axis, ref Int32 rel_pos, ref Int32 rel_cen, UInt16 arc_dir, UInt16 posi_mode, Int32 imark);

        [DllImport("PCI408.dll", EntryPoint = "PCI408_conti_rel_helix_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_conti_rel_helix_move(UInt16 card, ref Int32 rel_pos, ref Int32 rel_cen, Int32 height, UInt16 arc_dir, Int32 imark);
        //绝对螺旋插补运动函数

        [DllImport("PCI408.dll", EntryPoint = "PCI408_conti_helix_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 PCI408_conti_helix_move(UInt16 card, ref Int32 target_pos, ref Int32 cen_pos, Int32 height, UInt16 arc_dir, Int32 imark);

        //PC库错误码
        enum ERR_CODE_DMC
        {
            ERR_NOERR = 0,          //成功      
            ERR_UNKNOWN = 1,
            ERR_PARAERR = 2,

            ERR_TIMEOUT = 3,
            ERR_CONTROLLERBUSY = 4,
            ERR_CONNECT_TOOMANY = 5,

            ERR_CONTILINE = 6,
            ERR_CANNOT_CONNECTETH = 8,
            ERR_HANDLEERR = 9,
            ERR_SENDERR = 10,
            ERR_FIRMWAREERR = 12, //固件文件错误
            ERR_FIRMWAR_MISMATCH = 14, //固件不匹配

            ERR_FIRMWARE_INVALID_PARA = 20,  //固件参数错误
            ERR_FIRMWARE_PARA_ERR = 20,  //固件参数错误2
            ERR_FIRMWARE_STATE_ERR = 22, //固件当前状态不允许操作
            ERR_FIRMWARE_LIB_STATE_ERR = 22, //固件当前状态不允许操作2
            ERR_FIRMWARE_CARD_NOT_SUPPORT = 24,  //固件不支持的功能 控制器不支持的功能
            ERR_FIRMWARE_LIB_NOTSUPPORT = 24,    //固件不支持的功能2
        };

    }
}
