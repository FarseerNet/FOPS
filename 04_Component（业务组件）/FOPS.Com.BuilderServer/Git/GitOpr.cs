using System;
using System.Threading.Tasks;
using FOPS.Abstract.Builder.Entity;
using FOPS.Abstract.Builder.Server;
using FOPS.Abstract.MetaInfo.Entity;
using FOPS.Abstract.MetaInfo.Server;
using FS.Core.Entity;
using FS.Utils.Component;

namespace FOPS.Com.BuilderServer.Git
{
    public class GitOpr : IGitOpr
    {
        public IGitService      GitService      { get; set; }
        public IBuildLogService BuildLogService { get; set; }
        const  string           SavePath = "/var/lib/fops/git/";

        /// <summary>
        /// 消除仓库
        /// </summary>
        public async Task<RunShellResult> ClearAsync(int gitId)
        {
            var info = await GitService.ToInfoAsync(gitId);

            // 获取Git存放的路径
            var gitPath = GetGitPath(info);
            return await ShellTools.Run("rm", $"-rf {gitPath}", null);
        }

        /// <summary>
        /// 拉取最新代码
        /// </summary>
        public async Task<RunShellResult> PullAsync(GitVO git, Action<string> actReceiveOutput)
        {
            RunShellResult runShellResult;

            // 如果Git存放的目录不存在，则创建
            if (!System.IO.Directory.Exists(SavePath)) System.IO.Directory.CreateDirectory(SavePath);

            // 获取Git存放的路径
            var gitPath = GetGitPath(git);

            // 判断git是否有clone过
            if (!System.IO.Directory.Exists(gitPath))
            {
                runShellResult = await Clone(git, gitPath, actReceiveOutput);
            }
            else
            {
                runShellResult = await ShellTools.Run("git", $"-C {gitPath} pull --rebase", actReceiveOutput);
            }

            return runShellResult;
        }

        /// <summary>
        /// 拉取最新代码
        /// </summary>
        public async Task<RunShellResult> PullAsync(BuildVO build, ProjectVO project, GitVO git, Action<string> actReceiveOutput)
        {
            BuildLogService.Write(build.Id, "---------------------------------------------------------");
            BuildLogService.Write(build.Id, $"开始拉取git {git.Name} 分支：{git.Branch} 仓库：{git.Hub}。");

            var result = await PullAsync(git, actReceiveOutput);

            // 更新git拉取时间
            await GitService.UpdateAsync(git.Id, DateTime.Now);
            switch (result.IsError)
            {
                case false:
                    BuildLogService.Write(build.Id, $"拉取完成。");
                    break;
                case true:
                    BuildLogService.Write(build.Id, $"拉取出错了。");
                    break;
            }

            return result;
        }

        /// <summary>
        /// Clone代码
        /// </summary>
        private Task<RunShellResult> Clone(GitVO info, string path, Action<string> actReceiveOutput)
        {
            var url = info.Hub;
            // 需要密码
            if (!string.IsNullOrWhiteSpace(info.UserPwd))
            {
                url = url.Replace("//", $"//{info.UserName.Replace("@", "%40")}:{info.UserPwd}@");
            }

            return ShellTools.Run("git", $"clone -b {info.Branch} {url} {path}", actReceiveOutput);
        }

        /// <summary>
        /// 获取Git存放的路径
        /// </summary>
        public string GetGitPath(GitVO info)
        {
            var gitName                           = info.Hub.Substring(info.Hub.LastIndexOf('/') + 1);
            if (gitName.EndsWith(".git")) gitName = gitName.Substring(0, gitName.Length - 4);
            return SavePath + gitName + "/";
        }
    }
}