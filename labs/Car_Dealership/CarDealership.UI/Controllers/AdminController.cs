using CarDealership.DAL.Factories;
using CarDealership.Models.Tables;
using CarDealership.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.UI.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult Specials()
        {
            var repo = SpecialRepositoryFactory.GetRepository();
            var vm = new SpecialAddViewModel();

            vm.SpecialsList = repo.GetAll();
            vm.NewSpecial = new Special();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Specials(Special newSpecial)
        {
            var repo = SpecialRepositoryFactory.GetRepository();

            if (ModelState.IsValid)
            {
                repo.Insert(newSpecial);
                return RedirectToAction("Specials");
            }
            return View(newSpecial);
        }

        [HttpGet]
        public ActionResult Vehicles()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Makes()
        {
            var repo = MakeRepositoryFactory.GetRepository();
            var vm = new MakeAddViewModel();

            vm.MakeUserTable = repo.GetMakeUserTable();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Makes(MakeAddViewModel vm)
        {
            var repo = MakeRepositoryFactory.GetRepository();

            if (ModelState.IsValid)
            {
                var newMake = new Make()
                {
                    UserId = User.Identity.GetUserId(), // correct GUID?
                    Name = vm.NewMakeName // Data binding here?
                };

                repo.Insert(newMake);
                return RedirectToAction("Makes");
            }
            return View(vm);
        }

        [HttpGet]
        public ActionResult AddVehicle()
        {
            var vm = new VehicleAdminViewModel();

            var colorRepo   = ColorRepositoryFactory.GetRepository();
            var makeRepo    = MakeRepositoryFactory.GetRepository();
            var modelRepo   = ModelRepositoryFactory.GetRepository();

            vm.Makes          = new SelectList(makeRepo.GetAll(), "MakeId", "Name");
            vm.InteriorColors = new SelectList(colorRepo.GetAllInterior(), "InteriorColorId", "Name");
            vm.ExteriorColors = new SelectList(colorRepo.GetAllExterior(), "ExteriorColorId", "Name");

            return View(vm);
        }

        [HttpPost]
        public ActionResult AddVehicle(VehicleAdminViewModel vm)
        {
            var repo = VehicleRepositoryFactory.GetRepository();

            if (ModelState.IsValid)
            {
                var newVehicle = new Vehicle();

                newVehicle.UserId          = User.Identity.GetUserId();
                newVehicle.ModelId         = vm.ModelId;
                newVehicle.BodyStyleId     = vm.BodyStyleId;
                newVehicle.InteriorColorId = vm.InteriorColorId;
                newVehicle.ExteriorColorId = vm.ExteriorColorId;
                newVehicle.IsUsed          = vm.IsUsed;
                newVehicle.IsAutomatic     = vm.IsAutomatic;
                newVehicle.VIN             = vm.VIN;
                newVehicle.Description     = vm.Description;
                newVehicle.Image           = vm.Image;
                newVehicle.SalePrice       = vm.SalePrice;
                newVehicle.MSRP            = vm.MSRP;
                newVehicle.Mileage         = vm.Mileage;                

                repo.Insert(newVehicle);
                return RedirectToAction("Vehicles");
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditVehicle(int id)
        {
            var vm = new VehicleAdminViewModel();
            var repo = VehicleRepositoryFactory.GetRepository();
            var vehicle = repo.GetById(id);
            return View(vm);
        }

        [HttpPost]
        public ActionResult EditVehicle(VehicleAdminViewModel vm)
        {
            var repo = VehicleRepositoryFactory.GetRepository();

            if (ModelState.IsValid)
            {
                repo.Insert(vm.Vehicle);
                return RedirectToAction("Vehicles");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Users()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            var vm = new UserAddViewModel();
            return View(vm);
        }

        [HttpPost]
        public ActionResult AddUser(UserAddViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var context     = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var userStore   = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                vm.User = new ApplicationUser()
                {
                    FirstName = vm.FirstName,
                    LastName  = vm.LastName,
                    Email     = vm.Email,
                    UserName  = vm.Email
                };

                userManager.PasswordValidator = new PasswordValidator()
                {
                    RequiredLength          = 5,
                    RequireDigit            = true,
                    RequireLowercase        = true,
                    RequireUppercase        = true,
                    RequireNonLetterOrDigit = false
                };

                userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail             = true
                };

                var create = userManager.Create(vm.User, vm.Password);

                if (!create.Succeeded)
                {
                    ModelState.AddModelError("Email", "Email address is already in use.");
                    return View(vm);
                }
                else
                {
                    userManager.AddToRole(vm.User.Id, vm.Role);
                    return RedirectToAction("Users");
                }
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult EditUser(string id)
        {
            var context = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            var user    = context.Users.FirstOrDefault(u => u.Id == id);
            var vm      = new UserEditViewModel();
            vm.User     = user;

            return View(vm);
        }

        [HttpPost]
        public ActionResult EditUser(UserEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var context     = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var userStore   = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var user = userManager.Users.FirstOrDefault(u => u.Id == vm.User.Id);
                user.FirstName = vm.FirstName;
                user.LastName  = vm.LastName;
                user.Email     = vm.Email;
                user.UserName  = vm.Email;

                userManager.PasswordValidator = new PasswordValidator()
                {
                    RequiredLength          = 5,
                    RequireDigit            = true,
                    RequireLowercase        = true,
                    RequireUppercase        = true,
                    RequireNonLetterOrDigit = false
                };

                userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };

                var update = userManager.Update(user);

                if (!update.Succeeded)
                {
                    ModelState.AddModelError("", "Unknown error. Unable to update user.");
                    return View(vm);
                }
                else
                {
                    userManager.RemoveFromRoles(user.Id, userManager.GetRoles(user.Id).ToArray());
                    userManager.AddToRole(user.Id, vm.Role);
                    userStore.Context.SaveChanges();
                    return RedirectToAction("Users");
                }
            }
            else
            {
                return View(vm);
            }
        }
    }
}