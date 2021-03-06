using System.Collections.Generic;
using System.Threading.Tasks;
using FOPS.Abstract.Builder.Entity;
using FOPS.Abstract.MetaInfo.Entity;
using FS.DI;

namespace FOPS.Abstract.Builder.Server
{
    public interface IBuildService : ISingletonDependency
    {
        /// <summary>
        /// 创建构建任务
        /// </summary>
        Task<int> Add(int projectId, int clusterId);

        /// <summary>
        /// 主动取消任务
        /// </summary>
        Task Cancel(int id);

        /// <summary>
        /// 设置任务成功
        /// </summary>
        Task Success(int clusterId, ProjectVO project, int buildId);

        /// <summary>
        /// 当前构建的队列数量
        /// </summary>
        Task<int> CountAsync();

        /// <summary>
        /// 获取构建队列前30
        /// </summary>
        /// <returns></returns>
        Task<List<BuildVO>> ToBuildingList(int pageSize, int pageIndex);

        /// <summary>
        /// 查看构建信息
        /// </summary>
        Task<BuildVO> ToInfoAsync(int id);
    }
}