import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/components/Home/App.vue'
import Login from '@/components/Login/App.vue'
import Signup from '@/components/Signup/App.vue'
import Recurso from '@/components/Recurso/App.vue'
import Movimentacao from '@/components/Movimentacao/App.vue'

Vue.use(Router)

let router = new Router({
  routes: [
    { path: '*', redirect: '/home' },
    { path: '/home', component: Home },
    { path: '/login', component: Login },
    { path: '/signup', component: Signup },
    { path: '/recurso', component: Recurso },
    { path: '/movimentacao/:recurso_id', component: Movimentacao }
  ]
})

export default router
