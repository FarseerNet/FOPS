using System;
using System.Threading.Tasks;
using FOPS.Abstract.Fss.Entity;
using FOPS.Abstract.Fss.Enum;
using FOPS.Abstract.Fss.Server;
using FOPS.Com.FssServer.Tasks.Dal;
using FS.Cache.Redis;
using FS.Extends;

namespace FOPS.Com.FssServer.Tasks
{
    public class TaskAdd : ITaskAdd
    {
        public TaskAgent        TaskAgent       { get; set; }
        public TaskCache        TaskCache       { get; set; }
        public ITaskGroupInfo   TaskGroupInfo   { get; set; }
        public ITaskGroupUpdate TaskGroupUpdate { get; set; }

        /// <summary>
        /// 创建Task，并更新到缓存
        /// </summary>
        public async Task<TaskVO> CreateAsync(TaskGroupVO taskGroup)
        {
            var task = await TaskAgent.ToUnExecutedTaskAsync(taskGroup.Id).MapAsync<TaskVO, TaskPO>();
            if (task == null)
            {
                // 没查到时，自动创建一条对应的Task
                var po = new TaskPO
                {
                    TaskGroupId = taskGroup.Id,
                    StartAt     = taskGroup.NextAt,
                    Caption     = taskGroup.Caption,
                    JobName     = taskGroup.JobName,
                    RunSpeed    = 0,
                    ClientId    = 0,
                    ClientIp    = "",
                    Progress    = 0,
                    Status      = EumTaskType.None,
                    CreateAt    = DateTime.Now,
                    RunAt       = DateTime.Now,
                    SchedulerAt = DateTime.Now,
                };
                await TaskAgent.AddAsync(po);
                task = po.Map<TaskVO>();
            }

            await TaskCache.SaveAsync(task);
            return task;
        }

        /// <summary>
        /// 创建Task，并更新到缓存
        /// </summary>
        public async Task<TaskVO> GetOrCreateAsync(int taskGroupId)
        {
            var taskGroup = await TaskGroupInfo.ToInfoAsync(taskGroupId);
            var task      = await CreateAsync(taskGroup);
            taskGroup.TaskId = task.Id;
            await TaskGroupUpdate.UpdateAsync(taskGroup);
            return task;
        }

        /// <summary>
        /// 创建Task，并更新到缓存
        /// </summary>
        public async Task<TaskVO> GetOrCreateAsync(TaskGroupVO taskGroup)
        {
            var task = await CreateAsync(taskGroup);
            taskGroup.TaskId = task.Id;
            await TaskGroupUpdate.SaveAsync(taskGroup);
            return task;
        }
    }
}