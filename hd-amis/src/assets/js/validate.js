var CommonFn =  {};
CommonFn.validateDate = date => {
    let dateValue = date.split("/");
    if(date.value.length != 3){
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
}
CommonFn.fomatText = str =>{
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
}
CommonFn.getCurrentDay = ()=>{
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();
    return yyyy + '-' + mm + '-' + dd;
}
CommonFn.validateDate = date =>{
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
}
CommonFn.validateEmail = email =>{
    // validate email:
    // có 5-32 kí tự trước @
    // có 2 kí tự trở lên sau @
    let filter = /^[a-z][a-z0-9_]{5,32}@[a-z0-9]{2,}(\.[a-z0-9]{2,4}){1,2}$/g ;
    if(!filter.test(email)){
        return false;
    }
    return true;
}

CommonFn.validatePhoneNumber = phone =>{
    let filter = /\d{1}/g,
    // tách chuỗi SDT thành các số đơn độc
        listNumber = phone.match(filter);
    if(listNumber == undefined){
        return false;
    }
    // kiểm tra chuỗi có chứa kí tự chữ cái nào không
    if(listNumber.length != phone.length){
        return false;
    }
    // kiểm tra chiều dài của chuỗi
    if(listNumber.length > 12 || listNumber.length <10){
        return false;
    }
    return true;
}