using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesApp.Filter;
using MoviesApp.Models;
using MoviesApp.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using MoviesApp.Data;
using MoviesApp.Services.ArtistServices;
using MoviesApp.Services.Dto;


namespace MoviesApp.Controllers
{
    public class ArtistController: Controller
    {
       
        private readonly IArtistService _service;
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;


        public ArtistController(IArtistService service, ILogger<HomeController> logger,IMapper mapper, MoviesContext context)
        {
            
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

       
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            var artists = _mapper.Map<IEnumerable<ArtistDto>, IEnumerable<ArtistViewModel>>(_service.GetAllArtists());
            return View(artists);
        }
        
        [HttpGet]
        [Authorize]
        public IActionResult Films(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var viewModel = _mapper.Map<IEnumerable<ArtistDto>,IEnumerable <ViewFilmsViewModel>>(_service.MovieArtist((int) id)).ToList();
            
            return View(_mapper.Map<IEnumerable<ArtistDto>,IEnumerable <ViewFilmsViewModel>>(_service.MovieArtist((int) id)).ToList());
        }


        // GET: Movies/Details/5
        [HttpGet]
        [Authorize]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<ArtistViewModel>(_service.GetArtist((int) id));
                
            
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }
        
        // GET: Movies/Create
        [HttpGet]
        [Authorize(Roles = "Admin")] 
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")] 
        [ValidateAntiForgeryToken]
        [AgeArtistFilter]
        public IActionResult Create([Bind("FirstName,LastName,BirthdayDate")] InputArtistViewModel inputModel)
        {
            if (ModelState.IsValid)
            { 
                _service.AddArtist(_mapper.Map<ArtistDto>(inputModel)); 
                return RedirectToAction(nameof(Index));
            }
            return View(inputModel);
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")] 

        // GET: Movies/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editModel=_mapper.Map<EditArtistViewModel>(_service.GetArtist((int) id));
            
            
            if (editModel == null)
            {
                return NotFound();
            }
            
            return View(editModel);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")] 
        [ValidateAntiForgeryToken]
        [AgeArtistFilter]
        public IActionResult Edit(int id, [Bind("FirstName,LastName,BirthdayDate")] EditArtistViewModel editModel)
        {
            if (ModelState.IsValid)
            {
                var artist = _mapper.Map<ArtistDto>(editModel);
                artist.Id = id;

                var result = _service.UpdateArtist(artist);

                if (result == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(editModel);
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")] 
        // GET: Movies/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deleteModel=_mapper.Map<DeleteArtistViewModel>(_service.GetArtist((int) id));
          
            
            if (deleteModel == null)
            {
                return NotFound();
            }

            return View(deleteModel);
        }
        
        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")] 
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var artist= _service.DeleteMoviesArtists(id);
            if (artist==null)
            {
                return NotFound();
            }
            _logger.LogError($"Movie with id {artist.Id} has been deleted!");
            return RedirectToAction(nameof(Index));
        }
        
    }
}