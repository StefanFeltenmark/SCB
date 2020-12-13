using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SCB.Controller;
using SCB.Domain;


namespace SCB.WinFormsGUI
{
    public partial class Form1 : Form
    {
        private SCBController _controller;
        private SCBQuery _currentQuery;
        private SCBMetaData _currentMetaData;
        private SCBTable _currentTable;
        private SCBTreeNode _currentTreeNode;

        public Form1(SCBController controller)
        {
            _controller = controller;

            InitializeComponent();

       //     treeViewNavigation.AfterSelect += TreeViewNavigation_AfterSelect;

            SCBTreeNode rootNode = new SCBTreeNode(_controller.Current);
            rootNode.Text = "SCB";
            treeViewNavigation.Nodes.Add(rootNode);
            treeViewNavigation.TopNode = rootNode;
            treeViewNavigation.NodeMouseClick += TreeViewNavigation_NodeMouseClick;

        }

        private void TreeViewNavigation_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            _currentTreeNode = e.Node as SCBTreeNode;
            UpdateTree();
        }

    

        private void UpdateTree()
        {
            if(_currentTreeNode == null) return;

            if (_currentTreeNode.Node.IsLevel)
            {
                PopulateChildLevel(_currentTreeNode);
            }
            else if (_currentTreeNode.Node.IsTable)
            {
                LoadMetaData(_currentTreeNode);
            }
        }

        private async void LoadMetaData(SCBTreeNode node)
        {
            _currentMetaData = await _controller.GetMetaData(node.Node);
            _currentMetaData.Setup();

            UpdateMetadata();
        }

        private void UpdateMetadata()
        {
            if (_currentMetaData != null)
            {
                labelTitle.Text = _currentMetaData.title;

                listBoxVariables.Items.Clear();
                listBoxVariables.Text = "Variables";
                foreach (SCBVariable metaVariable in _currentMetaData.variables)
                {
                    listBoxVariables.Items.Add(metaVariable);
                }

                // default query
                _currentQuery = new SCBQuery();
                _currentQuery.SetUp(_currentMetaData.variables);

                listBoxVariables.SelectedIndex = 0;
                SCBVariable var = listBoxVariables.SelectedItem as SCBVariable;
                if (var != null)
                {
                    UpdateSelectedItems(var);
                }
            }
        }

        private async Task<bool> QueryDatabase(SCBTreeNode node)
        {
            _currentTable = await _controller.GetTable(node.Node, _currentQuery);
            if (_currentTable.columns == null)
            {
                MessageBox.Show("Error: no data","Alert!", MessageBoxButtons.OK);
                return false;
            }

            _currentTable.Setup();

            return true;
        }

        private void UpdateGridView()
        {
            dataGridViewTable.Columns.Clear();
            foreach (SCBColumn tableColumn in _currentTable.columns)
            {
                dataGridViewTable.Columns.Add(tableColumn.code, tableColumn.text);
            }

            int nRows = _currentTable.data.Count;
            dataGridViewTable.Rows.Add(nRows);
            int row = 0;
            foreach (SCBTableEntry tableEntry in _currentTable.data)
            {
                int col = 0;
                // NB variables need not appear in same order as keys/values!
                foreach (string s in tableEntry.key)
                {
                    string text = s;

                    SCBVariable var = _currentMetaData._keyToVariable[_currentTable.columns[col].text];
                    int index = var.values.IndexOf(s);
                    text = var.valueTexts[index];

                    dataGridViewTable.Rows[row].Cells[col].Value = text;
                    ++col;
                }
                foreach (string s in tableEntry.values)
                {

                    dataGridViewTable.Rows[row].Cells[col].Value = s;
                    ++col;
                }

                row++;
            }
        }

