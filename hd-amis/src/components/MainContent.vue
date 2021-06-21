<template>
  <div class="content d-flex" @keyup.enter="findKeyWord()">   
      <div class="action" :class ="{'expand_more':expandShow}" ref="expand" v-bind:style="{left:xClick, top:yClick}">
        <p v-on:click ="deleteEmployee()" >Xóa</p>
        <p v-on:click ="clone()">Nhân bản</p>
      </div>
      <div class="header-content" :style="{top:`${headPosition}px`}">
        <div class="top-header">
          <h2>Nhân viên</h2>
          <div class="right-header">
              <a href="#" class="white-bg">
                Tiện ích
                <img src="../assets/icon/dropdown.svg" alt="">
              </a>
              <div class="add-btn">
                <a href="#" v-on:click="add()" style="color:white; padding-right:0">
                <div style="padding-right:16px; border-right:1px solid #ffffff">Thêm</div>
                </a>
                <a href="#" style="width:46px" data-title = "Nhập từ excel">
                  <img src="../assets/icon/white-drop-down.svg" alt="">
                </a>
              </div>    
          </div>
        </div>
        <div class="bottom-header">
          <a href="#" >
            <img src="../assets/icon/left_arr.svg" alt="">
            Tất cả danh mục
          </a>
        </div>
      </div>
      <div class="table-content" ref="scrollcontent" @scroll="handlescroll()">

        <div class="table-header">
            <a href="#">
              Thực hiện hàng loạt
               <img src="../assets/icon/dropdown.svg" alt="">
            </a>
            <div class="right-table-header item-center">
              <div class="filter-input item-center">
                <input type="text" placeholder="Tìm kiếm theo mã, tên nhân viên" ref="findEmploy" class="italic-placeholder"> 
                <img src="../assets/icon/search.svg" alt="" @click="findKeyWord()">
              </div>
              <div data-title = "Lấy lại dữ liệu">
                <img src="../assets/icon/refresh.svg" alt="" @click="reLoadData()">
              </div>
              <div data-title = "Xuất ra excel">
                <img src="../assets/icon/excel.svg" alt="" @click="loadAllData()">
              </div>
            </div>
        </div>
        <div class="table-wraper">
          <table ref = "table">
          <thead>
            <tr>
            <th class="table-outline left"></th>
            <th class = "static">
              <span>
                <input type="checkbox">
              </span>
            </th>
            <div class="head-title">
            <th class="small-td">MÃ NHÂN VIÊN</th>
            <th class="big-td">TÊN NHÂN VIÊN</th>
            <th class="tiny-td">GIỚI TÍNH</th>
            <th class="center small-td ">NGÀY SINH</th>
            <th class="nor-td">SỐ CMND</th>
            <th class="big-td">CHỨC DANH</th>
            <th class="big-td">TÊN ĐƠN VỊ</th>
            <th class="small-td">SỐ TÀI KHOẢN</th>
            <th class="big-td">TÊN NGÂN HÀNG</th>
            <th class="big-td">CHI NHÁNH TK NGÂN HÀNG</th>
            </div>
            <th class="static-right tiny-td">
              <span>CHỨC NĂNG</span>   
            </th>
            <th class="table-outline right"></th>
            </tr>
          </thead>
          <tbody style="display:contents">
            <tr v-for="(item, index) in employees" :key="item.employeeId" 
            :id = "index"  v-on:click="initrowSelected(index)" @dblclick="edit(index)">
              <td class="table-outline left"></td>
              <td class = "static" :class ="{'rowSelected': rowSelected == index}"><input type="checkbox" ></td>
              <div class="row_content">
                <td class="small-td" :class ="{'rowSelected': rowSelected == index}">{{item.employeeCode}}</td>
                <td class="big-td" :class ="{'rowSelected': rowSelected == index}">{{item.employeeName}}</td>
                <td class="tiny-td" :class ="{'rowSelected': rowSelected == index}">{{fomatGender(item.gender)}}</td>
                <td class="small-td center" :class ="{'rowSelected': rowSelected == index}">{{fomatDate(item.dateOfBirth)}}</td>
                <td class="nor-td" :class ="{'rowSelected': rowSelected == index}">{{item.identityNumber}}</td>
                <td class="big-td" :class ="{'rowSelected': rowSelected == index}">{{item.employeePosition}}</td>
                <td class="big-td" :class ="{'rowSelected': rowSelected == index}">{{item.departmentName}}</td>
                <td class="small-td" :class ="{'rowSelected': rowSelected == index}">{{item.bankAccountNumber}}</td>
                <td class="big-td" :class ="{'rowSelected': rowSelected == index}">{{item.bankName}}</td>
                <td class="big-td" :class ="{'rowSelected': rowSelected == index}">{{item.bankBranchName}}</td>
              </div>
              <td class="static-right" :class ="{'rowSelected': rowSelected == index}"  >
                <div class="item-center d-right">
                  <div class="more_action">
                    <img src="../assets/icon/blue_drop.svg" alt="" v-on:click ="function(event){openMoreSelect(event,index)}">
                  </div>
                  <span v-on:click="edit(index)" style = "color:#0075c0" >Sửa</span>
                </div>
              </td>
              <td class="table-outline right"></td>
            </tr>
          </tbody>
        </table>
        </div>
        <div class="main-footer">
          <p class="margin-0">Tổng số: <b>{{numRecord}}</b> {{mode}}</p>
          <div data-app>
          </div>
          <div class="paging-footer item-center">
            <div class="select-footer">
            <ComboBox 
            :data="options" 
            ref = "selectrecord"
            :height="30" 
            :width="250" 
            :readonly="true"
            :slideUp="true"
            v-model="numRecordOnPage"
            @onchange = "onChange()"
            />
          </div>
          <v-pagination
          v-model="pageNum"
            :length="numPage"
            :total-visible="7"
            @input = "LoadData()"
          ></v-pagination>
        </div>
        </div>
      </div>
      <ReportForm @Ok = "del()" @close= "()=>{showwarning = false}" :formMode = "'Delete'" :content = "deleteContent" :show="showwarning" />
      <div class="loading-wrap" ref = "loading">
        <div class="loading">
          <img src="../assets/loading.svg">
          <p>Loading</p>
        </div>
      </div>
      <div class="notification item-center" ref = "notification" :style="{bottom: noteFormPos}">
        <img src="../assets/icon/Ok.svg" alt="">
        <div class="right-content" >
          {{note}}
        </div>
      </div>
  </div>
