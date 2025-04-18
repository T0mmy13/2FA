namespace MainApp
{
    partial class CodeInputForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox textBoxCode;
        private System.Windows.Forms.Button buttonSubmit;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.textBoxCode = new System.Windows.Forms.TextBox();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxCode
            // 
            this.textBoxCode.Location = new System.Drawing.Point(12, 12);
            this.textBoxCode.Name = "textBoxCode";
            this.textBoxCode.Size = new System.Drawing.Size(260, 20);
            this.textBoxCode.TabIndex = 0;
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Location = new System.Drawing.Point(197, 38);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(75, 23);
            this.buttonSubmit.TabIndex = 1;
            this.buttonSubmit.Text = "Submit";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // CodeInputForm
            // 
            this.AcceptButton = this.buttonSubmit;
            this.ClientSize = new System.Drawing.Size(284, 71);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.textBoxCode);
            this.Name = "CodeInputForm";
            this.Text = "Введите код";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}