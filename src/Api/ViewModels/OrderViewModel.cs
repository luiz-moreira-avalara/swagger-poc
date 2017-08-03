using System;

namespace Swagger.PoC.ViewModels
{

    /// <summary>
    /// 
    /// </summary>
    public class OrderViewModel
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public long? Id { get; set; }
        /// <summary>
        /// Gets or Sets PetId
        /// </summary>
        public long? PetId { get; set; }
        /// <summary>
        /// Gets or Sets Quantity
        /// </summary>
        public int? Quantity { get; set; }
        /// <summary>
        /// Gets or Sets ShipDate
        /// </summary>
        public DateTime? ShipDate { get; set; }
        /// <summary>
        /// OrderViewModel Status
        /// </summary>
        /// <value>OrderViewModel Status</value>
        public string Status { get; set; }
        /// <summary>
        /// Gets or Sets Complete
        /// </summary>
        public bool? Complete { get; set; }
    }
}
