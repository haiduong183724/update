using AmistServer.Object;
using CustomerCors.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using MySqlConnector;
using System.Data;
using System.Linq;
using CustomerCors.ErrorMsg;

namespace InterStructor.Service
{
    public class EmployeeRepo : MISABaseRepo<Employee>, IEmployeeRepo
    {
        #region Instructor
        public EmployeeRepo():base()
        {

        }

        public ServiceResult GetEmployeeByKey(string keyWord)
        {
            var sqlCommand = $"call Proc_GetEmployeesByKey(@keyword,1)";
            DynamicParameters param = new DynamicParameters();
            param.Add("@keyword", keyWord);
            var result = new ServiceResult(); 
            try 
            {
                var entities = this.Connect().Query<Employee>(sqlCommand, param: param).ToList();
                result.Data.Add(entities);
                result.ErrorMsg.Add("Lấy dữ liệu thành công!");
                return result;
            }
            catch (Exception e)
            {
                result.isValid = false;
                result.ErrorMsg.Add(e.Message);
                return result;
            }
        }

        public ServiceResult GetNewEmployeeCode()
        {
            var result = new ServiceResult();
            var sqlCommand = "CALL Proc_GetMaxEmployeeCode()";
            try
            {
                var res = this.Connect().QueryFirstOrDefault<string>(sqlCommand);
                var newIndex = (int.Parse(res.Substring(3)) + 1).ToString();
                var newEmployeeCode = newIndex.PadLeft(6,'0');
                result.Data.Add("NV-"+newEmployeeCode.ToString());
            }
            catch
            {
                result.Data.Add("NV-999999");
            }
            return result;
        }
        #endregion
        #region Excute Interface

        public ServiceResult GetPageTen(int index = 1)
        {
            if (index <= 0)
            {
                index = 1;
            }
            var start = (index-1) * 10;
            var sqlCommand = $"select * from {tableName} limit {start}, 10";
            var result = new ServiceResult();
            try
            {
                var entities = this.Connect().Query<Employee>(sqlCommand).ToList();
                result.Data.Add(entities);
                result.ErrorMsg.Add("Lấy dữ liệu thành công!");
            }
            catch (Exception e)
            {
                result.isValid = false;
                result.ErrorMsg.Add(e.Message);
            }
            return result;
        }

        public ServiceResult GetPageTwenty(int index = 1)
        {
            if (index <= 0)
            {
                index = 1;
            }
            var start = (index - 1) * 20;
            var sqlCommand = $"select * from {tableName} limit {start}, 20";
            var result = new ServiceResult();
            try
            {
                var entities = this.Connect().Query<Employee>(sqlCommand).ToList();
                result.Data.Add(entities);
                result.ErrorMsg.Add("Lấy dữ liệu thành công!");
            }
            catch (Exception e)
            {
                result.isValid = false;
                result.ErrorMsg.Add(e.Message);
            }
            return result;
        }

        public ServiceResult GetRecord(int index)
        {
            var sqlCommand = "call Proc_GetEmployeesRecord(@index)";
            DynamicParameters param = new DynamicParameters();
            var result = new ServiceResult();
            try
            {
                var employees = this.Connect().Query<Employee>(sqlCommand, param: param).ToList();
                result.Data.Add(employees);
                return result;
            }
            catch(Exception e)
            {
                result.isValid = false;
                result.ErrorMsg.Add(e.Message);
                return result;
            }
        }

        
        #endregion
    }
}
