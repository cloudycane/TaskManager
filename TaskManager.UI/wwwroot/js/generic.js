function get(idcontrol) {
    return document.getElementById(idcontrol).value;
}
function getI(idcontrol) {
    return document.getElementById(idcontrol).innerHTML;
}
function set(idcontrol, valor) {
    if (document.getElementById(idcontrol))
        document.getElementById(idcontrol).value = valor;
}
function setI(idcontrol, valor) {
    if (document.getElementById(idcontrol))
        document.getElementById(idcontrol).innerHTML = valor;
}
function setC(selector, valor = true) {
    if (document.querySelector(selector))
        document.querySelector(selector).checked = valor;
}
function setN(namecontrol, valor, idformulario) {
    if (idformulario == undefined) {
        document.getElementsByName(namecontrol)[0].value = valor;
    }
    else {
        document.querySelector("#" + idformulario + " [name='" + namecontrol + "']").value = valor;
    }
}
function setSRC(namecontrol, valor, idformulario) {
    if (idformulario == undefined) {
        document.getElementsByName(namecontrol)[0].src = valor;
    }
    else {
        document.querySelector("#" + idformulario + " [name='" + namecontrol + "']").src = valor;
    }
}
function recuperarGenerico(url, idformulario) {
    //Todos los elementos
    var elementosName = document.querySelectorAll("#" + idformulario + " [name]");
    var nombrename;
    fetchGet(url, "json", function (data) {
        for (var i = 0; i < elementosName.length; i++) {
            nombrename = elementosName[i].name;
            //CONDICIONES (type text / textarea / combos)
            if ((elementosName[i].type != undefined && elementosName[i].type == "text")
                || elementosName[i].tagName.toUpperCase() == "TEXTAREA" ||
                elementosName[i].tagName.toUpperCase() == "SELECT" || elementosName[i].type == "number") {

                setN(nombrename, data[nombrename], idformulario)
            } else if (elementosName[i].tagName.toUpperCase() == "IMG") {
                setSRC(nombrename, data[nombrename], idformulario)
            } else if (elementosName[i].type != undefined && elementosName[i].type == "radio") {
                setC("#" + idformulario + " [type='radio'][name='" + nombrename + "'][value='" + data[nombrename] + "']")
            } else if (elementosName[i].type != undefined && elementosName[i].type == "checkbox") {
                var name = nombrename.replace("[]", "");
                //[1,2,3]
                var valores = data[name]
                //array(Varios check)
                if (typeof (valores) == "object") {
                    var valor;
                    for (var j = 0; j < valores.length; j++) {
                        valor = valores[j];
                        setC("#" + idformulario + " [type='checkbox'][name='" + nombrename + "'][value='" + valor + "']")
                    }
                } else {
                    //string o entero (Hay un solo check)
                    setC("#" + idformulario + " [type='checkbox'][name='" + nombrename + "'][value='" + valores + "']")
                }
            }
        }
        configurarMostrarOcultar()
    });
}

function validarKeyPress(idformulario) {

    //Recorrimos todos los controles
    var elementosNames = document.querySelectorAll("#" + idformulario + " [name]");
    var control, nombreclases, clases, cantidad;
    var resultado;
    for (var i = 0; i < elementosNames.length; i++) {
        control = elementosNames[i];
        control.onkeypress = function (e) {

            //form-control ob (Sacamos su clase completa)

            nombreclases = e.target.className;
            //["form-control","ob"]
            clases = nombreclases.split(" ");
            //Solo letras
            resultado = clases.filter(p => p == "sl")
            if (resultado.length > 0) {
                var cadena = e.target.value + String.fromCharCode(e.keyCode)
                if (!/^[a-zA-Z]+$/.test(cadena)) {
                    e.preventDefault();
                }
            }
            //Letras con espacio en blanco
            resultado = clases.filter(p => p == "slcenb")
            if (resultado.length > 0) {
                var cadena = e.target.value + String.fromCharCode(e.keyCode)
                if (!/^[a-zA-Z ]+$/.test(cadena)) {
                    e.preventDefault();
                }
            }
            //Solo numeros
            resultado = clases.filter(p => p == "sn")
            if (resultado.length > 0) {

                var cadena = e.target.value + String.fromCharCode(e.keyCode)
                if (!/^[0-9]+$/.test(cadena)) {
                    e.preventDefault();
                }

            }
            //Solo numeros , letras y espacios en blanco
            resultado = clases.filter(p => p == "snslcenb")
            if (resultado.length > 0) {

                var cadena = e.target.value + String.fromCharCode(e.keyCode)
                if (!/^[a-zA-Z0-9(),;: ]+$/.test(cadena)) {
                    e.preventDefault();
                }

            }
            resultado = clases.filter(p => p.includes("max-"))
            if (resultado.length > 0) {
                var nombreClaseConMax = resultado[0]
                var valorMaximo = nombreClaseConMax.replace("max-", "") * 1
                var cadena = e.target.value + String.fromCharCode(e.keyCode)
                var longitudTexto = cadena.length;
                if (longitudTexto > valorMaximo) {
                    e.preventDefault();
                }


            }
        }

    }
}



