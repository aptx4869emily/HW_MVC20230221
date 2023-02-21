using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Demo20230215.Models
{
    public class EmployeeModel
    {
        /// <summary>
        /// 註解
        /// </summary>
        [DisplayName("電腦編號")]
        public string EmpNo { get; set; }

        [DisplayName("姓名")]
        public string Name { get; set; }

        [DisplayName("分機")]
        public int Ext { get; set; }

        public class MessageInfo
         {
            /// <summary>
            /// 錯誤訊息
            /// </summary>
            public string Msg { get; set; }
            /// <summary>
            /// 結果
            /// </summary>
            public bool IsSuccess { get; set; }
        }
    }
}