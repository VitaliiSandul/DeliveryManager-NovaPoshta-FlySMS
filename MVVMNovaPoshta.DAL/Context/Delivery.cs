namespace MVVMNovaPoshta.DAL.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Delivery")]
    public partial class Delivery
    {
        public int DeliveryId { get; set; }

        [StringLength(200)]
        public string DeliveryDescription { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOper { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(15)]
        public string CustomerPhone { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; }

        public int? NumStorage { get; set; }

        public double? WeightMax { get; set; }

        [StringLength(14)]
        public string TTN { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateArrival { get; set; }
    }
}
