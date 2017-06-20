export default {
	name: 'recurso',
	data() {
		return {
			acao: 'Cadastrar',
            verb: 'post',
			recurso: {
				recursoID: null,
				descricao: '',
				quantidade: 0,
                observacao: '',
			},
			atual: null,
            recursos: []
		}
	},
	created() {
		console.log(this.$route.params)
	},
	methods: {
		showCadastrar: function (event) {
			this.acao = 'Cadastrar'
			this.verb = 'post'
			this.atual = {}
			$("#modal").modal('show')
		},
		showAtualizar: function (recurso) {
			this.acao = 'Atualizar'
			this.verb = 'put'
			this.atual = recurso
			this.recurso = Object.assign({}, recurso)
			$("#modal").modal('show')
		},
		showApagar: function (recurso) {
			this.acao = 'Apagar'
			this.verb = 'delete'
			this.atual = recurso
			this.recurso = Object.assign({}, recurso)
			$("#modal").modal('show')
		},
		goMovimentacoes: function (event) {
			this.$router.push('/Movimentacao')
		},
		doAtualizar: function (event) {
			this.$toastr('info', 'Carregando Recursos', 'Fortes Core')			
			this.$http.get("api/Recurso")
				.then((res) => {
					this.recursos = res.body
				})
		},
		doSalvar: function (event) {
			var data = {
				RecursoID: this.recurso.recursoID,
				Descricao: this.recurso.descricao,
				Quantidade: this.recurso.quantidade,
				Observacao: this.recurso.observacao,
			}
			this.$http[this.verb]("api/Recurso", data)
				.then((res) => {
					var recursoID = res.body
					this.recurso.recursoID = recursoID

					switch (this.verb)
					{
						case 'post':
							var atual = Object.assign({}, this.recurso)
							this.recursos.push(atual)
							this.$toastr('success', 'Recurso inserido com sucesso', 'Fortes Core')
							break
						case 'put':
							this.atual = Object.assign({}, this.recurso)
							this.$toastr('success', 'Recurso atualizado com sucesso', 'Fortes Core')
							break
						case 'delete':
							var indice = this.recursos.indexOf(this.atual)
							this.recursos.splice(indice, 1)
							this.atual = Object.assign({}, this.recurso)
							this.$toastr('success', 'Recurso apagado com sucesso', 'Fortes Core')
							break
					}
					
					$("#modal").modal('hide')
				})
		},
		doLogout: function (event) {
			this.$http.post("api/Usuario/logout", data)
				.then((res) => {
					localStorage.removeItem('token');
					this.$toastr('success', 'Logout efetuado com Sucesso', 'Fortes Core')
					this.$router.push('/login')
				})
		}
	}
}
