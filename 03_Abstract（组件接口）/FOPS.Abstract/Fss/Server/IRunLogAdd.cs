using System.Threading.Tasks;
using FS.DI;
using Microsoft.Extensions.Logging;

namespace FOPS.Abstract.Fss.Server
{
    public interface IRunLogAdd : ITransientDependency
    {
        /// <summary>
        /// 添加日志记录
        /// </summary>
        Task AddAsync(int taskGroupId, int taskId, LogLevel logLevel, string content);
    }
}