using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SCB.Controller;
using SCB.Domain;

namespace SCB.WinFormsGUI
{
    public class SCBTreeNode : TreeNode
    {
        private SCBNode _node;

        public SCBTreeNode(SCBNode node)
        {
            Node = node;
            base.Text = node.id + " " + node.text;

        }


        public SCBNode Node
        {
            get => _node;
            set => _node = value;
        }

        public string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }
    }
}
