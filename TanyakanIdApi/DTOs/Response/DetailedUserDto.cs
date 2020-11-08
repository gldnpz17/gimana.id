using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanyakanIdApi.DTOs.Response
{
    public class DetailedUserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public List<UserPrivilegeDto> Privileges { get; set; }
        public ImageDto ProfilePicture { get; set; }
        public UserEmailDto Email { get; set; }
        public bool IsBanned { get; set; } 
        public virtual DateTime BanLiftedDate { get; set; }
    }
}
