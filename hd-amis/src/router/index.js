import Vue from 'vue'
import VueRouter from 'vue-router'
import MainContent from "../components/MainContent.vue"
import HelloWorld from "../components/HelloWorld.vue"

Vue.use(VueRouter)

const routes = [
  {path :"", component:MainContent},
  {path:"/helloWorld", component:HelloWorld}
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
