using AutoMapper;
using CompanyApp.Domain.Entities;
using CompanyApp.Repository.Interfaces;
using CompanyApp.Service.DTOs;
using CompanyApp.Service.DTOs.Employee_DTOs;
using CompanyApp.Service.Exceptions;
using CompanyApp.Service.Helpers.Constants;
using CompanyApp.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyApp.Service.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;
    private readonly IDepartmentRepository _deptRepository;
    private readonly IMapper _mapper;

    public EmployeeService(IEmployeeRepository repository, IDepartmentRepository deptRepository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _deptRepository = deptRepository ?? throw new ArgumentNullException(nameof(deptRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<EmployeeDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync() ?? new List<Employee>();
        return _mapper.Map<List<EmployeeDto>>(entities);
    }

    public async Task<EmployeeDto> GetByIdAsync(int id)
    {
        if (id <= 0) throw new ValidationException(Messages.IdMustBeGreaterThanZero);

        var entity = await _repository.GetByIdAsync(id)
            ?? throw new NotFoundException(string.Format(Messages.EmployeeNotFound, id));

        return _mapper.Map<EmployeeDto>(entity);
    }

    public async Task CreateAsync(EmployeeCreateDto dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Surname))
            throw new ValidationException(Messages.EmployeeNameSurnameRequired);

        if (dto.Name.Any(char.IsDigit) || dto.Surname.Any(char.IsDigit))
            throw new ValidationException(Messages.EmployeeNameSurnameNoDigits);

        if (dto.Age < 18 || dto.Age > 65)
            throw new ValidationException(Messages.EmployeeAgeRestriction);

        var employee = _mapper.Map<Employee>(dto);

        if (dto.DepartmentId.HasValue && dto.DepartmentId.Value > 0)
        {
            var dept = await _deptRepository.GetByIdAsync(dto.DepartmentId.Value);
            employee.Department = dept!;
        }

        await _repository.CreateAsync(employee);
    }

    public async Task EditAsync(EmployeeEditDto dto)
    {
        if (dto == null || dto.Id <= 0)
            throw new ValidationException(Messages.IdMustBeGreaterThanZero);

        var existing = await _repository.GetByIdAsync(dto.Id);
        if (existing == null)
            throw new NotFoundException(string.Format(Messages.EmployeeNotFound, dto.Id));

        string name = string.IsNullOrWhiteSpace(dto.Name) ? existing.Name : dto.Name;
        string surname = string.IsNullOrWhiteSpace(dto.Surname) ? existing.Surname : dto.Surname;

        if (name.Any(char.IsDigit) || surname.Any(char.IsDigit))
            throw new ValidationException(Messages.EmployeeNameSurnameNoDigits);

        int age = dto.Age <= 0 ? existing.Age : dto.Age;
        if (age < 18 || age > 65)
            throw new ValidationException(Messages.EmployeeAgeRestriction);

        existing.Name = name;
        existing.Surname = surname;
        existing.Age = age;
        existing.Address = string.IsNullOrWhiteSpace(dto.Address) ? existing.Address : dto.Address;

        if (dto.DepartmentId.HasValue)
        {
            existing.Department = dto.DepartmentId.Value > 0
                ? await _deptRepository.GetByIdAsync(dto.DepartmentId.Value)
                : null!;
        }

        await _repository.EditAsync(existing);
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0) throw new ValidationException(Messages.IdMustBeGreaterThanZero);
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null) throw new NotFoundException(string.Format(Messages.EmployeeNotFound, id));

        await _repository.DeleteAsync(id);
    }

    public async Task<List<EmployeeDto>> GetByAgeAsync(int age) =>
        _mapper.Map<List<EmployeeDto>>(await _repository.GetByAgeAsync(age) ?? new List<Employee>());

    public async Task<List<EmployeeDto>> GetByDepartmentIdAsync(int departmentId) =>
        _mapper.Map<List<EmployeeDto>>(await _repository.GetByDepartmentIdAsync(departmentId) ?? new List<Employee>());

    public async Task<List<EmployeeDto>> SearchByNameOrSurnameAsync(string text) =>
        _mapper.Map<List<EmployeeDto>>(await _repository.SearchByNameOrSurnameAsync(text) ?? new List<Employee>());

    public async Task<int> GetEmployeeCountAsync() => await _repository.GetEmployeeCountAsync();

    public async Task<List<EmployeeDto>> GetAllEmpByDepNameAsync(string departmentName)
    {
        if (string.IsNullOrWhiteSpace(departmentName)) return new List<EmployeeDto>();
        return _mapper.Map<List<EmployeeDto>>(await _repository.GetByDepartmentNameAsync(departmentName) ?? new List<Employee>());
    }
}