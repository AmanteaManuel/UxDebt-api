using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<int> AddTagToIssue(string codeTag, int issueId)
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
                var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Code == codeTag);
                if (tag == null)
                {
                    throw new Exception("Tag not found.");
                }

                // Verificar si la relación ya existe
                var existingIssueTag = await _context.IssueTags.FirstOrDefaultAsync(it => it.IssueId == issueId && it.TagId == tag.TagId);
                if (existingIssueTag != null)
                {
                    return -1; // La relación ya existe
                }

                // Crear la relación en la tabla de unión
                var issueTag = new IssueTag
                {
                    IssueId = issueId,
                    TagId = tag.TagId
                };
                _context.IssueTags.Add(issueTag);

                // Guardar cambios
                await _context.SaveChangesAsync();
                return issueTag.IssueTagId;
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
