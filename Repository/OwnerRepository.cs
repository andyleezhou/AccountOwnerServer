﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.ExtendedModels;
using Entities.Extensions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class OwnerRepository: RepositoryBase<Owner>, IOwnerRepository
    {
        public OwnerRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public async Task CreateOwnerAsync(Owner owner)
        {
            owner.Id = Guid.NewGuid();
            Create(owner);
            await SaveAsync();
        }

        public async Task DeleteOwnerAsync(Owner owner)
        {
            Delete(owner);
            await SaveAsync();
        }

        public async Task<IEnumerable<Owner>> GetAllOwnersAsync()
        {
            return await FindAll()
                .OrderBy(ow => ow.Name)
                .ToListAsync();
        }

        public async Task <Owner> GetOwnerByIdAsync(Guid ownerId)
        {
            return await FindByCondition(owner => owner.Id.Equals(ownerId)).DefaultIfEmpty(new Owner())
                .SingleAsync();
                
        }
        public async Task <OwnerExtended> GetOwnerWithDetailsAsync(Guid ownerId)
        {

            return await FindByCondition(owner => owner.Id.Equals(ownerId))
               .Select(o => new OwnerExtended(o)
               {
                   Accounts = RepositoryContext.Accounts
                    .Where(a => a.OwnerId == ownerId)
                    .ToList()
               })
                .SingleOrDefaultAsync();
        }

        public async Task UpdateOwnerAsync(Owner dbOwner, Owner owner)
        {
            dbOwner.Map(owner);
            Update(dbOwner);
            await SaveAsync();
        }
        
    }
}
