﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Domain.Repositories;

namespace ZeroStack.DeviceCenter.Domain.Aggregates.PermissionAggregate
{
    public interface IPermissionGrantRepository : IRepository<PermissionGrant, Guid>
    {
        Task<PermissionGrant?> FindAsync(string name, string providerName, string providerKey, CancellationToken cancellationToken = default);

        Task<List<PermissionGrant>> GetListAsync(string providerName, string providerKey, CancellationToken cancellationToken = default);

        Task<List<PermissionGrant>> GetListAsync(string[] names, string providerName, string providerKey, CancellationToken cancellationToken = default);
    }
}
