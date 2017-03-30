using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeroSaga.Data;

namespace HeroSaga.Controllers
{
    public class CharactersController : Controller
    {
        private readonly ICharacterRepo _characterRepo;

        public CharactersController(ICharacterRepo characterRepo)
        {
            _characterRepo = characterRepo;
        }
        // GET: Characters
        public ActionResult Index()
        {
            return View(_characterRepo.GetCharacters());
        }

        // GET: Characters/Details/5
        public ActionResult Details(int id)
        {
            return View(_characterRepo.GetCharacter(id));
        }

        // GET: Characters/Create
        public ActionResult Create()
        {
            return View(new Character());
        }

        // POST: Characters/Create
        [HttpPost]
        public ActionResult Create(Character character)
        {
            try
            {
                // TODO: Add insert logic here
                _characterRepo.CreateCharacter(character);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Characters/Edit/5
        public ActionResult Edit(int id)
        {
            
            return View(_characterRepo.GetCharacter(id));
        }

        // POST: Characters/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Character character)
        {
            try
            {
                // TODO: Add update logic here
                _characterRepo.UpdateCharacter(character);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(character);
            }
        }

        // GET: Characters/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Characters/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Character collection)
        {
            try
            {
                // TODO: Add delete logic here
                _characterRepo.DeleteCharacter(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
