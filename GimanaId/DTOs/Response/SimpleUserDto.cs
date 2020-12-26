using System;

namespace GimanaIdApi.DTOs.Response
{
    public class SimpleUserDto
    {
        public virtual Guid Id { get; set; }
        public virtual string Username { get; set; }
        public virtual ImageDto ProfilePicture { get; set; }
    }
}
