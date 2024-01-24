using Microsoft.AspNetCore.Mvc;
using Sudzinski.AudioCatalog.WebApp.Models;

namespace Sudzinski.AudioCatalog.WebApp.Controllers
{
    public class SpeakersController : Controller
    {
        private readonly BLC.BLC _blc;

        public SpeakersController(BLC.BLC blc)
        {
            _blc = blc;
        }

        // GET: /Speakers/Add
        public IActionResult Add()
        {
            var model = new AddSpeakerViewModel();
            model.AllProducers = _blc.GetAllProducers();
            return View(model);
        }

        // POST: /Speakers/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddSpeakerViewModel model)
        {
            if (ModelState.IsValid)
            {
                _blc.CreateSpeaker(model.Name, model.ProducerId, model.Power, model.Weight, model.Color);
                return RedirectToAction("Index", "Home", new { activeTab = "speakers-tab" });
            }

            return View(model);
        }

        // GET: /Speakers/Edit/{id}
        public IActionResult Edit(int id)
        {
            var speaker = _blc.GetSpeakerById(id);

            var editModel = new EditSpeakerViewModel
            {
                Id = speaker.Id,
                Name = speaker.Name,
                ProducerId = speaker.Producer.Id,
                Power = speaker.Power,
                Weight = speaker.Weight,
                Color = speaker.Color,
                AllProducers = _blc.GetAllProducers()
            };

            return View(editModel);
        }

        // POST: /Speakers/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditSpeakerViewModel model)
        {
            if (ModelState.IsValid)
            {
                _blc.UpdateSpeaker(model.Id, model.Name, model.ProducerId, model.Power, model.Weight, model.Color);
                return RedirectToAction("Index", "Home", new { activeTab = "speakers-tab" });
            }

            return View(model);
        }

        // GET: /Speakers/Delete/{id}
        public IActionResult Delete(int id)
        {
            var speaker = _blc.GetSpeakerById(id);
            var model = new DeleteSpeakerViewModel()
            {
                Id = speaker.Id,
                Name = speaker.Name
            };

            return View(model);
        }

        // POST: /Speakers/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(DeleteSpeakerViewModel model)
        {
            _blc.DeleteSpeaker(model.Id);
            return RedirectToAction("Index", "Home", new { activeTab = "speakers-tab" });
        }

    }
}
