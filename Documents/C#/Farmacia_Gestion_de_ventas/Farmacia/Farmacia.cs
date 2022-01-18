using System;
using System.Collections;
using System.Collections.Generic;


namespace Proyecto_5
{
    class Farmacia{

        static ArrayList lista_Empleados= new ArrayList();
        static ArrayList lista_Ventas= new ArrayList();   
        static List<int> codAsig = new List<int>();
        static List<int> ticketAsig = new List<int>();
        static Venta venta;
        static Empleado empleado;
        static DateTime fechaHora = DateTime.Now;
        

    /******************************************** METODOS DE VENTAS ******************************************************/

        public static void agregarVenta(){ /*Punto (A)*/

            Console.WriteLine("Registro de Venta\n"); // Titulo
            int codVendedor = veriCodVend(); // Ingreso del codigo de empleado. Verifica que exista y el valor ingresado sea entero
            if (codVendedor == 000) // Si no recuerdan y ponen 000, se cancela la operacion. Pueden ir a ver el listado de empleados para ver los disponibles
            {
                Console.Clear();
                Console.WriteLine("Venta cancelada!\nConsulte listado de empleado para ver los disponibles.");   
            }
            else
            {
                Console.Write("Ingrese el nombre comercial del medicamento: ");
                string nomCom = Console.ReadLine().ToUpper();   // Queda almacenado en mayusculas
                Console.Write($"Ingrese la Droga de {nomCom.ToUpper()}: ");
                string droga = Console.ReadLine().ToUpper();    // Queda almacenado en mayusculas
                Console.WriteLine("\nATENCIóN!  Si no es por Obra Social ingrese 'particular'.");
                Console.Write("Ingrese la obra social: ");
                string obSocial = Console.ReadLine().ToUpper(); // Queda almacenado en mayusculas
                Console.WriteLine("\nATENCIóN!!  Si no es por un plan determinado deje un espacio.");
                Console.Write("Ingrese el plan: ");
                string plan = Console.ReadLine().ToUpper(); // Queda almacenado en mayusculas
                double importe = cargaImporte(nomCom, codVendedor);// importe(nomCom,codVendedor); // Carga importe y se lo asigna al empleado seleccionado al pricipio
                int nroTicket = cargaTicket();
                
                venta = new Venta(nomCom, droga, obSocial, plan, importe, codVendedor, nroTicket, fechaHora); // Crea objeto v
                lista_Ventas.Add(venta); // Guarda objeto en la lista

                Console.Clear();
                Console.WriteLine("Venta Registrada");
            }
        }

        public static void eliminarVenta(){ /* Punto (C) */

            if (veriList(lista_Ventas) == false) // Verifica si la lista esta vacia. Si devuelve false significa que tiene algo
            {
                Console.WriteLine("Eliminar Venta\n"); // Titulo
                bool eliminado = false;
                bool encontre = false;
                int ticket = 0;
                try
                { 
                    Console.Write("Ingrese el numero de ticket-factura a eliminar: ");
                    ticket = int.Parse(Console.ReadLine());
                    Console.Clear();
                    foreach (Venta x in lista_Ventas)
                    {  
                        if (x.NroTicket == ticket)
                        {
                            encontre = true; // si encuentra coincidencia guarda true para que no se ejecute EL try catch
                            if (Msj.conf($"Quiere eliminar la venta con numero {x.NroTicket}?") == true)
                            {
                                lista_Ventas.Remove(x);               // Elimina el empleado
                                if (x.CodVendedor != 0) // 
                                {
                                    restarMontoVenta(x.Importe,x.CodVendedor); // Se busca el empleado de la venta y se le descuenta el monto   
                                }
                                eliminado = true;
                                break; // Evita que salga error de ingreso                
                                
                            }else
                            {
                                Console.Clear();
                                Console.WriteLine("Operacion cancelada.");
                                break; // Evita que salga error de ingreso
                            }
                           
                        }else
                        {
                            exe_tryCatch(encontre,"ticket"); // ejecuta el try-catch TicketNoValido()
                            break; // Evita que salga error de ingreso
                        }
                        
                    }
                    if (eliminado == true) // Muestra mensaje de eliminado
                    {
                        Console.Clear();
                        Console.WriteLine("Venta Eliminada.");
                    }
                }
                catch (Farmacia_Exception.TicketNoValido){ // Si no se encuentra(encontre en false) nada muestra el mesnaje
                    Msj.tcTicket(ticket);
                }
                catch(Exception){  // Se ejecuta cuando ponen un valor invalido
                    Msj.tryCatch(); // Devuelve mensaje de valor invalido
                }
            }
        }

