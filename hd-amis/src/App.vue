<template>
  <v-app>
    <div class="wraper">
      <Sidebar/>
      <div class="main-content">
        <Header/>
        <!-- <MainContent ref ="mainct"/> -->
        <router-view></router-view>
      </div>
      <Form/>
      <ReportForm/>
    </div>    
  </v-app>
</template>

<script>
import Header from "@/components/Header.vue";
import Sidebar from "@/components/Sidebar.vue";
// import MainContent from "@/components/MainContent.vue";
import Form from "@/components/Form.vue";
import ReportForm from '@/components/ReportForm.vue';
import "./assets/style/Common.css";
export default {
  name: 'App',
  components:{
    Header, Sidebar,Form,ReportForm,
  },
  data: () => ({
    //
  }),
};
</script>
