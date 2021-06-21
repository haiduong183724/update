.<template>
  <div class="form-wraper" @keyup.tab.exact='tabIndex()' @keyup.shift.tab.exact="shiftTab()" @keyup.esc.exact='closeForm()'
  >
      <div class="form-content">
            <div class="form-header">
                <div class="left-header item-center">
                    <h1>Thông tin nhân viên</h1>
                    <label for="" class="checkbox-label">
                        <input type="checkbox" >
                        Là khách hàng
                    </label>
                    <label for="" class="checkbox-label">
                        <input type="checkbox" >
                        Là khách hàng
                    </label>
                    <button ref="cacheFirst" ></button>
                </div>
                <div class="right-header">
                    <div class="pop-up-close">
                        <div data-title="Đóng(Esc)">
                            <img src="../assets/icon/x.svg" style="height:24px" alt="" @click="closeForm()" >
                        </div>
                        <div data-title = "Trợ giúp">
                            <img src="../assets/icon/help-outline.svg" style="height:24px" alt="">
                        </div>
                    </div>
                </div>
            </div>
            <div class="content-wraper">
                <div class="head-content">
                    <div class="head-content-left">
                        <div class="input-row d-flex">
                            <div class="short_length">
                                <div class="title-input">
                                    Mã <p> *</p>
                                </div>
                                <div>
                                    <input type="text" v-model="employee.employeeCode" autocomplete="off"
                                    FieldName = "Mã nhân viên" class="required" id = "employeeCode" 
                                    ref="firstElement" autocomplete:false @blur="checkEmployeeCode()">
                                </div>
                                
                            </div>
                            <div class="nor_length">
                                <div class="title-input">
                                    Tên <p> *</p>
                                </div>
                                <div>
                                    <input type="text"  v-model="employee.employeeName" autocomplete="off"
                                    FieldName = "Tên nhân viên" class="required" id = "employeeName" 
                                    @blur="checkEmployeeName()" ref = "employeeName">
                                </div>
                            </div>
                        </div>
                        <div class="input-row">
                            <div class="max_length">
                                <div class="title-input">
                                    Đơn vị<p>*</p>
                                </div>
                                <!-- <input type="text"  v-model="employee.companyName" 
                                FieldName = "Tên đơn vị" class="required" id = "companyName"> -->
                                <div style="padding:5px 0">
                                    <ComboBox 
                                    :data="selectData" 
                                    :height="32" 
                                    :width="398" 
                                    v-model="selectDepartment"
                                    @nexttab="nextTab()"
                                    ref = "selectbox"
                                    />
                                </div>
                            </div>
                        </div>
                        <div class="input-row">
                            <div class="max_length">
                                <div class="title-input">
                                    Chức danh <p></p>
                                </div>
                                <div>
                                    <input ref = "position" type="text"  v-model="employee.employeePosition" autocomplete="off"
                                    @blur="checkPosition()" FieldName = "Chức vụ" id = "position">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="head-content-right">
                        <div class="input-row d-flex">
                            <div class="short_length">
                                <div class="title-input">
                                    Ngày sinh
                                </div>
                                <div class="date_input">
                                    <input type="text" v-model="computedDateOfBirth" ref = "dateOfBirth"
                                     FieldName = "Ngày sinh" id = "dateOfBirth" class="date-input-field italic-placeholder" 
                                    @blur="checkDateOfBirth()" autocomplete="off" placeholder="DD/MM/YY">
                                    <img src="../assets/icon/datepicker.svg" alt="" @click ="openDateOfBirthPicker()"
                                        ref="DBIcon"
                                    >
                                </div>
                                <div class="date__picker" ref = "birthday" >
                                    <v-date-picker
                                        v-model="employee.dateOfBirth"
                                        no-title
                                        locale="vi-VN"
                                        @click:date="closeDateOfBirthPicker()"
                                    >
                                    <div class="date_picker_footer">
                                        <a href="#" @click="()=>{employee.dateOfBirth = getCurrentDay(); closeDateOfBirthPicker()}"> Hôm nay</a>
                                    </div>
                                    </v-date-picker>
                                </div>
                            </div>
                            <div class="nor_length">
                                <div class="title-input">
                                    Giới tính
                                </div>
                                <div class="item-center gender-radio" style="height:32px; margin-top:10px">
                                    <!-- <input type="radio" name="gentle" value="0" v-model="employee.gender">
                                    <label class="gentle">Nam</label>
                                    <input type="radio" name="gentle" value="1" v-model="employee.gender">
                                    <label class="gentle">Nữ</label>
                                    <input type="radio" name="gentle" value="2" v-model="employee.gender">
                                    <label class="gentle">Khác</label> -->
                                    <label class="container"> Nam
                                      <input type="radio" name="gender" value = "0" v-model="employee.gender">
                                      <span class="checkmark"></span>
                                    </label>
                                    <label class="container">Nữ
                                      <input type="radio" name="gender" value = "1" v-model="employee.gender">
                                      <span class="checkmark"></span>
                                    </label>
                                    <label class="container">Khác
                                      <input type="radio" name="gender" value = "2" v-model="employee.gender">
                                      <span class="checkmark"></span>
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="input-row d-flex">
                            <div class="nor_length">
                                <div class="title-input">
                                    Số CMND
                                </div>
                                <div>
                                    <input type="text" v-model="employee.identityNumber" autocomplete="off"
                                    FieldName = "Số CMND" id = " identityId" ref="identityCode" @blur="CheckIdentityCode()">
                                </div>
                            </div>
                            <div class="short_length">
                                <div class="title-input">
                                    Ngày cấp
                                </div>
                                <div class="date_input">
                                    <input type="text" v-model="copmutedDateidentity"  FieldName = "Ngày cấp" class="date-input-field italic-placeholder"
                                    ref = "dateOfidentity" autocomplete="off" placeholder="DD/MM/YY" @blur="checkDateOfidentity()">
                                    <img src="../assets/icon/datepicker.svg" alt="" 
                                    @click="openDateIdentifyPicker()" ref="DIIcon">
                                </div>
                                <div class="date__picker" ref="provday">
                                    <v-date-picker
                                        v-model="employee.identityDate"
                                        no-title
                                        scrollable
                                        locale="vi-VN"
                                        @click:date="closeDateIdentifyPicker()"
                                    >
                                    <div class="date_picker_footer">
                                        <a href="#" @click="()=>{employee.identityDate = getCurrentDay();closeDateIdentifyPicker();}"> Hôm nay</a>
                                    </div>
                                    </v-date-picker>
                                </div>
                            </div>
                        </div>
                         <div class="input-row">
                            <div class="max_length">
                                <div class="title-input">
                                    Nơi cấp <p></p>
                                </div>
                                <div>
                                    <input type="text" FieldName = "Nơi cấp" v-model="employee.identityPlace" 
                                    ref = "identityPlace" @blur="checkIdentiyPlace()" autocomplete="off">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="bottom-content">
                    <div class="input-row">
                        <div class="max_length">
                            <div class="title-input">
                                Địa chỉ
                            </div>
                            <div>
                                <input type="text" FieldName = "Địa chỉ" v-model="employee.address" 
                                ref = "address" @blur="checkAddress()" autocomplete="off">
                            </div>
                        </div>
                    </div>
                    <div class="input-row d-flex">
                            <div class="small_length">
                                <div class="title-input">
                                    ĐT di động 
                                </div>
                                <input type="text" FieldName = "ĐT di động" id ="mobilePhone" autocomplete="off"
                                ref = "phoneNumber" v-model="employee.phoneNumber" @blur="checkPhoneNumber()">
                            </div>
                            <div class="small_length">
                                <div class="title-input">
                                    ĐT cố định 
                                </div>
                                <div>
                                <input type="text" FieldName = "ĐT cố định"  id = "HomePhone" autocomplete="off"
                                ref = "telePhone" v-model="employee.telePhoneNumber" @blur="checkTelePhone()">
                                </div>
                            </div>
                            <div class="small_length">
                                <div class="title-input">
                                    Email 
                                </div>
                                <div>
                                    <input type="text"  id = "Email" ref = "email" v-model="employee.email" 
                                    @blur="checkEmail()" autocomplete="off">
                                </div>
                            </div>
                    </div>
                    <div class="input-row d-flex">
                            <div class="small_length">
                                <div class="title-input">
                                    Tài khoản ngân hàng
                                </div>
                                <div>
                                    <input type="text" v-model="employee.bankAccountNumber" @blur="checkBankAccount()"
                                    FieldName = "Tài khoản ngân hàng" id = "bankAccount" ref = "bankAccount" autocomplete="off">
                                </div>
                            </div>
                            <div class="small_length">
                                <div class="title-input">
                                    Tên ngân hàng
                                </div>
                                <div>
                                    <input type="text" v-model="employee.bankName" @blur="checkBankName()"
                                    FieldName = "Tên ngân hàng" id = "bankName" ref = "bankName" autocomplete="off">
                                </div>
                            </div>
                            <div class="small_length">
                                <div class="title-input">
                                    Chi nhánh 
                                </div>
                                <div>
                                    <input type="text" v-model="employee.bankBranchName" @blur="checkBankBranchName()"
                                    FieldName = "Chi nhánh" id = "bankBranch" ref = "bankBranch" autocomplete="off">
                                </div>
                            </div>
                    </div>
                </div>
                <div class="form-footer d-flex">
                    <button class="btn btn-white" v-on:click = "closeForm()">
                        <div class="btn-text">Hủy</div>
                    </button>
                    <div class="right-btn item-center">
                        <button class="btn btn-white" v-on:click="Submit()">
                            <div class="btn-text">Cất</div>
                        </button>
                        <button class="btn btn-green" ref="lastElement" @click="saveAndAdd()">
                            <div class="btn-text">Cất và Thêm</div>
                        </button>
                        <button ref="cacheLast" ></button>
                    </div>
                </div>
            </div>
      </div>
      <ReportForm 
        :formMode = "'warning'" 
        :content = "noteContent" 
        @Ok = "closeRpForm()"
        ref = "rpform"/>
        <div class="notification">
            
        </div>
  </div>
