using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using FluentValidation;
using iqoption.web.Models;

namespace iqoption.web.AutofacModules
{
    public class ApiFluentValidationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<Validations.LoginViewModelValidation>().As<IValidator<LoginViewModel>>();
            builder.RegisterType<Validations.RegisterViewModelValidation>().As<IValidator<RegisterViewModel>>();


        }
    }
}
