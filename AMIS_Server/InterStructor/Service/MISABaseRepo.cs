using CustomerCors.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using MySqlConnector;
using System.Data;
using System.Linq;
using CustomerCors.ErrorMsg;
using CustomerCors.Entity;

namespace InterStructor.Service
{
    public class MISABaseRepo<MISAEntity> : IBaseRepo<MISAEntity>
    {
        #region Instructor
        protected string tableName;
        public MISABaseRepo()
        {
            tableName = typeof(MISAEntity).Name;
        }
        #endregion
        #region ConnectDataBase
        protected IDbConnection Connect()
        {
            var connectionString = "" +
                "host = 47.241.69.179;" +
                "port = 3306;" +
                "database = MF848_AMIST_NHDUONG;" +
                "user id = nvmanh;" +
                "password = 12345678;";

            return new MySqlConnection(connectionString);
        }
        #endregion
        public ServiceResult GetAll()
        {
            var result = new ServiceResult();
            var sqlCommand = $"call Proc_Get{tableName}s()";
            try 
            {
                var entities = this.Connect().Query<MISAEntity>(sqlCommand).ToList();
                result.Data.Add(entities);
                if(entities.Count == 0)
                {
                    result.ErrorMsg.Add("Không có dữ liệu!");
                }
                else 
                {
                    result.ErrorMsg.Add("Lấy dữ liệu thành công!");
                }
                
            }
            catch(Exception e)
            {
                result.isValid = false;
                result.ErrorMsg.Add(e.Message);
            }
            return result;
        }

        public ServiceResult GetbyId(Guid entityId)
        {
            var sqlCommand = $"call Proc_Get{tableName}ById(@entityId)";
            DynamicParameters param = new DynamicParameters();
            param.Add("@entityId", entityId);
            var result = new ServiceResult();
            try
            {
                var entity = this.Connect().QueryFirstOrDefault<MISAEntity>(sqlCommand, param: param);
                if(entity == null)
                {
                    result.ErrorMsg.Add($"{tableName}Id: {entityId} không tồn tại trong hệ thống!");
                }
                else 
                {
                    result.ErrorMsg.Add("Lấy dữ liệu thành công!");
                    result.Data.Add(entity);
                }
                
            }
            catch (Exception e)
            {
                result.isValid = false;
                result.ErrorMsg.Add(e.Message);
            }
            return result;
        }

        public ServiceResult Insert(MISAEntity entity)
        {
            var sqlCommandField = "";
            var sqlCommandValue = "";
            var properties = entity.GetType().GetProperties();
            DynamicParameters _param = new DynamicParameters();
            int i = 0;
            foreach (var pro in properties)
            {
                i++;
                var propertiesRequired = pro.GetCustomAttributes(typeof(MISANoDB), true);
                if (propertiesRequired.Length == 0) 
                {
                    var propName = pro.Name;
                    if (i == properties.Length)
                    {
                        sqlCommandField += $"{propName}";
                        sqlCommandValue += $"@{propName}";
                    }
                    else
                    {
                        sqlCommandField += $"{propName},";
                        sqlCommandValue += $"@{propName},";
                    }
                }
            }
            var sqlCommand = $"insert into {tableName} ({sqlCommandField}) values ({sqlCommandValue})";
            foreach (var pro in properties)
            {
                var propName = pro.Name;
                var propValue = pro.GetValue(entity);
                if (propName == $"{tableName}Id")
                {
                    propValue = Guid.NewGuid().ToString();
                }
                _param.Add($"@{propName}", propValue);
            }
            var result = new ServiceResult();
            try
            {
                var res = this.Connect().Execute(sqlCommand, param: _param);
                result.Data.Add(res);
                result.ErrorMsg.Add("Thêm dữ liệu thành công!");
            }
            catch (Exception e)
            {
                result.isValid = false;
                result.ErrorMsg.Add(e.Message);
            }
            return result;
        }