        private void UpdateChart()
        {
            int timeCol = 0;
            int nValueSeries = 0;
            int col = 0;
            bool hasTime = false;
            List<int> valueIndices = new List<int>();
            foreach (SCBColumn tableColumn in _currentTable.columns)
            {
                if (tableColumn.type == "t")
                {
                    hasTime = true;
                    timeCol = col;
                }

                if (tableColumn.type == "c")
                {
                    valueIndices.Add(col);
                    nValueSeries++;
                }
                col++;
            }

            chartStats.Series.Clear();
            foreach (SCBTimeSeries series in _currentTable._timeSeries)
            {
                Series s = chartStats.Series.Add(series.Name);
                s.BorderWidth = 2;
                s.ChartType = SeriesChartType.Line;
                foreach (KeyValuePair<DateTime, double> keyValuePair in series)
                {
                    s.Points.AddXY(keyValuePair.Key, keyValuePair.Value);
                }
            }

            chartStats.ChartAreas.First().AxisX.Name = "Time";
            chartStats.ChartAreas.First().Name = "Test";
            chartStats.ChartAreas.First().BackColor = Color.Transparent;

        }

     

        private async void PopulateChildLevel(SCBTreeNode node)
        {
            
            node.Nodes.Clear();

            List<SCBNode> children = await _controller.GetChildren(node.Node);

            foreach (SCBNode child in children)
            {
                TreeNode newNode = new SCBTreeNode(child);
                
                node.Nodes.Add(newNode);
            }
        }

     

        private void listBoxVariables_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (listBoxVariables.SelectedItem is SCBVariable var)
            {

                SCBQueryItem item = _currentQuery._items[var.code];

                if (item.selection.filter.Equals("all"))
                {
                    radioButtonAll.Checked = true;
                }
                else if (item.selection.filter.Equals("item"))
                {
                    radioButtonItem.Checked = true;

                }
                else if (item.selection.filter.Equals("top"))
                {
                    radioButtonTop.Checked = true;
                }
                UpdateSelectedItems(var);
            }


        }

        private void radioButtonAll_CheckedChanged(object sender, System.EventArgs e)
        {
            if (radioButtonAll.Checked)
            {
                if (listBoxVariables.SelectedItem is SCBVariable var)
                {
                    if (!_currentQuery._items[var.code].selection.filter.Equals(SCBSelection.allString))
                    {
                        _currentQuery.SetSelection(var,SCBQuery.Filter.all,null);
                    }
                }
            }
        }

        private void radioButtonItem_CheckedChanged(object sender, System.EventArgs e)
        {
            if (radioButtonItem.Checked)
            {
                if (listBoxVariables.SelectedItem is SCBVariable var)
                {
                    UpdateSelectedItems(var);
                    _currentQuery._items[var.code].selection.filter = SCBSelection.itemString;
                }
            }
           
        }

        private void UpdateSelectedItems(SCBVariable var)
        {
            checkedListBoxItems.Items.Clear();
            foreach (SCBValue varValue in var.Values)
            {
                checkedListBoxItems.Items.Add(varValue, varValue.IsSelected);
            }
        }

        public void UpdateTableAndChart()
        {
            UpdateChart();
            UpdateGridView();
        }

        private void buttonUpdate_Click(object sender, System.EventArgs e)
        {
            _currentQuery.Update(); // prepare dto's

            bool success = false;
            try
            {
                success = QueryDatabase(_currentTreeNode).Result;
            }
            catch (Exception exception)
            {
                MessageBox.Show($"An error occurred: {exception.Message}", "Alert!", MessageBoxButtons.OK);

            }
            

            if (success)
            {
                UpdateTableAndChart();
            }
        }

    

        private void ReadSelectedValues(object sender, System.EventArgs e)
        {
            if (radioButtonItem.Checked)
            {
                if (listBoxVariables.SelectedItem is SCBVariable var)
                {
                    foreach (SCBValue varValue in var.Values)
                    {
                        varValue.IsSelected = false;
                    }
                    foreach (object item in checkedListBoxItems.CheckedItems)
                    {
                        SCBValue val = (SCBValue) item;
                        val.IsSelected = true;
                    }
                }
            }
        }
    }
}
