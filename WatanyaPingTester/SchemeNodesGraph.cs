using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatanyaPingTester {
    class SchemeNodesGraph {
        List<Node> graph;

        public SchemeNodesGraph() {
            graph = new List<Node>();
        }

        public void addNode(Node node) {
            graph.Add(node);
        }

        public List<Node> getGraph() {
            return graph;
        }
    }
}
