using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Gexf.Hierarchy;
using Gexf.Phylogeny;
using NUnit.Framework;

namespace Gexf.UnitTests
{
    [Explicit("Examples create output files on Windows Desktop")]
    public class Examples
    {
        class Location
        {
            public Location(int id, decimal lat, decimal lon, string label)
            {
                Id = id;
                Lat = lat;
                Lon = lon;
                Label = label;
            }

            public int Id { get; }
            public decimal Lat { get; }
            public decimal Lon { get; }
            public string Label { get; }
        }

        [Test]
        public void BreweriesTopology()
        {
            var dragon = new Location(1, 40.589649m, -105.045624m, "Horse & Dragon Brewing Company");
            var odell = new Location(2, 40.589476m, -105.063186m, "Odell Brewing Company");
            var belgium = new Location(3, 40.593238m, -105.068600m, "New Belgium Brewing Company");
            var equinox = new Location(4, 40.586356m, -105.075812m, "Equinox Brewing");

            var locations = new [] { dragon, odell, belgium, equinox };

            var gexf = new GexfDocument();
            gexf.Meta.LastModified = DateTimeOffset.Now;
            gexf.Meta.Creator = Environment.UserName;

            gexf.Graph.IdType = GexfIdType.Integer;

            GexfId lat = "lat";
            GexfId lon = "lon";

            gexf.Graph.NodeAttributes.AddRange(
                new GexfAttribute(lat, "latitude", GexfDataType.Double), 
                new GexfAttribute(lon, "longitude", GexfDataType.Double)
                );

            gexf.Graph.Nodes.AddRange(locations.Select(location => 
                new GexfNode(location.Id)
                {
                    Label = location.Label,
                    AttrValues =
                    {
                        new GexfAttributeValue(lat, location.Lat),
                        new GexfAttributeValue(lon, location.Lon)
                    }
                }));

            gexf.Graph.Edges.AddRange(
                new GexfEdge(1, dragon.Id, odell.Id),
                new GexfEdge(2, odell.Id, belgium.Id),
                new GexfEdge(3, belgium.Id, equinox.Id)
                );

            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "breweries.gexf");
            
            gexf.Save(path);
        }

        [Test]
        public void HierarchySequentialSafe()
        {
            var gexf = new GexfDocument();

            gexf.Graph.DefaultedEdgeType = GexfEdgeType.Directed;
            gexf.Graph.Nodes.AddRange(
                new GexfHierarchicalNode("a", "Kevin Bacon")
                {
                    Nodes = {
                        new GexfHierarchicalNode("b", "God")
                        {
                            Nodes = {
                                new GexfNode("c", "human1"),
                                new GexfNode("d", "human2")
                            }
                        }
                    }
                },
                new GexfHierarchicalNode("e", "Me")
                {
                    Nodes = { 
                        new GexfNode("f", "frog1"),
                        new GexfNode("g", "frog2")
                    }
                });
            gexf.Graph.Edges.AddRange(new []
            {
                new GexfEdge(0, "b", "e"),
                new GexfEdge(1, "c", "d"),
                new GexfEdge(2, "g", "b"),
                new GexfEdge(3, "f", "a")
            });

            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "hierarchy-sequential-safe.gexf");
            gexf.Save(path);
        }

        [Test]
        public void HierarchySequentialSafeWithEdgesInsideClusters()
        {
            var gexf = new GexfDocument();

            gexf.Graph.DefaultedEdgeType = GexfEdgeType.Directed;
            gexf.Graph.Nodes.AddRange(
                new GexfHierarchicalNode("a", "Kevin Bacon")
                {
                    Nodes = {
                        new GexfHierarchicalNode("b", "God")
                        {
                            Nodes = {
                                new GexfNode("c", "human1"),
                                new GexfNode("d", "human2"),
                            },
                            Edges = {
                                new GexfEdge(0, "c", "d")
                            }
                        }
                    }
                },
                new GexfHierarchicalNode("e", "Me")
                {
                    Nodes = { 
                        new GexfNode("f", "frog1"),
                        new GexfNode("g", "frog2")
                    }
                });
            gexf.Graph.Edges.AddRange(
                new GexfEdge(1, "b", "e"),
                new GexfEdge(3, "f", "a"),
                new GexfEdge(2, "g", "b")
                );

            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "hierarchy-sequential-safe-edges-inside.gexf");
            gexf.Save(path);
        }

        [Test]
        public void HierarchyWithRandomWriting()
        {
            var gexf = new GexfDocument();

            gexf.Graph.DefaultedEdgeType = GexfEdgeType.Directed;
            gexf.Graph.Nodes.AddRange(
                new GexfNode("a", "Kevin Bacon"),
                new GexfNode("b", "God")
                    .Parent("a"),
                new GexfNode("c", "human1")
                    .Parent("b"),
                new GexfNode("d", "human2")
                    .Parent("b"),
                new GexfNode("e", "Me")
                    .Parent("a"),
                new GexfNode("f", "frog1")
                    .Parent("e"),
                new GexfNode("g", "frog2")
                    .Parent("e")
            );

            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "hierarchy-random-writing.gexf");
            gexf.Save(path);
        }

        [Test]
        public void PhylogenyExample()
        {
            var gexf = new GexfDocument();

            gexf.Graph.Nodes.AddRange(
                new GexfPhylogenyNode("a", "cheese"),
                new GexfPhylogenyNode("b", "cherry"),
                new GexfPhylogenyNode("c", "cake")
                {
                    Parents = { "a", "b" }
                });
            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "phylogeny.gexf");
            gexf.Save(path);
        }
    }
}