        public static void verVenta(int ticket){ // Busca la venta a mostrar por numero tickets
            bool encontre = false;
            Venta venta = null;
            try{
                foreach(Venta v in lista_Ventas){
                    if (v.NroTicket == ticket){
                        encontre = true;
                        venta= v;
                        Console.WriteLine($"\nFecha y hora: {v.FechaHora}\nTicket: {v.NroTicket}\nNombre comercial: {v.NombreCom.ToUpper()} \nDroga: {v.Droga.ToUpper()} \nObra Social: {v.ObraSocial.ToUpper()} \nPlan: {v.Plan.ToUpper()} \nImporte: ${v.Importe}");
                        muestraVend(ticket); // Muestra el empleado asiganado a una determinada venta y si no hay asignado muestra que el vendedor fue eliminado
                    }
                }
                exe_tryCatch(encontre,"ticket"); // ejecuta el try-catch TicketNoValido()
                if(venta == null){
                    Console.WriteLine("No hay ventas para mostrar.\n");
                }
            }
            catch (Farmacia_Exception.TicketNoValido){ // Si no se encuentra(encontre en false) nada muestra el mesnaje
                Msj.tcTicket(ticket);
            }
            catch (Exception){  // Se ejecuta cuando ponen un valor invalido
                Msj.tryCatch(); // Devuelve mensaje de valor invalido
            }
        }

        public static void todasVentas()
        {
            if (veriList(lista_Ventas) == false) // Verifica si la lista esta vacia. Si devuelve false significa que tiene algo
            {    
                Console.WriteLine("Listado de Ventas: ");
                foreach (Venta x in lista_Ventas)
                {
                    verVenta(x.NroTicket);
                }
            }
        }

        public static void modificaCodVend(){ /*Punto (B)*/

            if (veriList(lista_Ventas) == false) // Verifica si la lista esta vacia. Si devuelve false significa que tiene algo
            {
                bool encontre = false;
                int ticket = 0;
                Console.WriteLine("Actualización de Venta\n");  

                try{
                    Console.Write("Ingrese el numero de ticket-factura de la venta a modificar: ");
                    ticket= Convert.ToInt32(Console.ReadLine());
                    Console.Clear();

                    foreach(Venta v in lista_Ventas){
                        
                        if(v.NroTicket == ticket){
                            encontre = true;
                            Console.WriteLine("Nuevo codigo de vendedor\n");
                            int codNuevoVend= veriCodVend();            // Ingreso del codigo de empleado. Verifica que exista y el valor ingresado sea entero
                                                
                            sumarMontoVenta(v.Importe, codNuevoVend);   // Suma monto de venta al nuevo vendedor asignado a la venta
                            restarMontoVenta(v.Importe, v.CodVendedor); // Resta al anterior vendedor
                            v.CodVendedor= codNuevoVend; // Setea el codigo del vendedor de la venta
                            Console.Clear();
                            Console.WriteLine($"Se ha actualizado el codigo de vendedor de la venta con numero de ticket: {ticket}");
                            break;
                        }
                    }
                    exe_tryCatch(encontre,"ticket"); // ejecuta el try-catch TicketNoValido()
                }
                catch (Farmacia_Exception.TicketNoValido){ // Si no se encuentra(encontre en false) nada muestra el mesnaje
                Msj.tcTicket(ticket);
            }
            catch (Exception){  // Se ejecuta cuando ponen un valor invalido
                Msj.tryCatch(); // Devuelve mensaje de valor invalido
            }
            }
        }
        
