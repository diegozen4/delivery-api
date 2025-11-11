using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double? CurrentLongitude { get; set; }

        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
        public virtual ICollection<Order> OrdersAsClient { get; set; } = new List<Order>();
        public virtual ICollection<Order> OrdersAsDelivery { get; set; } = new List<Order>();
        public virtual ICollection<DeliveryOffer> DeliveryOffers { get; set; } = new List<DeliveryOffer>();
    }
}