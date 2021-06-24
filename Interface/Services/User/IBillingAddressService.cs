using aspCart.Core.Domain.User;
using aspCart.Infrastructure.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspCart.Core.Interface.Services.User
{
    public interface IBillingAddressService
    {
        IQueryable<ApplicationUser> GetUsers();

        IQueryable<BillingAddress> GetBillingAddresses();
        /// <summary>
        /// Get billing address by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Billing address entity</returns>
        BillingAddress GetBillingAddressByEmail(string email);
        BillingAddress GetBillingAddressById(Guid id);

        /// <summary>
        /// Insert billing address
        /// </summary>
        /// <param name="billingAddress">Billing address entity</param>
        void InsertBillingAddress(BillingAddress billingAddress);

        /// <summary>
        /// Update billing address
        /// </summary>
        /// <param name="billingAddress">Billing address entity</param>
        void UpdateBillingAddress(BillingAddress billingAddress);
    }
}
