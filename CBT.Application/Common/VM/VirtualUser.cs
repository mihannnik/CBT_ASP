using CBT.Domain.Models;
using CBT.Domain.Models.Enums;

namespace CBT.Application.Common.VM;

public class VirtualUser
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Username { get; set; }
    public Role Role { get; set; }
    public VirtualUser(User user)
    {
        Id = user.Id;
        Name = user.Name;
        Role = user.Role;
    }
    public VirtualUser(Guid id, string name, string? username = null)
    {
        Id = id;
        this.Name = name;
        this.Username = username;
    }
    public static implicit operator VirtualUser(User user) => new(user);
}
