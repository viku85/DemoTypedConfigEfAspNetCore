using Microsoft.EntityFrameworkCore;

namespace DemoTypedConfigEfAspNetCore.DataLayer
{
    public class MyAppContext
        : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GemsContext"/> class.
        /// </summary>
        /// <param name="option">Database context options.</param>
        public MyAppContext(DbContextOptions option)
            : base(option)
        {
        }

        /// <summary>
        /// Gets or sets the configurations.
        /// </summary>
        /// <value>
        /// The configurations.
        /// </value>
        public DbSet<ConfigurationValue> Configurations { get; set; }
    }
}