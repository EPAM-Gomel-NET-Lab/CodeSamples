using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class Blogs
    {
        public string GetBlogs()
        {
            return "Here are blogs go";
        }
    }
}
