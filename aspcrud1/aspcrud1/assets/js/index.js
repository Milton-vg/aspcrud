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
			   			<button class="btn btn-danger" onclick="eliminar(`+ data[i].Id + `)" title="Eliminar" type="">
							<i class="fa fa-trash" aria-hidden="true"></i>
			   			</button>
			   			<button class="btn btn-primary" onclick="detalles(`+ data[i].Id + `)"  title="Ver Detalles" type="">Ver detalles
               	    	</button>
						<button class="btn btn-primary" onclick="editar(`+ data[i].Id + `)" title="Editar" type="">Editar
               	    	</button>`;
					} else {
						TablaPersonas +=  `<button class="btn btn-danger" onclick="recuperar(`+ data[i].Id + `)" title="Eliminar" type="">
							<i class="fa fa-check" aria-hidden="true"></i>`;
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

function detalles(id) {
	$.ajax({
		url: SITE_URL + '/Home/DetallesPersona',
		type: 'POST',
		data: { Id: id },
		dataType: 'JSON',
		beforeSend: function () {

			LoadingOn("Espere...");
		},
		error: function (error) {
			//console.log(error);
			MsgAlerta("Error!", error, 3000, "error");
			LoadingOff();
		},
		success: function (data) {
			//console.log(data);
			LoadingOff();

			if (data != "") {

				$("#Nombre").val(data[0].Nombre);
				$("#ApellidoP").val(data[0].ApellidoP);
				$("#ApellidoM").val(data[0].ApellidoM);
				$("#Direccion").val(data[0].Direccion);
				$("#Telefono").val(data[0].Telefono);
				
				$('#addUser').addClass('is-active');
				
			}
		}

	});

	
}

function eliminar(id) {
	$.ajax({
		url: SITE_URL + '/Home/EliminarPersona',
		contentType: "application/x-www-form-urlencoded",
		type: 'POST',
		data: { Id: id },
		dataType: 'JSON',
		beforeSend: function () {

			LoadingOn("Espere...");

		},
		success: function (data) {

			if (data) {
				console.log(data);
				LoadingOff();
				MsgAlerta("REGISTRO ELIMINADO");
				loadData();
			} else {
				MsgAlerta("ERROR AL ELIMINAR");
			}
		}
	});
}

function recuperar(id) {
	$.ajax({
		url: SITE_URL + '/Home/RecuperarPersona',
		contentType: "application/x-www-form-urlencoded",
		type: 'POST',
		data: { Id: id },
		dataType: 'JSON',
		beforeSend: function () {

			LoadingOn("Espere...");

		},
		success: function (data) {

			if (data) {
				console.log(data);
				LoadingOff();
				MsgAlerta("REGISTRO RECUPERADO");
				loadData();
			} else {
				MsgAlerta("ERROR AL RECUPERER REGISTRO");
			}
		}
	});
}

function editar(id) {
	$.ajax({
		url: SITE_URL + '/Home/DetallesPersona',
		type: 'POST',
		data: { Id: id },
		dataType: 'JSON',
		beforeSend: function () {

			LoadingOn("Espere...");
		},
		error: function (error) {
			//console.log(error);
			MsgAlerta("Error!", error, 3000, "error");
			LoadingOff();
		},
		success: function (data) {
			//console.log(data);
			LoadingOff();

			if (data != "") {

				$("#ENombre").val(data[0].Nombre);
				$("#EApellidoP").val(data[0].ApellidoP);
				$("#EApellidoM").val(data[0].ApellidoM);
				$("#EDireccion").val(data[0].Direccion);
				$("#ETelefono").val(data[0].Telefono);

				$('#EditarPersona').addClass('is-active');

			}
		}

	});
}

function Editarpersona() {
	let dato = {};
	dato.Nombre = $("#ENombre").val();
	dato.ApellidoP = $("#EApellidoP").val();
	dato.ApellidoM = $("#EApellidoM").val();
	dato.Direccion = $("#EDireccion").val();
	dato.Telefono = $("#ETelefono").val();

	//console.log(dato);
	EPersona(dato);
	
}

function EPersona(dato) {
	$.ajax({
		
		type: 'POST',
		contentType: "application/x-www-form-urlencoded",
		url: SITE_URL + '/Home/EditarPersona',
		data: dato,
		dataType: 'JSON',
		beforeSend: function () {

			LoadingOn("Espere...");
		},
		success: function (data) {
			
			console.log(data);
			if (data) {
				LoadingOff();
				MsgAlerta("REGISTRO EDITADO");
				$('#EditarPersona').removeClass('is-active');
				loadData();
			} else {
				MsgAlerta("ERROR AL EDITAR REGISTRO");
			}
		}
	});
}

function guardarp() {

	let dato = {};

	dato.Nombre = $("#Nombre").val();
	dato.ApellidoP = $("#ApellidoP").val();
	dato.ApellidoM = $("#ApellidoM").val();
	dato.Direccion = $("#Direccion").val();
	dato.Telefono = $("#Telefono").val();

	NPersona(dato);

}

function NPersona(dato) {
	$.ajax({
		url: SITE_URL + '/Home/GuardarPersona',
		type: 'POST',
		contentType: "application/x-www-form-urlencoded",
		data: dato,
		dataType: 'JSON',
		beforeSend: function () {

			LoadingOn("Espere...");
		},
		success: function (data) {
			LoadingOff();
			console.log(data);
			if (data) {
				MsgAlerta("Guardado");
				$('#addUser').removeClass('is-active');
				loadData();
			} else {
				MsgAlerta("ERROR AL INSERTAR REGISTRO");
			}
		}
	});
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

