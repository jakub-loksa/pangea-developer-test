using AutoMapper;
using Database.Logic.Profiles;
using DiffChecker.Api.Profiles;

namespace DiffChecker.Api.Tests.Libraries
{
    public class AutoMapperTests
    {
        private readonly MapperConfiguration _apiMappingConfiguration;
        private readonly MapperConfiguration _databaseMappingConfiguration;

        public AutoMapperTests()
        {
            _apiMappingConfiguration = new MapperConfiguration(mc => mc.AddProfile(new ApiProfile()));
            _databaseMappingConfiguration = new MapperConfiguration(mc => mc.AddProfile(new DatabaseProfile()));
        }

        [Fact]
        public void ApiProfile_Configuration_IsValid()
        {
            _apiMappingConfiguration.AssertConfigurationIsValid();
        }

        [Fact]
        public void DatabaseProfile_Configuration_IsValid()
        {
            _databaseMappingConfiguration.AssertConfigurationIsValid();
        }
    }
}
