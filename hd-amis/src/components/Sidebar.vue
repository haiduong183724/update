<template>
    <div class="sidebar">
        <div class="logo-container">
            <div class="left-logo">
                <a href="#">
                    <img src="../assets/icon/menu.svg" alt="">
                </a>
            </div>
            <a href="#">
                <img src="https://actappg1.misacdn.net/img/Logo_Module_TiengViet_White.66947422.svg" alt="amistlogo" class="img-icon">
            </a>
        </div>
        <div class="menu-item-list scroll">            
            <div class="item-choose">
                <router-link to="/">
                <a href="#" data-title = "Tổng quan" style="width:100%">
                    <div class="icon-wrap">
                        <img src="../assets/icon/sumary.svg" alt="">
                    </div>
                    <div class="menu-item-title">
                        Tổng quan
                    </div>
                </a>
                </router-link>
            </div>
            <div class="item-choose">
                <router-link to="/helloWorld">
                <a href="#" data-title = "Tiền mặt" style="width:100%">
                    <div class="icon-wrap">
                       <img src="../assets/icon/cash.svg" alt="">
                    </div>
                    <div class="menu-item-title">
                        Tiền mặt
                    </div>
                </a>
                </router-link>
            </div>
            <div class="item-choose">
                <a href="#" data-title = "Tiền gửi">
                    <div class="icon-wrap">
                        <img src="../assets/icon/keepmoney.svg" alt="">
                    </div>
                    <div class="menu-item-title">
                        Tiền gửi
                    </div>
                </a>
            </div>
            <div class="item-choose" >
                <a href="#" data-title = "Mua hàng">
                    <div class="icon-wrap">
                        <img src="../assets/icon/shop.svg" alt="">
                    </div>
                    <div class="menu-item-title">
                        Mua hàng
                    </div>
                </a>
            </div>
            <div class="item-choose">
                <a href="#" data-title = "Bán hàng">
                    <div class="icon-wrap">
                        <img src="../assets/icon/sell.svg" alt="">
                    </div>
                    <div class="menu-item-title">
                        Bán hàng
                    </div>
                </a>
            </div>
            <div class="item-choose">
                <a href="#" data-title = "Quản lý hóa đơn">
                    <div class="icon-wrap">
                        <img src="../assets/icon/manage.svg" alt="">
                    </div>
                    <div class="menu-item-title">
                        Quản lý hóa đơn
                    </div>
                </a>
            </div>
            <div class="item-choose">
                <a href="#" data-title = "Kho">
                    <div class="icon-wrap">
                        <img src="../assets/icon/repo.svg" alt="">
                    </div>
                    <div class="menu-item-title">
                        Kho
                    </div>
                </a>
            </div>
            <div class="item-choose">
                <a href="#" data-title = "Công cụ sử dụng">
                    <div class="icon-wrap">
                        <img src="../assets/icon/tool.svg" alt="">
                    </div>
                    <div class="menu-item-title">
                        Công cụ sử dụng
                    </div>
                </a>
            </div>
            <div class="item-choose">
                <a href="#" data-title = "Tài sản cố định">
                    <div class="icon-wrap">
                        <img src="../assets/icon/assets.svg" alt="">
                    </div>
                    <div class="menu-item-title">
                        Tài sản cố định
                    </div>
                </a>
            </div>
            <div class="item-choose">
                <a href="#" data-title = "Thuế">
                    <div class="icon-wrap">
                        <img src="../assets/icon/tax.svg" alt="">
                    </div>
                    <div class="menu-item-title">
                        Thuế
                    </div>
                </a>
            </div>
            <div class="item-choose">
                <a href="#" data-title = "Giá thành">
                    <div class="icon-wrap">
                        <img src="../assets/icon/price.svg" alt="">
                    </div>
                    <div class="menu-item-title">
                        Giá thành
                    </div>
                </a>
            </div>
            <div class="item-choose">
                <a href="#" data-title = "Tổng hợp">
                    <div class="icon-wrap">
                        <img src="../assets/icon/synthetic.svg" alt="">
                    </div>
                    <div class="menu-item-title">
                        Tổng hợp
                    </div>
                </a>
            </div>
            <div class="item-choose">
                <a href="#" data-title = "Ngân sách">
                    <div class="icon-wrap">
                        <img src="../assets/icon/budget.svg" alt="">
                    </div>
                    <div class="menu-item-title">
                        Ngân sách
                    </div>
                </a>
            </div>
            <div class="item-choose">
                <a href="#" data-title = "Báo cáo">
                    <div class="icon-wrap">
                        <img src="../assets/icon/report.svg" alt="">
                    </div>
                    <div class="menu-item-title">
                        Báo cáo
                    </div>
                </a>
            </div>
            <div class="item-choose">
                <a href="#" data-title = "Phân tích tài chính">
                    <div class="icon-wrap">
                        <img src="../assets/icon/analysis.svg" alt="">
                    </div>
                    <div class="menu-item-title">
                        Phân tích tài chính
                    </div>
                </a>
            </div>
        </div>
    </div>
</template>

<script>

export default {  
}
</script>

<style scoped>
@import "../assets/style/sidebar.css";
</style>