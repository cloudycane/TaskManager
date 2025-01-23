using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Dominio.Entidades;
using TaskManager.Dominio.Entidades.DTOs;

namespace TaskManager.Aplicacion.Interfaces
{
    public interface IOrdenAdquisicionService
    {
        OrdenAdquisicionDTO ConvertToDTO(OrdenAdquisicionModel model);
        OrdenAdquisicionModel ConvertToModel(OrdenAdquisicionDTO dto);
    }
}