function ValidarDatos(idformulario) {
    var error = "";

    //Buscar las validaciones(ob-) ->type radio
    var contenedorchecks = document.querySelectorAll("#" + idformulario + " [class*='ob-']")
    var contenedor;
    for (var i = 0; i < contenedorchecks.length; i++) {
        contenedor = contenedorchecks[i];
        var numero = contenedor.className.replace("ob-", "") * 1;
        var marcados = 0;
        var hijos = contenedor.children;
        var hijo;
        for (var j = 0; j < hijos.length; j++) {
            hijo = hijos[j];
            if (hijo.type == "radio" && hijo.checked == true) marcados++;
        }
        if (marcados == 0) {
            error = "Debe seleccionar un valor";
            return error;
        }
    }
    //Recorrimos todos los controles
    var elementosNames = document.querySelectorAll("#" + idformulario + " [name]");
    var control, nombreclases, clases, cantidad;
    var resultado;
    for (var i = 0; i < elementosNames.length; i++) {
        control = elementosNames[i];
        if (control.parentNode.style.display != "none") {
            nombreclases = control.className;
            clases = nombreclases.split(" ");
            //Obligatorios
            resultado = clases.filter(p => p == "ob")
            if (resultado.length > 0) {
                if (control.tagName.toUpperCase() == "INPUT" || control.tagName.toUpperCase() == "TEXTAREA") {
                    if (control.value.trim() == "") {
                        error = "Debe ingresar el campo " + control.name;
                        return error;
                    }
                } else if (control.tagName.toUpperCase() == "SELECT") {
                    if (control.selectedIndex == 0) {
                        error = "Debe ingresar el campo " + control.name;
                        return error;
                    }
                } else if (control.tagName.toUpperCase() == "IMG") {
                    if (control.src == "" || control.src == window.location.href) {
                        error = "Debe ingresar la imagen ";
                        return error;
                    }
                }

            }
            //Maximo
            resultado = clases.filter(p => p.includes("max-"))
            if (resultado.length > 0) {
                //max-100
                var nombreClaseConMax = resultado[0]
                //"100"
                var valorMaximo = nombreClaseConMax.replace("max-", "") * 1
                var longitudTexto = control.value.length
                if (longitudTexto > valorMaximo) {
                    error = "El campo  " + control.name + " su longitud maxima es " + valorMaximo +
                        " y usted a escrito una cadena con longitud " + longitudTexto;
                    return error;
                }
            }
            //Minimo
            resultado = clases.filter(p => p.includes("min-"))
            if (resultado.length > 0) {
                //max-100
                var nombreClaseConMin = resultado[0]
                //"100"
                var valorMinimo = nombreClaseConMin.replace("min-", "") * 1
                var longitudTexto = control.value.length
                if (longitudTexto < valorMinimo) {
                    error = "El campo  " + control.name + " su longitud minima es " + valorMinimo +
                        " y usted a escrito una cadena con longitud " + longitudTexto;
                    return error;
                }
            }
            //Solo letras
            resultado = clases.filter(p => p == "sl")
            if (resultado.length > 0) {
                if (!/^[a-zA-ZÀ-ÿ]+$/.test(control.value)) {
                    error = "El campo " + control.name + " solo debe tener letras minusculas o mayusculas ";
                    return error
                }
            }
            //Solo letras con espacio
            resultado = clases.filter(p => p == "slcenb")
            if (resultado.length > 0) {
                if (!/^[a-zA-ZÀ-ÿ,; ]+$/.test(control.value)) {
                    error = "El campo " + control.name + " solo debe tener letras minusculas , mayusculas o espacio en blanco ";
                    return error
                }
            }
            //Solo numeros
            resultado = clases.filter(p => p == "sn")
            if (resultado.length > 0) {
                if (!/^[0-9]+$/.test(control.value)) {
                    error = "El campo " + control.name + " solo debe tener numeros del 0 al 9 ";
                    return error
                }
            }
            //Solo numeros , letras y espacios en blanco
            resultado = clases.filter(p => p == "snslcenb")
            if (resultado.length > 0) {
                if (!/^[a-zA-Z0-9À-ÿ,;(): ]+$/.test(control.value)) {
                    error = "El campo " + control.name + " solo debe tener numeros , letras o espacios en blanco ";
                    return error
                }
            }
        }
    }
    return error;
}

