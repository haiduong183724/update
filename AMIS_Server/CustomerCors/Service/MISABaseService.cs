using CustomerCors.Entity;
using CustomerCors.ErrorMsg;
using CustomerCors.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerCors.Service
{
    public class MISABaseService<MISAEntity> : IBaseService<MISAEntity>
    {
        IBaseRepo<MISAEntity> _baseRepo;
        #region Instruction
        public MISABaseService(IBaseRepo<MISAEntity> baseRepo)
        {
            _baseRepo = baseRepo;
        }

        #endregion

        #region ExcuteInterface
        public ServiceResult Insert(MISAEntity entity)
        {
            ServiceResult result = new ServiceResult();
            ValidateData(entity, result);
            if (!result.isValid)
            {
                return result;
            }
            else
            {
                var properties = entity.GetType().GetProperties();
                var entityCode = "";
                foreach (var prop in properties)
                {
                    var propertiesCode = prop.GetCustomAttributes(typeof(MISAEntityCode), true);
                    if (propertiesCode.Length > 0)
                    {
                        entityCode = (string)prop.GetValue(entity);
                        var modifiedCode = entityCode.Substring(3).PadLeft(6, '0');
                        prop.SetValue(entity, "NV-"+ modifiedCode);
                    }
                    if(prop.Name == "CreatedDate")
                    {
                        prop.SetValue(entity, DateTime.Now);
                    }
                }
                entityCode = entityCode.Substring(3);
                entityCode = entityCode.PadLeft(6, '0');
                var res = _baseRepo.CheckCodeExits("NV-"+entityCode);
                if(res == true)
                {
                    result.isValid = false;
                    result.ErrorMsg.Add($"Mã nhân viên {entityCode} đã tồn tại trong hệ thống! Hãy chọn mã nhân viên khác");
                    return result;
                }
                CustomeValidate(entity);
                return _baseRepo.Insert(entity);
            }
        }

        public ServiceResult Update(MISAEntity entity)
        {
            ServiceResult result = new ServiceResult();
            ValidateData(entity, result);
            if (!result.isValid)
            {
                return result;
            }
            else
            {
                // Kiểm tra mã của đối tượng
                // Lấy đối tượng trong database;

                
                var properties = entity.GetType().GetProperties();
                var entityCode = "";
                var entityDBcode = "";
                var entityId = new Guid();
                foreach (var prop in properties)
                {
                    var propertiesCode = prop.GetCustomAttributes(typeof(MISAEntityId), true);
                    if (propertiesCode.Length > 0)
                    {
                        entityId = (Guid)prop.GetValue(entity);
                    }
                }
                var entityDB = _baseRepo.GetbyId(entityId);
                if (entityDB.Data.Count == 0)
                {
                    return Insert(entity);
                }
                var entityInDB = entityDB.Data[0];
                foreach (var prop in properties)
                {
                    var propertiesCode = prop.GetCustomAttributes(typeof(MISAEntityCode), true);
                    if (propertiesCode.Length > 0)
                    {
                        entityCode = (string)prop.GetValue(entity);
                        var modifiedCode = entityCode.Substring(3).PadLeft(6, '0');
                        prop.SetValue(entity, "NV-" +modifiedCode);
                        entityDBcode = (string)prop.GetValue(entityInDB);
                    }
                    if (prop.Name == "ModifiedDate")
                    {
                        prop.SetValue(entity, DateTime.Now);
                    }
                }
                var res = false;
                // Kiểm tra mã nhân viên có bị thay đổi hay không
                if(entityDBcode != entityCode)
                {
                    var modifiedCode = entityCode.Substring(3).PadLeft(6, '0');
                    res = _baseRepo.CheckCodeExits("NV-"+modifiedCode);
                }
                if (res == true)
                {
                    result.isValid = false;
                    result.ErrorMsg.Add($"Mã nhân viên {entityCode} đã tồn tại trong hệ thống! Hãy chọn mã nhân viên khác");
                    return result;
                }
                //CustomeValidate(entity);
                if (result.isValid == false)
                {
                    return result;
                }
                CustomeValidate(entity);
                return _baseRepo.Update(entity);
            }
            
        }
        #endregion
        /// <summary>
        /// hàm validate dữ liệu.
        /// </summary>
        protected void ValidateData(MISAEntity entity, ServiceResult result) {
            var properties = entity.GetType().GetProperties();
            // Duyệt các thuộc tính của Entity
            foreach(var prop in properties)
            {
                var propValue = prop.GetValue(entity);
                var propName = prop.Name;
                // Kiểm tra xem chiều dài có lớn hơn 50 kí tự hay không
                if (propValue != null && propValue.ToString().Length > 50)
                {
                    result.isValid = false;
                    result.ErrorMsg.Add($"{propName} vượt quá kí tự cho phép");
                }
                // Kiểm tra giá trị tiền có âm hay không
                if(propValue != null && propValue.GetType() == typeof(double))
                {
                    if((double)propValue < 0)
                    {
                        result.isValid = false;
                        result.ErrorMsg.Add($"{propName} số tiền phải là số dương");
                    }
                }
                // Kiểm tra thời gian trong phạm vi 1/1/2001 => 1/1/2025
                if (propValue != null && propValue.GetType() == typeof(DateTime))
                {
                    // chuyển từ kiểu datetime sang int : Năm*512 + tháng *64 + ngày
                    var minDate = 1900 * 512 + 1 * 64 + 1;
                    var maxDate = 2050 * 512 + 1 * 64 + 1;
                    // Lấy giá trị datetime nhận được
                    DateTime date = (DateTime)propValue;
                    var DateValue = date.Year * 512 + date.Month * 64 + date.Day;
                    if(DateValue>maxDate || DateValue< minDate)
                    {
                        result.isValid = false;
                        result.ErrorMsg.Add($"{propName}: Ngày nhập phải trong khoảng 1/1/1900 đến 1/1/2050");
                    }
                }
                // Kiểm tra các trường bắt buộc nhập
                var propertiesRequired = prop.GetCustomAttributes(typeof(MISARequired), true);
                if (propertiesRequired.Length > 0)
                {
                    if(propValue == null || propName.ToString() == string.Empty)
                    {
                        result.ErrorMsg.Add($"{propName} không được để trống");
                    }
                }
            }
        }

        protected virtual void CustomeValidate(MISAEntity entity)
        {
        }
    }
}
