namespace DemoTypedConfigEfAspNetCore.AppSetting
{
    /// <summary>
    /// Entire application settings
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// Gets or sets the setting version.
        /// </summary>
        /// <value>
        /// The setting version.
        /// </value>
        public int SettingVersion { get; set; }

        /// <summary>
        /// Gets or sets the name of the application.
        /// </summary>
        /// <value>
        /// The name of the application.
        /// </value>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets the LDAP.
        /// </summary>
        /// <value>
        /// The LDAP.
        /// </value>
        public LdapSetting Ldap { get; set; }
    }
}