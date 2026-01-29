using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using practiseapi.DAL.Interface;
using practiseapi.Data;
using practiseapi.DTO;
using practiseapi.Model;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace practiseapi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class apiController : ControllerBase
    {
        private readonly Iapiinterface _apiinterface;
        private readonly IConfiguration _config;
        private readonly ApiDbcontext _context;
        public apiController(Iapiinterface apiinterface, IConfiguration config, ApiDbcontext context)
        {
            _apiinterface = apiinterface;
            _config = config;
            _context = context;
        }

        #region login
        [AllowAnonymous] 
        [HttpPost]
        public async Task<IActionResult> loginprocess(logindto login)
        {
            var result = await _context.login.Where(x => x.username == login.username && x.password == login.password).FirstOrDefaultAsync();
            if (result == null)
                return Unauthorized("Invalid credentials");
                var token = Generatetoken(result);
            return Ok(new { token=token,role=result.Role });
           

        }

//LOGIN
// ↓
//Username + Password
// ↓
//JWT Token Created(with Role)   ✅ (Authentication data + Role)
// ↓
//Client stores token
// ↓
//Client sends token in Header
// ↓
//Authentication(401 if fail)   🔐 Is token valid?
// ↓
//Authorization(403 if role mismatch) 🔐 Is role allowed?
// ↓
//API ACCESS
        private string Generatetoken(login login)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
              new Claim(ClaimTypes.Name, login.username),
              new Claim(ClaimTypes.Role, login.Role)
             };
            var token = new JwtSecurityToken(_config["JWT:Issuer"], _config["JWT:Audience"], claims: claims,
                expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
        [Authorize(Roles ="Admin")]
        [HttpGet]
      
        public async Task<List<empreg>> GetAll()
        {

            //this is the changes
            //this is second change
            var result = await _apiinterface.Getallempreg();
            return result;
        }

        [HttpGet]
        public async Task<IActionResult> Getbyid1(int id)
        {
            var result = await _apiinterface.getbyid(id);
            if(result == null)
            {
                return BadRequest(new { message = "no id found" });
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> addorupd(empreg emp)
        {
            try
            {
                var result = await _apiinterface.addorupdate(emp);
                if (result == null)
                {
                    return BadRequest(new { message = "faileed add or update" });
                }
                return Ok(new { message = "succesfully added or updated" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server Error");
            }
        }

        [HttpDelete]

        public async Task<IActionResult> delete(int id)
        {
            var result = await _apiinterface.deletebyid(id);
            if (result == null)
            {
                return BadRequest(new { message = "no id found" });
            }
            return Ok(new { message = "delete succesfully" });
        }
    }

    }

