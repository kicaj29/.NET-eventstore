namespace EventStoreSamples
{
    partial class Form1
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
            this.btnPostMessage = new System.Windows.Forms.Button();
            this.btnGetLast = new System.Windows.Forms.Button();
            this.btnGetPrevious = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPostMessage
            // 
            this.btnPostMessage.Location = new System.Drawing.Point(12, 27);
            this.btnPostMessage.Name = "btnPostMessage";
            this.btnPostMessage.Size = new System.Drawing.Size(128, 23);
            this.btnPostMessage.TabIndex = 0;
            this.btnPostMessage.Text = "PostMessage";
            this.btnPostMessage.UseVisualStyleBackColor = true;
            this.btnPostMessage.Click += new System.EventHandler(this.btnPostMessage_Click);
            // 
            // btnGetLast
            // 
            this.btnGetLast.Location = new System.Drawing.Point(157, 27);
            this.btnGetLast.Name = "btnGetLast";
            this.btnGetLast.Size = new System.Drawing.Size(132, 23);
            this.btnGetLast.TabIndex = 1;
            this.btnGetLast.Text = "Get Last";
            this.btnGetLast.UseVisualStyleBackColor = true;
            this.btnGetLast.Click += new System.EventHandler(this.btnGetLast_Click);
            // 
            // btnGetPrevious
            // 
            this.btnGetPrevious.Location = new System.Drawing.Point(316, 27);
            this.btnGetPrevious.Name = "btnGetPrevious";
            this.btnGetPrevious.Size = new System.Drawing.Size(137, 23);
            this.btnGetPrevious.TabIndex = 2;
            this.btnGetPrevious.Text = "Get Previous";
            this.btnGetPrevious.UseVisualStyleBackColor = true;
            this.btnGetPrevious.Click += new System.EventHandler(this.btnGetPrevious_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 386);
            this.Controls.Add(this.btnGetPrevious);
            this.Controls.Add(this.btnGetLast);
            this.Controls.Add(this.btnPostMessage);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPostMessage;
        private System.Windows.Forms.Button btnGetLast;
        private System.Windows.Forms.Button btnGetPrevious;
    }
}

