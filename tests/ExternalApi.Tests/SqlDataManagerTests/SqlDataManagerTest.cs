﻿using Application;
using Infrastructure.Dkron.Common.Enums.Legacy;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Infrastructure.Tests.SqlDataManagerTests;

public class SqlDataManagerTest : IClassFixture<ServiceProviderFixture>
{
    private readonly ISqlDataManager _sqlDataManager;

    public SqlDataManagerTest(ServiceProviderFixture fixture)
    {
        _sqlDataManager = fixture.Sp.GetRequiredService<ISqlDataManager>();
    }

    [Theory]
    [ClassData(typeof(SqlDataManagerTestData))]
    public async Task DynamicTest(SqlServerJobFrequencyTypes freqType, SqlServerJobSubDayFrequencyTypes subDayIntervalType, bool isValid)
    {
        const string accessKey = "access-key";
        const string userGroupId = "user-group";
        const string orgDbName = "org-db-name";
        const string orgConnString = "org-conn-string";
        const int projectId = 1;
        var scheduleJobName = $"job-name-{Guid.NewGuid()}";
        var dataImportScheduleId = Guid.NewGuid();
        var startDate = DateTime.UtcNow;
        var endDate = DateTime.UtcNow.AddDays(1);
        var activeStartTime = DateTime.UtcNow.AddSeconds(10);
        var activeEndTime = DateTime.UtcNow.AddHours(1);
        const int freqInterval = 0;
        var subDayInterval = 0;
        var freqRelativeInterval = 0;
        var freqRecurrenceFactor = 0;

        var result = await _sqlDataManager.CreateJob(accessKey, userGroupId, orgDbName, orgConnString, projectId,
            scheduleJobName, dataImportScheduleId, startDate, endDate,activeStartTime,activeEndTime, freqType, freqInterval,
            subDayIntervalType, subDayInterval, freqRelativeInterval, freqRecurrenceFactor);

        if (isValid)
        {
            Assert.NotNull(result);
            Assert.NotNull(result.Name);
        }
        else
        {
            Assert.Null(result);
        }
    }
}