</template>

<script>
// import validateDate from "../assets/js/validate";
import EventBus from "../event-bus";
const axios = require("axios");
import ComboBox from '../components/ComboBox.vue';
import ReportForm from '@/components/ReportForm.vue';
// import CommonFn from "../assets/js/validate.js";
export default {
components:{ComboBox,ReportForm},
data(){
    return{
        // nhân viên được binding lên form
        employee :{},
        // Trạng thái hiển thị của form
        isShow:false,
        // Trạng thái hiển thị của Datepicker của IdentityDate
        isDate:false,
        // Trạng thái hiển thị của form thông báo
        isRpForm:false,
        // Trạng thái hiển thị của Datepicker của DateOfBirth
        isDateOfBirth:false,
        // Trạng thái làm việc của Form: Thêm, Sửa
        formMode:"",
        // Tính hợp lệ của các thông tin trong Form: Nếu hợp lệ thì được gửi
        formValid:true,
        // Chứa thông tin của các phòng ban
        selectData:[
        ],
        // Có tiếp tục thêm mới sau khi lưu dữ liệu hay không
        conti:false,
        // Phòng ban đang được lựa chọn
        selectDepartment:{text:null},
        // Nội dung của thông báo
        noteContent:"Mã nhân viên đã tồn tại trong hệ thống"
    }
},

async mounted(){
    let me = this;
    // Lấy danh sách các phòng ban
     await axios.get("https://localhost:44300/api/v1/Departments").then(response=>{
      let departments = response.data.data[0].slice();
      // Fomat lại phòng ban để đưa vào combobox
      for (let index = 0; index < departments.length; index++) {
          let department = {};
          department.text = departments[index].departmentName;
          department.value = departments[index].departmentId;
          this.selectData.push(department);
      }
    }).catch(err=>{
      console.log(err);
    });
    // Bắt sự kiện mở Form từ Content
    EventBus.$on("open_form", param =>{
        // Xác định mục đích mở Form
        me.formMode = param.formMode;
        // Copy đối tượng nhận được 
        if(param.Employee != undefined){
            me.employee = {...param.Employee};
            // Bind Phòng ban của nhân viên lên Form
            if(me.$refs.selectbox!= undefined){
                me.$refs.selectbox.select({
                text:me.employee.departmentName, value: me.employee.departmentId});
            }
        }
        // Hiển thị Form lên giao diện
        me.showform();
    });
    // Lắng nghe sự kiện click chuột
    document.addEventListener('mouseup', function(e) {
        let birthcontainer = me.$refs.birthday,
            birthdatinputIcon = me.$refs.DBIcon,
            procontainer = me.$refs.provday,
            dateOfIDIcon = me.$refs.DIIcon;
        // Đóng các datePicke khi click ra ngoài
        if(birthcontainer != undefined ){
          if (!birthcontainer.contains(e.target) && !birthdatinputIcon.contains(e.target)){
              me.closeDateOfBirthPicker();
            }
        }
        if(procontainer != undefined){
          if (!procontainer.contains(e.target) && !dateOfIDIcon.contains(e.target)){
              me.closeDateIdentifyPicker()
            }
        }
    });
},
methods:{
    // Hiển thị Form lên giao diện
    showform(){
        this.$el.classList.add('show');
        // Focus vào ô input đầu tiên
        this.autofocus();
    },
     autofocus(){
        // Focus vào ô input đàu tiên
        if(this.$refs.firstElement != undefined){
             this.$refs.firstElement.focus();
        }
    },
    // Hiển thị Datepicker lựa chọn ngày sinh
    openDateOfBirthPicker(){
        this.$refs.birthday.classList.toggle("show");
    },
    // Hiển thị DatePicker của ngày cấp CMND
    openDateIdentifyPicker(){
        this.$refs.provday.classList.toggle("show");
    },
    // Đóng DatePicker của ngày sinh
    closeDateOfBirthPicker(){
        this.$refs.birthday.classList.remove("show");
    },
    // Đóng Datepicker của ngày cấp CMND
    closeDateIdentifyPicker(){
        this.$refs.provday.classList.remove("show");
    },
    // Hàm đóng Form
    closeForm(){
        // Đóng Form trên giao diện
        this.$el.classList.remove('show');
        // Đóng các hiển thị trên Form
        this.isDate = false;
        this.isDateOfBirth = false;
        this.$refs.selectbox.selectValue ={ };
        // Reset lại các input trong Form
        this.beforeCloseForm();
        // Reset Combobox
        this.$refs.selectbox.dataShow = this.selectData;
        this.$refs.selectbox.rowMarked = -1;
    },
    // Hàm Fomat ngày sang dạng DD/MM/YYYY
    formatDate (date) {
      if (!date) return null
      var [year, month, day] = date.split('-');
      day = day.substr(0,2);
      return `${day}/${month}/${year}`
    },
    // Hàm kiểm tra các giá trị nhập
    checkValid(){
        this.formValid=true;
        // Kiểm tra Mã nhân viên
        this.checkEmployeeCode();
        // Kiểm tra tên nhân viên
        this.checkEmployeeName();
        // kiểm tra phòng ban
        this.checkDepartment();
        // Kiểm tra đơn vị
        this.checkPosition();
        // Kiểm tra ngày sinh
        this.checkDateOfBirth();
        // Kiểm tra mã CMND
        this.CheckIdentityCode();
        // Kiểm tra ngày cấp CMND
        this.checkDateOfidentity();
        // Kiểm tra nơi cấp CMND;
        this.checkIdentiyPlace();
        // Kiểm tra địa chỉ 
        this.checkAddress();
        // Kiểm tra Email
        this.checkEmail();
        // Kiểm tra các SDT 
        this.checkPhoneNumber();
        this.checkTelePhone();
        // Kiểm tra TK ngân hàng
        this.checkBankAccount();
        // Kiểm tra tên ngân hàng
        this.checkBankName();
        // Kiểm tra chi nhanh ngân hàng
        this.checkBankBranchName();
        // Nếu form có lỗi thì việc nhấn Cất và Thêm không được tính
        if(this.conti == true && this.formValid == false){
            this.conti = false;
        }
    },
    // Hàm cất dữ liệu
    Submit(){
        let me = this;
        // Check Form có hợp lệ không
        me.checkValid();
        // Fomat lại phòng ban của employee
        me.employee.departmentName = me.selectDepartment.text;
        me.employee.departmentId = me.selectDepartment.value;
        // Nếu Form hợp lệ
        if(me.formValid){
            // Thêm mới
            if(me.formMode == "Add"){
                me.Add();
            }
            // Chỉnh sửa
            if(me.formMode == "Edit"){
                me.Edit();
            }
        }
    },
    // Hàm thêm mới
    async Add(){
        let me = this;
        me.employee.createdDate = null;
        // thêm mới dữ liệu
        await axios.post("https://localhost:44300/api/v1/Employees", me.employee).then(response=>{
            if(response.data.isValid == true){
                me.closeForm();
                EventBus.$emit("resetData", "add");
            }
    }).then(()=>{
        // Thêm mới sau khi cất dữ liệu
        if(me.conti == true){
            EventBus.$emit("new");
        }
        me.conti = false;
    }).catch(err =>{console.log(err.response.data);
        // hiển thị thông báo khi mã nhân viên bị trùng
        me.noteContent = `Mã nhân viên <${me.employee.employeeCode}> đã tồn tại trong hệ thống, vui lòng kiểm tra lại`;
        me.openRpForm();
    });
    },
    async Edit(){
        let me = this;
        // Gửi request chỉnh sửa dữ liệu
        await axios.put("https://localhost:44300/api/v1/Employees", me.employee).then(response=>{
        if(response.data.isValid == true){
            me.closeForm();
            EventBus.$emit("resetData", "edit");
        }
    }).then(()=>{
        // Thêm mới sau khi cất
        if(me.conti == true){
            EventBus.$emit("new");
        }
        me.conti = false;
    }).catch(() =>{
        // Hiển thị thông báo nếu trùng mã
        me.noteContent = `Mã nhân viên <${me.employee.employeeCode}> đã tồn tại trong hệ thống, vui lòng kiểm tra lại`;
        this.openRpForm();});
    },
    // Hàm thêm mới sau khi cất
    saveAndAdd(){
        let me = this;
        me.conti = true;
        me.Submit();
    },
    // Focus về ô input đầu tiên khi nhấn tab ở ô cuối của form
    tabIndex(){
        if(document.activeElement == this.$refs.cacheLast){
            this.$refs.firstElement.focus();
        }
    },
    // Focus về ô cuối của Form khi nhấn shift+tab ở ô đầu của form
    shiftTab(){
        if(document.activeElement == this.$refs.cacheFirst){
            this.$refs.lastElement.focus();
        }
    },
    // nhận sự kiện tab từ Combobox
    nextTab(){
        this.$refs.position.focus();
    },
    // Lấy giá trị ngày hôm nay 
    getCurrentDay(){
        var today = new Date();
        var dd = String(today.getDate()).padStart(2, '0');
        var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
        var yyyy = today.getFullYear();
        return yyyy + '-' + mm + '-' + dd;
    },
    // Hàm xử lý trướng khi đóng form
    beforeCloseForm(){
        let me = this,
        inputElement =  me.$el.getElementsByTagName("input");
        // reset các trường nhập thông tin của form về trạng thái ban đầu
        for (let index = 0; index < inputElement.length; index++) {
            inputElement[index].parentElement.removeAttribute('data-title');
            inputElement[index].classList.remove("invalidField");
        }
        // reset trường nhập thông tin selecbox
        me.$refs.dateOfidentity.parentElement.classList.remove("invalidField");
        me.$refs.dateOfBirth.parentElement.classList.remove("invalidField");
        me.$refs.selectbox.valueValid();
    },
    // Hàm kiểm tra giá trị ngày
    validateDate(date){
        let dateValue = date.split("/");
        if(dateValue.length != 3){
            return false;
        }
        else{
            let dd = dateValue[0],
            mm = dateValue[1],
            yy = dateValue[2];
            if(yy>2100 || yy <1900){
                return false;
            }
            if( mm > 12 || mm <1 ){
                return false;
            }
            if(dd <1){
                return false;
            }
            // tháng 1,3,5,7,8,10,12 có 31 ngày
            if([1, 3, 5, 7, 8, 10, 12].includes(mm)){
                if( dd >31 ){
                    return false;
                }
            }
            // tháng 4, 6, 9, 11 có 30 ngày
            if( [4,6,9,11].includes(mm)){
                if(dd > 30){
                    return false;
                }
            }
            if(mm == 2){
                // năm nhuận tháng 2 có 29 ngày
                if(yy%4 == 0 && yy%100 != 0){
                    if( dd >29){
                        return false;
                    }
                }
                else{
                    // không nhuận có 28 ngày
                    if(dd>28){
                        return false;
                    }
                }
            }
            return true;
        }
    },
    // Hàm kiểm tra giá trị email có hợp lệ hay không
    validateEmail(email){
        // validate email:
        // có 5-32 kí tự trước @
        // kí tự đầu là chữ cái
        // có 2 kí tự trở lên sau @
        let filter = /^[a-z][a-z0-9_.]{5,32}@[a-z0-9]{2,}(\.[a-z0-9]{2,4}){1,2}$/g;
        // Nếu email khác rỗng thì check
        if(email != "" && email != null){
            if(!filter.test(email.toLowerCase())){
            return false;
        }
            return true;
        }
        return true;
    },
    // Hàm kiểm tra giá trị số điện thoại nhập có hợp lệ hay không
    // Số điện thoại phải có đúng 10 số
    validatePhoneNumber(phone){
        // lọc các số có đầu là 09, 03, 07, 08, 09
        var vnf_regex = /((09|03|07|08|05)+([0-9]{8})\b)/g;
        // Nếu chuỗi khác rỗng thì kiểm tra
        if(phone!= null && phone != ""){
            if(vnf_regex.test(phone) == false){
                return false
            }
            else{
                return true;
            }
        }
        return true;
    },
    //Hàm kiểm tra mã nhân viên
    checkEmployeeCode(){
        let me = this,
            EmployeeCode = me.employee.employeeCode,
            employeeElement = document.getElementById("employeeCode"),
            regrexCode = /[0-9]\b/g;
        // Kiểm tra mã nhân viên có đúng định dạng hay không
        if(EmployeeCode != ""){
            employeeElement.parentElement.removeAttribute('data-title');
            employeeElement.classList.remove("invalidField");
            let code = EmployeeCode.substr(3);
            if(EmployeeCode.substr(0,3) != "NV-" || regrexCode.test(code) == false || code.length < 1 || code.length > 6 ){
                employeeElement.parentElement.setAttribute('data-title', "Mã nhân viên không đúng định dạng");
                employeeElement.classList.add("invalidField");
                me.formValid = false;
            }
            else{
                employeeElement.parentElement.removeAttribute('data-title');
                employeeElement.classList.remove("invalidField");
                if(EmployeeCode.length>50){
                    employeeElement.parentElement.setAttribute('data-title', "Mã nhân viên không được quá 50 kí tự");
                    employeeElement.classList.add("invalidField");
                    me.formValid = false;
                }
            }
        }
        else{
            employeeElement.parentElement.setAttribute('data-title', "Mã nhân viên không được để trống");
            employeeElement.classList.add("invalidField");
        }
        
    },
    // Hàm kiểm tra tên nhân viên
    checkEmployeeName(){
        let me = this,
        employeeNameInput = me.$refs.employeeName,
        employeeName = employeeNameInput.value;
        // Kiểm tra tên nhân viên có trống hay không
        if(employeeName == "" || employeeName == null){
            employeeNameInput.parentElement.setAttribute("data-title", "Tên nhân viên không được phép để trống");
            employeeNameInput.classList.add('invalidField');
            me.formValid = false;
        }
        else{
            // Kiểm tra độ dài có hợp lệ không
            employeeNameInput.parentElement.removeAttribute("data-title");
            employeeNameInput.classList.remove('invalidField');
            if(employeeName.length > 100){
                employeeNameInput.parentElement.setAttribute("data-title", "Tên nhân viên không được phép quá 100 kí tự");
                employeeNameInput.classList.add('invalidField');
                me.formValid = false;
            }
            else{
                employeeNameInput.parentElement.removeAttribute("data-title");
                employeeNameInput.classList.remove('invalidField');
            }
        }
    },
    // Hàm kiểm tra phòng ban nhân viên
    checkDepartment(){
        
    },
    // Hàm kiểm tra chức danh
    checkPosition(){
        let me = this,
        positionInput = me.$refs.position,
        position = positionInput.value;
        if(position.length > 255){
            positionInput.parentElement.setAttribute("data-title", "Chức danh nhân viên không được phép quá 255 kí tự");
            positionInput.classList.add('invalidField');
            me.formValid = false;
        }
        else{
            positionInput.parentElement.removeAttribute("data-title");
            positionInput.classList.remove('invalidField');
        }
    },
    //Hàm kiểm tra giá trị input của ngày sinh có hợp lệ hay không
    checkDateOfBirth(){
        let me = this;
        let date = me.$refs.dateOfBirth.value;
        if(date != undefined && date != "" ){
            // Kiểm tra input ngày sinh có đúng định dạng không (DD/MM/YYYY)
            if(me.validateDate(date) == false){
                me.$refs.dateOfBirth.parentElement.classList.add("invalidField");
                me.$refs.dateOfBirth.parentElement.setAttribute('data-title', "Ngày sinh nhập không đúng định dạng");
                this.formValid = false
            }
            else{
                me.$refs.dateOfBirth.parentElement.removeAttribute('data-title');
                me.$refs.dateOfBirth.parentElement.classList.remove("invalidField");
            }
        }
        else{
            me.$refs.dateOfBirth.parentElement.removeAttribute('data-title');
            me.$refs.dateOfBirth.parentElement.classList.remove("invalidField");
        }
    },
    // Hàm kiểm tra mã CMND
    CheckIdentityCode(){

    },
    // Hàm kiểm tra ngày cấp CMND
    checkDateOfidentity(){
        let me = this;
        let date = me.$refs.dateOfidentity.value;
        if(date != undefined && date != "" ){
            // Kiểm tra input ngày cấp CMND có đúng định dạng không (DD/MM/YYYY)
            if(me.validateDate(date) == false){
                me.$refs.dateOfidentity.parentElement.classList.add("invalidField");
                me.$refs.dateOfidentity.parentElement.setAttribute('data-title', "Ngày cấp CMND nhập không đúng định dạng");
                this.formValid = false
            }
            else{
                me.$refs.dateOfidentity.parentElement.removeAttribute('data-title');
                me.$refs.dateOfidentity.parentElement.classList.remove("invalidField");
            }
        }
        else{
            me.$refs.dateOfidentity.parentElement.removeAttribute('data-title');
            me.$refs.dateOfidentity.parentElement.classList.remove("invalidField");
        }
    },
    // Hàm kiểm tra địa điểm cấp CMND
    checkIdentiyPlace(){
        let me = this,
        identityPlaceInput = me.$refs.identityPlace,
        indentityPlace = identityPlaceInput.value;
        // Kiểm tra độ dài có hợp lệ không
        if(indentityPlace.length > 255){
            identityPlaceInput.parentElement.setAttribute("data-title", "Nơi cấp CMND không được phép quá 255 kí tự");
            identityPlaceInput.classList.add('invalidField');
            me.formValid = false;
        }
        else{
            identityPlaceInput.parentElement.removeAttribute("data-title");
            identityPlaceInput.classList.remove('invalidField');
        }
    },
    // Hàm kiểm tra địa chỉ
    checkAddress(){
        let me = this,
        positionInput = me.$refs.address,
        position = positionInput.value;
        // Kiểm tra độ dài có hợp lệ không
        if(position.length > 255){
            positionInput.parentElement.setAttribute("data-title", "Chức danh nhân viên không được phép quá 255 kí tự");
            positionInput.classList.add('invalidField');
            me.formValid = false;
        }
        else{
            positionInput.parentElement.removeAttribute("data-title");
            positionInput.classList.remove('invalidField');
        }
    },
    // Hàm kiểm tra Email
    checkEmail(){
        let me = this,
        emailInput = me.$refs.email,
        email = emailInput.value;
        if(!me.validateEmail(email)){
            me.$refs.email.classList.add("invalidField");
            me.$refs.email.parentElement.setAttribute('data-title', "Email nhập không đúng định dạng");
            me.formValid = false;
        }
        else{
            me.$refs.email.classList.remove("invalidField");
            me.$refs.email.parentElement.removeAttribute('data-title');
        }
    },
    // Hàm kiểm tra số điện thoại
    checkPhoneNumber(){
        let me = this,
            phoneNumberInput = me.$refs.phoneNumber,
            phoneNumber = phoneNumberInput.value;
        // Kiểm tra hợp lệ của SDT
        if(me.validatePhoneNumber(phoneNumber) == false){
            phoneNumberInput.parentElement.setAttribute("data-title", "Số điện thoại nhập không đúng định dạng");
            phoneNumberInput.classList.add("invalidField");
            me.formValid = false;
        }
        else{
            phoneNumberInput.classList.remove("invalidField");
            phoneNumberInput.parentElement.removeAttribute('data-title');
        }
    },
    // Hàm kiểm tra số di động
    checkTelePhone(){
        let me = this,
            phoneNumberInput = me.$refs.telePhone,
            phoneNumber = phoneNumberInput.value;
            // Kiểm tra hợp lệ SDT
        if(me.validatePhoneNumber(phoneNumber) == false){
            phoneNumberInput.parentElement.setAttribute("data-title", "Số điện thoại nhập không đúng định dạng");
            phoneNumberInput.classList.add("invalidField");
            me.formValid = false;
        }
        else{
            phoneNumberInput.classList.remove("invalidField");
            phoneNumberInput.parentElement.removeAttribute('data-title');
        }
    },
    // Hàm kiểm tra tên ngân hàng
    checkBankName(){
        let me = this,
        BankNameInput = me.$refs.bankName,
        bankName = BankNameInput.value;
        // Kiểm tra độ dài
        if(bankName.length > 255){
            BankNameInput.parentElement.setAttribute("data-title", "Tên ngân hàng không được phép quá 255 kí tự");
            BankNameInput.classList.add('invalidField');
            me.formValid = false;
        }
        else{
            BankNameInput.parentElement.removeAttribute("data-title");
            BankNameInput.classList.remove('invalidField');
        }
    },
    // Hàm kiểm tra tài khoản ngân hàng
    checkBankAccount(){
    },
    // Hàm kiểm tra chi nhanh ngân hàng
    checkBankBranchName(){
        let me = this,
        bankBranchkNameInput = me.$refs.bankBranch,
        bankBranchName = bankBranchkNameInput.value;
        // Kiểm tra độ dài
        if(bankBranchName.length > 255){
            bankBranchkNameInput.parentElement.setAttribute("data-title", "Tên chi nhánh ngân hàng không được phép quá 255 kí tự");
            bankBranchkNameInput.classList.add('invalidField');
            me.formValid = false;
        }
        else{
            bankBranchkNameInput.parentElement.removeAttribute("data-title");
            bankBranchkNameInput.classList.remove('invalidField');
        }
    },
    // mở form cảnh báo
    openRpForm(){
        let me = this;
        // Hiển thị lên giao diện
        me.$refs.rpform.$el.classList.add("show");
    },
    // Đóng Form cảnh báo
    closeRpForm(){
        this.$refs.rpform.$el.classList.remove("show");
        this.autofocus()
    },
},
computed: {
    // Computed của dateofbirth
    computedDateOfBirth:{
        // chuyển từ dạng YYYY-MM-DD sang dạng DD/MM/YY
        get:function(){
            return this.formatDate(this.employee.dateOfBirth);
        },
        // chuyển từ dạng DD/MM/YYYY sang dạng YYYY/MM/DD
        set:function(date){
            let dateValue = date.split("/");
            if(this.validateDate(date) == true){
                let dd = dateValue[0],
                mm = dateValue[1],
                yy = dateValue[2];
                this.employee.dateOfBirth = yy+"-"+mm+"-"+dd;
            }
            else{
                this.employee.dateOfBirth = null;
            }
            
        }
        
    },
    copmutedDateidentity:{
        // chuyển từ dạng YYYY-MM-DD sang dạng DD/MM/YY
        get:function(){
            return this.formatDate(this.employee.identityDate);
        },
        // chuyển từ dạng DD/MM/YYYY sang dạng YYYY/MM/DD
        set:function(date){
            let dateValue = date.split("/");
            if(this.validateDate(date)== true){
                let dd = dateValue[0],
                mm = dateValue[1],
                yy = dateValue[2];
            this.employee.identityDate = yy+"-"+mm+"-"+dd;
            }
            else{
                this.employee.identityDate = null;
            }
            
        }
    },
  },
}
</script>

<style scoped>
@import "../assets/style/Form.css";
</style>