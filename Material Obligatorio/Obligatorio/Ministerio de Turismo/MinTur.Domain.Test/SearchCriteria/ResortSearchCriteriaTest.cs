using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Domain.BusinessEntities;
using MinTur.Domain.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinTur.Domain.Test.SearchCriteria
{
    [TestClass]
    public class ResortSearchCriteriaTest
    {
        [TestMethod]
        public void SearchCriteriaAcceptsByTouristPointIdWithoutAvailability()
        {
            ResortSearchCriteria searchCriteria = new ResortSearchCriteria()
            {
                TouristPointId = 3
            };
            Resort resort = new Resort()
            {
                TouristPointId = 3,
                Available = true
            };

            Assert.IsTrue(searchCriteria.MatchesCriteria(resort));
        }

        [TestMethod]
        public void SearchCriteriaAcceptsByAvailabilityWithoutTouristPointId()
        {
            ResortSearchCriteria searchCriteria = new ResortSearchCriteria()
            {
                Available = false
            };
            Resort resort = new Resort()
            {
                TouristPointId = 3,
                Available = false
            };

            Assert.IsTrue(searchCriteria.MatchesCriteria(resort));
        }

        [TestMethod]
        public void SearchCriteriaAcceptsByAvailabilityAndTouristPointId()
        {
            ResortSearchCriteria searchCriteria = new ResortSearchCriteria()
            {
                Available = false,
                TouristPointId = 3
            };
            Resort resort = new Resort()
            {
                TouristPointId = 3,
                Available = false
            };

            Assert.IsTrue(searchCriteria.MatchesCriteria(resort));
        }

        [TestMethod]
        public void EmptySearchCriteriaAcceptsAnyResort()
        {
            ResortSearchCriteria searchCriteria = new ResortSearchCriteria()
            {
            };
            Resort resort = new Resort()
            {
                TouristPointId = 98,
                Available = true
            };

            Assert.IsTrue(searchCriteria.MatchesCriteria(resort));
        }

        [TestMethod]
        public void SearchCriteriaDeniesBecauseItMatchesAvailabilityButNotTouristPointId()
        {
            ResortSearchCriteria searchCriteria = new ResortSearchCriteria()
            {
                Available = false,
                TouristPointId = 87
            };
            Resort resort = new Resort()
            {
                TouristPointId = 3,
                Available = false
            };

            Assert.IsFalse(searchCriteria.MatchesCriteria(resort));
        }

        [TestMethod]
        public void SearchCriteriaDeniesBecauseItMatchesTouristPointIdButNotAvailability()
        {
            ResortSearchCriteria searchCriteria = new ResortSearchCriteria()
            {
                Available = true,
                TouristPointId = 3
            };
            Resort resort = new Resort()
            {
                TouristPointId = 3,
                Available = false
            };

            Assert.IsFalse(searchCriteria.MatchesCriteria(resort));
        }

        [TestMethod]
        public void SearchCriteriaDeniesBecauseItDoesntMatchAny()
        {
            ResortSearchCriteria searchCriteria = new ResortSearchCriteria()
            {
                Available = false,
                TouristPointId = -7
            };
            Resort resort = new Resort()
            {
                TouristPointId = 3,
                Available = false
            };

            Assert.IsFalse(searchCriteria.MatchesCriteria(resort));
        }

    }
}
