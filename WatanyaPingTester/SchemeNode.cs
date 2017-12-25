﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WatanyaPingTester
{
    class SchemeNode
    {
        NetworkNode node;
        int id;
        PictureBox pic;
        Label label;
        bool visible = true;

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

        public bool isVisible()
        {
            return visible;
        }

        public void setVisiblility(bool visible)
        {
            this.visible = visible;
        }

        public string getPreviousNode()
        {
            return node.getPreviousNode();
        }

        public NetworkNode getNode()
        {
            return node;
        }
    }
}
