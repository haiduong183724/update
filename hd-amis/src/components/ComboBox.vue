.<template>
    <div class="combo-box-wraper " :class="{'r-flex':slideUp||false}" v-on:keyup.38 = "moveUp()" @keyup.tab="EventTab()" 
        v-on:keyup.40 = "moveDown()" v-on:keyup.enter = "selectedByEnter()" >
        
        <div class="combo-box-wrap" :class="{'no-data':!valid}" 
        :style="[widthLength, heightLength]" v-on:click = "toggleSelect()" ref = "input">
            <button ref="cachebtn"></button>
            <input type="text" ref = "textvalue" :value="selectValue.text" v-on:input = "search()" 
            :readonly="readonly||false" @blur="checkCombobox()">
            <div class="combo-select-show item-center" >
                <img src="../assets/icon/dropdown.svg" alt="">
            </div>
            <button ref="cachenextbtn"></button>
        </div>
        <div class="selected-list-combo-box" :class="{'selection-show':isshow}" id = 'combo-box-data' :style="widthLength" ref = "select">
            <div  v-for="(item, index) in dataShow" :key="index" class="combo-selection" 
            v-on:click = "select(item)" :class="{'item-selected': selectValue.text == item.text, 'row-current':index == rowMarked}">
                <div class="ticker-icon">
                <div class="icon-wrap" :class="{'selection-show': item.text == selectValue.text}">
                    <img src="../assets/icon/done.svg" alt="">
                </div>
                </div>
                <div class="select-text">
                    <span>{{item.text}}</span>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    props:{
        width:Number,
        height:Number,
        // thông tin của các select trong combo box
        data:Array,
        value:Object,
        readonly:Boolean,
        slideUp:Boolean,
    },
    data(){
        return{
            // isshow = true:  hiển thị selecbox, isshow = false: đóng 
            isshow:false,
            // giá trị đang được lựa chọn của selecbox
            selectValue: {...this._props.value},
            // giá trị của selectbox có hợp lệ hay không
            valid:true,
            // mảng lưu các giá trị select được truyền vào từ component cha
            cacheData:this._props.data,
            // mảng lưu các thông tin sẽ được hiển thị ta màn hình
            dataShow:[],
            // giá trị của dòng đang được đánh dấu trong combobox
            rowMarked:-1,
            comboboxId:this._props.id,
            widthLength:{
                width: `${this._props.width}px`,
            },
            heightLength:{
                height: `${this._props.height}px`,
            },
        };
        
    },
    //khởi tạo 
    created(){
        this.dataShow = this.cacheData;
    },
    mounted(){
        let me = this;
        // đóng combo box khi nhấp ra ngoài màn hình
        document.addEventListener('mouseup', function(e) {
        if(me.$refs.input!= undefined){
            if (!me.$refs.input.contains(e.target) && !me.$refs.select.contains(e.target)){
                me.hideform();
                if(me.selectValue.text != undefined && me.selectValue.text != '') {
                    me.checkValue();
                }
            }
        }
        });
    },
    methods:{
        // hàm xử lý khi nhấn nút xuống
        moveDown(){
            let me = this;
            // chuyển dòng đang được đánh dấu lên trên
            if(me.rowMarked < this.dataShow.length -1){
                this.rowMarked++;
                console.log("a");
            }
        },
        // hàm xử lý khi nhấn nút lên
        moveUp(){
            let me = this;
            // chuyển dòng đang được đánh dấu lên trên
            if(me.rowMarked > 0){
                this.rowMarked--;
            }  
        },
        toggleSelect(){
            this.isshow = !this.isshow;
            this.$refs.textvalue.focus();
        },
        // hàm xử lý khi nhấn enter
        selectedByEnter(){
            let me = this;
            // nếu có 1 dòng đã được đánh dấu
            if(me.rowMarked>=0 && me.rowMarked < me.dataShow.length){
                let currentValue = me.dataShow[me.rowMarked];
                // giá trị của combobox là giá trị dòng đánh dấu đó
                me.select(currentValue);
                me.rowMarked = -1;
                this.valueValid();
                this.dataShow = this.cacheData;
            }
            // chưa có dòng nào dược đánh dấu
            else{
                // nếu có ít nhất 1 giá trị khớp với text trong combobox
                if(me.dataShow.length >= 1){
                    // lấy luôn giá trị đầu tiên làm giá trị cho combobox
                    let currentValue = me.dataShow[0];
                    me.select(currentValue);
                    me.valueValid();
                    this.dataShow = this.cacheData;
                }
                else{
                    // nếu không có giá trị nào khớp: đóng form, hiện invalid
                    me.hideform();
                    me.valueInvalid();
                    this.dataShow = this.cacheData;
                }
            }
        },
        getValue(){
            return this.selectValue.value;
        },
        getText(){
            return this.selectValue.text;
        },
        // ẩn form đi
        hideform(){
            this.isshow = false;
        },
        // lựa chọn 1 giá trị
        select(value){
            this.selectValue = {...value};
            this.hideform();
            this.valueValid();
            this.$emit("input",this.selectValue);
            this.$emit("onchange");
        },
        // chuyển từ tiếng việt có dấu sang không dấu
        fomatText(str){
            str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g,"a"); 
            str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g,"e"); 
            str = str.replace(/ì|í|ị|ỉ|ĩ/g,"i"); 
            str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g,"o"); 
            str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g,"u"); 
            str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g,"y"); 
            str = str.replace(/đ/g,"d");
            str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
            str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
            str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
            str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
            str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
            str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
            str = str.replace(/Đ/g, "D");
            // Một vài bộ encode coi các dấu mũ, dấu chữ như một kí tự riêng biệt nên thêm hai dòng này
            str = str.replace(/\u0300|\u0301|\u0303|\u0309|\u0323/g, ""); // ̀ ́ ̃ ̉ ̣  huyền, sắc, ngã, hỏi, nặng
            str = str.replace(/\u02C6|\u0306|\u031B/g, ""); // ˆ ̆ ̛  Â, Ê, Ă, Ơ, Ư
            // Bỏ các khoảng trắng liền nhau
            str = str.replace(/ + /g," ");
            str = str.trim();
            // Bỏ dấu câu, kí tự đặc biệt
            str = str.replace(/Ư|@|%|\^|\*|\(|\)|\+|=|<|>|\?|\/|,|\.|:|;|'|"|&|#|\[|\]|~|\$|_|`|-|{|}|\||\\/g," ");
            return str;
        },
        // tìm kiếm các giá trị khớp với text trong combobox
        search(){
            // chuyển từ khóa tìm kiếm về chữ thường
            this.selectValue.text = this.$refs.textvalue.value;
            let value = this.fomatText(this.$refs.textvalue.value.toLowerCase());
            this.dataShow = [];
            if(value != undefined){
                this.isshow = true;
                // tìm kiếm các giá trị match với giá trị cần tìm
                for (let index = 0; index < this.cacheData.length; index++) {
                    // chuyển các text sang tiếng việt không dấu
                    const text = this.fomatText(this.cacheData[index].text.toLowerCase());
                    if(text.search(value.toLowerCase()) != -1){
                        this.dataShow.push(this.cacheData[index]);
                    }
                }
            }
        },
        valueInvalid(){
            this.valid = false;
            this.$refs.input.setAttribute("data-title", 'Trường thông tin nhập không hợp lệ')
        },
        valueValid(){
            this.$refs.input.removeAttribute('data-title');
            this.valid = true;
        },
        checkValue(){
            let valueCheck = this.selectValue.text;
            for (let index = 0; index < this.dataShow.length; index++) {
                let currentText = this.dataShow[index].text;
                let currentValue = this.dataShow[index];
                if(valueCheck == currentText){
                    this.selectValue = {...currentValue};
                    this.valueValid();
                    return;
                }
            }
            this.valueInvalid();
        },
        EventTab(){
            let me = this;
            if(document.activeElement == me.$refs.cachebtn){
                me.$refs.textvalue.focus();
                me.isshow = true;
            }
            else{
                if(me.$refs.textvalue.value != undefined && me.$refs.textvalue.value!= "" ){ 
                    me.selectedByEnter();   
                }
                if(me.rowMarked>=0 && me.rowMarked < me.dataShow.length){
                    me.selectedByEnter();
                }
                me.isshow = false;
                me.$emit('nexttab');
            }
        },
        checkCombobox(){
            let me = this;
            if(me.$refs.textvalue.value == ""){
                me.valid = false;
                me.$refs.input.setAttribute("data-title", "Đơn vị không được để trống !");
            }
            else{
                me.valid = true;
                me.$refs.input.removeAttribute("data-title");
            }
        }
    },
    updated(){
        if(this.selectValue.text == ''){
            this.dataShow = this.cacheData;
        }
    }
}
</script>

