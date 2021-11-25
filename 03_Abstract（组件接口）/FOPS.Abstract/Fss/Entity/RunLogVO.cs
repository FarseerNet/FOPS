using System;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace FOPS.Abstract.Fss.Entity
{
    /// <summary>
    /// 运行日志
    /// </summary>
    public class RunLogVO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// 任务组记录ID
        /// </summary>
        public int TaskGroupId { get; set; }
        
        /// <summary>
        /// 任务组标题
        /// </summary>
        public string Caption { get; set; }
        
        /// <summary>
        /// 实现Job的特性名称（客户端识别哪个实现类）
        /// </summary>
        public string JobName { get; set; }
        
        /// <summary>
        /// 日志级别
        /// </summary>
        public LogLevel LogLevel { get; set; }
        
        /// <summary>
        /// 日志内容
        /// </summary>
        public string Content { get; set; }
        
        /// <summary>
        /// 日志时间
        /// </summary>
        public DateTime CreateAt { get; set; }
    }
}