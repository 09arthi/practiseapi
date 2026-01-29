using practiseapi.DAL.Interface;
using practiseapi.DTO;
using practiseapi.Model;
using practiseapi.Data;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
namespace practiseapi.DAL.Service
{
    public class ApiService:Iapiinterface
    {
        private readonly ApiDbcontext _dbcontext;

        private readonly IConfiguration _config;

        public ApiService( ApiDbcontext dbcontext, IConfiguration config)
        {

            _dbcontext = dbcontext;
            _config = config;
        }

        

        #region empreg

        public async Task<List<empreg>> Getallempreg()
        {
            var result = await _dbcontext.empreg.ToListAsync();
            return result;
        }

        public async Task<empreg> getbyid(int id )
        {
            var result= await _dbcontext.empreg.Where(x=>x.empid== id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<empreg> addorupdate(empreg emp)
        {
            var result = await _dbcontext.empreg.Where(x=>x.empid== emp.empid).FirstOrDefaultAsync();
            if(result== null)
            {
                result = new empreg();
                result.empid = emp.empid;
                result.empname = emp.empname;
                result.age = emp.age;
                result.city = emp.city;
                _dbcontext.empreg.Add(result);

            }
            else
            {
                result.empname = emp.empname;
                result.age = emp.age;
                result.city = emp.city;
                _dbcontext.empreg.Add(result);

            }
            await _dbcontext.SaveChangesAsync();
            return result;
        }

        public async Task<empreg> addorupdatebydto(empregdto emp)
        {
            var result = await _dbcontext.empreg.Where(x => x.empid == emp.empid).FirstOrDefaultAsync();
            if (result == null)
            {
                result = new empreg();
                result.empid = emp.empid;
                result.empname = emp.empname;
                result.age = emp.age;
                result.city = emp.city;
                _dbcontext.empreg.Add(result);

            }
            else
            {
                result.empname = emp.empname;
                result.age = emp.age;
                result.city = emp.city;
                _dbcontext.empreg.Add(result);

            }
            await _dbcontext.SaveChangesAsync();
            return result;
        }

        public async Task<empreg> deletebyid(int id)
        {
            var result = await _dbcontext.empreg.Where(x => x.empid == id).FirstOrDefaultAsync();
            _dbcontext.empreg.Remove(result);
            await _dbcontext.SaveChangesAsync();
            return result;
        }
        #endregion
    }
}
