﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AiOption.Application.Repositories;
using AiOption.Application.Repositories.ReadOnly;
using AiOption.Application.Repositories.WriteOnly;
using AiOption.Domain.IqOptionAccount;

using AutoMapper;

using Dapper;

namespace AiOption.Infrastructure.DataAccess.Repositories
{
    public class IqAccountRepository : IIqOptionAccountReadOnlyRepository, IIqOptionWriteOnlyRepository {

        private readonly IDbConnection _connection;
        private readonly IMapper _mapper;

        public IqAccountRepository(IDbConnection connection, IMapper mapper) {
            _connection = connection;
            _mapper = mapper;
        }


        #region WriteAccess


        public async Task<bool> UpdateSecuredToken(int userId, string securedToken) {

            var sql = $"UPDATE iQOptionAccount SET ssid = @ssid, ssidUpdated = Getdate() WHERE IqUserId = @userId ";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("@ssid", securedToken);
            dynamicParams.Add("@userId", userId);

            var rows = await _connection.ExecuteAsync(sql, dynamicParams);

            return rows == 1;
        }


        #endregion

        #region ReadAccess

        public async Task<IEnumerable<IqOptionAccount>> GetAllTask() {

            var sql = "SELECT * FROM IqOptionAccount";
            var result = await _connection.QueryAsync<IqAccountDto>(sql);

            return _mapper.Map<IEnumerable<IqOptionAccount>>(result);
        }

        public async Task<IqOptionAccount> GetByUserIdTask(int userId)
        {
            var sql = "SELECT * FROM IqOptionAccount WHERE IqOptionUserId = @userId";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("@userId", userId);
            var result = await _connection.QueryAsync<IqAccountDto>(sql, dynamicParams);

            var iqAccountDtos = result as IqAccountDto[] ?? result.ToArray();
            if (iqAccountDtos.Any()) {
                return _mapper.Map<IqAccountDto, IqOptionAccount>(iqAccountDtos.FirstOrDefault());
            }

            return null;
        }

        public async Task<IqOptionAccount> GetByUserNameTask(string userName)
        {
            var sql = "SELECT * FROM IqOptionAccount WHERE IqOptionUserName = @IqUserName";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("@IqUserName", userName);
            var result = await _connection.QueryAsync<IqAccountDto>(sql, dynamicParams);

            var iqAccountDtos = result as IqAccountDto[] ?? result.ToArray();
            if (iqAccountDtos.Any())
            {
                return _mapper.Map<IqAccountDto, IqOptionAccount>(iqAccountDtos.FirstOrDefault());
            }

            return null;
        }

        #endregion


    }
}