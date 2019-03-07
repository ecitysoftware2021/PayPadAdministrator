var usuario= {
    
Listar : function(){
    jQuery.ajax({
        statusCode: {
            404: function() {
            }
        },
        data: {
            'id': '',
            'username':'',
            'q': 2
        },
        type: "POST",
        dataType: "json",
        url: "Controller/Ctrol_usuario.php"
    }).done(function (data, textStatus, jqXHR) {
         if (data != 0){   
            var tabla = "<table id='dev-table' class='table table-hover'><thead><tr><th>Cédula</th><th>Nombre</th><th>Apellido</th><th>Fecha creacion</th><th>Opciones</th></tr></thead><tbody>";
            var count = 0;
            var c = "";
            for(var i = 0; i < data.length; i++){
                tabla += "<tr><td style='display:none;'>"+data[i].id_usuario+"</td><td 'id='identificacion_usuario'>"+data[i].identificacion_usuario+"</td><td>"+data[i].nombre_usuario+"</td><td>"+data[i].apellidos_usuario+"</td><td>"+data[i].fecha_creacion+"</td><td><a id='formM"+count+"' class='btn btn-success btn-xs' data-toggle='modal' href='#myModalModificar' onclick='usuario.MontarDato("+data[i].id_usuario+");' id='btn_modificar' title='Modificar' style='cursor: pointer;'><i class='fa fa-pencil fa-lg'></i><input type='hidden' value='"+data[i].id_usuario+"' id='id_usuario' name='id_usuario'></a>&nbsp;&nbsp;&nbsp;<input type='hidden' value='"+data[i].id_usuario+"' name='id_usuario'><a class='btn btn-danger btn-xs' id='formM"+count+"' onclick='usuario2.Eliminar("+data[i].id_usuario+");' id='btnEdit' title='Eliminar' style='cursor: pointer;'><i class='fa fa-trash-o fa-lg'></i><input type='hidden' value='"+data[i].id_usuario+"' id='id_usuario' name='id_usuario'></a></td><td style='display:none;'></tr>";
            count++;
            }
            tabla += '</tbody></table>';
            $('#Listar').html(tabla);
        }else{
            alert('No existen datos');
        }
    }).fail(function (qXHR, textStatus, errorThrown) {
    }
    ).always(function (data, textStatus, errorThrown) {
        
    });
  },
  MontarDato:function(dato){
    sessionStorage.setItem('id_usuario',dato);
    return true;
  }
}

 var usuario1 = {  
  Editar : function(Edit){
    jQuery.ajax({
      statusCode: {
        404: function() {
          alert('Error 404!');
        }
      },
      data: {
        'id_usuario': Edit,
        'q': 6
      },
      type: "POST",
      dataType: "json",
      url: "Controller/Ctrol_usuario.php"
    }).done(function (data, textStatus, jqXHR) {
      if (data != 0){         
         $("#txt_id").val(data[0].id_usuario);
         $("#txt_identificacion_mod").val(data[0].identificacion_usuario);
         $("#txt_nombre_mod").val(data[0].nombre_usuario);
         $("#txt_apellido_mod").val(data[0].apellidos_usuario);
         $("#txt_fecha_crea_mod").val(data[0].fecha_creacion);
         $("#txt_rol_mod").val(data[0].tipo_rol);
         $("#txt_clave_mod").val(data[0].clave);
      }else{
        alert('Error');
      }
    }).fail(function (qXHR, textStatus, errorThrown) {
      alert('Error');
    });
  },
  ModificarUsuario : function(id){
        jQuery.ajax({
                statusCode: {
              },
              data: {
                  'id_usuario': $('#txt_id').val(),
                  'identificacion_usuario': $('#txt_identificacion_mod').val(),
                  'nombre_usuario': $('#txt_nombre_mod').val(),
                  'apellidos_usuario': $('#txt_apellido_mod').val(),
                  'clave':$('#txt_clave_mod').val(),
                  'fecha_creacion':$('#txt_fecha_crea_mod').val(),
                  'tipo_rol':$('#txt_rol_mod').val(),
                  'q': 9
              },
              type: "POST",
              dataType: "json",
              url: "Controller/Ctrol_usuario.php"      
          }).done(function (data, textStatus, jqXHR) {
              if (data == 1){
                  alert('Modificación exitosa');
                  location.href='usuarios.php';
              }else{
                alert('El servicio no esta disponible, Intentelo de nuevo');
              }
          }).fail(function (qXHR, textStatus, errorThrown) {
            alert('¡Error!');            
          }
          ).always(function (data, textStatus, errorThrown) {
            alert(data);
        });
          
      }

 }

