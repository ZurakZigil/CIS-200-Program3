namespace UPVApp
{
    partial class EditAddressForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listBoxSelectAddress = new System.Windows.Forms.ListBox();
            this.lblEditAddress = new System.Windows.Forms.Label();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.NoAddressError = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.NoAddressError)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxSelectAddress
            // 
            this.listBoxSelectAddress.FormattingEnabled = true;
            this.listBoxSelectAddress.Location = new System.Drawing.Point(13, 37);
            this.listBoxSelectAddress.Name = "listBoxSelectAddress";
            this.listBoxSelectAddress.Size = new System.Drawing.Size(284, 277);
            this.listBoxSelectAddress.TabIndex = 0;
            this.listBoxSelectAddress.Validating += new System.ComponentModel.CancelEventHandler(this.listBoxSelectAddress_Validating);
            this.listBoxSelectAddress.Validated += new System.EventHandler(this.listBoxSelectAddress_Validated);
            // 
            // lblEditAddress
            // 
            this.lblEditAddress.AutoSize = true;
            this.lblEditAddress.Location = new System.Drawing.Point(13, 18);
            this.lblEditAddress.Name = "lblEditAddress";
            this.lblEditAddress.Size = new System.Drawing.Size(125, 13);
            this.lblEditAddress.TabIndex = 1;
            this.lblEditAddress.Text = "Select an Address to edit";
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(138, 330);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 9;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cancelBtn_MouseDown);
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(222, 330);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 8;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // NoAddressError
            // 
            this.NoAddressError.ContainerControl = this;
            // 
            // EditAddressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 365);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.lblEditAddress);
            this.Controls.Add(this.listBoxSelectAddress);
            this.Name = "EditAddressForm";
            this.Text = "EditAddressForm";
            this.Enter += new System.EventHandler(this.okBtn_Click);
            ((System.ComponentModel.ISupportInitialize)(this.NoAddressError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxSelectAddress;
        private System.Windows.Forms.Label lblEditAddress;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.ErrorProvider NoAddressError;
    }
}