        public static void infoVentasQuinOS(){ /*Punto (D)*/
 
            if (veriList(lista_Ventas) == false)    // Verifica si la lista esta vacia. Si devuelve false significa que tiene algo
            { 
                bool primQuincena = false;
                int contVOS = 0;                    // Contador de ventas con Obra Social
                int cantV = lista_Ventas.Count;     // Cantidad de ventas

                foreach (Venta v in lista_Ventas)
                {  
                    if ((primQuincena = veriQuincena(v)) == true) // Verifica que se este en la primera quincena del mes
                    {    
                        if (v.ObraSocial.ToLower() != "particular") // Busca las ventas por OBRA SOCIAL
                        {                                           // Ya que busca todo lo que difiere de particular
                            contVOS ++;                             // Cuenta las Ventas con Obra Social
                        }
                    }
                    else
                    {
                        Console.WriteLine("No hay ventas en la primera quincena del corriente mes.");
                        break;  // Evita que repita el mensaje
                    }   
                }
                if (primQuincena == true)
                {
                    double porc = (contVOS * 100) / cantV;          // Realiza cuenta de porcentaje
                    Console.WriteLine($"El porcentaje de ventas de la primera quincena con Obra Social es: {porc}%");
                }
                
            }
        }
        public static void ventasDrogaPlan(){ /*Punto (E)*/
            
            if (veriList(lista_Ventas) == false) // Verifica si la lista esta vacia. Si devuelve false significa que tiene algo
            { 
                Console.WriteLine("Listado de ventas por Droga y Plan determinado\n");

                ArrayList listaDrogaPlan= new ArrayList(); // lista de busquedas
                string droga= "";
                string plan= "";
                bool esPlan= true;
                do{
                    Console.Write("Indique la droga del medicamento: ");
                    droga= Console.ReadLine().ToUpper(); // Pasa a Mayusc. para realizar la comparacion
                    Console.Write("Indique el Plan: ");
                    plan= Console.ReadLine().ToUpper();  // Pasa a Mayusc. para realizar la comparacion
                    if(plan == "PARTICULAR"){
                        esPlan= false;
                        Msj.noPlan(); // Sale el aviso para ingreso de "PARTICULAR" como plan
                        Msj.pausa();
                    }
                    else esPlan= true;

                }while(esPlan != true);

                bool encontre = false;
                try{
                    foreach(Venta v in lista_Ventas){
                        if (v.FechaHora.ToString("MM") == fechaHora.ToString("MM")) // Compara el mes de venta con el actual para mostrar solo lo del mes en curso
                        {
                            if (v.Droga == droga && v.Plan == plan){ // Busca la droga y el plan solicitado
                                encontre= true;
                                listaDrogaPlan.Add(v); // Agrega la venta a la lista auxiliar para listar
                            }
                        }
                    }
                    if(encontre == false) // si no encuentra la droga y plan determinado
                        Console.WriteLine($"No se ha encontrado los requerimientos de Droga: {droga} y Plan: {plan}\n");

                    else{
                        foreach(Venta v in listaDrogaPlan){ // recorre la lista auxiliar
                            verVenta(v.NroTicket);   // Metodo para imprimir la venta "v"
                        }
                    }
                }
                catch(Exception){
                    Msj.tryCatch();
                }
            }
        }
        public static void sumarMontoVenta(double importe, int codVendedor){ //Suma el monto de venta al empleado buscado por codigo
            
            bool encontre = false;
            try{
                foreach(Empleado e in lista_Empleados){
                    if(e.CodEmpleado == codVendedor){
                        encontre = true;
                        e.MontoVenta += importe; //Suma y actualiza el monto de venta del Vendedor
                    }
                }
                exe_tryCatch(encontre,"empleado"); // Si no encuentra el empleado ejecuta el Ty-Catch
            }
            catch (Farmacia_Exception.EmpleadoNoEncont){
                Msj.tcNoEmpleado(codVendedor);
            }
            catch (Exception){
                Msj.tryCatch();
            }
        }
        public static void restarMontoVenta(double importe, int codVendedor){
            
            bool encontre = false;
            try{
                foreach(Empleado e in lista_Empleados){
                    if(e.CodEmpleado == codVendedor){
                        encontre = true;
                        e.MontoVenta -= importe; // Resta y actualiza el monto de venta del Vendedor
                    }
                }
                if(encontre == false){
                    Console.WriteLine("El codigo del empleado no es valido");
                }
            }
            catch (Exception){
                Console.WriteLine("Ha ocurrido un error.");
            }
        }

        public static double cargaImporte(string nomCom,int codVendedor) // Carga importe y se lo asigna al empleado seleccionado al pricipio
        {
            double importe = 0;
            bool ok;
            do
            {
                ok = true; // Setea bool para que no entre un bucle infinito al pedir el precio
                try
                {
                    Console.Write($"Ingrese el precio (con IVA) de {nomCom.ToUpper()}: $");
                    importe = double.Parse(Console.ReadLine());  
                    if (importe < 0) // Si el valor es menor a 0 lo vuelve a consultar ya que no es valido 
                    {                // No se condiciona para cuando es igual a 0 porque el medicamento puede ser gratuito --> $0
                        ok = false;
                        Console.Clear();
                        Console.WriteLine("El importe no debe ser menor a $0!\nIntente nuevamente.");
                        Msj.pausa();
                    }
                }
                catch (Exception)
                {
                    ok = false; // si ingresa un caracter invalido cambia a false para que vuelva a preguntar
                    Msj.tryCatch(); // Devuelve mensaje de valor invalido
                }
            } while (ok != true);
            sumarMontoVenta(importe,codVendedor); // Suma el importe al empledo correspondiente
            return importe;                       // Devuelve el importe para ser agregado a la Venta
        }

