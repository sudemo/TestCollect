using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoChangeDisplay
{
    // 映射 DEVMODE 结构
    // 可以参照 DEVMODE结构的指针定义：
    // http://msdn.microsoft.com/en-us/library/windows/desktop/dd183565(v=vs.85).aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct DEVMODE
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string dmDeviceName;

        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public int dmFields;
        public int dmPositionX;
        public int dmPositionY;
        public int dmDisplayOrientation;
        public int dmDisplayFixedOutput;
        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string dmFormName;

        public short dmLogPixels;
        public short dmBitsPerPel;
        public int dmPelsWidth;
        public int dmPelsHeight;
        public int dmDisplayFlags;
        public int dmDisplayFrequency;
        public int dmICMMethod;
        public int dmICMIntent;
        public int dmMediaType;
        public int dmDitherType;
        public int dmReserved1;
        public int dmReserved2;
        public int dmPanningWidth;
        public int dmPanningHeight;
    };
    // Win32 函数在托管环境下的声明，外部函数
    public class NativeMethods
    {
        // 平台调用的申明
        [DllImport("user32.dll")]
        public static extern int EnumDisplaySettings(
          string deviceName, int modeNum, ref DEVMODE devMode);
        [DllImport("user32.dll")]
        public static extern int ChangeDisplaySettings(
              ref DEVMODE devMode, int flags);

        // 控制改变屏幕分辨率的常量
        public const int ENUM_CURRENT_SETTINGS = -1;
        public const int CDS_UPDATEREGISTRY = 0x01;
        public const int CDS_TEST = 0x02;
        public const int DISP_CHANGE_SUCCESSFUL = 0;
        public const int DISP_CHANGE_RESTART = 1;
        public const int DISP_CHANGE_FAILED = -1;

        // 控制改变方向的常量定义
        public const int DMDO_DEFAULT = 0;
        public const int DMDO_90 = 1;
        public const int DMDO_180 = 2;
        public const int DMDO_270 = 3;
    }
    // 改变分辨率
   
    class ChangeDisplayWrapper
    {
        public void ChangeResolution(int width, int height)
        {
            // 初始化 DEVMODE结构
            DEVMODE devmode = new DEVMODE();
            devmode.dmDeviceName = new String(new char[32]);
            devmode.dmFormName = new String(new char[32]);
            devmode.dmSize = (short)Marshal.SizeOf(devmode);

            if (0 != NativeMethods.EnumDisplaySettings(null, NativeMethods.ENUM_CURRENT_SETTINGS, ref devmode))
            {
                devmode.dmPelsWidth = width;
                devmode.dmPelsHeight = height;

                // 改变屏幕分辨率
                int iRet = NativeMethods.ChangeDisplaySettings(ref devmode, NativeMethods.CDS_TEST);

                if (iRet == NativeMethods.DISP_CHANGE_FAILED)
                {
                    MessageBox.Show("不能执行你的请求", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    iRet = NativeMethods.ChangeDisplaySettings(ref devmode, NativeMethods.CDS_UPDATEREGISTRY);

                    switch (iRet)
                    {
                        // 成功改变
                        case NativeMethods.DISP_CHANGE_SUCCESSFUL:
                            {
                                break;
                            }
                        case NativeMethods.DISP_CHANGE_RESTART:
                            {
                                MessageBox.Show("你需要重新启动电脑设置才能生效", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                            
                        default:
                            {
                                MessageBox.Show("改变屏幕分辨率失败:"+iRet.ToString(), "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                    }
                }
            }
        }
    }
}
