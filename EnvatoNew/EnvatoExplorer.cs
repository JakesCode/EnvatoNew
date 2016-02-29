using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnvatoNew
{
    public partial class EnvatoExplorer : Form
    {
        public EnvatoExplorer()
        {
            InitializeComponent();
        }

        private void envato_explorer_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