        public static int cargaTicket()
        {
            int ticket = 0;
            bool ok; // Ejecuta el bucle hasta que se ingrese un valor correcto.
            do{
                ok = true; // Setea bool para que no entre un bucle infinito al pedir el ticket
                try
                {
                    Console.Write("Ingrese el Nro de Ticket: ");
                    ticket = int.Parse(Console.ReadLine());
                    foreach (int x in ticketAsig)
                    {
                        if (x == ticket)
                        {
                            ok = false;
                            Console.Clear();
                            Console.WriteLine($"El numero de ticket {ticket} ya fue ingresado!\nVuelva a intentar\n");
                            break; // Cuando encuentra coincidencia se sale del foreach
                        }
                    }    
                }
                catch (System.Exception)
                {
                    ok = false;
                    Msj.tryCatch();
                }
            }while (ok != true);
            ticketAsig.Add(ticket);
            return ticket;
        }

    /*********************************************************************************************************************/

    /******************************************* METODOS DE EMPLEADOS ****************************************************/

        public static void nuevoEmpleado(){ 

            Console.WriteLine("Registro de nuevo empleado\n");
            Console.Write("Ingrese nombre: ");
            string nom= Console.ReadLine().ToUpper();   // Queda almacenado en mayusculas
            Console.Write("Ingrese apellido: ");
            string ape= Console.ReadLine().ToUpper();   // Queda almacenado en mayusculas

            int cod= nuevoCodEmpleado();

            empleado = new Empleado(nom, ape, cod, 0); // Crea objeto Empleado
            lista_Empleados.Add(empleado); // Agrega el nuevo empleado a la lista

            Console.Clear();
            Console.WriteLine($"Se ha completado el registro del empleado\n{cod}: {nom} {ape}");
        }
        public static int nuevoCodEmpleado(){ // Crea codigos unicos e irrepetibles
            
            int codigo = 0;
            
            if (lista_Empleados.Count == 0){
                codigo = 1;                // Si no hay nada cargado asigna el numero 1
                codAsig.Add(codigo);       // Guarda el numero asignado
            }
            else{
                codigo = codAsig[codAsig.Count - 1] + 1;    // Guarda el nuevo codigo sumando 1 al ultimo
                codAsig.Add(codigo);                        // Guarda el numero asignado en la lista
            }
            return codigo;
        }
        public static void eliminarEmpleado(){
            if (veriList(lista_Empleados) == false) // Verifica si la lista esta vacia. Si devuelve false significa que tiene algo
            {
                int codVendedor = 0;
                try
                {
                    bool eliminado = false;
                    bool encontro = false;

                    Console.WriteLine("Eliminar empleado\n");
                    codVendedor = veriCodVend(); // Ingreso del codigo de empleado. Verifica que exista y el valor ingresado sea entero
                    Console.Clear();
                    foreach (Empleado x in lista_Empleados)
                    {
                        if (x.CodEmpleado == codVendedor)
                        {
                            encontro = true; // si encuentra coincidencia guarda true para que no se ejecute el try catch
                            if (Msj.conf($"Quiere eliminar el usuario de {x.Nombre} {x.Apellido}?") == true) // Consulta si se quiere eliminar el empleado
                            {
                                if (Msj.advVenta(codVendedor, lista_Ventas, x) == true)
                                {
                                    asigCod(x); // Consulta si se quiere asignar la venta a otro empleado y elimina el empleado
                                    eliminado = true;
                                    break; // Evita que salga error de Index
                                }else
                                {
                                    eliminaEmpl(x); // Elimina el emplado
                                    eliminado = true;
                                    break; // Evita que salga error de Index
                                }
                                
                            }else
                            {
                                Console.Clear();
                                Console.WriteLine("Operación cancelada.\n");
                            }    
                        }
                    }
                    if (eliminado == true) // Mensaje al eliminar usuario
                    {
                        Console.Clear();
                        Console.WriteLine("Usuario de empleado eliminado.");
                    }
                    exe_tryCatch(encontro,"empleado"); // ejecuta el try-catch EmpleadoNoEncont()                
                }
                catch (Farmacia_Exception.EmpleadoNoEncont)
                {
                    Msj.tcNoEmpleado(codVendedor); // Si no se encuentra nada muestra un mensnaje
                }
            }
        }
        

