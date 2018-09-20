using System.Linq;
using AutoMapper;
using NGH.API.Controllers.Resources;
using NGH.API.Models;

namespace NGH.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerResource>()
                .ForMember(dest => dest.CustomerGroupName, opt => 
                    opt.MapFrom(src => src.CustomerGroup.GroupName)).ReverseMap();
            CreateMap<User, UserListResource>().ReverseMap();
            CreateMap<User, UserResource>().ReverseMap();
            CreateMap<User, RegisterUserResource>().ReverseMap();
            CreateMap<User, LoginUserResource>().ReverseMap();
            CreateMap<DeliveryNote, DeliveryNoteResource>()
                .ForMember(dest => dest.CustomerName, opt => 
                    opt.MapFrom(src => src.Customer.CompanyName))
                .ForMember(dest => dest.TaxId, opt => 
                    opt.MapFrom(src => src.Customer.TaxId))
                .ForMember(dest => dest.StatusName, opt => 
                    opt.MapFrom(src => src.Status.StatusName))
                .ForMember(dest => dest.BranchCode, opt => 
                    opt.MapFrom(src => src.Customer.BranchCode)).ReverseMap();
            CreateMap<DeliveryNoteLine, DeliveryNoteLineResource>().ReverseMap();
            CreateMap<DeliveryNoteResource, DeliveryNote>()
                .ForMember(dest => dest.Customer, opt => opt.Ignore());
            CreateMap<Product, ProductResource>();
            CreateMap<ProductDiscount, ProductDiscountResource>();
            CreateMap<ProductResource, Product>()
                //.ForMember(src => src.Discounts, opt => opt.Ignore())
                .AfterMap((src, dest) => {
                    // Remove unselected features
                    var removedFeatures = dest.ProductDiscounts.ToList();
                        /*.Where(
                        d => !src.Discounts.Any(
                            x => x.CustomerId == d.CustomerId)).ToList();*/
                    foreach (var f in removedFeatures)
                        dest.ProductDiscounts.Remove(f);

                    // Add new features
                    var addedFeatures = src.Discounts.Select(pd => new ProductDiscount {
                            CustomerId = pd.CustomerId,
                            DiscPrice = pd.DiscPrice,
                            Discount = pd.Discount
                        }).ToList();
                        /*.Where(
                        disc => !dest.Discounts.Any(
                            f => f.CustomerId == disc.CustomerId)).Select(
                                pd => new ProductDiscount { Id = pd.Id }).ToList();*/
                    foreach (var f in addedFeatures)
                        dest.ProductDiscounts.Add(f);
                });
        }
    }
}