using System;

namespace Proyecto_5
{
    class Farmacia_Exception
    {
        public class TicketNoValido : Exception{} // try-catch para cuando el ticket no es valido
        public class EmpleadoNoEncont : Exception{} // try-catch para cuando no se encuentra el empleado
    }
}