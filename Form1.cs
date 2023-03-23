using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulacion
{
    public partial class Form1 : Form
    {
        List<Departamento> departamentos = new List<Departamento>();
        List<Temperatura> temperaturas = new List<Temperatura>();
        List<Reporte> reportes = new List<Reporte>();

        public Form1()
        {
            InitializeComponent();
        }

        private void LeerDepartamentos(string fileName)
        {             

            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Departamento departamento = new Departamento();
                departamento.Codigo = Convert.ToInt32(reader.ReadLine());
                departamento.Nombre = reader.ReadLine();

                departamentos.Add(departamento);
            }
            reader.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            LeerDepartamentos(@"..\..\Departamentos.txt");
            //Como la lista de departamentos tiene codigo y nombre
            //hay que decirle al combobox cual de los dos mostrar en pantalla
            comboBox1.ValueMember = "codigo";
            comboBox1.DisplayMember = "nombre";

            comboBox1.DataSource = departamentos;
        }

        private void LeerTemperaturas()
        {
            string fileName = @"..\..\Temperatura.txt";

            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Temperatura temperatura = new Temperatura();
                temperatura.CodigoDepartamento = Convert.ToInt32(reader.ReadLine());
                temperatura.Medicion = Convert.ToInt32(reader.ReadLine());
                temperatura.Fecha = Convert.ToDateTime(reader.ReadLine());

                temperaturas.Add(temperatura);
            }
            reader.Close();
        }


        private void Guardar(string fileName)
        {
           
            FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);

            //Recorrer la lista de temperaturas e ir guardando al archivo cada una
            foreach (var temperatura in temperaturas)
            {
                writer.WriteLine(temperatura.CodigoDepartamento);
                writer.WriteLine(temperatura.Medicion);
                writer.WriteLine(temperatura.Fecha);
            }
            
            writer.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Temperatura temperatura = new Temperatura;

            temperatura.CodigoDepartamento = Convert.ToInt32(comboBox1.SelectedValue);
            temperatura.Fecha = dateTimePicker1.Value;
            temperatura.Medicion = Convert.ToInt32(textBox1.Text);

            temperaturas.Add(temperatura);

            Guardar(@"..\..\Temperaturas.txt");



        }

        private void button2_Click(object sender, EventArgs e)
        {
            LeerTemperaturas();
            //Los departamentos ya se cargaron al arrancar el programa para llenar el combobox
            //ya no es necesario leerlos otra vez

            for (int i = 0; i < temperaturas.Count; i++)
            {
                for (int j = 0; j < departamentos.Count; j++)
                {
                    if (temperaturas[i].CodigoDepartamento == departamentos[j].Codigo)
                    {
                        Reporte reporte = new Reporte();
                        reporte.Departamento = departamentos[j].Nombre;
                        reporte.Fecha = temperaturas[i].Fecha;
                        reporte.Temperatura = temperaturas[i].Medicion;

                        reportes.Add(reporte);
                    }

                }
            }

            dataGridView1.DataSource = reportes;
            dataGridView1.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Ordenar el reporte
            reportes = reportes.OrderBy(r => r.Temperatura).ToList();

            //Borrar los datos anteriores del gridview
            dataGridView1.DataSource = null;
            //Recargar los datos ya ordenados
            dataGridView1.DataSource = reportes;
            dataGridView1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double promedio = temperaturas.Average(t => t.Medicion);

            MessageBox.Show("El promedio de temperaturas es" + promedio.ToString());
        }
    }
}
