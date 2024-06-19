using AutoMapper;
using UxDebt.Entities;
using UxDebt.Models.ViewModel;

namespace UxDebt.Mappers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<IssueViewModel, Issue>();
            CreateMap<Issue, IssueViewModel>();
            CreateMap<TagViewModel, Tag>();
            CreateMap<RepositoryViewModel, Repository>();
            CreateMap<Repository, RepositoryViewModel>();
            CreateMap<Issue, GetIssueViewModel>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.IssueTags.Select(it => it.Tag)))
            .ForMember(dest => dest.Labels, opt => opt.Ignore()); // Asumimos que no necesitas mapear Labels por ahora

            CreateMap<Tag, TagViewModel>(); // Si necesitas un view model específico para Tag


        }
    }
}