        public static void informarMayorVendedor(){ /*Punto (F)*/
            
            if (veriList(lista_Empleados) == false) // Verifica si la lista esta vacia. Si devuelve false significa que tiene algo
            {
                double mayorMonto= 0;
                int codVendedorMayor= 0;

                foreach(Empleado vend in lista_Empleados){
                    
                    if(vend.MontoVenta > mayorMonto){
                        mayorMonto= vend.MontoVenta;        // Actualiza monto mayor de venta para comparar
                        codVendedorMayor= vend.CodEmpleado; // Guarda el codigo del empleado con mayor venta para luego mostrarlo
                    }
                }

                if(codVendedorMayor == 0){
                    Console.WriteLine("No hay registrado de un vendedor con una venta.");
                
                }else{
                
                    Console.WriteLine($"Vendedor con mayor Venta: {verEmpleado(codVendedorMayor)}"); // Muestra el vendedor
                }
            }
        }

        public static void todosEmpleados(){ // Muestra todos los empleados
            if (veriList(lista_Empleados) == false) // Verifica si la lista esta vacia. Si devuelve false significa que tiene algo
            {
                Console.WriteLine($"Listado de empleados:\n");
            
                foreach (Empleado e in lista_Empleados){
                    Console.WriteLine(verEmpleado(e.CodEmpleado));    
                }
            }
        }

        public static string verEmpleado(int cod){  // Busca por codigo y muestra el vendedor 

            string datos= "";
            
            foreach(Empleado e in lista_Empleados){
                if (e.CodEmpleado == cod){
                    if(e.MontoVenta > 0){
                        datos= ($"Empleado {cod}: {e.Nombre} {e.Apellido} - Monto de ventas: ${e.MontoVenta}"); // Si el empleado es vendedor
                    }
                    else{
                        datos= ($"Empleado {cod}: {e.Nombre} {e.Apellido} - Sin ventas"); // Si el empleado no es vendedor o no ha vendido
                    }
                }
            }
            return datos;
        }

        public static int veriCodVend() // Ingreso del codigo de empleado. Verifica que exista y el valor ingresado sea entero
        {
            bool encontro = false;
            int codVendedor = 0;
            do
            {   
                try
                {
                    Console.Write("Ingrese el codigo del empleado: ");
                    codVendedor = int.Parse(Console.ReadLine());
                     
                    foreach (Empleado x in lista_Empleados)
                    {
                        if(x.CodEmpleado == codVendedor)
                        {
                            encontro = true;
                        }
                    }
                    exe_tryCatch(encontro,"empleado"); // Si no encuentra el empleado ejecuta el try-catch EmpleadoNoEncont()
                }
                catch (Farmacia_Exception.EmpleadoNoEncont)
                {
                    Console.Clear();
                    Msj.tcNoEmpleado(codVendedor);
                    Console.WriteLine("Si no se acuerda ingrese 000 para salir.\n");
                }
                catch (Exception)
                {
                    Msj.tryCatch(); // Devuelve mensaje de valor invalido
                    Msj.pausa();
                }
                
            } while (encontro != true && codVendedor != 000);

            return codVendedor; // Retorna un codigo valido de vendedor 
        }                       // o 000 si no se recuerda el codigo que quiere buscar
                                // cuando se usa el metodo para validar el codigo de una busqueda.
        public static void muestraVend(int nroTicket) // Muestra el vendedor asignado a una determinada venta
        {
            foreach (Venta v in lista_Ventas)
            {
                if (v.NroTicket == nroTicket)   // Busca la venta de determinado Nro de Ticket
                {
                    if (v.CodVendedor == 000)   // Si la venta tiene codigo de vendedor 0 va a informar que el vendedor fue eliminado
                    {
                        Console.WriteLine("Vendedor: El vendedor de esta venta fue eliminado y no se asigno otro.");
                        break;
                    }
                    else                        // Si tiene un vendedor asignado lo busca y lo muestra
                    {
                        Console.WriteLine($"Vendedor: {verEmpleado(v.CodVendedor)}");
                        break;
                    }
                }
            }
            
        }

