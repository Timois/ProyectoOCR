using System.Drawing.Printing;
using System.Windows.Forms;
using System.Management;
using MaterialSkin.Controls;
namespace ProyectoOCR
{
    public partial class Form1 : MaterialSkin.Controls.MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear una búsqueda con la clase Win32_Printer
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");

                foreach (ManagementObject printer in searcher.Get())
                {
                    // Obtener nombre de la impresora
                    string nombreImpresora = printer["Name"].ToString();

                    // Agregar el nombre de la impresora al ListBox
                    listBox1.Items.Add(nombreImpresora);
                }
            }
            catch (Exception ex)
            {
                listBox1.Items.Add("Error: " + ex.Message); // Mostrar el error si ocurre
            }
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            try
            {
                // Limpiar el ComboBox antes de agregar las impresoras.
                comboBox1.Items.Clear();

                // Usar WMI para obtener información sobre las impresoras instaladas.
                string query = "SELECT * FROM Win32_Printer WHERE Network = TRUE";
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

                foreach (ManagementObject printer in searcher.Get())
                {
                    // Agregar solo las impresoras de red al ComboBox.
                    comboBox1.Items.Add(printer["Name"].ToString());
                }

                if (comboBox1.Items.Count > 0)
                {
                    comboBox1.SelectedIndex = 0; // Seleccionar el primer ítem por defecto.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar impresoras de red: " + ex.Message);
            }
        }
    }
}
