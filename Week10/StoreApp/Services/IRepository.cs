using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Services
{
    public interface IRepository<TDto, in TCreateDto, in TUpdateDto>
    {
        IEnumerable<TDto> All { get; }

        ActionResult<TDto> this[int id] { get; }

        ActionResult<TDto> Add(TCreateDto dto);

        ActionResult<TDto> Update(TUpdateDto dto);

        ActionResult Delete(int id);
    }
}
