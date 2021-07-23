using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChallengeNivelatorio.Models;

namespace ChallengeNivelatorio.Controllers
{
    public class BlogsController : Controller
    {
        private readonly ChallengeBlogContext _context;

        public BlogsController(ChallengeBlogContext context)
        {
            _context = context;
        }

        // GET: Blogs
        public async Task<IActionResult> Index()
        {
            var list = await _context.Blog.OrderByDescending(x => x.CreationDate).ToListAsync();
            return View(list);
        }

        // GET: Home
        public async Task<IActionResult> Home()
        {
            var list = await _context.Blog.OrderByDescending(x => x.CreationDate).ToListAsync();
            return View(list);
        }

        // GET: Blogs/SearchBlog
        public async Task<IActionResult> SearchBlog()
        {
            return View();
        }

        // POST: Blogs/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            var search = await _context.Blog.Where(x => x.Title.Contains(SearchPhrase)).ToListAsync();
            var view = View("Home", await _context.Blog.Where(x => x.Title.Contains(SearchPhrase)).ToListAsync());

            if (SearchPhrase == null)
            {                
                return BadRequest();
            }
            else if (search.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return (view);
            }
           
        }

        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blog
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: Blogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blogs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create([Bind("BlogId,Title,Contents,Image,Category,CreationDate")] Blog blog)
        {
            var b = new Blog
            {
                Title = blog.Title,
                Contents = blog.Contents
            };

            if (b.Title != null & b.Contents != null)
            {
                _context.Add(b);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Home));
            }
            else
            {
                return BadRequest();
            }            
        }

        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blog.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogId,Title,Contents,Image,Category,CreationDate")] Blog blog)
        {
            if (id != blog.BlogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.BlogId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Home));
            }
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blog
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blog.FindAsync(id);
            _context.Blog.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Home));
        }       

        // GET: Blogs/SearchEdit
        public async Task<IActionResult> SearchEdit()
        {
            return View();
        }


        // POST: Blogs/ShowEditResults
        public async Task<IActionResult> ShowEditResults(int SearchID)
        {
            var search = await _context.Blog.Where(x => x.BlogId.Equals(SearchID)).ToListAsync();
            var view = View("Home", await _context.Blog.Where(x => x.BlogId.Equals(SearchID)).ToListAsync());

            if (SearchID == 0)
            {
                return View("Error");
            }
            else if (search.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return (view);
            }
        }

        private bool BlogExists(int id)
        {
            return _context.Blog.Any(e => e.BlogId == id);
        }
    }
}
