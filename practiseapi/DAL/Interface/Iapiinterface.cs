using practiseapi.DAL.Service;
using practiseapi.DTO;
using practiseapi.Model;

namespace practiseapi.DAL.Interface
{
    public interface Iapiinterface
    {
        Task<List<empreg>> Getallempreg();
        Task<empreg> getbyid(int id);
        Task<empreg> addorupdate(empreg emp);
        Task<empreg> addorupdatebydto(empregdto emp);
        Task<empreg> deletebyid(int id);
    }
}
