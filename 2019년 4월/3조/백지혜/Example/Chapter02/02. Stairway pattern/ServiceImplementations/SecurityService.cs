using Domain;
using ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementations
{
	public class SecurityService : ISecurityService
	{
		private readonly IUserRepository userRepository;

		public SecurityService(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}
		public void ChangeUsersPassword()
		{

		}
	}
}
