using AmistServer.Object;
using CustomerCors.Entity;
using CustomerCors.ErrorMsg;
using CustomerCors.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerCors.Service
{
    public class EmployeeService :MISABaseService<Employee>, IEmployeeService
    {
        public IEmployeeRepo _icustomer;
        public IBaseRepo<Department> _idepartment;
        public EmployeeService(IEmployeeRepo _excute, IBaseRepo<Department> idepartment):base(_excute)
        {
            _icustomer = _excute;
            _idepartment = idepartment;
        }
        /// <summary>
        /// Hàm thực hiện gán giá trị DepartmentId cho một Employee.
        /// </summary>
        /// <param name="Employee" Employee cần gán giá trị></param>
        /// <param name="result" Kết quả nhận được></param>
        /// <returns></returns>
        protected override void CustomeValidate(Employee employee) 
        {
        }
    }
}
