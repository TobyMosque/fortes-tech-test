// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import '../wwwroot/css/app.css'
import '@deveodk/vue-toastr/dist/@deveodk/vue-toastr.css'

import Vue from 'vue'
import VueToastr from '@deveodk/vue-toastr'
import resource from 'vue-resource'

import App from './root/App.vue'
import router from './router'

import '../wwwroot/scripts/bootstrap-sass/bootstrap.min.js'

Vue.use(VueToastr, { defaultPosition: 'toast-bottom-full-width' })
Vue.use(resource)
Vue.config.productionTip = false

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  template: '<App/>',
  components: { App }
})
