.<template>
    <div class="rp-form-wraper hidden" :class="{'show':show}">
        <div class="pop-up-form d-flex">
            <div class="popup-content">
                <div class="form-rp-content">
                    <div class="right-rp-icon">
                        <img src="../assets/icon/warning.svg" alt="" >
                    </div>
                    <div class="content-text">
                        <p>{{content}}</p>
                    </div>
                </div>
                <div class="form-rp-bottom">
                    <button class="btn  green-btn" @click="OK()">{{Okbtn}}</button>
                    <button class="btn default-btn hidden" :class="{'show': formMode == 'Delete'}" @click="close()">Không</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    props:{
        title:String,
        content:String,
        formMode:String,
        show:Boolean
    },
    mounted(){
        if(this._props.formMode == "Delete"){
            this.Okbtn = 'Có';
        }
    },
    data(){
        return{
            Okbtn:'Đồng ý',
        }
    },
    methods:{
        close(){
            this.$emit('close');
        },
        OK(){
            this.$emit('Ok');
        }
    },
}
</script>

<style scoped>
    .rp-form-wraper{
        display: flex;
        align-items: center;
        background: rgba(0, 0 ,0,0.5);
        justify-content: center;
        width: 100vw;
        height: 100vh; 
        z-index: 100000;
        position: fixed;
        top:0;
        left: 0;   
    }
    .rp-form-wraper .d-flex{
        display: flex;
        flex-direction: column;
    }
    .rp-form-wraper .pop-up-form{
        width:444px;
        height: 200px;
        background: white;
        padding: 32px;
    }

    .rp-form-wraper .popup-content{
        flex: 1;
        padding-bottom: 32px;
    }
    .form-title h2{
        padding: 0 24px 24px 0;
        font-family: 'GoogleSans-Bold';
        font-size:15px ;
    }
    .form-rp-content{
        display: flex;
        height: 80px;
        padding-bottom:24px ;
        border-bottom: 2px solid #8d9096;
    }
    .right-rp-icon{
        height: 100%;
        border-radius: 50%;
        padding-right:10px ;
        height: 48px;
    }
    .right-rp-icon img{
        height: 100%;
    }
    .content-rp-text{
        padding: 16px 0 0 12px;
    }
    .content-rp-text p{
        font-family: "notosans-regular";
        font-size: 13px;
    }
    .form-rp-bottom{
        padding: 20px 0;
        display: flex;
        justify-content: space-between;
        flex-direction: row-reverse;
    }
    .btn{
        height: 36px;
        font-family: "notosans-bold";
        font-size: 13px;
        outline: 0;
        padding: 8px 20px;
        border-radius: 4px;
    }
    .green-btn{
        background: #2ca01c;
        color: white;
    }
    .green-btn:hover{
        background: #35bf22;
    }
    .green-btn:active{
        background: #248b15;
    }
    .default-btn{
        border: 1px solid #8d9096;
    }
    .default-btn:hover{
        background: #8d9096;
    }
    .default-btn:active{
        background: #bbbcbc;
    }
    .hidden{
        display: none;
    }
    .show{
        display: block;
    }
</style>