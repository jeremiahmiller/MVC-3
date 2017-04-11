using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuessingGame.Models;
using GuessingGame.Services;
using Ninject;

namespace GuessingGame.Controllers
{
    public class GameController : Controller
    {
        private readonly IRandomNumberGenerator _rng;

        public GameController([Named("AdvancedRNG")]IRandomNumberGenerator rng)
        {
            _rng = rng;
        }

        private bool GuessWasCorrect(int guess) =>
            guess == (int)Session["Answer"];

        public ActionResult Index()
        {
            IRandomNumberGenerator rng = new AdvancedRandomNumberGenerator();

            Session["Answer"] = rng.GetNext(1, 10);
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(GameModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Win = GuessWasCorrect(model.Guess);
            }

            return View(model);
        }
    }
}
