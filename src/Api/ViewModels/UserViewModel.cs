namespace Swagger.PoC.ViewModels
{

    /// <summary>
    /// 
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// Gets or Sets Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or Sets FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or Sets LastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or Sets Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or Sets Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or Sets Phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// UserViewModel Status
        /// </summary>
        /// <value>UserViewModel Status</value>
        public int? UserStatus { get; set; }
    }
}
