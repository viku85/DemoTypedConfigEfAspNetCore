using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace DemoTypedConfigEfAspNetCore.AppConfigProvider
{
    /// <summary>
    /// Application Configuration source.
    /// </summary>
    /// <seealso cref="IConfigurationSource" />
    public class EFConfigSource
        : IConfigurationSource
    {
        /// <summary>
        /// The database context builder action
        /// </summary>
        private readonly Action<DbContextOptionsBuilder> DbContextBuilderAction;

        /// <summary>
        /// The hosting environment name
        /// </summary>
        private readonly string EnvironmentName;

        /// <summary>
        /// Initializes a new instance of the <see cref="EFConfigSource"/> class.
        /// </summary>
        /// <param name="optionsAction">The options action.</param>
        /// <param name="environmentName">Name of the hosting environment.</param>
        public EFConfigSource(Action<DbContextOptionsBuilder> optionsAction, string environmentName)
        {
            DbContextBuilderAction = optionsAction;
            EnvironmentName = environmentName;
        }

        /// <summary>
        /// Builds the <see cref="T:Microsoft.Extensions.Configuration.IConfigurationProvider" /> for this source.
        /// </summary>
        /// <param name="builder">The <see cref="T:Microsoft.Extensions.Configuration.IConfigurationBuilder" />.</param>
        /// <returns>
        /// An <see cref="T:Microsoft.Extensions.Configuration.IConfigurationProvider" />
        /// </returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new EFConfigProvider(DbContextBuilderAction, EnvironmentName);
        }
    }
}