using System.Security;

namespace AbrirImagenes2022
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeOpenFileDialog()
        {
            this.openFileDialog1.Filter = "Images (*.BMP; *.JPG; *.GIF |" +
                "All Files (*.*)|*.*";
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.Title = "Seleccione una o varias imagenes";
        }

        private void abrirImagenesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr= this.openFileDialog1.ShowDialog();
            if(dr== DialogResult.OK)
            {
                //Leer los archivos
                foreach(string file in openFileDialog1.FileNames)
                {
                    try
                    {
                        PictureBox pb = new PictureBox();
                        Image img = EscalarImagen(Image.FromFile(file), 300, 200);
                        pb.Height = img.Height;
                        pb.Width = img.Width;
                        pb.Image = img;
                        flowLayoutPanel1.Controls.Add(pb);
                    }
                    catch(SecurityException ex)
                    {
                        MessageBox.Show("Error de seguridad: "+ ex.Message);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Error, no se puede mostrar la imagen");
                    }
                }
            }
        }
        public static Image EscalarImagen(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage)) 
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}