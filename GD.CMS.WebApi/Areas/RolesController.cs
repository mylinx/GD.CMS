﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GD.CMS.WebApi.Areas
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    public class RolesController : ControllerBase
    {
         
    }
}
