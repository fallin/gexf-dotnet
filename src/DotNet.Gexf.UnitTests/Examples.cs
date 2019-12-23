using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using DotNet.Gexf.Hierarchy;
using DotNet.Gexf.Phylogeny;
using DotNet.Gexf.Visualization;
using NUnit.Framework;

namespace DotNet.Gexf.UnitTests
{
    [Explicit("Examples create output files on Windows Desktop")]
    public class Examples
    {
        class Location
        {
            public Location(decimal lat, decimal lon, string label)
            {
                Lat = lat;
                Lon = lon;
                Label = label;
            }

            public decimal Lat { get; }
            public decimal Lon { get; }
            public string Label { get; }
        }

        [Test]
        public void BreweriesTopology()
        {
            ObjectIDGenerator idGenerator = new ObjectIDGenerator();

            GexfId UniqueId(Location loc)
            {
                return (int) idGenerator.GetId(loc, out bool _);
            }

            var dragon = new Location(40.589649m, -105.045624m, "Horse & Dragon Brewing Company, Craft Brewery");
            var odell = new Location(40.589476m, -105.063186m, "Odell Brewing Company");
            var belgium = new Location(40.593238m, -105.068600m, "New Belgium Brewing Company");
            var equinox = new Location(40.586356m, -105.075812m, "Equinox Brewing");

            var locations = new List<Location>()
            {
                dragon,
                odell,
                belgium,
                equinox
            };

            var gexf = new GexfDocument();
            gexf.Meta.LastModified = DateTimeOffset.Now;
            gexf.Meta.Creator = "NUnit";

            gexf.Graph.IdType = GexfIdType.Integer;

            GexfId lat = "lat";
            GexfId lon = "lon";

            gexf.Graph.NodeAttributes.AddRange(new[]
            {
                new GexfAttribute(lat)
                {
                    Title = "latitude",
                    Type = GexfDataType.Double
                },
                new GexfAttribute(lon)
                {
                    Title = "longitude",
                    Type = GexfDataType.Double
                }
            });

            foreach (var location in locations)
            {
                var node = new GexfVizNode(UniqueId(location))
                {
                    Label = location.Label,
                    AttrValues =
                    {
                        new GexfAttributeValue(lat, location.Lat),
                        new GexfAttributeValue(lon, location.Lon),
                    }
                };

                gexf.Graph.Nodes.Add(node);
            };

            gexf.Graph.Edges.AddRange(
                new GexfEdge(1, UniqueId(dragon), UniqueId(odell)),
                new GexfEdge(2, UniqueId(odell), UniqueId(belgium)),
                new GexfEdge(3, UniqueId(belgium), UniqueId(equinox))
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

            gexf.Graph.DefaultEdgeType = GexfEdgeType.Directed;
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

            gexf.Graph.DefaultEdgeType = GexfEdgeType.Directed;
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

            gexf.Graph.DefaultEdgeType = GexfEdgeType.Directed;
            gexf.Graph.Nodes.AddRange(
                new GexfParentedNode("a", "Kevin Bacon"),
                new GexfParentedNode("b", "God", "a"),
                new GexfParentedNode("c", "human1", "b"),
                new GexfParentedNode("d", "human2", "b"),
                new GexfParentedNode("e", "Me", "a"),
                new GexfParentedNode("f", "frog1", "e"),
                new GexfParentedNode("g", "frog2", "e")
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