</template>
<script>
const axios = require("axios");
import EventBus from "../event-bus";
import ComboBox from '../components/ComboBox.vue';
import ReportForm from '@/components/ReportForm.vue';
export default {
  components:{ComboBox, ReportForm},
  data(){
    return{
      // Danh sách các nhân viên hiển thị ra màn hình
      employees:[],
      // Dòng đang được chọn
      rowSelected:0,
      // Vị trí lề trái của selector xóa, nhân bản
      xClick:'100px',
      // Vị trí lề trên của selector xóa, nhân bản
      yClick:'100px',
      // Dòng đang chọn của selector
      ownSelection:-1,
      // Số bản ghi có trong 1 trang
      numRecordOnPage:{text: '10 bản ghi trong 1 trang', value: 10},
      // Số trang hiện tại 
      pageNum : 1,      
      // Trạng thái của selector xóa, sửa
      expandShow:false,
      // Vị trí của header-content Thêm
      headPosition:-10,
      // thuộc tính scrollTop của table-content
      scrollPos:0,
      // Trạng thái của form cảnh báo khi xóa
      showwarning:false,
      // Số nhân viên có trong cơ sở dữ liệu
      numRecord:0,
      // Tổng số trang
      numPage:0,
      // Trạng thái tìm kiếm hoặc bình thường
      mode:" bản ghi",
      // Vị trí của thông báo muốn hiển thị
      noteFormPos:"-40px",
      // Nội dung của thông báo muốn hiển thị
      note:"",
      // Nội dung hiển thị của cảnh báo khi xóa bản ghi
      deleteContent:"",
      // Các lựa chọn số bản ghi/ 1 trang 
      options: [
        { text: '10 bản ghi trong 1 trang', value: 10 },
        { text: '20 bản ghi trong 1 trang', value: 20 },
        { text: '50 bản ghi trong 1 trang', value: 50 },
      ],
      
      uri :'data:application/vnd.ms-excel;base64,',
      template:'<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>',
      base64: function(s){ return window.btoa(unescape(encodeURIComponent(s))) },
      format: function(s, c) { return s.replace(/{(\w+)}/g, function(m, p) { return c[p]; }) }
    }
  },
  methods:{
    // Hiển thị select Xóa, nhân bản khi nhấn expand more
    openMoreSelect(event, index){
      // hiển thị
        if(this.ownSelection != index){
          this.xClick =`${(event.clientX-188).toString()}px`;
          this.yClick =`${(event.clientY-40).toString()}px`;
          this.expandShow = true;
          this.ownSelection = index;
        }
      // nhấn lần nữa để ẩn
        else{
          document.getElementsByClassName("action")[0].classList.remove("expand_more");
          this.ownSelection = -1;
        } 
    },
    // hàm xử lý sự kiện thay đổi số bản ghi trên 1 trang
    onChange(){
      this.pageNum = 1;
      this.reLoadData();
    },

    // Gọi đến chức năng chỉnh sửa
    edit(index){
      let me = this,
      // Copy đối tượng
        editEmployee = {...me.employees[index]};
      // Với các thuộc tính kiểu datetime, ta chỉ lấy về kiểu date:YYYY/MM/DD
      if(editEmployee.dateOfBirth != null && editEmployee.dateOfBirth!= ""){
          editEmployee.dateOfBirth = editEmployee.dateOfBirth.substr(0,10);
        }
        if(editEmployee.identityDate != null && editEmployee.identityDate!= ""){
          editEmployee.identityDate = editEmployee.identityDate.substr(0,10);
        }
      // Tạo param để truyền cho Form
      let param = {
        formMode :"Edit",
        Employee :editEmployee,
      }
      // Truyền dữ liệu cho Form
      me.open_form(param);
    },

    // gọi đến chức năng thêm
    async add(){
      let newEmployee = {},
      newEmployeeCode = "";
      // Lấy về mã nhân viên mới, giới tính mặc định là nam
      await axios.get("https://localhost:44300/api/v1/Employees/NewCode").then(response=>{
        if(response.data.isValid == true){
          newEmployeeCode = response.data.data[0];
          newEmployee.employeeCode = newEmployeeCode;
          newEmployee.gender = 0;
        }
      }).then(()=>{
        // Khởi tạo param
        let param = {
        formMode:"Add",
        Employee:newEmployee,
      }
      this.open_form(param);
      }).catch(err=>{
        console.log(err);
      });
      
    },
    // Hàm xóa đối tượng đang chọn
    deleteEmployee(){
      let me = this,
          employeeCode = me.employees[me.rowSelected].employeeCode;
      // Hiển thị cảnh báo khi xóa
      me.showwarning = true;
      me.deleteContent = "Bạn có chắc muốn xóa bản ghi \" "+employeeCode+"\" hay không ?";
    },
    // Thực hiện xóa đối tượng
    del(){
      let me = this,
      // Lấy ID của nhân viên
        EmployeeID = this.employees[me.rowSelected].employeeId,
      url = "https://localhost:44300/api/v1/Employees/"+EmployeeID;
      me.showwarning = false;
      me.$refs.loading.classList.add('show');
      // Thực hiện xóa đối tượng
      axios.delete(url).then(() =>{
              me.reLoadData();
              // Hiện thông báo xóa thành công
              me.showNote("Xóa dữ liệu thành công !");
              // Tắt selector xóa, nhân bản
              document.getElementsByClassName("action")[0].classList.remove("expand_more");
          }).catch(err =>{
            console.log(err);
          })
    },
    // Hàm nhân bản đối tượng đang chọn
    async clone(){
      // khai báo
      let me = this,
      // Nhân bản nhân viên đang được chọn
        newEmployee = {...me.employees[me.rowSelected]},
        newEmployeeCode = "";
        // Fomat lại thuộc tính ngày
        if(newEmployee.dateOfBirth != undefined && newEmployee.dateOfBirth!= ""){
          newEmployee.dateOfBirth = newEmployee.dateOfBirth.substr(0,10);
        }
        if(newEmployee.identityDate != undefined && newEmployee.identityDate!= ""){
          newEmployee.identityDate = newEmployee.identityDate.substr(0,10);
        }
        // tạo mã nhân viên mới
      await axios.get("https://localhost:44300/api/v1/Employees/NewCode").then(response=>{
        if(response.data.isValid == true){
          newEmployeeCode = response.data.data[0];
          newEmployee.employeeCode = newEmployeeCode;
        }
      }).catch(err=>{
        console.log(err);
      });
      // Khởi tạo param
      let param = {
        formMode :"Add",
        Employee : newEmployee,
      }
      // Gửi dữ liệu, mở form
      me.open_form(param);
      // Đóng selector 
      document.getElementsByClassName("action")[0].classList.remove("expand_more");
    },

    // gọi đến chức năng mở form
    open_form(param)
    {
      EventBus.$emit('open_form', param);
    },
    // fomat lại ngày sang định dạng DD/MM/YYYY
    fomatDate(datesrc){
      if(datesrc != null){
        let date = new Date(datesrc),
        year = date.getFullYear().toString(),
        month = (date.getMonth() + 1).toString().padStart(2, '0'),
        day = date.getDate().toString().padStart(2, '0');
      return `${day}/${month}/${year}`;
      }
      else return "";
    },
    // Xử lý khi có một cột được chọn
    initrowSelected(index){
      this.rowSelected = index;
    },
    // Hàm reload lại dữ liệu
    async LoadData(){
      let me = this,
      // Lấy từ khóa cần tìm, nếu không có => tìm không có điều kiện 
      kw = me.$refs.findEmploy.value,
      // index = số thứ tự của trang muốn lấy, number là số bản ghi muốn lấy
      // lấy được các bản ghi từ (index-1)*number đến index*number 
      url = `?index=${me.pageNum}&number=${me.numRecordOnPage.value}`;
      me.$refs.loading.classList.add('show');
      await axios.request({
        method: 'post',
        url: 'https://localhost:44300/api/v1/Employees/Find'+url,
        data:"\""+kw+"\"",
        headers: {
            'Content-Type': 'application/json',
        }
    }).then(response=>{
      me.employees = response.data.data[0];
    }).catch(err=>{
      console.log(err);
    });
    // Lấy tổng số bản ghi phù hợp trong database
    me.getNumberRecord(kw)
    me.$refs.loading.classList.remove('show');
    },
    getNumberRecord(kw){
      let me = this;
      axios.request({
        method: 'post',
        url: 'https://localhost:44300/api/v1/Employees/NumRecord',
        data:"\""+kw+"\"",
        headers: {
            'Content-Type': 'application/json',
        }
    }).then(response=>{
      // Lấy tổng số bản ghi
      me.numRecord = response.data;
      // Tính số trang
      me.numPage = Math.ceil(me.numRecord/me.numRecordOnPage.value);
    }).catch(err=>{
      console.log(err);
      me.$refs.loading.classList.remove('show');
    });
    },
    // Hàm load lại dữ liệu
    reLoadData(){
      let me = this;
      // Set trang hiện tại về trang đầu
      me.pageNum = 1;
      // Thay đổi trạng thái của trang
      me.mode = " bản ghi";
      // Thay đổi giá trị tìm kiếm về rỗng
      me.$refs.findEmploy.value = "";
      // load dữ liệu
      me.LoadData();
    },
    // Hàm tìm kiếm nhân viên dựa theo mã, tên, số điện thoại
    findKeyWord(){
      let me = this,
      // Lấy từ khóa tìm kiếm
        keyword = me.$refs.findEmploy.value;
      // Set trang hiện tại về trang đầu
        me.pageNum = 1;
        // thay đổi trạng thái của trang
        me.mode = " kết quả";
        // tìm kiếm kết quả
        this.LoadData(keyword);  
    },
    // Định dạng lại giới tính
    fomatGender(gender){
      switch(gender){
        case 0: return 'Nam';
        case 1: return 'Nữ';
        case 2: return 'Khác';
      }
    },
    // Hàm xử lý khi cuộn chuột
    handlescroll(){
      let me = this,
          scrollcontent = me.$refs.scrollcontent;
      // Xử lý khi cuộn xuống
      if(this.scrollPos < scrollcontent.scrollTop){
        // Dịch header lên trên
        if(scrollcontent.scrollTop < 102){
          me.headPosition = -scrollcontent.scrollTop;
        }
        else{
          me.headPosition = -102;
          me.scrollPos = scrollcontent.scrollTop;
        }
      }
      // Xử lý khi cuộn lên
      else{
        me.headPosition = 0;
        me.scrollPos = scrollcontent.scrollTop;
      }
    },
    // Hiển thị thông báo 
    showNote(content){
      let me = this;
      // thiết lập nội dung hiển thị
      me.note = content;
      me.noteFormPos = "10px";
      // Hiển thị trong 2s
      setTimeout(()=>{
        me.noteFormPos = "-40px";
      },2000);
    },
    tableToExcel(tablecontent,name){
      // tạo file excel
      var ctx = {worksheet: name || 'Worksheet', table: tablecontent}
      // tải về file excel
      window.location.href = this.uri + this.base64(this.format(this.template, ctx))
    },
    rendertable(Employees){
      let me = this,
        table = "",
        tbody = "",
        thead = "";
      // sinh header
      thead = "<thead><th >MÃ NHÂN VIÊN</th><th>TÊN NHÂN VIÊN</th><th >GIỚI TÍNH</th><th >NGÀY SINH</th><th >SỐ CMND</th><th >CHỨC DANH</th><th >TÊN ĐƠN VỊ</th> <th >SỐ TÀI KHOẢN</th><th>TÊN NGÂN HÀNG</th> <th >CHI NHÁNH TK NGÂN HÀNG</th></tr></thead>"
      // sinh body
      for (let index = 0; index < Employees.length; index++) {
        const element = Employees[index];
        let tr = "<tr>";
        tr = tr + `<td>${element.employeeCode||""}</td> `
        tr = tr + `<td>${element.employeeName||""}</td> ` 
        tr = tr + `<td>${me.fomatGender(element.gender)||""}</td> ` 
        tr = tr + `<td>${me.fomatDate(element.dateOfBirth)||""}</td> `  
        tr = tr + `<td>${element.identityNumber||""}</td> ` 
        tr = tr + `<td>${element.employeePosition||""}</td> ` 
        tr = tr + `<td>${element.departmentName||""}</td> ` 
        tr = tr + `<td>${element.bankAccountNumber||""}</td> ` 
        tr = tr + `<td>${element.bankName||""}</td> ` 
        tr = tr + `<td>${element.bankBranchName||""}</td> ` 
        tr = tr +"</tr>"
        tbody = tbody+tr;
      }
      table = `<table>${thead} ${tbody}</table>`;
      return table;
    },
    async loadAllData(){
      let me = this;
      me.$refs.loading.classList.add('show');
      await axios.get("https://localhost:44300/api/v1/Employees").then(response=>{
      let table = me.rendertable(response.data.data[0]);
      me.tableToExcel(table,"Nhân viên");
      // Fomat lại phòng ban để đưa vào combobox
      }).then(()=>{
        me.showNote("đã tải xong dữ liệu");
        me.$refs.loading.classList.remove('show');
        
      }).catch(err=>{
        console.log(err);
      });
    },
  },
   mounted(){
    let me = this;
    // Load dữ liệu khi trang khởi tạo trang
    me.reLoadData();
    // lắng nghe sử kiện click chuột
    me.$el.addEventListener('mouseup', function(e) {
      // Đóng selector xóa, nhân bản khi click ra ngoài nó
        var container = me.$refs.expand;
        if(me.expandShow == true){
          if (!container.contains(e.target)) {
            me.expandShow = false;
        }
      }
    });
    // Lắng nghe sự kiện khi Form thêm, sửa nhân viên thành công
    EventBus.$on('resetData', param =>{
      if(param == "edit"){
        // Nếu sửa thì load lại trang(số trang, từ khóa tìm kiếm không đổi)
        me.LoadData();
        // Hiển thị thông báo sửa thành công
        me.showNote("Chỉnh sửa bản ghi thành công");
      }
      else{
        // Nếu là thêm mới thì reload lại trang(số trang về từ đầu, từ khóa tìm kiếm rỗng)
        me.reLoadData();
        // Hiển thị thông báo xóa thành công
        me.showNote("Thêm dữ liệu thành công");
      }
    });
    // Lắng nghe sự kiện khi người dùng bấm Cất và Thêm ở form
    EventBus.$on("new", ()=>{
      // Gọi đến hàm tạo employee mới
      me.add();
    });
    // Lắng nghe sự kiện khi đóng form
    EventBus.$on('closeform',()=>{
      // focus vào ô tìm kiếm
      if(me.$refs.findEmploy != undefined)
      me.$refs.findEmploy.focus();
    });
    
  },
}
</script>

<style scoped>
@import "../assets/style/content.css";
</style>
