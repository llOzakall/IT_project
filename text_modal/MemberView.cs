using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace text_modal
{
    public class MemberView
    {
        [DisplayName("id")]
        public int member_id { get; set; }

        [DisplayName("ชื่อ - สกุล")]
        public string fullname { get; set; }

        [DisplayName("ที่อยู่")]
        public string address { get; set; }

        [DisplayName("วันเกิด")]
        public string birthday { get; set; }

        [DisplayName("อายุ")]
        public int age { get; set; }

    }
}
