<template>
  <div>
	<nav class="navbar navbar-default">
	  <div class="container-fluid">
		<div class="navbar-header">
		  <a class="navbar-brand" href="#">Movimentações</a>
		</div>
		<ul class="nav navbar-nav navbar-right">
		  <li>
			<a href="#" v-on:click="doLogout">
			  <span class="glyphicon glyphicon-log-out"></span> Sair
			</a>
		  </li>
		</ul>
	  </div>
	</nav>
	<div class="content">
	  <div class="container-fluid">
		<button type="button" class="btn btn-default pull-left" v-on:click="goBack">Voltar</button>
		<div class="clearfix"></div>
		<hr></hr>
		<div class="panel panel-default">
		  <div class="panel-heading">Detalhes do Recurso</div>
		  <div class="panel-body form-horizontal">
			<div class="row form-horizontal">
			  <div class="col col-sm-12 col-md-6 form-group">
				<label class="control-label col-sm-2 col-md-4" for="descricao">Descrição:</label>
				<div class="col-sm-10 col-md-8">
				  <input type="text" id="descricao" class="form-control" v-model="recurso.descricao" readonly="" />
				</div>
			  </div>
			  <div class="col col-sm-12 col-md-6 form-group">
				<label class="control-label col-sm-2 col-md-4" for="quantidade">Quantidade:</label>
				<div class="col-sm-10 col-md-8">
				  <input type="number" id="quantidade" class="form-control" v-model="recurso.quantidade" readonly="" />
				</div>
			  </div>
			  <div class="col col-sm-12 form-group">
				<label class="control-label col-sm-2" for="observacao">Observação:</label>
				<div class="col-sm-10">
				  <textarea id="observacao" class="form-control" v-model="recurso.observacao" readonly="" />
				</div>
			  </div>
			</div>
		  </div>
		  <div class="panel-footer">
			<div class="clearfix">
			  <button type="button" class="btn btn-primary pull-left" v-on:click="showCadastrar">Nova Movimentação</button>
			  <button type="button" class="btn btn-info pull-right" v-on:click="doAtualizar">Atualizar</button>
			</div>
		  </div>
		</div>
		<table class="table table-striped table-bordered table-hover">
		  <thead>
			<tr>
			  <th>Usuario</th>
			  <th>Tipo</th>
			  <th>Quantidade</th>
			  <th>Ações</th>
			</tr>
		  </thead>
		  <tbody>
			<tr v-for="(movimentacao, index) in movimentacoes">
			  <td class="col col-sm-4">{{ movimentacao.usuarioNome }}</td>
			  <td class="col col-sm-2">{{ tipos.filter((t) => t.tipoMovimentacaoID == movimentacao.tipoMovimentacaoID)[0].descricao }}</td>
			  <td class="col col-sm-2">{{ movimentacao.quantidade }}</td>
			  <td class="col col-sm-4">
				<button type="button" class="btn btn-info" aria-label="Left Align" v-on:click="showAtualizar(movimentacao)" v-bind:readonly="movimentacao.isOwner">
				  <span class="glyphicon glyphicon-edit" aria-hidden="true"></span> Modificar
				</button>
				<button type="button" class="btn btn-danger" aria-label="Left Align" v-on:click="showApagar(movimentacao)" v-bind:readonly="movimentacao.isOwner">
				  <span class="glyphicon glyphicon-trash" aria-hidden="true"></span> Apagar
				</button>
			  </td>
			</tr>
		  </tbody>
		</table>
	  </div>
	</div>
	<div class="modal fade" id="modal" role="dialog">
	  <div class="modal-dialog">
		<!-- Modal content-->
		<div class="modal-content">
		  <div class="modal-header">
			<button type="button" class="close" data-dismiss="modal">&times;</button>
			<h4 class="modal-title">{{ acao }} - Movimentação</h4>
		  </div>
		  <div class="modal-body">
			<div class="row form-horizontal">
			  <div class="col col-sm-12 col-md-6 form-group">
				<label class="control-label col-sm-2 col-md-4" for="usuario">Usuario:</label>
				<div class="col-sm-10 col-md-8">
				  <input type="text" id="usuario" class="form-control" v-model="movimentacao.usuarioNome" readonly="" />
				</div>
			  </div>
			  <div class="col col-sm-12 col-md-6 form-group">
				<label class="control-label col-sm-2 col-md-4" for="quantidade">Tipo:</label>
				<div class="col-sm-10 col-md-8">
				  <select id="quantidade" class="form-control" v-model="movimentacao.tipoMovimentacaoID"  v-bind:readonly="verb == 'delete'" >
					<option v-for="(tipo, indice) in tipos" v-bind:value="tipo.tipoMovimentacaoID">{{tipo.descricao}}</option>
				  </select>
				</div>
			  </div>
			  <div class="col col-sm-12 col-md-6 form-group">
				<label class="control-label col-sm-2 col-md-4" for="estoque">Estoque:</label>
				<div class="col-sm-10 col-md-8">
				  <input type="text" id="estoque" class="form-control" v-model="recurso.quantidade" readonly="" />
				</div>
			  </div>
			  <div class="col col-sm-12 col-md-6 form-group">
				<label class="control-label col-sm-2 col-md-4" for="mov_quantidade">Quantidade:</label>
				<div class="col-sm-10 col-md-8">
				  <input type="number" id="mov_quantidade" class="form-control" v-model="movimentacao.quantidade" v-bind:readonly="verb == 'delete'" />
				</div>
			  </div>
			</div>
		  </div>
		  <div class="modal-footer">
			<button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
			<button type="button" class="btn btn-primary" v-on:click="doSalvar">{{ acao }}</button>
		  </div>
		</div>
	  </div>
	</div>
  </div>
</template>

<script src="./App.js">

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped="" src="./App.css">

</style>
