using CustomerCors.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerCors.Entity;
namespace AmistServer.API
{
    
    public class DepartmentController : MISAEntityController<Department>
    {
        IBaseService<Department> _apartmentService;
        IBaseRepo<Department> _apartmentRepo;
        #region Instructor
        public DepartmentController(IBaseService<Department> apartmentService,
        IBaseRepo<Department> apartmentRepo):base(apartmentService,apartmentRepo)
        {
            _apartmentService = apartmentService;
            _apartmentRepo = apartmentRepo;
        }
        #endregion

    }
}
