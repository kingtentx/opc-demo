using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPCForm.Mqtt
{
    #region 反馈信号

    /// <summary>
    /// 反馈信号
    /// </summary>
    public class MqttSignal
    {
        /// <summary>
        /// 类型
        /// <para>0：异常消息</para>
        /// <para>1：日志消息</para>
        /// <para>2：接收到数据</para>
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public string Data { get; set; }
    }
    #endregion
}
