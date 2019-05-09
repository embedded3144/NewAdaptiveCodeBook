using ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
	public class AccountController
	{
		private readonly ISecurityService securityService;
		public AccountController(ISecurityService securityService)
		{
			this.securityService = securityService;
		}
	}
}
