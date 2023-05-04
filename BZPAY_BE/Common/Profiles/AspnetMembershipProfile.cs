using AutoMapper;
using BZPAY_BE.DataAccess;
using BZPAY_BE.Models;
using System;

namespace BZPAY_BE.Common.Profiles
{
	/// <summary>
	/// Mapping Profile for Aspnet Membership
	/// </summary>
	public class AspnetMembershipProfile : Profile
	{
		/// <summary>
		/// Constructor Method for mapping profile
		/// </summary>
		public AspnetMembershipProfile()
		{
            AspnetMapper(CreateMap<AspnetMembership, AspnetMembershipDo>());
		}

        /// <summary>
        /// Mapped AspnetMembership to AspnetMembershipDo
        /// </summary>
        /// <param name="mappingExpression"></param>
        private void AspnetMapper(IMappingExpression<AspnetMembership, AspnetMembershipDo> mappingExpression)
        {
            mappingExpression.ForMember(dest => dest.ApplicationId, act => act.MapFrom(src => src.ApplicationId))
                             .ForMember(dest => dest.UserId, act => act.MapFrom(src => src.UserId))
                             .ForMember(dest => dest.Password, act => act.MapFrom(src => src.Password))
                             .ForMember(dest => dest.PasswordFormat, act => act.MapFrom(src => src.PasswordFormat))
                             .ForMember(dest => dest.PasswordSalt, opt => opt.MapFrom(src => src.PasswordSalt))
                             .ForMember(dest => dest.MobilePin, act => act.MapFrom(src => src.MobilePin))
                             .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                             .ForMember(dest => dest.LoweredEmail, opt => opt.MapFrom(src => src.LoweredEmail))
                             .ForMember(dest => dest.PasswordQuestion, opt => opt.MapFrom(src => src.PasswordQuestion))
                             .ForMember(dest => dest.PasswordAnswer, opt => opt.MapFrom(src => src.PasswordAnswer))
                             .ForMember(dest => dest.IsApproved, opt => opt.MapFrom(src => src.IsApproved))
                             .ForMember(dest => dest.IsLockedOut, opt => opt.MapFrom(src => src.IsLockedOut))
                             .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate))
                             .ForMember(dest => dest.LastLoginDate, opt => opt.MapFrom(src => src.LastLoginDate))
                             .ForMember(dest => dest.LastPasswordChangedDate, opt => opt.MapFrom(src => src.LastPasswordChangedDate))
                             .ForMember(dest => dest.LastLockoutDate, opt => opt.MapFrom(src => src.LastLockoutDate))
                             .ForMember(dest => dest.FailedPasswordAttemptCount, opt => opt.MapFrom(src => src.FailedPasswordAttemptCount))
                             .ForMember(dest => dest.FailedPasswordAttemptWindowStart, opt => opt.MapFrom(src => src.FailedPasswordAttemptWindowStart))
                             .ForMember(dest => dest.FailedPasswordAnswerAttemptCount, opt => opt.MapFrom(src => src.FailedPasswordAnswerAttemptCount))
                             .ForMember(dest => dest.FailedPasswordAnswerAttemptWindowStart, opt => opt.MapFrom(src => src.FailedPasswordAnswerAttemptWindowStart))
                             .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment));
        }
    }
}
