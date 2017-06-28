namespace DemoTypedConfigEfAspNetCore.AppSetting
{
    /// <summary>
    /// LDAP settings
    /// </summary>
    public class LdapSetting
    {
        /// <summary>
        /// Gets or sets the domain host.
        /// </summary>
        /// <value>
        /// The domain host.
        /// </value>
        public string DomainHost { get; set; }

        /// <summary>
        /// Gets or sets the name of the domain.
        /// </summary>
        /// <value>
        /// The name of the domain.
        /// </value>
        public string DomainName { get; set; }
    }
}