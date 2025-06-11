using Microsoft.AspNetCore.Mvc;

namespace OnlineMuhasebeServer.Presentation.Abstraction
{
    //[ApiController]
    //[Route("api/[controller]")]
    //public abstract class APIController : ControllerBase
    //{
    //} 
    //Bu genel kullanılabilecek bir controller yapısıdır. 
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController:ControllerBase
    {
    }
}