function getN(namecontrol) {
    return document.getElementsByName(namecontrol)[0].value
}

function Error(titulo = "Error", texto = "Ocurrio un error") {
    if (titulo != "No transport could be initialized successfully. Try specifying a different transport or none at all for auto initialization.")
        Swal.fire({
            icon: 'error',
            title: titulo,
            text: texto
        })
}

function Confirmacion(titulo = "Confirmacion", texto = "Desea guardar los cambios?", callback) {
    return Swal.fire({
        title: titulo,
        text: texto,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si',
        cancelButtonText: "No"
    }).then((result) => {
        if (result.isConfirmed) {
            callback();
        }
    })
}

function Exito(titulo = "Se guardo correctamente") {
    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: titulo,
        showConfirmButton: false,
        timer: 1500
    })
}

function LimpiarDatos(idformulario) {
    var elementosName = document.querySelectorAll("#" + idformulario + " [name]");
    var elementoActual;
    var elementoName;
    for (var i = 0; i < elementosName.length; i++) {
        elementoActual = elementosName[i]
        elementoName = elementoActual.name;
        //Combo
        if (elementoActual.tagName.toUpperCase() == "SELECT") {
            document.getElementById(elementoActual.id).selectedIndex = 0;
        }
        else if (elementoActual.tagName.toUpperCase() == "IMG") {
            setSRC(elementoName, "", idformulario)
        }
        else if ((elementoActual.tagName.toUpperCase() == "INPUT" && elementoActual.type.toUpperCase() != "RADIO")
            || (elementoActual.tagName.toUpperCase() == "TEXTAREA")) {
            //INPUE
            setN(elementoName, "", idformulario);
        }

    }
    //Radio Button (Selector CSS)
    var radios = document.querySelectorAll("#" + idformulario + " [type*='radio']");
    for (var i = 0; i < radios.length; i++) {
        radios[i].checked = false;
    }
    //CheckBox
    var checboks = document.querySelectorAll("#" + idformulario + " [type*='checkbox']");
    for (var i = 0; i < checboks.length; i++) {
        checboks[i].checked = false;
    }
    for (var i = 0; i < idradios.length; i++) {
        if (document.getElementById(idradios[i]))
            document.getElementById(idradios[i]).checked = true;
    }
}

function setURL(url) {
    var raiz = document.getElementById("hdfOculto").value;
    //http://localhost........
    var urlCompleta = window.location.protocol + "//" + window.location.host + "/" + raiz
        + url

    return urlCompleta;
}

async function fetchGet(url, tiporespuesta, callback, retorno = false) {
    document.getElementById("divLoading").style.display = "block";
    try {
        var raiz = document.getElementById("hdfOculto").value;
        //http://localhost........
        var urlCompleta = window.location.protocol + "//" + window.location.host + "/" + raiz
            + url
        var res = await fetch(urlCompleta)
        if (tiporespuesta == "json")
            res = await res.json();
        else if (tiporespuesta == "text")
            res = await res.text();
        //JSON (Object)

        document.getElementById("divLoading").style.display = "none";
        if (retorno == false || retorno == null)
            callback(res)
        else
            return res;
    } catch (e) {
        alert("Ocurrion un error");
        console.log(e)
        document.getElementById("divLoading").style.display = "none";
    }
}
//[{"iidlaboratorio":1,"nombre":"SynLab","direccion":null,"personacontacto":null}
//, { "iidlaboratorio": 2, "nombre": "Multilab", "direccion": null, "personacontacto": null }, { "iidlaboratorio": 3, "nombre": "Suiza Lab", "direccion": null, "personacontacto": null }]
function llenarCombo(data, idcontrol, propiedadId, propiedadNombre, textoprimeraopcion = "--Seleccione--", valueprimeraopcion = "") {

    var contenido = "";
    var objActual;

    contenido += "<option value='" + valueprimeraopcion + "'>" + textoprimeraopcion + "</option>"
    for (var i = 0; i < data.length; i++) {
        objActual = data[i];
        contenido += "<option value='" + objActual[propiedadId] + "'>" + objActual[propiedadNombre] + "</option>"
    }
    if (typeof (idcontrol) == "string")
        setI(idcontrol, contenido)
    else {
        for (var j = 0; j < idcontrol.length; j++) {
            setI(idcontrol[j], contenido);
        }
    }
    //document.getElementById(idcontrol).innerHTML = contenido;
}

