namespace WindowsFormsApp1
{
    partial class GameForm
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
            this.game1 = new ClassLibrary1.Game();
            this.Game = new ClassLibrary1.Game();
            this.SuspendLayout();
            // 
            // game1
            // 
            this.game1.BackColor = System.Drawing.Color.White;
            this.game1.Location = new System.Drawing.Point(12, 12);
            this.game1.Name = "game1";
            this.game1.ObjColor = System.Drawing.Color.Firebrick;
            this.game1.ObjSize = 25;
            this.game1.Size = new System.Drawing.Size(651, 375);
            this.game1.TabIndex = 0;
            this.game1.Text = "game1";
            // 
            // Game
            // 
            this.Game.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Game.Location = new System.Drawing.Point(12, -25);
            this.Game.Name = "Game";
            this.Game.ObjColor = System.Drawing.Color.Gray;
            this.Game.ObjSize = 20;
            this.Game.Size = new System.Drawing.Size(200, 600);
            this.Game.TabIndex = 14;
            this.Game.Text = "svet1";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 399);
            this.Controls.Add(this.game1);
            this.Name = "GameForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private ClassLibrary1.Game Game;
        private ClassLibrary1.Game game1;
    }
}

