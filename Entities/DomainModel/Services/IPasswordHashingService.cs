using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Services
{
    public interface IPasswordHashingService
    {
        string HashPassword(string password, string salt);
    }
}
