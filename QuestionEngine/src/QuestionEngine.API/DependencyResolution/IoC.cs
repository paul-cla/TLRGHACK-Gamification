// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IoC.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


using System;
using System.Web;
using Infrastructure.DataAccess.Dapper;
using Infrastructure.Logging.LogToUdp;
using Infrastructure.Logging.Shared;
using Keywords.API.Errors;
using Keywords.DataAccess;
using Keywords.Domain;
using Keywords.Services;
using StructureMap;
using log4net;

namespace Keywords.API.DependencyResolution
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            log4net.Config.XmlConfigurator.Configure();
            var logStashConfig = LogStashConfig.GetConfig();

            ObjectFactory.Initialize(x =>
                        {

                            x.For<ILog>().Use(LogManager.GetLogger(typeof(WebApiApplication)));

                            x.For<IMessageLogger>().Use<LogToUdp>().Ctor<LogToUdpConfiguration>().Is(new LogToUdpConfiguration
                            {
                                Enabled = logStashConfig.Enabled,
                                IpAddress = logStashConfig.IPAdress,
                                Port = logStashConfig.Port,
                                Source = Environment.MachineName,
                                Type = "keyword_api_error",
                            });

                            x.For<ICastUdpMessages>().Use<UdpErrorCaster>();

                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
                                        scan.AssemblyContainingType<IGetKeyword>();
                                        scan.AssemblyContainingType<IKeywordRepository>();
                                        scan.AssemblyContainingType<KeywordRepository>();
                                    });
                            x.For<IDatabase>().Use<LateRoomsDatabase>();
                            x.For<HttpContextBase>().Use(() => new HttpContextWrapper(HttpContext.Current));
                        });

            return ObjectFactory.Container;
        }
    }
}