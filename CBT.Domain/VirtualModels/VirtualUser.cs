using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBT.Domain.Models.Auth;

namespace CBT.Domain.Models;

public class VirtualUser
{
    public Guid Id { get; set; }
    public string Name { get; set; } 
    public string? Username { get; set; }
    public Role Role { get; set; }
    public VirtualUser(User user)
    {
        this.Id = user.Id;
        this.Name = user.Name;
        this.Role = user.Role;
    }
    public VirtualUser(Guid id, string Name, string Username = null)
    {
        this.Id=id;
        this.Name = Name;
        this.Username = Username;
    }
    public static implicit operator VirtualUser(User user) => new(user);
}
