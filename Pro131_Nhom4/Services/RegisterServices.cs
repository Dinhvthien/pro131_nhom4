﻿using App_Shared.ViewModels;
using Microsoft.AspNetCore.Identity;
using Pro131_Nhom4.Data;
using Pro131_Nhom4.IService;
using System.ComponentModel.DataAnnotations;

namespace Pro131_Nhom4.Services
{
    public class RegisterServices : IRegisterService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public RegisterServices(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<Response> RegisterAsync(RegisterUser registerUser, string role)
        {
            // Check user is exists or not
            if (await _userManager.FindByEmailAsync(registerUser.Email) != null)
            {
                return new Response
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = "This email is already exists!"
                };
            }
            else if (await _userManager.FindByNameAsync(registerUser.Username) != null)
            {
                return new Response
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = "This username is already exists!"
                };
            }

            // Check password is matching with confirm password or not
            if (registerUser.Password != registerUser.ConfirmPassword)
            {
                return new Response
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = "This password doesn't match with confirm password!"
                };
            }

            // Create an identity user
            User identityUser = new()
            {

        //                public DateTime DateOfBirth { get; set; }
        //public int Gender { get; set; }
        //[MaxLength(10)]
        //[RegularExpression(@"\d*[0-9]\d*", ErrorMessage = "The field PhoneNumber only has input number")]

        //public double Point { get; set; }
        //public string Address { get; set; }
        //public int Status { get; set; }
        Id = Guid.NewGuid(),
                UserName = registerUser.Username,
                Email = registerUser.Email,
                RankID = Guid.Parse("7a35b65c-c482-408b-b6b4-7bee2bf7e03e"),
                Point = 1,
                Status = 1,
                DateOfBirth = Convert.ToDateTime(registerUser.DateOfBirth),
                Gender =Convert.ToInt32(registerUser.Gender),
                PhoneNumber = registerUser.PhoneNumber,
                RoleId = Guid.Parse("654ad899-b858-4b64-b24a-d357279742be"),
                Address = registerUser.Address,
            };  

            // Check if roles is exists or not
            if (await _roleManager.RoleExistsAsync(role))
            {
                // Add user to the database          
                    var result = await _userManager.CreateAsync(identityUser, registerUser.Password);           
                // Check if register is fail
                //if (!result.Succeeded)
                //{
                //    return new Response
                //    {
                //        IsSuccess = false,
                //        StatusCode = 500,
                //        Message = "Register failed, something went wrong!"
                //    };
                //}

                // Add role to the user
                await _userManager.AddToRoleAsync(identityUser, role);
                return new Response
                {
                    IsSuccess = true,
                    StatusCode = 201,
                    Message = "Register successfully!"
                };
            }
            else
            {
                return new Response
                {
                    IsSuccess = false,
                    StatusCode = 404,
                    Message = "This role doesn't exists!"
                };
            }
        }
    }
}
