using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using NUnit.Framework;

namespace DotNet.Gexf.UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

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
        public void Test1()
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
            gexf.Graph.IdType = GexfIdType.Integer;

            GexfId lat = new GexfId("lat");
            GexfId lon = new GexfId("lon");

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

            gexf.Graph.Edges.Add(new GexfEdge(1) {
                Source = UniqueId(dragon),
                Target = UniqueId(odell)
            });

            gexf.Graph.Edges.Add(new GexfEdge(2)
            {
                Source = UniqueId(odell),
                Target = UniqueId(belgium)
            });

            gexf.Graph.Edges.Add(new GexfEdge(3)
            {
                Source = UniqueId(belgium),
                Target = UniqueId(equinox)
            });

            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "pubcrawl.gexf");
            gexf.Save(path);
        }
    }
}