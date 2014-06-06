namespace xSimulate.UI
{
    partial class KeywordSearchCtrl
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
            this.ListViewKeyword = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtKeyword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtMaxPage = new System.Windows.Forms.TextBox();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.RadioButtonTmall = new System.Windows.Forms.RadioButton();
            this.RadioButtonTaobao = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListViewKeyword
            // 
            this.ListViewKeyword.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.ListViewKeyword.Location = new System.Drawing.Point(0, 78);
            this.ListViewKeyword.MultiSelect = false;
            this.ListViewKeyword.Name = "ListViewKeyword";
            this.ListViewKeyword.Size = new System.Drawing.Size(834, 473);
            this.ListViewKeyword.TabIndex = 0;
            this.ListViewKeyword.UseCompatibleStateImageBehavior = false;
            this.ListViewKeyword.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序号";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "关键词";
            this.columnHeader2.Width = 226;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "渠道";
            this.columnHeader3.Width = 156;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "页码";
            this.columnHeader4.Width = 74;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "位置";
            this.columnHeader5.Width = 78;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RadioButtonTaobao);
            this.groupBox1.Controls.Add(this.RadioButtonTmall);
            this.groupBox1.Controls.Add(this.BtnSearch);
            this.groupBox1.Controls.Add(this.TxtMaxPage);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TxtKeyword);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(828, 68);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "关键词查询";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "关键词：";
            // 
            // TxtKeyword
            // 
            this.TxtKeyword.Location = new System.Drawing.Point(77, 30);
            this.TxtKeyword.Name = "TxtKeyword";
            this.TxtKeyword.Size = new System.Drawing.Size(319, 20);
            this.TxtKeyword.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(420, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "最大查询页码：";
            // 
            // TxtMaxPage
            // 
            this.TxtMaxPage.Location = new System.Drawing.Point(517, 30);
            this.TxtMaxPage.Name = "TxtMaxPage";
            this.TxtMaxPage.Size = new System.Drawing.Size(73, 20);
            this.TxtMaxPage.TabIndex = 3;
            this.TxtMaxPage.TextChanged += new System.EventHandler(this.TxtMaxPage_TextChanged);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Location = new System.Drawing.Point(733, 28);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(75, 23);
            this.BtnSearch.TabIndex = 4;
            this.BtnSearch.Text = "查询";
            this.BtnSearch.UseVisualStyleBackColor = true;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // RadioButtonTmall
            // 
            this.RadioButtonTmall.AutoSize = true;
            this.RadioButtonTmall.Checked = true;
            this.RadioButtonTmall.Location = new System.Drawing.Point(600, 31);
            this.RadioButtonTmall.Name = "RadioButtonTmall";
            this.RadioButtonTmall.Size = new System.Drawing.Size(50, 17);
            this.RadioButtonTmall.TabIndex = 5;
            this.RadioButtonTmall.TabStop = true;
            this.RadioButtonTmall.Text = "Tmall";
            this.RadioButtonTmall.UseVisualStyleBackColor = true;
            // 
            // RadioButtonTaobao
            // 
            this.RadioButtonTaobao.AutoSize = true;
            this.RadioButtonTaobao.Location = new System.Drawing.Point(654, 32);
            this.RadioButtonTaobao.Name = "RadioButtonTaobao";
            this.RadioButtonTaobao.Size = new System.Drawing.Size(62, 17);
            this.RadioButtonTaobao.TabIndex = 5;
            this.RadioButtonTaobao.Text = "Taobao";
            this.RadioButtonTaobao.UseVisualStyleBackColor = true;
            // 
            // KeywordSearchCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ListViewKeyword);
            this.Name = "KeywordSearchCtrl";
            this.Size = new System.Drawing.Size(834, 554);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ListViewKeyword;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtKeyword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtMaxPage;
        private System.Windows.Forms.Button BtnSearch;
        private System.Windows.Forms.RadioButton RadioButtonTmall;
        private System.Windows.Forms.RadioButton RadioButtonTaobao;
    }
}