async function fetchPost(url, tiporespuesta, frm, callback) {
    try {
        var raiz = document.getElementById("hdfOculto").value;
        document.getElementById("divLoading").style.display = "block";
        //http://localhost........
        var urlCompleta = window.location.protocol + "//" + window.location.host + "/" + raiz + url
        var res = await fetch(urlCompleta, {
            method: "POST",
            body: frm
        });
        if (tiporespuesta == "json")
            res = await res.json();
        else if (tiporespuesta == "text")
            res = await res.text();
        //JSON (Object)
        callback(res)
        document.getElementById("divLoading").style.display = "none";

    } catch (e) {
        console.log(e)
        alert("Ocurrion un error");
        document.getElementById("divLoading").style.display = "none";
    }
}


var objConfiguracionGlobal;
var objBusquedaGlobal;
var objFormularioGlobal;
var dataCompleta;
function pintar(objConfiguracion, objBusqueda, objFormulario) {

    var contenido = "";


    if (objConfiguracion != null) {
        if (objConfiguracion.divContenedorTabla == undefined)
            objConfiguracion.divContenedorTabla = "divContenedorTabla"
        if (objConfiguracion.divPintado == undefined)
            objConfiguracion.divPintado = "divTabla"
        if (objConfiguracion.editar == undefined)
            objConfiguracion.editar = false
        if (objConfiguracion.eliminar == undefined)
            objConfiguracion.eliminar = false
        if (objConfiguracion.propiedadId == undefined)
            objConfiguracion.propiedadId = ""
        if (objConfiguracion.popup == undefined)
            objConfiguracion.popup = false
        if (objConfiguracion.type == undefined)
            objConfiguracion.type = ""
        if (objConfiguracion.columnreadonly == undefined)
            objConfiguracion.columnreadonly = []
        if (objConfiguracion.agregar == undefined)
            objConfiguracion.agregar = false
        if (objConfiguracion.idtabla == undefined)
            objConfiguracion.idtabla = "tabla"
    }


    if (objFormulario != undefined && objFormulario.type == undefined)
        objFormulario.type = "fieldset"
    if (objFormulario != undefined && objFormulario.type == "popup") {
        contenido += `<button onclick="EditarGenerico(0)" type="button" class="btn btn-primary"
                           data-bs-toggle="modal" data-bs-target="#${objConfiguracion.popupId}">
            Nuevo
                         </button>`
        contenido += `
<div class="modal fade" id="${objConfiguracion.popupId}" 
        data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
  <div class="modal-dialog modal-xl">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="lbltitulo"></h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">`;

        contenido += ConstruirFormulario(objFormulario)

        contenido += `
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" id="btnCerrarModal" data-bs-dismiss="modal">Cerrar</button>
        <button type="button" class="btn btn-primary" onclick="GuardarGenericoFormulario('${objFormulario.idformulario}','popup')">Guardar</button>
      </div>
    </div>
  </div>
</div>
            `
    }
    //Este es su lugar
    //agregar
    objConfiguracionGlobal = objConfiguracion;
    objBusquedaGlobal = objBusqueda;
    objFormularioGlobal = objFormulario;
    //Configuraciòn
    if (objConfiguracion != null && objConfiguracion.agregar == true) {
        contenido += `<button onclick="AgregarFila('${objConfiguracion.idtabla}')" type="button" class="btn btn-primary" >
                      Agregar
                    </button>`
    }
    if (objConfiguracion != null && objConfiguracion.nuevo == true && objConfiguracion.popup == true) {
        contenido += `<button onclick="CallbackEditar(0)" type="button" class="btn btn-primary" data-bs-toggle="modal" 
                      data-bs-target="#${objConfiguracion.popupId}">
                      Nuevo
                    </button>`
    }

    if (objFormulario != undefined && objFormulario.type == "fieldset") {
        if (objFormulario.pos != undefined && objFormulario.pos == "top") {
            //outerHTML
            var contenidoDatos = document.getElementById(objFormulario.posid).outerHTML;
            contenido += contenidoDatos;
            document.getElementById(objFormulario.posid).outerHTML = "";
        }
        contenido += ConstruirFormulario(objFormulario, "registro")
        if (objFormulario.pos != undefined && objFormulario.pos == "bottom") {
            var contenidoDatos = document.getElementById(objFormulario.posid).outerHTML;
            contenido += contenidoDatos;
            document.getElementById(objFormulario.posid).outerHTML = "";
        }
    }

    if (objBusqueda != null && objBusqueda != undefined) {
        if (objBusqueda.pos != undefined && objBusqueda.pos == "top") {
            //outerHTML
            var contenidoDatos = document.getElementById(objBusqueda.posid).outerHTML;
            contenido += contenidoDatos;
            document.getElementById(objBusqueda.posid).outerHTML = "";
        }
        contenido += ConstruirFormulario(objBusqueda, "busqueda")
        if (objBusqueda.pos != undefined && objBusqueda.pos == "bottom") {
            var contenidoDatos = document.getElementById(objBusqueda.posid).outerHTML;
            contenido += contenidoDatos;
            document.getElementById(objBusqueda.posid).outerHTML = "";
        }
    }
    if (objConfiguracion != null) {
        fetchGet(objConfiguracion.url, "json", function (res) {

            dataCompleta = res;
            contenido += "<div id='" + objConfiguracion.divContenedorTabla + "'>";
            //........................................................
            contenido += generarTabla(dataCompleta)
            contenido += "</div>";
            //Pinta los controles en pantalla
            setI(objConfiguracion.divPintado, contenido)
            configurarPaginacion();
            manejoCheck();
            //Pintado
            var objeto;
            for (var j = 0; j < objBusquedaCombos.length; j++) {
                objeto = objBusquedaCombos[j]
                llenarCombo(objeto.data, objeto.id, objeto.valuemostrar, objeto.propiedadmostrar, "-------Todos--------", "0")
            }
            if (objFormulario != null)
                validarKeyPress(objFormulario.idformulario)
            /////////


            //  document.getElementById(objConfiguracion.divPintado).innerHTML = contenido;
        })
    } else {
        if (objFormulario.divPintado == undefined) objFormulario.divPintado = "divTabla";
        setI(objFormulario.divPintado, contenido)

        var objeto;
        for (var j = 0; j < objBusquedaCombos.length; j++) {
            objeto = objBusquedaCombos[j]
            llenarCombo(objeto.data, objeto.id, objeto.valuemostrar, objeto.propiedadmostrar, "-------Todos--------", "0")
        }
    }




}
var filasChecks = []
function manejoCheck() {
    if (objConfiguracionGlobal.check) {
        var checks = document.getElementsByClassName("Check");
        var nChecks = checks.length;
        var fila;
        var seleccionado;
        var pos;
        for (var i = 0; i < nChecks; i++) {
            checks[i].onchange = function () {
                seleccionado = this.checked;
                //fila = this.parentNode.parentNode;
                //cod = fila.children[1].innerHTML;
                cod = this.className.replace("Check ", "") * 1;
                if (seleccionado) {
                    idsChecks.push(cod);
                    filasChecks.push(buscarCodigo(cod));
                }
                else {
                    pos = idsChecks.indexOf(cod);
                    if (pos > -1) {
                        idsChecks.splice(pos, 1);
                        filasChecks.splice(pos, 1);
                    }
                }
            }
        }
        var chkCabecera = document.getElementById("chkCabecera");
        if (chkCabecera != null) {
            chkCabecera.onchange = function () {
                var seleccionado = this.checked;
                if (!seleccionado) {
                    idsChecks = [];
                    filasChecks = [];
                }
                var fila;
                var cod;
                for (var i = 0; i < nChecks; i++) {
                    checks[i].checked = seleccionado;
                    if (seleccionado) {
                        fila = checks[i].parentNode.parentNode;
                        cod = fila.children[0].children[0].className.replace("Check ", "") * 1;
                        idsChecks.push(cod);
                        filasChecks.push(buscarCodigo(cod));
                    }
                }
            }
        }
    }
}

