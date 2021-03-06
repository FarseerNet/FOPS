using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using FOPS.Abstract.Fss.Entity;
using FOPS.Abstract.Fss.Enum;
using FS.Core;
using FS.Core.Net;
using FS.DI;
using Microsoft.Extensions.Logging;

namespace FOPS.Abstract.Fss.Server
{
    public interface IFssApi: ISingletonDependency
    {
        /// <summary>
        /// 取出全局客户端列表
        /// </summary>
        Task<List<ClientVO>> GetClientListAsync(ILocalStorageService localStorageService);
        /// <summary>
        /// 取出全局客户端数量
        /// </summary>
        Task<long> GetClientCountAsync(ILocalStorageService localStorageService);
        /// <summary>
        /// 添加任务组信息
        /// </summary>
        Task<ApiResponseJson<int>> AddTaskGroupAsync(ILocalStorageService localStorageService, TaskGroupVO vo);
        /// <summary>
        /// 复制任务组信息
        /// </summary>
        Task<ApiResponseJson<int>> CopyTaskGroupAsync(ILocalStorageService localStorageService, int taskGroupId);
        /// <summary>
        /// 删除任务组
        /// </summary>
        Task<ApiResponseJson> DeleteTaskGroupAsync(ILocalStorageService localStorageService, int taskGroupId);
        /// <summary>
        /// 获取任务组信息
        /// </summary>
        Task<TaskGroupVO> GetTaskGroupInfoAsync(ILocalStorageService localStorageService, int taskGroupId);
        /// <summary>
        /// 获取全部任务列表
        /// </summary>
        Task SyncCacheToDbAsync(ILocalStorageService localStorageService);
        /// <summary>
        /// 获取全部任务组列表
        /// </summary>
        Task<List<TaskGroupVO>> GetTaskGroupListAsync(ILocalStorageService localStorageService);
        /// <summary>
        /// 获取任务组数量
        /// </summary>
        Task<long> GetTaskGroupCountAsync(ILocalStorageService localStorageService);
        /// <summary>
        /// 获取未执行的任务数量
        /// </summary>
        Task<int> GetTaskGroupUnRunCountAsync(ILocalStorageService localStorageService);
        /// <summary>
        /// 保存TaskGroup
        /// </summary>
        Task SaveTaskGroupAsync(ILocalStorageService localStorageService, TaskGroupVO vo);
        /// <summary>
        /// 今日执行失败数量
        /// </summary>
        Task<int> TodayTaskFailCountAsync(ILocalStorageService localStorageService);
        /// <summary>
        /// 获取全部任务列表
        /// </summary>
        Task<List<TaskVO>> GetTaskUnFinishListAsync(ILocalStorageService localStorageService, int top);
        /// <summary>
        /// 获取指定任务组的任务列表
        /// </summary>
        Task<PageList<TaskVO>> GetTaskListAsync(ILocalStorageService localStorageService, int groupId, int pageSize, int pageIndex);
        /// <summary>
        /// 获取已完成的任务列表
        /// </summary>
        Task<PageList<TaskVO>> GetTaskFinishListAsync(ILocalStorageService localStorageService, int pageSize, int pageIndex);
        /// <summary>
        /// 取消任务
        /// </summary>
        Task CancelTask(ILocalStorageService localStorageService, int taskGroupId);
        /// <summary>
        /// 获取日志
        /// </summary>
        Task<PageList<RunLogVO>> GetRunLogListAsync(ILocalStorageService localStorageService, string jobName, LogLevel? logLevel, int pageSize, int pageIndex);
        /// <summary>
        /// 获取在用的任务
        /// </summary>
        Task<PageList<TaskVO>> GetEnableTaskListAsync(ILocalStorageService localStorageService, EumTaskType? status, int pageSize, int pageIndex);
    }
}