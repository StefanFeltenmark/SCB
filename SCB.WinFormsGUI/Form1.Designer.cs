
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SCB.WinFormsGUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chartStats = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dataGridViewTable = new System.Windows.Forms.DataGridView();
            this.panelQuery = new System.Windows.Forms.Panel();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.checkedListBoxItems = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonTop = new System.Windows.Forms.RadioButton();
            this.radioButtonItem = new System.Windows.Forms.RadioButton();
            this.radioButtonAll = new System.Windows.Forms.RadioButton();
            this.textBoxTop = new System.Windows.Forms.TextBox();
            this.listBoxVariables = new System.Windows.Forms.ListBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.treeViewNavigation = new System.Windows.Forms.TreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControlMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartStats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).BeginInit();
            this.panelQuery.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPage1);
            this.tabControlMain.Controls.Add(this.tabPage2);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1919, 1079);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.chartStats);
            this.tabPage1.Controls.Add(this.dataGridViewTable);
            this.tabPage1.Controls.Add(this.panelQuery);
            this.tabPage1.Controls.Add(this.treeViewNavigation);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1911, 1050);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tables";
            // 
            // chartStats
            // 
            this.chartStats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chartStats.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartStats.Legends.Add(legend1);
            this.chartStats.Location = new System.Drawing.Point(483, 607);
            this.chartStats.Name = "chartStats";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartStats.Series.Add(series1);
            this.chartStats.Size = new System.Drawing.Size(1375, 435);
            this.chartStats.TabIndex = 4;
            this.chartStats.Text = "Chart";
            // 
            // dataGridViewTable
            // 
            this.dataGridViewTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTable.Location = new System.Drawing.Point(483, 195);
            this.dataGridViewTable.Name = "dataGridViewTable";
            this.dataGridViewTable.ReadOnly = true;
            this.dataGridViewTable.RowHeadersWidth = 51;
            this.dataGridViewTable.RowTemplate.Height = 24;
            this.dataGridViewTable.Size = new System.Drawing.Size(1375, 406);
            this.dataGridViewTable.TabIndex = 3;
            // 
            // panelQuery
            // 
            this.panelQuery.Controls.Add(this.buttonUpdate);
            this.panelQuery.Controls.Add(this.checkedListBoxItems);
            this.panelQuery.Controls.Add(this.groupBox1);
            this.panelQuery.Controls.Add(this.listBoxVariables);
            this.panelQuery.Controls.Add(this.labelTitle);
            this.panelQuery.Location = new System.Drawing.Point(483, 22);
            this.panelQuery.Name = "panelQuery";
            this.panelQuery.Size = new System.Drawing.Size(1375, 151);
            this.panelQuery.TabIndex = 2;
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(859, 34);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(111, 31);
            this.buttonUpdate.TabIndex = 6;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // checkedListBoxItems
            // 
            this.checkedListBoxItems.CheckOnClick = true;
            this.checkedListBoxItems.FormattingEnabled = true;
            this.checkedListBoxItems.Location = new System.Drawing.Point(1059, 14);
            this.checkedListBoxItems.Name = "checkedListBoxItems";
            this.checkedListBoxItems.Size = new System.Drawing.Size(298, 123);
            this.checkedListBoxItems.TabIndex = 5;
            this.checkedListBoxItems.Leave += new System.EventHandler(this.ReadSelectedValues);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonTop);
            this.groupBox1.Controls.Add(this.radioButtonItem);
            this.groupBox1.Controls.Add(this.radioButtonAll);
            this.groupBox1.Controls.Add(this.textBoxTop);
            this.groupBox1.Location = new System.Drawing.Point(460, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(334, 120);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // radioButtonTop
            // 
            this.radioButtonTop.AutoSize = true;
            this.radioButtonTop.Location = new System.Drawing.Point(26, 75);
            this.radioButtonTop.Name = "radioButtonTop";
            this.radioButtonTop.Size = new System.Drawing.Size(54, 21);
            this.radioButtonTop.TabIndex = 9;
            this.radioButtonTop.TabStop = true;
            this.radioButtonTop.Text = "Top";
            this.radioButtonTop.UseVisualStyleBackColor = true;
            // 
            // radioButtonItem
            // 
            this.radioButtonItem.AutoSize = true;
            this.radioButtonItem.Location = new System.Drawing.Point(26, 48);
            this.radioButtonItem.Name = "radioButtonItem";
            this.radioButtonItem.Size = new System.Drawing.Size(55, 21);
            this.radioButtonItem.TabIndex = 8;
            this.radioButtonItem.TabStop = true;
            this.radioButtonItem.Text = "Item";
            this.radioButtonItem.UseVisualStyleBackColor = true;
            this.radioButtonItem.CheckedChanged += new System.EventHandler(this.radioButtonItem_CheckedChanged);
            // 
            // radioButtonAll
            // 
            this.radioButtonAll.AutoSize = true;
            this.radioButtonAll.Location = new System.Drawing.Point(26, 21);
            this.radioButtonAll.Name = "radioButtonAll";
            this.radioButtonAll.Size = new System.Drawing.Size(44, 21);
            this.radioButtonAll.TabIndex = 7;
            this.radioButtonAll.TabStop = true;
            this.radioButtonAll.Text = "All";
            this.radioButtonAll.UseVisualStyleBackColor = true;
            this.radioButtonAll.CheckedChanged += new System.EventHandler(this.radioButtonAll_CheckedChanged);
            // 
            // textBoxTop
            // 
            this.textBoxTop.Location = new System.Drawing.Point(86, 75);
            this.textBoxTop.Name = "textBoxTop";
            this.textBoxTop.Size = new System.Drawing.Size(50, 22);
            this.textBoxTop.TabIndex = 6;
            this.textBoxTop.Text = "10";
            // 
            // listBoxVariables
            // 
            this.listBoxVariables.FormattingEnabled = true;
            this.listBoxVariables.ItemHeight = 16;
            this.listBoxVariables.Location = new System.Drawing.Point(33, 34);
            this.listBoxVariables.Name = "listBoxVariables";
            this.listBoxVariables.Size = new System.Drawing.Size(332, 100);
            this.listBoxVariables.TabIndex = 1;
            this.listBoxVariables.SelectedIndexChanged += new System.EventHandler(this.listBoxVariables_SelectedIndexChanged);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(30, 14);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(46, 17);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "label1";
            // 
            // treeViewNavigation
            // 
            this.treeViewNavigation.Location = new System.Drawing.Point(27, 22);
            this.treeViewNavigation.Name = "treeViewNavigation";
            this.treeViewNavigation.Size = new System.Drawing.Size(450, 1020);
            this.treeViewNavigation.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1911, 1050);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1919, 1079);
            this.Controls.Add(this.tabControlMain);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "SCB Browser";
            this.tabControlMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartStats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).EndInit();
            this.panelQuery.ResumeLayout(false);
            this.panelQuery.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControlMain;
        private TabPage tabPage1;
        private TreeView treeViewNavigation;
        private TabPage tabPage2;
        private Panel panelQuery;
        private Label labelTitle;
        private ListBox listBoxVariables;
        private DataGridView dataGridViewTable;
        private Chart chartStats;
        private CheckedListBox checkedListBoxItems;
        private GroupBox groupBox1;
        private TextBox textBoxTop;
        private RadioButton radioButtonTop;
        private RadioButton radioButtonItem;
        private RadioButton radioButtonAll;
        private Button buttonUpdate;
    }
}

