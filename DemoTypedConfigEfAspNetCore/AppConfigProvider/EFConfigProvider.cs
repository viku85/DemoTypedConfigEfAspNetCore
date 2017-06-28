using DemoTypedConfigEfAspNetCore.AppSetting;
using DemoTypedConfigEfAspNetCore.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoTypedConfigEfAspNetCore.AppConfigProvider
{
    /// <summary>
    /// EF configuration provider for the application.
    /// </summary>
    /// <seealso cref="ConfigurationProvider" />
    public class EFConfigProvider
        : ConfigurationProvider
    {
        /// <summary>
        /// Gets or sets the name of the hosting environment.
        /// </summary>
        /// <value>
        /// The name of the hosting environment.
        /// </value>
        private readonly string EnvironmentName;

        /// <summary>
        /// The default values for settings
        /// </summary>
        private Lazy<Dictionary<string, string>> DefaultValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="EFConfigProvider"/> class.
        /// </summary>
        /// <param name="dbContextBuilderAction">The database context builder action.</param>
        /// <param name="environmentName">Name of the hosting environment.</param>
        public EFConfigProvider(Action<DbContextOptionsBuilder> dbContextBuilderAction, string environmentName)
        {
            DbContextBuilderAction = dbContextBuilderAction;
            EnvironmentName = environmentName;
            InitDefaultValues(environmentName);
        }

        /// <summary>
        /// Gets the database context builder action.
        /// </summary>
        /// <value>
        /// The database context builder action.
        /// </value>
        private Action<DbContextOptionsBuilder> DbContextBuilderAction { get; }

        /// <summary>
        /// Loads (or reloads) the data for this provider.
        /// </summary>
        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<MyAppContext>();
            DbContextBuilderAction(builder);

            using (var dbContext = new MyAppContext(builder.Options))
            {
                dbContext.Database.EnsureCreated();
                Data = !dbContext.Configurations.Any()
                    ? CreateAndSaveDefaultValues(dbContext)
                    : EnsureAllSync(dbContext.Configurations.ToDictionary(c => c.Key, c => c.Value), dbContext);
            }
        }

        /// <summary>
        /// Creates the and save default values.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <returns>Data feeds for IOption.</returns>
        private IDictionary<string, string> CreateAndSaveDefaultValues(
            MyAppContext dbContext)
        {
            AddInDb(DefaultValues.Value, dbContext);
            return DefaultValues.Value;
        }

        private void AddInDb(IDictionary<string, string> configurations, MyAppContext dbContext)
        {
            dbContext.Configurations.AddRange(configurations
                .Select(kvp => new ConfigurationValue { Key = kvp.Key, Value = kvp.Value }));

            dbContext.SaveChanges();
        }

        /// <summary>
        /// Ensures all values are present.
        /// </summary>
        /// <param name="dbValues">The database values.</param>
        /// <param name="dbContext">The database context.</param>
        /// <returns>Synced dictionary.</returns>
        private IDictionary<string, string> EnsureAllSync(IDictionary<string, string> dbValues, MyAppContext dbContext)
        {
            // Find item which does not exist in DB values, for anything added later stage.
            var newItems = DefaultValues.Value.Where(item => !dbValues.Keys.Contains(item.Key))
                .ToDictionary(item => item.Key, item => item.Value);

            if (newItems.Any())
            {
                AddInDb(newItems, dbContext);
                return dbValues
                    .Concat(newItems.Select(item => new KeyValuePair<string, string>(item.Key, item.Value)))
                    .ToDictionary(item => item.Key, item => item.Value);
            }
            return dbValues;
        }

        /// <summary>
        /// Initializes the default values.
        /// </summary>
        /// <param name="environmentName">Name of the environment.</param>
        private void InitDefaultValues(string environmentName)
        {
            // Can be done based on environment name.
            DefaultValues = new Lazy<Dictionary<string, string>>(() =>
            {
                return new Dictionary<string, string>
             {
                    { nameof(Setting.SettingVersion), "1" },
                    { nameof(Setting.ApplicationName), $"App - {environmentName}" },
                    { $"{nameof(Setting.Ldap)}:{nameof(LdapSetting.DomainHost)}", "NA" },
                    { $"{nameof(Setting.Ldap)}:{nameof(LdapSetting.DomainName)}", "NA" }
             };
            });
        }
    }
}