function buscarCodigo(id) {
    var fila = dataCompleta.filter(p => p[objConfiguracionGlobal.propiedadId] == id)[0]
    return fila;
}


//Paginacion Simple
var indicePagina = 0;
//Paginacion Por Bloques
var paginasBloque = 3;
var indiceBloque = 0;

var registrosPagina = 15;
var idsChecks = [];
function generarTabla(res) {
    var inicio = indicePagina * registrosPagina;
    var fin = inicio + registrosPagina;
    var contenido = "";
    contenido += "<table id='tablita' class='table'>";
    contenido += "<thead>";
    contenido += "<tr>";
    if (objConfiguracionGlobal != undefined && objConfiguracionGlobal.check == true) {
        contenido += "<td style='width:30px'>";
        contenido += "<input id='chkCabecera";
        contenido += "' type='checkbox'/>";
        contenido += "</td>";
    }
    for (var i = 0; i < objConfiguracionGlobal.cabeceras.length; i++) {
        contenido += "<td>" + objConfiguracionGlobal.cabeceras[i] + "</td>";
    }
    //Una columna adicional (thead)
    if (objConfiguracionGlobal.editarEspecifico == true || objConfiguracionGlobal.eliminarEspecifico == true || objConfiguracionGlobal.typeEspecifico == "edit") {
        contenido += "<td>Operaciones</td>";
    }
    contenido += "</tr>";
    contenido += "</thead>"
    var nregistros = dataCompleta.length;
    var obj;
    var propiedadActual;
    var existeIdCheck = false;
    contenido += "<tbody id='tbody'>";
    for (var i = inicio; i < fin; i++) {
        if (nregistros > i) {
            obj = res[i]
            contenido += `<tr ${objConfiguracionGlobal != null && objConfiguracionGlobal.cursor != undefined ?
                "style='cursor:pointer'" : ''}
                        ${objConfiguracionGlobal != null && objConfiguracionGlobal.rowClick != undefined ?
                    `onclick='rowClickEvent(${JSON.stringify(obj)})'` : ""}
                  >`;
            if (objConfiguracionGlobal != undefined && objConfiguracionGlobal.check == true) {
                existeIdCheck = (idsChecks.indexOf(obj[objConfiguracionGlobal.propiedadId]) > -1);
                contenido += `<td><input type='checkbox'
                                 ${existeIdCheck == true ? "checked" : ""}
                                class='Check ${obj[objConfiguracionGlobal.propiedadId]}'
                             name="${objConfiguracionGlobal.propiedadId}[]"
                        value="${obj[objConfiguracionGlobal.propiedadId]}" /></td> `
            }
            for (var j = 0; j < objConfiguracionGlobal.propiedades.length; j++) {
                propiedadActual = objConfiguracionGlobal.propiedades[j]
                contenido += "<td>" + obj[propiedadActual] + "</td>";
            }
            contenido += "</tr>";
        }
    }
    contenido += "</tbody>"
    contenido += "<tfoot id='tdPagina'>";
    contenido += "</tfoot>";

    contenido += "</table>";
    return contenido;
}

