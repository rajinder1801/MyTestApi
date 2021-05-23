using System;

namespace MyApiTest.Models
{
    /// <summary>
    /// User Model
    /// </summary>
    public class User
    {
        /// <summary>
        /// The name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The unique token of the user.
        /// </summary>
        public Guid Token { get; set; }
    }
}
