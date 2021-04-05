
namespace PolyTool
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listView1 = new System.Windows.Forms.ListView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polycomSiren14ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.waveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polycomSiren14ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.waveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conversionSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.backgroundWorker_WH = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker_Siren14 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker_Siren14Enc = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker_Move = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker_NU_Single = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker_NU_Multi = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            resources.ApplyResources(this.listView1, "listView1");
            this.listView1.Name = "listView1";
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView1_ColumnClick);
            this.listView1.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.ListView1_ColumnWidthChanging);
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileFToolStripMenuItem,
            this.settingsCToolStripMenuItem,
            this.helpHToolStripMenuItem});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileFToolStripMenuItem
            // 
            this.fileFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openOToolStripMenuItem,
            this.openFolderToolStripMenuItem,
            this.closeCToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitXToolStripMenuItem});
            this.fileFToolStripMenuItem.Name = "fileFToolStripMenuItem";
            resources.ApplyResources(this.fileFToolStripMenuItem, "fileFToolStripMenuItem");
            // 
            // openOToolStripMenuItem
            // 
            this.openOToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.polycomSiren14ToolStripMenuItem1,
            this.waveToolStripMenuItem1});
            this.openOToolStripMenuItem.Name = "openOToolStripMenuItem";
            resources.ApplyResources(this.openOToolStripMenuItem, "openOToolStripMenuItem");
            // 
            // polycomSiren14ToolStripMenuItem1
            // 
            this.polycomSiren14ToolStripMenuItem1.Name = "polycomSiren14ToolStripMenuItem1";
            resources.ApplyResources(this.polycomSiren14ToolStripMenuItem1, "polycomSiren14ToolStripMenuItem1");
            this.polycomSiren14ToolStripMenuItem1.Click += new System.EventHandler(this.PolycomSiren14ToolStripMenuItem1_Click);
            // 
            // waveToolStripMenuItem1
            // 
            this.waveToolStripMenuItem1.Name = "waveToolStripMenuItem1";
            resources.ApplyResources(this.waveToolStripMenuItem1, "waveToolStripMenuItem1");
            this.waveToolStripMenuItem1.Click += new System.EventHandler(this.WaveToolStripMenuItem1_Click);
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.polycomSiren14ToolStripMenuItem,
            this.waveToolStripMenuItem});
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            resources.ApplyResources(this.openFolderToolStripMenuItem, "openFolderToolStripMenuItem");
            // 
            // polycomSiren14ToolStripMenuItem
            // 
            this.polycomSiren14ToolStripMenuItem.Name = "polycomSiren14ToolStripMenuItem";
            resources.ApplyResources(this.polycomSiren14ToolStripMenuItem, "polycomSiren14ToolStripMenuItem");
            this.polycomSiren14ToolStripMenuItem.Click += new System.EventHandler(this.PolycomSiren14ToolStripMenuItem_Click);
            // 
            // waveToolStripMenuItem
            // 
            this.waveToolStripMenuItem.Name = "waveToolStripMenuItem";
            resources.ApplyResources(this.waveToolStripMenuItem, "waveToolStripMenuItem");
            this.waveToolStripMenuItem.Click += new System.EventHandler(this.WaveToolStripMenuItem_Click);
            // 
            // closeCToolStripMenuItem
            // 
            this.closeCToolStripMenuItem.Name = "closeCToolStripMenuItem";
            resources.ApplyResources(this.closeCToolStripMenuItem, "closeCToolStripMenuItem");
            this.closeCToolStripMenuItem.Click += new System.EventHandler(this.CloseCToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // exitXToolStripMenuItem
            // 
            this.exitXToolStripMenuItem.Name = "exitXToolStripMenuItem";
            resources.ApplyResources(this.exitXToolStripMenuItem, "exitXToolStripMenuItem");
            this.exitXToolStripMenuItem.Click += new System.EventHandler(this.ExitXToolStripMenuItem_Click);
            // 
            // settingsCToolStripMenuItem
            // 
            this.settingsCToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.conversionSettingsToolStripMenuItem});
            this.settingsCToolStripMenuItem.Name = "settingsCToolStripMenuItem";
            resources.ApplyResources(this.settingsCToolStripMenuItem, "settingsCToolStripMenuItem");
            // 
            // conversionSettingsToolStripMenuItem
            // 
            this.conversionSettingsToolStripMenuItem.Name = "conversionSettingsToolStripMenuItem";
            resources.ApplyResources(this.conversionSettingsToolStripMenuItem, "conversionSettingsToolStripMenuItem");
            this.conversionSettingsToolStripMenuItem.Click += new System.EventHandler(this.ConversionSettingsToolStripMenuItem_Click);
            // 
            // helpHToolStripMenuItem
            // 
            this.helpHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutAToolStripMenuItem,
            this.toolStripMenuItem2,
            this.checkForUpdatesToolStripMenuItem});
            this.helpHToolStripMenuItem.Name = "helpHToolStripMenuItem";
            resources.ApplyResources(this.helpHToolStripMenuItem, "helpHToolStripMenuItem");
            // 
            // aboutAToolStripMenuItem
            // 
            this.aboutAToolStripMenuItem.Name = "aboutAToolStripMenuItem";
            resources.ApplyResources(this.aboutAToolStripMenuItem, "aboutAToolStripMenuItem");
            this.aboutAToolStripMenuItem.Click += new System.EventHandler(this.AboutAToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            resources.ApplyResources(this.checkForUpdatesToolStripMenuItem, "checkForUpdatesToolStripMenuItem");
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.CheckForUpdatesToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listView1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PolyTool.Properties.Resources.imas_basic_logo;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // backgroundWorker_WH
            // 
            this.backgroundWorker_WH.WorkerReportsProgress = true;
            this.backgroundWorker_WH.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_WH_DoWork);
            this.backgroundWorker_WH.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_WH_ProgressChanged);
            // 
            // backgroundWorker_Siren14
            // 
            this.backgroundWorker_Siren14.WorkerReportsProgress = true;
            this.backgroundWorker_Siren14.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_Siren14_DoWork);
            this.backgroundWorker_Siren14.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_Siren14_ProgressChanged);
            // 
            // backgroundWorker_Siren14Enc
            // 
            this.backgroundWorker_Siren14Enc.WorkerReportsProgress = true;
            this.backgroundWorker_Siren14Enc.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_Siren14Enc_DoWork);
            this.backgroundWorker_Siren14Enc.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_Siren14Enc_ProgressChanged);
            // 
            // backgroundWorker_Move
            // 
            this.backgroundWorker_Move.WorkerReportsProgress = true;
            this.backgroundWorker_Move.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_Move_DoWork);
            this.backgroundWorker_Move.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_Move_ProgressChanged);
            // 
            // backgroundWorker_NU_Single
            // 
            this.backgroundWorker_NU_Single.WorkerReportsProgress = true;
            this.backgroundWorker_NU_Single.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_NU_Single_DoWork);
            this.backgroundWorker_NU_Single.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_NU_Single_ProgressChanged);
            // 
            // backgroundWorker_NU_Multi
            // 
            this.backgroundWorker_NU_Multi.WorkerReportsProgress = true;
            this.backgroundWorker_NU_Multi.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_NU_Multi_DoWork);
            this.backgroundWorker_NU_Multi.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_NU_Multi_ProgressChanged);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polycomSiren14ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem waveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polycomSiren14ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem waveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeCToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem conversionSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutAToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker backgroundWorker_WH;
        private System.ComponentModel.BackgroundWorker backgroundWorker_Siren14;
        private System.ComponentModel.BackgroundWorker backgroundWorker_Siren14Enc;
        private System.ComponentModel.BackgroundWorker backgroundWorker_Move;
        private System.ComponentModel.BackgroundWorker backgroundWorker_NU_Single;
        private System.ComponentModel.BackgroundWorker backgroundWorker_NU_Multi;
    }
}

