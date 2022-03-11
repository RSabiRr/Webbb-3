﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Universite_Web.Data;
using Universite_Web.Models;
using Universite_Web.ViewModel;

namespace Universite_Web.Controllers
{
    public class AccountUserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountUserController(AppDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
       

        public IActionResult Register()
        {
            ViewData["EducationSectionId"] = new SelectList(_context.EducationSections, "Id", "Country");
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name");
            ViewData["SpecialtyId"] = new SelectList(_context.Specialty, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(VmStudentRegister model)
        {
            if (ModelState.IsValid)
            {
                CustomUser user = new CustomUser()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    UserName = model.Email,
                    PhoneNumber = model.Phone,
                    Adress = model.Adress,
                    AtaAdi=model.AtaAdi,
                    PassportNumber=model.PassportNumber,
                    Gender=model.Gender,
                    Money=model.Money,
                    FormEducation=model.FormEducation,
                    DateOfBirth=model.DateOfBirth,
                    AdmissionDate=model.AdmissionDate,
                    DateOfCompletion=model.DateOfCompletion,
                    Image=model.Image,
                    FacultyId=model.FacultyId,
                    SpecialtyId=model.FacultyId,
                    EducationSectionId=model.EducationSectionId
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("index", "home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }

               
            }
            ViewData["EducationSectionId"] = new SelectList(_context.EducationSections, "Id", "Country");
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name");
            ViewData["SpecialtyId"] = new SelectList(_context.Specialty, "Id", "Name");
            return View(model);
        }

        public IActionResult Index()
        {
            var salam= _signInManager.IsSignedIn(User);
            var sagol = _userManager.GetUserId(User);

            Users users = new Users()
            {
                CustomUsers = _context.CustomUser.Where(m => m.Id == sagol).ToList()
            };
            return View(users);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(VmStudentLogin model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Email or password is not valid");
                    return View(model);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index","home");
        }

        public async Task<IActionResult> About(string id)
        {
            var user = await _context.CustomUser.Include(m => m.EducationSection).Include(r => r.Faculty).Include(r => r.Specialty)
                   .FirstOrDefaultAsync(m => m.Id == id);
                   

            return View(user);
        }
    }
}

