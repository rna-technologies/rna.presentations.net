using System.IO;
using System.Reflection;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using rna.Authorization.Application.Tellers;
using rna.Core.Base.Infrastructure.Model.Constants;
using rna.Core.Infrastructure.Logics.Users.Verifications.ContactSignInVerification;
using rna.Core.Infrastructure.Services.MiddleWare;


RnaWebApplication.Run(args, o => o
.AddApplicationAssembly(Assembly.GetExecutingAssembly())
.AllowAnyCorsOrigin()
.AddRnaCommandHandlers()
.AddAuthentication()
.AddSms()
.AddEmail()
.AddMediatRAssemblies(typeof(VerifyUserEmailHandler), typeof(GetRegisterableTellerPage))
);