        public static void eliminaEmpl(Empleado x)  // Elimina un empleado determinado
        {
            codAsig.Remove(x.CodEmpleado);          // Elimina el codigo de Empleado
            lista_Empleados.Remove(x);              // Elimina el empleado       
        }

        public static void asigCod(Empleado x) // Pide codigo de otro empleado y asigna el mismo a las ventas del anterior 
        {
            if (Msj.conf($"Quiere asignar las ventas del empleado {x.Nombre} {x.Apellido} a otro") == true)
            {
                Console.Clear();
                Console.WriteLine("Asignar codigo a otro empleado\n");
                int codVendedorN = veriCodVend();       // Ingreso del codigo del nuevo empleado. Verifica que exista y el valor ingresado sea entero
                foreach (Venta y in lista_Ventas)
                {
                    if (x.CodEmpleado == y.CodVendedor) // Busca las ventas que tiene asignada el antiguo empleado
                    {
                        y.CodVendedor = codVendedorN;   // Asigna el nuevo codigo de empleado a la venta
                        sumarMontoVenta(y.Importe, codVendedorN);
                    }
                }
            }
            else
            {
                asigVentZero(x); // Busca las ventas de un determinado codigo de empleado y asigna 0. Luego eliminar el empleado.
            }    
            eliminaEmpl(x);  
        }

    /*********************************************************************************************************************/

    /*********************************************** OTROS METODOS *******************************************************/
    
        public static void init(){ // Carga de dos Empleados-Vendedores y 2 Ventas

        empleado = new Empleado("COSME", "FULANITO", 1, 750.75);
        lista_Empleados.Add(empleado);
        codAsig.Add(1);

        empleado = new Empleado("MAX", "POWER", 2, 832);
        lista_Empleados.Add(empleado);
        codAsig.Add(2);

        venta = new Venta("ATROPINA", "ATROPINA", "OSECAC", "INTEGRAL", 750, 1, 1234, fechaHora);
        lista_Ventas.Add(venta);
        ticketAsig.Add(1234);

        venta = new Venta("ASPIRINA","ACIDO ACETILSALICILICO", "OSDE", "PARTICULAR", 832, 2, 12345, fechaHora);
        lista_Ventas.Add(venta);
        ticketAsig.Add(12345);
        }

        public static void exe_tryCatch(bool encontre, string quien) // ejecuta el try-catch EmpleadoNoEncont()
        {
            switch (quien)
            {
                case "empleado":
                    if (encontre == false)
                    {
                        throw new Farmacia_Exception.EmpleadoNoEncont(); // Si no se encuentra el empleado ejecuta el try-catch
                    }
                break;
                case "ticket":
                    if (encontre == false)
                    {
                        throw new Farmacia_Exception.TicketNoValido();  // Si no se encuentra el ticket ejecuta el try-catch
                    }
                break;
            }
        }

        public static void asigVentZero(Empleado x) // Busca las ventas de un determinado codigo de empleado y asigna 0
        {                                           // Asigna 0 cuando no se quiere asignar las ventas de un empleado a otro
            foreach (Venta v in lista_Ventas)
            {
                if (x.CodEmpleado == v.CodVendedor)
                {
                    v.CodVendedor = 000;            // Asigna 0 para luego mostrar que no tiene empleado asignado  
                }
            }
            eliminaEmpl(x);
        }

        public static bool veriList(ArrayList lista)    // Verifica si la lista esta vacia
        {
            bool vacia = true;
            if (lista.Count == 0)
            {
                Console.WriteLine("Lista Vacia!");     // Si la lista no tiene nada muestra un mensaje
            }
            else
            {
                vacia = false;
            }
            return vacia; // Si la lista esta vacia devuelve true y si tiene algo devuelve false (o sea no esta vacia)
        }

        public static bool veriQuincena(Venta v) // Devuelve true si estamos en la primer quincena del mes
        {
            bool primQuincena = false;                                 
            int mesAct = int.Parse(fechaHora.ToString("MM"));   // Recupera y guarda el mes actual
            int mesV = int.Parse(v.FechaHora.ToString("MM"));   // Recupera y guarda el mes de la venta       
            int diaV = int.Parse(v.FechaHora.ToString("dd"));   // Recupera y guarda el dia de la venta

            if (mesV == mesAct && diaV >= 1 && diaV <= 15) 
            {
                primQuincena = true;
            }

            return primQuincena;
        }

    /*********************************************************************************************************************/

    }
}