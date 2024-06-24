using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using UxDebt.Context;
using UxDebt.Entities;
using UxDebt.Models.Entities;
using UxDebt.Models.ViewModel;
using UxDebt.Services.Interfaces;

namespace UxDebt.Services
{
    public class TagService : ITagService
    {

        private readonly UxDebtContext _context;
        private readonly IMapper _mapper;

        public TagService(UxDebtContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AddTagToIssue(List<int> tagsId, int issueId)
        {
            try
            {
                // Obtener el issue
                var issue = await _context.Issues.FirstOrDefaultAsync(i => i.IssueId == issueId);
                if (issue == null)
                {
                    throw new Exception("Issue not found.");
                }


                // Obtener el tag
                var tags = _context.Tags.Where(t => tagsId.Contains(t.TagId));                
                if (tags != null)
                {
                    if(tags.Count() != tagsId.Count())
                        throw new Exception("At least one tag was not found.");
                }
                else
                    throw new Exception("No tags were found.");

                //busco los tags que no estan en el issue pero si en la lista
                var existingIssueTags = await _context.IssueTags
                    .Where(it => it.IssueId == issueId)
                    .ToListAsync();

                var tagsToRemove = existingIssueTags
                    .Where(it => !tagsId.Contains(it.TagId))
                    .ToList();

                _context.IssueTags.RemoveRange(tagsToRemove);

                // Verificar si la relación ya existe 
                foreach (int tagId in tagsId)
                {
                    //busco el tag en el issue
                    var existingIssueTag = await _context.IssueTags.AnyAsync(it => it.IssueId == issueId && it.TagId == tagId);

                    //si al rel NO existe la agrego
                    if (!existingIssueTag)
                    {

                        // Crear la relación en la tabla de unión
                        var issueTag = new IssueTag
                        {
                            IssueId = issueId,
                            TagId = tagId
                        };
                        _context.IssueTags.Add(issueTag);                        
                    }                    

                }

                // Guardar cambios
                return await _context.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> Create(TagViewModel tag)
        {
            try
            {
                var TagEF = _mapper.Map<Tag>(tag);

                _context.Add(TagEF);
                await _context.SaveChangesAsync();
                return TagEF.TagId; // Assuming IssueId is generated after save
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Tag> Get(int id)
        {
            try
            {
                return await _context.Tags.FirstOrDefaultAsync(x => x.TagId == id);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Tag>> GetAll()
        {
            try
            {
                return await _context.Tags.ToListAsync();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception(ex.Message);
            }
        }
    }
}
