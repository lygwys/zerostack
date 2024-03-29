﻿using System;
using System.Linq;
using System.Threading.Tasks;
using ZeroStack.DeviceCenter.Application.Models.Permissions;
using ZeroStack.DeviceCenter.Application.PermissionProviders;
using ZeroStack.DeviceCenter.Domain.Aggregates.TenantAggregate;
using ZeroStack.DeviceCenter.Domain.Repositories;

namespace ZeroStack.DeviceCenter.Application.Services.Permissions
{
    public class PermissionDataSeedProvider : IDataSeedProvider
    {
        private readonly IPermissionApplicationService _permissionService;

        private readonly IPermissionDefinitionManager _permissionDefinitionManager;

        private readonly ICurrentTenant _currentTenant;

        public PermissionDataSeedProvider(IPermissionApplicationService permissionService, IPermissionDefinitionManager permissionDefinitionManager, ICurrentTenant currentTenant)
        {
            _permissionService = permissionService;
            _permissionDefinitionManager = permissionDefinitionManager;
            _currentTenant = currentTenant;
        }

        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var permissionNames = _permissionDefinitionManager.GetPermissions().Where(p => !p.AllowedProviders.Any() || p.AllowedProviders.Contains(RolePermissionValueProvider.ProviderName)).Select(p => p.Name).ToArray();
            var permissionModels = Array.ConvertAll(permissionNames, pn => new PermissionUpdateRequestModel { Name = pn, IsGranted = true });

            await _permissionService.UpdateAsync(RolePermissionValueProvider.ProviderName, "role1", permissionModels.Where(e=>e.Name!= ProductPermissions.Products.Create));

            using (_currentTenant.Change(Guid.Parse("f30e402b-9de2-4b48-9ff0-c073cf499103")))
            {
                await _permissionService.UpdateAsync(UserPermissionValueProvider.ProviderName, "3", permissionModels.Where(e => e.Name != ProductPermissions.Products.Delete));
            }

            using (_currentTenant.Change(Guid.Parse("F30E402B-9DE2-4B48-9FF0-C073CF499102")))
            {
                await _permissionService.UpdateAsync(UserPermissionValueProvider.ProviderName, "2", permissionModels.Where(e => e.Name != ProductPermissions.Products.Delete));
            }
        }
    }
}
