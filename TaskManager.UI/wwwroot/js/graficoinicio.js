window.onload = function () {
	fetchGet("/Dashboard/Grafico/graficoInicial", "text", function (data) {
		document.getElementById("imgFoto").src = "data:image/png;base64," + data;
	})
}