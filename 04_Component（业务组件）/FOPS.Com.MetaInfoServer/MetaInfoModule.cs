using FS.Data;
using FS.Modules;

namespace FOPS.Com.MetaInfoServer
{
    /// <summary>
    ///     元信息模块
    /// </summary>
    [DependsOn(typeof(DataModule))]
    public class MetaInfoModule : FarseerModule
    {
        public override void PreInitialize()
        {
        }

        public override void PostInitialize()
        {
            IocManager.Container.Install(new MetaInfoInstaller(IocManager));
        }
    }
}