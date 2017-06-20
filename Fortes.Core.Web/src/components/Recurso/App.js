var Recurso = function () {
	this.recursoID = null;
	this.descricao = '';
	this.quantidade = 0;
	this.observacao = '';
}

export default {
	name: 'recurso',
	data() {
		return {
			acao: 'Cadastrar',
            verb: 'post',
			recurso: new Recurso(),
			atual: null,
            recursos: []
		}
	},
	created() {
		this.doAtualizar()
	},
	methods: {
		showCadastrar: function (event) {
			this.acao = 'Cadastrar'
			this.verb = 'post'
			this.recurso = new Recurso()
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
		goMovimentacoes: function (recurso) {
			console.log(recurso.recursoID)
			this.$router.push('/movimentacao/' + recurso.recursoID)
		},
		doAtualizar: function (event) {
			this.$toastr('info', 'Carregando Recursos', 'Fortes Core')			
			this.$http.get("api/Recurso")
				.then((res) => {
					this.recursos = res.body
				})
		},
		doSalvar: function (event) {
			var url, data;
			if (this.verb == 'delete') {
				url = "api/Recurso/" + this.recurso.recursoID
				data = null
			} else {
				url = "api/Recurso"
				data = {
					RecursoID: this.recurso.recursoID,
					Descricao: this.recurso.descricao,
					Quantidade: this.recurso.quantidade,
					Observacao: this.recurso.observacao,
				}
			}
			this.$http[this.verb](url, data)
				.then((res) => {
					switch (this.verb)
					{
						case 'post':
							var atual = Object.assign({}, this.recurso)
							atual.recursoID = res.body
							this.recursos.push(atual)
							this.$toastr('success', 'Recurso inserido com sucesso', 'Fortes Core')
							break
						case 'put':
							var indice = this.recursos.indexOf(this.atual)
							this.recursos[indice].descricao = this.recurso.descricao
							this.recursos[indice].observacao = this.recurso.observacao
							this.recursos[indice].quantidade = this.recurso.quantidade
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
