using Biblioteca.Data;
using Biblioteca.Domain.Model;
using Biblioteca.DTOs;

namespace Biblioteca.Domain.Services
{
    public class EditorialService
    {
        private readonly EditorialRepository _editorialRepository;

        public EditorialService(EditorialRepository editorialRepository)
        {
            _editorialRepository = editorialRepository;
        }

        public EditorialDto Add(CrearEditorialDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var editorial = new Editorial(0, dto.Nombre);
            
            _editorialRepository.Add(editorial);
            _editorialRepository.SaveChanges();

            var saved = _editorialRepository.GetAll().LastOrDefault(e => string.Equals(e.Nombre, dto.Nombre, StringComparison.Ordinal));

            return new EditorialDto
            {
                Id = saved?.Id ?? editorial.Id,
                Nombre = editorial.Nombre
            };
        }

        public bool Delete(int id)
        {
            try
            {
                var result = _editorialRepository.Delete(id);
                if (result)
                    _editorialRepository.SaveChanges();
                return result;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
        }

        public EditorialDto? Get(int id)
        {
            var editorial = _editorialRepository.Get(id);
            if (editorial == null)
                return null;

            return new EditorialDto
            {
                Id = editorial.Id,
                Nombre = editorial.Nombre
            };
        }

        public IEnumerable<EditorialDto> GetAll()
        {
            var editoriales = _editorialRepository.GetAll();
            return editoriales.Select(e => new EditorialDto
            {
                Id = e.Id,
                Nombre = e.Nombre
            });
        }

        public bool Update(EditorialDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var editorial = new Editorial(dto.Id, dto.Nombre);
            
            var result = _editorialRepository.Update(editorial);
            if (result)
                _editorialRepository.SaveChanges();
            
            return result;
        }

        public IEnumerable<EditorialDto> GetByCriteria(BusquedaCriterioDto criterioDto)
        {
            var editoriales = _editorialRepository.GetByCriteria(criterioDto.Texto);
            return editoriales.Select(e => new EditorialDto
            {
                Id = e.Id,
                Nombre = e.Nombre
            });
        }
    }
}
