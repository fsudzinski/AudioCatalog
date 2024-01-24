using Microsoft.AspNetCore.Mvc;
using Sudzinski.AudioCatalog.WebApp.Models;

namespace Sudzinski.AudioCatalog.WebApp.Controllers
{
    public class ProducersController : Controller
    {
        private readonly BLC.BLC _blc;

        public ProducersController(BLC.BLC blc)
        {
            _blc = blc;
        }

        // GET: /Producers/Add
        public IActionResult Add()
        {
            var model = new AddProducerViewModel();
            return View(model);
        }

        // POST: /Producers/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddProducerViewModel model)
        {
            if (ModelState.IsValid)
            {
                _blc.CreateProducer(model.Name, model.CountryOfOrigin, model.Website);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        // GET: /Producers/Edit/{id}
        public IActionResult Edit(int id)
        {
            var producer = _blc.GetProducerById(id);

            var editModel = new EditProducerViewModel
            {
                Id = producer.Id,
                Name = producer.Name,
                CountryOfOrigin = producer.CountryOfOrigin,
                Website = producer.Website
            };

            return View(editModel);
        }

        // POST: /Producers/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditProducerViewModel model)
        {
            if (ModelState.IsValid)
            {
                _blc.UpdateProducer(model.Id, model.Name, model.CountryOfOrigin, model.Website);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        // GET: /Producers/Delete/{id}
        public IActionResult Delete(int id)
        {
            var producer = _blc.GetProducerById(id);
            var model = new DeleteProducerViewModel()
            {
                Id = producer.Id,
                Name = producer.Name
            };            

            return View(model);
        }

        // POST: /Producers/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(DeleteProducerViewModel model)
        {
            _blc.DeleteProducer(model.Id);
            return RedirectToAction("Index", "Home");
        }
    }
}
