using CarDealership.DAL.Factories;
using CarDealership.Models.Tables;
using CarDealership.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.UI.Controllers
{
    //[Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
        [HttpGet]
        public ActionResult Specials()
        {
            var repo = SpecialRepositoryFactory.GetRepository();
            var vm   = new SpecialAddViewModel();

            vm.SpecialsList = repo.GetAll();
            vm.NewSpecial   = new Special();

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
            var vm = new VehicleSearchViewModel();
            vm.SearchParams.IsAspNetUser = true;
            return View(vm);
        }

        [HttpGet]
        public ActionResult Makes()
        {
            var repo = MakeRepositoryFactory.GetRepository();
            var vm   = new MakeAddViewModel();

            vm.MakeUserTable = repo.GetMakeUserTable();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Makes(MakeAddViewModel vm)
        {
            var repo = MakeRepositoryFactory.GetRepository();

            if (ModelState.IsValid)
            {
                var newMake    = new Make();
                newMake.UserId = User.Identity.GetUserId();
                newMake.Name   = vm.NewMakeName;

                repo.Insert(newMake);

                return RedirectToAction("Makes");
            }
            else
            {
                repo = MakeRepositoryFactory.GetRepository();

                vm.MakeUserTable = repo.GetMakeUserTable();
                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult Models()
        {
            var modelRepo = ModelRepositoryFactory.GetRepository();
            var makeRepo  = MakeRepositoryFactory.GetRepository();
            var vm        = new ModelAddViewModel();

            vm.ModelUserTable = modelRepo.GetModelUserTable();
            vm.Makes          = new SelectList(makeRepo.GetAll(), "MakeId", "Name");

            return View(vm);
        }

        [HttpPost]
        public ActionResult Models(ModelAddViewModel vm)
        {
            var repo = ModelRepositoryFactory.GetRepository();

            if (ModelState.IsValid)
            {
                var newModel    = new Model();
                newModel.UserId = User.Identity.GetUserId();
                newModel.MakeId = vm.Make.MakeId;
                newModel.Name   = vm.NewModelName;
                newModel.Year   = DateTime.Now.Year; // can add possible Year option later

                repo.Insert(newModel);

                return RedirectToAction("Models");
            }
            else
            {
                var modelRepo = ModelRepositoryFactory.GetRepository();
                var makeRepo  = MakeRepositoryFactory.GetRepository();

                vm = new ModelAddViewModel();
                vm.ModelUserTable = modelRepo.GetModelUserTable();
                vm.Makes = new SelectList(makeRepo.GetAll(), "MakeId", "Name");

                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult AddVehicle()
        {
            var vm = new VehicleAdminViewModel();

            var colorRepo = ColorRepositoryFactory.GetRepository();
            var makeRepo  = MakeRepositoryFactory.GetRepository();
            var modelRepo = ModelRepositoryFactory.GetRepository();

            vm.Makes          = new SelectList(makeRepo.GetAll(), "MakeId", "Name");
            vm.InteriorColors = new SelectList(colorRepo.GetAllInterior(), "InteriorColorId", "Name");
            vm.ExteriorColors = new SelectList(colorRepo.GetAllExterior(), "ExteriorColorId", "Name");

            return View(vm);
        }

        [HttpPost]
        public ActionResult AddVehicle(VehicleAdminViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var repo = VehicleRepositoryFactory.GetRepository();

                try
                {
                    vm.Vehicle.UserId = User.Identity.GetUserId();

                    if (vm.ImageUpload != null && vm.ImageUpload.ContentLength > 0)
                    {
                        var savepath  = Server.MapPath("~/Images/Vehicles");
                        var fileName  = Path.GetFileNameWithoutExtension(vm.ImageUpload.FileName);
                        var extension = Path.GetExtension(vm.ImageUpload.FileName);
                        var filePath  = Path.Combine(savepath, fileName + extension);
                        var counter   = 1;

                        while (System.IO.File.Exists(filePath))
                        {
                            filePath = Path.Combine(savepath, fileName + counter.ToString() + extension);
                            counter++;
                        }

                        vm.ImageUpload.SaveAs(filePath);
                        vm.Vehicle.Image = Path.GetFileName(filePath);
                    }

                    repo.Insert(vm.Vehicle);

                    return RedirectToAction("EditVehicle", new { id = vm.Vehicle.VehicleId });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var vehicleRepo = VehicleRepositoryFactory.GetRepository();
                var colorRepo   = ColorRepositoryFactory.GetRepository();
                var makeRepo    = MakeRepositoryFactory.GetRepository();
                var modelRepo   = ModelRepositoryFactory.GetRepository();
                
                vm.Makes          = new SelectList(makeRepo.GetAll(), "MakeId", "Name");
                vm.InteriorColors = new SelectList(colorRepo.GetAllInterior(), "InteriorColorId", "Name");
                vm.ExteriorColors = new SelectList(colorRepo.GetAllExterior(), "ExteriorColorId", "Name");

                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult EditVehicle(int id)
        {
            var vm          = new VehicleAdminViewModel();
            var vehicleRepo = VehicleRepositoryFactory.GetRepository();
            var colorRepo   = ColorRepositoryFactory.GetRepository();
            var makeRepo    = MakeRepositoryFactory.GetRepository();
            var modelRepo   = ModelRepositoryFactory.GetRepository();

            vm.Makes          = new SelectList(makeRepo.GetAll(), "MakeId", "Name");
            vm.InteriorColors = new SelectList(colorRepo.GetAllInterior(), "InteriorColorId", "Name");
            vm.ExteriorColors = new SelectList(colorRepo.GetAllExterior(), "ExteriorColorId", "Name");
            vm.Vehicle        = vehicleRepo.GetById(id);

            return View(vm);
        }

        [HttpPost]
        public ActionResult EditVehicle(VehicleAdminViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var repo = VehicleRepositoryFactory.GetRepository();

                repo.Update(vm.Vehicle);
                return RedirectToAction("Vehicles");
            }
            else
            {
                var colorRepo = ColorRepositoryFactory.GetRepository();
                var makeRepo  = MakeRepositoryFactory.GetRepository();
                var modelRepo = ModelRepositoryFactory.GetRepository();

                vm.Makes          = new SelectList(makeRepo.GetAll(), "MakeId", "Name");
                vm.InteriorColors = new SelectList(colorRepo.GetAllInterior(), "InteriorColorId", "Name");
                vm.ExteriorColors = new SelectList(colorRepo.GetAllExterior(), "ExteriorColorId", "Name");

                return View(vm);
            }
            
        }

        [HttpGet]
        public ActionResult Users()
        {
            var vm   = new UserReportViewModel();
            vm.Users = AdminManagerFactory.GetManager().GetUserReport();

            return View(vm);
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
                    UserName  = vm.FirstName + vm.LastName
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
            var context  = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            var vm       = new UserEditViewModel();

            vm.User      = context.Users.FirstOrDefault(u => u.Id == id);
            vm.FirstName = vm.User.FirstName;
            vm.LastName  = vm.User.LastName;
            vm.Email     = vm.User.Email;

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
                var user        = userManager.Users.FirstOrDefault(u => u.Id == vm.User.Id);

                user.FirstName = vm.FirstName;
                user.LastName  = vm.LastName;
                user.Email     = vm.Email;
                user.UserName  = vm.FirstName + vm.LastName;

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