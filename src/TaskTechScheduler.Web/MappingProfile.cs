using AutoMapper;
using Models;
using TaskTechScheduler.Web.ViewModels.MainAdminViewModel;

namespace TaskTechScheduler.Web;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Tasks, ListOfTasks>()
            .ForMember(t => t.TaskId,
                opt => opt.MapFrom(x => x.Id))
            .ForMember(t => t.Title,
                opt => opt.MapFrom(x => x.Title))
            .ForMember(t => t.Status, 
            opt => opt.MapFrom(x => x.isCompleted 
                ? ListOfTasks.StatusTask.Done 
                : x.AcceptedUserAdminId != null 
                    ? ListOfTasks.StatusTask.Doing
                    : ListOfTasks.StatusTask.Issued
            ));

        CreateMap<CreateTaskViewModel, Tasks>();
        CreateMap<Tasks, SingleTaskViewModel>().ReverseMap();
    }
    
}