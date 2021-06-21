using AmistServer.Object;
using CustomerCors.ErrorMsg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerCors.Interface
{
    public interface IEmployeeRepo:IBaseRepo<Employee>
    {
        /// <summary>
        /// Hàm lấy danh sách nhân viên theo số lượng 10 người / 1
        /// </summary>
        /// <param name="index"> chỉ số lấy từ (index-1)*10+1 -> index*10</param>
        /// <returns> 10 bản ghi</returns>
        /// CreateBy NHDUONG 29/5
        public ServiceResult GetPageTen(int index);
        /// <summary>
        /// Hàm lấy danh sách nhân viên theo số lượng 20 người / 1
        /// </summary>
        /// <param name="index">chỉ số lấy từ (index-1)*20+1 -> index*20</param>
        /// <returns>20 bản ghi</returns>
        /// CreateBy NHDUONG 29/5
        public ServiceResult GetPageTwenty(int index);
        /// <summary>
        /// Hàm lấy mã nhân viên mới 
        /// </summary>
        /// <returns> ServiceResult chứa mã nhân viên mới</returns>
        public ServiceResult GetNewEmployeeCode();
        /// <summary>
        /// Hàm tìm kiếm nhân viên theo mã, tên SDT 
        /// </summary>
        /// <param name="keyWord"> Từ khóa tìm kiếm</param>
        /// <returns> danh sách các nhân viên</returns>
        public ServiceResult GetEmployeeByKey(string keyWord);
        /// <summary>
        /// Lấy 100 bản ghi 1 lần
        /// </summary>
        /// <param name="index"> thứ tự của bản ghi</param>
        /// <returns>100 bản ghi </returns>
        public ServiceResult GetRecord(int index);
    }
}