var usuario2 = {
  RegistrarUsuarios: function() {
    debugger;
        // if(valida.vali(id)){
            jQuery.ajax({
                statusCode: {
                    404: function() {
                    }
                },
                data: {            
                    'email':$("#txtEmail").val(),
                    'identificacion_usuario':$('#txtIdentification').val(),
                    'nombre_usuario': $('#txtName').val(),                    
                    'apellidos_usuario': $('#txtLasName').val(),                    
                    'clave':$('#txtpassword').val(),   
                    'datebi':$('#datepicker').val(),   
                    'quarter':$('#txtquater').val(),   
                    'department':$('#txtDepartment').val(),   
                    'city':$('#txtcity').val(),   
                    'cellphone':$('#txtcellphone').val(),              
                    'phone':$('#txtphone').val(),  
                    'address':$('#txtaddress').val(),              
                    'id_rol':1,
                    'q': 1
                },
                type: "POST",
                dataType: "json",
                url: "Controller/UserController.php"
            }).done(function (data, textStatus, jqXHR) {
                if (data == 1){
                      debugger;
                             swal({   
                                    title: "Good job!",   
                                    text: "Usuario registrado correctamente",   
                                    type: "success",   
                                    showCancelButton: false,   
                                    confirmButtonColor: "#DD6B55",                                       
                                    closeOnConfirm: false ,
                                    closeOnConfirm: false,   
                                    closeOnCancel: false 
                                }, function(isConfirm){   
                                      if (isConfirm) {     
                                          location.href='index.html';  
                                      }  
                                  });
                      //swal("Good job!", "Usuario registrado correctamente", "success");
                      
                }else{
                    alert('El servicio no esta disponible, Intentelo de nuevo');
                }
            }).fail(function (qXHR, textStatus, errorThrown) {
              alert('El servicio no esta disponible, Intentelo de nuevo');
            })
    }, 

    Eliminar : function(Edit){
            jQuery.ajax({
                statusCode : {
                    404: function(){
                    }
                },
                data:{
                    'id_usuario': Edit, 
                    'q': 10
                },
                type: "POST",
                dataType: "json",
                url: "Controller/Ctrol_usuario.php"
            }).done(function (data, textStatus, jqXHR){
              alert('El usuario ha sido eliminado');
                location.href='usuarios.php';
            }).fail(function (qXHR, textStatus, errorThrown){
                alert('Error');
            }); 
        }
}

var login={
    inicio: function(id){
            jQuery.ajax({
                statusCode:{
                    404: function(){
                  }
                },
                data:{
                    'Usuario': $('#txtUsername').val(),
                    'Contrasena': $('#txtpassword').val(),
                    'q': 3
                },
                type: "POST",
                dataType: "json",
                url: "Controller/UserController.php"
            }).done(function (data){
                if (data== "1"){
                    location.href='dashboard.php'; 
                }
                if (data== "2"){
                    location.href='dashboard-main.php'; 
                }
                if (data== "3"){
                    location.href='dashboardEmpresas.php'; 
                }
                if(data== "0"){
                    alert('Usuario incorrecto');
                }
                if(data== "4"){
                    alert('Contraseña incorrecta');
                } 

            }).fail(function(){
               alert('error');
            });
    }
}
var loginAlternative={
      inicio: function(id){

            jQuery.ajax({
                statusCode:{
                    404: function(){
                  }
                },
                data:{
                    'Usuario': $('#txtuser').val(),
                    'Contrasena': $('#txtAlternativepassword').val(),
                    'q': 4
                },
                type: "POST",
                dataType: "json",
                url: "Controller/UserController.php"
            }).done(function (data){
                if (data== "1"){
                    location.href='dashboard.php'; 
                }
                if (data== "2"){
                    location.href='dashboardSuperUsuario.php'; 
                }
                if (data== "3"){
                    location.href='dashboardEmpresas.php'; 
                }
                if(data== "0"){
                    alert('Usuario incorrecto');
                }
                if(data== "4"){
                    alert('Contraseña incorrecta');
                } 

            }).fail(function(){
               alert('error');
            });
    }
}