using System;
using System.Collections;

namespace Proyecto_5
{
    class Empleado
    {   
        string nombre;
        string apellido;
        int codigoEmpleado;
        double montoVenta;

        public Empleado(string n, string a, int codE, double monto){
            
            nombre= n;
            apellido= a;
            codigoEmpleado= codE;
            montoVenta= monto;
        }

        public Empleado(int codE){
            this.codigoEmpleado = codE;
        }

        public string Nombre{
            set{
                this.nombre = value;
            }
            get{
                return this.nombre; 
            }     
        }

        public string Apellido{
            set{
                this.apellido = value;
            }
            get{
                return apellido; 
            }
        }

        public int CodEmpleado{
            set{
                this.codigoEmpleado = value;
            }
            get{
                return codigoEmpleado; 
            }
        }
        
        public double MontoVenta{
            set{
                this.montoVenta = value;
            }
            get{
                return montoVenta; 
            }     
        }
    }
}
