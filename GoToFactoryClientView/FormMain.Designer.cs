namespace GoToWorkFactoryClientView
{
    partial class FormMain
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
            this.orderViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.войтиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.зарегестрироватьсяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonCommit = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.labelUser = new System.Windows.Forms.Label();
            this.buttonSend = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.orderViewModelBindingSource)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // orderViewModelBindingSource
            // 
            this.orderViewModelBindingSource.DataSource = typeof(GoToWorkFactoryServiceDAL.ViewModels.OrderViewModel);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.войтиToolStripMenuItem,
            this.зарегестрироватьсяToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(797, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // войтиToolStripMenuItem
            // 
            this.войтиToolStripMenuItem.Name = "войтиToolStripMenuItem";
            this.войтиToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.войтиToolStripMenuItem.Text = "Войти";
            this.войтиToolStripMenuItem.Click += new System.EventHandler(this.войтиToolStripMenuItem_Click);
            // 
            // зарегестрироватьсяToolStripMenuItem
            // 
            this.зарегестрироватьсяToolStripMenuItem.Name = "зарегестрироватьсяToolStripMenuItem";
            this.зарегестрироватьсяToolStripMenuItem.Size = new System.Drawing.Size(130, 20);
            this.зарегестрироватьсяToolStripMenuItem.Text = "Зарегестрироваться";
            this.зарегестрироватьсяToolStripMenuItem.Click += new System.EventHandler(this.зарегестрироватьсяToolStripMenuItem_Click);
            // 
            // buttonCommit
            // 
            this.buttonCommit.Location = new System.Drawing.Point(643, 35);
            this.buttonCommit.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCommit.Name = "buttonCommit";
            this.buttonCommit.Size = new System.Drawing.Size(146, 51);
            this.buttonCommit.TabIndex = 2;
            this.buttonCommit.Text = "Оформить заказ";
            this.buttonCommit.UseVisualStyleBackColor = true;
            this.buttonCommit.Click += new System.EventHandler(this.buttonCommit_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(643, 91);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(146, 43);
            this.buttonAdd.TabIndex = 3;
            this.buttonAdd.Text = "Добавить в корзину";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.priceDataGridViewTextBoxColumn});
            this.dataGridView.DataSource = this.productViewModelBindingSource;
            this.dataGridView.Location = new System.Drawing.Point(10, 28);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(616, 276);
            this.dataGridView.TabIndex = 4;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // priceDataGridViewTextBoxColumn
            // 
            this.priceDataGridViewTextBoxColumn.DataPropertyName = "Price";
            this.priceDataGridViewTextBoxColumn.HeaderText = "Price";
            this.priceDataGridViewTextBoxColumn.Name = "priceDataGridViewTextBoxColumn";
            // 
            // productViewModelBindingSource
            // 
            this.productViewModelBindingSource.DataSource = typeof(GoToWorkFactoryServiceDAL.ViewModels.ProductViewModel);
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Location = new System.Drawing.Point(715, 9);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(70, 13);
            this.labelUser.TabIndex = 5;
            this.labelUser.Text = "Залогинься!";
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(643, 138);
            this.buttonSend.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(146, 46);
            this.buttonSend.TabIndex = 6;
            this.buttonSend.Text = "Прислать на почту информацию о заказах";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(645, 191);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(143, 44);
            this.buttonBack.TabIndex = 7;
            this.buttonBack.Text = "Сделать Бэкап";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 319);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.labelUser);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonCommit);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormMain";
            this.Text = "Список товаров";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.orderViewModelBindingSource)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource orderViewModelBindingSource;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button buttonCommit;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ToolStripMenuItem войтиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem зарегестрироватьсяToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource productViewModelBindingSource;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Button buttonBack;
    }
}