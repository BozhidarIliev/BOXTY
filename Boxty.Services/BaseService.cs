using AutoMapper;
using Boxty.Data;
using Boxty.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace Boxty.Services
{
	public abstract class BaseService
	{
		//TODO: maybe move user manager in admin and user service only but leaving it for now!
		protected BaseService(UserManager<BoxtyUser> userManager,
			BoxtyDbContext context,
			IMapper mapper)
		{
			this.UserManager = userManager;

			this.Context = context;
			this.Mapper = mapper;
		}

		protected IMapper Mapper { get; }
		
		protected BoxtyDbContext Context { get; }

		protected UserManager<BoxtyUser> UserManager { get; }
	}
}
