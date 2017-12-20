using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WatanyaPingoConsole
{
    public class SchemeNode
    {
        NetworkNode node;
        int id;
        PictureBox pic;
        Label label;

        public SchemeNode(NetworkNode n)
        {
            node = n;
            string[] arr = n.getIP().Split('.');
            id = Int32.Parse(arr[3]);
        }

        public int getID()
        {
            return id;
        }

        public void setLabel(Label l)
        {
            label = l;
        }

        public void setPic(PictureBox pb)
        {
            pic = pb;
        }

        public string getIP()
        {
            return node.getIP();
        }

        public string getName()
        {
            return node.getName();
        }

        public PictureBox getPic()
        {
            return pic;
        }

        public Label getLabel()
        {
            return label;
        }

        public NetworkNode getNode()
        {
            return node;
        }
    }
}
