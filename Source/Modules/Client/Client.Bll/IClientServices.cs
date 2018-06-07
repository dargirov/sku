﻿using Client.Entities;
using Infrastructure.Data.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Bll
{
    public interface IClientServices
    {
        Task<Entities.Client> GetByIdAsync(int id, ClientTypeEnum type);
        Task<IEnumerable<Entities.Client>> GetListAsync(string molName, string firmNamePersonalNo, int? cityId, string address, string phone, string email);
        Task<bool> EditAsync(Entities.Client client, Messages messages);
    }
}