function configurarPaginacion() {
    var nRegistros = dataCompleta.length;
    var totalPaginas = Math.floor(nRegistros / registrosPagina);
    if (nRegistros % registrosPagina > 0) totalPaginas++;
    var html = "<tr><td colspan='2'>";
    if (totalPaginas > 1) {
        var totalRegistros = dataCompleta.length;
        var registrosBloque = registrosPagina * paginasBloque;
        var totalBloques = Math.floor(totalRegistros / registrosBloque);
        if (totalRegistros % registrosBloque > 0) totalBloques++;
        if (indiceBloque > 0) {
            html += "<button class='m-1 btn btn-danger Pag ";
            html += " Pagina' data-pag='-1'>";
            html += "<<";
            html += "</button>";
            html += "<button class='m-1 btn btn-danger Pag ";
            html += " Pagina' data-pag='-2'>";
            html += "<";
            html += "</button>";
        }
        var inicio = indiceBloque * paginasBloque;
        var fin = inicio + paginasBloque;
        for (var j = inicio; j < fin; j++) {
            if (j < totalPaginas) {
                html += "<button class='m-1  Pag ";
                html += " ";
                if (indicePagina == j) html += "btn btn-primary";
                else html += "btn btn-outline-primary";
                html += "' data-pag='";
                html += j;
                html += "'>";
                html += (j + 1);
                html += "</button> ";
            }
            else break;
        }
        if (indiceBloque < (totalBloques - 1)) {
            html += "<button class='m-1 btn btn-success Pag ";
            html += " Pagina' data-pag='-3'>";
            html += ">";
            html += "</button>";
            html += "<button class='m-1 btn btn-success Pag ";
            html += " Pagina' data-pag='-4'>";
            html += ">>";
            html += "</button>";
        }
        html += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        html += "</td></tr>";
    }
    var tdPagina = document.getElementById("tdPagina");
    if (tdPagina != null) {
        tdPagina.innerHTML = html;
        configurarEventosPagina();
    }
}

function configurarEventosPagina() {
    var paginas = document.getElementsByClassName("Pag ");
    var nPaginas = paginas.length;
    for (var j = 0; j < nPaginas; j++) {
        paginas[j].onclick = function () {
            paginar(+this.getAttribute("data-pag"));
        }
    }
}

function paginar(indice) {
    if (indice > -1) {
        indicePagina = indice;
        indiceBloque = Math.floor(indicePagina / paginasBloque);
    }
    else {
        var totalRegistros = dataCompleta.length;
        var registrosBloque = registrosPagina * paginasBloque;
        var totalBloques = Math.floor(totalRegistros / registrosBloque);
        if (totalRegistros % registrosBloque > 0) totalBloques++;
        switch (indice) {
            case -1: //Primer Bloque
                indiceBloque = 0;
                indicePagina = 0;
                break;
            case -2: //Bloque Anterior
                indiceBloque--;
                indicePagina = indiceBloque * paginasBloque;
                break;
            case -3: //Bloque Siguiente
                indiceBloque++;
                indicePagina = indiceBloque * paginasBloque;
                break;
            case -4: //Ultimo Bloque
                indiceBloque = (totalBloques - 1);
                indicePagina = indiceBloque * paginasBloque;
                break;
        }
    }
    var contenido = generarTabla(dataCompleta)
    document.getElementById("divContenedorTabla").innerHTML = contenido;
    configurarPaginacion()
    manejoCheck()
}




