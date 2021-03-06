using System;
using Newtonsoft.Json;

namespace FOPS.Abstract.Fss.Entity
{
    /// <summary>
    /// 客户端连接信息
    /// </summary>
    [Serializable]
    public class ClientVO
    {
        /// <summary>
        /// 客户端ID
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// 客户端IP
        /// </summary>
        public string ClientIp { get; set; }
        
        /// <summary>
        /// 客户端名称
        /// </summary>
        public string ClientName { get; set; }
        
        /// <summary>
        /// 客户端能执行的任务
        /// </summary>
        public string[] Jobs { get; set; }
        
        /// <summary>
        /// 活动时间
        /// </summary>
        public DateTime ActivateAt { get; set; }

        /// <summary>
        /// JSON
        /// </summary>
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}