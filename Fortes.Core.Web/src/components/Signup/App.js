export default {
	name: 'login',
	data() {
		return {
			logon: '',
			senha: '',
			grupoId: 0,
			grupos: []
		}
	},
	created() {
		this.$toastr('info', 'Carregando Grupos', 'Fortes Core')
		this.$http.get("api/Auth/grupos")
			.then((res) => {
				this.grupos = res.body
			})
	},
	methods: {
		doSignup: function (event) {
			var data = { GrupoId: this.grupoId, Logon: this.logon, Senha: this.senha }
			this.$http.post("api/Auth/signup", data)
				.then((res) => {
					localStorage.setItem('token', res.body);
					this.$toastr('success', 'Login efetuado com Sucesso', 'Fortes Core')
					this.$router.push('/recurso')
				})
		},
		goLogin: function (event) {
			this.$router.push('/login')
		}
	}
}