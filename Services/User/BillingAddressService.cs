using aspCart.Core.Domain.User;
using aspCart.Infrastructure.EFModels;
using aspCart.Infrastructure.EFRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aspCart.Infrastructure.Services.User
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
        void DeleteBillingAddresses(List<string> emails);
    }
    public class BillingAddressService : IBillingAddressService
    {
        #region Fields

        private readonly ApplicationDbContext _context;
        private readonly IRepository<BillingAddress> _billingAddressRepository;

        #endregion

        #region Constructor

        public BillingAddressService(
            ApplicationDbContext context,
            IRepository<BillingAddress> billingAddressRepository)
        {
            _context = context;
            _billingAddressRepository = billingAddressRepository;
        }

        public void DeleteBillingAddresses(List<string> emails)
        {
            _context.BillingAddresses.RemoveRange(_context.BillingAddresses.Where(b=>emails.Any( e=> e == b.Email.ToLowerInvariant())));
            _context.SaveChanges();
        }

        #endregion

        #region Methods


        public BillingAddress GetBillingAddressByEmail(string email)
        {
            return _context.BillingAddresses.Where(b => b.Email == email).FirstOrDefault();
        }

        /// <summary>
        /// Get billing address by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Billing address entity</returns>
        public BillingAddress GetBillingAddressById(Guid id)
        {
            return _billingAddressRepository.FindByExpression(x => x.Id == id);
        }

        public IQueryable<BillingAddress> GetBillingAddresses()
        {
            return _billingAddressRepository.GetAll().Where(B=>_context.Users.Any(u=>u.BillingAddressId== B.Id));
        }

        public IQueryable<ApplicationUser> GetUsers()
        {
            return _context.Users;
        }

        /// <summary>
        /// Insert billing address
        /// </summary>
        /// <param name="billingAddress">Billing address entity</param>
        public void InsertBillingAddress(BillingAddress billingAddress)
        {
            if (billingAddress == null)
                throw new ArgumentNullException("billingAddress");

            _billingAddressRepository.Insert(billingAddress);
            _billingAddressRepository.SaveChanges();
        }

        /// <summary>
        /// Update billing address
        /// </summary>
        /// <param name="billingAddress">Billing address entity</param>
        public void UpdateBillingAddress(BillingAddress billingAddress)
        {
            if (billingAddress == null)
                throw new ArgumentNullException("billingAddress");

            _billingAddressRepository.Update(billingAddress);
            _billingAddressRepository.SaveChanges();
        }

        #endregion
    }
}
