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
		<button type="button" class="btn btn-primary pull-left" v-on:click="showCadastrar">Novo</button>
		<button type="button" class="btn btn-info pull-right" v-on:click="doAtualizar">Atualizar</button>
		<div class="clearfix"></div>
		<hr></hr>
		<table class="table table-striped table-bordered table-hover">
		  <thead>
			<tr>
			  <th>Descrição</th>
			  <th>Quantidade</th>
			  <th>Ações</th>
			</tr>
		  </thead>
		  <tbody>
			<tr v-for="(recurso, index) in recursos">
			  <td class="col col-sm-5">{{ recurso.descricao }}</td>
			  <td class="col col-sm-2">{{ recurso.quantidade }}</td>
			  <td class="col col-sm-5">
				<button type="button" class="btn btn-primary" aria-label="Left Align" v-on:click="goMovimentacoes">
				  <span class="glyphicon glyphicon-list-alt" aria-hidden="true"></span> Detalhes
				</button>
				<button type="button" class="btn btn-info" aria-label="Left Align" v-on:click="showAtualizar(recurso)">
				  <span class="glyphicon glyphicon-edit" aria-hidden="true"></span> Modificar
				</button>
				<button type="button" class="btn btn-danger" aria-label="Left Align" v-on:click="showApagar(recurso)">
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
			<h4 class="modal-title">{{ acao }} - Recurso</h4>
		  </div>
		  <div class="modal-body">
			<div class="row form-horizontal">
			  <div class="col col-sm-12 col-md-6 form-group">
				<label class="control-label col-sm-2 col-md-4" for="descricao">Descrição:</label>
				<div class="col-sm-10 col-md-8">
				  <input type="text" id="descricao" class="form-control" v-model="recurso.descricao" v-bind:readonly="verb == 'delete'" />
				</div>
			  </div>
			  <div class="col col-sm-12 col-md-6 form-group">
				<label class="control-label col-sm-2 col-md-4" for="quantidade">Quantidade:</label>
				<div class="col-sm-10 col-md-8">
				  <input type="number" id="quantidade" class="form-control" v-model="recurso.quantidade" v-bind:readonly="verb == 'delete'" />
				</div>
			  </div>
			  <div class="col col-sm-12 form-group">
				<label class="control-label col-sm-2" for="observacao">Observação:</label>
				<div class="col-sm-10">
				  <textarea id="observacao" class="form-control" v-model="recurso.observacao" v-bind:readonly="verb == 'delete'" />
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
