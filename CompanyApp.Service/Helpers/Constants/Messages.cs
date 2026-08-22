namespace CompanyApp.Service.Helpers.Constants
{
    public static class Messages
    {
        public const string CapacityCantBeNegative = "Capacity can't be negative";

        public const string IdMustBeGreaterThanZero = "Id must be greater than zero.";
        public const string EntityNotFound = "Entity of type {0} with id {1} not found.";
        public const string DepartmentNotFound = "Department with id {0} not found.";
        public const string DepartmentMustNotBeNull = "Department must not be null.";
        public const string DontHaveAnyDepartments = "There are no departments.";
        public const string EmployeeNotFound = "Employee with id {0} not found.";
        public const string DepartmentNameRequired = "Department name must be provided.";
        public const string SameDepartmentName = "The name of department can't be same";
        public const string DepartmentNameNoDigits = "Department name must not contain digits.";
        public const string DepartmentCapacityInvalidNumber = "Department capacity must be a non-negative integer.";
        public const string DepartmentCapacityNonNegative = "Department capacity must be non-negative.";
        public const string EmployeeAgeCantBeNegative = "Employee age can't be negative";
        public const string EmployeeAgeRestriction = "The age does not meet the criteria.";
        public const string EmployeeNameSurnameRequired = "Employee name and surname must be provided.";
        public const string EmployeeNameSurnameNoDigits = "Employee name and surname must not contain digits.";
        public const string OrderMustBeAscOrDesc = "Order must be 'asc' or 'desc'.";
        public const string OperationNotSupportedForType = "Operation is not supported for type {0}.";
    }
}
