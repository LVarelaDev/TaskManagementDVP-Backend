using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using TaskManagement.Business.Enums;
using TaskManagement.Business.IRepository.Task;
using TaskManagement.Business.Models.DTO;
using TaskManagement.Business.Models.Payload;
using TaskManagement.Business.Models.Responses;
using TaskManagement.DataAccess.Context;

namespace TaskManagement.DataAccess.Repository.TaskRepository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<KeyValueResponse> CreateTask(TaskDTO taskDTO)
        {
            try
            {
                Entities.Task oTask = new Entities.Task
                {
                    Code = GenerateCodeRandom(),
                    Title = taskDTO.Title,
                    Description = taskDTO.Description,
                    Type = taskDTO.Type,
                    StatusId = (int)TaskEnum.Pending,
                    Active = true,
                    CreatedDate = DateTime.Now,
                    AssignedUserId = taskDTO.AssignedUserId,
                    CreatedUser = taskDTO.User,
                    UpdateDate = null,
                    UpdateUser = null
                };

                _context.Add(oTask);
                await _context.SaveChangesAsync();

                return new KeyValueResponse
                {
                    Key = (int)ResponseEnum.Success,
                    Value = "Tarea creada correctamente"
                };
            }
            catch
            {
                return new KeyValueResponse
                {
                    Key = (int)ResponseEnum.Error,
                    Value = "Ocurrió un error al crear la tarea"
                };
            }

        }

        public async Task<KeyValueResponse> UpdateTask(UpdateTaskPayload taskDTO)
        {
            try
            {
                Entities.Task? task = await _context.Tasks.Where(x => x.Code == taskDTO.code).FirstOrDefaultAsync();

                if (task != null)
                {
                    task.Title = taskDTO.Title;
                    task.Description = taskDTO.Description;
                    task.Type = taskDTO.Type;
                    task.StatusId = taskDTO.Status;
                    task.StartDate = taskDTO.Status == (int)TaskEnum.InProgress ? DateTime.Now : null;
                    task.EndDate = taskDTO.Status == (int)TaskEnum.Completed ? DateTime.Now : null;
                    task.AssignedUserId = taskDTO.AssignedUserId;
                    task.Active = true;
                    task.CreatedDate = DateTime.Now;
                    task.CreatedUser = taskDTO.User;
                    task.UpdateDate = DateTime.Now;
                    task.UpdateUser = taskDTO.User;

                    _context.Update(task);
                    await _context.SaveChangesAsync();

                    return new KeyValueResponse
                    {
                        Key = (int)ResponseEnum.Success,
                        Value = "Tarea actualizada correctamente"
                    };
                }

                return new KeyValueResponse
                {
                    Key = (int)ResponseEnum.Invalid,
                    Value = $"No existe una tarea con el codigo {taskDTO.code}"
                };
            }
            catch
            {
                return new KeyValueResponse
                {
                    Key = (int)ResponseEnum.Error,
                    Value = "Ocurrió un error al actualizar la tarea"
                };
            }
        }

        public async Task<List<TaskResponse>> GetTaskByCodeAndTitle(string searchValue)
        {
            List<Entities.Task> listTask = await _context.Tasks
                                                    .Where(x => x.Code.ToString().Contains(searchValue) || x.Title.Contains(searchValue))
                                                    .ToListAsync();

            return listTask.Adapt<List<TaskResponse>>();
        }

        public async Task<List<TaskResponse>> GetAll()
        {
            List<Entities.Task> tasks = await _context.Tasks.ToListAsync();

            return tasks.Adapt<List<TaskResponse>>();
        }

        public async Task<KeyValueResponse> UpdateStatus(UpdateStatus payload)
        {
            try
            {
                Entities.Task? task = await _context.Tasks.Where(x => x.Code == payload.Code).FirstOrDefaultAsync();

                if (task == null)
                {
                    return new KeyValueResponse
                    {
                        Key = (int)ResponseEnum.Invalid,
                        Value = $"No existe una tarea con el codigo {payload.Code}"
                    };
                }

                task.StatusId = payload.Status;
                task.StartDate = payload.Status == (int)TaskEnum.InProgress ? DateTime.Now : task.StartDate;
                task.EndDate = payload.Status == (int)TaskEnum.Completed ? DateTime.Now : task.EndDate;
                task.UpdateDate = DateTime.Now;

                _context.Update(task);
                await _context.SaveChangesAsync();

                return new KeyValueResponse
                {
                    Key = (int)ResponseEnum.Success,
                    Value = "Tarea actualizada correctamente"
                };
            }
            catch
            {
                return new KeyValueResponse
                {
                    Key = (int)ResponseEnum.Error,
                    Value = "Ocurrió un error al actualizar la tarea"
                };
            }

        }

        public async Task<List<UsersSelectDTO>> GetUsersForSelect()
        {
            try
            {
                List<UsersSelectDTO> listUsers = await _context.Users.Where(x => x.RolId == (int)RolesEnum.Empleado).Select(x => new UsersSelectDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    LastName = x.LastName
                }).ToListAsync();

                return listUsers;
            }
            catch
            {
                throw;
            }

        }

        #region private method
        private static int GenerateCodeRandom()
        {
            Random random = new Random();

            int numRandom = random.Next(1000, 10000);

            return numRandom;
        }
        #endregion
    }
}
