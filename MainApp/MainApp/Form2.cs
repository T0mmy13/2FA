using System;
using System.Windows.Forms;

namespace MainApp
{
    public partial class CodeInputForm : Form
    {
        public string InputCode { get; private set; }

        public CodeInputForm()
        {
            InitializeComponent();
            this.AcceptButton = buttonSubmit;
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            InputCode = textBoxCode.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}