using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2_Interfaz_Grafica
{
    struct Datos  //37 variables
    {
        //Entrada proceso
        public bool Camion;
        public double Temp_ent, Caudal_ent;
        public bool Estado_analisis;
        public bool Bomba1;
        //Almacenaje depósito 100k
        public double Temp_100k, Capacidad_100k;
        public bool Bomba2;
        //Pasteurizado
        public double Temp_vapor, Temp_leche, Caudal_Bomba3;
        public bool Int_Calefactor, Bomba3;
        //Almacenaje depósito 50k
        public double Temp_50k, Capacidad_50k, Caudal_agua;
        public bool BombaAguaFria, Estado_agitador;
        //Entrada de datos por parte del operario
        public string lote, caducidad, numero_palets, tipo_palet, primer_palet; // TextBox trabaja con String
        public double Caudal_fabricacion;
        public bool Inicio_fabricacion;
        //Almacenaje depósito Cuajo
        public double Capacidad_cuajo;
        public bool Nivel_cuajo_bajo, Nivel_leche_bajo, Fabricacion_en_marcha;
        public bool Trasvasar_cuajo; //Integer porque se supone que debe contar cuántas veces se pulsa, sabiendo que cada Pulsación trasvasa 5l de cuajada
        //Variables control de calidad
        public bool Ing1, Ing2, Ing3, Ing4, Ing5;
        //Variable resultado control de calidad
        public bool Calidad;
        public int Prueba;
    }
}
