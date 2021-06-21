using CustomerCors.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmistServer.API
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class MISAEntityController<MISAEntity> : ControllerBase
    {
        #region Properties
        IBaseService<MISAEntity> _baseService;
        IBaseRepo<MISAEntity> _baseRepo;
        #endregion

        #region Instructor
        public MISAEntityController(IBaseService<MISAEntity> baseService,
        IBaseRepo<MISAEntity> baseRepo)
        {
            _baseService = baseService;
            _baseRepo = baseRepo;
        }
        #endregion
        #region 
        [HttpGet]
        public IActionResult Get()
        {
            var customers = _baseRepo.GetAll();
            return Ok(customers);
        }
        [HttpGet("{entityId}")]
        public IActionResult Get(Guid entityId)
        {
            var customer = _baseRepo.GetbyId(entityId);
            return Ok(customer);
        }
        [HttpPost("NumRecord")]
        public IActionResult Post([FromBody] string keyword)
        {
            var res = _baseRepo.getNumberRecord(keyword);
            return Ok(res);
        }

        [HttpPost("Find")]
        public IActionResult Post(int index, int number, [FromBody] string keyWord)
        {
            var res = _baseRepo.GetByCondition(index, number, keyWord);

            return Ok(res);
        }
        [HttpPost]
        public IActionResult Post(MISAEntity entity)
        {
            var res = _baseService.Insert(entity);
            if(res.isValid == false)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
        [HttpPut]
        public IActionResult Put(MISAEntity entity)
        {
            var res = _baseService.Update(entity);
            if (res.isValid == false)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
        [HttpDelete("{entityId}")]
        public IActionResult Delete(Guid entityId)
        {
            var res = _baseRepo.Delete(entityId);
            return Ok(res);

        }
        #endregion

    }
}
