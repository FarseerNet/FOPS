using System.Collections.Generic;
using System.Threading.Tasks;
using FOPS.Abstract.Fss.Entity;
using FOPS.Infrastructure.Repository;
using FS.Cache;
using FS.DI;
using FS.Extends;

namespace FOPS.Com.FssServer.TaskGroup.Dal
{
    /// <summary>
    /// 任务组缓存
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public class TaskGroupCache : ISingletonDependency
    {
        public TaskGroupAgent TaskGroupAgent { get; set; }
        
        /// <summary>
        /// 保存任务组信息
        /// </summary>
        public Task SaveAsync(int taskGroupId, TaskGroupVO taskGroup)
        {
            var key = CacheKeys.TaskGroupKey(EumCacheStoreType.Redis);
            return RedisContext.Instance.CacheManager.SaveItemAsync(key, taskGroup, taskGroupId);
        }

        /// <summary>
        /// 保存任务组信息
        /// </summary>
        public Task SaveAsync(List<TaskGroupVO> lstTaskGroup)
        {
            var key = CacheKeys.TaskGroupKey(EumCacheStoreType.MemoryAndRedis);
            return RedisContext.Instance.CacheManager.SaveListAsync(key, lstTaskGroup, o => o.Id);
        }

        /// <summary>
        /// 当前任务组的列表
        /// </summary>
        public Task<List<TaskGroupVO>> ToListAsync(EumCacheStoreType cacheStoreType)
        {
            var key = CacheKeys.TaskGroupKey(cacheStoreType);
            return RedisContext.Instance.CacheManager.GetListAsync(key, () => TaskGroupAgent.ToListAsync().MapAsync<TaskGroupVO, TaskGroupPO>(), o => o.Id);
        }
        
        /// <summary>
        /// 获取任务组
        /// </summary>
        public Task<TaskGroupVO> ToEntityAsync(int taskGroupId)
        {
            var key = CacheKeys.TaskGroupKey(EumCacheStoreType.Redis);
            return RedisContext.Instance.CacheManager.GetItemAsync<TaskGroupVO, int>(key, taskGroupId, () => TaskGroupAgent.ToListAsync().MapAsync<TaskGroupVO, TaskGroupPO>(), o => o.Id);
        }
    }
}