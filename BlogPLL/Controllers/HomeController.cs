using System.Diagnostics;
using AutoMapper;
using BlogBLL.Repository;
using BlogBLL.UnitOfWork;
using BlogBLL.ViewModels.Post;
using Microsoft.AspNetCore.Mvc;
using BlogDAL.Models;


namespace BlogPLL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HomeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var repository = _unitOfWork.GetRepository<Post>() as PostRepository;
            var model = repository!.GetAllPosts();
            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
