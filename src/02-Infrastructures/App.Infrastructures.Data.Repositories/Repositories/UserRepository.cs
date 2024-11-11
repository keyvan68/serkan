using App.Domain.Core.Contracts.Repository;
using App.Domain.Core.DtoModels.UserDtoModels;
using App.Domain.Core.Entities;
using App.Infrastructures.Db.SqlServer.Ef.Database;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App.Infrastructures.Data.Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
 

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Create(UserDto userDto, CancellationToken cancellationToken)
        {
            var record = new User
            {
                Name = userDto.Name,
                LastName = userDto.LastName,
                BirthDate = userDto.BirthDate,
                Gender = userDto.Gender,
                Province = userDto.Province,
                City = userDto.City,
            };
            await _dbContext.Users.AddAsync(record, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return record.Id;
        }

        public async Task Delete(int UserID, CancellationToken cancellationToken)
        {
          var Record =await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == UserID, cancellationToken);
            if (Record != null)
            {
                _dbContext.Users.Remove(Record);
            }
            else
            {
                throw new KeyNotFoundException("User with the specified ID was not found.");
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<UserDto>> GetAll(CancellationToken cancellationToken)
        {
            var Record = await _dbContext.Users.Select(x => new UserDto
            {
                Id=x.Id,
                Name = x.Name,
                LastName = x.LastName,
                BirthDate = x.BirthDate,
                Gender = x.Gender,
                Province = x.Province,
                City = x.City,


            }).ToListAsync((cancellationToken));
            return Record;
        }

        public async Task<UserDto> GetById(int UserId, CancellationToken cancellationToken)
        {
            var Record = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id ==
            UserId, cancellationToken);
            var user = new UserDto
            {
                Id=Record.Id,
                Name=Record.Name,
                LastName=Record.LastName,
                BirthDate=Record.BirthDate,
                Gender=Record.Gender,
                Province=Record.Province,
                City=Record.City,
            };
            return user;
        }

        public async Task Update(UserDto userDto, CancellationToken cancellationToken)
        {
            var Record = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id ==
            userDto.Id, cancellationToken);

            Record.Name = userDto.Name;
            Record.LastName = userDto.LastName;
            Record.BirthDate = userDto.BirthDate;
            Record.Gender = userDto.Gender;
            Record.Province = userDto.Province;
            Record.City = userDto.City;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