<style scoped>
    *{
        padding: 0;
        margin: 0;
    }
    .r-flex{
        display: flex;
        flex-direction: column-reverse;
    }
    .combo-box-wraper{
        max-width: 400px;
    }
    .combo-box-wrap{
        display: flex;
        border: 1px solid #babec5;
        align-items: center;
        box-sizing: border-box;
        position: relative;
    }
    .combo-box-wrap:focus-within{
        border: 1px solid #20ac1c !important;
    }
    .selection-show{
        display: block !important;  
    }
    .no-data{
        border: 1px solid red;
    }
    .combo-box-wrap input[type="text"]{
        border: none !important ;
        outline: 0;
        width: 100%;
        padding: 5px;
        font-size:13px ;
    }
    .combo-box-wrap .combo-select-show{
        padding:5px ;
        cursor: pointer;
        width: calc(height);
        height: 100%;
    }
    .selected-list-combo-box{
        text-align: left;
        border: 1px solid #babec5;
        display: none;
        border-top:none;
        z-index: 10000;
        position: absolute;
        box-sizing: border-box;
        background: white;
    }
    .selected-list-combo-box .combo-selection{
        padding: 5px;
        display: flex;
        align-items: center;
        cursor: pointer;
    }
    .selected-list-combo-box .combo-selection:hover{
        background:#ebedf0;
        color: #35bf22;
    }
    .item-selected{
       font-weight: 700;
    }
    .row-current{
        background: rgb(112, 184, 202) !important ;
    }
    .ticker-icon{
        height: 100%;
        width: 30px;
    }
    .icon-wrap{
        display: none;
    }
    .select-text{
        width: 100%;
        text-align: left;
    }
    .show-select{
        display: block !important;
        transform: rotate(180deg);
    }
</style>