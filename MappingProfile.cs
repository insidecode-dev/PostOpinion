using AutoMapper;
using PostOpinion.Domain.Entities;
using PostOpinion.DTO;

namespace PostOpinion
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CommentForInsertionDTO, Comment>().ForMember(dest => dest.ID, opt => opt.Ignore());
            CreateMap<PostForInsertionDTO, Post>().ForMember(dest => dest.ID, opt => opt.Ignore());
            CreateMap<UserDTO, User>().ForMember(dest => dest.ID, opt => opt.Ignore());
        }        
    }
}
