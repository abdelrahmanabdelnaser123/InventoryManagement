using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using InventoryManagement.Services.DTOS;
using InventoryManagement.Services.IServices;
using InventoryManagement.Services.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using InventoryManagement.Infrastructure;
using InventoryManagement.Infrastructure.Extensions;





namespace InventoryManagement.Services.Services
{

    public class AuthServices : IAuthServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;

        public AuthServices(UserManager<ApplicationUser> userManager,IConfiguration config)
        {
            this.userManager = userManager;
            this.config = config;
        }

        public async Task<Response<bool>> Register(RegisterDTO registerDTO)
        {
            ApplicationUser Finduser = await userManager.FindByEmailAsync(registerDTO.Email);
            if (Finduser != null)
            {
                return new Response<bool>
                {
                    Status = ResponseStatus.Conflict,
                    Message = " user already exist",

                };

            }


            ApplicationUser user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Email,

            };


            IdentityResult result = await userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                return new Response<bool>
                {
                    Status = ResponseStatus.Success,
                    Message = " User registered successfully",

                };
            }

            else
            {
                return new Response<bool>
                {
                    Status = ResponseStatus.InternalServerError,
                    Message = " Sorry please try again",
                    InternalMessage = "error from server",

                };
            }
        }

        public async Task<Response<LoginResponseDTO>> Login(LoginDTO loginDTO)
        {
            try
            {
                ApplicationUser user = await userManager.FindByEmailAsync(loginDTO.Email);

                if (user == null)
                {
                    return new Response<LoginResponseDTO>
                    {
                        Status = ResponseStatus.NotFound,
                        Message = " error",

                    };
                }

                bool isValid = await userManager.CheckPasswordAsync(user, loginDTO.Password);

                if (isValid)
                {
                    string token = await userManager.GenerateTokenAsync(user, config);
                    return new Response<LoginResponseDTO>
                    {

                        Status = ResponseStatus.Success,
                        Message = " User login successfully",
                        Data = new LoginResponseDTO
                        {
                            Token = token,
                            Name = user.UserName,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber,
                        
                        }


                    };
                }

                return new Response<LoginResponseDTO>
                {
                    Status = ResponseStatus.BadRequest,
                };


            }

            catch (Exception ex)
            {
                return new Response<LoginResponseDTO>
                {
                    Status = ResponseStatus.InternalServerError,
                    Message = "check your email.",
                    InternalMessage = $"error from server {ex.Message}",

                };
            }
            }
    }
}



