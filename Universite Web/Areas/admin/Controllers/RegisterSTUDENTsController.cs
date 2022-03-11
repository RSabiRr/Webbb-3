using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Universite_Web.Data;
using Universite_Web.Models;
using Universite_Web.ViewModel;

namespace Universite_Web.Areas.admin.Controllers
{
    [Area("admin")]
    public class RegisterSTUDENTsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public RegisterSTUDENTsController(AppDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: admin/RegisterSTUDENTs
        public async Task<IActionResult> Index()
        {

            var appDbContext = _context.RegisterSTUDENT.Include(r => r.EducationSection).Include(r => r.Faculty).Include(r => r.Specialty);
            return View(await appDbContext.ToListAsync());
        }

        // GET: admin/RegisterSTUDENTs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var registerSTUDENT = await _context.RegisterSTUDENT
                .Include(r => r.EducationSection)
                .Include(r => r.Faculty)
                .Include(r => r.Specialty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registerSTUDENT == null)
            {
                return NotFound();
            }

            return View(registerSTUDENT);
        }

        // GET: admin/RegisterSTUDENTs/Create
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

                return RedirectToAction("index", "RegisterSTUDENTs");

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
        public IActionResult Indexx()
        {
            return View();
        }

        public JsonResult GetState(int id)
        {
            List<Specialty> specialties = new List<Specialty>();
            specialties= _context.Specialty.Where(z => z.Faculty.Id == id).ToList();
            return Json(new SelectList(specialties, "Id", "Name"));
        }


        // GET: admin/RegisterSTUDENTs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registerSTUDENT = await _context.RegisterSTUDENT.FindAsync(id);
            if (registerSTUDENT == null)
            {
                return NotFound();
            }
            ViewData["EducationSectionId"] = new SelectList(_context.EducationSections, "Id", "Country", registerSTUDENT.EducationSectionId);
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name", registerSTUDENT.FacultyId);
            ViewData["SpecialtyId"] = new SelectList(_context.Specialty, "Id", "Name", registerSTUDENT.SpecialtyId);
            return View(registerSTUDENT);
        }

        // POST: admin/RegisterSTUDENTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  RegisterSTUDENT registerSTUDENT)
        {
            if (id != registerSTUDENT.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registerSTUDENT);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegisterSTUDENTExists(registerSTUDENT.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EducationSectionId"] = new SelectList(_context.EducationSections, "Id", "Country", registerSTUDENT.EducationSectionId);
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name", registerSTUDENT.FacultyId);
            ViewData["SpecialtyId"] = new SelectList(_context.Specialty, "Id", "Name", registerSTUDENT.SpecialtyId);
            return View(registerSTUDENT);
        }

        // GET: admin/RegisterSTUDENTs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registerSTUDENT = await _context.RegisterSTUDENT
                .Include(r => r.EducationSection)
                .Include(r => r.Faculty)
                .Include(r => r.Specialty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registerSTUDENT == null)
            {
                return NotFound();
            }

            return View(registerSTUDENT);
        }

        // POST: admin/RegisterSTUDENTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registerSTUDENT = await _context.RegisterSTUDENT.FindAsync(id);
            _context.RegisterSTUDENT.Remove(registerSTUDENT);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegisterSTUDENTExists(int id)
        {
            return _context.RegisterSTUDENT.Any(e => e.Id == id);
        }
    }
}
