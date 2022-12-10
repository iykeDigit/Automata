using AutoMapper;
using ExpenseWebApp.Dtos;
using ExpenseWebApp.Dtos.ExpenseAccountDtos;
using ExpenseWebApp.Dtos.NotificationDtos;
using ExpenseWebApp.Dtos.ExpenseFormDto;
using ExpenseWebApp.Models;
using ExpenseWebApp.Dtos.ExpenseFormDetailsDtos;
using ExpenseWebApp.Dtos.ExpenseAdvanceDtos;

namespace ExpenseWebApp.Utilities.AutoMapper
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            //Expense Form response Dto mapping
            CreateMap<ExpenseForm, ExpenseFormResponseDto>()
                .ForMember(x => x.ExpenseFormNo, y => y.MapFrom(x => x.ExpenseFormNo))
                .ForMember(x => x.Description, y => y.MapFrom(x => x.Description))
                .ForMember(x => x.ReimburseableAmount, y => y.MapFrom(x => x.ReimburseableAmount))
                .ForMember(x => x.ReimbursementDate, y => y.MapFrom(x => x.ReimbursementDate))
                .ForMember(x => x.DateCreated, y => y.MapFrom(x => x.DateCreated))
                .ForMember(x => x.PaidBy, y => y.MapFrom(x => x.PaidBy))
                .ForMember(x => x.ApprovedBy, y => y.MapFrom(x => x.ApprovedBy))
                .ForMember(x => x.ExpenseDetails, y => y.MapFrom(x => x.ExpenseFormDetails))
                .ForMember(x => x.ExpenseStatus, y => y.MapFrom(x => x.ExpenseStatus.Description));


            
                

            CreateMap<ExpenseFormCreateRequestDto, ExpenseForm>();
            CreateMap<ExpenseForm, ExpenseFormCreateResponseDto>();


            //Expense Advance - submit expense advence
            CreateMap<SubmitExpenseAdvanceDto, ExpenseAdvance>().ReverseMap();


            CreateMap<ExpenseFormDetails, ExpenseFormDetailDto>()
                .ForMember(x => x.ExpenseCategoryName, y => y.MapFrom(src => src.ExpenseCategory.ExpenseCategoryName))
                .ForMember(x => x.ExpenseAmount, y => y.MapFrom(src => src.ExpenseAmount))
                .ForMember(x => x.ExpenseDate, y => y.MapFrom(src => src.ExpenseDate))
                .ForMember(x => x.ExpenseFormId, y => y.MapFrom(src => src.ExpenseFormId))
                .ForMember(x => x.PaidByCompany, y => y.MapFrom(src => src.PaidByCompany))
                .ForMember(x => x.Attachments, y => y.MapFrom(src => src.Attachments))
                .ForMember(x => x.ExpenseNote, y => y.MapFrom(src => src.ExpenseNote)).ReverseMap();

            CreateMap<ExpenseFormDetails, ExpenseFormDetailResponseDto>()
                .ForMember(x => x.ExpenseCategoryName, y => y.MapFrom(src => src.ExpenseCategory.ExpenseCategoryName))
                .ForMember(x => x.ExpenseAmount, y => y.MapFrom(src => src.ExpenseAmount))
                .ForMember(x => x.ExpenseDate, y => y.MapFrom(src => src.ExpenseDate))
                .ForMember(x => x.ExpenseFormId, y => y.MapFrom(src => src.ExpenseFormId))
                .ForMember(x => x.PaidByCompany, y => y.MapFrom(src => src.PaidByCompany))
                .ForMember(x => x.ExpenseNote, y => y.MapFrom(src => src.ExpenseNote)).ReverseMap();



            //ExpenseAdvance - ExpenseAdvanceResponseDTO
            CreateMap<ExpenseAdvance, ExpenseAdvanceFormResponseDTO>()
                .ForMember(x => x.AdvanceFormNo, y => y.MapFrom(x => x.AdvanceFormNo))
                .ForMember(x => x.AdvanceDescription, y => y.MapFrom(x => x.AdvanceDescription))
                .ForMember(x => x.AdvanceAmount, y => y.MapFrom(x => x.AdvanceAmount))
                .ForMember(x => x.AdvancePurpose, y => y.MapFrom(x => x.AdvancePurpose))
                .ForMember(x => x.AdvanceDate, y => y.MapFrom(x => x.AdvanceDate))
                .ForMember(x => x.ApprovedBy, y => y.MapFrom(x => x.ApprovedBy))
                .ForMember(x => x.DisbursedBy, y => y.MapFrom(x => x.DisbursedBy))
                .ForMember(x => x.DisbursementDate, y => y.MapFrom(x => x.DisbursementDate))
                .ForMember(x => x.ExpenseStatus, y => y.MapFrom(x => x.ExpenseStatus.Description))
                .ForMember(x => x.PaidFrom, y => y.MapFrom(x => x.PaidFrom.PaidFromName))
                .ForMember(x => x.ExpenseStatus, y => y.MapFrom(x => x.ExpenseStatus.Description));



            // Expense Accounts Dto Mappings
            CreateMap<ExpenseAccount, CompanyAccountsDto>();


            //Notification
            CreateMap<Notification, NotificationDto>().ReverseMap();
            CreateMap<Notification, NotificationCreateDto>().ReverseMap();

            CreateMap<CreateExpenseAdvanceDto, ExpenseAdvance>().ReverseMap();
            CreateMap<EditExpenseAdvanceDto, ExpenseAdvance>().ReverseMap();
        
            //Expense Advance
            CreateMap<ExpenseAdvanceReturnDto, ExpenseAdvance>().ReverseMap();
        }
    }
}