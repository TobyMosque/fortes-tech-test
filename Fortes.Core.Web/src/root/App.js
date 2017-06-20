import Vue from 'vue'
import router from '@/router'

export default {
	name: 'app',
	created() {
		Vue.http.interceptors.push(function (request, next) {
			request.headers.set('token', localStorage.getItem('token'));
			next(function (response) {
				if (response.status === 401) {					
					router.push('/login')
					this.$toastr('info', 'É necessario efetuar o Login para continuar.', 'Fortes Core')
				} else if (response.status !== 200) {
					this.$toastr('error', response.body, 'Fortes Core - Error')
				}
			});
		});
	}
}