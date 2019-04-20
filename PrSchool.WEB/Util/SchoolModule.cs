using Ninject.Modules;
using PrSchool.BLL.Interfaces;
using PrSchool.BLL.Services;
using PrSchool.WEB.Infrastructure.Abstract;
using PrSchool.WEB.Infrastructure.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrSchool.WEB.Util
{
    public class SchoolModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISchoolService>().To<SchoolServices>();
            Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}