function EditarGenerico(id) {
    LimpiarDatos(objFormularioGlobal.idformulario);
    //Nuevo
    if (id == 0) {
        if (objFormularioGlobal.type == "popup")
            setI("lbltitulo", "Agregar " + objConfiguracionGlobal.titlePopup)
    }
    //Editar
    else {
        if (objFormularioGlobal.type == "popup")
            setI("lbltitulo", "Editar " + objConfiguracionGlobal.titlePopup)
        recuperarGenerico("" + objFormularioGlobal.urlrecuperar + "/?" + objFormularioGlobal.parametrorecuperar + "=" + id,
            objFormularioGlobal.idformulario)
    }
}

function CallbackEliminar(id) {
    if (objConfiguracionGlobal.urleliminar == undefined) {
        Eliminar(id)
    }
    else {
        if (objConfiguracionGlobal.nombreparametroeliminar == undefined) {
            objConfiguracionGlobal.nombreparametroeliminar = "id";
        }
        if (objConfiguracionGlobal.confirmacioneliminar == undefined) {
            objConfiguracionGlobal.confirmacioneliminar = "Desea eliminar el registro?"
        }
        Confirmacion(undefined, objConfiguracionGlobal.confirmacioneliminar, function () {
            fetchGet(objConfiguracionGlobal.urleliminar + "/?" + objConfiguracionGlobal.nombreparametroeliminar + "=" + id,
                "text", function (data) {
                    if (data == "1") {
                        Exito("Se elimino correctamente");
                        fetchGet(objConfiguracionGlobal.url, "json", function (rpta) {
                            InicializarPaginacion();
                            document.getElementById(objConfiguracionGlobal.divContenedorTabla).innerHTML = generarTabla(rpta);
                            manejoCheck()
                        })
                        //listarTipoAdministracion();
                    } else Error();
                })
        });
    }
}

function InicializarPaginacion() {
    indicePagina = 0;
    indiceBloque = 0;
}

function CallbackEditar(id) {

    if (objConfiguracionGlobal != null && objConfiguracionGlobal.popup == true) {
        //Nuevo
        if (objConfiguracionGlobal.titleId != undefined && objConfiguracionGlobal.titlePopup != undefined) {
            if (id == 0) {

                setI(objConfiguracionGlobal.titleId, "Agregar " + objConfiguracionGlobal.titlePopup);
            }
            //Editar
            else {
                setI(objConfiguracionGlobal.titleId, "Editar " + objConfiguracionGlobal.titlePopup);
            }
        }

    }
    if (objFormularioGlobal == undefined || objFormularioGlobal.urlrecuperar == undefined) {
        Editar(id);
    } else {
        recuperarGenerico("" + objFormularioGlobal.urlrecuperar + "/?" + objFormularioGlobal.parametrorecuperar + "=" + id,
            objFormularioGlobal.idformulario)
    }

}
var objBusquedaCombos = [];
var idradios = [];



function GuardarGenericoFormulario(idformulario, type) {
    var errores = ValidarDatos(idformulario)
    if (errores != "") {
        Error(errores)
        return;
    }
    var frmGuardar = document.getElementById(idformulario);
    var frm = new FormData(frmGuardar);
    Confirmacion(undefined, undefined, function (rpta) {
        fetchPost(objFormularioGlobal.urlguardar, "text", frm, function (data) {
            if (data == "1") {
                if (type == "popup") {
                    document.getElementById("btnCerrarModal").click();
                }
                Exito();
                //Refrezcar
                //listarTipoMedicamento();
                if (objConfiguracionGlobal != null) {
                    fetchPost(objConfiguracionGlobal.url, "json", frm, function (res) {
                        InicializarPaginacion();
                        document.getElementById(objConfiguracionGlobal.divContenedorTabla).innerHTML = generarTabla(res);
                        manejoCheck()
                    })
                } else {
                    if (document.getElementById("btnRegresar"))
                        document.getElementById("btnRegresar").click();
                }

                //
                LimpiarDatos(idformulario)
            } else Error();
        })
    })
}

function LimpiarGenericoBusqueda(idformulario) {
    idsChecks = []
    filasChecks = []
    LimpiarDatos(idformulario);
    var frBusqueda = document.getElementById(idformulario);
    var frm = new FormData(frBusqueda)
    fetchPost(objConfiguracionGlobal.url, "json", frm, function (res) {
        dataCompleta = res;
        InicializarPaginacion();
        document.getElementById(objConfiguracionGlobal.divContenedorTabla).innerHTML = generarTabla(res);
        manejoCheck();
        configurarPaginacion()
    })
}