        public ServiceResult Update(MISAEntity entity)
        {
            var sqlCommandValue = "";
            var properties = entity.GetType().GetProperties();
            var condition = "";
            DynamicParameters param = new DynamicParameters();
            
            var i = 0;
            foreach (var prop in properties)
            {
                
                var propName = prop.Name;
                var propValue = prop.GetValue(entity);
                var propertiesRequired = prop.GetCustomAttributes(typeof(MISANoDB), true);
                if(propertiesRequired.Length == 0) 
                {
                    if (i == 0)
                    {
                        condition = $"{propName} = @{propName}";
                    }
                    else if (i == properties.Length - 1)
                    {
                        sqlCommandValue += $"{propName} = @{propName}";
                    }
                    else
                    {

                        sqlCommandValue += $"{propName} = @{propName},";
                    }
                }
                i++;

            }
            // Khởi tạo câu lệnh truy vấn
            var sqlCommand = $"UPDATE {tableName} SET {sqlCommandValue} WHERE {condition}";
            foreach (var pro in properties)
            {
                var propName = pro.Name;
                var propValue = pro.GetValue(entity);
                param.Add($"@{propName}", propValue);
            }
            var result = new ServiceResult();
            // Request tới server
            try
            {
                var res = this.Connect().Execute(sqlCommand, param: param);
                result.Data.Add(res);
                result.ErrorMsg.Add("Chỉnh sửa thành công!");
            }
            catch (Exception e)
            {
                result.isValid = false;
                result.ErrorMsg.Add(e.Message);
            }
            return result;
        }
        public ServiceResult Delete(Guid entityId)
        {
            var sqlCommand = $"call Proc_Delete{tableName}ById(@entityId)";
            DynamicParameters param = new DynamicParameters();
            param.Add("@entityId", entityId);
            var result = new ServiceResult();
            try
            {
                var rowEffect = this.Connect().Execute(sqlCommand, param: param);
                result.Data.Add(rowEffect);
                if(rowEffect == 0)
                {
                    result.ErrorMsg.Add($"{tableName}Id: {entityId} không tồn tại trong hệ thống!");
                }
                else
                {
                    result.ErrorMsg.Add("Xóa dữ liệu thành công!");
                }
                
            }
            catch (Exception e)
            {
                result.isValid = false;
                result.ErrorMsg.Add(e.Message);
            }
            return result;
        }

        public ServiceResult GetAllCode()
        {
            var sqlCommand = $"SELECT {tableName}Code from {tableName}";
            var result = new ServiceResult();
            try 
            {
                var res = this.Connect().Query<string>(sqlCommand).ToList();
                result.Data.Add(res);
            }
            catch(Exception e)
            {
                result.isValid = false;
                result.ErrorMsg.Add(e.Message);
            }
            return result;
        }

        public ServiceResult GetByCode(string entityCode)
        {
            var sqlCommand = $"select * from {tableName} WHERE {tableName}Code = @entityCode";
            DynamicParameters param = new DynamicParameters();
            param.Add("@entityCode", entityCode);
            var result = new ServiceResult();
            try
            {
                var entity = this.Connect().QueryFirstOrDefault<MISAEntity>(sqlCommand, param: param);
                result.Data.Add(entity);
                result.ErrorMsg.Add("Lấy dữ liệu thành công!");
            }
            catch (Exception e)
            {
                result.isValid = false;
                result.ErrorMsg.Add(e.Message);
            }
            return result;
        }

        public int getNumberRecord(string condition)
        {
            var sqlcommand = $"call Proc_Get{tableName}NumberRecord(@condition)";
            DynamicParameters param = new DynamicParameters();
            param.Add("@condition", condition);
            try
            {
                var res = this.Connect().QueryFirstOrDefault<int>(sqlcommand,param:param);
                return res;
            }
            catch
            {
                return 0;
            }
        }

        public ServiceResult GetByCondition(int index, int number, string condition)
        {
            
            var sqlCommand = $"call Proc_Get{tableName}sByKW(@st, @numrecord, @kw)";
            DynamicParameters param = new DynamicParameters();
            var rs = new ServiceResult();
            param.Add("@st", (index-1)*number);
            param.Add("@numrecord", number);
            param.Add("@kw", condition);
            try
            {
                var res = this.Connect().Query<MISAEntity>(sqlCommand, param: param).ToList();
                rs.Data.Add(res);
                return rs;
            }
            catch
            {
                rs.isValid = false;
                return rs;
            }
        }

        public bool CheckCodeExits(string code)
        {
            string sqlcommand = $"call Proc_Check{tableName}CodeExist(@Code)";
            DynamicParameters param = new DynamicParameters();
            param.Add("@Code", code);
            try 
            {
                var res = this.Connect().QueryFirstOrDefault<bool>(sqlcommand, param: param);
                return res;
            }
            catch
            {
                return false;
            }
        }
    }
}
