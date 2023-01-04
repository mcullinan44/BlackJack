namespace BlackJack.WinForm
{
    partial class PlayerHandControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

            this.lblWinning = new System.Windows.Forms.Label();
            this.lblBet = new System.Windows.Forms.Label();
  
            this.pnlMoney.SuspendLayout();
            this.pnlHand.SuspendLayout();
     
         
       
            this.pnlMoney.Controls.Add(this.lblWinning);
            this.pnlMoney.Controls.Add(this.lblBet);

  
            // 
            // lblWinning
            // 
            this.lblWinning.AutoSize = true;
            this.lblWinning.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWinning.ForeColor = System.Drawing.Color.Yellow;
            this.lblWinning.Location = new System.Drawing.Point(65, 4);
            this.lblWinning.Name = "lblWinning";
            this.lblWinning.Size = new System.Drawing.Size(34, 17);
            this.lblWinning.TabIndex = 1;
            this.lblWinning.Text = "{$0}";
            this.lblWinning.Visible = false;
            // 
            // lblBet
            // 
            this.lblBet.AutoSize = true;
            this.lblBet.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBet.ForeColor = System.Drawing.Color.Yellow;
            this.lblBet.Location = new System.Drawing.Point(10, 4);
            this.lblBet.Name = "lblBet";
            this.lblBet.Size = new System.Drawing.Size(34, 17);
            this.lblBet.TabIndex = 0;
            this.lblBet.Text = "{$0}";
            this.lblBet.Visible = false;
   
            // 
            // PlayerHandControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlHand);
            this.Controls.Add(this.pnlMoney);
            this.Name = "PlayerHandControl";
            //this.Size = new System.Drawing.Size(132, 254);
            this.pnlMoney.ResumeLayout(false);
            this.pnlMoney.PerformLayout();
            this.pnlHand.ResumeLayout(false);
            this.pnlHand.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.Label lblBet;
        private System.Windows.Forms.Label lblWinning;


    }
}