function BuscarDatosGenericoBusqueda(id) {
    var formu = document.getElementById(id);
    var frm = new FormData(formu);
    fetchPost(objBusquedaGlobal.url, "json", frm, function (res) {
        dataCompleta = res;
        InicializarPaginacion();
        document.getElementById(objConfiguracionGlobal.divContenedorTabla).innerHTML = generarTabla(res);
        manejoCheck();
        configurarPaginacion()
    })
}

function ConstruirFormulario(objFormulario, tipo = "") {
    var contenido = "";
    var formulario = objFormulario.formulario;
    var elemento
    var numeroelementos
    var objetoActual;
    contenido += `<fieldset>`
    contenido += `<legend>${objFormulario.legend}</legend>`
    contenido += `<form id="${objFormulario.idformulario}" method="post" class="mb-3">`;
    for (var i = 0; i < formulario.length; i++) {
        //array () [{},{}]
        elemento = formulario[i];
        numeroelementos = elemento.length;
        contenido += "<div class='row'>";
        for (var j = 0; j < numeroelementos; j++) {
            //{class: "col-md-6", label: "Id medicamento", name: "iidmedicamento", type:"number", readonly:false}
            objetoActual = elemento[j]
            if (objetoActual.class == undefined) objetoActual.class = "col-md-6"
            if (objetoActual.type == undefined) objetoActual.type = "text"
            if (objetoActual.readonly == undefined) objetoActual.readonly = false
            if (objetoActual.label == undefined) objetoActual.label = ""
            if (objetoActual.name == undefined) objetoActual.name = ""
            if (objetoActual.classControl == undefined) objetoActual.classControl = ""
            if (objetoActual.id == undefined) objetoActual.id = ""
            contenido += `<div class="${objetoActual.class}">`;
            contenido += `<label>${objetoActual.label}</label>`;
            if (objetoActual.type == "text" || objetoActual.type == "number") {

                contenido += `
                      <input type="${objetoActual.type}"  ${objetoActual.readonly == true ? "readonly" : ""}  class="form-control ${objetoActual.classControl}"
                              name="${objetoActual.name}" />
                  `;

            } else if (objetoActual.type == "textarea") {

                contenido += `
                    <textarea name="${objetoActual.name}" ${objetoActual.readonly == true ? "readonly" : ""} class="form-control ${objetoActual.classControl}" ></textarea>
                `;

            } else if (objetoActual.type == "combobox") {
                contenido += `
                           <select id="${objetoActual.id}" class="form-control ${objetoActual.classControl}" name="${objetoActual.name}">
                             </select>
                    `;
                objBusquedaCombos.push(objetoActual);
            } else if (objetoActual.type == "radio" || objetoActual.type == "checkbox") {
                contenido += "<br />"
                for (var z = 0; z < objetoActual.labels.length; z++) {
                    contenido += `
                       <input type="${objetoActual.type}" ${objetoActual.checked.includes(objetoActual.ids[z]) ? "checked" : ""} 
                id="${objetoActual.ids[z]}"
                         name="${objetoActual.name}${objetoActual.type == "checkbox" ? "[]" : ""}" 
             value="${objetoActual.values[z]}" /> <label>${objetoActual.labels[z]}</label>
                     `;
                }
                //checked(string)
                if (objetoActual.type == "radio")
                    idradios.push(objetoActual.checked);
                else
                    idradios = idradios.concat(objetoActual.checked)

            } else if (objetoActual.type == "file") {
                contenido += ` <br />
                <img id="img${objetoActual.namefoto}" width="${objetoActual.imgwidth}" class="${objetoActual.classControl}"
                      height="${objetoActual.imgheight}" name="${objetoActual.namefoto}"  />
                <br />
                <input accept=".jpg,.png" onchange='previewImage(this,"img${objetoActual.namefoto}")' 
                     type="file" name="${objetoActual.name}" />`
            }
            contenido += `</div>`;
        }
        contenido += "</div>";

    }
    contenido += `</form>`;
    if (tipo == "busqueda") {
        contenido += ` <button class="btn btn-primary" onclick="BuscarDatosGenericoBusqueda('${objFormulario.idformulario}')">Buscar</button>
        <button class="btn btn-danger" onclick="LimpiarGenericoBusqueda('${objFormulario.idformulario}')" >Limpiar</button>`
    } else if (tipo == "registro") {
        contenido += ` <button class="btn btn-primary" onclick="GuardarGenericoFormulario('${objFormulario.idformulario}')">
                            Guardar</button>
        <button class="btn btn-danger" onclick="LimpiarGenericoBusqueda('${objFormulario.idformulario}')" >Limpiar</button>`
        if (objFormularioGlobal.regresar == true) {
            contenido += ` 
          <button class="btn btn-secondary" onclick="Regresar()" >Regresar</button>`
        }
    }
    contenido += `</fieldset>`
    return contenido;
}