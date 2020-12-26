using System;
using System.Collections.Generic;

namespace GimanaIdApi.DTOs.Response
{
    public class DetailedUserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public List<UserPrivilegeDto> Privileges { get; set; }
        public ImageDto ProfilePicture { get; set; }
        public UserEmailDto Email { get; set; }
        public DateTime BanLiftedDate { get; set; }
        public List<SimpleArticleDto> ContributedArticles { get; set; }
    }
}
