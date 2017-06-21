var Usuario = function () {
	this.usuarioId = null;
	this.logon = '';
}

var Recurso = function () {
	this.recursoID = null;
	this.descricao = '';
	this.quantidade = 0;
	this.observacao = '';
}

var Movimentacao = function (usuario = {}, recurso = {}, tipo = {}) {
	this.movimentacaoID = null;
	this.recursoID = recurso.recursoID;
	this.usuarioID = usuario.usuarioId;
	this.tipoMovimentacaoID = tipo.tipoMovimentacaoID;
	this.quantidade = 0;
	this.usuarioNome = usuario.logon;
	this.isOwner = true;
}

export default {
	name: 'movimentacao',
	data() {
		return {
			acao: 'Cadastrar',
			verb: 'post',
			usuario: new Usuario(),
			recurso: new Recurso(),
			movimentacao: new Movimentacao(),
			atual: null,
            tipos: [],
			movimentacoes: []
		}
	},
	created() {
		this.doAtualizar()		
	},
	methods: {
		showCadastrar: function (event) {
			this.acao = 'Cadastrar'
			this.verb = 'post'
			this.atual = {}
			this.movimentacao = new Movimentacao(this.usuario, this.recurso, this.tipos[0])
			$("#modal").modal('show')
		},
		showAtualizar: function (movimentacao) {
			this.acao = 'Atualizar'
			this.verb = 'put'
			this.atual = movimentacao
			this.movimentacao = Object.assign({}, movimentacao)
			$("#modal").modal('show')
		},
		showApagar: function (movimentacao) {
			this.acao = 'Apagar'
			this.verb = 'delete'
			this.atual = movimentacao
			this.movimentacao = Object.assign({}, movimentacao)
			$("#modal").modal('show')
		},
		doAtualizar: function (event) {
			this.$toastr('info', 'Carregando Movimentações', 'Fortes Core')			
			this.$http.get("api/Movimentacao/" + this.$route.params.recurso_id)
				.then((res) => {
					this.usuario = res.body.usuario
					this.recurso = res.body.recurso
					this.movimentacoes = res.body.movimentacoes
					this.tipos = res.body.tipos
				})
		},
		doSalvar: function (event) {
			var url, data;
			if (this.verb == 'delete') {
				url = "api/Movimentacao/" + this.movimentacao.movimentacaoID
				data = null
			} else {
				url = "api/Movimentacao"
				data = {
					MovimentacaoID: this.movimentacao.movimentacaoID,
					RecursoID: this.movimentacao.recursoID,
					UsuarioID: this.movimentacao.usuarioID,
					TipoMovimentacaoID: this.movimentacao.tipoMovimentacaoID,
					Quantidade: this.movimentacao.quantidade,
					UsuarioNome: this.movimentacao.usuarioNome,
					IsOwner: this.movimentacao.isOwner
				}
			}
			this.$http[this.verb](url, data)
				.then((res) => {
					var movimentacaoID = res.body.movimentacaoID
					var estoque = res.body.estoque
					this.movimentacao.movimentacaoID = movimentacaoID

					this.recurso.quantidade = estoque
					switch (this.verb)
					{
						case 'post':
							var atual = Object.assign({}, this.movimentacao)
							this.movimentacoes.push(atual)
							this.$toastr('success', 'Movimentação inserida com sucesso', 'Fortes Core')
							break
						case 'put':
							var indice = this.movimentacoes.indexOf(this.atual)
							this.movimentacoes[indice].tipoDescricao = this.tipos[this.atual.tipoMovimentacaoID]
							this.movimentacoes[indice].quantidade = this.atual.quantidade
							this.$toastr('success', 'Movimentação atualizada com sucesso', 'Fortes Core')
							break
						case 'delete':
							var indice = this.movimentacoes.indexOf(this.atual)
							this.movimentacoes.splice(indice, 1)
							this.atual = Object.assign({}, this.movimentacao)
							this.$toastr('success', 'Movimentação apagada com sucesso', 'Fortes Core')
							break
					}
					
					$("#modal").modal('hide')
				})
		},
		doLogout: function (event) {
			event.preventDefault();
			this.$http.post("api/Usuario/logout", data)
				.then((res) => {
					localStorage.removeItem('token');
					this.$toastr('success', 'Logout efetuado com Sucesso', 'Fortes Core')
					this.$router.push('/login')
				})
		},
		goBack: function (event) {
			this.$router.push('/recurso')
		}
	}
}
