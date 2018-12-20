using AutoMapper;
using EncoreDAL;
using EncoreML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoreBL.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>();
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
            CreateMap<UserLogin, UserLoginModel>();
            CreateMap<UserLoginModel, UserLogin>();
        }

        public static void Run()
        {
            AutoMapper.Mapper.Initialize(a => a.AddProfile<AutoMapperProfile>());
        }
    }
}
