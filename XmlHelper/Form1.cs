using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace XmlHelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Request q = new Request();
            q.ReqType = "connection.getStatus";
            q.NameID = "8489237";
            Telegram telegram = new Telegram();
            telegram.Requestins = q;
            var res = Object2Bytes(telegram);
        }

        public byte[] Object2Bytes(object obj)
        {
            byte[] buff;
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                buff = ms.GetBuffer();
            }
            return buff;
        }
    }
    [Serializable]
    public class Request
    {
        [XmlAttribute("requestId")]
        public string NameID
        {
            get;
            set;
        }

        [XmlAttribute("requestType")]
        public string ReqType
        {
            get;
            set;
        }

    }

    //[XmlRootAttribute("Telegram")]
   [XmlRoot("Telegram", Namespace = "LancePlatform", IsNullable = false)][Serializable]
    public class Telegram
    {
        //    [XmlAttribute("Request")]
        //    public Request Name
        //    {
        //        get;
        //        set;
        //    }

        [XmlElement("Request")]
        public Request Requestins
        {
            get;
            set;
        }
    }
}
