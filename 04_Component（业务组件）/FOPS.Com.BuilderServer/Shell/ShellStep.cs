using System;
using System.Threading.Tasks;
using FOPS.Abstract.Builder.Entity;
using FOPS.Abstract.Builder.Server;
using FOPS.Abstract.MetaInfo.Entity;
using FS.Core.Entity;
using FS.Utils.Component;

namespace FOPS.Com.BuilderServer.Shell
{
    /// <summary>
    /// 构建shell脚本
    /// </summary>
    public class ShellStep: IBuildStep
    {
        public IBuildLogService BuildLogService { get; set; }
        const  string           SavePath = "/var/lib/fops/shell/";
        
        public async Task<RunShellResult> Build(BuildEnvironment env, BuildVO build, ProjectVO project, GitVO git, Action<string> actReceiveOutput)
        {
            BuildLogService.Write(build.Id, "---------------------------------------------------------");
            BuildLogService.Write(build.Id, $"开始执行Shell脚本。");
            
            // 每次执行时，需要生成shell脚本
            var path = SavePath + $"{build.Id}.sh";
            if (!System.IO.Directory.Exists(SavePath)) System.IO.Directory.CreateDirectory(SavePath);
            System.IO.File.AppendAllText(path, project.ShellScript);
            
            // 执行脚本
            var result = await ShellTools.Run("sh", path, actReceiveOutput, env);
            switch (result.IsError)
            {
                case false:
                    BuildLogService.Write(build.Id, $"执行脚本完成。");
                    break;
                case true:
                    BuildLogService.Write(build.Id, $"执行脚本出错了。");
                    break;
            }

            return result;
        }
    }
}