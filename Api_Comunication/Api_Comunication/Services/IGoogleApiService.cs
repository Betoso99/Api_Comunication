using Api_Comunication.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api_Comunication.Services
{
	interface IGoogleApiService
	{
		Task<Root> GetDistance(string originPlace, string destinationPlace);
	}
}
