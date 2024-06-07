using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models.Configurations;

public class UserAdminsConfiguration : IEntityTypeConfiguration<UserAdmins>
{
    public void Configure(EntityTypeBuilder<UserAdmins> builder)
    {
        builder.HasData(
            new UserAdmins
            {
                Id = 1,
                FirstName = "John",
                LastName = "White",
                Email = "John@mail.ru",
                Password = "1",
                Role = UserAdmins.UserRole.MainAdmin,
            },
            new UserAdmins
            {
                Id = 2,
                FirstName = "Ben",
                LastName = "Brown",
                Email = "Ben@mail.ru",
                Password = "2",
                Role = UserAdmins.UserRole.TechAdmin,
            },
            new UserAdmins
            {
                Id = 3,
                FirstName = "Tom",
                LastName = "Red",
                Email = "Tom@mail.ru",
                Password = "3",
                Role = UserAdmins.UserRole.TechAdmin,
            },
            new UserAdmins
            {
                Id = 4,
                FirstName = "Mick",
                LastName = "Blue",
                Email = "Mick@mail.ru",
                Password = "4",
                Role = UserAdmins.UserRole.TechAdmin,
            }
        );
    }
}