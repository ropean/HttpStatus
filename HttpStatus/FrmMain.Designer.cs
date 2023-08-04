namespace HttpStatus
{
  partial class FrmMain
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    System.ComponentModel.IContainer components = null;

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
    void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
      this.label1 = new System.Windows.Forms.Label();
      this.txtURL = new System.Windows.Forms.TextBox();
      this.btnRequest = new System.Windows.Forms.Button();
      this.txtResponse = new System.Windows.Forms.TextBox();
      this.chkSSL = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(51, 20);
      this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(32, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "URL:";
      // 
      // txtURL
      // 
      this.txtURL.Location = new System.Drawing.Point(87, 17);
      this.txtURL.Margin = new System.Windows.Forms.Padding(2);
      this.txtURL.Name = "txtURL";
      this.txtURL.Size = new System.Drawing.Size(264, 20);
      this.txtURL.TabIndex = 0;
      this.txtURL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtURL_KeyPress);
      // 
      // btnRequest
      // 
      this.btnRequest.Location = new System.Drawing.Point(434, 19);
      this.btnRequest.Margin = new System.Windows.Forms.Padding(2);
      this.btnRequest.Name = "btnRequest";
      this.btnRequest.Size = new System.Drawing.Size(56, 21);
      this.btnRequest.TabIndex = 99;
      this.btnRequest.Text = "Request";
      this.btnRequest.UseVisualStyleBackColor = true;
      this.btnRequest.Click += new System.EventHandler(this.BtnRequest_Click);
      // 
      // txtResponse
      // 
      this.txtResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtResponse.Location = new System.Drawing.Point(53, 60);
      this.txtResponse.Margin = new System.Windows.Forms.Padding(2);
      this.txtResponse.Multiline = true;
      this.txtResponse.Name = "txtResponse";
      this.txtResponse.ReadOnly = true;
      this.txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtResponse.Size = new System.Drawing.Size(562, 319);
      this.txtResponse.TabIndex = 999999;
      // 
      // chkSSL
      // 
      this.chkSSL.AutoSize = true;
      this.chkSSL.Location = new System.Drawing.Point(356, 22);
      this.chkSSL.Name = "chkSSL";
      this.chkSSL.Size = new System.Drawing.Size(46, 17);
      this.chkSSL.TabIndex = 1;
      this.chkSSL.Text = "SSL";
      this.chkSSL.UseVisualStyleBackColor = true;
      // 
      // FrmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(648, 407);
      this.Controls.Add(this.chkSSL);
      this.Controls.Add(this.btnRequest);
      this.Controls.Add(this.txtResponse);
      this.Controls.Add(this.txtURL);
      this.Controls.Add(this.label1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(2);
      this.Name = "FrmMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "HttpStatus";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    System.Windows.Forms.Label label1;
    System.Windows.Forms.TextBox txtURL;
    System.Windows.Forms.Button btnRequest;
    System.Windows.Forms.TextBox txtResponse;
        private System.Windows.Forms.CheckBox chkSSL;
    }
}

