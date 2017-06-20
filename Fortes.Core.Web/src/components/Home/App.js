export default {
	name: 'home',
	data() {
		return {

		}
	},
	created() {
		var token = localStorage.getItem('token')
		if (token) {
			this.$router.push('/recurso')
		} else {
			this.$router.push('/login')
		}
	}
}
