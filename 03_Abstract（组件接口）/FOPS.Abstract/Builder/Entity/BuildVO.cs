using System;
using FOPS.Abstract.Builder.Enum;

namespace FOPS.Abstract.Builder.Entity
{
    public class BuildVO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectId { get; set; }
        
        /// <summary>
        /// 集群ID
        /// </summary>
        public int ClusterId { get; set; }
        
        /// <summary>
        /// 构建号
        /// </summary>
        public int BuildNumber { get; set; }
        
        /// <summary>
        /// 状态
        /// </summary>
        public EumBuildStatus Status { get; set; }
        
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime CreateAt { get; set; }
        
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime FinishAt { get; set; }
    }
}