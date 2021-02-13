$(function () {

	loadData();

});

var error = "Ocurrió un error insesperado en el sitio, por favor intentelo mas tarde o pongase en contacto con su administrador.";
var success = "La accion se ralizó con exito";
var datosIncorrectos = "Datos incorrectos, vuelve a intentarlo.";

function loadData(){

	var filtro = $('#select_status').val();

	$.ajax({
		url: SITE_URL +'/Home/TablaPersonas',
		type:'POST',
		data: { Filtro: filtro},
		dataType:'JSON',
		beforeSend: function () {

			LoadingOn("Espere...");
			$("#tbody").empty();
		},
		error: function(error){
			//console.log(error);
			MsgAlerta("Error!", error, 3000, "error");
			LoadingOff();
		},
		success: function(data){
			//console.log(data);
			LoadingOff();

			if(data != ""){

				var TablaPersonas = "";

				for (var i = 0; i < data.length; i++) {
					//Console.log(data[i]);
					TablaPersonas += '<tr>';
					TablaPersonas += '<td>' + data[i].Id + '</td>';
					TablaPersonas += '<td>' + data[i].Nombre + '</td>';
					TablaPersonas += '<td>' + data[i].Direccion + '</td>';
					TablaPersonas += '<td>' + data[i].Telefono + '</td>';
					TablaPersonas += '<td>' + data[i].Estatus + '</td>';
					TablaPersonas += '<td>';
					if (data[i].Estatus == 1) {
						TablaPersonas += `
			   			<button class="btn btn-danger" onclick="eliminar(`+ data[i].id + `)" title="Eliminar" type="">
							<i class="fa fa-trash" aria-hidden="true"></i>
			   			</button>
			   			<button class="btn btn-primary" onclick="detalles(`+ data[i].id + `)"  title="Ver Detalles" type="">Ver detalles
               	    	</button></tr>`;
					}

				}

				$('#tbody').html(TablaPersonas);
			}
			else{
				MsgAlerta("Atencion!", "No hay personas para mostrar", 5000, "warning");
			}
		}
	});

}

function detalles(id){

}

function eliminar(id){

}

function editar(id) {

}

$(document).on('change', '#select_status', function(e){
	loadData();
});


$(document).on('keyup', '#txt_busqueda', function (e) {

	$.ajax({
		url: SITE_URL + '/Home/TablaPersonasbusqueda',
		type:'POST',
		async: false,
		data: { Busqueda: $(this).val()},
		dataType:'JSON',
		beforeSend: function () {

			LoadingOn("Espere...");
			$("#tbody").empty();

		},
		error: function(error){
			//console.log(error);
			MsgAlerta("Error!", error, 5000, "error");
			LoadingOff();
		},
		success: function(data){
			console.log(data);
			LoadingOff();

			if (data != "") {

				var TablaPersonas = "";

				for (var i = 0; i < data.length; i++) {
					//Console.log(data[i]);
					TablaPersonas += '<tr>';
					TablaPersonas += '<td>' + data[i].Id + '</td>';
					TablaPersonas += '<td>' + data[i].Nombre + '</td>';
					TablaPersonas += '<td>' + data[i].Direccion + '</td>';
					TablaPersonas += '<td>' + data[i].Telefono + '</td>';
					TablaPersonas += '<td>' + data[i].Estatus + '</td>';
					TablaPersonas += '<td>';
					if (data[i].Estatus == 1) {
						TablaPersonas += `
			   			<button class="btn btn-danger" onclick="eliminar(`+ data[i].id + `)" title="Eliminar" type="">Eliminar
			   			</button>
			   			<button class="btn btn-primary" onclick="detalles(`+ data[i].id + `)"  title="Ver Detalles" type="">Ver detalles
               	    	</button></tr>`;
					}

				}

				$('#tbody').html(TablaPersonas);
			}
			else {
				MsgAlerta("Atencion!", "No hay personas para mostrar", 5000, "warning");
			}
		}
	});
});

// validaOnlyNumbers('txt_busqueda');