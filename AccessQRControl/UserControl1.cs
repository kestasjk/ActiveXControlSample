using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Reflection;

namespace AccessQRControl
{
    [ProgId("AccessQRControl.UserControl1")]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }
            #region Sys: Register As COM ActiveX 
            //--------------------< region: Sys:Register COM ActiveX >----------------- 
            // register COM ActiveX object 
            [ComRegisterFunction()]
            public static void RegisterClass(string key)
            {
                StringBuilder skey = new StringBuilder(key);
                skey.Replace(@"HKEY_CLASSES_ROOT\", "");

                RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(skey.ToString(), true);
                RegistryKey ctrl = regKey.CreateSubKey("Control");
                ctrl.Close();
                RegistryKey inprocServer32 = regKey.OpenSubKey("InprocServer32", true);
                inprocServer32.SetValue("CodeBase", Assembly.GetExecutingAssembly().CodeBase);
                inprocServer32.Close();
                regKey.Close();
            }


            // Unregister COM ActiveX object 
            [ComUnregisterFunction()]
            public static void UnregisterClass(string key)
            {
                StringBuilder skey = new StringBuilder(key);
                skey.Replace(@"HKEY_CLASSES_ROOT\", "");
                RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(skey.ToString(), true);
                regKey.DeleteSubKey("Control", false);
                RegistryKey inprocServer32 = regKey.OpenSubKey("InprocServer32", true);
                regKey.DeleteSubKey("CodeBase", false);
                regKey.Close();
            }
        //--------------------< region: /Sys:Register COM ActiveX >----------------- 
        #endregion /Sys:Register As COM ActiveX 


        private void UserControl1_Load(object sender, EventArgs e)
        {
        }

        private string _QRCodeText = "";
        public string QRCodeText
        {
            get
            {
                return _QRCodeText;
            }
            set
            {
                _QRCodeText = value;
            }
        }
        private int _UnitSize = 5;
        public int UnitSize
        {
            get
            {
                return _UnitSize;
            }
            set
            {
                _UnitSize = value;
            }
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
        }

        private void UserControl1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var bmpByte = QRCoder.BitmapByteQRCodeHelper.GetQRCode(_QRCodeText, QRCoder.QRCodeGenerator.ECCLevel.H, _UnitSize);
            var ms = new System.IO.MemoryStream(bmpByte);
            var im = new System.Drawing.Bitmap(ms);
            e.Graphics.DrawImage(im, new Point(0, 0));
        }
    }
}
