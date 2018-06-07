﻿using Client.Entities;
using Infrastructure.Data.Common;
using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Bll
{
    public class ClientServices : IClientServices
    {
        private readonly IRepository _repository;
        private readonly IEntityServices _entityServices;

        public ClientServices(IRepository repository, IEntityServices entityServices)
        {
            _repository = repository;
            _entityServices = entityServices;
        }

        public Task<Entities.Client> GetByIdAsync(int id, ClientTypeEnum type)
        {
            IQueryable<Entities.Client> query = _repository.GetQueryable<NaturalClient>();

            if (type == ClientTypeEnum.Legal)
            {
                query = _repository.GetQueryable<LegalClient>();
            }

            return  query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Entities.Client>> GetListAsync(string molName, string firmNamePersonalNo, int? cityId, string address, string phone, string email)
        {
            var naturalClients = _repository.GetQueryable<NaturalClient>();
            var legalClients = _repository.GetQueryable<LegalClient>();

            if (!string.IsNullOrWhiteSpace(molName))
            {
                naturalClients = naturalClients.Where(c => c.Name.Contains(molName));
                legalClients = legalClients.Where(c => c.Mol.Contains(molName));
            }

            if (!string.IsNullOrWhiteSpace(firmNamePersonalNo))
            {
                naturalClients = naturalClients.Where(c => c.PersonalNo.Contains(firmNamePersonalNo));
                legalClients = legalClients.Where(c => c.FirmName.Contains(firmNamePersonalNo));
            }

            if (cityId.HasValue && cityId.Value > 0)
            {
                naturalClients = naturalClients.Where(c => c.CityId == cityId.Value);
                legalClients = legalClients.Where(c => c.CityId == cityId.Value);
            }

            if (!string.IsNullOrWhiteSpace(address))
            {
                naturalClients = naturalClients.Where(c => c.Address.Contains(address));
                legalClients = legalClients.Where(c => c.Address.Contains(address));
            }

            if (!string.IsNullOrWhiteSpace(phone))
            {
                naturalClients = naturalClients.Where(c => c.Phone.Contains(phone));
                legalClients = legalClients.Where(c => c.Phone.Contains(phone));
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                naturalClients = naturalClients.Where(c => c.Email.Contains(email));
                legalClients = legalClients.Where(c => c.Email.Contains(email));
            }

            var result = new List<Entities.Client>();

            var legalClientIds = legalClients.Select(c => new { Id = c.Id, Type = ClientTypeEnum.Legal });
            var naturalClientIds = naturalClients.Select(c => new { Id = c.Id, Type = ClientTypeEnum.Natural });

            var clientIds = legalClientIds.Union(naturalClientIds).ToList();

            result.AddRange(await _repository.GetQueryable<LegalClient>().Include(c => c.City).Where(c => clientIds.Where(lc => lc.Type == ClientTypeEnum.Legal).Select(lc => lc.Id).Contains(c.Id)).ToListAsync());
            result.AddRange(await _repository.GetQueryable<NaturalClient>().Include(c => c.City).Where(c => clientIds.Where(nc => nc.Type == ClientTypeEnum.Natural).Select(nc => nc.Id).Contains(c.Id)).ToListAsync());

            return result;
        }

        public Task<bool> EditAsync(Entities.Client client, Messages messages)
        {
            if (client is LegalClient)
            {
                return _entityServices.SaveAsync<LegalClient>(client as LegalClient, messages);
            }
            else if (client is NaturalClient)
            {
                return _entityServices.SaveAsync<NaturalClient>(client as NaturalClient, messages);
            }

            throw new Exception("Unknown client type");
        }
    }
}
