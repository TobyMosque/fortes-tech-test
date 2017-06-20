<template>
  <div id="signup-form" class="panel panel-default">
	<div class="panel-heading">Cadastro - Controle de Recursos</div>
	<div class="panel-body form-horizontal">
	  <div class="form-group">
		<label class="control-label col-sm-2" for="grupoId">Grupo:</label>
		<div class="col-sm-10">
		  <select id="grupoId" class="form-control" v-model="grupoId">
			<option v-for="grupo in grupos" v-bind:value="grupo.grupoID">
			  {{ grupo.descricao }}
			</option>
		  </select>
		</div>
	  </div>
	  <div class="form-group">
		<label class="control-label col-sm-2" for="logon">Logon:</label>
		<div class="col-sm-10">
		  <input type="text" id="logon" class="form-control" v-model="logon" />
		</div>
	  </div>
	  <div class="form-group">
		<label class="control-label col-sm-2" for="senha">Senha:</label>
		<div class="col-sm-10">
		  <input type="password" id="senha" class="form-control"  v-model="senha" />
		</div>
	  </div>
	</div>
	<div class="panel-footer">
	  <div class="clearfix">
		<button type="button" class="btn btn-default pull-left" v-on:click="goLogin">Já tenho Cadastro</button>
		<button type="button" class="btn btn-default pull-right" v-on:click="doSignup">Efetuar Cadastro</button>
	  </div>
	</div>
  </div>
</template>

<script src="./App.js">

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped="" src="./App.css">

</style>
