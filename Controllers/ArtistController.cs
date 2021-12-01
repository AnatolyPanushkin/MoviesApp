using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesApp.Data;
using MoviesApp.Filter;
using MoviesApp.Models;
using MoviesApp.ViewModels;
using AutoMapper;

namespace MoviesApp.Controllers
{
    public class ArtistController: Controller
    {
        private readonly MoviesContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;


        public ArtistController(MoviesContext context, ILogger<HomeController> logger,IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: Movies
        [HttpGet]
        public IActionResult Index()
        {
            var artists = _mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistViewModel>>(_context.Artists
                .ToList());
            return View(artists);
        }
        
        [HttpGet]
        public IActionResult Films(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = _context.MoviesArtists.Where(m => m.ArtistId == id).Select(m => new ViewFilmsViewModel
            {
                movieName = m.Movie.Title
            }).ToList();
           

            
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }


        // GET: Movies/Details/5
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<ArtistViewModel>(_context.Artists.FirstOrDefault(m=>m.Id==id));
                
            
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }
        
        // GET: Movies/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AgeArtistFilter]
        public IActionResult Create([Bind("FirstName,LastName,BirthdayDate")] InputArtistViewModel inputModel)
        {
            if (ModelState.IsValid)
            {
                _context.Artists.Add(_mapper.Map<Artist>(inputModel));
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(inputModel);
        }
        
        [HttpGet]

        // GET: Movies/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editModel=_mapper.Map<EditArtistViewModel>(_context.Artists.FirstOrDefault(m => m.Id == id));
            
            
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
        [ValidateAntiForgeryToken]
        [AgeArtistFilter]
        public IActionResult Edit(int id, [Bind("FirstName,LastName,BirthdayDate")] EditArtistViewModel editModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var artist = _mapper.Map<Artist>(editModel);
                    artist.Id = id;
                    
                    _context.Update(artist);
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (!ArtistExists(id))
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
            return View(editModel);
        }
        
        [HttpGet]
        // GET: Movies/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deleteModel=_mapper.Map<DeleteArtistViewModel>(_context.Artists.FirstOrDefault(m => m.Id == id));
          
            
            if (deleteModel == null)
            {
                return NotFound();
            }

            return View(deleteModel);
        }
        
        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var artist = _context.Artists.Find(id);
            foreach (var v in _context.MoviesArtists) {
                if (v.ArtistId == id) {
                    _context.MoviesArtists.Remove(v);
                }
            }
            _context.Artists.Remove(artist);
            _context.SaveChanges();
            _logger.LogError($"Movie with id {artist.Id} has been deleted!");
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistExists(int id)
        {
            return _context.Artists.Any(e => e.Id == id);
        }
    }
}