using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Mapping
{
    public class BlogMapping
    {
        public static Assembly[] GetMappingAssemblys()
        {
            //获取所有IProfile实现类
            List<Assembly> assemblies = new List<Assembly>();
            var allAssemblyList =
            Assembly
               .GetEntryAssembly()//获取默认程序集
               .GetReferencedAssemblies()//获取所有引用程序集
               .Select(Assembly.Load);

            allAssemblyList.ToList().ForEach(ret =>
            {
                if (ret.DefinedTypes.Any(type => type.IsAssignableFrom(typeof(Profile))))
               {
                    assemblies.Add(ret);
                }
                if (ret.ManifestModule.Name== "BlogModel.dll")
                {
                }
            });
            return assemblies.ToArray();
        }
    }
}
