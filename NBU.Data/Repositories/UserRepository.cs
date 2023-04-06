﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NBU.Data.Configurations;
using NBU.Data.IRepositories;
using NBU.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBU.Data.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly NBUContext nBUContext;

		public UserRepository(NBUContext nBUContext)
		{
			this.nBUContext = nBUContext;
		}


		public IQueryable<User> GetAllUsers() => this.nBUContext.Users;

		public async ValueTask<User> GetUserByIdAsync(int id) => await this.nBUContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));

		public async ValueTask<User> InsertUserAsync(User user)
		{
			EntityEntry<User> entity = await this.nBUContext.Users.AddAsync(user);
			await this.nBUContext.SaveChangesAsync();
			return entity.Entity;
		}

		public async ValueTask<User> UpdateUserAsync(int Id, User user)
		{
			EntityEntry<User> entity = this.nBUContext.Users.Update(user);
			await this.nBUContext.SaveChangesAsync();
			return entity.Entity;

		}

		public async ValueTask<bool> DeleteUserAsync(int id)
		{
			User user = await this.nBUContext.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));
			if (user == null)
			{
				return false;
			}
			this.nBUContext.Users.Remove(user);
			await this.nBUContext.SaveChangesAsync();
			return true;
		}
	}
}
