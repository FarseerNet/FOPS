using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FS.DI;

namespace FOPS.Com.MetaInfoServer
{
    public class MetaInfoInstaller : IWindsorInstaller
    {
        /// <summary>
        ///     依赖获取接口
        /// </summary>
        private readonly IIocResolver _iocResolver;

        /// <summary>
        ///     构造函数
        /// </summary>
        public MetaInfoInstaller(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        /// <summary>
        ///     注册依赖
        /// </summary>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            IocManager.Instance.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}