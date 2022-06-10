using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpcLabs.EasyOpc.DataAccess;

namespace P2_Interfaz_Grafica
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow(){
            InitializeComponent();
            InicializaDatos();
            InicializaTemporizacion();
            //Prueba.Content = "" + (bool)clienteOPCDA.ReadItemValue("", "Kepware.KEPServerEX.V6", "Cuajada.Automata.SuministroLeche.Camion");
            //Prueba.Content = parametros.Temp_leche = leeOPCDouble("Cuajada.Automata.Pasteurizador.Temp_leche");

        }

        Datos parametros;
        //CheckBox de camión
        private void Camion_Click(object sender, RoutedEventArgs e) {
            parametros.Camion = (bool)Camion.IsChecked;
        }


        //Función para inicializar los datos por defecto
        public void InicializaDatos()
        {
            Lampara_marcha.Fill = Brushes.White;
            Lampara_cuajo.Visibility = Visibility.Hidden;
            Lampara_leche.Visibility = Visibility.Hidden;
            Camion1.Visibility = Visibility.Hidden;
            Calefactor_piloto.Fill = Brushes.White;
            Analisis_piloto.Fill = Brushes.White;
        }

        //RadioButton análisis
        private void Analisis_pos_Click(object sender, RoutedEventArgs e) {
            parametros.Estado_analisis = true;
        }
        private void Analisis_neg_Click(object sender, RoutedEventArgs e) {
            parametros.Estado_analisis = false;
        }

        //Controlador Bomba1
        private void Bomba1_estado_Click(object sender, RoutedEventArgs e) {
            parametros.Bomba1 = (bool)Bomba1_estado.IsChecked;
        }
        //Controlador Bomba2
        private void Bomba2_estado_Click(object sender, RoutedEventArgs e) {
            parametros.Bomba2 = (bool)Bomba2_estado.IsChecked;
        }
        //Controlador Bomba3
        private void Bomba3_estado_Click(object sender, RoutedEventArgs e) {
            parametros.Bomba3 = (bool)Bomba3_estado.IsChecked;
        }
        //Controlador Bomba agua fría
        private void Bomba_agua_fria_estado_Click(object sender, RoutedEventArgs e) {
            parametros.BombaAguaFria = (bool)Bomba_agua_fria_estado.IsChecked;
        }

        //Calefactor
        private void Int_Calefactor_Click(object sender, RoutedEventArgs e) {
            parametros.Int_Calefactor = (bool)Int_Calefactor.IsChecked;
        }

        //Agitador
        private void Agitador_Click(object sender, RoutedEventArgs e) {
            parametros.Estado_agitador = (bool)Agitador.IsChecked;
        }

        //Botón inicio fabricación
        private void Inicio_fabricacion_Click(object sender, RoutedEventArgs e)
        {
            parametros.Inicio_fabricacion = true;
            parametros.numero_palets = textBox_Unidades.Text;
            parametros.lote = textBox_Lote.Text;
            parametros.primer_palet = textBox_Primero.Text;
            parametros.caducidad = textBox_Fecha.Text;
            ComboBoxItem item = (ComboBoxItem)comboPalet.SelectedItem;
            parametros.tipo_palet = (string)item.Content;
        }

        /*//Botón trasvase 5l de cuajo
        private void Boton_trasvase_Click(object sender, RoutedEventArgs e) {
            parametros.Trasvasar_cuajo++;
        }*/

        //Escalas rectángulos
        public double escala_100k = 130.0 / 100000.0;
        public double escala_50k = 130.0 / 50000.0;
        public double escala_cuajo = 130.0 / 50.0;

        //Método temporizador
        void InicializaTemporizacion()
        {
            temporizador = new System.Windows.Threading.DispatcherTimer();
            temporizador.Tick += new EventHandler(temporizador_Tick);
            temporizador.Interval = new TimeSpan(0, 0, 0, 1, 0);
            temporizador.Start();// Arranca el temporizador 
        }

        System.Windows.Threading.DispatcherTimer temporizador;


        private void temporizador_Tick(object sender, EventArgs e)
        {
            actualizaVisualizacion();
            actualizaDatos();
        }

        void actualizaVisualizacion() {
            //Escalado rectángulos
            //Se hace de la siguiente forma
            //Nivel_x.Height = datos.Capacidad_x * escala_x
            //Nivel_x.Margin = new Thickness (margen_izq, margen_sup + (Capacidad_x - datos.Capacidad) * escala_x, 0, 0);
            Nivel_100k_Copy.Height = 130.0 - (parametros.Capacidad_100k * escala_100k);
            Nivel_50k_Copy.Height = 130.0 - (parametros.Capacidad_50k * escala_50k);
            Nivel_cuajo_Copy.Height = 130.0 - (parametros.Capacidad_cuajo * escala_cuajo);

            //Análisis
            if (parametros.Estado_analisis == true){
                Analisis_piloto.Fill = Brushes.Green;
            }
            else {
                Analisis_piloto.Fill = Brushes.Red;
            }

            //Camión
            if (parametros.Camion == true){
                Camion1.Visibility = Visibility.Visible;
            }
            else{
                Camion1.Visibility = Visibility.Hidden;
                Analisis_piloto.Fill = Brushes.White;
            }

            //Piloto luminoso Bomba1
            if (parametros.Bomba1 == true) {
                Bomba1_piloto.Fill = Brushes.Green;
            }
            else {
                Bomba1_piloto.Fill = Brushes.Red;
            }

            //Piloto Luminoso Bomba2
            if (parametros.Bomba2 == true) {
                Bomba2_piloto.Fill = Brushes.Green;
            }
            else {
                Bomba2_piloto.Fill = Brushes.Red;
            }

            //Piloto luminoso Bomba3
            if (parametros.Bomba3 == true) {
                Bomba3_piloto.Fill = Brushes.Green;
            }
            else {
                Bomba3_piloto.Fill = Brushes.Red;
            }

            //Piloto luminoso Bomba agua fría
            if (parametros.BombaAguaFria || parametros.Estado_agitador == true) {
                Bomba_agua_fria_piloto.Fill = Brushes.Green;
            }
            else {
                Bomba_agua_fria_piloto.Fill = Brushes.Red;
            }

            //Piloto luminoso calefactor
            if (parametros.Int_Calefactor == false) {
                Calefactor_piloto.Fill = Brushes.White;
            }
            else {
                Calefactor_piloto.Fill = Brushes.Orange;
            }
            //Control de calidad
            if (parametros.Ing1 || parametros.Ing2 || parametros.Ing3 || parametros.Ing4 || parametros.Ing5 == true)
            {
                parametros.Calidad = false;
            }
        }

        EasyDAClient clienteOPCDA = new EasyDAClient();

        //Funciones lectura de datos
        public bool leeOPCBool(string var) {
            return (bool)clienteOPCDA.ReadItemValue("", "Kepware.KEPServerEX.V6", var);
        }

        public double leeOPCDouble(string var) {
            return (float)clienteOPCDA.ReadItemValue("", "Kepware.KEPServerEX.V6", var);
        }

        public string LeeOPCString(string var) {
            return (string)clienteOPCDA.ReadItemValue("", "Kepware.KEPServerEX.V6", var);
        }

        //Funciones escritura de datos
        void escribeOPCString(string direccion, string parametro)
        {
            clienteOPCDA.WriteItemValue("", "Kepware.KEPServerEX.V6", direccion, parametro);
        }

        void escribeOPCBool(string direccion, bool parametro)
        {
            clienteOPCDA.WriteItemValue("", "Kepware.KEPServerEX.V6", direccion, parametro);
        }



        void actualizaDatos()
        {//Lectura de datos
            //parametros.caducidad = "112213";
            //escribeOPCBool("Cuajada.Automata.SuministroLeche.Bomba1", true);
            //clienteOPCDA.WriteItemValue("", "Kepware.KEPServerEX.V6", "Cuajada.Automata.SuministroLeche.Estado_analisis_E", true);
            //escribeOPCString("Cuajada.Automata.NuevaOperacion.caducidad", parametros.caducidad);

            parametros.Camion = leeOPCBool("Cuajada.Automata.SuministroLeche.Camion");
            parametros.Temp_ent = leeOPCDouble("TempEnt");
            parametros.Caudal_ent = leeOPCDouble("CaudalEnt");
            parametros.Estado_analisis = leeOPCBool("EstadoAnalisis");
            parametros.Bomba1 = leeOPCBool("Bomba1");
            parametros.Temp_100k = leeOPCDouble("Temp100k");
            parametros.Capacidad_100k = leeOPCDouble("Capacidad100k");
            parametros.Bomba2 = leeOPCBool("Bomba2");
            parametros.Temp_vapor = leeOPCDouble("TempVapor");
            parametros.Temp_leche = leeOPCDouble("TempLeche");
            parametros.Caudal_Bomba3 = leeOPCDouble("CaudalBomba3");
            parametros.Int_Calefactor = leeOPCBool("IntCalefactor");
            parametros.Bomba3 = leeOPCBool("Bomba3");
            parametros.Temp_50k = leeOPCDouble("Temp50k");
            parametros.Capacidad_50k = leeOPCDouble("Capacidad50k");
            parametros.Caudal_agua = leeOPCDouble("CaudalAgua");
            parametros.BombaAguaFria = leeOPCBool(".BombaAguaFria");
            parametros.Estado_agitador = leeOPCBool("EstadoAgitador");
            parametros.lote = leeOPCString("Lote");
            parametros.caducidad = leeOPCString("Caducidad");
            parametros.numero_palets = leeOPCString("NumeroPalets");
            parametros.tipo_palet = leeOPCString("TipoPalet");
            parametros.primer_palet = leeOPCString("PrimerPalet");
            parametros.Caudal_fabricacion = leeOPCDouble("CaudalFabricacion");
            parametros.Inicio_fabricacion = leeOPCBool("InicioFabricacion");
            parametros.Capacidad_cuajo = leeOPCDouble("CapacidadCuajo");
            parametros.Nivel_cuajo_bajo = leeOPCBool("NivelCuajoBajo");
            parametros.Nivel_leche_bajo = leeOPCBool("NivelLecheBajo");
            parametros.Fabricacion_en_marcha= leeOPCBool("FabricacionEnMarcha");
            parametros.Trasvasar_cuajo= leeOPCBool("TrasvasarCuajo");
            parametros.Calidad = leeOPCBool("Calidad");
            //Escritura de datos
            if (parametros.Inicio_fabricacion == true){
                escribeOPCBool("Cuajada.Automata.NuevaOperacion.", parametros.Estado_analisis);
                escribeOPCString("Cuajada.Automata.NuevaOperacion.lote", parametros.lote);
                escribeOPCString("Cuajada.Automata.NuevaOperacion.caducidad", parametros.caducidad);
                escribeOPCString("Cuajada.Automata.NuevaOperacion.numero_palets", parametros.numero_palets);
                escribeOPCString("Cuajada.Automata.NuevaOperacion.tipo_palet", parametros.tipo_palet);
                escribeOPCString("Cuajada.Automata.NuevaOperacion.primer_palet", parametros.primer_palet);
                escribeOPCBool("Cuajada.Automata.NuevaOperacion.Comprobacion", parametros.Inicio_fabricacion);
                
                parametros.Inicio_fabricacion = false;
            }
            if (parametros.Trasvasar_cuajo == true){
                escribeOPCBool("TrasvasarCuajo", parametros.Trasvasar_cuajo);
                parametros.Trasvasar_cuajo = false;
            }
            escribeOPCBool("", parametros.Calidad);
        }
    }
}
