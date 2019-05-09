using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class SecurityService : ISecurityService
	{
		private readonly IUserRepository userRepository;

		public SecurityService(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}
		public void ChageUserPassword()
		{
		}
	}
}
