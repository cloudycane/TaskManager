using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Dominio.Entidades
{
    public class PedidoProductoModel
    {
        public int PedidoClienteId { get; set; }
        public PedidosClienteModel PedidoCliente { get; set; }
        public int ProductoParaVenderId { get; set; }
        public int CantidadDePedido { get; set; }
        public double PrecioTotal {  get; set; }
        public ProductosParaVenderModel ProductoParaVender { get; set; }

    }
}
