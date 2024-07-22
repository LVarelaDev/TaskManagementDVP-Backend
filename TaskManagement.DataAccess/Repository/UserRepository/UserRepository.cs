using Mapster;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Business.Enums;
using TaskManagement.Business.IRepository.User;
using TaskManagement.Business.Models.Payload;
using TaskManagement.Business.Models.Payload.Filters;
using TaskManagement.Business.Models.Responses;
using TaskManagement.DataAccess.Context;
using TaskManagement.DataAccess.Entities;
using TaskManagement.DataAccess.Paginator;
using TaskManagement.Models.Models.DTO;

namespace TaskManagement.DataAccess.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public PaginatorResponse<UserDto> GetUserPaginated(PaginatorPayload<UserFilter> payload)
        {
            IQueryable<User> query = (from q in _context.Users
                                      select q);

            if (payload.Filters != null)
            {
                query = query.Where(x => x.Username.Contains(payload.Filters.SearchValue)
                    || x.Name.Contains(payload.Filters.SearchValue)
                    || x.LastName.Contains(payload.Filters.SearchValue)
                    || x.Email.Contains(payload.Filters.SearchValue));
            }

            Paginator<User> paginatedResult = Paginator<User>.GetPagedResult(query, payload.PageIndex, payload.PageSize);

            List<UserDto> result = paginatedResult.Query.ToList().Adapt<List<UserDto>>();

            return new PaginatorResponse<UserDto>()
            {
                CurrentPage = payload.PageIndex,
                PageSize = payload.PageSize,
                TotalRecords = paginatedResult.TotalRecords,
                TotalPages = paginatedResult.TotalPages,
                DataList = result
            };
        }

        public async Task<KeyValueResponse> CreateUser(CreateUserPayload payload)
        {
            try
            {
                User? user = await _context.Users.Where(x => x.Username == payload.Username).FirstOrDefaultAsync();

                if (user != null)
                {
                    return new KeyValueResponse
                    {
                        Key = (int)ResponseEnum.Invalid,
                        Value = $"Ya existe el usuario {payload.Username}"
                    };
                }

                User oUser = new User
                {
                    Username = payload.Username,
                    Name = payload.Name,
                    LastName = payload.LastName,
                    Email = payload.Email,
                    Password = payload.Password,
                    RolId = payload.RolId,
                    Active = true,
                    CreatedDate = DateTime.Now,
                    CreatedUser = payload.User,
                };

                _context.Add(oUser);
                await _context.SaveChangesAsync();

                return new KeyValueResponse
                {
                    Key = (int)ResponseEnum.Success,
                    Value = "Usuario creado correctamente"
                };
            }
            catch (Exception ex)
            {
                return new KeyValueResponse
                {
                    Key = (int)ResponseEnum.Error,
                    Value = ex.Message
                };
            }
        }

        public async Task<KeyValueResponse> UpdateUser(UpdateUserPayload payload)
        {
            try
            {
                User? user = await _context.Users.Where(x => x.Id == x.Id).FirstOrDefaultAsync();

                if (user == null)
                {
                    return new KeyValueResponse
                    {
                        Key = (int)ResponseEnum.Invalid,
                        Value = "Ocurrio un error al actualizar el usuario"
                    };
                }

                user.Username = payload.Username;
                user.Name = payload.Name;
                user.LastName = payload.LastName;
                user.Email = payload.Email;
                user.UpdateDate = DateTime.Now;
                user.UpdateUser = payload.User;

                _context.Update(user);
                await _context.SaveChangesAsync();

                return new KeyValueResponse
                {
                    Key = (int)ResponseEnum.Success,
                    Value = "Usuario actualizado correctamente"
                };
            }
            catch (Exception ex)
            {
                return new KeyValueResponse
                {
                    Key = (int)ResponseEnum.Error,
                    Value = ex.Message
                };
            }
        }

        public async Task<KeyValueResponse> DeleteUser(int id)
        {
            try
            {
                User? user = await _context.Users.Where(x => x.Id == x.Id).FirstOrDefaultAsync();

                if (user == null)
                {
                    return new KeyValueResponse
                    {
                        Key = (int)ResponseEnum.Invalid,
                        Value = "Ocurrio un error al eliminar el usuario"
                    };
                }

                _context.Remove(user);
                await _context.SaveChangesAsync();

                return new KeyValueResponse
                {
                    Key = (int)ResponseEnum.Success,
                    Value = "Usuario eliminado correctamente"
                };
            }
            catch (Exception ex)
            {
                return new KeyValueResponse
                {
                    Key = (int)ResponseEnum.Error,
                    Value = "Ocurrio un error al eliminar el usuario"
                };
            }
        }

    }
}
