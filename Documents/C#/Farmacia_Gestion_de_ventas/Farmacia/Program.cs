using System;

namespace Proyecto_5
{
    class Program
    {
        static void Main(string[] args)
        {
            Farmacia.init(); // pre carga para pruebas
            int op = 0;
            Console.Clear();
            Msj.bien_desp(); // Mensaje de bienvenida
            do
            {   
                op = opPrincipal();

                switch (op)
                {
                    case 1: // VENTAS
                    do
                    {
                        op = opVenta();
                        switch (op)
                        {
                            case 1: // Punto a)
                                Console.Clear();
                                Farmacia.agregarVenta();
                                Msj.pausa();
                            break;
                            case 2: /// Punto c)
                                Console.Clear();
                                Farmacia.eliminarVenta();
                                Msj.pausa();
                            break;
                            case 3: /// Punto d)
                                Console.Clear();
                                Farmacia.infoVentasQuinOS();
                                Msj.pausa();
                            break;
                            case 4: /// Punto e)
                                Console.Clear();
                                Farmacia.ventasDrogaPlan();
                                Msj.pausa();
                            break;
                            case 5: // Punto b)
                                Console.Clear();
                                Farmacia.modificaCodVend();
                                Msj.pausa();
                            break;
                            case 6: // Lista de todas las ventas
                                Console.Clear();
                                Farmacia.todasVentas();
                                Msj.pausa();
                            break;
                            case 7: // Vuelve al menu principal
                                Console.Clear();
                            break;
                            case 0: // Opcion que evita que envie 2 mensaje
                            break;
                            default: 
                                Console.Clear();
                                Msj.opcIncorrecta();
                                Msj.pausa();
                            break;
                        }
                    } while (op != 1 && op != 2 && op != 3 && op != 4 && op != 5 && op != 6 && op != 7); // Tiene que selecionar una opcion valida para poder continuar
                    op=0; // Evita que al seleccionar la opcion 3 se salga.
                    break;
                    case 2: // VENDEDORES
                    do
                    {
                        op = opEmpleado();
                        switch (op)
                        {
                            case 1: 
                                Console.Clear();
                                Farmacia.nuevoEmpleado();
                                Msj.pausa();                    
                            break;
                            case 2:
                                Console.Clear();
                                Farmacia.eliminarEmpleado();
                                Msj.pausa();
                            break;
                            case 3: // Punto F
                                Console.Clear();
                                Farmacia.informarMayorVendedor();
                                Msj.pausa();
                            break;
                            case 4:  // Muestra todos los empleados
                                Console.Clear();
                                Farmacia.todosEmpleados();
                                Msj.pausa();
                            break;
                            case 5: // Vuelve al menu principal
                                Console.Clear();
                            break;
                            case 0: // Opcion que evita que envie 2 mensaje
                            break;
                            default: 
                                Console.Clear();
                                Msj.opcIncorrecta();
                                Msj.pausa();
                            break;
                        }
                    } while (op != 1 && op != 2 && op != 3 && op != 4 && op != 5); // Tiene que selecionar una opcion valida para poder continuar
                    op=0; // evita que al seleccionar la opcion 3 se salga.
                    break;
                    case 3: //SALIR
                        Console.Clear();
                        Msj.bien_desp();
                        Console.Write("\nGracias, vuelva prontos!\n\nPresione cualquier tecla para finalizar...");
                        Console.ReadKey();
                        Console.Clear();
                        Environment.Exit(1);
                    break;
                    case 0: // Opcion que evita que envie 2 mensaje
                    break;
                    default: 
                        Console.Clear();
                        Msj.opcIncorrecta();
                        Msj.pausa();
                    break;
                }    
                
            } while (op != 3);
        }

        public static int opPrincipal()
        {
            Console.WriteLine("1- Ventas.");
            Console.WriteLine("2- Empleados.");
            Console.WriteLine("3- Salir.");
            Console.Write("\nSeleccione una opción: ");
            int op = selecOp(); // Devuelve la opcion seleccionada la cual pasa por un try-catch para validar
            return op;
        }

        public static int opVenta()
        {
            Console.Clear();
            Console.WriteLine("1- Agregar venta.");                                    // Punto A
            Console.WriteLine("2- Eliminar venta.");                                   // Punto C
            Console.WriteLine("3- Porcentaje de ventas por OS.");                      // Punto D
            Console.WriteLine("4- Lista de ventas con Dogra dada y Plan determinado.");// Punto E
            Console.WriteLine("5- Modificar codigo de vendedor.");                     // Punto B
            Console.WriteLine("6- Listado de ventas.");                      // Listado de todas las ventas
            Console.WriteLine("7- Volver.");                                           // Vuelve al menu principal
            Console.Write("\nSeleccione otra opción: ");
            int op = selecOp(); // Devuelve la opcion seleccionada la cual pasa por un try-catch para validar
            return op;
        }

        public static int opEmpleado()
        {
            Console.Clear();
            Console.WriteLine("1- Nuevo."); 
            Console.WriteLine("2- Borrar.");
            Console.WriteLine("3- Vendedor con mayor monto de ventas.");                // Punto f)
            Console.WriteLine("4- Listado de empleados.");
            Console.WriteLine("5- Volver.");
            Console.Write("\nSeleccione otra opción: ");
            int op = selecOp(); // Devuelve la opcion seleccionada la cual pasa por un try-catch para validar
            return op;
        }

        public static int selecOp() // Devuelve la opcion seleccionada la cual pasa por un try-catch para validar
        {
            int op = 0;

            try
            {
                op = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                op = 0; // seleciona un case que no hace nada para evitar el default y que envie doble mensaje
                Msj.tryCatch();
                Msj.pausa();
            }
            return op;
        }

    }
}
