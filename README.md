# gexf-dotnet

A .NET/C# library to generate [GEXF](https://gephi.org/gexf/format/) (Graph Exchange XML Format) files

[![NuGet Badge](https://buildstats.info/nuget/gexf-dotnet)](https://www.nuget.org/packages/gexf-dotnet/)

## Getting Started

Install using the NuGet Package Manager

```powershell
PM> Install-Package gexf-dotnet
```

### Example Usage

```c#
void Main()
{
    var dragon = new Location(1, 40.589649m, -105.045624m, "Horse & Dragon Brewing Company");
    var odell = new Location(2, 40.589476m, -105.063186m, "Odell Brewing Company");
    var belgium = new Location(3, 40.593238m, -105.068600m, "New Belgium Brewing Company");
    var equinox = new Location(4, 40.586356m, -105.075812m, "Equinox Brewing");

    var locations = new[] { dragon, odell, belgium, equinox };

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
```

This code generates the following GEXF file:

```xml
<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<gexf xmlns="http://www.gexf.net/1.2draft" xmlns:viz="http://www.gexf.net/1.2draft/viz" version="1.2">
  <meta lastmodifieddate="2020-01-29">
    <creator>you</creator>
  </meta>
  <graph idtype="integer">
    <attributes class="node">
      <attribute id="lat" title="latitude" type="double" />
      <attribute id="lon" title="longitude" type="double" />
    </attributes>
    <nodes count="4">
      <node id="1" label="Horse &amp; Dragon Brewing Company">
        <attvalues>
          <attvalue for="lat" value="40.589649" />
          <attvalue for="lon" value="-105.045624" />
        </attvalues>
      </node>
      <node id="2" label="Odell Brewing Company">
        <attvalues>
          <attvalue for="lat" value="40.589476" />
          <attvalue for="lon" value="-105.063186" />
        </attvalues>
      </node>
      <node id="3" label="New Belgium Brewing Company">
        <attvalues>
          <attvalue for="lat" value="40.593238" />
          <attvalue for="lon" value="-105.068600" />
        </attvalues>
      </node>
      <node id="4" label="Equinox Brewing">
        <attvalues>
          <attvalue for="lat" value="40.586356" />
          <attvalue for="lon" value="-105.075812" />
        </attvalues>
      </node>
    </nodes>
    <edges count="3">
      <edge id="1" source="1" target="2" />
      <edge id="2" source="2" target="3" />
      <edge id="3" source="3" target="4" />
    </edges>
  </graph>
</gexf>
```

### Change Log

The change log is available [here](https://github.com/fallin/gexf-dotnet/blob/master/ChangeLog.md).