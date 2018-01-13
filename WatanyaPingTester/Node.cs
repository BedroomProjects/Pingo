using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatanyaPingTester {
    class Node {
        List<Node> parents;
        public Node() {
            parents = new List<Node>();
        }

        public void addParent(Node parent) {
            parents.Add(parent);
        }

        public List<Node> getParent() {
            return parents;
        }
    }
}
