﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using AiOption.Infrastructure.DataAccess;
using AiOption.TestCore.FixtureSetups;

using Xunit;

namespace AiOption.Infrastructure.Integration.UseCases.CreateIqAccount
{
    public class CreateNewIqAccount : IClassFixture<DataContextSetupCleaner<AiOptionDbContext>>
    {

        private readonly DataContextSetupCleaner<AiOptionDbContext> _dataSetup;

        public CreateNewIqAccount(DataContextSetupCleaner<AiOptionDbContext> dataSetup) {
            _dataSetup = dataSetup;
            
        }

        [Fact]
        public async Task CreateNewIqAccountWithDetail_IqAccountWithDetail_ShouldCreated() {
            
            //arrange
           



            //act

            
            //assert


        }
    }
}
