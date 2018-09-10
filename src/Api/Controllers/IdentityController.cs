// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("[controller]")]
    
    public class IdentityController : ControllerBase
    {
        //public bool setScope(string scope)
        //{
        //    var a= new JsonResult(from c in User.Claims
        //                   where c.Type.Contains("scope")
        //                   where c.Value.Contains("api1")
        //                   select new { c.Type, c.Value });
        //    return true;
        //}
        public bool setScope(String copeString)
        {
            String Name = User.Identity.Name;
            var b = User.Claims.Where(s => s.Type.Equals("scope")).Select(x=>new{value= x.Value}).ToList();
            foreach (var item in b)
            {
                if (item.value.ToString().Equals(copeString))
                {
                    Console.WriteLine("ok mennnnnn");
                }
                
            }
            return false;
            
        }
        
        [HttpGet]
        public IActionResult Get()
        {

            String Name = User.Identity.Name;
            //String EmailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            //String role = User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            if (Name.Equals("adminrole"))
            {
                Console.WriteLine("admin role ");
            }
            if (Name.Equals("userrole"))
            {
                Console.WriteLine("user role ");
            }
           

            return Ok();

        }
             //return new JsonResult(from c in User.Claims select new { c.Type, c.Value});
            



    }
    
    
}