using MinTur.Domain.DiscountPolicies.GuestGroups;
using MinTur.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace MinTur.Domain.BusinessEntities
{
    public class GuestGroup
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Amount { get; set; }
        private GuestType _guestType;
        [Required]
        public string GuestGroupType 
        {
            get
            {
                return Regex.Replace(_guestType.ToString(), "([a-z])([A-Z])", "$1 $2");
            }
            set
            {
                if (value == null || !Enum.TryParse(value.Replace(" ", string.Empty), out GuestType guestType))
                    _guestType = GuestType.Invalid;
                else
                    _guestType = guestType;
            }
        }

        public virtual void ValidOrFail()
        {
            ValidateGuestType();
            ValidateAmmount();
        }

        private void ValidateAmmount()
        {
            if (Amount < 0)
                throw new InvalidRequestDataException("Guests amount must be a positive integer");
        }

        private void ValidateGuestType()
        {
            if(_guestType == GuestType.Invalid)
                throw new InvalidRequestDataException("Guests must be either Adult, Kid, Baby or Retired");
        }

        public List<IGuestGroupDiscountPolicy> GetApplicableDiscountPolicies()
        {
            return LoadAllDiscountPolicies().Where(dp => dp.PolicyAppliesToGuestGroup(_guestType)).ToList();
        }

        private List<IGuestGroupDiscountPolicy> LoadAllDiscountPolicies()
        {
            List<IGuestGroupDiscountPolicy> allDiscountPolicies = new List<IGuestGroupDiscountPolicy>();

            IEnumerable<Type> discountPolicyTypes = GetAllDiscountPolicyTypes();

            foreach (Type discountPolicyType in discountPolicyTypes)
            {
                IGuestGroupDiscountPolicy discountPolicy = (IGuestGroupDiscountPolicy)Activator.CreateInstance(discountPolicyType);
                allDiscountPolicies.Add(discountPolicy);
            }

            return allDiscountPolicies;
        }

        private IEnumerable<Type> GetAllDiscountPolicyTypes()
        {
            return typeof(IGuestGroupDiscountPolicy).Assembly.GetTypes()
               .Where(t => typeof(IGuestGroupDiscountPolicy).IsAssignableFrom(t) && !t.IsInterface);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var category = obj as Category;
            return Id == category.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
