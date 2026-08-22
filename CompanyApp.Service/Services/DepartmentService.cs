using AutoMapper;
using CompanyApp.Domain.Entities;
using CompanyApp.Domain.Enums;
using CompanyApp.Repository.Interfaces;
using CompanyApp.Service.DTOs;
using CompanyApp.Service.DTOs.Department_DTOs;
using CompanyApp.Service.Exceptions;
using CompanyApp.Service.Helpers.Constants;
using CompanyApp.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyApp.Service.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repository;
    private readonly IMapper _mapper;

    public DepartmentService(IDepartmentRepository repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<DepartmentDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync() ?? new List<Department>();
        return _mapper.Map<List<DepartmentDto>>(entities);
    }

    public async Task<DepartmentDto> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ValidationException(Messages.IdMustBeGreaterThanZero);

        var entity = await _repository.GetByIdAsync(id)
            ?? throw new NotFoundException(string.Format(Messages.DepartmentNotFound, id));

        return _mapper.Map<DepartmentDto>(entity);
    }

    public async Task CreateAsync(DepartmentCreateDto dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.Name))
            throw new ValidationException(Messages.DepartmentNameRequired);

        if (dto.Capacity < 0)
            throw new ValidationException(Messages.CapacityCantBeNegative);

        var department = _mapper.Map<Department>(dto);
        await _repository.CreateAsync(department);
    }

    public async Task EditAsync(DepartmentEditDto dto)
    {
        if (dto == null || dto.Id <= 0)
            throw new ValidationException(Messages.IdMustBeGreaterThanZero);

        var existing = await _repository.GetByIdAsync(dto.Id);
        if (existing == null)
            throw new NotFoundException(string.Format(Messages.DepartmentNotFound, dto.Id));

        existing.Name = string.IsNullOrWhiteSpace(dto.Name) ? existing.Name : dto.Name;
        existing.Capacity = dto.Capacity ?? existing.Capacity;

        await _repository.EditAsync(existing);
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0)
            throw new ValidationException(Messages.IdMustBeGreaterThanZero);

        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            throw new NotFoundException(string.Format(Messages.DepartmentNotFound, id));

        await _repository.DeleteAsync(id);
    }

    public async Task<List<DepartmentDto>> SearchByNameAsync(string text)
    {
        var entities = string.IsNullOrWhiteSpace(text)
            ? await _repository.GetAllAsync()
            : await _repository.SearchByNameAsync(text);

        return _mapper.Map<List<DepartmentDto>>(entities ?? new List<Department>());
    }

    public async Task<List<DepartmentDto>> GetSortedByCapacityAsync(SortDirection direction)
    {
        var entities = await _repository.GetSortedByCapacityAsync(direction) ?? new List<Department>();
        return _mapper.Map<List<DepartmentDto>>(entities);
    }
}