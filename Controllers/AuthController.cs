using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using smr.Core.Repositories;
using smr.Core.Services;
using smr.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace smr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IuserService _userService;

        // קונסטרוקטור עם הוספת ה-IuserRepository
        public AuthController(IConfiguration configuration, IuserService userService)
        {
            _configuration = configuration;
            _userService = userService;  // השגת השירות כדי לעבוד עם מאגר היוזרים
        }

       

        // פעולה זו מבצעת התחברות עם שם משתמש וסיסמה ומחזירה את ה-JWT Token אם שם המשתמש והסיסמה נכונים
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
           
            var user = await _userService.GetUserByUserNameAsync(loginModel.UserName, loginModel.Password);

            // אם המשתמש קיים והסיסמה נכונה
            if (user != null)
            {
                // יצירת רשימה של תביעות (Claims) עם מידע על המשתמש
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.UserName),  // שם המשתמש
                    new Claim(ClaimTypes.Role, user.Role.ToString())  // role (משכיר או שוכר)
                };

                // הגדרת סוד מוצפן ופרטי החתימה של ה-JWT
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Key")));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                // הגדרת פרטי ה-JWT כולל תוקף
                var tokenOptions = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(6),  // הגבלת תוקף ל-6 דקות
                    signingCredentials: signinCredentials
                );

                // יצירת ה-JWT Token
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                // מחזירים את ה-token למשתמש
                return Ok(new { Token = tokenString,user });
            }

            // אם שם המשתמש או הסיסמה לא נכונים, מחזירים שגיאה
            return Unauthorized();
        }
    }
}
