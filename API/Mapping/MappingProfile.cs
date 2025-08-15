using AutoMapper;
using HRManagement.Entitites;
using HRManagement.DTOs;

namespace HRManagement.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<UserDetail, UserDetailDto>().ReverseMap();
            CreateMap<Pozisyon, PozisyonDto>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestDto>().ReverseMap();
            CreateMap<TaskItem, TaskItemDto>().ReverseMap();
            CreateMap<AppNotification, AppNotificationDto>().ReverseMap();
            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<Payment, PaymentDto>().ReverseMap();
        }
    }
}
    public enum LeaveStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2
    }