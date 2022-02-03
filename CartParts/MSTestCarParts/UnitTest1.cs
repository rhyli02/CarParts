using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CartParts.Services;
using CartParts.Models;
using CartParts.Controllers;
using FakeItEasy;
using CartParts.Services.Interfaces;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MSTestCarParts
{
    [TestClass]
    public class HomeControllerTests
    {
        private readonly ILogger<HomeController> _logger;


        [TestMethod] 
        public async Task Test_Get_Profile_By_Id_Correct_Value()
        {
            int id = 22;

            Profile fakeProfile = new Profile
            {
                id = 22,
                name = "BeerNa Bear",
                username = "Boggart",
                email = "Boggart@hotmail.com",
                address = new Address { street = "kanto", city = "qc", geo = new Geo { lat = (decimal)-33.33, lng = (decimal)23.32 }, suite = "Apt.123", zipcode = "1227" },
                phone = "123-2233-22",
                website = "boggart.com",
                company = new Company { bs = "test bs", catchPhrase = "test catch", name = "AlphaDakmaNokma" }

            };

            var profileService = A.Fake<IProfileServices>();
            A.CallTo(() => profileService.GetProfileById(id)).Returns(Task.FromResult(fakeProfile));
            var controller = new HomeController(_logger, profileService);

            var actionResult = await controller.DetailsAsync(id);
            var res = actionResult as ViewResult;
            var result = res.Model as Profile;
            Assert.AreEqual(fakeProfile, result);

        }

        [TestMethod]
        public async Task Test_Get_Profile_All_Correct_Value()
        {
            int count = 30;

            var fakeresponse = A.CollectionOfDummy<Profile>(count).AsEnumerable();
            var profileService = A.Fake<IProfileServices>();
            A.CallTo(() => profileService.GetAllProfiles()).Returns(Task.FromResult(fakeresponse));
            var controller = new HomeController(_logger, profileService);

            var actionResult = await controller.IndexAsync();
            var res = actionResult as ViewResult;
            var result = res.Model as IEnumerable<Profile>;

            Assert.AreEqual(count, result.Count());
            

        }

    }
}
