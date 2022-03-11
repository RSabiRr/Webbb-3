using Microsoft.AspNetCore.Identity;
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
    
    public class LoginStudentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginStudentController(AppDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            var appDbContext = _context.RegisterSTUDENT.Include(r => r.EducationSection).Include(r => r.Faculty).Include(r => r.Specialty);


            ViewData["EducationSectionId"] = new SelectList(_context.EducationSections, "Id", "Country");
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name");
            ViewData["SpecialtyId"] = new SelectList(_context.Specialty, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(VmStudentRegister model)
        {
            var appDbContext = _context.RegisterSTUDENT.Include(r => r.EducationSection).Include(r => r.Faculty).Include(r => r.Specialty);

            if (ModelState.IsValid)
            {
                CustomUser user = new CustomUser()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    UserName = model.Email
                   
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(model);
                }

                //_context.Add(model.RegisterSTUDENT);
                await _context.SaveChangesAsync();
                
                return RedirectToAction("index", "RegisterSTUDENTs","admin");

            }
            ViewData["EducationSectionId"] = new SelectList(_context.EducationSections, "Id", "Country");
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name");
            ViewData["SpecialtyId"] = new SelectList(_context.Specialty, "Id", "Name");
            return View(model);

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
                    return RedirectToAction("about", "LoginStudent");
                }
                else
                {
                    ModelState.AddModelError("", "Email ve ya parol sehvdir");
                    return View(model);
                }
            }
            return View();
        }

        public IActionResult About()
        {

            return View();
        }
        [HttpPost]
        public IActionResult About(VmStudentLogin model)
        {
            
            return View(_context.RegisterSTUDENT.Find(model.Email));
        }
        public IActionResult Continuity()
        {
            return View();
        }

        public IActionResult Valuation()
        {
            return View();
        }
        public IActionResult Document()
        {
            return View();
        }

    }
}
