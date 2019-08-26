﻿using Feign;
using Feign.Cache;
using Feign.DependencyInjection;
using Feign.Discovery;
using Feign.Logging;
using Feign.Proxy;
using Feign.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ServiceCollectionExtensions
    {

        public static IDependencyInjectionFeignBuilder AddFeignClients(this IServiceCollection services)
        {
            return AddFeignClients(services, (FeignOptions)null);
        }

        public static IDependencyInjectionFeignBuilder AddFeignClients(this IServiceCollection services, Action<IFeignOptions> setupAction)
        {
            FeignOptions options = new FeignOptions();
            setupAction?.Invoke(options);
            return AddFeignClients(services, options);
        }

        public static IDependencyInjectionFeignBuilder AddFeignClients(this IServiceCollection services, IFeignOptions options)
        {
            if (options == null)
            {
                options = new FeignOptions();
            }

            DependencyInjectionFeignBuilder feignBuilder = new DependencyInjectionFeignBuilder();
            feignBuilder.Services = services;
            feignBuilder.Options = options;
            feignBuilder.AddFeignClients(options)
                .AddLoggerFactory<LoggerFactory>()
                .AddCacheProvider<CacheProvider>()
                ;
            return feignBuilder;
        }


    }
}