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
    public class RegisterTEACHERsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public RegisterTEACHERsController(AppDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: admin/RegisterTEACHERs
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.RegisterTEACHER.Include(r => r.Subject);
            return View(await appDbContext.ToListAsync());
        }

        // GET: admin/RegisterTEACHERs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registerTEACHER = await _context.RegisterTEACHER
                .Include(r => r.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registerTEACHER == null)
            {
                return NotFound();
            }

            return View(registerTEACHER);
        }



        public IActionResult Register()
        {

            ViewData["SubjectId"] = new SelectList(_context.Subject, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(VmTeahcerRegister model)
        {

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

                _context.Add(model.RegisterTEACHER);
                await _context.SaveChangesAsync();

                return RedirectToAction("index", "RegisterTEACHERs");

            }
            ViewData["SubjectId"] = new SelectList(_context.Subject, "Id", "Name", model.RegisterTEACHER.SubjectId);
            return View(model);

        }


        // GET: admin/RegisterTEACHERs/Create



        public IActionResult Create()
        {
            ViewData["SubjectId"] = new SelectList(_context.Subject, "Id", "Id");
            return View();
        }

        // POST: admin/RegisterTEACHERs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Image,Surname,Name,AtaAdi,PassportNumber,Gender,Phone,Adress,DateOfBirth,SubjectId")] RegisterTEACHER registerTEACHER)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registerTEACHER);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectId"] = new SelectList(_context.Subject, "Id", "Id", registerTEACHER.SubjectId);
            return View(registerTEACHER);
        }

        // GET: admin/RegisterTEACHERs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registerTEACHER = await _context.RegisterTEACHER.FindAsync(id);
            if (registerTEACHER == null)
            {
                return NotFound();
            }
            ViewData["SubjectId"] = new SelectList(_context.Subject, "Id", "Name", registerTEACHER.SubjectId);
            return View(registerTEACHER);
        }

        // POST: admin/RegisterTEACHERs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,Surname,Name,AtaAdi,PassportNumber,Gender,Phone,Adress,DateOfBirth,SubjectId")] RegisterTEACHER registerTEACHER)
        {
            if (id != registerTEACHER.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registerTEACHER);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegisterTEACHERExists(registerTEACHER.Id))
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
            ViewData["SubjectId"] = new SelectList(_context.Subject, "Id", "Name", registerTEACHER.SubjectId);
            return View(registerTEACHER);
        }

        // GET: admin/RegisterTEACHERs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registerTEACHER = await _context.RegisterTEACHER
                .Include(r => r.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registerTEACHER == null)
            {
                return NotFound();
            }

            return View(registerTEACHER);
        }

        // POST: admin/RegisterTEACHERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registerTEACHER = await _context.RegisterTEACHER.FindAsync(id);
            _context.RegisterTEACHER.Remove(registerTEACHER);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegisterTEACHERExists(int id)
        {
            return _context.RegisterTEACHER.Any(e => e.Id == id);
        }
    }
}
