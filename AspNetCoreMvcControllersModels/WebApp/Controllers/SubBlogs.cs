using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("[controller]")]
    public class SubBlogs : Blogs
    {
        [HttpGet("list")]
        public string GetSubBlogs()
        {
            return "Here are sub blogs go";
        }
    }
}
