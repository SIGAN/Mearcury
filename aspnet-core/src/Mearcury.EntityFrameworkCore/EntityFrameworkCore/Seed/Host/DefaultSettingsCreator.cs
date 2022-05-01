﻿using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Configuration;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Net.Mail;

namespace Mearcury.EntityFrameworkCore.Seed.Host
{
    public class DefaultSettingsCreator
    {
        private readonly MearcuryDbContext _context;

        public DefaultSettingsCreator(MearcuryDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            int? tenantId = null;

#pragma warning disable CS0162 // Unreachable code detected
            if (!MearcuryConsts.MultiTenancyEnabled)
            {
                tenantId = MultiTenancyConsts.DefaultTenantId;
            }
#pragma warning restore CS0162 // Unreachable code detected

            // Emailing
            AddSettingIfNotExists(EmailSettingNames.DefaultFromAddress, "admin@mydomain.com", tenantId);
            AddSettingIfNotExists(EmailSettingNames.DefaultFromDisplayName, "mydomain.com mailer", tenantId);

            // Languages
            AddSettingIfNotExists(LocalizationSettingNames.DefaultLanguage, "en", tenantId);
        }

        private void AddSettingIfNotExists(string name, string value, int? tenantId = null)
        {
            if (_context.Settings.IgnoreQueryFilters().Any(s => s.Name == name && s.TenantId == tenantId && s.UserId == null))
            {
                return;
            }

            _context.Settings.Add(new Setting(tenantId, null, name, value));
            _context.SaveChanges();
        }
    }
}
