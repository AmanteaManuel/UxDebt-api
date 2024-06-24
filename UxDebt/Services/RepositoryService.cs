using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using UxDebt.Context;
using UxDebt.Entities;
using UxDebt.Models.Response.Dtos;
using UxDebt.Models.ViewModel;
using UxDebt.Services.Interfaces;

namespace UxDebt.Services
{
    public class RepositoryService : IRepositoryService
    {

        private readonly UxDebtContext _context;
        private readonly IMapper _mapper;

        public RepositoryService(UxDebtContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Create(RepositoryViewModel repository)
        {
            try
            {
                var RepositoryEF = _mapper.Map<Repository>(repository);

                _context.Add(RepositoryEF);
                await _context.SaveChangesAsync();
                return RepositoryEF.RepositoryId; // Asumiendo que el ID se genera después de guardar
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(int id, RepositoryViewModel repository)
        {
            if (id != repository.RepositoryId)
            {
                return false;
            }

            var RepositoryEF = _mapper.Map<Repository>(repository);

            try
            {
                _context.Entry(RepositoryEF).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            var resultado = await _context.Repositories.FirstOrDefaultAsync(x => x.RepositoryId == id);

            if (resultado == null)
            {
                return false;
            }

            try
            {
                _context.Repositories.Remove(resultado);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception(ex.Message);
            }
        }

        public async Task<Repository> Get(int id)
        {
            try
            {
                return await _context.Repositories.FirstOrDefaultAsync(x => x.RepositoryId == id);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception(ex.Message);
            }
        }

        public async Task<Repository> GetWithIssues(int id)
        {
            try
            {
                return _context.Repositories
                .Include(r => r.Issues)
                .FirstOrDefault(r => r.RepositoryId == id);

            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Repository>> GetAll()
        {
            try
            {
                return await _context.Repositories.ToListAsync();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception(ex.Message);
            }
        }

        public async Task<Repository> Get(string owner, string name)
        {
            try
            {
                return await _context.Repositories.FirstOrDefaultAsync(x => x.Name == name && x.Owner == owner);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception(ex.Message);
            }
        }        
    }
}
