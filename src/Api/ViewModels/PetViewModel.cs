using System.Collections.Generic;

namespace Swagger.PoC.ViewModels
{

    /// <summary>
    /// 
    /// </summary>
    public class PetViewModel
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public long? Id { get; set; }
        /// <summary>
        /// Gets or Sets Category
        /// </summary>
        public CategoryViewModel Category { get; set; }
        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or Sets PhotoUrls
        /// </summary>
        public List<string> PhotoUrls { get; set; }
        /// <summary>
        /// Gets or Sets Tags
        /// </summary>
        public List<TagViewModel> Tags { get; set; }
        /// <summary>
        /// pet status in the store
        /// </summary>
        /// <value>pet status in the store</value>
        public string Status { get; set; }
    }
}
