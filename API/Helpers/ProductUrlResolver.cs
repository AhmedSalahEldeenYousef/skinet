using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config;
        //IConfiguration To Access To appsettings.Development.json
        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;

        }


        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                //Result => ApiUrl = > https://localhost:5001/PictureUrl
                return _config["ApiUrl"] + source.PictureUrl;
            }
            else
                return null;
        }
    }
}