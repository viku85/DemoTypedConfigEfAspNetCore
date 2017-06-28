using DemoTypedConfigEfAspNetCore.AppSetting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DemoTypedConfigEfAspNetCore.AppConfigProvider
{
    /// <summary>
    /// EF configuration helper.
    /// </summary>
    public static class ExtensionEfConfig
    {
        /// <summary>
        /// Adds the entity framework configuration.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="setup">The setup.</param>
        /// <param name="environmentName">Name of the environment.</param>
        /// <returns><see cref="IConfigurationBuilder"/> after creation of EF config provider.</returns>
        public static IConfigurationBuilder AddEntityFrameworkConfig(
                   this IConfigurationBuilder builder, Action<DbContextOptionsBuilder> setup, string environmentName)
        {
            return builder.Add(new EFConfigSource(setup, environmentName));
        }

        /// <summary>
        /// Adds the configurations.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration root.</param>
        public static void AddConfigurations(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.Configure<Setting>(configuration);
            services.Configure<LdapSetting>(configuration.GetSection(nameof(Setting.Ldap)));
        }
    }
}