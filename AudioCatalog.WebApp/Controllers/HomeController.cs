using System.Diagnostics;
using Sudzinski.AudioCatalog.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Sudzinski.AudioCatalog.Interfaces;

namespace Sudzinski.AudioCatalog.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BLC.BLC _blc;
        public HomeController(ILogger<HomeController> logger, BLC.BLC blc)
        {
            _logger = logger;
            _blc = blc;
        }                

        public IActionResult Index(string producersSearchTerm, string speakersSearchTerm, string activeTab)
        {
            var producers = _blc.GetAllProducers();
            var speakers = _blc.GetAllSpeakers();

            if (producers == null)
            {
                producers =  new List<IProducer>();
            }
            if (speakers == null)
            {
                speakers = new List<ISpeaker>();
            }            

            if (!string.IsNullOrEmpty(producersSearchTerm))
            {
                producers = producers.Where(p => p.Name.Contains(producersSearchTerm, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(speakersSearchTerm))
            {
                speakers = speakers.Where(s => s.Name.Contains(speakersSearchTerm, StringComparison.OrdinalIgnoreCase));
            }            

            var viewModel = new HomeViewModel
            {
                Producers = producers.OrderBy(p => p.Id),
                Speakers = speakers.OrderBy(p => p.Id),
                AllCountries = producers.Select(s => s.CountryOfOrigin).Distinct().Order().ToList()
            };

            ViewBag.ActiveTab = activeTab;

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
