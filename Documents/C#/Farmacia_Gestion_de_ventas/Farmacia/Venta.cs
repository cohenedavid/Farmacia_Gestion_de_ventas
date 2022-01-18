using System;
using System.Collections;

namespace Proyecto_5{
    class Venta{
        string nomCom;
        string droga;
        string obraSocial; 
        string plan;
        double importe;
        int codVendedor;
        int nroTicket;
        DateTime fechaHora;

        public Venta (string ncom, string drog, string oS, string pl, double imp, int cod,int nT, DateTime fYh){
            this.nomCom = ncom;
            this.droga = drog;
            this.obraSocial = oS;
            this.plan= pl;
            this.importe = imp;
            this.codVendedor= cod;
            this.nroTicket = nT;
            this.fechaHora = fYh;
        }

        public string NombreCom{
            set{
                this.nomCom = value;
            }
            get{
                return this.nomCom;
            }
        }

        public string Droga{
            set{
                this.droga = value;
            }
            get{
                return this.droga;
            }
        } 

        public string ObraSocial{
            set{
                this.obraSocial = value;
            }
            get{
                return this.obraSocial;
            }
        }
        public string Plan{
            set{
                this.plan= value;
            }
            get{
                return this.plan;
            }
        }
        public double Importe{
            set{
                this.importe = value;
            }
            get{
                return this.importe;
            }
        }
        public int CodVendedor{
            set{
                this.codVendedor= value;
            }
            get{
                return this.codVendedor;
            }
        }
        public DateTime FechaHora{
            set{
                this.fechaHora = value;
            }
            get{
                return this.fechaHora;
            }
        }

        public int NroTicket{
            set{
                this.nroTicket = value;
            }
            get{
                return this.nroTicket;
            }
        }
    }
}